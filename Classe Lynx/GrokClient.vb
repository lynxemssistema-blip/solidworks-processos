Imports System.Collections.Generic
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks
Imports Newtonsoft.Json

' ===========================================================
'  Cliente Grok (xAI) – VB.NET 4.7
'  - HttpClient singleton com timeout configurável
'  - Função Async ObterRespostaGPT (mesmo nome que você usa)
'  - Validações, retries exponenciais p/ 429/5xx
'  - Tipos p/ desserialização da resposta xAI
'  - Evita hardcode de chave em código-fonte (injeção por parâmetro)
' ===========================================================
Public Module GrokClient

    ' HttpClient único e reaproveitado (thread-safe)
    Private ReadOnly _http As HttpClient = CreateHttpClient()

    Private Function CreateHttpClient() As HttpClient
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim handler = New HttpClientHandler() With {
            .AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
        }

        Dim client = New HttpClient(handler) With {
            .BaseAddress = New Uri("https://api.x.ai/"),
            .Timeout = TimeSpan.FromSeconds(90)
        }

        client.DefaultRequestHeaders.Clear()
        client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json")
        client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "SwLynx-GrokClient/1.0")

        Return client
    End Function

    ' -----------------------------------------------------------
    ' Assinatura compatível com seu uso atual
    ' - prompt: conteúdo do usuário
    ' - Agente: system prompt
    ' - apiKey: passe por parâmetro (NÃO deixar hardcoded)
    ' - model/temperature: opcionais
    ' -----------------------------------------------------------
    Public Async Function ObterRespostaGPT(prompt As String,
                                           Agente As String,
                                           Optional apiKey As String = Nothing,
                                           Optional model As String = "grok-4-latest",
                                           Optional temperature As Double = 0.7,
                                           Optional cancel As CancellationToken = Nothing) As Task(Of String)

        If String.IsNullOrWhiteSpace(prompt) Then
            Return "Prompt vazio."
        End If

        If String.IsNullOrWhiteSpace(Agente) Then
            Agente = "Você é um assistente especializado em VB.NET, MySQL e melhores práticas de design robusto."
        End If

        ' Permite injetar via parâmetro ou variável de ambiente
        Dim key As String = If(apiKey,
                               Environment.GetEnvironmentVariable("XAI_API_KEY"),
                               Environment.GetEnvironmentVariable("XAI_API_KEY"))

        If String.IsNullOrWhiteSpace(key) Then
            Return "Chave de API ausente. Defina o parâmetro 'apiKey' ou a variável de ambiente XAI_API_KEY."
        End If

        ' Payload conforme /v1/chat/completions da xAI
        Dim body = New ChatCompletionsRequest With {
            .model = model,
            .temperature = temperature,
            .stream = False,
            .messages = New List(Of ChatMessage) From {
                New ChatMessage With {.role = "system", .content = Agente},
                New ChatMessage With {.role = "user", .content = prompt}
            }
        }

        Dim json As String = JsonConvert.SerializeObject(body)
        Dim content = New StringContent(json, Encoding.UTF8, "application/json")

        ' Header de autorização por requisição (seguro caso mude a chave em runtime)
        If _http.DefaultRequestHeaders.Contains("Authorization") Then
            _http.DefaultRequestHeaders.Remove("Authorization")
        End If
        _http.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " & key)

        ' Retry exponencial básico para 429/5xx
        Dim maxAttempts As Integer = 3
        Dim attempt As Integer = 0
        Dim lastError As String = Nothing

        While attempt < maxAttempts
            attempt += 1
            '  Try
            Dim resp As HttpResponseMessage = Await _http.PostAsync("v1/chat/completions", content, cancel)
            Dim respText As String = Await resp.Content.ReadAsStringAsync()

            If resp.IsSuccessStatusCode Then
                Dim parsed = JsonConvert.DeserializeObject(Of ChatCompletionsResponse)(respText)

                If parsed IsNot Nothing AndAlso
                   parsed.choices IsNot Nothing AndAlso
                   parsed.choices.Count > 0 AndAlso
                   parsed.choices(0).message IsNot Nothing AndAlso
                   Not String.IsNullOrWhiteSpace(parsed.choices(0).message.content) Then

                    Return parsed.choices(0).message.content.Trim()
                End If

                ' Resposta sem conteúdo útil
                Return "A API respondeu sem conteúdo em choices[0].message.content."
            Else
                ' Tenta extrair erro estruturado
                Dim apiErr = TryDeserialize(Of ApiErrorEnvelope)(respText)
                Dim errMsg As String = $"HTTP {(CInt(resp.StatusCode))} - {resp.ReasonPhrase}"
                If apiErr IsNot Nothing AndAlso apiErr.err IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(apiErr.err.message) Then
                    errMsg &= " | " & apiErr.err.message
                ElseIf apiErr IsNot Nothing AndAlso apiErr.error IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(apiErr.error.message) Then
                    errMsg &= " | " & apiErr.error.message
                Else
                    errMsg &= " | " & respText
                End If

                ' Repetir em 429/5xx
                If resp.StatusCode = CType(429, HttpStatusCode) OrElse CInt(resp.StatusCode) >= 500 Then
                    lastError = errMsg
                    Await Task.Delay(GetBackoffDelay(attempt), cancel)
                    Continue While
                End If

                ' Erros 4xx não são retentáveis
                Return "Erro HTTP da API: " & errMsg
            End If

            'Catch ex As TaskCanceledException (When Not cancel.IsCancellationRequested
            '    lastError = "Timeout na chamada à API."
            '    Await Task.Delay(GetBackoffDelay(attempt), CancellationToken.None)
            'Catch ex As Exception(
            '    lastError = "Exceção na chamada à API: " & ex.Message
            '    Await Task.Delay(GetBackoffDelay(attempt), CancellationToken.None)
            'End Try
        End While

        Return If(lastError, "Falha desconhecida ao contatar a API.")
    End Function

    Private Function GetBackoffDelay(attempt As Integer) As TimeSpan
        ' 1s, 2s, 4s (máx 4s)
        Dim ms = Math.Min(4000, CInt(1000 * Math.Pow(2, attempt - 1)))
        Return TimeSpan.FromMilliseconds(ms)
    End Function

    Private Function TryDeserialize(Of T)(json As String) As T
        Try
            Return JsonConvert.DeserializeObject(Of T)(json)
        Catch
            Return Nothing
        End Try
    End Function

    ' ===================== Tipos (Request/Response) =====================

    Private Class ChatCompletionsRequest
        Public Property model As String
        Public Property messages As List(Of ChatMessage)
        Public Property temperature As Double?
        Public Property stream As Boolean?
    End Class

    Private Class ChatMessage
        Public Property role As String  ' "system" | "user" | "assistant"
        Public Property content As String
    End Class

    Private Class ChatCompletionsResponse
        Public Property id As String
        Public Property model As String
        Public Property choices As List(Of Choice)
        Public Property usage As Usage
        Public Property created As Long?
        Public Property [object] As String
    End Class

    Private Class Choice
        Public Property index As Integer
        Public Property message As Message
        Public Property finish_reason As String
    End Class

    Private Class Message
        Public Property role As String
        Public Property content As String
    End Class

    Private Class Usage
        Public Property prompt_tokens As Integer?
        Public Property completion_tokens As Integer?
        Public Property total_tokens As Integer?
    End Class

    ' Alguns payloads de erro da xAI seguem chave "error", outros "err"
    Private Class ApiErrorEnvelope
        Public Property [error] As ApiError
        Public Property err As ApiError
    End Class

    Private Class ApiError
        Public Property message As String
        Public Property type As String
        Public Property param As String
        Public Property code As String
    End Class

End Module