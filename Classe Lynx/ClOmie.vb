' Pseudocódigo detalhado:
' 1. Verificar se o payload está correto conforme a documentação da API Omie.
' 2. Verificar se os nomes dos campos da classe RespostaOmie batem com o JSON retornado.
' 3. Adicionar cabeçalhos necessários na requisição HTTP.
' 4. Tratar possíveis erros de autenticação ou formato de payload.

Imports System.Collections.Generic
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Newtonsoft.Json

Public Class ClOmie

    Private ReadOnly OmieUrl As String = "https://app.omie.com.br/api/v1/geral/produtos/"
    Private ReadOnly AppKey As String = "6142741857252"
    Private ReadOnly AppSecret As String = "f105a79fabb5629cb7fbe37c01a1d160"

    Public Async Function ChamarOmieAsync01() As Task
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim json As String = "{""call"":""ListarProdutos"",""app_key"":""6142741857252"",""app_secret"":""f105a79fabb5629cb7fbe37c01a1d160"",""param"":[{""pagina"":1,""registros_por_pagina"":50,""apenas_importado_api"":""N"",""filtrar_apenas_omiepdv"":""N""}]}"

        Using client As New HttpClient()
            Dim request As New HttpRequestMessage(HttpMethod.Post, "https://app.omie.com.br/api/v1/geral/produtos/")
            request.Content = New ByteArrayContent(Encoding.UTF8.GetBytes(json))
            request.Content.Headers.Remove("Content-Type")
            request.Content.Headers.TryAddWithoutValidation("Content-Type", "application/json")
            request.Headers.TryAddWithoutValidation("User-Agent", "VB.NET-App")

            Dim response As HttpResponseMessage = Await client.SendAsync(request)
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                MessageBox.Show("Produtos listados com sucesso! 🎉", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show($"Erro Omie 🚨{vbCrLf}Status: {response.StatusCode}{vbCrLf}Resposta: {responseBody}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Using
    End Function

    'Função que busca e joga no DataGridView
    Public Async Function CarregarProdutosOmieAsync(dgv As DataGridView) As Task
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim json As String = "{""call"":""ListarProdutos"",""app_key"":""6142741857252"",""app_secret"":""f105a79fabb5629cb7fbe37c01a1d160"",""param"":[{""pagina"":1,""registros_por_pagina"":50,""apenas_importado_api"":""N"",""filtrar_apenas_omiepdv"":""N""}]}"

        Using client As New HttpClient()
            Dim request As New HttpRequestMessage(HttpMethod.Post, "https://app.omie.com.br/api/v1/geral/produtos/")
            request.Content = New ByteArrayContent(Encoding.UTF8.GetBytes(json))
            request.Content.Headers.Remove("Content-Type")
            request.Content.Headers.TryAddWithoutValidation("Content-Type", "application/json")
            request.Headers.TryAddWithoutValidation("User-Agent", "VB.NET-App")

            Dim response As HttpResponseMessage = Await client.SendAsync(request)
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                ' Dim resultado As RespostaOmieProdutos = JsonConvert.DeserializeObject(Of RespostaOmieProdutos)(responseBody)

                Dim jsonString As String = responseBody

                Dim wrapper As ProdutoWrapper = JsonConvert.DeserializeObject(Of ProdutoWrapper)(jsonString)
                Dim listaProdutos As List(Of Produto) = wrapper.produto_servico_cadastro

                dgv.DataSource = listaProdutos
                ' MessageBox.Show("Produtos carregados com sucesso! 🟢", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ' MessageBox.Show($"Erro Omie 🚨{vbCrLf}Status: {response.StatusCode}{vbCrLf}Resposta: {responseBody}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Using

    End Function

    Dim dtomie As New DataTable()

    Public Async Function CarregarProdutosOmieAsyncDataTable(desc1 As String, desc2 As String, desc3 As String, cod As String) As Task(Of DataTable)

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim primeiraPagina As Boolean = True

        Dim json As String = "{""call"":""ListarProdutos"",""app_key"":""341143325523"",
""app_secret"":""8004c3fe1663d1360a8408c93c57c253"",
""param"":
[{""pagina"":1,""registros_por_pagina"":50,
""apenas_importado_api"":""N"",
""filtrar_apenas_omiepdv"":""N"",
""filtrar_apenas_descricao"":""%" & desc1 & "%"",
""filtrar_apenas_descricao"":""%" & desc2 & "%"",
""filtrar_apenas_descricao"":""%" & desc3 & "%"",
""produtosPorCodigo"":""%" & cod & "%""
}]}"

        Using client As New HttpClient()
            Dim request As New HttpRequestMessage(HttpMethod.Post, "https://app.omie.com.br/api/v1/geral/produtos/")
            request.Content = New ByteArrayContent(Encoding.UTF8.GetBytes(json))
            request.Content.Headers.TryAddWithoutValidation("Content-Type", "application/json")
            request.Headers.TryAddWithoutValidation("User-Agent", "VB.NET-App")

            Dim response As HttpResponseMessage = Await client.SendAsync(request)
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            If response.IsSuccessStatusCode Then
                Dim wrapper As ProdutoWrapper = JsonConvert.DeserializeObject(Of ProdutoWrapper)(responseBody)
                Dim listaProdutos As List(Of Produto) = wrapper.produto_servico_cadastro
                'Dim dt As DataTable = ConverterProdutosParaDataTable(listaProdutos)
                Return ConverterProdutosParaDataTable(wrapper.produto_servico_cadastro)
            Else
                Throw New Exception($"Erro Omie 🚨{vbCrLf}Status: {response.StatusCode}{vbCrLf}Resposta: {responseBody}")
            End If
        End Using

    End Function

    Public Function ConverterProdutosParaDataTable(lista As List(Of Produto)) As DataTable

        ' Obter somente propriedades públicas não nulas e que não estão comentadas
        Dim props = GetType(Produto).GetProperties()

        '' Criar colunas no DataTable com base nas propriedades
        'For Each prop In props
        '    dt.Columns.Add(prop.Name, If(Nullable.GetUnderlyingType(prop.PropertyType), prop.PropertyType))
        'Next

        ' Criar colunas no DataTable com base nas propriedades
        For Each prop In props
            Dim nomeColuna As String = prop.Name
            If Not dtomie.Columns.Contains(nomeColuna) Then
                dtomie.Columns.Add(nomeColuna, If(Nullable.GetUnderlyingType(prop.PropertyType), prop.PropertyType))
            End If
        Next

        ' Adicionar os dados das propriedades na tabela
        For Each item In lista
            Dim row = dtomie.NewRow()
            For Each prop In props
                row(prop.Name) = If(prop.GetValue(item, Nothing), DBNull.Value)
            Next
            dtomie.Rows.Add(row)
        Next

        Return dtomie
    End Function

End Class

Public Class InfoProduto
    Public Property dAlt As String
    Public Property dInc As String
    Public Property hAlt As String
    Public Property hInc As String
    Public Property uAlt As String
    Public Property uInc As String
End Class

Public Class RecomendacoesFiscais
    Public Property cnpj_fabricante As String
    Public Property cupom_fiscal As String
    Public Property id_cest As String
    Public Property id_preco_tabelado As Integer
    Public Property indicador_escala As String
    Public Property market_place As String
    Public Property origem_mercadoria As String
End Class

Public Class Produto

    'Public Property aliquota_cofins As Decimal
    'Public Property aliquota_ibpt As Decimal
    Public Property aliquota_icms As Decimal

    'Public Property aliquota_pis As Decimal
    'Public Property altura As Decimal
    'Public Property bloqueado As String
    'Public Property bloquear_exclusao As String
    'Public Property cest As String
    'Public Property cfop As String
    'Public Property codInt_familia As String
    Public Property codigo As String

    'Public Property codigo_beneficio As String
    'Public Property codigo_familia As Integer
    Public Property codigo_produto As Long

    'Public Property codigo_produto_integracao As String
    Public Property csosn_icms As String

    'Public Property cst_cofins As String
    Public Property cst_icms As String

    'Public Property cst_pis As String
    Public Property descr_detalhada As String

    Public Property descricao As String

    'Public Property descricao_familia As String
    'Public Property dias_crossdocking As Integer
    'Public Property dias_garantia As Integer
    'Public Property ean As String
    'Public Property estoque_minimo As Decimal
    'Public Property exibir_descricao_nfe As String
    'Public Property exibir_descricao_pedido As String
    'Public Property importado_api As String
    'Public Property inativo As String
    'Public Property info As InfoProduto
    'Public Property largura As Decimal
    'Public Property lead_time As Integer
    Public Property marca As String

    Public Property modelo As String

    'Public Property motivo_deson_icms As String
    'Public Property ncm As String
    'Public Property obs_internas As String
    'Public Property per_icms_fcp As Decimal
    'Public Property peso_bruto As Decimal
    Public Property peso_liq As Decimal

    'Public Property profundidade As Decimal
    Public Property quantidade_estoque As Decimal

    'Public Property recomendacoes_fiscais As RecomendacoesFiscais
    'Public Property red_base_cofins As Decimal
    'Public Property red_base_icms As Decimal
    'Public Property red_base_pis As Decimal
    'Public Property tipoItem As String
    Public Property unidade As String

    Public Property valor_unitario As Decimal

End Class

Public Class ProdutoWrapper
    Public Property produto_servico_cadastro As List(Of Produto)
End Class