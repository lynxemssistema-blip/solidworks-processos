Imports System.Collections.Generic
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Newtonsoft.Json
Imports Environment = System.Environment

Public Class clAjudaAI

    Private Shared ReadOnly client As New HttpClient()

    Public Async Function ObterRespostaGPT(prompt As String, Agente As String) As Task(Of String)

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim apiKey As String = "SUA_CHAVE_AQUI" ' <-- Substitua pela sua chave da OpenAI
        Dim url As String = "https://api.openai.com/v1/chat/completions"

        Dim requestData = New With {
            .model = "gpt-4o",
            .messages = New Object() {
                New With {.role = "system", .content = Agente},
                New With {.role = "user", .content = prompt}
            },
            .temperature = 0.7
        }

        Dim json = JsonConvert.SerializeObject(requestData)
        Dim content = New StringContent(json, Encoding.UTF8, "application/json")

        client.DefaultRequestHeaders.Clear()
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " & apiKey)

        Try
            Dim response = Await client.PostAsync(url, content)
            Dim responseString = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim jsonResponse As OpenAIResponse = JsonConvert.DeserializeObject(Of OpenAIResponse)(responseString)

                If jsonResponse IsNot Nothing AndAlso jsonResponse.choices.Count > 0 Then
                    Return jsonResponse.choices(0).message.content.Trim()
                Else
                    Return "A resposta da API não continha escolhas válidas."
                End If
            Else
                Return "Erro HTTP da API: " & response.StatusCode & Environment.NewLine & responseString
            End If
        Catch ex As Exception
            Return "Erro: " & ex.Message
        End Try
    End Function

    Dim rows As New List(Of Dictionary(Of String, Object))

    Public Function DataGridViewToJson(dgv As DataGridView) As String

        For Each row As DataGridViewRow In dgv.Rows
            If Not row.IsNewRow Then ' Ignora a última linha em branco
                Dim dict As New Dictionary(Of String, Object)
                For Each cell As DataGridViewCell In row.Cells
                    Dim columnName As String = dgv.Columns(cell.ColumnIndex).HeaderText
                    dict(columnName) = cell.Value
                Next
                rows.Add(dict)
            End If
        Next

        ' Serializa a lista de dicionários em JSON formatado
        Return JsonConvert.SerializeObject(rows, Formatting.Indented)

    End Function

    ' Classes para deserializar a resposta da OpenAI
    Public Class OpenAIResponse
        Public Property choices As List(Of Choice)
    End Class

    Public Class Choice
        Public Property message As Message
        Public Property finish_reason As String
        Public Property index As Integer
    End Class

    Public Class Message
        Public Property role As String
        Public Property content As String
    End Class

End Class

