Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class frmCriaProdutos

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        OrdemServico.ProdutoPadrao = "SIM"
        OrdemServico.CodDesenhoProduto = Me.txCodDesenhoProduto.Text
        OrdemServico.CodOmie = Me.txtCodOmie.Text
        OrdemServico.DescricaoProduto = Me.txtDescricaoProduto.Text
        OrdemServico.EnderecoFichaTecnica = Me.lblEnderecoFichaTecnica.Text
        OrdemServico.EnderecoIsometrico = Me.lblEnderecoIsometrico.Text
        OrdemServico.ProdutoCriadoPor = Usuario.NomeCompleto
        OrdemServico.DataCriacaoProduto = Date.Now.Date.ToShortDateString

        ValidarDadosOrdemServico()

        '  MsgBox("OS Transformada em produto com Sucesso!", vbInformation, "Atenção")

    End Sub

    Private Sub ValidarDadosOrdemServico()
        ' Verifica se o ID do item da ordem de serviço é válido
        If Not ValidarIDOrdemServico() Then Exit Sub

        ' Verifica se o código de desenho do produto foi informado
        If Not ValidarCampoObrigatorio(OrdemServico.CodDesenhoProduto, "O código de desenho do produto é obrigatório. Por favor, preencha este campo.") Then Exit Sub

        ' Verifica se o código Omie foi informado
        If Not ValidarCampoObrigatorio(OrdemServico.CodOmie, "O código RM é obrigatório. Por favor, preencha este campo.") Then Exit Sub

        ' Verifica se a descrição do produto foi informada
        If Not ValidarCampoObrigatorio(OrdemServico.DescricaoProduto, "A descrição do produto é obrigatória. Por favor, preencha este campo.") Then Exit Sub

        ' Verifica se o endereço da ficha técnica foi informado e se é um arquivo PDF válido
        ' If Not ValidarArquivoPDF(OrdemServico.EnderecoFichaTecnica, "O endereço da ficha técnica é obrigatório. Por favor, preencha este campo.", "O arquivo da ficha técnica não foi encontrado. Verifique o caminho informado.", "O arquivo da ficha técnica não é um PDF válido. Por favor, forneça um arquivo PDF.") Then Exit Sub

        ' Verifica se o endereço do isométrico foi informado e se é um arquivo PDF válido
        ' If Not ValidarArquivoPDF(OrdemServico.EnderecoIsometrico, "O endereço do isométrico é obrigatório. Por favor, preencha este campo.", "O arquivo do isométrico não foi encontrado. Verifique o caminho informado.", "O arquivo do isométrico não é um PDF válido. Por favor, forneça um arquivo PDF.") Then Exit Sub

        ' Define os campos automáticos relacionados ao usuário e à data
        OrdemServico.ProdutoCriadoPor = Usuario.NomeCompleto
        OrdemServico.DataCriacaoProduto = Date.Now.Date.ToShortDateString

        cl_BancoDados.AlteracaoEspecificaDadosOSProduto(OrdemServico.IdOrdemServico)
        MessageBox.Show("Dados validados e salvos com sucesso!", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Timer1dgvProdutos.Enabled = True

    End Sub

    Private Function ValidarIDOrdemServico() As Boolean
        If String.IsNullOrWhiteSpace(OrdemServico.IdOrdemServico.ToString()) OrElse OrdemServico.IdOrdemServico = 0 Then
            MessageBox.Show("O ID do item da ordem de serviço é inválido ou não foi informado.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function

    Private Function ValidarCampoObrigatorio(campo As String, mensagemErro As String) As Boolean
        If String.IsNullOrWhiteSpace(campo) Then
            MessageBox.Show(mensagemErro, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function

    Private Function ValidarArquivoPDF(caminhoArquivo As String, mensagemErroCampo As String, mensagemErroNaoEncontrado As String, mensagemErroInvalido As String) As Boolean
        If String.IsNullOrWhiteSpace(caminhoArquivo) Then
            MessageBox.Show(mensagemErroCampo, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf Not File.Exists(caminhoArquivo) Then
            MessageBox.Show(mensagemErroNaoEncontrado, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf Path.GetExtension(caminhoArquivo).ToLower() <> ".pdf" Then
            MessageBox.Show(mensagemErroInvalido, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function

    Private Sub btnBuscarFichaTecnica_Click(sender As Object, e As EventArgs) Handles btnBuscarFichaTecnica.Click
        Dim caminhoArquivo As String = cl_BancoDados.SelecionarArquivoPDF()

        If Not String.IsNullOrEmpty(caminhoArquivo) Then
            OrdemServico.EnderecoFichaTecnica = caminhoArquivo
            lblEnderecoFichaTecnica.Text = caminhoArquivo
            MessageBox.Show("Arquivo PDF selecionado: " & caminhoArquivo, "Seleção de Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Nenhum arquivo foi selecionado.", "Seleção de Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnBuscarIsometrico_Click(sender As Object, e As EventArgs) Handles btnBuscarIsometrico.Click
        Dim caminhoArquivo As String = cl_BancoDados.SelecionarArquivoPDF()

        If Not String.IsNullOrEmpty(caminhoArquivo) Then
            OrdemServico.EnderecoIsometrico = caminhoArquivo
            lblEnderecoIsometrico.Text = caminhoArquivo
            MessageBox.Show("Arquivo PDF selecionado: " & caminhoArquivo, "Seleção de Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Nenhum arquivo foi selecionado.", "Seleção de Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub lblEnderecoFichaTecnica_DoubleClick(sender As Object, e As EventArgs) Handles lblEnderecoFichaTecnica.DoubleClick
        AbrirArquivoPDF(lblEnderecoFichaTecnica.Text)
    End Sub

    Private Sub lblEnderecoIsometrico_DoubleClick(sender As Object, e As EventArgs) Handles lblEnderecoIsometrico.DoubleClick
        AbrirArquivoPDF(lblEnderecoIsometrico.Text)
    End Sub

    Private Sub AbrirArquivoPDF(caminhoArquivo As String)
        Try
            Dim arquivoPdf As String = Path.ChangeExtension(caminhoArquivo, ".PDF")
            arquivoPdf = Path.GetFullPath(arquivoPdf)

            If File.Exists(arquivoPdf) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(arquivoPdf)
                    p.Start()
                    ' p.WaitForExit()
                End Using
            Else
                MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
            End If
        Catch ex As Exception
            MsgBox("Erro ao abrir o arquivo: " & ex.Message, vbCritical, "Atenção")
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

        Me.Hide()

    End Sub

    Private Sub frmCriaProdutos_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        OrdemServico.IdOrdemServico = 0
    End Sub

    Private Sub frmCriaProdutos_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        btnSalvar.Enabled = True
        btnExcluir.Enabled = False

        Dim nomeImagemReferencia As String

        cl_BancoDados.RetornaCampoDaPesquisa("Select EnderecoImagem from material where codMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'", "EnderecoImagem")

        Try

            If DadosArquivoCorrente.NomeArquivoSemExtensao <> "" Then

                Try
                    PictureBox1.Image = Image.FromFile(VCampo0)
                Catch ex As Exception
                    ' Imagem não encontrada ou inválida — limpa sem exibir erro
                    PictureBox1.Image = Nothing
                    PictureBox1.Refresh()
                End Try

            End If

            cl_BancoDados.RetornaCampoDaPesquisa("Select ProdutoPadrao, CodDesenhoProduto, CodOmie, DescricaoProduto, EnderecoFichaTecnica, EnderecoIsometrico" &
                                                 " FROM ordemservico WHERE IdOrdemServico = '" & OrdemServico.IdOrdemServico & "'",
                                                 "CodDesenhoProduto",
                                                 "CodOmie",
                                                 "DescricaoProduto",
                                                 "EnderecoFichaTecnica",
                                                 "EnderecoIsometrico")

            txCodDesenhoProduto.Text = DadosArquivoCorrente.NomeArquivoSemExtensao
            txtCodOmie.Text = VCampo1
            lblEnderecoFichaTecnica.Text = VCampo2
            lblEnderecoIsometrico.Text = VCampo3
            txtDescricaoProduto.Text = VCampo4
        Catch ex As Exception
            PictureBox1.Image = Nothing
            PictureBox1.Refresh()

        End Try

        Timer1dgvProdutos.Enabled = True

    End Sub

    'Private Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click

    '    If OrdemServico.IdOrdemServico <> "" And
    '  '  DadosArquivoCorrente.NomeArquivoSemExtensao = dgvProdutos.CurrentRow.Cells("CodDesenhoProduto").Value.ToString Then

    '        cl_BancoDados.AlteracaoEspecifica("ordemservico", "ProdutoPadrao", Nothing, "idordemservico", OrdemServico.IdOrdemServico)

    '        MessageBox.Show("A OS Não deixara de existir porem o produto não e mais considerado produto padrão.",
    '                        "Exclusão do Produto Padrão da Lista", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '        OrdemServico.IdOrdemServico = Nothing

    '    End If

    'End Sub

    Private Sub dgvProdutos_DataError(sender As Object, e As DataGridViewDataErrorEventArgs)
        Try
        Catch ex As Exception
        Finally
        End Try
    End Sub

    'Private Sub dgvProdutos_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs)

    '    cl_BancoDados.FormatarDataGridView(dgvProdutos, "SIM")

    'End Sub

    'Private Sub AbrirPDFFichaTecnicaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirPDFFichaTecnicaToolStripMenuItem.Click

    '    Try

    '        Dim ArquivoPdf As String = dgvProdutos.CurrentRow.Cells("EnderecoFichaTecnica").Value.ToString()

    '        ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
    '        ArquivoPdf = Path.ChangeExtension(ArquivoPdf, ".PDF")

    '        ' Obtém o caminho completo
    '        ArquivoPdf = Path.GetFullPath(ArquivoPdf)

    '        ' Verifica se o arquivo existe e o abre
    '        If File.Exists(ArquivoPdf) Then
    '            Using p As New Diagnostics.Process
    '                p.StartInfo = New ProcessStartInfo(ArquivoPdf)

    '                p.Start()
    '                p.WaitForExit()

    '                dgvProdutos.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
    '            End Using
    '        End If
    '    Catch ex As Exception
    '        MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
    '    Finally

    '    End Try

    'End Sub

    'Private Sub AbrirPDFIsometricoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirPDFIsometricoToolStripMenuItem.Click

    '    Try

    '        Dim ArquivoPdf As String = dgvProdutos.CurrentRow.Cells("EnderecoIsometrico").Value.ToString()

    '        ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
    '        ArquivoPdf = Path.ChangeExtension(ArquivoPdf, ".PDF")

    '        ' Obtém o caminho completo
    '        ArquivoPdf = Path.GetFullPath(ArquivoPdf)

    '        ' Verifica se o arquivo existe e o abre
    '        If File.Exists(ArquivoPdf) Then
    '            Using p As New Diagnostics.Process
    '                p.StartInfo = New ProcessStartInfo(ArquivoPdf)

    '                p.Start()
    '                p.WaitForExit()

    '                dgvProdutos.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
    '            End Using
    '        End If
    '    Catch ex As Exception
    '        MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
    '    Finally

    '    End Try

    'End Sub
End Class