Public Class clAjudaAI_Arquivos

    Private Shared ReadOnly http As New HttpClient()

    Private Function GetApiKey() As String

        Dim apiKey As String = "SUA_CHAVE_AQUI" ' <-- Substitua pela sua chave da OpenAI

        ' 1) Variável de ambiente
        Dim key As String = Environment.GetEnvironmentVariable(apiKey)
        ' 2) (Opcional) App.config -> My.Settings.OpenAIKey
        If String.IsNullOrWhiteSpace(key) Then
            Try
                key = apiKey
            Catch
            End Try
        End If
        If String.IsNullOrWhiteSpace(key) Then
            Throw New ApplicationException("Configure a OPENAI_API_KEY (variável de ambiente ou My.Settings.OpenAIKey).")
        End If
        Return key
    End Function

    ''' <summary>
    ''' Fluxo simples: faz upload do PDF e envia para análise no Responses API.
    ''' </summary>
    Public Async Function AnalisarPdfAsync(pdfPath As String,
                                           prompt As String,
                                           Optional modelo As String = "gpt-4o") As Task(Of String)

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim apiKey = GetApiKey()

        ' 1) Upload do arquivo -> /v1/files (purpose=assistants)
        Dim fileId As String = Await UploadFileAsync(pdfPath, apiKey)

        ' 2) Chamada ao Responses API incluindo o file_id como "input_file"
        Dim url As String = "https://api.openai.com/v1/responses"

        ' Estrutura mínima do Responses API com arquivo + texto
        Dim requestObj = New With {
            .model = modelo,
            .input = New Object() {
                New With {
                    .role = "user",
                    .content = New Object() {
                        New With {.type = "input_text", .text = prompt},
                        New With {.type = "input_file", .file_id = fileId}
                    }
                }
            }
        }

        Dim jsonBody As String = JsonConvert.SerializeObject(requestObj)
        Dim content As New StringContent(jsonBody, Encoding.UTF8, "application/json")

        http.DefaultRequestHeaders.Clear()
        http.DefaultRequestHeaders.Add("Authorization", "Bearer " & apiKey)

        Dim resp = Await http.PostAsync(url, content)
        Dim body = Await resp.Content.ReadAsStringAsync()

        If Not resp.IsSuccessStatusCode Then
            Return $"Erro HTTP ({CInt(resp.StatusCode)}): {resp.ReasonPhrase}" & Environment.NewLine & body
        End If

        ' Desserializa apenas o necessário do Responses API
        Dim parsed = JsonConvert.DeserializeObject(Of ResponsesMinimal)(body)

        ' O texto geralmente vem em output[*].content[*].text
        Dim texto As String = TryExtractText(parsed)
        If String.IsNullOrWhiteSpace(texto) Then
            ' Fallback: retorna o JSON para inspeção
            Return body
        End If
        Return texto
    End Function

    ''' <summary>
    ''' Faz upload do arquivo ao Files API (purpose=assistants) e retorna file_id.
    ''' </summary>
    Private Async Function UploadFileAsync(filePath As String, apiKey As String) As Task(Of String)
        Dim url As String = "https://api.openai.com/v1/files"

        Using form As New MultipartFormDataContent()
            ' purpose=assistants é o indicado para uso com Responses/File Search
            form.Add(New StringContent("assistants"), "purpose")

            Dim fileBytes As Byte() = IO.File.ReadAllBytes(filePath)
            Dim fileContent As New ByteArrayContent(fileBytes)
            fileContent.Headers.ContentType = New Headers.MediaTypeHeaderValue("application/pdf")

            ' Nome do campo precisa ser "file"
            form.Add(fileContent, "file", IO.Path.GetFileName(filePath))

            http.DefaultRequestHeaders.Clear()
            http.DefaultRequestHeaders.Add("Authorization", "Bearer " & apiKey)

            Dim resp = Await http.PostAsync(url, form)
            Dim body = Await resp.Content.ReadAsStringAsync()

            If Not resp.IsSuccessStatusCode Then
                Throw New ApplicationException("Falha no upload do arquivo: " & body)
            End If

            Dim uploaded = JsonConvert.DeserializeObject(Of OpenAIFileUploadResponse)(body)
            If uploaded Is Nothing OrElse String.IsNullOrWhiteSpace(uploaded.id) Then
                Throw New ApplicationException("Upload sem retorno de file_id.")
            End If
            Return uploaded.id
        End Using
    End Function

    ' ----------------- MODELOS DE RESPOSTA (mínimos) -----------------

    Private Class OpenAIFileUploadResponse
        Public Property id As String
        Public Property [object] As String
        Public Property bytes As Integer
        Public Property created_at As Long
        Public Property filename As String
        Public Property purpose As String
    End Class

    Private Class ResponsesMinimal
        Public Property id As String
        Public Property output As List(Of OutputItem)
        Public Property text As Object ' alguns clients retornam text.flatten
    End Class

    Private Class OutputItem
        Public Property type As String  ' "message", "file_search_call", etc.
        Public Property content As List(Of OutputContent)
        Public Property role As String
    End Class

    Private Class OutputContent
        Public Property type As String  ' "output_text"
        Public Property text As String
        ' Pode haver "annotations" aqui, se usar file_search
    End Class

    Private Function TryExtractText(parsed As ResponsesMinimal) As String
        If parsed Is Nothing OrElse parsed.output Is Nothing Then Return Nothing
        Dim sb As New StringBuilder()
        For Each item In parsed.output
            If item?.content Is Nothing Then Continue For
            For Each c In item.content
                If c IsNot Nothing AndAlso c.type = "output_text" AndAlso Not String.IsNullOrWhiteSpace(c.text) Then
                    sb.AppendLine(c.text)
                End If
            Next
        Next
        Dim result = sb.ToString().Trim()
        Return If(result, Nothing)
    End Function

End Class