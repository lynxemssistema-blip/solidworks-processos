Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports iText.Commons.Bouncycastle
Imports iText.Kernel.Pdf
Imports iText.Kernel.Pdf.Canvas
Imports Microsoft.Office.Interop.Excel
Imports MySql.Data.MySqlClient
Imports netDxf
Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst
Imports Environment = System.Environment
Imports Font = System.Drawing.Font

Imports System.Text.RegularExpressions


Imports IO = System.IO

Imports View = SolidWorks.Interop.sldworks.View

' <-- Alias para System.IO

' =========================================

<ComVisible(True)>
<ProgId("SwLynx_4._1")>
Public Class Painel_Leitura_Dados

    ' ====== DECLARAÇÕES NO TOPO DO FORM ======
    'E'strutura para evitar tuple (compatível com versões antigas do VB/.NET)
    Private Structure DxfPdfState
        Public HasDxf As Boolean
        Public HasPdf As Boolean
    End Structure

    ' Cache thread-safe: EnderecoArquivo -> (HasDxf, HasPdf)
    ' Private ReadOnly _existCacheDxfPdf As New ConcurrentDictionary(Of String, DxfPdfState)(StringComparer.OrdinalIgnoreCase)

    ' Versão do “pré-aquecimento” (para invalidar tasks antigas quando a fonte de dados muda)
    Private _prewarmVersion As Integer = 0

    Private WithEvents TimerResetBotao As New System.Windows.Forms.Timer()

    Friend Sub getSwApp(ByRef swAddin As SolidWorks.Interop.sldworks.SldWorks)

        swapp = swAddin

    End Sub

    Dim dt As New System.Data.DataTable
    Dim dtConsolidado As New System.Data.DataTable()

    Public iconeTipoArquivo As System.Drawing.Image
    Public iconeAtencao As System.Drawing.Image
    Public iconePDF As System.Drawing.Image
    Public iconeDXF As System.Drawing.Image
    Public iconeLXDS As System.Drawing.Image

    Dim SALVAR As Boolean = False
    Dim Vem_de_Onde As String = ""

    Public Function AtualizaTela(ByVal swModel As ModelDoc2, chkBoxProcesso As CheckedListBox) As Boolean

        ' Conectar ao SolidWorks
        IntanciaSolidWorks.ConectarSolidWorks()

        ' Obter o documento ativo
        ' swModel = swapp.ActiveDoc
        DadosArquivoCorrente.EnderecoArquivo = swModel.GetPathName().ToUpper()
        DadosArquivoCorrente.NomeArquivoComExtensao = Path.GetFileName(DadosArquivoCorrente.EnderecoArquivo).ToUpper()
        DadosArquivoCorrente.NomeArquivoSemExtensao = Path.GetFileNameWithoutExtension(DadosArquivoCorrente.EnderecoArquivo)

        If vemdalista = False Then

            ' Verifique se o modelo foi aberto com sucesso
            If Not swModel Is Nothing Then

                ' Usa Select Case para diferenciar o tipo do documento
                If swModel.GetType() = swDocumentTypes_e.swDocPART Or swModel.GetType() = swDocumentTypes_e.swDocASSEMBLY Then

                    '****************************************************************
                    'BUSCA DADOS DA LEITURA DO DESENHO

                    'Me.cboTitulo.Text = swModel.SummaryInfo(swSummInfoField_e.swSumInfoTitle).ToUpper
                    Me.txtTitulo.Text = swModel.SummaryInfo(swSummInfoField_e.swSumInfoTitle).ToUpper()
                    Me.txtAssuntoSubiTitulo.Text = swModel.SummaryInfo(swSummInfoField_e.swSumInfoSubject).ToUpper()
                    Me.txtComentarios.Text = swModel.SummaryInfo(swSummInfoField_e.swSumInfoComment).ToUpper()
                    Me.txtAuthor.Text = swModel.SummaryInfo(swSummInfoField_e.swSumInfoAuthor).ToUpper()
                    Me.txtPalavraChave.Text = swModel.SummaryInfo(swSummInfoField_e.swSumInfoKeywords).ToUpper()
                    txtNomeArquivo.Text = DadosArquivoCorrente.NomeArquivoSemExtensao

                    txtAprovado.Text = DadosArquivoCorrente.NomeArquivoSemExtensao

                    'Lista de Corte
                    Me.lblEspessura.Text = DadosArquivoCorrente.Espessura
                    Me.lblLargura.Text = DadosArquivoCorrente.LarguraBlank
                    Me.lblComprimento.Text = DadosArquivoCorrente.ComprimentoBlank

                    Me.lblNumeroDobra.Text = DadosArquivoCorrente.NumeroDobras
                    Me.lblPeso.Text = DadosArquivoCorrente.Massa.ToString
                    Me.lblMaterial.Text = DadosArquivoCorrente.material.ToString
                    Me.lblAreaPintura.Text = DadosArquivoCorrente.AreaPintura
                    Me.txtAprovado.Text = DadosArquivoCorrente.Aprovado
                    Me.txtVerificado.Text = DadosArquivoCorrente.Verificado

                    Me.lblAlturaTotalCaixaDelimitadora.Text = DadosArquivoCorrente.Alturacaixadelimitadora
                    Me.lblLarguraTotalCaixaDelimitadora.Text = DadosArquivoCorrente.Larguracaixadelimitadora
                    Me.lblProfundidadeTotalCaixaDelimitadora.Text = DadosArquivoCorrente.Profundidadeaixadelimitadora

                    optProcessoSoldagemSim.Checked = (DadosArquivoCorrente.soldagem = "SIM")
                    If optProcessoSoldagemSim.Checked = False Then
                        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtSoldagem", "NÃO", "NÃO")
                    End If

                    OPTEstoqueSim.Checked = (DadosArquivoCorrente.ItemEstoque = "SIM")
                    If OPTEstoqueSim.Checked = False Then
                        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtItemEstoque", "NÃO", "NÃO")
                    End If

                    Me.lblPeso.Text = DadosArquivoCorrente.Massa

                    Me.lblMaterial.Text = DadosArquivoCorrente.material.ToString

                    Dim extensoes As String() = {".pdf", ".dxf", ".dft", ".lxds"}
                    Dim checkboxes As System.Windows.Forms.CheckBox() = {chkVerificarPDF, chkVerificarDXF, chkVerificarDFT, chkVerificarLXDS}

                    For i As Integer = 0 To extensoes.Length - 1
                        Dim caminhoAlterado As String = System.IO.Path.ChangeExtension(DadosArquivoCorrente.EnderecoArquivo, extensoes(i))
                        Select Case extensoes(i)
                            Case ".pdf"
                                DadosArquivoCorrente.ArquivoPdf = caminhoAlterado
                            Case ".dxf"
                                DadosArquivoCorrente.ArquivoDxf = caminhoAlterado
                            Case ".dft"
                                DadosArquivoCorrente.ArquivoDft = caminhoAlterado
                            Case ".lxds"
                                DadosArquivoCorrente.ArquivoLXDS = caminhoAlterado
                        End Select
                        checkboxes(i).Checked = File.Exists(caminhoAlterado)
                    Next

                    For i As Integer = 0 To chkBoxAcabamento.Items.Count - 1
                        Try

                            If DadosArquivoCorrente.Acabamento.ToString = chkBoxAcabamento.Items(i).ToString Then
                                chkBoxAcabamento.Text = DadosArquivoCorrente.Acabamento
                                chkBoxAcabamento.SetItemChecked(i, True) ' Marca o item correspondente
                            Else
                                chkBoxAcabamento.SetItemChecked(i, False) ' Desmarca outros itens
                            End If
                        Catch ex As Exception
                            Continue For
                        End Try
                    Next

                    For i As Integer = 0 To chkBoxTipoDesenho.Items.Count - 1
                        Try

                            If DadosArquivoCorrente.TipoDesenho.ToString = chkBoxTipoDesenho.Items(i).ToString Then
                                chkBoxTipoDesenho.Text = DadosArquivoCorrente.TipoDesenho
                                chkBoxTipoDesenho.SetItemChecked(i, True) ' Marca o item correspondente
                            Else
                                chkBoxTipoDesenho.SetItemChecked(i, False) ' Desmarca outros itens
                            End If
                        Catch ex As Exception
                            Continue For
                        End Try
                    Next

                    Me.lblEspessura.Text = DadosArquivoCorrente.Espessura

                    DadosArquivoCorrente.LerPropriedadesPersonalizadas(swModel, chkBoxProcesso)

                    If DadosArquivoCorrente.Bloqueado = "S" Then

                        chkAtualizacao.Checked = True
                    Else
                        chkAtualizacao.Checked = False

                    End If

                    'DadosArquivoCorrente.Acabamento = Me.chkBoxAcabamento.Text
                    If DadosArquivoCorrente.Bloqueado = "S" Then
                        chkAtualizacao.Image = My.Resources.bloqueado
                        chkAtualizacao.Text = "Bloqueado"
                        chkAtualizacao.Checked = True
                    Else
                        chkAtualizacao.Checked = False
                        DadosArquivoCorrente.Bloqueado = "N"
                        chkAtualizacao.Image = My.Resources.desbloqueado
                        chkAtualizacao.Text = "Desbloqueado"
                    End If

                    'btnPendencias.Enabled = False

                    btnPendencias.Enabled = DadosArquivoCorrente.VerificarProcessodaPecaCorrente(swModel, True)

                    '22-09-2025
                    ''''''''  DadosArquivoCorrente.LerDadosCaixaDelimitadora(swModel)

                End If

            End If

        End If

    End Function

    ' Método auxiliar para limpar pendências
    Private Sub LimparPendencias()
        DadosArquivoCorrente.DescricaoPendencia = ""
        DadosArquivoCorrente.rnc = ""
        'txtDescricaoPendencia.Clear()
        btnPendencias.Image = My.Resources.verificado
        btnPendencias.Enabled = False
    End Sub

    ' Função para verificar se um valor é Nothing, vazio ou igual a "0"
    Private Function IsNullOrZero(ByVal valor As String) As Boolean
        Return valor Is Nothing OrElse String.IsNullOrEmpty(valor.Trim()) OrElse valor.Trim() = "0"
    End Function

    Public Sub GetSheetMetalProperties(ByVal SwModel As ModelDoc2)

        IntanciaSolidWorks.ConectarSolidWorks()
        ' swApparq = CreateObject("SldWorks.Application")

        SwModel = swapp.ActiveDoc

        Dim sheetThickness As Double = 0.0
        Dim blankLength As Double = 0.0
        Dim blankWidth As Double = 0.0
        Dim swFlatPattern As Feature = Nothing

        If SwModel IsNot Nothing Then
            ' Verifica se o documento ativo é uma peça
            If SwModel.GetType = swDocumentTypes_e.swDocPART Then
                Dim swPart As PartDoc = CType(SwModel, PartDoc)

                ' Itera através das features para obter a espessura da chapa
                Dim swFeature As Feature = swPart.FirstFeature()
                While swFeature IsNot Nothing

                    If swFeature.GetTypeName2() = "SheetMetal" Then
                        ' Obtém os dados da feature Sheet Metal
                        Dim swSheetMetalData As ISheetMetalFeatureData = CType(swFeature.GetDefinition(), ISheetMetalFeatureData)
                        sheetThickness = (swSheetMetalData.Thickness * 1000)
                        Exit While ' Encontrou a feature SheetMetal, não precisa continuar
                    End If

                    swFeature = swFeature.GetNextFeature() ' Avança para a próxima feature

                End While

                If DadosArquivoCorrente.Espessura = "" Then

                    DadosArquivoCorrente.Espessura = sheetThickness.ToString("F2")
                    Me.lblEspessura.Text = DadosArquivoCorrente.Espessura

                End If

            End If

        End If

    End Sub

    Private Sub btnLerDados_Click(sender As Object, e As EventArgs)

        Try

            DadosArquivoCorrente.ArquivoCorrente(swModel, chkBoxProcessos)

            DadosArquivoCorrente.PercorrerPropriedadesDaListaDeCorte(swModel)

            'dados da caixa delimitadora
            DadosArquivoCorrente.LerDadosCaixaDelimitadora(swModel)

            AtualizaTela(swModel, chkBoxProcessos)
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub btnSalvar_Click_1(sender As Object, e As EventArgs)

        Try

            DadosArquivoCorrente.AtualizaDesenho(swModel)

            'If swModel Is Nothing Then

            '    Exit Sub

            'End If

            swModel.GraphicsRedraw2()

            ' Salva o arquivo com as opções de salvamento padrão e com a miniatura
            swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)

            'Verifica se o desenhop atualizado esta carregado na BOM se sim, atualiza os dados do desenho'
            If dgvDataGridBOM.Rows.Count > 0 Then

                For i As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

                    If DadosArquivoCorrente.NomeArquivoSemExtensao.ToString.Trim = dgvDataGridBOM.Rows(i).Cells("CodMatFabricante").Value.ToString.Trim Then

                        ' Adicione os parâmetros ao comando
                        'dgvDataGridBOM.Rows(i).Cells("DescResumo").Value = Me.cboTitulo.Text.ToUpper  ' UCase(DadosArquivoCorrente.Titulo)
                        dgvDataGridBOM.Rows(i).Cells("DescResumo").Value = Me.txtTitulo.Text.ToUpper  ' UCase(DadosArquivoCorrente.Titulo)
                        dgvDataGridBOM.Rows(i).Cells("DescDetal").Value = Me.txtAssuntoSubiTitulo.Text ' UCase(DadosArquivoCorrente.AssuntoSubiTitulo)
                        dgvDataGridBOM.Rows(i).Cells("Autor").Value = Me.txtAuthor.Text ' UCase(DadosArquivoCorrente.Author)
                        dgvDataGridBOM.Rows(i).Cells("Palavrachave").Value = Me.txtPalavraChave.Text ' UCase(DadosArquivoCorrente.PalavraChave)
                        dgvDataGridBOM.Rows(i).Cells("Notas").Value = Me.txtComentarios.Text ' UCase(DadosArquivoCorrente.Comentarios)
                        dgvDataGridBOM.Rows(i).Cells("Espessura").Value = Me.lblEspessura.Text '  UCase(DadosArquivoCorrente.Espessura)
                        dgvDataGridBOM.Rows(i).Cells("AreaPintura").Value = Me.lblAreaPintura.Text ' UCase(DadosArquivoCorrente.AreaPintura)
                        dgvDataGridBOM.Rows(i).Cells("NumeroDobras").Value = Me.lblNumeroDobra.Text ' UCase(DadosArquivoCorrente.NumeroDobras)
                        dgvDataGridBOM.Rows(i).Cells("Peso").Value = Me.lblPeso.Text  ' UCase(DadosArquivoCorrente.Massa)
                        'dgvDataGridBOM.Rows(i).Cells("Unidade").Value = "PC"
                        dgvDataGridBOM.Rows(i).Cells("Altura").Value = Me.lblComprimento.Text ' UCase(DadosArquivoCorrente.ComprimentoBlank)
                        dgvDataGridBOM.Rows(i).Cells("Largura").Value = Me.lblLargura.Text ' UCase(DadosArquivoCorrente.LarguraBlank)
                        'dgvDataGridBOM.Rows(i).Cells("Profundidade").Value = ""
                        dgvDataGridBOM.Rows(i).Cells("material").Value = Me.lblMaterial.Text ' UCase(DadosArquivoCorrente.material)
                        dgvDataGridBOM.Rows(i).Cells("Acabamento").Value = UCase(DadosArquivoCorrente.Acabamento)
                        dgvDataGridBOM.Rows(i).Cells("txtSoldagem").Value = UCase(DadosArquivoCorrente.soldagem)
                        dgvDataGridBOM.Rows(i).Cells("txtTipoDesenho").Value = UCase(DadosArquivoCorrente.TipoDesenho)
                        dgvDataGridBOM.Rows(i).Cells("txtCorte").Value = UCase(DadosArquivoCorrente.Corte)
                        dgvDataGridBOM.Rows(i).Cells("txtDobra").Value = UCase(DadosArquivoCorrente.Dobra)
                        dgvDataGridBOM.Rows(i).Cells("txtSolda").Value = UCase(DadosArquivoCorrente.Solda)
                        dgvDataGridBOM.Rows(i).Cells("txtPintura").Value = UCase(DadosArquivoCorrente.Pintura)
                        dgvDataGridBOM.Rows(i).Cells("txtMontagem").Value = UCase(DadosArquivoCorrente.Montagem)
                        dgvDataGridBOM.Rows(i).Cells("Comprimentocaixadelimitadora").Value = Me.lblAlturaTotalCaixaDelimitadora.Text '  DadosArquivoCorrente.Alturacaixadelimitadora
                        dgvDataGridBOM.Rows(i).Cells("Larguracaixadelimitadora").Value = Me.lblProfundidadeTotalCaixaDelimitadora.Text ' DadosArquivoCorrente.Larguracaixadelimitadora
                        dgvDataGridBOM.Rows(i).Cells("Espessuracaixadelimitadora").Value = Me.lblProfundidadeTotalCaixaDelimitadora.Text ' DadosArquivoCorrente.Profundidadeaixadelimitadora
                        dgvDataGridBOM.Rows(i).Cells("txtItemEstoque").Value = DadosArquivoCorrente.ItemEstoque

                        dgvDataGridBOM.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen

                        Exit Sub

                    End If

                Next

            End If
        Catch ex As Exception
        Finally
        End Try

    End Sub

    'Public Function AtualisarItensOrdemServico()

    '    Dim ordemservicoitem As String

    '    ordemservicoitem = "UPDATE " & ComplementoTipoBanco & " ordemservicoitem Set DescResumo = @DescResumo, DescDetal = @DescDetal, " &
    '            "Autor = @Autor, Palavrachave = @Palavrachave, Notas = @Notas, Espessura = @Espessura, AreaPintura = @AreaPintura, " &
    '            "NumeroDobras = @NumeroDobras, Peso = @Peso, Unidade = @Unidade, Altura = @Altura, Largura = @Largura, " &
    '            "MaterialSW = @MaterialSW, EnderecoArquivo = @EnderecoArquivo, Acabamento = @Acabamento, " &
    '            "txtSoldagem = @txtSoldagem, txtTipoDesenho = @txtTipoDesenho, txtCorte = @txtCorte, txtDobra = @txtDobra, " &
    '            "txtSolda = @txtSolda, txtPintura = @txtPintura, txtMontagem = @txtMontagem, Comprimentocaixadelimitadora = @Compcx, " &
    '            "Larguracaixadelimitadora = @Largcx, Espessuracaixadelimitadora = @Espcx, txtItemEstoque = @txtItemEstoque " &
    '            " WHERE IDOrdemServicoItem = @IDOrdemServicoItem"

    '    '  DGVTimerFiltroPecaAtivaOS.Refresh()

    '    DadosArquivoCorrente.ArquivoCorrente(swModel, chkBoxProcessos)

    '    DadosArquivoCorrente.PercorrerPropriedadesDaListaDeCorte(swModel)

    '    'dados da caixa delimitadora
    '    DadosArquivoCorrente.LerDadosCaixaDelimitadora(swModel)

    '    If My.Settings.TipoConexao = "MYSQL" Then

    '        For i As Integer = 0 To DGVTimerFiltroPecaAtivaOS.Rows.Count - 1

    '            Using cmdidordemservicoitem As New MySqlCommand(ordemservicoitem, myconect)

    '                If Convert.ToBoolean(DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("dgvSelecaoAtualizacaoItemOs").Value) = True Then

    '                    Dim IDOrdemServicoItem As String = DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("IDOrdemServicoItem").Value.ToString

    '                    ' Adicione os parâmetros ao comando
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@DescResumo", UCase(DadosArquivoCorrente.Titulo))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@DescDetal", UCase(DadosArquivoCorrente.AssuntoSubiTitulo))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Autor", UCase(DadosArquivoCorrente.Author))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Palavrachave", UCase(DadosArquivoCorrente.PalavraChave))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Notas", UCase(DadosArquivoCorrente.Comentarios))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Espessura", UCase(DadosArquivoCorrente.Espessura))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@AreaPintura", UCase(DadosArquivoCorrente.AreaPintura))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@NumeroDobras", UCase(DadosArquivoCorrente.NumeroDobras))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Peso", UCase(DadosArquivoCorrente.Massa))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Unidade", "PC")
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Altura", UCase(DadosArquivoCorrente.ComprimentoBlank))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Largura", UCase(DadosArquivoCorrente.LarguraBlank))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Profundidade", String.Empty)
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@MaterialSW", UCase(DadosArquivoCorrente.material))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@EnderecoArquivo", UCase(DadosArquivoCorrente.EnderecoArquivo))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Acabamento", UCase(DadosArquivoCorrente.Acabamento))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@txtSoldagem", UCase(DadosArquivoCorrente.soldagem))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@txtTipoDesenho", UCase(DadosArquivoCorrente.TipoDesenho))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@txtCorte", UCase(DadosArquivoCorrente.Corte))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@txtDobra", UCase(DadosArquivoCorrente.Dobra))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@txtSolda", UCase(DadosArquivoCorrente.Solda))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@txtPintura", UCase(DadosArquivoCorrente.Pintura))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@txtMontagem", UCase(DadosArquivoCorrente.Montagem))
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Compcx", DadosArquivoCorrente.Alturacaixadelimitadora)
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Largcx", DadosArquivoCorrente.Larguracaixadelimitadora)
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@Espcx", DadosArquivoCorrente.Profundidadeaixadelimitadora)
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@txtItemEstoque", DadosArquivoCorrente.ItemEstoque)
    '                    DadosArquivoCorrente.AddTextParameterMysql(cmdidordemservicoitem, "@IDOrdemServicoItem", IDOrdemServicoItem)

    '                    Try

    '                        ' Antes de executar o comando, exibir a SQL para verificação
    '                        Dim sqlComando As String = cmdidordemservicoitem.CommandText

    '                        ' Loop pelos parâmetros para exibir seus valores
    '                        For Each param As MySqlParameter In cmdidordemservicoitem.Parameters
    '                            sqlComando = sqlComando.Replace(param.ParameterName, param.Value.ToString())
    '                        Next

    '                        ' Exibir o comando SQL montado (usando Console ou MessageBox)
    '                        ' Console.WriteLine(sqlComando) ' Se for aplicação console
    '                        ' InputBox("", "", sqlComando) ' Se for aplicação WinForms

    '                        ' Executar o comando após depurar/verificar a SQL
    '                        Try
    '                            cmdidordemservicoitem.ExecuteNonQuery()
    '                        Catch ex As MySqlException
    '                            '     InputBox("", "", "Erro ao executar SQL: " & ex.Message)
    '                        End Try

    '                        cmdidordemservicoitem.ExecuteNonQuery()
    '                    Catch ex As MySqlException

    '                        '    InputBox("", "", ex.Message)
    '                        ' Exibe a mensagem de erro
    '                        MessageBox.Show("Erro ao executar o comando SQL: " & ex.Message, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    Catch ex As Exception
    '                        ' Exibe outros erros gerais
    '                        MessageBox.Show("Erro inesperado: " & ex.Message, "Erro Geral", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    End Try

    '                    DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("dgvSelecaoAtualizacaoItemOs").Value = False

    '                End If

    '            End Using
    '        Next

    '    ElseIf My.Settings.TipoConexao = "SQL" Then

    '        For i As Integer = 0 To DGVTimerFiltroPecaAtivaOS.Rows.Count - 1

    '            Using cmdidordemservicoitem As New SqlCommand(ordemservicoitem, myconectSQL)

    '                If Convert.ToBoolean(DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("dgvSelecaoAtualizacaoItemOs").Value) = True Then

    '                    Dim IDOrdemServicoItem As String = DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("IDOrdemServicoItem").Value.ToString

    '                    ' Adicione os parâmetros ao comando
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@DescResumo", UCase(DadosArquivoCorrente.Titulo))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@DescDetal", UCase(DadosArquivoCorrente.AssuntoSubiTitulo))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Autor", UCase(DadosArquivoCorrente.Author))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Palavrachave", UCase(DadosArquivoCorrente.PalavraChave))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Notas", UCase(DadosArquivoCorrente.Comentarios))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Espessura", UCase(DadosArquivoCorrente.Espessura))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@AreaPintura", UCase(DadosArquivoCorrente.AreaPintura))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@NumeroDobras", UCase(DadosArquivoCorrente.NumeroDobras))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Peso", UCase(DadosArquivoCorrente.Massa))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Unidade", "PC")
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Altura", UCase(DadosArquivoCorrente.ComprimentoBlank))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Largura", UCase(DadosArquivoCorrente.LarguraBlank))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Profundidade", String.Empty)
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@MaterialSW", UCase(DadosArquivoCorrente.material))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@EnderecoArquivo", UCase(DadosArquivoCorrente.EnderecoArquivo))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Acabamento", UCase(DadosArquivoCorrente.Acabamento))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@txtSoldagem", UCase(DadosArquivoCorrente.soldagem))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@txtTipoDesenho", UCase(DadosArquivoCorrente.TipoDesenho))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@txtCorte", UCase(DadosArquivoCorrente.Corte))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@txtDobra", UCase(DadosArquivoCorrente.Dobra))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@txtSolda", UCase(DadosArquivoCorrente.Solda))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@txtPintura", UCase(DadosArquivoCorrente.Pintura))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@txtMontagem", UCase(DadosArquivoCorrente.Montagem))
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Compcx", DadosArquivoCorrente.Alturacaixadelimitadora)
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Largcx", DadosArquivoCorrente.Larguracaixadelimitadora)
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@Espcx", DadosArquivoCorrente.Profundidadeaixadelimitadora)
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@txtItemEstoque", DadosArquivoCorrente.ItemEstoque)
    '                    DadosArquivoCorrente.AddTextParameterSql(cmdidordemservicoitem, "@IDOrdemServicoItem", IDOrdemServicoItem)

    '                    Try

    '                        ' Antes de executar o comando, exibir a SQL para verificação
    '                        Dim sqlComando As String = cmdidordemservicoitem.CommandText

    '                        ' Loop pelos parâmetros para exibir seus valores
    '                        For Each param As SqlParameter In cmdidordemservicoitem.Parameters
    '                            sqlComando = sqlComando.Replace(param.ParameterName, param.Value.ToString())
    '                        Next

    '                        ' Exibir o comando SQL montado (usando Console ou MessageBox)
    '                        ' Console.WriteLine(sqlComando) ' Se for aplicação console
    '                        ' InputBox("", "", sqlComando) ' Se for aplicação WinForms

    '                        ' Executar o comando após depurar/verificar a SQL
    '                        Try
    '                            cmdidordemservicoitem.ExecuteNonQuery()
    '                        Catch ex As MySqlException
    '                            '     InputBox("", "", "Erro ao executar SQL: " & ex.Message)
    '                        End Try

    '                        cmdidordemservicoitem.ExecuteNonQuery()
    '                    Catch ex As SqlException

    '                        '    InputBox("", "", ex.Message)
    '                        ' Exibe a mensagem de erro
    '                        MessageBox.Show("Erro ao executar o comando SQL: " & ex.Message, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    Catch ex As Exception
    '                        ' Exibe outros erros gerais
    '                        MessageBox.Show("Erro inesperado: " & ex.Message, "Erro Geral", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    End Try

    '                    DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("dgvSelecaoAtualizacaoItemOs").Value = False

    '                End If

    '            End Using
    '        Next

    '    End If

    '    DGVTimerFiltroPecaAtivaOS.Enabled = True

    'End Function

    Public Function SalvarArquivoCorrente(ByVal swModel As ModelDoc2)

        Try
            DadosArquivoCorrente.ArquivoCorrente(swModel, chkBoxProcessos)
        Catch ex As Exception
            Debug.WriteLine("[SalvarArquivoCorrente] ArquivoCorrente: " & ex.Message)
        End Try

        Try
            DadosArquivoCorrente.PercorrerPropriedadesDaListaDeCorte(swModel)
        Catch ex As Exception
            Debug.WriteLine("[SalvarArquivoCorrente] PercorrerPropriedades: " & ex.Message)
        End Try

        Try
            DadosArquivoCorrente.LerDadosCaixaDelimitadora(swModel)
        Catch ex As Exception
            Debug.WriteLine("[SalvarArquivoCorrente] LerCaixaDelimitadora: " & ex.Message)
        End Try

        Try
            Me.txtPalavraChave.Text = DadosArquivoCorrente.EnderecoArquivo
            AtualizaTela(swModel, chkBoxProcessos)
        Catch ex As Exception
            Debug.WriteLine("[SalvarArquivoCorrente] AtualizaTela: " & ex.Message)
        End Try

        Try
            DadosArquivoCorrente.AtualizaDesenho(swModel)
        Catch ex As Exception
            Debug.WriteLine("[SalvarArquivoCorrente] AtualizaDesenho: " & ex.Message)
        End Try

    End Function

    Private Sub btndxf_Click(sender As Object, e As EventArgs)

        Try

            'Verifique se o modelo foi aberto com sucesso
            If Not swModel Is Nothing Then

                'Usa Select Case para diferenciar o tipo do documento
                If swModel.GetType() = swDocumentTypes_e.swDocPART Then

                    IntanciaSolidWorks.ConectarSolidWorks()

                    swModel = swapp.ActiveDoc
                    'swModel = swApparq.ActiveDoc

                    swModel.Visible = True

                    swModelDocExt = swModel.Extension

                    Try

                        DadosArquivoCorrente.ExportDXF(swModel, True, True)

                        ' DadosArquivoCorrente.ExportSheetMetalBlankToDXF(swModel.GetPathName, Path.ChangeExtension(swModel.GetPathName, ".dxf"))

                        chkVerificarDXF.Checked = True
                    Catch ex As Exception

                        MsgBox(ex.Message & " o arquivo não e valido para conversão em dxf")
                    Finally

                    End Try
                End If
            End If
        Catch ex As Exception
        Finally
        End Try

        ''''''''''''''''' InserirTextoDXF(swModel, "", "")
        '''
    End Sub

    Sub InserirTextoDXF(swModel As Object, ByVal EnderecoIncial As String, ByVal EnderecoSaida As String)

        Try
            ' Caminho do arquivo DXF existente
            Dim inputFilePath As String = swModel.GetPathName
            If String.IsNullOrEmpty(inputFilePath) OrElse Not File.Exists(inputFilePath) Then
                Console.WriteLine("O caminho do arquivo DXF é inválido ou o arquivo não existe.")
                Exit Sub
            End If

            ' Caminho para salvar o arquivo atualizado
            Dim outputFilePath As String = Path.ChangeExtension(inputFilePath, ".dxf")

            ' Carregar o arquivo DXF
            Dim dxf As DxfDocument = DxfDocument.Load(outputFilePath)

            ' Criar ou buscar o layer "MeuTexto"
            Dim layerName As String = "MeuTexto"
            Dim meuLayer As netDxf.Tables.Layer
            If Not dxf.Layers.Contains(layerName) Then
                meuLayer = New netDxf.Tables.Layer(layerName)
                dxf.Layers.Add(meuLayer)
            Else
                meuLayer = dxf.Layers(layerName)
            End If

            ' Criar um novo texto
            Dim texto As New netDxf.Entities.Text(Path.GetFileNameWithoutExtension(inputFilePath),
                              New netDxf.Vector2(10, 1), ' Coordenadas X e Y
            5) ' Altura do texto
            texto.Layer = meuLayer ' Adicionar o texto ao layer específico
            texto.Color = AciColor.Blue  ' Opcional: Cor do texto

            ' Adicionar o texto ao desenho
            dxf.Entities.Add(texto)

            ' Salvar o arquivo modificado
            dxf.Save(outputFilePath)

            MsgBox("Texto inserido com sucesso no arquivo:  " & outputFilePath)
        Catch ex As Exception
            MsgBox("Erro: " & ex.Message)
        End Try
    End Sub

    Private Sub btnListaMaterial_Click(sender As Object, e As EventArgs)

        Try

            'If cl_BancoDados.AbrirBanco = False Then

            '    cl_BancoDados.AbrirBanco()

            'End If

            TabelaViewMontaPeca = cl_BancoDados.CarregarDados("SELECT * FROM  " & ComplementoTipoBanco & "viewmontapeca WHERE (D_E_L_E_T_E <> '*' AND D_E_L_E_T_E IS NULL) OR D_E_L_E_T_E = ''")

            ' Conectar ao SolidWorks
            IntanciaSolidWorks.ConectarSolidWorks()

            ' Obter o documento ativo
            swModel = swapp.ActiveDoc

            ' Verificar se o documento foi aberto
            If swModel Is Nothing Then
                MsgBox("Nenhum documento ativo no SolidWorks.", vbCritical, "Erro")
                Exit Sub
            End If

            ' Definir modelo como visível
            'swModel.Visible = True
            swModelDocExt = swModel.Extension

            ' Verifica o tipo de documento (Desenho)
            If swModel.GetType() = swDocumentTypes_e.swDocDRAWING Then

                ' Obter o caminho completo do arquivo
                DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(swModel.GetPathName().ToUpper())

                ' Se a opção de converter para PDF estiver marcada
                If chkConverterPDF.Checked Then
                    ' Verificar se o arquivo existe antes de tentar converter
                    If File.Exists(DadosArquivoCorrente.EnderecoArquivo) Then
                        DadosArquivoCorrente.ExportToPDF(swModel, DadosArquivoCorrente.EnderecoArquivo, True)
                        iconePDF = My.Resources.ficheiro_pdf
                    Else
                        MsgBox("Arquivo não encontrado: " & DadosArquivoCorrente.EnderecoArquivo, vbCritical, "Erro")
                        Exit Sub
                    End If
                End If

                ' Alterar a extensão para SLDASM
                DadosArquivoCorrente.EnderecoArquivo = Path.ChangeExtension(DadosArquivoCorrente.EnderecoArquivo, "SLDASM")

                ' Verificar se o arquivo SLDASM existe
                If File.Exists(DadosArquivoCorrente.EnderecoArquivo) Then
                    OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)
                Else
                    MsgBox("O desenho de detalhamento não pertence a um desenho de conjunto!", vbCritical, "Atenção")
                    Exit Sub
                End If

            End If

            ' Processar PART ou ASSEMBLY
            If swModel.GetType() = swDocumentTypes_e.swDocPART Or swModel.GetType() = swDocumentTypes_e.swDocASSEMBLY Then

                ''retirado 03/12/2024 edson  DadosArquivoCorrente.AtualizaDesenho(swModel)

                FormatarColunaIconeDGVListaBom(DadosArquivoCorrente.EnderecoArquivo)

                ' Preencher o DataGridView com os dados da peça
                dgvDataGridBOM.Rows.Add(iconeDXF,
                                        iconePDF,
                                        iconeTipoArquivo,
                                    iconeAtencao,
                                    DadosArquivoCorrente.IdMaterial,
                                    DadosArquivoCorrente.NomeArquivoSemExtensao,
                                    DadosArquivoCorrente.Titulo,
                                    DadosArquivoCorrente.AssuntoSubiTitulo,
                                    DadosArquivoCorrente.Author,
                                    DadosArquivoCorrente.PalavraChave,
                                    DadosArquivoCorrente.Comentarios,
                                    DadosArquivoCorrente.Espessura,
                                    DadosArquivoCorrente.ComprimentoBlank,
                                    DadosArquivoCorrente.LarguraBlank,
                                    DadosArquivoCorrente.material,
                                    DadosArquivoCorrente.AreaPintura,
                                    DadosArquivoCorrente.NumeroDobras,
                                    DadosArquivoCorrente.Massa,
                                    DadosArquivoCorrente.EnderecoArquivo,
                                    DadosArquivoCorrente.Acabamento,
                                    DadosArquivoCorrente.soldagem,
                                    DadosArquivoCorrente.TipoDesenho,
                                    DadosArquivoCorrente.Corte,
                                    DadosArquivoCorrente.Dobra,
                                    DadosArquivoCorrente.Solda,
                                    DadosArquivoCorrente.Pintura,
                                    DadosArquivoCorrente.Montagem,
                                    DadosArquivoCorrente.rnc,
                                    DadosArquivoCorrente.Alturacaixadelimitadora,
                                    DadosArquivoCorrente.Larguracaixadelimitadora,
                                    DadosArquivoCorrente.Profundidadeaixadelimitadora,
                                    DadosArquivoCorrente.ItemEstoque,
                                    1, DadosArquivoCorrente.Bloqueado)

                Try

                    If DadosArquivoCorrente.rnc.ToString <> "" Then

                        dgvDataGridBOM.Rows(dgvDataGridBOM.CurrentRow.Index).DefaultCellStyle.BackColor = Color.LightPink

                    End If
                Catch ex As Exception
                Finally
                End Try

                ' Ler dados da view de montagem
                LerDadosViewMontaPeca()

                Try

                    ' Fechar o documento
                    swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                    cl_BancoDados.FecharArquivoMemoria()
                    IntanciaSolidWorks.LiberarRecurso(swModel)
                Catch ex As Exception
                Finally

                End Try

                ' Processar a lista de material
                ProcessarListaMaterial(swModel)

            End If
        Catch ex As Exception
            '  MsgBox("Erro: " & ex.Message, vbCritical, "Erro")
        Finally
        End Try

    End Sub

    ' Sub para processar a lista de material
    Private Sub ProcessarListaMaterial(swModel As Object)
        Try
            Dim swFeat As Object = swModel.FirstFeature

            ' Iterar sobre as características do modelo
            Do While Not swFeat Is Nothing

                Try

                    If "BomFeat" = swFeat.GetTypeName Then
                        '''    Dim resultado As DialogResult = MessageBox.Show(
                        '''"Gostaria de Exportar os dados da lista de material? " & swFeat.Name, "Lista de peças",
                        '''MessageBoxButtons.YesNo)
                        '''

                        Using frmTopMost As New Form()
                            frmTopMost.TopMost = True
                            frmTopMost.StartPosition = FormStartPosition.CenterScreen ' FormStartPosition.Manual
                            frmTopMost.ShowInTaskbar = False
                            frmTopMost.FormBorderStyle = FormBorderStyle.None
                            frmTopMost.Size = New Size(1, 1)
                            'frmTopMost.Location = New centr 'Point(-2000, -2000) ' Fora da tela
                            frmTopMost.Show()

                            Dim resultado As DialogResult = MessageBox.Show(frmTopMost,
                                "Gostaria de Exportar os dados da lista de material? " & swFeat.Name,
                                "Lista de peças", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                            If resultado = DialogResult.Yes Then
                                Dim swBomFeat As Object = swFeat.GetSpecificFeature2
                                ProcessBomFeatureComformTabela(swModel, swBomFeat)
                            End If

                        End Using

                    End If
                    swFeat = swFeat.GetNextFeature()
                Catch ex As Exception
                    ' MsgBox("Erro ao ler a lista de dados da lista de material", MsgBoxStyle.Critical, "Atenção")
                    Continue Do

                End Try

            Loop
        Catch ex As Exception
        Finally
            ' MsgBox("Erro ao processar a lista de material: " & ex.Message)
        End Try
    End Sub

    Public Function LerDadosViewMontaPeca()

        Dim dv As New System.Data.DataView(TabelaViewMontaPeca)
        ' dv.RowFilter = "NomeArquivoSemExtensao = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "' AND D_E_L_E_T_E <> '*' OR D_E_L_E_T_E IS NULL"
        dv.RowFilter = "NomeArquivoSemExtensao = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'"

        Dim dtFiltrado As System.Data.DataTable = dv.ToTable()

        'For Each linha As DataRow In dtFiltrado.Rows
        '    ' Processa as linhas filtradas
        'Next

        For Each linha As DataRow In dtFiltrado.Rows

            Try
                If Trim(linha("NomeArquivoSemExtensao").ToString) = DadosArquivoCorrente.NomeArquivoSemExtensao Then
                    iconeTipoArquivo = My.Resources.sinco_Diversos  'My.Resources.material_escolar_32
                    Dim peso As Double
                    Dim PecaQtde As Double
                    ' Try
                    ' Obtém o valor da célula "PecaQtde" e remove espaços em branco
                    Dim pecaQtdeStr As String

                    Try

                        pecaQtdeStr = linha.Item("PecaQtde").ToString().Trim().Replace(",", ".")

                    Catch ex As Exception
                        pecaQtdeStr = 0
                    End Try

                    PecaQtde = cl_BancoDados.converteStringParaDouble(pecaQtdeStr)



                        Dim pecaPeso As String

                    Try
                        pecaPeso = linha.Item("Peso").ToString().Trim().Replace(",", ".")

                    Catch ex As Exception

                        pecaPeso = 0

                    End Try

                    peso = cl_BancoDados.converteStringParaDouble(pecaPeso)
                    ' MsgBox(peso)


                    Dim PesoMaterial As Double

                    Try
                        PesoMaterial = CDbl(peso) * CDbl(DadosArquivoCorrente.qtde)
                    Catch ex As Exception
                        PesoMaterial = 0
                    End Try

                    Dim QtdeMaterial As Double

                    Try
                        QtdeMaterial = CDbl(PecaQtde) * CDbl(DadosArquivoCorrente.qtde)
                    Catch ex As Exception
                        QtdeMaterial = 0
                    End Try


                    ' Preencher o DataGridView com os dados da peça
                    dgvDataGridBOM.Rows.Add(My.Resources.Sem_Incone,
                                                    iconeDXF,
                                                    iconePDF,
                                                    iconeTipoArquivo,
                                                iconeAtencao,
                                                Trim(linha.Item("IdMaterial").ToString.ToUpper), 'IdMaterial,
                                                Trim(linha.Item("NumeroRP").ToString.ToUpper), 'CodMatFabricante,
                                                Trim(linha.Item("CodMatFabricante").ToString.ToUpper),'DescResumo
                                                Trim(linha.Item("DescDetal").ToString.ToUpper),'DescDetal,
                                                "",'Autor,
                                                "",'Palavrachave,
                                                "",'Notas,
                                                "",'Espessura,
                                                "",'ComprimentoBlank,
                                                "",'LarguraBlank,
                                                "",'material,
                                                "",'AreaPintura,
                                                "",'NumeroDobras,
                                                PesoMaterial.ToString, 'Replace((Trim(TabelaViewMontaPeca.Rows(i).Item("Peso").ToString) * qtdePecaLm), ",", "."),'Peso,
                                                "",'EnderecoArquivo,
                                                "",'Acabamento,
                                                "",'soldagem,
                                                "MATERIAL",'TipoDesenho,
                                                "",'Corte,
                                                "",'Dobra,
                                                "",'Solda,
                                                "",'Pintura,
                                                "",'Montagem,
                                                "",'rnc,
                                                "",'Alturacaixadelimitadora,
                                                "",'Larguracaixadelimitadora,
                                                "",'Profundidadeaixadelimitadora,
                                                "",'ItemEstoque,
                                                QtdeMaterial.ToString,
                                                "",
                                                Trim(linha.Item("unidade").ToString.ToUpper)) 'Replace(Trim(TabelaViewMontaPeca.Rows(i).Item("PecaQtde").ToString * qtdePecaLm), ",", ".")) 'qtde,

                    iconeDXF = My.Resources.Sem_Incone
                    iconePDF = My.Resources.Sem_Incone
                    iconeTipoArquivo = My.Resources.Sem_Incone
                    iconeAtencao = My.Resources.Sem_Incone

                End If
            Catch ex As Exception
                Continue For
            Finally
            End Try

        Next

        'removento o tratamento de erro 04/04/2025
        'Catch ex As Exception
        'Finally
        'End Try

    End Function

    Private Sub FormatarColunaIconeDGVListaBom(ByVal EnderecoArquivo As String)

        ' Configuração dos ícones padrão para maior clareza
        iconeTipoArquivo = My.Resources.Sem_Incone
        iconeAtencao = My.Resources.verificado1
        iconePDF = My.Resources.Sem_Incone
        iconeDXF = My.Resources.Sem_Incone
        iconeLXDS = My.Resources.Sem_Incone

        ' Validar EndereçoArquivoCorrente
        DadosArquivoCorrente.EnderecoArquivo = EnderecoArquivo
        If String.IsNullOrEmpty(EnderecoArquivo) Then
            Exit Sub ' Sai se o endereço for inválido
        End If

        ''''''If DadosArquivoCorrente.TipoDesenho <> "MATERIAL" Then

        ''''''    If DadosArquivoCorrente.VerificarProcessodaPecaCorrente(swModel, False) = True Then

        ''''''        iconeAtencao = My.Resources.atencao

        ''''''    Else

        ''''''        iconeAtencao = My.Resources.verificado1

        ''''''    End If

        ''''''End If

        Dim VerificarArquivo As Func(Of String, String, Boolean) =
    Function(caminho, novaExtensao)
        If String.IsNullOrEmpty(caminho) Then
            '  MsgBox ("O caminho do arquivo está vazio ou é nulo.")
            Return False
        End If

        Dim novoCaminho = Path.ChangeExtension(caminho, novaExtensao)
        ' MsgBox($"Verificando arquivo: {novoCaminho}")

        Return Not String.IsNullOrEmpty(novoCaminho) AndAlso File.Exists(novoCaminho)
    End Function

        ' Verificar existência de arquivos PDF e DXF
        If VerificarArquivo(EnderecoArquivo, ".pdf") Then
            iconePDF = My.Resources.ficheiro_pdf
        End If
        If VerificarArquivo(EnderecoArquivo, ".dxf") Then
            iconeDXF = My.Resources.arquivo_dxf
        End If

        ' Definir o tipo de ícone com base na extensão
        Dim extensao As String = Path.GetExtension(EnderecoArquivo)?.ToUpperInvariant()
        Select Case extensao
            Case ".SLDASM"
                iconeTipoArquivo = My.Resources.IcopneMontagemSW
            Case ".SLDPRT"
                iconeTipoArquivo = My.Resources.IcopneMontagemPRT
        End Select

    End Sub

    Sub ProcessBomFeatureComformTabela(ByVal swModel As ModelDoc2, ByVal swBomFeat As BomFeature)
        Try

            Dim swFeat As Feature
            Dim vTableArr As Object
            Dim vTable As Object
            Dim vConfigArray As Object
            Dim vConfig As Object
            Dim ConfigName As String
            Dim swTable As TableAnnotation

            swFeat = swBomFeat.GetFeature
            vTableArr = swBomFeat.GetTableAnnotations

            For Each vTable In vTableArr
                swTable = vTable
                vConfigArray = swBomFeat.GetConfigurations(True, True)
                For Each vConfig In vConfigArray

                    Try

                        ConfigName = vConfig

                        ProcessTableAnnComformTabela(swModel, swTable, ConfigName)
                    Catch ex As Exception

                        MsgBox("Erro ao Ler item da Lista BOM", MsgBoxStyle.Critical, "Atenção")
                        Continue For

                    End Try

                Next vConfig

            Next vTable
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Sub ReadBOMFields(ByVal swModel As ModelDoc2, ByVal swTableAnn As TableAnnotation)
        Dim swBOMTable As BomTableAnnotation
        swBOMTable = swTableAnn

        If swBOMTable Is Nothing Then
            MsgBox("A tabela BOM não foi encontrada.")
            Exit Sub
        End If

        Dim nNumRows As Long
        nNumRows = swBOMTable.RowCount

        Dim nNumCols As Long
        nNumCols = swBOMTable.ColumnCount

        Dim columnNames As New List(Of String)

        ' Loop para obter os nomes das colunas
        For col As Integer = 0 To nNumCols - 1
            Dim colName As String
            colName = swBOMTable.GetColumnTitle(col)
            columnNames.Add(colName)
        Next

        ' Exibir os nomes das colunas
        MsgBox("Nomes das colunas na tabela BOM:" & vbCrLf & String.Join(vbCrLf, columnNames))

        ' Loop para obter os dados de cada célula
        For row As Integer = 1 To nNumRows - 1 ' Começa em 1 para ignorar o cabeçalho
            For col As Integer = 0 To nNumCols - 1
                Dim cellValue As String
                cellValue = swBOMTable.GetCellValue(row, col)
                ' Aqui você pode fazer o que quiser com cellValue, como armazenar ou exibir
            Next
        Next

        MsgBox("Leitura concluída!")
    End Sub

    ' ===========================================
    '  PROCESSA A BOM SEM TRAZER ITENS SUPRIMIDOS
    '  E RESPEITANDO "EXCLUIR DA BOM"
    ' ===========================================
    Async Sub ProcessTableAnnComformTabela(ByVal swModel As ModelDoc2, ByVal swTableAnn As TableAnnotation, ByVal ConfigName As String)

        Dim dxf As String
        Dim espessura As String
        Dim material As String
        Dim Valido As Boolean = True

        Dim nNumRow As Long = swTableAnn.RowCount
        Dim swBOMTableAnn As BomTableAnnotation = TryCast(swTableAnn, BomTableAnnotation)
        If swBOMTableAnn Is Nothing Then
            MsgBox("A Tabela selecionada não é uma BOM válida.", vbExclamation, "Atenção")
            Exit Sub
        End If

        Dim extensoesPermitidas As String() = {".sldprt", ".sldasm"}

        Dim tempTable As New System.Data.DataTable
        tempTable.Columns.Add("qtdePeca", GetType(Double))
        tempTable.Columns.Add("EnderecoArquivo", GetType(String))
        tempTable.Columns.Add("CodMatFabricante", GetType(String))

        ' Opcional: dicionário para lookup rápido (evita loop por DataTable a cada inserção)
        Dim idxPorCodigo As New Dictionary(Of String, DataRow)(StringComparer.OrdinalIgnoreCase)

        iconeDXF = My.Resources.Sem_Incone
        iconePDF = My.Resources.Sem_Incone
        iconeTipoArquivo = My.Resources.Sem_Incone
        iconeAtencao = My.Resources.Sem_Incone

        ProgressBarListaSW.Minimum = 0
        ProgressBarListaSW.Maximum = System.Math.Max(0, CInt(nNumRow - 1))

        ''''''' -------------------------------
        ''''''' 1) LÊ A BOM E AGREGA ITENS VÁLIDOS
        ''''''' -------------------------------
        ''''''For J As Integer = 1 To nNumRow - 1

        ''''''    Dim vPtArr As Object = swBOMTableAnn.GetComponents2(J, ConfigName)
        ''''''    If vPtArr Is Nothing Then
        ''''''        ProgressBarListaSW.Value = J
        ''''''        Continue For
        ''''''    End If

        ''''''    Dim qtdeValidas As Integer = 0
        ''''''    Dim primeiroCaminhoValido As String = Nothing
        ''''''    Dim primeiroNomeSemExtensao As String = Nothing

        ''''''    ' Filtra componentes válidos (não suprimidos, não excluídos da BOM, com documento e caminho)
        ''''''    For i As Integer = 0 To UBound(vPtArr)
        ''''''        Dim comp As IComponent2 = TryCast(vPtArr(i), IComponent2)
        ''''''        If comp Is Nothing Then Continue For

        ''''''        If Not EhContavelNaBOM(comp, ConfigName) Then
        ''''''            Continue For
        ''''''        End If

        ''''''        Dim pathTemp As String = SafeGetPath(comp)
        ''''''        If String.IsNullOrWhiteSpace(pathTemp) Then Continue For
        ''''''        ' Registra o primeiro caminho/nome válidos para representar a "peça" dessa linha
        ''''''        If String.IsNullOrEmpty(primeiroNomeSemExtensao) Then
        ''''''            primeiroCaminhoValido = pathTemp.ToUpperInvariant().Trim()
        ''''''            primeiroNomeSemExtensao = Path.GetFileNameWithoutExtension(pathTemp).ToUpperInvariant().Trim()
        ''''''        End If

        ''''''        qtdeValidas += 1
        ''''''    Next
        ''''''    ' Se nada válido sobrou, pula a linha
        ''''''    If qtdeValidas <= 0 OrElse String.IsNullOrWhiteSpace(primeiroNomeSemExtensao) Then
        ''''''        ProgressBarListaSW.Value = J
        ''''''        Continue For
        ''''''    End If

        ''''''    ' Agrega por CodMatFabricante (nome do arquivo sem extensão)
        ''''''    Dim key As String = primeiroNomeSemExtensao
        ''''''    Dim row As DataRow = Nothing
        ''''''    If idxPorCodigo.TryGetValue(key, row) Then
        ''''''        row("qtdePeca") = CDbl(row("qtdePeca")) + qtdeValidas
        ''''''    Else
        ''''''        row = tempTable.NewRow()
        ''''''        row("qtdePeca") = qtdeValidas
        ''''''        row("EnderecoArquivo") = primeiroCaminhoValido
        ''''''        row("CodMatFabricante") = key
        ''''''        tempTable.Rows.Add(row)
        ''''''        idxPorCodigo(key) = row
        ''''''    End If

        ''''''    ProgressBarListaSW.Value = J
        ''''''Next
        '''

        ' -------------------------------
        ' 1) LÊ A BOM E AGREGA ITENS VÁLIDOS (com ocultos/lightweight incluídos)
        ' -------------------------------

        ' Helpers locais (lambdas) para manter tudo neste escopo:

        ' - Conta na BOM: exclui apenas suprimidos e "Exclude from BOM".
        Dim EhContavelNaBOM As Func(Of IComponent2, String, Boolean) =
    Function(comp As IComponent2, cfg As String) As Boolean
        If comp Is Nothing Then Return False

        ' Suprimido?
        Dim supp = CType(comp.GetSuppression2(), swComponentSuppressionState_e)
        If supp = swComponentSuppressionState_e.swComponentSuppressed Then Return False

        ' Exclude from BOM? (protege caso a propriedade não exista em versões antigas)
        Dim excl As Boolean = False
        Try
            excl = comp.ExcludeFromBOM
        Catch
            excl = False
        End Try
        If excl Then Return False

        ' NÃO filtrar por visibilidade/estado lightweight
        Return True
    End Function

        ' - Path "seguro": tenta Component.Path, depois ModelDoc, depois Title/Name2.
        Dim SafeGetPathLocal As Func(Of IComponent2, String) =
    Function(comp As IComponent2) As String
        If comp Is Nothing Then Return ""
        Dim p As String = comp.GetPathName()
        If Not String.IsNullOrWhiteSpace(p) Then Return p

        Dim md As ModelDoc2 = TryCast(comp.GetModelDoc2(), ModelDoc2)
        If md IsNot Nothing Then
            p = md.GetPathName()
            If Not String.IsNullOrWhiteSpace(p) Then Return p
            Dim t = md.GetTitle()
            If Not String.IsNullOrWhiteSpace(t) Then Return t
        End If

        Return comp.Name2 ' fallback (virtual/levezinha/sem salvar)
    End Function

        ' - Nome-base para agrupar: independente de ter path físico.
        Dim GetNomeBase As Func(Of String, IComponent2, String) =
    Function(pathOuNome As String, comp As IComponent2) As String
        Dim s As String = pathOuNome
        If String.IsNullOrWhiteSpace(s) AndAlso comp IsNot Nothing Then s = comp.Name2

        ' Se vier Name2 de peça virtual, costuma ter '^'
        Dim caret As Integer = s.IndexOf("^"c)
        If caret >= 0 Then s = s.Substring(0, caret)

        ' Tenta extrair somente o nome do arquivo
        Try
            Dim fname = IO.Path.GetFileName(s)
            If Not String.IsNullOrWhiteSpace(fname) Then s = fname
        Catch
            ' Se não for um caminho, mantém s
        End Try

        ' Remove extensão conhecida
        If s.EndsWith(".SLDPRT", StringComparison.OrdinalIgnoreCase) OrElse
           s.EndsWith(".SLDASM", StringComparison.OrdinalIgnoreCase) Then
            Try
                s = IO.Path.GetFileNameWithoutExtension(s)
            Catch
                ' ignora
            End Try
        End If

        Return (If(s, "")).ToUpperInvariant().Trim()
    End Function

        ' ------------ Loop principal da BOM ------------
        For J As Integer = 1 To nNumRow - 1

            Dim vPtArr As Object = swBOMTableAnn.GetComponents2(J, ConfigName)
            If vPtArr Is Nothing Then
                ' Atualiza progressivo sem travar UI (reduz flicker)
                If J Mod 8 = 0 Then ProgressBarListaSW.Value = J
                Continue For
            End If

            Dim qtdeValidas As Integer = 0
            Dim primeiroCaminhoValido As String = Nothing
            Dim primeiroNomeSemExtensao As String = Nothing

            ' Percorre os componentes da linha da BOM
            For i As Integer = 0 To UBound(vPtArr)
                Dim comp As IComponent2 = TryCast(vPtArr(i), IComponent2)
                If comp Is Nothing Then Continue For

                ' Conta somente se não for suprimido e não for Exclude from BOM
                If Not EhContavelNaBOM(comp, ConfigName) Then Continue For

                ' Pega um identificador que funciona para lightweight/virtual/oculto
                Dim rawPath As String = SafeGetPathLocal(comp)
                Dim nomeBase As String = GetNomeBase(rawPath, comp)

                ' Registra o primeiro representativo (para preencher EnderecoArquivo e chave)
                If String.IsNullOrEmpty(primeiroNomeSemExtensao) Then
                    primeiroNomeSemExtensao = nomeBase
                    primeiroCaminhoValido = If(String.IsNullOrWhiteSpace(rawPath), comp.Name2, rawPath)
                    If primeiroCaminhoValido IsNot Nothing Then
                        primeiroCaminhoValido = primeiroCaminhoValido.ToUpperInvariant().Trim()
                    End If
                End If

                qtdeValidas += 1
            Next

            ' Se nada válido sobrou, pula a linha
            If qtdeValidas <= 0 OrElse String.IsNullOrWhiteSpace(primeiroNomeSemExtensao) Then
                If J Mod 8 = 0 Then ProgressBarListaSW.Value = J
                Continue For
            End If

            ' Agrega por CodMatFabricante (nome base sem extensão)
            Dim key As String = primeiroNomeSemExtensao
            Dim row As DataRow = Nothing

            If idxPorCodigo.TryGetValue(key, row) Then
                row("qtdePeca") = CDbl(row("qtdePeca")) + qtdeValidas
            Else
                row = tempTable.NewRow()
                row("qtdePeca") = qtdeValidas
                row("EnderecoArquivo") = primeiroCaminhoValido
                row("CodMatFabricante") = key
                tempTable.Rows.Add(row)
                idxPorCodigo(key) = row
            End If

            If J Mod 8 = 0 Then
                ProgressBarListaSW.Value = J
                ' Opcional: mantém UI responsiva em BOMs muito grandes
                'Application.DoEvents()
            End If
        Next

        ' ---------------------------------------------
        ' 2) PÓS-PROCESSAMENTO: ABRE, EXPORTA E PREENCHE GRID
        ' ---------------------------------------------
        ProgressBarListaSW.Value = 0
        ProgressBarListaSW.Maximum = tempTable.Rows.Count
        Dim progresso As Integer = 0

        For Each row As DataRow In tempTable.Rows

            Dim caminhoArquivo As String = row("EnderecoArquivo").ToString().ToUpper().Trim()
            Dim pdf As String = caminhoArquivo.Replace(".SLDPRT", ".PDF").Replace(".SLDASM", ".PDF")
            Dim dxf1 As String = caminhoArquivo.Replace(".SLDPRT", ".DXF").Replace(".SLDASM", ".DXF")

            Try
                ' Só processa peças/montagens com extensões permitidas
                If extensoesPermitidas.Any(Function(ext) caminhoArquivo.EndsWith(ext, StringComparison.OrdinalIgnoreCase)) Then

                    OpenDocumentAndWait(caminhoArquivo, True, swModel)

                    If DadosArquivoCorrente.Bloqueado <> "S" Then
                        ' Exporta DXF, se habilitado e não bloqueado por "bloquear existente"
                        If chkConverterDXF.Checked Then
                            If Not (choBloqueaArquivoExistente.Checked AndAlso File.Exists(dxf1)) Then
                                DadosArquivoCorrente.ExportDXF2(swModel, False, True)
                                iconeDXF = My.Resources.arquivo_dxf
                            End If
                        End If

                        ' Exporta PDF a partir do desenho (se houver) e/ou troca formato
                        Dim SLDDRW As String = caminhoArquivo.Replace(".SLDPRT", ".SLDDRW").Replace(".SLDASM", ".SLDDRW")
                        If chkTrocarFormato.Checked OrElse chkConverterPDF.Checked Then
                            If Not (choBloqueaArquivoExistente.Checked AndAlso File.Exists(pdf)) Then
                                If File.Exists(SLDDRW) Then
                                    OpenDocumentAndWait(SLDDRW, True, swModel)
                                    Try
                                        If chkTrocarFormato.Checked Then
                                            DadosArquivoCorrente.TrocarFormatoA3(swModel)
                                        End If
                                        If chkConverterPDF.Checked Then
                                            DadosArquivoCorrente.ExportToPDF(swModel, SLDDRW, True)
                                            iconePDF = My.Resources.ficheiro_pdf
                                        End If
                                    Finally
                                        swapp.CloseDoc(SLDDRW)
                                    End Try
                                End If
                            End If
                        End If
                    End If

                    ' Reabre a peça/montagem, atualiza metadados e ícones
                    OpenDocumentAndWait(caminhoArquivo, True, swModel)

                    DadosArquivoCorrente.AtualizaDesenho(swModel, False)
                    FormatarColunaIconeDGVListaBom(DadosArquivoCorrente.EnderecoArquivo)

                    If DadosArquivoCorrente.VerificarProcessodaPecaCorrente(swModel, False) = False Then
                        DadosArquivoCorrente.rnc = ""
                        iconeAtencao = My.Resources.verificado
                    Else
                        DadosArquivoCorrente.rnc = "S"
                        iconeAtencao = My.Resources.atencao
                    End If


                    If My.Settings.BancoDadosAtivo = "alfatec2" Then

                        If DadosArquivoCorrente.Espessura <> "" And DadosArquivoCorrente.material <> "" Then

                            DadosArquivoCorrente.Corte = "1"

                        End If

                        If DadosArquivoCorrente.NumeroDobras <> "" Then

                            DadosArquivoCorrente.Dobra = "1"

                        End If

                    End If


                    ' Adiciona linha na grid
                    dgvDataGridBOM.Rows.Add(
                    My.Resources.Sem_Incone, iconeDXF, iconePDF, iconeTipoArquivo, iconeAtencao,
                    DadosArquivoCorrente.IdMaterial,
                    DadosArquivoCorrente.NomeArquivoSemExtensao,
                    DadosArquivoCorrente.Titulo,
                    DadosArquivoCorrente.AssuntoSubiTitulo,
                    DadosArquivoCorrente.Author,
                    DadosArquivoCorrente.PalavraChave,
                    DadosArquivoCorrente.Comentarios,
                    DadosArquivoCorrente.Espessura,
                    DadosArquivoCorrente.ComprimentoBlank,
                    DadosArquivoCorrente.LarguraBlank,
                    DadosArquivoCorrente.material,
                    DadosArquivoCorrente.AreaPintura,
                    DadosArquivoCorrente.NumeroDobras,
                    DadosArquivoCorrente.Massa,
                    DadosArquivoCorrente.EnderecoArquivo,
                    DadosArquivoCorrente.Acabamento,
                    DadosArquivoCorrente.soldagem,
                    DadosArquivoCorrente.TipoDesenho,
                    DadosArquivoCorrente.Corte,
                    DadosArquivoCorrente.Dobra,
                    DadosArquivoCorrente.Solda,
                    DadosArquivoCorrente.Pintura,
                    DadosArquivoCorrente.Montagem,
                    DadosArquivoCorrente.rnc,
                    DadosArquivoCorrente.Alturacaixadelimitadora,
                    DadosArquivoCorrente.Larguracaixadelimitadora,
                    DadosArquivoCorrente.Profundidadeaixadelimitadora,
                    DadosArquivoCorrente.ItemEstoque,
                    row("qtdePeca")
                )

                    DadosArquivoCorrente.qtde = row("qtdePeca")

                    ' Reset ícones para próxima iteração
                    iconeDXF = My.Resources.Sem_Incone
                    iconePDF = My.Resources.Sem_Incone
                    iconeTipoArquivo = My.Resources.Sem_Incone
                    iconeAtencao = My.Resources.Sem_Incone

                    LerDadosViewMontaPeca()

                    ' Fecha para liberar memória
                    swapp.CloseDoc(caminhoArquivo)
                    cl_BancoDados.FecharArquivoMemoria()
                    IntanciaSolidWorks.LiberarRecurso(swModel)
                    IntanciaSolidWorks.LiberarRecurso(swPart) ' assumindo variável global
                End If
            Catch ex As Exception
                ' Garante o fechamento para evitar reter documentos
                Try : swapp.CloseDoc(caminhoArquivo) : Catch : End Try
                ' Segue para o próximo
            End Try

            progresso += 1
            If progresso <= ProgressBarListaSW.Maximum Then
                ProgressBarListaSW.Value = progresso
            End If
        Next

        ' ---------------------------------------------
        ' 3) VALIDAÇÃO DE DXF POR LINHA DA GRID (VISUAL)
        ' ---------------------------------------------
        If dgvDataGridBOM.Rows.Count > 0 Then
            For a As Integer = 0 To dgvDataGridBOM.Rows.Count - 1
                Try
                    If a <> 1 Then
                        Dim caminho As String = dgvDataGridBOM.Rows(a).Cells("EnderecoArquivo").Value.ToString().ToLower()
                        If caminho.EndsWith(".sldprt") OrElse caminho.EndsWith(".sldasm") Then
                            swapp.CloseDoc(caminho)
                            swapp.CloseDoc(caminho.Replace(".sldprt", ".slddrw").Replace(".sldasm", ".slddrw"))
                        End If
                    End If
                Catch
                    ' ignora
                End Try

                Try
                    If a <= ProgressBarListaSW.Maximum Then
                        ProgressBarListaSW.Value = a
                    Else
                        ProgressBarListaSW.Value = 0
                    End If
                Catch
                    ProgressBarListaSW.Value = 0
                End Try

                Try
                    dxf = Path.ChangeExtension(dgvDataGridBOM.Rows(a).Cells("EnderecoArquivo").Value.ToString, ".dxf")
                Catch
                    dxf = ""
                End Try

                Try
                    espessura = dgvDataGridBOM.Rows(a).Cells("espessura").Value.ToString
                Catch
                    espessura = ""
                End Try

                Try
                    material = dgvDataGridBOM.Rows(a).Cells("materialsw").Value.ToString
                Catch
                    material = ""
                End Try

                If espessura <> "" AndAlso material <> "" AndAlso dxf <> "" Then
                    If Not File.Exists(dxf) Then
                        dgvDataGridBOM.Rows(a).DefaultCellStyle.BackColor = Color.LightSalmon
                        Valido = False
                    End If
                End If
            Next
        End If

        ProgressBarListaSW.Value = 0
        chkConverterPDF.Checked = False
        chkConverterDXF.Checked = False

        tempTable.Dispose()
        Cursor.Current = Cursors.Default

        MsgBox("Processo Finalizado com sucesso!", vbInformation, "Atenção")

    End Sub

    ' ===========================================
    ' HELPERS
    ' ===========================================

    ' Caminho seguro do componente (trata virtual/sem doc)
    Private Function SafeGetPath(comp As IComponent2) As String
        Try
            Dim p = comp.GetPathName()
            If Not String.IsNullOrWhiteSpace(p) Then Return p
        Catch
        End Try
        ' Tenta via doc
        Try
            Dim m As ModelDoc2 = comp.GetModelDoc2()
            If m IsNot Nothing Then
                Dim p2 = m.GetPathName()
                If Not String.IsNullOrWhiteSpace(p2) Then Return p2
            End If
        Catch
        End Try
        Return Nothing
    End Function

    ' Testa se o componente deve ser contado na BOM
    Private Function EhContavelNaBOM(comp As IComponent2, configName As String) As Boolean
        If comp Is Nothing Then Return False

        ' 1) Suprimido na configuração?
        Try
            If EstaSuprimido(comp, configName) Then Return False
        Catch
            Return False ' por prudência, não conta
        End Try

        ' 2) Oculto (opcional – remova se quiser contar ocultos)
        Try
            If comp.IsHidden(True) Then Return False
        Catch
        End Try

        ' 3) Documento subjacente válido?
        Dim mdl As ModelDoc2 = Nothing
        Try
            mdl = comp.GetModelDoc2()
        Catch
        End Try
        If mdl Is Nothing Then Return False

        ' 4) Excluído da BOM (propriedades conhecidas)
        Dim cfgRef As String = Nothing
        Try
            cfgRef = comp.ReferencedConfiguration
        Catch
        End Try
        If String.IsNullOrEmpty(cfgRef) Then cfgRef = configName

        Try
            Dim ext As ModelDocExtension = mdl.Extension
            If ext IsNot Nothing Then
                Dim mgrCfg As CustomPropertyManager = ext.CustomPropertyManager(cfgRef)
                Dim mgrGen As CustomPropertyManager = ext.CustomPropertyManager("")

                If PropriedadeVerdadeira(mgrCfg, "SW-ExcludeFromBOM") OrElse
               PropriedadeVerdadeira(mgrGen, "SW-ExcludeFromBOM") OrElse
               PropriedadeVerdadeira(mgrCfg, "SW-BOM Exclude") OrElse
               PropriedadeVerdadeira(mgrGen, "SW-BOM Exclude") Then
                    Return False
                End If
            End If
        Catch
            ' se falhar ao ler propriedade, não exclui por isso
        End Try

        Return True
    End Function

    ' Compatível com PIA que tem só Get5 (sem linkToProp)
    Private Function PropriedadeVerdadeira(mgr As CustomPropertyManager, propName As String) As Boolean
        If mgr Is Nothing Then Return False

        Dim val As String = Nothing
        Dim resVal As String = Nothing
        Dim wasResolved As Boolean = False

        Try
            Dim ret As Integer = mgr.Get5(propName, False, val, resVal, wasResolved)
            If ret = 1 Then
                Dim s As String = If(String.IsNullOrEmpty(resVal), val, resVal)
                If String.IsNullOrEmpty(s) Then Return False
                s = s.Trim().ToUpperInvariant()
                Return (s = "YES" OrElse s = "TRUE" OrElse s = "SIM" OrElse s = "1")
            End If
        Catch
        End Try

        Return False
    End Function

    ' Trata diferentes versões do Interop: IsSuppressed2, IsSuppressed, GetSuppression2
    Private Function EstaSuprimido(compObj As Object, configName As String) As Boolean
        ' 1) Early-binding preferencial
        Dim c As IComponent2 = TryCast(compObj, IComponent2)
        If c IsNot Nothing Then
            ' a) IsSuppressed2(opção, Nothing) – versões mais novas
            Try
                Dim suprimido As Boolean = c.IsSuppressed2(CType(swInConfigurationOpts_e.swThisConfiguration, Integer), Nothing)
                Return suprimido
            Catch
            End Try

            ' b) IsSuppressed() – versões antigas
            Try
                Return c.IsSuppressed()
            Catch
            End Try

            ' c) GetSuppression2() – SEM parâmetros nesta PIA
            Try
                Dim st As Integer = c.GetSuppression2()
                ' Conta como suprimido SOMENTE quando o estado for "Suppressed"
                Return (st = CInt(swComponentSuppressionState_e.swComponentSuppressed))
            Catch
            End Try
        End If

        ' 2) Fallback por late-binding (CallByName), cobrindo todas
        ' IsSuppressed2(opção, Nothing)
        Try
            Dim res As Object = CallByName(compObj, "IsSuppressed2", CallType.Method, CType(swInConfigurationOpts_e.swThisConfiguration, Integer), Nothing)
            If TypeOf res Is Boolean Then Return CBool(res)
        Catch
        End Try

        ' IsSuppressed()
        Try
            Dim res As Object = CallByName(compObj, "IsSuppressed", CallType.Method)
            If TypeOf res Is Boolean Then Return CBool(res)
        Catch
        End Try

        ' GetSuppression2() – SEM parâmetros
        Try
            Dim st As Object = CallByName(compObj, "GetSuppression2", CallType.Method)
            If TypeOf st Is Integer Then
                Return (CInt(st) = CInt(swComponentSuppressionState_e.swComponentSuppressed))
            End If
        Catch
        End Try

        ' 3) Se não foi possível determinar, por prudência NÃO conta (trata como suprimido)
        Return True
    End Function

    Private Function SafeToString(cellValue As Object) As String
        If cellValue Is Nothing OrElse IsDBNull(cellValue) Then
            Return ""
        End If
        Return cellValue.ToString()
    End Function

    Private Sub AlterarEstiloDataGrid(rowIndex As Integer, dgv As DataGridView, Optional cellName As String = "", Optional cellColor As Color = Nothing, Optional rowColor As Color = Nothing)
        ' Verifica se foi definido um valor para rowColor e aplica à linha inteira
        If rowColor <> Nothing Then
            dgv.Rows(rowIndex).DefaultCellStyle.BackColor = rowColor
        End If

        ' Verifica se foi definido um valor para cellColor e aplica à célula específica, se cellName for fornecido
        If Not String.IsNullOrEmpty(cellName) AndAlso cellColor <> Nothing Then
            dgv.Rows(rowIndex).Cells(cellName).Style.BackColor = cellColor
        End If
    End Sub

    Private Sub RestartSolidWorks()

        System.Threading.Thread.Sleep(100) ' Delay de 100 milissegundos

        Try
            swapp.ExitApp()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(swapp)
            swapp = Nothing

            ' Criar uma nova instância do SolidWorks
            swapp = New SldWorks()
        Catch ex As Exception
            ' Console.WriteLine($"Erro ao reiniciar o SolidWorks: {ex.Message}")
        Finally

        End Try
    End Sub

    Private Sub DisconnectSolidWorks()
        If swapp IsNot Nothing Then
            swapp.ExitApp()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(swapp)
            swapp = Nothing
        End If
        GC.Collect()
    End Sub

    '''''Sub OpenDocumentAndWait(filePath As String, ByVal visualizarDesenho As Boolean, ByRef swModel As ModelDoc2)

    '''''    Try
    '''''        ' Verifica se o SolidWorks está rodando antes de abrir o documento
    '''''        Dim solidWorksProcess As Process = GetSolidWorksProcess()

    '''''        If solidWorksProcess Is Nothing Then
    '''''            Throw New Exception("O SolidWorks não está em execução.")
    '''''        End If

    '''''        ' Conecta ao SolidWorks
    '''''        IntanciaSolidWorks.ConectarSolidWorks()

    '''''        ' Obtém o tipo de documento baseado no arquivo
    '''''        Dim docType As swDocumentTypes_e = GetDocumentType(filePath)

    '''''        ' Define opções para abrir o documento em modo exclusivo
    '''''        Dim openOptions As Integer = swOpenDocOptions_e.swOpenDocOptions_Silent Or
    '''''                          swOpenDocOptions_e.swOpenDocOptions_LoadModel

    '''''        ' Variáveis para erros e avisos
    '''''        Dim errors As Integer = 0
    '''''        Dim warnings As Integer = 0

    '''''        swModel = swapp.OpenDoc6(filePath, docType, openOptions, "", errors, warnings)

    '''''        ' Verifica se houve falha ao abrir
    '''''        If swModel Is Nothing OrElse errors <> 0 Then
    '''''            Throw New Exception($"Falha ao abrir o documento. Código de erro: {errors}")
    '''''        End If

    '''''        swModel = swapp.ActivateDoc(filePath)
    '''''        If swModel Is Nothing Then
    '''''            Throw New Exception("Falha ao ativar o documento.")
    '''''        End If

    '''''        ' Verifica se o documento foi carregado completamente
    '''''        If Not WaitForDocumentToLoad(swModel, docType) Then
    '''''            Throw New Exception("O documento não foi carregado completamente após várias tentativas.")
    '''''        End If

    '''''        If vemdalista = False Then

    '''''            ' Ajusta a visualização caso seja um desenho
    '''''            If docType = swDocumentTypes_e.swDocDRAWING AndAlso visualizarDesenho Then
    '''''                '   swModel.ViewZoomtofit2()
    '''''            End If

    '''''        End If

    '''''    Catch ex As Exception
    '''''        ' Log ou mensagem de erro
    '''''        '  MsgBox($"Erro ao abrir o documento: {ex.Message}")

    '''''        ' Chamada da função de tratamento de erro de forma assíncrona
    '''''        Task.Run(Sub()
    '''''                     ClasseEmail.EmailTratamentoErro("Erro ao atualizar ou inserir dados no banco: " & ex.Message)
    '''''                 End Sub)

    '''''        ' ClasseEmail.EmailTratamentoErro(ex.Message)

    '''''    Finally

    '''''        ' Verifica se o SolidWorks ainda está ativo
    '''''        'Dim solidWorksProcess As Process = GetSolidWorksProcess()
    '''''        'If solidWorksProcess Is Nothing OrElse solidWorksProcess.HasExited Then
    '''''        '    MsgBox("O SolidWorks foi fechado inesperadamente.", MsgBoxStyle.Critical)
    '''''        'End If

    '''''    End Try

    '''''End Sub

    '' teste peças ocultas ou peso leve 25/09/2025
    Sub OpenDocumentAndWait(filePath As String, ByVal visualizarDesenho As Boolean, ByRef swModel As ModelDoc2)
        Try
            ' 1) Garante SW ativo
            Dim solidWorksProcess As Process = GetSolidWorksProcess()
            If solidWorksProcess Is Nothing Then
                Throw New Exception("O SolidWorks não está em execução.")
            End If

            ' 2) Conecta
            IntanciaSolidWorks.ConectarSolidWorks()

            ' 3) Descobre tipo e abre
            Dim docType As swDocumentTypes_e = GetDocumentType(filePath)

            Dim openOptions As Integer =
            swOpenDocOptions_e.swOpenDocOptions_Silent Or
            swOpenDocOptions_e.swOpenDocOptions_LoadModel   ' evita prompt e garante carga do modelo

            Dim errors As Integer = 0
            Dim warnings As Integer = 0
            swModel = swapp.OpenDoc6(filePath, docType, openOptions, "", errors, warnings)
            If swModel Is Nothing OrElse errors <> 0 Then
                Throw New Exception($"Falha ao abrir o documento. Código de erro: {errors}")
            End If

            ' 4) Ativa doc
            swModel = swapp.ActivateDoc(filePath)
            If swModel Is Nothing Then Throw New Exception("Falha ao ativar o documento.")

            ' 5) Espera carregar por completo
            If Not WaitForDocumentToLoad(swModel, docType) Then
                Throw New Exception("O documento não foi carregado completamente após várias tentativas.")
            End If

            ' 6) === GARANTIR: sem lightweight e sem ocultos ===
            Select Case docType
                Case swDocumentTypes_e.swDocASSEMBLY
                    GarantirResolvidoEVisivel_Assembly(TryCast(swModel, AssemblyDoc))

                Case swDocumentTypes_e.swDocDRAWING
                    GarantirResolvidoEVisivel_Drawing(TryCast(swModel, DrawingDoc))
            End Select

            ' 7) Visual (opcional)
            If (docType = swDocumentTypes_e.swDocDRAWING) AndAlso visualizarDesenho AndAlso (Not vemdalista) Then
                ' swModel.ViewZoomtofit2()
            End If
        Catch ex As Exception
            ' Log assíncrono
            Task.Run(Sub()
                         ClasseEmail.EmailTratamentoErro("Erro ao abrir/normalizar documento: " & ex.Message)
                     End Sub)
            'MsgBox($"Erro ao abrir o documento: {ex.Message}")
        Finally
            ' pós-processo se necessário
        End Try
    End Sub

    ' ==========================================================
    ' Helpers: resolver lightweight e desocultar (montagem)
    ' ==========================================================
    Private Sub GarantirResolvidoEVisivel_Assembly(asm As AssemblyDoc)
        If asm Is Nothing Then Exit Sub

        ' Resolve todos componentes lightweight (topo + subníveis)
        Try
            ' Algumas versões têm assinatura diferente; tente com/sem parâmetro
            Try
                ' Preferencial (se disponível): parâmetro True resolve recursivamente
                CallByName(asm, "ResolveAllLightWeightComponents", CallType.Method, True)
            Catch
                CallByName(asm, "ResolveAllLightWeightComponents", CallType.Method)
            End Try
        Catch
            ' se a API variar por versão, ignoramos a exceção
        End Try

        ' Desocultar toda a árvore
        Dim root As Component2 = TryGetRootComponent(asm)
        If root IsNot Nothing Then
            UnhideRecursive(root)
        End If

        ' Rebuild para estabilizar
        Dim md As ModelDoc2 = TryCast(asm, ModelDoc2)
        md?.EditRebuild3()
    End Sub

    ' ==========================================================
    ' Helpers: desenho → normaliza views e montagens referenciadas
    ' ==========================================================
    Private Sub GarantirResolvidoEVisivel_Drawing(drw As DrawingDoc)
        If drw Is Nothing Then Exit Sub

        Dim md As ModelDoc2 = TryCast(drw, ModelDoc2)

        ' Percorre as views (a primeira é a "Sheet", as próximas são as views válidas)
        Dim v As View = drw.GetFirstView()
        If v IsNot Nothing Then v = v.GetNextView()

        While v IsNot Nothing
            Try
                ' Documento referenciado pela view
                Dim refDoc As ModelDoc2 = TryCast(v.ReferencedDocument, ModelDoc2)
                If refDoc IsNot Nothing AndAlso refDoc.GetType() = swDocumentTypes_e.swDocASSEMBLY Then
                    GarantirResolvidoEVisivel_Assembly(TryCast(refDoc, AssemblyDoc))
                End If

                ' Rebuild da view (se disponível) ou do drawing
                Try
                    CallByName(v, "Rebuild", CallType.Method)
                Catch
                    ' fallback: rebuild no doc
                    md?.EditRebuild3()
                End Try
            Catch
                ' segue para próxima view mesmo que uma falhe
            End Try

            v = v.GetNextView()
        End While

        ' Rebuild final do drawing
        md?.ForceRebuild3(False)
    End Sub

    ' ==========================================================
    ' Utilitários de árvore/visibilidade
    ' ==========================================================
    Private Function TryGetRootComponent(asm As AssemblyDoc) As Component2
        If asm Is Nothing Then Return Nothing
        Try
            ' Versões novas
            Dim root = TryCast(CallByName(asm, "GetRootComponent2", CallType.Method, True), Component2)
            If root IsNot Nothing Then Return root
        Catch
        End Try
        Try
            ' Versões antigas
            Dim rootLegacy = TryCast(CallByName(asm, "GetRootComponent", CallType.Method), Component2)
            Return rootLegacy
        Catch
        End Try
        Return Nothing
    End Function

    Private Sub UnhideRecursive(comp As Component2)
        If comp Is Nothing Then Exit Sub

        ' Tenta marcar como visível (API varia entre versões)
        Try
            ' Propriedade Visible (algumas versões permitem set)
            comp.Visible = swComponentVisibilityState_e.swComponentVisible
        Catch
            ' Fallback via seleção + mostrar
            Try
                comp.Select4(False, Nothing, False)
                Dim asm As AssemblyDoc = TryCast(comp.GetModelDoc2(), AssemblyDoc)
                If asm Is Nothing Then
                    ' se comp pertence a uma submontagem, pegue o doc da montagem proprietária
                    Dim md As ModelDoc2 = comp.GetModelDoc2()
                    asm = TryCast(md, AssemblyDoc)
                End If
                If asm IsNot Nothing Then
                    ' ShowComponent2(true) costuma exibir componente(s) selecionados
                    CallByName(asm, "ShowComponent2", CallType.Method, True)
                End If
            Catch
                ' ignora se não for possível
            End Try
        End Try

        ' Resolve filhos lightweight (se algum sobrou) e desoculta recursivamente
        Dim children As Object = Nothing
        Try
            children = comp.GetChildren()
        Catch
            children = Nothing
        End Try

        If children Is Nothing Then Exit Sub

        Dim arr = TryCast(children, Object())
        If arr Is Nothing Then Exit Sub

        For Each ch In arr
            Dim c As Component2 = TryCast(ch, Component2)
            If c Is Nothing Then Continue For
            UnhideRecursive(c)
        Next
    End Sub

    ' Função para obter o processo do SolidWorks
    Private Function GetSolidWorksProcess() As Process
        Return Process.GetProcessesByName("SLDWORKS").FirstOrDefault()
    End Function

    Private Function WaitForDocumentToLoad(ByVal swModel As ModelDoc2, ByVal docType As swDocumentTypes_e) As Boolean
        Dim maxRetries As Integer = 10
        Dim retryInterval As Integer = 100 ' Tempo em milissegundos

        For retryCount As Integer = 1 To maxRetries
            ' Verifica se o documento está carregado baseado no tipo
            Select Case docType
                Case swDocumentTypes_e.swDocPART
                    If TypeOf swModel Is PartDoc Then Return True
                Case swDocumentTypes_e.swDocASSEMBLY
                    If TypeOf swModel Is AssemblyDoc Then Return True
                Case swDocumentTypes_e.swDocDRAWING
                    If TypeOf swModel Is DrawingDoc Then Return True
            End Select

            ' Aguarda antes de tentar novamente
            Threading.Thread.Sleep(retryInterval)
        Next

        ' Retorna falso se não foi possível confirmar o carregamento
        Return False
    End Function

    ' Função para obter o tipo de documento com base na extensão do arquivo
    Private Function GetDocumentType(ByVal filePath As String) As swDocumentTypes_e
        ExtensaoArquivoCorrente = IO.Path.GetExtension(filePath).ToLower()
        Select Case ExtensaoArquivoCorrente
            Case ".sldprt"
                Return swDocumentTypes_e.swDocPART
            Case ".sldasm"
                Return swDocumentTypes_e.swDocASSEMBLY
            Case ".slddrw"
                Return swDocumentTypes_e.swDocDRAWING
            Case Else
                Throw New ArgumentException("Tipo de documento desconhecido.")
        End Select
    End Function

    Public Sub CriarColunasDataGridView(ByVal dgv As DataGridView, ByVal colunas As String())
        ' Limpa todas as colunas existentes no DataGridView
        ' dgv.Columns.Clear()

        ' Cria a primeira coluna como do tipo imagem
        Dim colunaImagem As New DataGridViewImageColumn()
        colunaImagem.HeaderText = "tipo"
        colunaImagem.Name = "dgvListaBomTipoArquivo"
        colunaImagem.ImageLayout = DataGridViewImageCellLayout.Zoom
        colunaImagem.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
        colunaImagem.Frozen = True

        ' Adiciona a coluna de imagem ao DataGridView
        dgv.Columns.Add(colunaImagem)

        ' Cria a primeira coluna como do tipo imagem
        Dim colunaImagemAtencao As New DataGridViewImageColumn()
        colunaImagemAtencao.HeaderText = "RNC"
        colunaImagemAtencao.Name = "dgvRNC"
        colunaImagemAtencao.ImageLayout = DataGridViewImageCellLayout.Zoom
        colunaImagemAtencao.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader

        ' Adiciona a coluna de imagem ao DataGridView
        dgv.Columns.Add(colunaImagemAtencao)

        ' Adiciona as colunas de texto restantes ao DataGridView
        For Each coluna As String In colunas
            ' Cria uma nova coluna
            Dim novaColuna As New DataGridViewTextBoxColumn()

            ' Define o nome da coluna
            novaColuna.HeaderText = coluna
            novaColuna.Name = coluna
            novaColuna.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader

            ' Adiciona a coluna ao DataGridView
            dgv.Columns.Add(novaColuna)
        Next

        dgv.Columns("CodMatFabricante").Frozen = True

        dgv.Columns("IdMaterial").Visible = False ''edson 19/01/2025

        dgv.Columns("CodMatFabricante").Visible = True
        dgv.Columns("DescResumo").Visible = True
        dgv.Columns("DescDetal").Visible = True
        dgv.Columns("Autor").Visible = True
        dgv.Columns("Palavrachave").Visible = False
        dgv.Columns("Notas").Visible = False
        dgv.Columns("Espessura").Visible = True
        dgv.Columns("Altura").Visible = True
        dgv.Columns("Largura").Visible = True
        dgv.Columns("material").Visible = True
        dgv.Columns("AreaPintura").Visible = True
        dgv.Columns("NumeroDobras").Visible = False
        dgv.Columns("Peso").Visible = True
        dgv.Columns("EnderecoArquivo").Visible = False
        dgv.Columns("Acabamento").Visible = True
        dgv.Columns("txtSoldagem").Visible = False
        dgv.Columns("txttipodesenho").Visible = False
        dgv.Columns("txtCorte").Visible = True
        dgv.Columns("txtDobra").Visible = True
        dgv.Columns("txtSolda").Visible = True
        dgv.Columns("txtPintura").Visible = True
        dgv.Columns("txtMontagem").Visible = True
        '  dgv.Columns("RNC").Visible = False
        ' dgv.Columns("ComprimentoClimitadora").Visible = False
        ' dgv.Columns("Larguracaixadelimitadora").Visible = False
        ' dgv.Columns("Espessuracaixadelimitadora").Visible = False
        dgv.Columns("txtItemEstoque").Visible = False
        dgv.Columns("qtde").Visible = True
        dgv.Columns("Bloqueado").Visible = False

    End Sub

    ' Oculta uma TabPage
    Public Sub OcultarTabPage(tabControl As TabControl, tabPage As TabPage)
        If tabControl.TabPages.Contains(tabPage) Then
            tabControl.TabPages.Remove(tabPage)
        End If
    End Sub

    ' Exibe uma TabPage
    Public Sub ExibirTabPage(tabControl As TabControl, tabPage As TabPage)
        If Not tabControl.TabPages.Contains(tabPage) Then
            tabControl.TabPages.Add(tabPage)
        End If
    End Sub

    ' Private WithEvents bgWorker As New BackgroundWorker()

    Private CorOriginal As Color
    Private TextoOriginal As String
    Private ImagemOriginal As System.Drawing.Image

    Private Sub Painel_Leitura_Dados_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim dtSetor As System.Data.DataTable

        'dtSetor = cl_BancoDados.CarregarDados("Select ProcessoFabricacao from " & ComplementoTipoBanco & "processofabricacao WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') ORDER BY ProcessoFabricacao ")

        'Dim colunaSetor As String = ""

        'For i As Integer = 0 To dtSetor.Rows.Count - 1

        '    colunaSetor += ",txt" & Replace(dtSetor.Rows(i)("ProcessoFabricacao").ToString, " ", "")

        'Next

        If My.Settings.AtualizaCadastroComLeituraBOM = "SIM" Then

            tspOpcaoSalvamentoAUTOMADICO.Image = My.Resources.marcado
        Else

            tspOpcaoSalvamentoAUTOMADICO.Image = My.Resources.desmarcado

        End If

        'Cria as Colunas para ler a lista de material
        colunas = {"IdMaterial",
                     "CodMatFabricante",
                 "DescResumo",
                 "DescDetal",
                 "Autor",'
                 "Palavrachave",
                 "Notas",
                 "Espessura",
                 "Altura",
                 "Largura",
                 "material",
                 "AreaPintura",
                 "NumeroDobras",
                 "Peso",
                 "EnderecoArquivo",
                 "Acabamento",
                 "txtSoldagem",
                 "txttipodesenho",
                 "txtCorte",
                "txtDobra",
                "txtSolda",
                "txtPintura",
                "txtMontagem",
               "RNC",
"Comprimentocaixalimitadora",
"Larguracaixadelimitadora",
"Espessuracaixadelimitadora",
"txtItemEstoque",
"qtde",
"Bloqueado",
"Unidade"}

        ' Chama a função para criar as colunas
        CriarColunasDataGridView(dgvDataGridBOM, colunas)

        Timerdgvos.Enabled = True

        AtualziarDadosSinco()

        CorOriginal = btnPendencias.BackColor
        TextoOriginal = btnPendencias.Text
        ImagemOriginal = btnPendencias.Image

        If My.Settings.BancoDadosAtivo = "mettapaineis" Then

            BuscarListaDePeçasAvulçasToolStripMenuItem.Enabled = True

        ElseIf My.Settings.BancoDadosAtivo = "alfatec2" Then

            BuscarListaDeMaterialNoFAPToolStripMenuItem.Enabled = True

        ElseIf My.Settings.BancoDadosAtivo = "lynxlocal" Then

            BuscarListaDePeçasAvulçasToolStripMenuItem.Enabled = True
            BuscarListaDeMaterialNoFAPToolStripMenuItem.Enabled = True

        End If


        cl_BancoDados.FormatarDataGridView(DGVListaMaterialSW, "SIM")
        ' Sua rotina de formatação global (se já existir)
        cl_BancoDados.FormatarDataGridView(dgvos, "SIM")

    End Sub

    Private Sub ReorganizarCheckboxes()

        If vemdalista = False Then

            Dim topAtual As Integer = 20 ' posição inicial no topo

            Dim checkboxes() As System.Windows.Forms.CheckBox = {chkTrocarFormato, choBloqueaArquivoExistente, chkConverterPDF, chkConverterDXF}

            For Each chk In checkboxes
                chk.Top = topAtual
                chk.Left = 20
                topAtual += chk.Height + 5 ' espaço entre eles
            Next

        End If

    End Sub

    Public Sub AplicarTema()
        ' Obtém a cor de fundo do sistema
        Dim corFundo As Color = SystemColors.Control
        Dim corTexto As Color = SystemColors.ControlText

        ' Verifica se o fundo é mais escuro que um certo limiar (tema escuro)
        If corFundo.GetBrightness() < 0.5 Then
            ' Definir cores para tema escuro
            corFundo = Color.FromArgb(45, 45, 48) ' Fundo escuro padrão
            corTexto = Color.White
        Else
            ' Definir cores para tema claro
            corFundo = Color.White
            corTexto = Color.Black
        End If

        ' Aplica o fundo no próprio painel
        Me.BackColor = corFundo
        Me.ForeColor = corTexto

        ' Chama a função para atualizar todos os controles dentro do painel
        AtualizarCoresDosControles(Me, corFundo, corTexto)
    End Sub

    Private Sub AtualizarCoresDosControles(ctrl As Control, corFundo As Color, corTexto As Color)
        ' Aplica a cor para o próprio controle
        ctrl.BackColor = corFundo
        ctrl.ForeColor = corTexto

        ' Percorre todos os controles dentro do painel e aplica as cores
        For Each subCtrl As Control In ctrl.Controls
            AtualizarCoresDosControles(subCtrl, corFundo, corTexto)
        Next
    End Sub

    Private viewDadosmaterial As DataView

    Public Sub LimparTelaVariaveis()

        ' Limpar campos do formulário
        LimparCamposFormulario()

        ' Limpar propriedades do objeto DadosArquivoCorrente

        LimparDadosArquivoCorrente()

        ' Limpar campos relacionados à lista de corte
        LimparCamposListaCorte()

        ' Limpar campos relacionados ao processo
        LimparCamposProcesso()

        ' Limpar campos da caixa delimitadora
        LimparCamposCaixaDelimitadora()

    End Sub

    ''' <summary>
    ''' Limpa os campos do formulário.
    ''' </summary>
    Private Sub LimparCamposFormulario()
        'Me.cboTitulo.Text = ""
        Me.txtTitulo.Clear()
        Me.txtAssuntoSubiTitulo.Clear()
        Me.txtComentarios.Clear()
        Me.txtAuthor.Clear()
        Me.txtPalavraChave.Clear()

        Me.btnPendencias.Image = Nothing
        'Me.txtDescricaoPendencia.Clear()

        optProcessoSoldagemSim.Checked = False
        optProcessoSoldagemNao.Checked = False

        OPTEstoqueSim.Checked = False
        OPTEstoqueNao.Checked = False

        'chkCorte.Checked = False
        'chkDobra.Checked = False
        'chkSolda.Checked = False
        'chkPintura.Checked = False
        'chkMontagem.Checked = False

        'cboTipoDesenho.Text = ""
        chkVerificarPDF.Checked = False
        chkVerificarDXF.Checked = False
        chkVerificarDFT.Checked = False
        chkVerificarLXDS.Checked = False

        ' cboAcabamento.Text = ""
    End Sub

    ''' <summary>
    ''' Limpa os campos relacionados à lista de corte.
    ''' </summary>
    Private Sub LimparCamposListaCorte()
        Me.lblEspessura.Text = ""
        Me.lblLargura.Text = ""
        Me.lblComprimento.Text = ""
        Me.lblNumeroDobra.Text = ""
        Me.lblPeso.Text = ""
        Me.lblMaterial.Text = ""
        Me.lblAreaPintura.Text = ""
    End Sub

    ''' <summary>
    ''' Limpa os campos relacionados à caixa delimitadora.
    ''' </summary>
    Private Sub LimparCamposCaixaDelimitadora()
        Me.lblAlturaTotalCaixaDelimitadora.Text = ""
        Me.lblProfundidadeTotalCaixaDelimitadora.Text = ""
        Me.lblProfundidadeTotalCaixaDelimitadora.Text = ""
    End Sub

    ''' <summary>
    ''' Limpa os campos relacionados ao processo.
    ''' </summary>
    Private Sub LimparCamposProcesso()
        Me.lblPeso.Text = ""
        Me.lblMaterial.Text = ""
    End Sub

    ''' <summary>
    ''' Limpa as propriedades do objeto DadosArquivoCorrente.
    ''' </summary>
    Private Sub LimparDadosArquivoCorrente()
        With DadosArquivoCorrente
            ' Informações gerais
            .IdMaterial = Nothing
            .NomeArquivoComExtensao = Nothing
            .NomeArquivoSemExtensao = Nothing
            .EnderecoArquivo = Nothing
            .EnderecoArquivoAterior = Nothing
            .Extencao = Nothing
            .DataCriacaDesenho = Nothing
            .DataUltimoSalvamento = Nothing
            .SalvoUltimaVezPor = Nothing
            .Titulo = Nothing
            .AssuntoSubiTitulo = Nothing
            .Comentarios = Nothing
            .Author = Nothing
            .PalavraChave = Nothing

            ' Processo
            .soldagem = Nothing
            .Acabamento = Nothing
            .TipoDesenho = Nothing
            .Corte = Nothing
            .Dobra = Nothing
            .Solda = Nothing
            .Pintura = Nothing
            .Montagem = Nothing
            .ItemEstoque = Nothing
            .rnc = Nothing
            .qtde = Nothing

            ' Arquivos associados
            .ArquivoPdf = Nothing
            .ArquivoDxf = Nothing
            .ArquivoDft = Nothing
            .ArquivoLXDS = Nothing

            ' Caixa delimitadora
            .Profundidadeaixadelimitadora = Nothing
            .Larguracaixadelimitadora = Nothing
            .Alturacaixadelimitadora = Nothing

            ' Lista de corte
            .ComprimentoBlank = Nothing
            .LarguraBlank = Nothing
            .Espessura = Nothing
            .PerimetroCorteExterno = Nothing
            .PerimetroCorteInterno = Nothing
            .NumeroDobras = Nothing
            .Massa = Nothing
            .material = Nothing
            .AreaPintura = Nothing
        End With
    End Sub

    Public Sub PreencherCheckedListBox(ByVal query As String, ByVal clb As CheckedListBox)
        ' Limpa o CheckedListBox antes de preencher
        clb.Items.Clear()

        ' Chama a função CarregarDados para obter os dados
        Dim dt As System.Data.DataTable = cl_BancoDados.CarregarDados(query)

        Try

            ' Verifica se há dados retornados
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ' Itera sobre as linhas do DataTable e adiciona ao CheckedListBox
                For Each row As DataRow In dt.Rows
                    ' Supondo que a primeira coluna do DataTable seja o que você quer exibir
                    clb.Items.Add(row(0).ToString())
                Next
                ' Else
                ' MessageBox.Show("Nenhum dado encontrado.")
            End If
        Catch ex As Exception
        Finally
        End Try
    End Sub

    '    Private Sub TimerdgvDesenhos_Tick(sender As Object, e As EventArgs) Handles TimerdgvDesenhos.Tick

    '        Try
    '            dgvDesenhos.DataSource = cl_BancoDados.CarregarDados("Select IdMaterial, Sobra_Fabrica,
    '			   CodMatFabricante,
    '			   DescResumo,
    '			   DescDetal,
    '			   Autor,
    '			   Palavrachave,
    '			   Notas,
    '			   Espessura,
    '			   MaterialSW,
    '			   Altura,
    '			   Largura,
    '			   AreaPintura,
    '			   NumeroDobras,
    '			   Peso,
    '				UPPER(RTrim(Replace(EnderecoArquivo, '##', '\\'))) AS EnderecoArquivo,
    '			   Acabamento,
    '			   txtSoldagem,
    '			   txtTipoDesenho,
    '			   txtCorte,
    '			   txtDobra,
    '			   txtSolda,
    '			   txtPintura,
    '			   txtMontagem,
    '			   RNC,
    'Comprimentocaixadelimitadora,
    'Larguracaixadelimitadora,
    'Espessuracaixadelimitadora,
    'txtItemEstoque
    'from  " & ComplementoTipoBanco & "material where (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '')
    'AND (DescResumo LIKE '%" & Me.TxtPesgTitulo.Text & "%')
    'and (CodMatFabricante LIKE '%" & Me.TxtPesgNomeDesenho.Text & "%')
    'and (DescDetal LIKE '%" & Me.TxtPesqSubtitulo.Text & "%')
    'and (DescDetal LIKE '%" & Me.TxtPesqSubtitulo2.Text & "%')
    'and (DescDetal LIKE '%" & Me.TxtPesqSubtitulo3.Text & "%')
    'and   (EnderecoArquivo <> '' AND StatusMat = 'A')
    '	   ORDER BY
    '	CASE
    '		WHEN RNC IS NOT NULL AND RNC <> '' THEN 0
    '		ELSE 1
    '	END,
    '	CodMatFabricante,
    '	DescResumo limit 100;")
    '            Try

    '                dgvDesenhos.Columns("IdMaterial").Visible = False
    '                dgvDesenhos.Columns("CodMatFabricante").Frozen = True
    '                dgvDesenhos.Columns("EnderecoArquivo").Visible = False
    '                dgvDesenhos.Columns("Palavrachave").Visible = False
    '                dgvDesenhos.Columns("Notas").Visible = False
    '                dgvDesenhos.Columns("RNC").Visible = False

    '            Catch ex As Exception
    '            Finally
    '            End Try

    '            For Each col As DataGridViewColumn In dgvDesenhos.Columns
    '                If col.Width > 350 Then
    '                    col.Width = 351
    '                End If
    '            Next

    '            '  cl_BancoDados.ProcessarArquivosDGV(dgvDesenhos)

    '            TimerdgvDesenhos.Enabled = False

    '            cl_BancoDados.FormatarDataGridView(dgvDesenhos, "SIM")

    '        Catch ex As Exception
    '        Finally
    '        End Try

    '    End Sub

    '    Private Sub TxtPesgNomeDesenho_TextChanged(sender As Object, e As EventArgs)
    '        TimerdgvDesenhos.Enabled = True
    '    End Sub

    '    Private Sub TxtPesgTitulo_TextChanged(sender As Object, e As EventArgs)
    '        TimerdgvDesenhos.Enabled = True
    '    End Sub

    '    Private Sub TxtPesqSubtitulo_TextChanged(sender As Object, e As EventArgs)
    '        TimerdgvDesenhos.Enabled = True
    '    End Sub

    '    Private Sub TxtPesqSubtitulo2_TextChanged(sender As Object, e As EventArgs)
    '        TimerdgvDesenhos.Enabled = True
    '    End Sub

    '    Private Sub TxtPesqSubtitulo3_TextChanged(sender As Object, e As EventArgs)
    '        TimerdgvDesenhos.Enabled = True
    '    End Sub

    'Private Sub dgvDesenhos_DoubleClick(sender As Object, e As EventArgs)
    '    OpenDocumentAndWait(dgvDesenhos.CurrentRow.Cells("EnderecoArquivo").Value.ToString, True, swModel)
    'End Sub

    Private Sub AbrirPDFDaLinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirPDFDaLinhaSelecionadaToolStripMenuItem.Click

        Try

            Dim ArquivoPdf As String = DGVListaMaterialSW.CurrentRow.Cells("EnderecoArquivoItemOrdemServico").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoPdf = Path.ChangeExtension(ArquivoPdf, ".PDF")

            ' Obtém o caminho completo
            ArquivoPdf = Path.GetFullPath(ArquivoPdf)

            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoPdf) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoPdf)

                    p.Start()
                    'p.WaitForExit()

                    DGVListaMaterialSW.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub AbrirDXFDaLinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirDXFDaLinhaSelecionadaToolStripMenuItem.Click
        Try

            Dim ArquivoDXF As String = DGVListaMaterialSW.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoDXF = Path.ChangeExtension(ArquivoDXF, ".DXF")

            ' Obtém o caminho completo
            ArquivoDXF = Path.GetFullPath(ArquivoDXF)
            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoDXF) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoDXF)

                    p.Start()
                    p.WaitForExit()

                    DGVListaMaterialSW.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub btnLimparBom_Click(sender As Object, e As EventArgs)

        dgvDataGridBOM.DataSource = Nothing
        dgvDataGridBOM.Rows.Clear()
        dgvDataGridBOM.Refresh()

    End Sub

    Private Sub dgvDataGrid_DoubleClick(sender As Object, e As EventArgs) Handles dgvDataGridBOM.DoubleClick

        Dim ArquivoListaBom As String = dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString

        ' Obtém o caminho completo
        ArquivoListaBom = Path.GetFullPath(ArquivoListaBom)

        ' Verifica se o arquivo existe e o abre
        If File.Exists(ArquivoListaBom) Then
            Process.Start(ArquivoListaBom)

        End If

        dgvDataGridBOM.CurrentRow.DefaultCellStyle.BackColor = Color.BlueViolet

    End Sub

    Private Sub txtPalavraChave_LostFocus(sender As Object, e As EventArgs) Handles txtPalavraChave.LostFocus

    End Sub

    Private Sub txtComentarios_LostFocus(sender As Object, e As EventArgs) Handles txtComentarios.LostFocus

        Try

            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then

                swModel.SummaryInfo(swSummInfoField_e.swSumInfoComment) = Me.txtComentarios.Text

                swModel.SaveSilent()

            End If
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub txtAssuntoSubiTitulo_LostFocus(sender As Object, e As EventArgs) Handles txtAssuntoSubiTitulo.LostFocus

    End Sub

    Private Sub btnpdf_Click(sender As Object, e As EventArgs)

        Try

            ' Verifique se o modelo foi aberto com sucesso
            If Not swModel Is Nothing Then

                ' Usa Select Case para diferenciar o tipo do documento
                If swModel.GetType() = swDocumentTypes_e.swDocDRAWING Then

                    Try

                        IntanciaSolidWorks.ConectarSolidWorks()
                        'swApparq = CreateObject("SldWorks.Application")

                        swModel = swapp.ActiveDoc
                        'swModel = swApparq.ActiveDoc

                        swModel.Visible = True

                        swModelDocExt = swModel.Extension

                        DadosArquivoCorrente.ExportToPDF(swModel, swModel.GetPathName.ToString, True)

                        chkVerificarPDF.Checked = True
                    Catch ex As Exception

                        MsgBox(ex.Message & " o arquivo não e valido para conversão em PDF")
                    Finally

                    End Try
                End If
            End If
        Catch ex As Exception
        Finally
        End Try

        ' Dim pdfFilePath As String = Path.ChangeExtension(swModel.GetPathName.ToString, ".pdf")

        'pdfsinco.EscreverPdf(pdfFilePath, "C:\bin", "testo no pdf")

    End Sub

    Private Sub btnAtualizaDados_Click(sender As Object, e As EventArgs)

        If dgvDataGridBOM.Rows.Count > 0 Then

            For i As Integer = 0 To dgvDataGridBOM.Rows.Count

                If File.Exists(dgvDataGridBOM.Rows(i).Cells("EnderecoArquivo").Value) Then

                    dgvDataGridBOM.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen

                End If

            Next

        End If

    End Sub

    'Private Sub Abrir3DDaLinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs)

    '    Dim ArquivoSW As String = dgvDesenhos.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

    '    ' Obtém o caminho completo
    '    ArquivoSW = Path.GetFullPath(ArquivoSW)

    '    ' Verifica se o arquivo existe e o abre
    '    If File.Exists(ArquivoSW) Then
    '        Using p As New Diagnostics.Process
    '            p.StartInfo = New ProcessStartInfo(ArquivoSW)

    '            p.Start()
    '            p.WaitForExit()

    '            dgvDesenhos.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
    '        End Using
    '    End If

    'End Sub

    Private Sub dgvDesenhos_DataError(sender As Object, e As DataGridViewDataErrorEventArgs)
        Try
        Catch ex As Exception
        Finally

        End Try
    End Sub

    Private Sub ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem.Click

        If OrdemServico.Liberado_Engenharia <> "" Then

            MsgBox("Ordem de Serviço já Liberada para Produção, não pode mais ser modificada!", vbCritical, "Atenção")
            Exit Sub
        Else

            If MsgBox("Tem certeza que deseja desabilitar o Desenho Selecionado?", MsgBoxStyle.YesNo, "Confirmar desabilitação!") = MsgBoxResult.No Then

                Exit Sub
            Else

                ' Atualiza o status no banco de dados
                cl_BancoDados.Salvar("UPDATE ordemservicoitem SET D_E_L_E_T_E = '*' WHERE CodMatFabricante = '" & DGVListaMaterialSW.CurrentRow.Cells("CodMatFabricante").Value.ToString() & "'")

                ' Remove a linha corrente do DataGridView
                If DGVListaMaterialSW.CurrentRow IsNot Nothing Then
                    DGVListaMaterialSW.Rows.Remove(DGVListaMaterialSW.CurrentRow)
                End If
                ' MsgBox("Desenho desabilitado e removido da lista.")
            End If

        End If

    End Sub

    '    Private Sub TimerMontaPeca_Tick(sender As Object, e As EventArgs) Handles TimerMontaPeca.Tick

    '        DGVMontaPeca.DataSource = cl_BancoDados.CarregarDados("Select IdMontaPeca, NomeArquivoSemExtensao,
    'IdMaterial,NomeArquivoSemExtensao, CodMatFabricante,
    'DescDetal, DescFamilia, CodigoJuridicoMat, IdMaterial,
    'Peso, Unidade, D_E_L_E_T_E, Valor, PecaQtde, Total,
    'txtItemEstoque, DataCriacao, UsuarioCriacao
    'From  " & ComplementoTipoBanco & " viewmontapeca1
    'Where NomeArquivoSemExtensao = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'
    'And (D_E_L_E_T_E IS NOT NULL or D_E_L_E_T_E is null)
    'order by DescDetal")

    '        ' Verifique se o controle DGVMontaPeca foi criado antes de acessar suas colunas
    '        If DGVMontaPeca.Columns.Count > 0 Then

    '            DGVMontaPeca.Columns("DescDetal").HeaderText = "Descricao"
    '            DGVMontaPeca.Columns("CodMatFabricante").HeaderText = "Cod.Fabr."
    '            DGVMontaPeca.Columns("CodigoJuridicoMat").HeaderText = "Fabricante"

    '            DGVMontaPeca.Columns("IdMontaPeca").Visible = False
    '            DGVMontaPeca.Columns("NomeArquivoSemExtensao").Visible = False
    '            DGVMontaPeca.Columns("IdMaterial").Visible = False
    '            'DGVMontaPeca.Columns("IdMaterial").Visible = False
    '            'DGVMontaPeca.Columns("DescDetal").Visible = False
    '            DGVMontaPeca.Columns("D_E_L_E_T_E").Visible = False

    '            DGVMontaPeca.Columns("DescFamilia").Visible = False
    '            DGVMontaPeca.Columns("CodigoJuridicoMat").Visible = False
    '            DGVMontaPeca.Columns("IdMaterial").Visible = False
    '            DGVMontaPeca.Columns("txtItemEstoque").Visible = False
    '            DGVMontaPeca.Columns("DataCriacao").Visible = False
    '            DGVMontaPeca.Columns("UsuarioCriacao").Visible = False

    '        End If

    '        'edson 20-01-2025
    '        'para verificar a necessidade processamento na abertura de cada arquivo
    '        '''For Each col As DataGridViewColumn In DGVMontaPeca.Columns
    '        '''    If col.Width > 400 Then
    '        '''        col.Width = 401
    '        '''    End If
    '        '''Next
    '        TimerMontaPeca.Enabled = False

    '        cl_BancoDados.FormatarDataGridView(DGVMontaPeca, "SIM")

    '    End Sub

    Private Sub cboProjeto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProjeto.SelectedIndexChanged

        Try

            'OrdemServico.Projeto = cboProjeto.Text
            'OrdemServico.idProjeto = Convert.ToInt32(cboProjeto.SelectedValue)

            ' Verificar se o combo box contém algum valor selecionado
            If cboProjeto.SelectedItem Is Nothing Then
                Throw New Exception("Nenhum Projeto selecionado. Por favor, selecione um Projeto válido.")
            End If

            ' Garantir que o texto do combo box não está vazio ou nulo
            If String.IsNullOrEmpty(cboProjeto.Text) Then
                Throw New Exception("O nome do Projeto não pode estar vazio. Selecione um Projeto válido.")
            End If

            ' Atribuir valores ao objeto OrdemServico
            OrdemServico.Projeto = cboProjeto.Text
            OrdemServico.idProjeto = cboProjeto.SelectedValue

            Try

                ' Tentar converter o valor selecionado para inteiro
                ' Dim idProjeto As Integer
                If Integer.TryParse(cboProjeto.SelectedValue?.ToString(), Convert.ToInt32(OrdemServico.idProjeto)) Then

                    If My.Settings.TipoConexao = "MYSQL" Then

                        '	OrdemServico.idProjeto = OrdemServico.idProjeto

                        ' Preenchendo o ComboBox com os dados do banco
                        cl_BancoDados.ComboBoxDataSet("tags", "idTag", "Tag", cboTag, " where (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '')   and (Finalizado = '' OR Finalizado Is NULL) AND idProjeto = '" & OrdemServico.idProjeto & "'")

                        ' Tentativa de retornar o nome da empresa
                        cl_BancoDados.RetornaCampoDaPesquisa("SELECT DescEmpresa,idempresa FROM  " & ComplementoTipoBanco & "projetos where idProjeto  = " & OrdemServico.idProjeto, "DescEmpresa", "idempresa")
                        txtCliente.Text = VCampo0

                        OrdemServico.DescEmpresa = VCampo0

                        If VCampo1 = "" Then

                            VCampo1 = 0

                        End If

                        OrdemServico.idempresa = VCampo1

                    ElseIf My.Settings.TipoConexao = "SQL" Then

                        'codificação codigo protheus
                        OrdemServico.idProjeto = cl_BancoDados.FormatarPara6Caracteres(OrdemServico.idProjeto)

                        ' Preenchendo o ComboBox com os dados do banco
                        cl_BancoDados.ComboBoxDataSet("View_SZ2010_GESTAO01", "Z2_PRODUTO", "Z2_DESC", cboTag, " where Z2_NUM = '" & OrdemServico.idProjeto & "'", "[MP12OFICIAL].[dbo].")

                        ' Tentativa de retornar o nome da empresa
                        cl_BancoDados.RetornaCampoDaPesquisa("SELECT Z1_DESC FROM  " & "[MP12OFICIAL].[dbo].[View_SZ1010_GESTAO] where Z1_NUM  = " & OrdemServico.idProjeto, "Z1_DESC")
                        txtCliente.Text = VCampo0

                    End If

                End If

                OrdemServico.Tag = cboTag.Text

                OrdemServico.DescEmpresa = txtCliente.Text
            Catch ex As Exception
                Me.txtCliente.Clear()
                OrdemServico.Projeto = Nothing
                OrdemServico.Tag = Nothing
                OrdemServico.DescEmpresa = Nothing

            End Try
        Catch ex As Exception
            ' MsgBox(ex.Message)
        Finally
        End Try

    End Sub

    Private Sub cboTag_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTag.SelectedIndexChanged

        Try

            'OrdemServico.Tag = cboTag.Text
            'OrdemServico.idTag = cboTag.SelectedValue

            ' Verificar se o combo box contém algum valor selecionado
            If cboTag.SelectedItem Is Nothing Then
                Throw New Exception("Nenhuma Tag selecionada. Por favor, selecione uma Tag válida.")
            End If

            ' Garantir que o texto do combo box não está vazio ou nulo
            If String.IsNullOrEmpty(cboTag.Text) Then
                Throw New Exception("O nome da Tag não pode estar vazio. Selecione uma Tag válida.")
            End If

            ' Atribuir o texto da Tag ao objeto OrdemServico
            OrdemServico.Tag = cboTag.Text
            OrdemServico.idTag = cboTag.SelectedValue

            ' Tentar converter o valor selecionado para inteiro
            ' Dim idTag As Integer
            If Integer.TryParse(cboTag.SelectedValue?.ToString(), Convert.ToInt32(OrdemServico.idTag)) Then

                OrdemServico.DescTag = cboTag.Text

                ' OrdemServico.idTag = cl_BancoDados.FormatarPara7Caracteres(OrdemServico.idTag)

                '	OrdemServico.idTag = OrdemServico.idTag

                '    OrdemServico.idTag = idTag
                ' Else
                '   Throw New Exception("O ID da Tag selecionada não é válido. Por favor, verifique.")
            End If

            If My.Settings.TipoConexao = "SQL" Then

                ' Tenta retornar a descrição da Tag
                cl_BancoDados.RetornaCampoDaPesquisa("SELECT DISTINCT(Z2_DESC) FROM [MP12OFICIAL].[dbo].[View_SZ2010_GESTAO01] where Z2_PRODUTO = '" & OrdemServico.idTag & "'", "Z2_DESC")
                txtDescricaoTag.Text = VCampo0
                ' Tenta retornar a descrição da Tag
                cl_BancoDados.RetornaCampoDaPesquisa("SELECT DISTINCT(QtdeTag) FROM [MP12OFICIAL].[dbo].[View_SZ2010_GESTAO] where Z2_PRODUTO = '" & OrdemServico.idTag & "'", "QtdeTag")
                lbltxtQtdeTag.Text = VCampo0

                Try
                    cl_BancoDados.RetornaCampoDaPesquisa("SELECT DataPrevisao FROM [MP12OFICIAL].[dbo].[View_SZ2010_GESTAO] where Z2_PRODUTO = '" & OrdemServico.idTag & "'", "DataPrevisao")
                    OrdemServico.DataPrevisao = VCampo0
                Catch ex As Exception

                    OrdemServico.DataPrevisao = ""

                End Try

            ElseIf My.Settings.TipoConexao = "MYSQL" Then

                Me.lbltxtQtdeLiberada.Text = ""
                Me.lbltxtQtdeTag.Text = ""
                Me.lbltxtSaldoTag.Text = ""
                txtDescricaoTag.Clear()

                ' Tenta retornar a descrição da Tag
                'txtDescricaoTag.Text = cl_BancoDados.RetornaCampoDaPesquisa("SELECT DescTag FROM  " & ComplementoTipoBanco & "tags where idTag = '" & OrdemServico.idTag & "'", "DescTag")
                'lbltxtQtdeTag.Text = cl_BancoDados.RetornaCampoDaPesquisa("SELECT QtdeTag FROM  " & ComplementoTipoBanco & "tags where idTag = '" & OrdemServico.idTag & "'", "QtdeTag")
                'lbltxtQtdeLiberada.Text = cl_BancoDados.RetornaCampoDaPesquisa("SELECT QtdeLiberada FROM  " & ComplementoTipoBanco & "tags where idTag = '" & OrdemServico.idTag & "'", "QtdeLiberada")
                'lbltxtSaldoTag.Text = cl_BancoDados.RetornaCampoDaPesquisa("SELECT SaldoTag FROM  " & ComplementoTipoBanco & "tags where idTag = '" & OrdemServico.idTag & "'", "SaldoTag")

                cl_BancoDados.VerificaSaldoTag(OrdemServico.idTag)

                txtDescricaoTag.Text = OrdemServico.Descricao
                Me.lbltxtQtdeTag.Text = OrdemServico.QtdeTag
                Me.lbltxtQtdeLiberada.Text = OrdemServico.QtdeLiberada
                Me.lbltxtSaldoTag.Text = OrdemServico.SaldoTag

                ' OrdemServico.DataPrevisao = cl_BancoDados.RetornaCampoDaPesquisa("SELECT DataPrevisao FROM  " & ComplementoTipoBanco & "tags where idTag = '" & OrdemServico.idTag & "'", "DataPrevisao")

                'Try
                '    OrdemServico.QtdeTag = lbltxtQtdeTag.Text

                'Catch ex As Exception
                '    OrdemServico.QtdeTag = 0
                'End Try

                'Try
                '    OrdemServico.QtdeLiberada = lbltxtQtdeLiberada.Text
                'Catch ex As Exception
                '    OrdemServico.QtdeLiberada = 0
                'End Try

                'Try
                '    OrdemServico.SaldoTag = lbltxtSaldoTag.Text
                'Catch ex As Exception
                '    OrdemServico.SaldoTag = 0
                'End Try

            End If

            ' My.Settings.TipoConexao = "SQL"
        Catch ex As Exception
            ' Em caso de erro, limpa o campo de descrição
            Me.txtDescricaoTag.Clear()
            lbltxtQtdeTag.Text = ""

            OrdemServico.Tag = Nothing
            OrdemServico.idTag = Nothing

            ' Opcional: Registrar o erro em um log
            ' LogError(ex) ' Função fictícia para logar erros
            ' MessageBox.Show("Erro ao carregar a descrição da Tag.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Timerdgvos_Tick(sender As Object, e As EventArgs) Handles Timerdgvos.Tick

        'CarregarDadosAgrupados()

        Dim Filtro As String

        ' Verifica se a checkbox está marcada e ajusta o filtro
        If chkMostraLiberadasPelaEngenharia.Checked = False Then
            Filtro = " AND (Liberado_Engenharia = '' OR Liberado_Engenharia IS NULL )"
        End If

        dgvos.DataSource = cl_BancoDados.CarregarDados("SELECT IdOrdemServico,
												idProjeto,
												Projeto,
                                                Fator,
												Tag,
                                                DescTag,
												idTag,
												Descricao,
												DescEmpresa,
                                                replace(EnderecoOrdemServico,'##','\\') as ENDERECO,
												CriadoPor as CriadoPor,
												DataCriacao as DataCriacao,
												Liberado_Engenharia,
												Data_Liberacao_Engenharia,
												Estatus,
												DataPrevisao,
                                                ProdutoPadrao,
                                                format(PesoTotal,2) as PesoTotal,
                                                format(AreaPinturaTotal,2) as AreaPinturaTotal,
                                                idempresa,NumeroOpOmie
												FROM  " & ComplementoTipoBanco & "ordemservico WHERE (D_E_L_E_T_E <> '*' or D_E_L_E_T_E is null)" & Filtro &
                                                " AND CriadoPor LIKE '%" & Me.txtPesqCriadoPor.Text & "%' order by IdOrdemServico desc")

        Timerdgvos.Enabled = False

        '   cl_BancoDados.FormatarDataGridView(dgvos, "SIM")

    End Sub

    Private Sub chkMostraLiberadasPelaEngenharia_CheckedChanged(sender As Object, e As EventArgs) Handles chkMostraLiberadasPelaEngenharia.CheckedChanged
        Timerdgvos.Enabled = True
    End Sub

    Private Sub dgvos_Click(sender As Object, e As EventArgs) Handles dgvos.Click

        Try
            OrdemServico.Liberado_Engenharia = dgvos.CurrentRow.Cells("Liberado_Engenharia").Value.ToString

        Catch ex As Exception
            OrdemServico.Liberado_Engenharia = ""
        End Try

        ' Se não houver linha atual, sai
        If dgvos.CurrentRow Is Nothing Then Exit Sub



        ' Carrega itens da OS (preenche OrdemServico.*)
        CarregarItemsOs()

        ' Lê idTag com segurança
        Dim vIdTag As Object = dgvos.CurrentRow.Cells("idTag").Value
        Dim idTag As Integer
        If vIdTag IsNot Nothing AndAlso Not Convert.IsDBNull(vIdTag) AndAlso Integer.TryParse(vIdTag.ToString(), idTag) Then
            OrdemServico.idTag = idTag
        Else
            OrdemServico.idTag = 0
        End If

        ' Lê IdOrdemServico com segurança
        Dim vIdOs As Object = dgvos.CurrentRow.Cells("IdOrdemServico").Value
        Dim idOs As Integer
        If vIdOs IsNot Nothing AndAlso Not Convert.IsDBNull(vIdOs) AndAlso Integer.TryParse(vIdOs.ToString(), idOs) Then
            OrdemServico.IdOrdemServico = idOs
        Else
            OrdemServico.IdOrdemServico = 0
        End If

        Dim vIdDescEmpresa As Object = dgvos.CurrentRow.Cells("descempresa").Value.ToString
        OrdemServico.DescEmpresa = vIdDescEmpresa

        Dim vIdDescTag As Object = dgvos.CurrentRow.Cells("DescTag").Value.ToString
        OrdemServico.DescTag = vIdDescTag


        OrdemServico.idempresa = dgvos.CurrentRow.Cells("idempresa").Value.ToString

        ' Atualiza saldos (se sua função depende do idTag já carregado)
        cl_BancoDados.VerificaSaldoTag(OrdemServico.idTag)

        Try
            ' Quantidade de Tags
            Me.lbltxtQtdeTag.Text =
            If(OrdemServico.QtdeTag = Nothing, "0", Convert.ToString(OrdemServico.QtdeTag))

            ' Quantidade Liberada
            Me.lbltxtQtdeLiberada.Text =
            If(OrdemServico.QtdeLiberada = Nothing, "0", Convert.ToString(OrdemServico.QtdeLiberada))

            ' Saldo de Tags
            Me.lbltxtSaldoTag.Text =
            If(OrdemServico.SaldoTag = Nothing, "0", Convert.ToString(OrdemServico.SaldoTag))
        Catch ex As Exception
            MessageBox.Show("Erro ao carregar dados da Ordem de Serviço: " & ex.Message,
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Refresh explícito é desnecessário na maioria dos casos; o binding já atualiza
        ' Me.lbltxtQtdeTag.Refresh()
        ' Me.lbltxtQtdeLiberada.Refresh()
        ' Me.lbltxtSaldoTag.Refresh()



    End Sub

    Private Function CarregarItemsOs()

        Cursor.Current = Cursors.WaitCursor

        Try
            Dim row As DataGridViewRow = dgvos.CurrentRow
            If row Is Nothing Then Return Nothing

            ' Garante que a célula-chave existe e não é nula/DBNull
            Dim vIdOs As Object = row.Cells("IdOrdemServico").Value
            If vIdOs Is Nothing OrElse Convert.IsDBNull(vIdOs) Then Return Nothing

            ' Campos numéricos com TryParse
            Dim tmpInt As Integer

            ' Liberado_Engenharia (string)
            OrdemServico.Liberado_Engenharia =
            If(Convert.IsDBNull(row.Cells("Liberado_Engenharia").Value), "",
               Convert.ToString(row.Cells("Liberado_Engenharia").Value))

            ' IdOrdemServico
            If Integer.TryParse(Convert.ToString(vIdOs), tmpInt) Then
                OrdemServico.IdOrdemServico = tmpInt
            Else
                OrdemServico.IdOrdemServico = 0
            End If

            ' idProjeto
            Dim vIdProj As Object = row.Cells("idProjeto").Value
            If vIdProj IsNot Nothing AndAlso Not Convert.IsDBNull(vIdProj) AndAlso Integer.TryParse(vIdProj.ToString(), tmpInt) Then
                OrdemServico.idProjeto = tmpInt
            Else
                OrdemServico.idProjeto = 0
            End If

            ' Projeto / Tag / Estatus / ENDERECO / DataPrevisao / Descricao / DescEmpresa
            OrdemServico.Projeto =
            If(Convert.IsDBNull(row.Cells("Projeto").Value), "", Convert.ToString(row.Cells("Projeto").Value))

            OrdemServico.Tag =
            If(Convert.IsDBNull(row.Cells("Tag").Value), "", Convert.ToString(row.Cells("Tag").Value))

            OrdemServico.Estatus =
            If(Convert.IsDBNull(row.Cells("Estatus").Value), "", Convert.ToString(row.Cells("Estatus").Value))

            OrdemServico.EnderecoOrdemServico =
            If(Convert.IsDBNull(row.Cells("ENDERECO").Value), "", Convert.ToString(row.Cells("ENDERECO").Value))

            ' DataPrevisao pode vir como Date/DBNull/String — normaliza para string
            If Convert.IsDBNull(row.Cells("DataPrevisao").Value) OrElse row.Cells("DataPrevisao").Value Is Nothing Then
                OrdemServico.DataPrevisao = ""
            Else
                Dim dt As DateTime
                Dim s As String = Convert.ToString(row.Cells("DataPrevisao").Value)
                If DateTime.TryParse(s, dt) Then
                    OrdemServico.DataPrevisao = dt.ToString("dd/MM/yyyy HH:mm")
                Else
                    ' Mantém como string se não parsear
                    OrdemServico.DataPrevisao = s
                End If
            End If

            ' Fator (numérico)
            Dim vFator As Object = row.Cells("Fator").Value
            If vFator IsNot Nothing AndAlso Not Convert.IsDBNull(vFator) AndAlso Integer.TryParse(vFator.ToString(), tmpInt) Then
                OrdemServico.Fator = tmpInt
            Else
                OrdemServico.Fator = 0
            End If

            ' Controles da UI (sem chamar ToString em DBNull)
            Me.cboProjeto.Text = OrdemServico.Projeto
            Me.txtCliente.Text =
            If(Convert.IsDBNull(row.Cells("DescEmpresa").Value), "", Convert.ToString(row.Cells("DescEmpresa").Value))
            Me.cboTag.Text = OrdemServico.Tag
            Me.txtDescricaoTag.Text = OrdemServico.Tag
            Me.txtDescricao.Text =
            If(Convert.IsDBNull(row.Cells("Descricao").Value), "", Convert.ToString(row.Cells("Descricao").Value))

            Me.lblOrdemServicoAtiva.Text =
            $"Projeto: {OrdemServico.Projeto} - Tag: {OrdemServico.Tag} - OS: {OrdemServico.IdOrdemServico}"
        Catch ex As Exception
            ' Se algo deu errado, evita deixar lixo no Id
            OrdemServico.IdOrdemServico = 0
            Debug.WriteLine("CarregarItemsOs: " & ex.Message)
        Finally
            ' Dispara a atualização da outra grid (se é isso que você precisa)
            TimerDGVListaMaterialSW.Enabled = True
            Cursor.Current = Cursors.Default
        End Try

        Return Nothing ' (mantendo assinatura como Function, sem alterar)
    End Function

    'Private Sub dgvos_Click(sender As Object, e As EventArgs) Handles dgvos.Click

    '    CarregarItemsOs()

    '    OrdemServico.idTag = Convert.ToInt32(dgvos.CurrentRow.Cells("idTag").Value)
    '    OrdemServico.IdOrdemServico = Convert.ToInt32(dgvos.CurrentRow.Cells("IdOrdemServico").Value)

    '    cl_BancoDados.VerificaSaldoTag(OrdemServico.idTag)

    '    Try
    '        ' Quantidade de Tags
    '        If OrdemServico.QtdeTag.ToString IsNot Nothing Then
    '            Me.lbltxtQtdeTag.Text = OrdemServico.QtdeTag.ToString()
    '        Else
    '            Me.lbltxtQtdeTag.Text = "0"
    '        End If

    '        ' Quantidade Liberada
    '        If OrdemServico.QtdeLiberada.ToString IsNot Nothing Then
    '            Me.lbltxtQtdeLiberada.Text = OrdemServico.QtdeLiberada.ToString()
    '        Else
    '            Me.lbltxtQtdeLiberada.Text = "0"
    '        End If

    '        ' Saldo de Tags
    '        If OrdemServico.SaldoTag.ToString IsNot Nothing Then
    '            Me.lbltxtSaldoTag.Text = OrdemServico.SaldoTag.ToString()
    '        Else
    '            Me.lbltxtSaldoTag.Text = "0"
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show("Erro ao carregar dados da Ordem de Serviço: " & ex.Message,
    '                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    '    Me.lbltxtQtdeTag.Refresh()
    '    Me.lbltxtQtdeLiberada.Refresh()
    '    Me.lbltxtSaldoTag.Refresh()

    'End Sub

    'Private Function CarregarItemsOs()

    '    Cursor.Current = Cursors.WaitCursor

    '    Try

    '        ' Verifica se há uma linha selecionada e se o valor da célula não é nulo
    '        If dgvos.CurrentRow IsNot Nothing AndAlso dgvos.CurrentRow.Cells("IdOrdemServico").Value IsNot Nothing Then

    '            OrdemServico.Liberado_Engenharia = dgvos.CurrentRow.Cells("Liberado_Engenharia").Value.ToString
    '            OrdemServico.IdOrdemServico = dgvos.CurrentRow.Cells("IdOrdemServico").Value.ToString

    '            OrdemServico.IdOrdemServico = Convert.ToInt32(dgvos.CurrentRow.Cells("IdOrdemServico").Value)
    '            OrdemServico.idProjeto = Convert.ToInt32(dgvos.CurrentRow.Cells("idProjeto").Value)
    '            '   OrdemServico.idTag = Convert.ToInt32(dgvos.CurrentRow.Cells("idTag").Value)

    '            OrdemServico.Projeto = dgvos.CurrentRow.Cells("Projeto").Value.ToString
    '            OrdemServico.Tag = dgvos.CurrentRow.Cells("Tag").Value.ToString

    '            OrdemServico.Liberado_Engenharia = dgvos.CurrentRow.Cells("Liberado_Engenharia").Value.ToString

    '            OrdemServico.Estatus = dgvos.CurrentRow.Cells("Estatus").Value.ToString

    '            OrdemServico.EnderecoOrdemServico = dgvos.CurrentRow.Cells("ENDERECO").Value.ToString

    '            OrdemServico.DataPrevisao = dgvos.CurrentRow.Cells("DataPrevisao").Value.ToString

    '            Try

    '                OrdemServico.Fator = Convert.ToInt32(dgvos.CurrentRow.Cells("Fator").Value)

    '            Catch ex As Exception
    '                ' MsgBox(ex.Message)

    '                OrdemServico.Fator = 0
    '            Finally
    '            End Try

    '            ' OrdemServico.Projeto = dgvos.CurrentRow.Cells("Projeto").Value.ToString
    '            Me.cboProjeto.Text = OrdemServico.Projeto
    '            ' OrdemServico.idProjeto = dgvos.CurrentRow.Cells("idProjeto").Value.ToString

    '            Me.txtCliente.Text = dgvos.CurrentRow.Cells("DescEmpresa").Value.ToString

    '            'OrdemServico.Tag = dgvos.CurrentRow.Cells("Tag").Value.ToString
    '            Me.cboTag.Text = OrdemServico.Tag
    '            'OrdemServico.idTag = dgvos.CurrentRow.Cells("idTag").Value.ToString
    '            Me.txtDescricaoTag.Text = OrdemServico.Tag
    '            Me.txtDescricao.Text = dgvos.CurrentRow.Cells("Descricao").Value.ToString

    '            Me.lblOrdemServicoAtiva.Text = "Projeto: " & OrdemServico.Projeto & " - Tag: " & OrdemServico.Tag & " - OS: " & OrdemServico.IdOrdemServico
    '            'Me.lblOrdemServicoAtiva.Refresh()

    '        End If
    '    Catch ex As Exception

    '        OrdemServico.IdOrdemServico = Nothing

    '    Finally

    '    End Try

    '    TimerDGVListaMaterialSW.Enabled = True

    '    Cursor.Current = Cursors.Default

    'End Function

    Private Sub TimerDGVListaMaterialSW_Tick(sender As Object, e As EventArgs) Handles TimerDGVListaMaterialSW.Tick

        Try

            DGVListaMaterialSW.DataSource = cl_BancoDados.CarregarDados("SELECT
            			IdOrdemServicoItem,
            			IdOrdemServico,
            			Projeto,
            			Tag,
            			CodMatFabricante,
            			Fator,
            			Qtde,
            			QtdeTotal,
            			DescResumo,
            			DescDetal,
            			Estatus_OrdemServico,
            			IdMaterial,
            			CriadoPor,
            			DataCriacao,
            			Estatus,
            			Acabamento,
            			D_E_L_E_T_E,
            			OrdemServicoItemFinalizado,
            			IdEmpresa,
            			IdProjeto,
            			IdTag,
            			Autor,
            			PalavraChave,
            			Notas,
            			Espessura,
            			MaterialSW,
            			AreaPintura,
            			NumeroDobras,
            			Peso,
            			Unidade,
            			UnidadeSW,
            			ValorSW,
            			Altura,
            			Largura,
            			DtCad,
            			UsuarioCriacao,
            			UsuarioAlteracao,
            			DtAlteracao,
            			EnderecoArquivo,
            			txtSoldagem,
            			txtTipoDesenho,
            			txtCorte,
            			txtDobra,
            			txtSolda,
            			txtPintura,
            			txtMontagem,
            			QtdeRomaneio,
            			Liberado_Engenharia,
            			Data_Liberacao_Engenharia,
            			DescEmpresa,
            			ProdutoPrincipal,
            			RNC,
            			ComprimentoCaixaDelimitadora,
            			LarguraCaixaDelimitadora,
            			EspessuraCaixaDelimitadora,
            			txtItemEstoque,
            			AreaPinturaUnitario,
            			PesoUnitario,
            			DataPrevisao,
            EnderecoArquivoItemOrdemServico
            						   FROM
            				 " & ComplementoTipoBanco & "ordemservicoitem
            				WHERE
            				(D_E_L_E_T_E <> '*')
            				AND (IdOrdemServico = '" & OrdemServico.IdOrdemServico & "')
            				AND (CodMatFabricante LIKE '%" & Me.txtPesqNumeroDesenho.Text & "%')
            				AND (Acabamento LIKE '%" & Me.txtPesqAcabamentoDesenho.Text & "%')
            				ORDER BY  IdOrdemServicoItem")



            '            DGVListaMaterialSWMateriais.DataSource = cl_BancoDados.CarregarDados("SELECT
            '    o.PROJETO,
            '    o.TAG,
            '    o.CodMatFabricante,
            '    m.NumeroRP,
            '    o.DescResumo,
            '    o.DescDetal,
            '    SUM(o.QtdeTotal) AS QtdeTotal,
            '    o.Unidade,
            '    SUM(o.Peso) AS Peso,
            '    o.IDOrdemServico
            'FROM
            '     " & ComplementoTipoBanco & "ordemservicoitem o
            'LEFT JOIN
            '    material m ON m.CodMatFabricante = o.CodMatFabricante
            'WHERE
            '    (o.D_E_L_E_T_E <> '*' or o.D_E_L_E_T_E is null)
            '    AND (IdOrdemServico = '" & OrdemServico.IdOrdemServico & "')
            '    and m.NumeroRP <> ''
            'GROUP BY
            '    o.PROJETO,
            '    o.TAG,
            '    o.CodMatFabricante,
            '    m.NumeroRP,
            '    o.DescResumo,
            '    o.DescDetal,
            '    o.Unidade,
            '    o.IDOrdemServico;
            '")


            DGVListaMaterialSWMateriais.DataSource = cl_BancoDados.CarregarDados("SELECT
    o.PROJETO,
    o.TAG,
    o.CodMatFabricante,
    o.DescResumo,
    o.DescDetal,
    SUM(o.QtdeTotal) AS QtdeTotal,
    o.Unidade,
    SUM(o.Peso) AS Peso,
    o.IDOrdemServico
FROM
     " & ComplementoTipoBanco & "ordemservicoitem o
WHERE
    (o.D_E_L_E_T_E <> '*' or o.D_E_L_E_T_E is null)
    AND (IdOrdemServico = '" & OrdemServico.IdOrdemServico & "') and enderecoarquivo = ''
    GROUP BY
    o.PROJETO,
    o.TAG,
    o.CodMatFabricante,
    o.DescResumo,
    o.DescDetal,
    o.Unidade,
    o.IDOrdemServico")


        Catch ex As Exception
        Finally

        End Try

        'CarregarDadosDGV()

        TimerDGVListaMaterialSW.Enabled = False

    End Sub

    '  Private Sub CarregarDadosDGV()
    '      Try
    '          ' Certifique-se de que o banco está aberto
    '          ' If Not cl_BancoDados.AbrirBanco Then Exit Sub

    '          ' Consultas SQL otimizadas
    '          Dim sqlListaMaterial As String =
    '          "SELECT
    'IDOrdemServicoItem,
    '  qtdeTotal,
    ' UPPER(RTrim(CodMatFabricante)) As CodMatFabricante,
    ' UPPER(RTrim(DescResumo)) As DescResumo,
    '			UPPER(RTrim(DescDetal)) As DescDetal,
    '			UPPER(RTrim(Autor)) As Autor,
    '			UPPER(RTrim(Palavrachave)) As Palavrachave,
    '			UPPER(RTrim(Notas)) As Notas,
    '			Espessura,
    '			Altura,
    '			Largura,
    '			Replace(AreaPintura, ',', '.') AS AreaPintura,
    '			AreaPinturaUnitario,
    '			NumeroDobras,
    '			Peso,
    '			PesoUnitario,
    '			UPPER(RTrim(Unidade)) As Unidade,
    '			IdOrdemServico,
    '			UPPER(RTrim(Projeto)) As Projeto,
    '			UPPER(RTrim(Tag)) As Tag,
    '			UPPER(RTrim(ESTATUS_OrdemServico)) As ESTATUS_OrdemServico,
    '			IdMaterial,
    '			qtdeProduzida,
    '			qtdeFaltante,
    '			UPPER(RTrim(CriadoPor)) As CriadoPor,
    '			DataCriacao,
    '			UPPER(RTrim(Estatus)) As Estatus,
    '			D_E_L_E_T_E,
    '			UPPER(RTrim(ORDEMSERVICOITEMFINALIZADO)) As ORDEMSERVICOITEMFINALIZADO,
    '			IdEmpresa,
    '			idProjeto,
    '			idTag,
    '			UPPER(RTrim(UnidadeSW)) As UnidadeSW,
    '			UPPER(RTrim(ValorSW)) As ValorSW,
    '			DtCad,
    '			UPPER(RTrim(UsuarioCriacao)) As UsuarioCriacao,
    '			UsuarioAlteracao,
    '			UPPER(RTrim(DtAlteracao)) As DtAlteracao,
    '			UPPER(RTrim(Replace(EnderecoArquivo, '##', '\\'))) AS EnderecoArquivo,
    '			UPPER(RTrim(MaterialSW)) As MaterialSW,
    '			Fator,
    '			qtde,
    '			UPPER(RTrim(Acabamento)) As Acabamento,
    '			UPPER(RTrim(txtTipoDesenho)) As TipoDesenho,
    '			UPPER(RTrim(MaterialSW)) As material,
    '			ProdutoPrincipal,
    '			txtItemEstoque,
    '			AreaPinturaUnitario,
    '			PesoUnitario,
    '			 Acabamento,
    '			   txtSoldagem,
    '			   txtTipoDesenho,
    '			   txtCorte,
    '			   txtDobra,
    '			   txtSolda,
    '			   txtPintura,
    '			   txtMontagem,
    '			   RNC,
    'Comprimentocaixadelimitadora,
    'Larguracaixadelimitadora,
    'Espessuracaixadelimitadora
    '	 FROM  " & ComplementoTipoBanco & "ordemservicoitem
    '	 WHERE D_E_L_E_T_E <> '*'
    '	   AND txtTipoDesenho <> 'material'
    '	   AND IdOrdemServico = @IdOrdemServico
    '	   AND CodMatFabricante LIKE @NumeroDesenho
    '	   AND Acabamento LIKE @Acabamento
    '	 ORDER BY IDOrdemServicoItem"

    '          Dim parametrosListaMaterial = New Dictionary(Of String, Object) From {
    '          {"@IdOrdemServico", OrdemServico.IdOrdemServico},
    '          {"@NumeroDesenho", $"%{Me.txtPesqNumeroDesenho.Text.Trim()}%"},
    '          {"@Acabamento", $"%{Me.txtPesqAcabamentoDesenho.Text.Trim()}%"}
    '      }

    '          Dim sqlListaMaterialSW As String =
    '          "SELECT IdOrdemServico,
    '			CodMatFabricante,
    '			DescResumo,
    '			DescDetal,
    '			SUM(REPLACE(QtdeTotal, ',', '.')) AS QtdeTotal,
    '			txtTipoDesenho
    '	 FROM  " & ComplementoTipoBanco & "ordemservicoitem
    '	 WHERE D_E_L_E_T_E <> '*'
    '	   AND IdOrdemServico = @IdOrdemServico
    '	   AND txtTipoDesenho = 'material'
    '	 GROUP BY IdOrdemServico,
    '			  CodMatFabricante,
    '			  DescResumo,
    '			  DescDetal,
    '			  txtTipoDesenho
    '	 ORDER BY IdOrdemServico"

    '          Dim parametrosListaMaterialSW = New Dictionary(Of String, Object) From {
    '          {"@IdOrdemServico", OrdemServico.IdOrdemServico}
    '      }

    '          ' Carregar dados de forma assíncrona
    '          Dim listaMaterialTask = Task.Run(Function() cl_BancoDados.CarregarDadosNovaAsync(sqlListaMaterial, parametrosListaMaterial))
    '          Dim listaMaterialSWTask = Task.Run(Function() cl_BancoDados.CarregarDadosNovaAsync(sqlListaMaterialSW, parametrosListaMaterialSW))

    '          Task.WaitAll(listaMaterialTask, listaMaterialSWTask)

    '          ' Configurar DataGridView principal
    '          DGVListaMaterialSW.SuspendLayout()
    '          DGVListaMaterialSW.DataSource = listaMaterialTask.Result

    '          ' Configuração de colunas
    '          'ConfigurarColunasDGV(DGVListaMaterialSW, {"QtdeTotal", "CodMatFabricante", "DescResumo", "DescDetal", "Espessura", "Altura", "Largura", "AreaPintura", "Peso", "Acabamento", "TipoDesenho"})

    '          Dim colunasVisiveis As String() = {"dgvSelecao", "dgvIconeItemOS", "dgvDXF", "dgvPDF",
    '      "QtdeTotal", "DescResumo", "DescDetal", "Espessura", "Altura", "Largura",
    '      "AreaPintura", "Peso", "CodMatFabricante", "Acabamento", "TipoDesenho"}

    '          ' Lista de colunas invisíveis
    '          Dim colunasInvisiveis As String() = {
    '      "IdOrdemServico", "Projeto", "Tag", "ESTATUS_OrdemServico", "IdMaterial",
    '      "QtdeProduzida", "QtdeFaltante", "CriadoPor", "DataCriacao", "Estatus",
    '      "D_E_L_E_T_E", "ORDEMSERVICOITEMFINALIZADO", "IdEmpresa", "idProjeto",
    '      "idTag", "Autor", "Palavrachave", "Notas", "AreaPinturaUnitario", "NumeroDobras",
    '      "PesoUnitario", "Unidade", "UnidadeSW", "ValorSW", "DtCad", "UsuarioCriacao",
    '      "UsuarioAlteracao", "DtAlteracao", "EnderecoArquivo", "MaterialSW", "Fator",
    '      "qtde", "material", "ProdutoPrincipal", "txtItemEstoque", "txtSoldagem",
    '      "txtTipoDesenho", "txtCorte", "txtDobra", "txtSolda", "txtPintura", "txtMontagem",
    '      "RNC", "Comprimentocaixadelimitadora", "Larguracaixadelimitadora",
    '      "Espessuracaixadelimitadora"}

    '          'Chamar a função para configurar as colunas
    '          ConfigurarColunasDGV(DGVListaMaterialSW, colunasVisiveis, colunasInvisiveis)

    '          ' Configurar DataGridView secundário
    '          'DGVListaMaterialSWMaterial.SuspendLayout()
    '          ' DGVListaMaterialSWMaterial.DataSource = listaMaterialSWTask.Result
    '          ' ConfigurarColunasDGV(DGVListaMaterialSWMaterial, {"CodMatFabricante", "DescResumo", "DescDetal", "QtdeTotal", "TipoDesenho"})

    '      Catch ex As Exception
    '          MessageBox.Show("Erro ao carregar dados: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '          LogarErro(ex)
    '      Finally
    '          ' Certifique-se de que os layouts foram retomados
    '          ' DGVListaMaterialSW.ResumeLayout()
    '          ' DGVListaMaterialSWMaterial.ResumeLayout()
    '      End Try
    '  End Sub

    'Private Sub ConfigurarColunasDGV(dgv As DataGridView, colunasVisiveis As String(), colunasInvisiveis As String())

    '    '' Definindo as colunas que serão visíveis
    '    'Dim colunasVisiveis As String() = {"CodMatFabricante", "DescResumo", "QtdeTotal", "AreaPintura"}

    '    '' Definindo as colunas que serão invisíveis
    '    'Dim colunasInvisiveis As String() = {"IdMaterial", "IdOrdemServico", "UsuarioAlteracao"}

    '    '' Chamar a função para configurar as colunas
    '    'ConfigurarColunasDGV(DGVListaMaterialSW, colunasVisiveis, colunasInvisiveis)
    '    '
    '    'Tornar todas as colunas invisíveis inicialmente
    '    For Each coluna As DataGridViewColumn In dgv.Columns
    '        coluna.Visible = False
    '    Next

    '    ' Configurar as colunas que serão visíveis
    '    For Each nomeColuna As String In colunasVisiveis
    '        ' Verifica se a coluna existe no DataGridView
    '        If dgv.Columns.Contains(nomeColuna) Then
    '            ' Torna a coluna visível
    '            dgv.Columns(nomeColuna).Visible = True

    '            ' Se a coluna for "CodMatFabricante", a congura para ficar "congelada" (fixa)
    '            If nomeColuna = "CodMatFabricante" Then
    '                dgv.Columns(nomeColuna).Frozen = True
    '            End If
    '        End If
    '    Next

    '    ' Configurar as colunas que serão invisíveis (caso não estejam na lista de visíveis)
    '    For Each nomeColuna As String In colunasInvisiveis
    '        ' Verifica se a coluna existe no DataGridView
    '        If dgv.Columns.Contains(nomeColuna) Then
    '            ' Torna a coluna invisível
    '            dgv.Columns(nomeColuna).Visible = False
    '        End If
    '    Next

    '    ' Ajustar a largura das colunas
    '    For Each coluna As DataGridViewColumn In dgv.Columns
    '        ' Ajusta a largura da coluna para 351, caso seja maior que 350
    '        If coluna.Width > 350 Then
    '            coluna.Width = 351
    '        End If
    '    Next
    'End Sub

    'Private Sub LogarErro(ex As Exception)
    '    Try
    '        Dim logPath As String = "C:\Caminho\Para\Seu\Log\log.txt"
    '        File.AppendAllText(logPath, $"{DateTime.Now}: {ex.Message}{System.Environment.NewLine}")
    '    Catch logEx As Exception
    '        ' Evitar interrupção por erro de log
    '    End Try
    'End Sub

    Private Sub FormatarColunaIconeDGVListaMaterialSW()

        Dim dxf, pdf As String

        For Each row As DataGridViewRow In DGVListaMaterialSW.Rows
            Dim valorEnderecoArquivo As String = If(row.Cells("EnderecoArquivo").Value, "").ToString()
            Dim valorProdutoPrincipal As String = If(row.Cells("ProdutoPrincipal").Value, "").ToString()

            ' Verifica se a string ".SLDASM" está contida na célula e se "ProdutoPrincipal" é "SIM" (ignora maiúsculas/minúsculas)
            If valorEnderecoArquivo.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 AndAlso
           valorProdutoPrincipal.IndexOf("SIM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                row.Cells("dgvIconeItemOS").Value = My.Resources.IconeswPrincipal ' Substitua pelo seu ícone
            ElseIf valorEnderecoArquivo.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                ' Define a imagem na coluna "dgvIconeItemOS" se for .SLDASM
                row.Cells("dgvIconeItemOS").Value = My.Resources.IcopneMontagemSW ' Substitua pelo seu ícone
            ElseIf valorEnderecoArquivo.IndexOf(".SLDPRT", StringComparison.OrdinalIgnoreCase) >= 0 Then
                ' Define outra imagem se for .SLDPRT
                row.Cells("dgvIconeItemOS").Value = My.Resources.IcopneMontagemPRT
            Else
                row.Cells("dgvIconeItemOS").Value = My.Resources.material_escolar_32
            End If

            ' Verifica se o arquivo é uma peça (.SLDPRT) ou uma montagem (.SLDASM) e altera para .dxf
            If valorEnderecoArquivo.EndsWith(".SLDPRT", StringComparison.OrdinalIgnoreCase) OrElse
               valorEnderecoArquivo.EndsWith(".SLDASM", StringComparison.OrdinalIgnoreCase) Then
                dxf = Path.ChangeExtension(valorEnderecoArquivo, ".dxf")

                ' Verifica se o arquivo DXF existe
                If File.Exists(dxf) Then
                    row.Cells("DGVDXF").Value = My.Resources.arquivo_dxf
                Else
                    row.Cells("DGVDXF").Value = My.Resources.Sem_Incone
                End If
            End If

            ' Altera para .pdf
            If valorEnderecoArquivo.EndsWith(".SLDPRT", StringComparison.OrdinalIgnoreCase) OrElse
               valorEnderecoArquivo.EndsWith(".SLDASM", StringComparison.OrdinalIgnoreCase) Then
                pdf = Path.ChangeExtension(valorEnderecoArquivo, ".pdf")

                ' Verifica se o arquivo PDF existe
                If File.Exists(pdf) Then
                    row.Cells("DGVPDF").Value = My.Resources.ficheiro_pdf
                Else
                    row.Cells("DGVPDF").Value = My.Resources.Sem_Incone
                End If
            End If

        Next
    End Sub

    Private Sub dgvos_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvos.DataError
        Try
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub DGVListaMaterialSW_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DGVListaMaterialSW.DataError
        Try
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub btnNovoOS_Click(sender As Object, e As EventArgs)

        OrdemServico.IdOrdemServico = Nothing
        OrdemServico.Projeto = Nothing
        OrdemServico.Tag = Nothing
        OrdemServico.Descricao = Nothing
        OrdemServico.Estatus = Nothing
        OrdemServico.idTag = Nothing
        OrdemServico.idProjeto = Nothing
        OrdemServico.DescEmpresa = Nothing

        Me.lblOrdemServicoAtiva.Text = ""

        Me.cboProjeto.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboTag.DropDownStyle = ComboBoxStyle.DropDownList

        Me.cboProjeto.Text = ""
        Me.cboTag.Text = ""
        Me.txtCliente.Clear()
        Me.txtDescricaoTag.Clear()
        Me.txtDescricao.Clear()
        Me.cboProjeto.Enabled = True
        Me.cboTag.Enabled = True

        Me.cboProjeto.Focus()

        TimerDGVListaMaterialSW.Enabled = True

    End Sub

    Private Sub btnSalvarOs_Click(sender As Object, e As EventArgs)

        Try
            OrdemServico.Descricao = Me.txtDescricao.Text
            OrdemServico.idTag = cboTag.SelectedValue
            OrdemServico.idProjeto = cboProjeto.SelectedValue

            OrdemServico.CriarOsCompleta(dgvos, Timerdgvos, TimerDGVListaMaterialSW)

            ' Seleciona a última linha do DataGridView
            If dgvos.Rows.Count > 0 Then
                ' Obtém o índice da última linha
                Dim ultimaLinha As Integer = dgvos.Rows.Count - 1

                ' Define a última linha como selecionada
                dgvos.ClearSelection() ' Limpa seleções anteriores
                dgvos.Rows(ultimaLinha).Selected = True

                ' Faz a rolagem para garantir que a linha esteja visível
                dgvos.FirstDisplayedScrollingRowIndex = ultimaLinha

                dgvos.Rows(ultimaLinha).DefaultCellStyle.BackColor = Color.LightCyan

                TimerDGVListaMaterialSW.Enabled = True

            End If
        Catch ex As Exception
            MsgBox("Erro ao criar OS")
        Finally
        End Try

    End Sub

    ' Função para obter valores da célula e tratar erros
    Private Function ObterValorCelula(row As DataGridViewRow, coluna As String, Optional valorPadrao As Object = "") As Object
        Try
            Dim valor = row.Cells(coluna).Value
            If valor Is Nothing Then
                Return valorPadrao
            Else
                Return valor.ToString().ToUpper()
            End If
        Catch ex As Exception
            Return valorPadrao
        End Try
    End Function

    ' Função para converter de ponto (.) para vírgula (,) e vice-versa
    Private Function ConverterSeparadorDecimal(valor As String, paraVirgula As Boolean) As String
        If String.IsNullOrEmpty(valor) Then Return valor
        If paraVirgula Then
            ' Trocar ponto por vírgula
            Return valor.Replace(".", ",")
        Else
            ' Trocar vírgula por ponto
            Return valor.Replace(",", ".")
        End If
    End Function

    Private Sub btnInserirItensOrdemServico_Click(sender As Object, e As EventArgs)

        '  Dim verificaRnc As Boolean = False

        Dim result As DialogResult = MessageBox.Show("Deseja Realmente Inserir os itens da lista BOM na OS: " & Me.lblOrdemServicoAtiva.Text, "Inserção de Itens da OS", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then

            Dim rnc As String

            Dim Fator As Double
            '  Try

            ProgressBarListaSW.Minimum = 0
            ProgressBarListaSW.Maximum = dgvDataGridBOM.Rows.Count

            'If dgvDataGridBOM.Rows.Count > 0 Then

            '    For b As Integer = 0 To dgvDataGridBOM.Rows.Count - 1
            '        Try

            '            Try

            '                rnc = dgvDataGridBOM.Rows(b).Cells("RNC").Value.ToString

            '            Catch ex As Exception
            '                rnc = ""
            '            End Try

            '            If rnc = "S" Then

            '                MsgBox("Na lista, há peças com RNC pendente; para prosseguir com o processo de liberação, é necessário remover a peça da lista ou resolver a RNC.", vbCritical, "Atenção")

            '                rnc = dgvDataGridBOM.Rows(b).DefaultCellStyle.BackColor = Color.LightSalmon

            '                verificaRnc = True

            '                Exit Sub

            '            Else

            '                verificaRnc = False

            '            End If

            '        Catch ex As Exception
            '            Continue For
            '        End Try

            '    Next

            'End If

            'If verificaRnc = False Then

            If dgvDataGridBOM.Rows.Count > 0 Then

                If OrdemServico.IdOrdemServico = Nothing Or OrdemServico.IdOrdemServico = 0 Then

                    MsgBox("A Ordem de Serviço deve ser selecionada", vbCritical, "Atenção")

                    Exit Sub
                Else

                    Try
                        Fator = InputBox("Informe o Valor Multiplicado de fabricação", "Fator de Multiplicação", 1)

                        ' Verifica se o usuário clicou em "Cancelar" (Fator será uma string vazia)
                        If Fator = "" Then
                            MsgBox("A Operação foi cancelda", vbInformation, "Atenação")

                            Exit Sub ' Sai do procedimento
                        End If

                        ' Verifica se o valor inserido é numérico e maior que 0
                        If Not IsNumeric(Fator) OrElse Convert.ToDouble(Fator) <= 0 Then
                            MsgBox("A operação foi cancelada. O valor informado não é um número válido!", vbInformation, "Atenação")
                            Exit Sub
                        End If
                    Catch ex As Exception
                        ' Fator = "1"
                    Finally
                        ' Código adicional, se necessário
                    End Try

                    For A As Integer = 0 To dgvDataGridBOM.Rows.Count - 1
                        Try
                            If dgvDataGridBOM.Rows(A).Cells("CodMatFabricante").Value.ToString <> "" Or dgvDataGridBOM.Rows(A).Cells("CodMatFabricante").Value.ToString <> Nothing Then

                                Try
                                    OrdemServico.EnderecoArquivo = dgvDataGridBOM.Rows(A).Cells("EnderecoArquivo").Value.ToString.ToUpper
                                    'OrdemServico.EnderecoArquivo = Replace(OrdemServico.EnderecoArquivo, "\", "##")
                                    ' OrdemServico.EnderecoArquivo = Replace(OrdemServico.EnderecoArquivo, "/", "#")
                                Catch ex As Exception
                                    OrdemServico.EnderecoArquivo = ""
                                End Try
                                Try
                                    OrdemServico.CodMatFabricante = dgvDataGridBOM.Rows(A).Cells("CodMatFabricante").Value.ToString.ToUpper
                                Catch ex As Exception
                                    OrdemServico.CodMatFabricante = ""
                                End Try

                                Try
                                    OrdemServico.DescResumo = dgvDataGridBOM.Rows(A).Cells("DescResumo").Value.ToString.ToUpper
                                Catch ex As Exception
                                    OrdemServico.DescResumo = ""
                                End Try

                                Try
                                    OrdemServico.DescDetal = dgvDataGridBOM.Rows(A).Cells("DescDetal").Value.ToString.ToUpper
                                Catch ex As Exception
                                    OrdemServico.DescDetal = ""
                                End Try

                                Try
                                    OrdemServico.Autor = dgvDataGridBOM.Rows(A).Cells("Autor").Value.ToString.ToUpper
                                Catch ex As Exception
                                    OrdemServico.Autor = ""
                                End Try
                                Try
                                    OrdemServico.Palavrachave = dgvDataGridBOM.Rows(A).Cells("Palavrachave").Value.ToString.ToUpper
                                Catch ex As Exception

                                    OrdemServico.Palavrachave = ""
                                End Try
                                Try
                                    OrdemServico.Notas = dgvDataGridBOM.Rows(A).Cells("Notas").Value.ToString.ToUpper
                                Catch ex As Exception

                                    OrdemServico.Notas = ""
                                End Try
                                Try
                                    OrdemServico.Espessura = dgvDataGridBOM.Rows(A).Cells("Espessura").Value.ToString
                                Catch ex As Exception
                                    OrdemServico.Espessura = ""
                                End Try
                                Try
                                    OrdemServico.NumeroDobras = dgvDataGridBOM.Rows(A).Cells("NumeroDobras").Value.ToString
                                Catch ex As Exception
                                    OrdemServico.NumeroDobras = ""
                                End Try
                                If dgvDataGridBOM.Rows(A).Cells("EnderecoArquivo").Value.ToString().IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                                    OrdemServico.Unidade = "CONJ"
                                    OrdemServico.UnidadeSW = "CONJ"
                                Else
                                    OrdemServico.Unidade = "PC"
                                    OrdemServico.UnidadeSW = "PC"

                                End If
                                OrdemServico.ValorSW = ""
                                Try
                                    OrdemServico.Altura = Replace(dgvDataGridBOM.Rows(A).Cells("Altura").Value.ToString, ",", "")
                                Catch ex As Exception
                                    OrdemServico.Altura = ""
                                End Try

                                Try
                                    OrdemServico.Largura = Replace(dgvDataGridBOM.Rows(A).Cells("Largura").Value.ToString, ",", "")
                                Catch ex As Exception

                                    OrdemServico.Largura = ""
                                End Try
                                OrdemServico.DtCad = ""
                                OrdemServico.UsuarioCriacao = ""
                                OrdemServico.UsuarioAlteracao = ""
                                OrdemServico.DtAlteracao = ""
                                Try
                                    OrdemServico.MaterialSW = dgvDataGridBOM.Rows(A).Cells("material").Value.ToString.ToUpper
                                Catch ex As Exception
                                    OrdemServico.MaterialSW = ""

                                End Try
                                Try
                                    OrdemServico.qtde = dgvDataGridBOM.Rows(A).Cells("qtde").Value.ToString
                                Catch ex As Exception

                                    OrdemServico.qtde = 0
                                End Try
                                Try
                                    OrdemServico.AreaPintura = Replace(dgvDataGridBOM.Rows(A).Cells("AreaPintura").Value.ToString, ".", ",")
                                    OrdemServico.AreaPintura = OrdemServico.AreaPintura * OrdemServico.qtde * Fator
                                    OrdemServico.AreaPintura = Replace(OrdemServico.AreaPintura, ",", ".")
                                Catch ex As Exception

                                    OrdemServico.AreaPintura = ""
                                End Try
                                Try
                                    OrdemServico.AreaPinturaUnitario = Replace(dgvDataGridBOM.Rows(A).Cells("AreaPintura").Value.ToString, ".", ",")
                                    OrdemServico.AreaPinturaUnitario = Replace(OrdemServico.AreaPinturaUnitario, ",", ".")
                                Catch ex As Exception
                                    OrdemServico.AreaPinturaUnitario = ""
                                End Try
                                Try
                                    OrdemServico.Peso = Replace(dgvDataGridBOM.Rows(A).Cells("Peso").Value.ToString, ".", ",")

                                    OrdemServico.Peso = OrdemServico.Peso * OrdemServico.qtde * Fator
                                    OrdemServico.Peso = Replace(OrdemServico.Peso, ",", ".")
                                Catch ex As Exception
                                    OrdemServico.Peso = 0
                                End Try
                                Try
                                    OrdemServico.PesoUnitario = Replace(dgvDataGridBOM.Rows(A).Cells("Peso").Value.ToString, ".", ",")
                                    OrdemServico.PesoUnitario = Replace(OrdemServico.PesoUnitario, ",", ".")
                                Catch ex As Exception
                                    OrdemServico.PesoUnitario = 0
                                End Try

                                Try
                                    OrdemServico.txtSoldagem = dgvDataGridBOM.Rows(A).Cells("txtSoldagem").Value.ToString
                                Catch ex As Exception
                                    OrdemServico.txtSoldagem = ""
                                End Try

                                Try
                                    OrdemServico.QtdeTotal = Replace(OrdemServico.QtdeTotal, ".", ",")
                                    OrdemServico.QtdeTotal = OrdemServico.qtde * Fator
                                    OrdemServico.QtdeTotal = Replace(OrdemServico.QtdeTotal, ",", ".")
                                Catch ex As Exception
                                    OrdemServico.QtdeTotal = 0
                                End Try
                                Try
                                    OrdemServico.txtTipoDesenho = dgvDataGridBOM.Rows(A).Cells("txtTipoDesenho").Value.ToString
                                Catch ex As Exception

                                    OrdemServico.txtTipoDesenho = ""

                                End Try

                                Try
                                    OrdemServico.txtCorte = dgvDataGridBOM.Rows(A).Cells("txtCorte").Value.ToString
                                Catch ex As Exception
                                    OrdemServico.txtCorte = ""
                                End Try
                                Try
                                    OrdemServico.txtDobra = dgvDataGridBOM.Rows(A).Cells("txtDobra").Value.ToString
                                Catch ex As Exception

                                    OrdemServico.txtDobra = ""

                                End Try
                                Try
                                    OrdemServico.txtSolda = dgvDataGridBOM.Rows(A).Cells("txtSolda").Value.ToString
                                Catch ex As Exception
                                    OrdemServico.txtSolda = ""
                                End Try
                                Try
                                    OrdemServico.txtPintura = dgvDataGridBOM.Rows(A).Cells("txtPintura").Value.ToString
                                Catch ex As Exception
                                    OrdemServico.txtPintura = ""
                                End Try
                                Try
                                    OrdemServico.txtMontagem = dgvDataGridBOM.Rows(A).Cells("txtMontagem").Value.ToString
                                Catch ex As Exception

                                    OrdemServico.txtMontagem = ""
                                End Try

                                Try
                                    OrdemServico.Comprimentocaixadelimitadora = dgvDataGridBOM.Rows(A).Cells("Comprimentocaixadelimitadora").Value.ToString
                                    OrdemServico.Comprimentocaixadelimitadora = Replace(OrdemServico.Comprimentocaixadelimitadora, ",", ".")
                                Catch ex As Exception

                                    OrdemServico.Comprimentocaixadelimitadora = ""
                                End Try
                                Try
                                    OrdemServico.Larguracaixadelimitadora = dgvDataGridBOM.Rows(A).Cells("Larguracaixadelimitadora").Value.ToString
                                    OrdemServico.Larguracaixadelimitadora = Replace(OrdemServico.Larguracaixadelimitadora, ",", ".")
                                Catch ex As Exception

                                    OrdemServico.Larguracaixadelimitadora = ""
                                End Try

                                Try
                                    OrdemServico.Espessuracaixadelimitadora = dgvDataGridBOM.Rows(A).Cells("Espessuracaixadelimitadora ").Value.ToString
                                    OrdemServico.Espessuracaixadelimitadora = Replace(OrdemServico.Espessuracaixadelimitadora, ",", ".")
                                Catch ex As Exception

                                    OrdemServico.Espessuracaixadelimitadora = ""

                                End Try
                                Try
                                    OrdemServico.txtItemEstoque = dgvDataGridBOM.Rows(A).Cells("txtItemEstoque").Value.ToString.ToUpper
                                Catch ex As Exception
                                    OrdemServico.txtItemEstoque = ""
                                End Try
                                Try
                                    OrdemServico.txtAcabamento = dgvDataGridBOM.Rows(A).Cells("Acabamento").Value.ToString.ToUpper
                                Catch ex As Exception

                                    OrdemServico.txtAcabamento = ""

                                End Try
                                OrdemServico.QtdeTotal = Replace(OrdemServico.QtdeTotal, ",", "")
                                ProgressBarListaSW.Value = A

                                Dim query As String = "INSERT INTO ordemservicoitem (
							IdOrdemServico, idProjeto, Projeto, idTag, Tag,
							ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal,
							Autor, Palavrachave, Notas, Espessura, AreaPintura,
							NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura,
							Largura, CodMatFabricante, DtCad, UsuarioCriacao,
							UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW,
							QtdeTotal,CriadoPor,
							DataCriacao, Estatus, Acabamento, D_E_L_E_T_E, fator, qtde,
							txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda,
							txtPintura, txtMontagem, tttxtCorte, tttxtDobra, tttxtSolda,
							tttxtPintura, tttxtMontagem, Comprimentocaixadelimitadora,
							Larguracaixadelimitadora, Espessuracaixadelimitadora,
							AreaPinturaUnitario, PesoUnitario, txtItemEstoque
						   ) VALUES (
							@IdOrdemServico, @idProjeto, @Projeto, @idTag, @Tag,
							@ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal,
							@Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura,
							@NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura,
							@Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao,
							@UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW,
							@QtdeTotal, @CriadoPor,
							@DataCriacao, @Estatus, @Acabamento, @D_E_L_E_T_E, @fator, @qtde,
							@txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda,
							@txtPintura, @txtMontagem, @tttxtCorte, @tttxtDobra, @tttxtSolda,
							@tttxtPintura, @tttxtMontagem, @Comprimentocaixadelimitadora,
							@Larguracaixadelimitadora, @Espessuracaixadelimitadora,
							@AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque
						   );"
                                Using command As New MySqlCommand(query, myconect)
                                    ' Adicionando os parâmetros
                                    command.Parameters.AddWithValue("@IdOrdemServico", OrdemServico.IdOrdemServico)
                                    command.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
                                    command.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                                    command.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
                                    command.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                                    command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.Estatus)
                                    command.Parameters.AddWithValue("@IdMaterial", OrdemServico.IdMaterial)
                                    command.Parameters.AddWithValue("@DescResumo", OrdemServico.DescResumo)
                                    command.Parameters.AddWithValue("@DescDetal", OrdemServico.DescDetal)
                                    command.Parameters.AddWithValue("@Autor", OrdemServico.Autor)
                                    command.Parameters.AddWithValue("@Palavrachave", OrdemServico.Palavrachave)
                                    command.Parameters.AddWithValue("@Notas", OrdemServico.Notas)
                                    command.Parameters.AddWithValue("@Espessura", OrdemServico.Espessura)
                                    command.Parameters.AddWithValue("@AreaPintura", OrdemServico.AreaPintura)
                                    command.Parameters.AddWithValue("@NumeroDobras", OrdemServico.NumeroDobras)
                                    command.Parameters.AddWithValue("@Peso", OrdemServico.Peso.ToString.Replace(",", "."))
                                    command.Parameters.AddWithValue("@Unidade", OrdemServico.Unidade)
                                    command.Parameters.AddWithValue("@UnidadeSW", OrdemServico.UnidadeSW)
                                    command.Parameters.AddWithValue("@ValorSW", OrdemServico.ValorSW)
                                    command.Parameters.AddWithValue("@Altura", OrdemServico.Altura)
                                    command.Parameters.AddWithValue("@Largura", OrdemServico.Largura)
                                    command.Parameters.AddWithValue("@CodMatFabricante", OrdemServico.CodMatFabricante)
                                    command.Parameters.AddWithValue("@DtCad", "")
                                    command.Parameters.AddWithValue("@UsuarioCriacao", "")
                                    command.Parameters.AddWithValue("@UsuarioAlteracao", "")
                                    command.Parameters.AddWithValue("@DtAlteracao", "")
                                    command.Parameters.AddWithValue("@EnderecoArquivo", OrdemServico.EnderecoArquivo)
                                    command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSW)
                                    command.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal)
                                    'command.Parameters.AddWithValue("@QtdeProduzida", "")
                                    'command.Parameters.AddWithValue("@QtdeFaltante", "")
                                    command.Parameters.AddWithValue("@CriadoPor", Usuario.NomeCompleto.ToString)
                                    command.Parameters.AddWithValue("@DataCriacao", Date.Now)
                                    command.Parameters.AddWithValue("@Estatus", "A")
                                    command.Parameters.AddWithValue("@Acabamento", OrdemServico.txtAcabamento)
                                    command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                                    command.Parameters.AddWithValue("@fator", Fator)
                                    command.Parameters.AddWithValue("@qtde", OrdemServico.qtde)
                                    command.Parameters.AddWithValue("@txtSoldagem", OrdemServico.txtSoldagem)
                                    command.Parameters.AddWithValue("@txtTipoDesenho", OrdemServico.txtTipoDesenho)
                                    command.Parameters.AddWithValue("@txtCorte", OrdemServico.txtCorte)
                                    command.Parameters.AddWithValue("@txtDobra", OrdemServico.txtDobra)
                                    command.Parameters.AddWithValue("@txtSolda", OrdemServico.txtSolda)
                                    command.Parameters.AddWithValue("@txtPintura", OrdemServico.txtPintura)
                                    command.Parameters.AddWithValue("@txtMontagem", OrdemServico.txtMontagem)
                                    command.Parameters.AddWithValue("@tttxtCorte", OrdemServico.tttxtCorte)
                                    command.Parameters.AddWithValue("@tttxtDobra", OrdemServico.tttxtDobra)
                                    command.Parameters.AddWithValue("@tttxtSolda", OrdemServico.tttxtSolda)
                                    command.Parameters.AddWithValue("@tttxtPintura", OrdemServico.tttxtPintura)
                                    command.Parameters.AddWithValue("@tttxtMontagem", OrdemServico.tttxtMontagem)
                                    command.Parameters.AddWithValue("@Comprimentocaixadelimitadora", OrdemServico.Comprimentocaixadelimitadora)
                                    command.Parameters.AddWithValue("@Larguracaixadelimitadora", OrdemServico.Larguracaixadelimitadora)
                                    command.Parameters.AddWithValue("@Espessuracaixadelimitadora", OrdemServico.Espessuracaixadelimitadora)
                                    command.Parameters.AddWithValue("@AreaPinturaUnitario", OrdemServico.AreaPinturaUnitario)
                                    command.Parameters.AddWithValue("@PesoUnitario", OrdemServico.PesoUnitario)
                                    command.Parameters.AddWithValue("@txtItemEstoque", OrdemServico.txtItemEstoque)

                                    ' Abrir conexão e executar comando

                                    'If cl_BancoDados.AbrirBanco = False Then
                                    '    cl_BancoDados.AbrirBanco()

                                    'End If
                                    'myconect.Open()
                                    command.ExecuteNonQuery()
                                End Using
                            End If
                            ProgressBarListaSW.Value = A
                        Catch ex As Exception
                            MsgBox(ex.Message & " ERRO ao ler o arquivo: " & OrdemServico.EnderecoArquivo, MsgBoxStyle.Critical, "Atenção")
                            Continue For
                        End Try
                    Next A

                    ''''''''''''''cl_BancoDados.Salvar(SQL)

                    ''''''''''''''SQL = Nothing

                End If
            End If
            MessageBox.Show("Operação finalizada com sucesso, os itens foram inseridos na OS!")
            ProgressBarListaSW.Value = 0
            TimerDGVListaMaterialSW.Enabled = True
            'Else

            '    MsgBox("Operação cancelada, os itens não serão inseridos na OS! Existem RNC em aberto, verificar as linhas marcadas", vbCritical, "Atenção")

            'End If

        End If

        'Else

        '    MsgBox("Não será possivel criar OS existe peças com RNC em aberto!", MsgBoxStyle.Critical, "Atenção")

        'End If

    End Sub

    Private Sub AbrirPastaDaOrdemDeServiçoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirPastaDaOrdemDeServiçoToolStripMenuItem.Click
        Try
            ' Verifica se a célula "Endereco" não está vazia ou nula
            If IsNothing(dgvos.CurrentRow.Cells("Endereco").Value) OrElse IsDBNull(dgvos.CurrentRow.Cells("Endereco").Value) Then
                MsgBox("O endereço não foi informado!", vbExclamation, "Atenção")
                Exit Sub
            End If

            ' Obtém o endereço da célula e tenta abrir o Explorer
            OrdemServico.EnderecoOrdemServico = dgvos.CurrentRow.Cells("Endereco").Value.ToString()
            If Not String.IsNullOrWhiteSpace(OrdemServico.EnderecoOrdemServico) Then
                Process.Start("Explorer", OrdemServico.EnderecoOrdemServico)
            Else
                MsgBox("O endereço está vazio ou não foi informado corretamente!", vbExclamation, "Atenção")
            End If
        Catch ex As Exception
            ' Exibe uma mensagem de erro detalhada
            MsgBox($"Ocorreu um erro ao tentar abrir o endereço: {ex.Message}", vbCritical, "Erro")
        Finally
            ' Código opcional que pode ser executado mesmo após uma exceção
            ' (e.g., limpar variáveis, liberar recursos)
        End Try
    End Sub

    Private Sub LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem.Click

        If Usuario.NomeCompleto = "" Then
            MsgBox("Você não está logado com um usuario valido, não e possivel cria um OS", vbCritical, "Atenção")
            Exit Sub
        End If

        If OrdemServico.Liberado_Engenharia <> "" Then
            MsgBox("A OS já está liberada!", vbCritical, "Atenção")
            Exit Sub
        End If

        cl_BancoDados.VerificaSaldoTag(OrdemServico.idTag)
        Dim totalItens As Integer
        Dim txtacabamento As String
        Dim dxf As String
        Dim espessura As String
        Dim material As String
        Dim Valido As Boolean = True
        Dim txtTipoDesenho As String
        Dim principal As String

        ' totalItens = Convert.ToInt32(cl_BancoDados.RetornaCampoDaPesquisa("Select Max(IdOrdemServico) as IdOrdemServico from ordemservicoitem where idordemservico = '" & OrdemServico.IdOrdemServico & "')", "IdOrdemServico"))

        If DGVListaMaterialSW.Rows.Count <= 0 Then
            MsgBox("Não há itens inseridos da Ordem de Serviço", vbInformation, "Atenção")
            Exit Sub
        ElseIf DGVListaMaterialSW.Rows.Count >= 0 Then
            ProgressBarProcessoLiberacaoOrdemServico.Minimum = 0

            ProgressBarProcessoLiberacaoOrdemServico.Maximum = DGVListaMaterialSW.Rows.Count - 1

            For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

                '''
                 'Verifica se há um produto principal para Ordem de serviço
                Try

                    principal = DGVListaMaterialSW.Rows(i).Cells("ProdutoPrincipal").Value.ToString

                    If principal = "SIM" Then
                        '  principal = DGVListaMaterialSW.Rows(i).Cells("ProdutoPrincipal").Value.ToString
                        ProdutoPrincipal = DGVListaMaterialSW.Rows(i).Cells("CodMatFabricante").Value.ToString

                        Exit For

                    ElseIf principal = "" Then

                        principal = ""
                        ProdutoPrincipal = ""
                        MsgBox("Para gerar a Lista de material e obrigatorio que o produto 'Principal'da Ordem de Serviço já esteja selecionado.", vbInformation, "Atenção")
                        Exit Sub
                    End If
                Catch ex As Exception

                    principal = ""
                    ProdutoPrincipal = ""
                    MsgBox("Para gerar a Lista de material e obrigatorio que o produto 'Principal'da Ordem de Serviço já esteja selecionado.", vbInformation, "Atenção")

                    Exit Sub

                End Try

                Try
                    dxf = Path.ChangeExtension(DGVListaMaterialSW.Rows(i).Cells("EnderecoArquivo").Value.ToString, ".dxf")
                Catch ex As Exception
                    dxf = ""
                End Try

                Try
                    espessura = DGVListaMaterialSW.Rows(i).Cells("espessura").Value.ToString
                Catch ex As Exception
                    espessura = ""
                End Try

                Try
                    material = DGVListaMaterialSW.Rows(i).Cells("materialsw").Value.ToString
                Catch ex As Exception
                    material = ""
                End Try

                If espessura <> "" And material <> "" And dxf <> "" Then
                    ' Verifica se o arquivo DXF existe
                    If File.Exists(dxf) = False Then
                        dgvDataGridBOM.Rows(i).DefaultCellStyle.BackColor = Color.LightSalmon
                        Valido = False
                    End If

                End If

                ProgressBarProcessoLiberacaoOrdemServico.Value = i

            Next

            If Valido = False Then

                MsgBox("Itens marcados em rosa indicam que essas peças deveriam ter o respectivo DFx vinculado ao seu desenho.", vbInformation, "Atenção.")
            End If

        End If
        ProgressBarProcessoLiberacaoOrdemServico.Value = 0

        If My.Settings.chkAcabamentoObrigatorio = "SIM" Then

            For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1
                'Verifica se há acabamento indicado para as peças
                Try
                    txtacabamento = DGVListaMaterialSW.Rows(i).Cells("Acabamento").Value.ToString
                Catch ex As Exception
                    txtacabamento = ""
                End Try

                Try
                    txtTipoDesenho = DGVListaMaterialSW.Rows(i).Cells("txtTipoDesenho").Value.ToString
                Catch ex As Exception
                    txtTipoDesenho = ""
                End Try

                If txtacabamento = "" And txtTipoDesenho <> "Material" Then

                    DGVListaMaterialSW.Rows(i).DefaultCellStyle.BackColor = Color.LightSalmon
                End If

            Next

            MsgBox("Há itens na OS que devem ter seus 'Acabamentos' preenchido, favor verificar os itens destacados!", vbInformation, "Atenção")

            Exit Sub

        End If

        Cursor.Current = Cursors.WaitCursor
        '
        Try

            txtPesqNumeroDesenho.Clear()

            txtPesqTipoDesenho.Clear()

            txtPesqAcabamentoDesenho.Clear()

            '  TimerDGVListaMaterialSW.Enabled = True

            ' Verifica se há uma linha selecionada no DataGridView
            If dgvos.CurrentRow IsNot Nothing Then

                OpcaoLiberacaoOrdemServico.ShowDialog()

                If TipoLiberacaoOrdemServico.ToString() = "Total" Then

                    If OrdemServico.SaldoTag < OrdemServico.Fator Then

                        MsgBox("A operação foi cancelada. O valor informado é maior que o saldo disponível,
                    altere o fator multiplicador e ou solicite ao PCP para inserir saldo na Tag! o Valor Multiplicador é :" & OrdemServico.Fator &
                    " é o saldo da tag é : " & OrdemServico.SaldoTag & "!", vbInformation, "Atenção")

                        Exit Sub

                    End If

                    If OrdemServico.QtdeTag >= OrdemServico.Fator + OrdemServico.QtdeLiberada Then

                        ' Verifica se o usuário clicou em "Cancelar" (Fator será uma string vazia)
                        If OrdemServico.Fator = 0 Then

                            MsgBox("A Operação foi cancelda", vbInformation, "Atenação")

                            Exit Sub ' Sai do procedimento

                        End If

                        ' Verifica se o valor inserido é numérico e maior que 0
                        If Not IsNumeric(OrdemServico.Fator) OrElse Convert.ToDouble(OrdemServico.Fator) <= 0 Then

                            MsgBox("A operação foi cancelada. O valor informado não é um número válido!", vbInformation, "Atenação")

                            Exit Sub
                            'End If

                        End If

                        '						cl_BancoDados.AlteracaoEspecifica("OrdemServico", "Fator", OrdemServico.Fator, "IdOrdemServico", OrdemServico.IdOrdemServico)

                        cl_BancoDados.AlteracaoEspecifica("Tags", "QtdeLiberada", (OrdemServico.QtdeLiberada + OrdemServico.Fator), "IdTag", OrdemServico.idTag)
                        cl_BancoDados.AlteracaoEspecifica("Tags", "SaldoTag", (OrdemServico.SaldoTag - OrdemServico.Fator), "IdTag", OrdemServico.idTag)
                        cl_BancoDados.AlteracaoEspecifica("Ordemservico", "TipoLiberacaoOrdemServico", "Total", "IdOrdemServico", OrdemServico.IdOrdemServico)


                    Else

                        MsgBox("A operação foi cancelada. O valor informado é maior que o saldo disponível!", vbInformation, "Atenação")
                        Exit Sub

                    End If

                ElseIf TipoLiberacaoOrdemServico.ToString() = "Parcial" Then

                    cl_BancoDados.AlteracaoEspecifica("Ordemservico", "TipoLiberacaoOrdemServico", "Parcial", "IdOrdemServico", OrdemServico.IdOrdemServico)

                ElseIf TipoLiberacaoOrdemServico.ToString() = "Sair" Then

                    MsgBox("A operação foi cancelada. O processo de liberação foi cancelada!", vbInformation, "Atenção")

                    Exit Sub

                End If

                ' Verifica e obtém o valor da célula "Estatus"
                If dgvos.CurrentRow.Cells("Estatus") IsNot Nothing AndAlso dgvos.CurrentRow.Cells("Estatus").Value IsNot DBNull.Value Then
                    OrdemServico.Estatus = dgvos.CurrentRow.Cells("Estatus").Value.ToString()
                Else
                    OrdemServico.Estatus = String.Empty ' Valor padrão em caso de ausência
                End If

                ' Verifica e obtém o valor da célula "ENDERECO"
                If dgvos.CurrentRow.Cells("ENDERECO") IsNot Nothing AndAlso dgvos.CurrentRow.Cells("ENDERECO").Value IsNot DBNull.Value Then
                    OrdemServico.EnderecoOrdemServico = dgvos.CurrentRow.Cells("ENDERECO").Value.ToString()
                Else
                    OrdemServico.EnderecoOrdemServico = String.Empty ' Valor padrão em caso de ausência
                End If

                ' Verifica e obtém o valor da célula "Liberado_Engenharia"
                If dgvos.CurrentRow.Cells("Liberado_Engenharia") IsNot Nothing AndAlso dgvos.CurrentRow.Cells("Liberado_Engenharia").Value IsNot DBNull.Value Then
                    OrdemServico.Liberado_Engenharia = dgvos.CurrentRow.Cells("Liberado_Engenharia").Value.ToString()
                Else
                    OrdemServico.Liberado_Engenharia = String.Empty ' Valor padrão em caso de ausência
                End If

                ' Verifica e obtém o valor da célula "Descricao"
                If dgvos.CurrentRow.Cells("Descricao") IsNot Nothing AndAlso dgvos.CurrentRow.Cells("Descricao").Value IsNot DBNull.Value Then
                    OrdemServico.Descricao = dgvos.CurrentRow.Cells("Descricao").Value.ToString()
                Else
                    OrdemServico.Descricao = String.Empty ' Valor padrão em caso de ausência
                End If
            Else
                ' Tratar caso não exista uma linha selecionada
                Throw New Exception("Nenhuma linha selecionada no DataGridView.")
            End If

            If OrdemServico.Liberado_Engenharia = "S" Then

                MsgBox("OS Já liberada, não e possivel liberar novamente!", vbInformation, "Atenção")

                Exit Sub
            Else

                Dim diretorio As String = OrdemServico.EnderecoOrdemServico

                ''Libera pasata para edição
                'cl_BancoDados.PermitirEscritaNaPasta(diretorio & "\PDF")
                'cl_BancoDados.PermitirEscritaNaPasta(diretorio & "\DXF")
                'cl_BancoDados.PermitirEscritaNaPasta(diretorio & "\DFT")

                LimparDiretorio(diretorio & "\PDF")
                LimparDiretorio(diretorio & "\DXF")
                LimparDiretorio(diretorio & "\DFT")
                LimparDiretorio(diretorio & "\LXDS")

                ImportarLXDSParaOS(DGVListaMaterialSW, "DXF", ProgressBarProcessoLiberacaoOrdemServico, "")
                ImportarLXDSParaOS(DGVListaMaterialSW, "PDF", ProgressBarProcessoLiberacaoOrdemServico, "IdOrdemServicoItem")
                ImportarLXDSParaOS(DGVListaMaterialSW, "DFT", ProgressBarProcessoLiberacaoOrdemServico, "")
                ImportarLXDSParaOS(DGVListaMaterialSW, "LXDS", ProgressBarProcessoLiberacaoOrdemServico, "")

                '          cl_BancoDados.Salvar("Update ordemservicoitem set Liberado_Engenharia = 'S',
                'Data_Liberacao_Engenharia = '" & Date.Now & "'
                'where (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') and IdOrdemServico = '" & OrdemServico.IdOrdemServico & "'")
                '          dgvos.CurrentRow.Cells("Liberado_Engenharia").Value = "S"
                '          dgvos.CurrentRow.Cells("Data_Liberacao_Engenharia").Value = Date.Now
                '          dgvos.CurrentRow.Cells("dgvStatus").Value = My.Resources.verificado1
                '          dgvos.Refresh()

                '          cl_BancoDados.Salvar("Update ordemservico set Liberado_Engenharia = 'S',
                'Data_Liberacao_Engenharia = '" & Date.Now & "'
                'where (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') and IdOrdemServico = '" & OrdemServico.IdOrdemServico & "'")
                '          dgvos.CurrentRow.Cells("Liberado_Engenharia").Value = "S"
                '          dgvos.CurrentRow.Cells("Data_Liberacao_Engenharia").Value = Date.Now
                '          dgvos.CurrentRow.Cells("dgvStatus").Value = My.Resources.verificado1
                '          dgvos.Refresh()

                ' inicio teste query unica 25/09/2025
                cl_BancoDados.Salvar("Update ordemservicoitem set Liberado_Engenharia = 'S',
						Data_Liberacao_Engenharia = '" & Date.Now & "'
						where (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') and IdOrdemServico = '" & OrdemServico.IdOrdemServico & "';
                        Update ordemservico set Liberado_Engenharia = 'S',
						Data_Liberacao_Engenharia = '" & Date.Now & "'
						where (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') and IdOrdemServico = '" & OrdemServico.IdOrdemServico & "';")
                dgvos.CurrentRow.Cells("Liberado_Engenharia").Value = "S"
                dgvos.CurrentRow.Cells("Data_Liberacao_Engenharia").Value = Date.Now
                dgvos.CurrentRow.Cells("dgvStatus").Value = My.Resources.verificado1
                ' dgvos.Refresh()

                cl_CalculoBancoDados.CalcularordemservicoitemFatorOS_TAG_PROJETO()

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                cl_BancoDados.VerificaSaldoTag(OrdemServico.idTag)

                Me.lbltxtQtdeTag.Text = OrdemServico.QtdeTag
                Me.lbltxtQtdeLiberada.Text = OrdemServico.QtdeLiberada
                Me.lbltxtSaldoTag.Text = OrdemServico.SaldoTag

                Try

                    If My.Settings.BancoDadosAtivo = "mettapaineis" Then

                        PadraoMetta.ExportarOrdemServicoPadraoMettaAntigo(DGVListaMaterialSW, ProgressBarProcessoLiberacaoOrdemServico, OrdemServico.EnderecoOrdemServico, Me.txtDescricao.Text.Trim.ToUpper, dgvos, True, DGVListaMaterialSW)
                    Else

                        ' TemplatesExcel.ExportarOrdemServicoPadrao(DGVListaMaterialSW, ProgressBarProcessoLiberacaoOrdemServico, OrdemServico.EnderecoOrdemServico, Me.txtDescricao.Text.Trim.ToUpper, dgvos, DGVListaMaterialSW)

                        TemplatesExcel.ExportarOrdemServicoPadrao(DGVListaMaterialSW, ProgressBarProcessoLiberacaoOrdemServico, OrdemServico.EnderecoOrdemServico, Me.txtDescricao.Text.Trim.ToUpper, dgvos, DGVListaMaterialSWMateriais)

                    End If
                Catch ex As Exception
                Finally

                End Try

                If Usuario.EnviarEmailLiberacaoOS <> "" Then

                    Dim resultado As MsgBoxResult = MessageBox.Show("Deseja enviar o e-mail para o PCP, de comunicado de Liberação da Ordem de Serviço: " & OrdemServico.IdOrdemServico, "Liberação", MessageBoxButtons.YesNo)

                    If resultado = DialogResult.Yes Then

                        ClasseEmail.EmailLiberacaoOS()

                    End If

                End If

            End If
        Catch ex As Exception
        Finally

        End Try

        ''''  Email.EnviarEmailComOs01(OrdemServico.EnderecoOrdemServico, OrdemServico.Descricao, Date.Now)
        ' Retornar o cursor ao normal
        Cursor.Current = Cursors.Default

    End Sub

    Private Sub MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem.Click

        For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

            OrdemServico.ProdutoPrincipal = DGVListaMaterialSW.Rows(i).Cells("ProdutoPrincipal").Value.ToString

            If OrdemServico.ProdutoPrincipal <> "" Then

                MsgBox("Esta ordem de serviço já possui um produto principal", vbCritical, "Atenção")

                Exit Sub

            End If

        Next

        Try
            OrdemServico.IDOrdemServicoItem = DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoItem").Value.ToString

            cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "ProdutoPrincipal", "SIM", "IDOrdemServicoItem", OrdemServico.IDOrdemServicoItem)

            DGVListaMaterialSW.CurrentRow.Cells("ProdutoPrincipal").Value = "SIM".ToUpper

            DGVListaMaterialSW.CurrentRow.Cells("dgvIconeItemOS").Value = My.Resources.IconeswPrincipal

            ' DadosArquivoCorrente.NomeArquivoSemExtensao = DGVListaMaterialSW.CurrentRow.Cells("CodMatFabricante").Value.ToString


            cl_BancoDados.RetornaCampoDaPesquisa("Select EnderecoImagem from material where CodMatFabricante = '" & OrdemServico.CodMatFabricante & "'", "EnderecoImagem")

            cl_BancoDados.AlteracaoEspecifica("ordemservico", "EnderecoImagem", VCampo0, "idordemservico", OrdemServico.IdOrdemServico)

            cl_BancoDados.AlteracaoEspecifica("material", "ProdutoPrincipal", "SIM", "CodMatFabricante", OrdemServico.CodMatFabricante)

            cl_BancoDados.AlteracaoEspecifica("ordemservico", "CodDesenhoProduto", OrdemServico.CodMatFabricante, "idordemservico", OrdemServico.IdOrdemServico)

            cl_BancoDados.AlteracaoEspecifica("ordemservico", "DescricaoProduto", OrdemServico.DescResumo & "-" & OrdemServico.DescDetal, "idordemservico", OrdemServico.IdOrdemServico)



            '     '
            '     '  ClasseclOrdemServico.CodDesenhoProduto = dgvTimerProdutos.CurrentRow.Cells("CodDesenhoProduto").Value.ToString()
            '     ' ClasseclOrdemServico.CodMatFabricante = ClasseclOrdemServico.CodDesenhoProduto
            '     ' ClasseclOrdemServico.CodOmie = dgvTimerProdutos.CurrentRow.Cells("CodOmie").Value.ToString()
            '     ClasseclOrdemServico.DescricaoProduto = dgvTimerProdutos.CurrentRow.Cells("DescricaoProduto").Value.ToString()
            '     ClasseclOrdemServico.EnderecoFichaTecnica = dgvTimerProdutos.CurrentRow.Cells("EnderecoFichaTecnica").Value.ToString()
            '     ClasseclOrdemServico.EnderecoIsometrico = dgvTimerProdutos.CurrentRow.Cells("EnderecoIsometrico").Value.ToString()
            '     ClasseclOrdemServico.ProdutoCriadoPor = ClasseUsuario.NomeCompleto
            '     ClasseclOrdemServico.DataCriacaoProduto = Date.Now.Date.ToShortDateString
            '     ClasseclOrdemServico.DataPrevisao = dgvTimerProdutos.CurrentRow.Cells("DataPrevisao").Value.ToString()
            '


        Catch ex As Exception

            OrdemServico.IDOrdemServicoItem = Nothing

            MsgBox("Item da Ordem de Serviço não Valido", vbCritical, "Atenção")

        End Try

    End Sub

    Private Sub DesmarcarComoConjuntoPrincipalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DesmarcarComoConjuntoPrincipalToolStripMenuItem.Click

        Try
            OrdemServico.IDOrdemServicoItem = DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoItem").Value.ToString

            cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "ProdutoPrincipal", "", "IDOrdemServicoItem", OrdemServico.IDOrdemServicoItem)

            cl_BancoDados.AlteracaoEspecifica("material", "ProdutoPrincipal", "", "CodMatFabricante", DadosArquivoCorrente.NomeArquivoSemExtensao)

            DGVListaMaterialSW.CurrentRow.Cells("ProdutoPrincipal").Value = "SIM".ToUpper

            DGVListaMaterialSW.CurrentRow.Cells("dgvIconeItemOS").Value = My.Resources.IcopneMontagemSW
        Catch ex As Exception

            OrdemServico.IDOrdemServicoItem = Nothing

            MsgBox("Item da Ordem de Serviço não Valido", vbCritical, "Atenção")

        End Try

    End Sub

    Private Sub DGVListaMaterialSW_DoubleClick(sender As Object, e As EventArgs) Handles DGVListaMaterialSW.DoubleClick

        Dim ArquivoListaBom As String = DGVListaMaterialSW.CurrentRow.Cells("EnderecoArquivo").Value.ToString

        ' Obtém o caminho completo
        ArquivoListaBom = Path.GetFullPath(ArquivoListaBom)

        ' Verifica se o arquivo existe e o abre
        If File.Exists(ArquivoListaBom) Then
            Process.Start(ArquivoListaBom)

        End If

    End Sub

    Private Function ImportarDXFParaOS(ByVal ObjetoDgv As DataGridView) As Boolean

        ImportarDXFParaOS = False

        Dim Origem, Destino, Prefixo, MaterialSW, QtdeTotal, Espessura As String
        Dim caminhoArquivoDestino As String

        Destino = dgvos.CurrentRow.Cells("Endereco").Value.ToString()
        Destino = Path.Combine(Destino, "DXF") ' Usar Path.Combine para melhor manipulação de caminhos

        For i As Integer = 0 To ObjetoDgv.Rows.Count - 1 ' Ajustado para evitar erro de índice

            Try

                ' Acessando os valores diretamente da linha da tabela temporária
                Origem = ObjetoDgv.Rows(i).Cells("EnderecoArquivo").Value.ToString()

                ' Substituindo extensões de arquivos .SLDPRT e .SLDASM para .DXF, sem comparação de maiúsculas/minúsculas
                Origem = Replace(Origem, ".SLDPRT", ".DXF")
                Origem = Replace(Origem, ".SLDASM", ".DXF")

                ' Tentar obter MaterialSW, QtdeTotal e Espessura, com tratamento de exceção
                Try
                    MaterialSW = ObjetoDgv.Rows(i).Cells("material").Value.ToString
                Catch ex As Exception
                    MaterialSW = "Sem material"
                End Try

                Try
                    QtdeTotal = ObjetoDgv.Rows(i).Cells("QtdeTotal").Value.ToString
                Catch ex As Exception
                    QtdeTotal = "Sem Quantidade"
                End Try

                Try
                    Espessura = ObjetoDgv.Rows(i).Cells("Espessura").Value.ToString
                Catch ex As Exception
                    Espessura = "Sem Espessura"
                End Try

                Prefixo = Espessura & " - " & MaterialSW & " - " & QtdeTotal & " - "

                ' Verifica se o arquivo de origem existe
                Origem = Path.GetFullPath(Origem)

                If File.Exists(Origem) Then
                    Try
                        ' Obtém o nome do arquivo sem a extensão
                        Dim nomeArquivoSemExtensao As String = Path.GetFileNameWithoutExtension(Origem)
                        ' Obtém a extensão do arquivo
                        Dim extensaoArquivo As String = Path.GetExtension(Origem)

                        Dim novoNomeArquivo As String

                        ' Verifica qual formato de exportação foi selecionado
                        If My.Settings.ParametroExportarDXF = "1" Then
                            novoNomeArquivo = $"{Espessura} - {MaterialSW} - {QtdeTotal} - {nomeArquivoSemExtensao}{extensaoArquivo}"
                        ElseIf My.Settings.ParametroExportarDXF = "2" Then
                            novoNomeArquivo = $"{QtdeTotal} - {nomeArquivoSemExtensao} - {MaterialSW} - {Espessura}{extensaoArquivo}"
                        Else
                            MessageBox.Show("Nenhuma opção de exportação de DXF selecionada. Vá nas configurações e selecione a opção desejada!")
                            Exit For ' Saída antecipada se não houver configuração válida
                        End If

                        ' Verificar se o item é de estoque
                        If ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = "NÃO" OrElse
                            ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = "" OrElse
                             ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = Nothing Then
                            ' Caminho para não itens de estoque
                            caminhoArquivoDestino = Path.Combine(Destino, novoNomeArquivo)
                        ElseIf ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = "SIM" Then
                            ' Caminho para itens de estoque
                            caminhoArquivoDestino = Replace(Destino, "\DXF", "\PEÇAS DE ESTOQUE")
                            caminhoArquivoDestino = Path.Combine(caminhoArquivoDestino, novoNomeArquivo)
                        End If

                        '' Copia o arquivo para a pasta de destino com o novo nome
                        '' Verifica se o arquivo de origem existe
                        'Origem = Path.GetFullPath(Origem)
                        '' Verifica se o arquivo de origem existe
                        'caminhoArquivoDestino = Path.GetFullPath(caminhoArquivoDestino)

                        'File.Copy(Origem, caminhoArquivoDestino, True)
                        cl_BancoDados.CopiarArquivoInteligente(Origem, caminhoArquivoDestino)
                    Catch ex As Exception
                    Finally
                        ' MessageBox.Show($"Erro ao copiar o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    ' Else
                    '    MessageBox.Show($"Arquivo de origem não encontrado: {Origem}", "Arquivo Não Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    ImportarDXFParaOS = True

                End If
            Catch ex As Exception
                Continue For

            End Try

        Next

    End Function

    Private Function ImportarPDFParaOS(ByVal ObjetoDgv As DataGridView) As Boolean

        ImportarPDFParaOS = False

        Dim Origem, Destino, Prefixo, QtdeTotal, NumeroOS As String
        Dim caminhoArquivoDestino, PastaDestino As String

        'Destino = dgvos.CurrentRow.Cells("Endereco").Value.ToString()
        'Destino = Path.Combine(Destino, "PDF") ' Usar Path.Combine para melhor manipulação de caminhos

        ' Obtém o caminho do arquivo
        Destino = dgvos.CurrentRow.Cells("Endereco").Value.ToString().ToUpper().Trim()

        ' Altera a extensão do arquivo para .pdf
        Destino = Path.ChangeExtension(Destino, "\PDF")

        caminhoArquivoDestino = dgvos.CurrentRow.Cells("Endereco").Value & "\PDF"

        NumeroOS = dgvos.CurrentRow.Cells("IdOrdemServico").Value.ToString().ToUpper().Trim()

        ' PastaDestino = dgvos.CurrentRow.Cells("Endereco").Value & "\PDF"

        For i As Integer = 0 To ObjetoDgv.Rows.Count - 1 ' Ajustado para evitar erro de índice

            Try

                ' Acessando os valores diretamente da linha
                Origem = ObjetoDgv.Rows(i).Cells("EnderecoArquivo").Value.ToString().ToUpper().Trim()

                Origem = Replace(Origem, ".SLDPRT", ".PDF", , , CompareMethod.Text)
                Origem = Replace(Origem, ".SLDASM", ".PDF", , , CompareMethod.Text)

                ' Tentar obter QtdeTotal com tratamento de exceção
                Try
                    QtdeTotal = ObjetoDgv.Rows(i).Cells("QtdeTotal").Value.ToString()
                Catch ex As Exception

                    QtdeTotal = "Sem Quantidade"

                End Try

                Prefixo = "OS - " & NumeroOS & " - " & QtdeTotal & " - "

                ' Verifica se o arquivo de origem existe

                Origem = Path.GetFullPath(Origem)

                If File.Exists(Origem) Then
                    Try
                        ' Obtém o nome do arquivo sem a extensão
                        Dim nomeArquivoSemExtensao As String = Path.GetFileNameWithoutExtension(Origem)

                        ' Obtém a extensão do arquivo
                        Dim extensaoArquivo As String = Path.GetExtension(Origem)

                        ' Constrói o novo nome do arquivo com o sufixo
                        Dim novoNomeArquivo As String = $"{Prefixo}{nomeArquivoSemExtensao}{extensaoArquivo}"

                        ' Verificar se o item é de estoque
                        If ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = "NÃO" OrElse
                            ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = "" OrElse
                             ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = Nothing Then
                            ' Caminho para não itens de estoque
                            caminhoArquivoDestino = Path.Combine(Destino, novoNomeArquivo)
                        ElseIf ObjetoDgv.Rows(i).Cells("txtItemEstoque").Value.ToString() = "SIM" Then
                            ' Caminho para itens de estoque
                            caminhoArquivoDestino = Replace(Destino, "\PDF", "\PEÇAS DE ESTOQUE")
                            caminhoArquivoDestino = Path.Combine(caminhoArquivoDestino, novoNomeArquivo)
                        End If

                        '' Copia o arquivo para a pasta de destino com o novo nome
                        'Origem = Path.GetFullPath(Origem)
                        'caminhoArquivoDestino = Path.GetFullPath(caminhoArquivoDestino)

                        'File.Copy(Origem, caminhoArquivoDestino, True)
                        cl_BancoDados.CopiarArquivoInteligente(Origem, caminhoArquivoDestino)
                    Catch ex As Exception
                    Finally

                        ' MessageBox.Show($"Erro ao copiar o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                End If
            Catch ex As Exception

                Continue For

            End Try

        Next

    End Function

    Private Function ImportarPDFParaOSIndividual(ByVal Endereco As String, ByVal qtde As String) As Boolean

        ImportarPDFParaOSIndividual = False

        Dim Origem, Destino, Prefixo, QtdeTotal, OS, TextoPdf As String
        Dim caminhoArquivoDestino As String

        Destino = dgvos.CurrentRow.Cells("Endereco").Value.ToString()
        Destino = Path.Combine(Destino, "PDF") ' Usar Path.Combine para melhor manipulação de caminhos

        OS = dgvos.CurrentRow.Cells("IdOrdemServico").Value.ToString()

        Try

            ' Acessando os valores diretamente da linha
            Origem = Endereco

            Origem = Replace(Origem, ".SLDPRT", ".PDF", , , CompareMethod.Text)
            Origem = Replace(Origem, ".SLDASM", ".PDF", , , CompareMethod.Text)

            ' Tentar obter QtdeTotal com tratamento de exceção
            Try
                QtdeTotal = qtde
            Catch ex As Exception
                QtdeTotal = "Sem Quantidade"
            End Try

            Prefixo = $"{QtdeTotal} - "
            TextoPdf = "OS: " & OS & " qtde: " & qtde

            ' Copia o arquivo para a pasta de destino com o novo nome
            Origem = Path.GetFullPath(Origem)
            Destino = Path.GetFullPath(Destino)

            pdfsinco.EscreverPdf(Origem, Destino, TextoPdf)

            ' Verifica se o arquivo de origem existe
            If File.Exists(Origem) Then
                Try
                    ' Obtém o nome do arquivo sem a extensão
                    Dim nomeArquivoSemExtensao As String = Path.GetFileNameWithoutExtension(Origem)

                    ' Obtém a extensão do arquivo
                    Dim extensaoArquivo As String = Path.GetExtension(Origem)

                    ' Constrói o novo nome do arquivo com o sufixo
                    Dim novoNomeArquivo As String = $"{Prefixo}{nomeArquivoSemExtensao}{extensaoArquivo}"

                    caminhoArquivoDestino = Path.Combine(Destino, novoNomeArquivo)

                    'Origem = Path.GetFullPath(Origem)
                    'caminhoArquivoDestino = Path.GetFullPath(caminhoArquivoDestino)

                    'File.Copy(Origem, caminhoArquivoDestino, True)
                    cl_BancoDados.CopiarArquivoInteligente(Origem, caminhoArquivoDestino)
                Catch ex As Exception
                Finally

                    ' MessageBox.Show($"Erro ao copiar o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                'Else
                '    MessageBox.Show($"O arquivo de origem não existe: {Origem}", "Arquivo Não Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            End If
        Catch ex As Exception
        Finally

        End Try

    End Function

    Private Function ImportarDFTParaOS(ByVal ObjetoDgv As DataGridView) As Boolean

        Dim Origem, Destino, Prefixo, MaterialSW, QtdeTotal, Espessura As String
        Dim caminhoArquivoDestino As String

        Destino = dgvos.CurrentRow.Cells("Endereco").Value.ToString
        Destino = Path.Combine(Destino, "DFT")

        For i As Integer = 0 To ObjetoDgv.Rows.Count - 1

            ' Acessando os valores diretamente da linha
            Origem = ObjetoDgv.Rows(i).Cells("EnderecoArquivo").ToString
            Origem = Replace(Origem, ".SLDPRT", ".DFT", , , CompareMethod.Text)
            Origem = Replace(Origem, ".SLDASM", ".DFT", , , CompareMethod.Text)

            Try
                MaterialSW = ObjetoDgv.Rows(i).Cells("material").ToString
            Catch ex As Exception
                MaterialSW = "Sem material"
            End Try

            Try
                QtdeTotal = ObjetoDgv.Rows(i).Cells("QtdeTotal").ToString
            Catch ex As Exception
                QtdeTotal = "Sem Quantidade"
            End Try

            Try
                Espessura = ObjetoDgv.Rows(i).Cells("Espessura").ToString
            Catch ex As Exception
                Espessura = "Sem Espessura"
            End Try

            Prefixo = Espessura & " - " & MaterialSW & " - " & QtdeTotal & " - "

            Origem = Path.GetFullPath(Origem)
            caminhoArquivoDestino = Path.GetFullPath(caminhoArquivoDestino)

            If File.Exists(Origem) Then
                Try
                    ' Obtém o nome do arquivo sem a extensão
                    Dim nomeArquivoSemExtensao As String = Path.GetFileNameWithoutExtension(Origem)

                    ' Obtém a extensão do arquivo
                    Dim extensaoArquivo As String = Path.GetExtension(Origem)

                    Dim novoNomeArquivo As String

                    ' Verifica qual formato de exportação foi selecionado
                    If My.Settings.ParametroExportarDXF = "1" Then
                        novoNomeArquivo = $"{Espessura} - {MaterialSW} - {QtdeTotal} - {nomeArquivoSemExtensao}{extensaoArquivo}"
                    ElseIf My.Settings.ParametroExportarDXF = "2" Then
                        novoNomeArquivo = $"{QtdeTotal} - {nomeArquivoSemExtensao} - {MaterialSW} - {Espessura}{extensaoArquivo}"
                    Else
                        MessageBox.Show("Nenhuma opção de exportação de DFT selecionada. Vá nas configurações e selecione a opção desejada!")
                        Exit For ' Saída antecipada se não houver configuração válida
                    End If

                    ' Verificar se o item é de estoque
                    If ObjetoDgv.Rows(i).Cells("txtItemEstoque").ToString() = "NÃO" OrElse
                            ObjetoDgv.Rows(i).Cells("txtItemEstoque").ToString() = "" OrElse
                             ObjetoDgv.Rows(i).Cells("txtItemEstoque").ToString() = Nothing Then
                        ' Caminho para não itens de estoque
                        caminhoArquivoDestino = Path.Combine(Destino, novoNomeArquivo)
                    ElseIf ObjetoDgv.Rows(i).Cells("txtItemEstoque").ToString() = "SIM" Then
                        ' Caminho para itens de estoque
                        caminhoArquivoDestino = Replace(Destino, "\DFT", "\PEÇAS DE ESTOQUE")
                        caminhoArquivoDestino = Path.Combine(caminhoArquivoDestino, novoNomeArquivo)
                    End If

                    'Origem = Path.GetFullPath(Origem)
                    'caminhoArquivoDestino = Path.GetFullPath(caminhoArquivoDestino)

                    'File.Copy(Origem, caminhoArquivoDestino, True)
                    cl_BancoDados.CopiarArquivoInteligente(Origem, caminhoArquivoDestino)

                    ' MessageBox.Show("Arquivo copiado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception

                End Try

            End If

        Next

    End Function

    Private Function ImportarLXDSParaOS(ByVal ObjetoDgv As DataGridView, ByVal Pasta As String, ByVal BarraProgresso As ToolStripProgressBar, ByVal NomeidColuna As String)

        Dim Origem, Destino, Prefixo, MaterialSW, QtdeTotal, Espessura, tipoDesenho, Acabamento As String
        Dim caminhoArquivoDestino As String

        BarraProgresso.Minimum = 0
        BarraProgresso.Maximum = ObjetoDgv.Rows.Count - 1

        Destino = dgvos.CurrentRow.Cells("Endereco").Value.ToString
        Destino = Path.Combine(Destino, Pasta)

        For i As Integer = 0 To ObjetoDgv.Rows.Count - 1

            Try

                Try
                    ' Acessando os valores diretamente da linha
                    Origem = ObjetoDgv.Rows(i).Cells("EnderecoArquivo").Value.ToString
                    Origem = Replace(Origem, ".SLDPRT", "." & Pasta, , , CompareMethod.Text)
                    Origem = Replace(Origem, ".SLDASM", "." & Pasta, , , CompareMethod.Text)
                Catch ex As Exception
                    Origem = ""
                Finally
                End Try

                Try
                    MaterialSW = ObjetoDgv.Rows(i).Cells("MaterialSW").Value.ToString
                Catch ex As Exception
                    MaterialSW = "Sem material"
                Finally
                End Try

                Try
                    QtdeTotal = ObjetoDgv.Rows(i).Cells("QtdeTotal").Value.ToString
                Catch ex As Exception
                    QtdeTotal = "Sem Quantidade"
                Finally
                End Try

                Try
                    Espessura = ObjetoDgv.Rows(i).Cells("Espessura").Value.ToString
                Catch ex As Exception
                    Espessura = "Sem Espessura"
                Finally
                End Try

                Try
                    tipoDesenho = ObjetoDgv.Rows(i).Cells("txtTipoDesenho").Value.ToString
                Catch ex As Exception
                    tipoDesenho = "Sem Tipo Desenho"
                Finally
                End Try

                Try
                    Acabamento = ObjetoDgv.Rows(i).Cells("Acabamento").Value.ToString
                Catch ex As Exception
                    Acabamento = "Sem Acabamento"
                Finally
                End Try

                Prefixo = Espessura & " - " & MaterialSW & " - " & QtdeTotal & " - "

                ' Verifica se o arquivo de origem existe
                If File.Exists(Origem) Then

                    ' Obtém o nome do arquivo sem a extensão
                    Dim nomeArquivoSemExtensao As String = Path.GetFileNameWithoutExtension(Origem)

                    ' Obtém a extensão do arquivo
                    Dim extensaoArquivo As String = Path.GetExtension(Origem)

                    Dim novoNomeArquivo As String

                    Dim invalidChars = Path.GetInvalidFileNameChars()
                    Espessura = String.Concat(Espessura.Where(Function(c) Not invalidChars.Contains(c)))

                    ' Dim invalidChars = Path.GetInvalidFileNameChars()
                    MaterialSW = String.Concat(MaterialSW.Where(Function(c) Not invalidChars.Contains(c)))

                    'Dim invalidChars = Path.GetInvalidFileNameChars()
                    QtdeTotal = String.Concat(QtdeTotal.Where(Function(c) Not invalidChars.Contains(c)))

                    ' Dim invalidChars = Path.GetInvalidFileNameChars()
                    nomeArquivoSemExtensao = String.Concat(nomeArquivoSemExtensao.Where(Function(c) Not invalidChars.Contains(c)))

                    ' Dim invalidChars = Path.GetInvalidFileNameChars()
                    extensaoArquivo = String.Concat(extensaoArquivo.Where(Function(c) Not invalidChars.Contains(c)))

                    ' Verifica qual formato de exportação foi selecionado
                    If My.Settings.ParametroExportarDXF = "1" Then
                        novoNomeArquivo = $"OS_{OrdemServico.IdOrdemServico} - {Espessura} - {MaterialSW} - {QtdeTotal} - {nomeArquivoSemExtensao}{extensaoArquivo}"
                    ElseIf My.Settings.ParametroExportarDXF = "2" Then
                        novoNomeArquivo = $"OS_{OrdemServico.IdOrdemServico} -{QtdeTotal} - {nomeArquivoSemExtensao} - {MaterialSW} - {Espessura}{extensaoArquivo}"
                    Else
                        MessageBox.Show("Nenhuma opção de exportação de LXDS selecionada. Vá nas configurações e selecione a opção desejada!")
                        Exit For ' Saída antecipada se não houver configuração válida
                    End If

                    If My.Settings.BancoDadosAtivo = "mettapaineis" Then

                        If ObjetoDgv.Rows(i).Cells("EnderecoArquivo").Value.ToString.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                            novoNomeArquivo = $"CPF_{OrdemServico.IdOrdemServico} - {tipoDesenho} - {QtdeTotal} - {nomeArquivoSemExtensao}{extensaoArquivo}"
                        End If
                    Else

                        If ObjetoDgv.Rows(i).Cells("EnderecoArquivo").Value.ToString.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                            novoNomeArquivo = $"OS_{OrdemServico.IdOrdemServico} - {tipoDesenho} - {QtdeTotal} - {nomeArquivoSemExtensao}{extensaoArquivo}"
                        End If

                    End If

                    caminhoArquivoDestino = Path.Combine(Destino, novoNomeArquivo)

                    If Pasta = "PDF" Then

                        Dim id As String

                        If NomeidColuna <> "" Then

                            id = ObjetoDgv.Rows(i).Cells(NomeidColuna).Value

                        End If

                        If Not {"amceletrica", "MP12OFICIAL"}.Contains(My.Settings.BancoDadosAtivo.ToString) Then

                            If My.Settings.BancoDadosAtivo = "mettapaineis" Then

                                EditarPDFParaOs(Origem, caminhoArquivoDestino, novoNomeArquivo, QtdeTotal, OrdemServico.IdOrdemServico, Acabamento, "CPF")
                            Else
                                EditarPDFParaOs(Origem, caminhoArquivoDestino, novoNomeArquivo, QtdeTotal, OrdemServico.IdOrdemServico, Acabamento, "OS")

                            End If
                        Else

                            EditarPDFParaOsFormatoLynx(Origem, caminhoArquivoDestino, novoNomeArquivo, QtdeTotal, OrdemServico.IdOrdemServico, Acabamento, "OS")

                        End If

                        cl_BancoDados.AlteracaoEspecifica("OrdemServicoItem", "EnderecoArquivoItemOrdemServico", caminhoArquivoDestino, "IdOrdemServicoItem", id)

                        ObjetoDgv.Rows(i).Cells("EnderecoArquivoItemOrdemServico").Value = caminhoArquivoDestino
                    Else

                        '' Copia o arquivo para o destino diretamente se não for PDF
                        'Origem = Path.GetFullPath(Origem)
                        'caminhoArquivoDestino = Path.GetFullPath(caminhoArquivoDestino)

                        'File.Copy(Origem, caminhoArquivoDestino, True)

                        cl_BancoDados.CopiarArquivoInteligente(Origem, caminhoArquivoDestino)

                    End If

                End If

                Origem = ""
                Prefixo = ""
                MaterialSW = ""
                QtdeTotal = ""
                Espessura = ""
                tipoDesenho = ""
            Catch ex As Exception
                Continue For
            End Try

            BarraProgresso.Value = i

        Next

        BarraProgresso.Value = 0

    End Function

    Public Sub EditarPDFParaOs(Origem As String, caminhoArquivoDestino As String, novoNomeArquivo As String, QtdeTotal As String, Identificado As String, Acabamento As String, TipoIdentidicado As String)

        Origem = System.Text.RegularExpressions.Regex.Replace(Origem, ".sldprt$|.sldasm$", ".pdf", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        ' Verifica se o arquivo de entrada existe
        If System.IO.File.Exists(Origem) Then
            Try
                ' Abre o PDF de entrada
                Dim pdfReader As New PdfReader(Origem)
                ' Cria um escritor para o novo arquivo PDF
                Dim pdfWriter As New PdfWriter(caminhoArquivoDestino)
                ' Abre o documento PDF para edição
                Dim pdfDocument As New PdfDocument(pdfReader, pdfWriter)

                ' Acessa a primeira página do PDF
                Dim page As PdfPage = pdfDocument.GetPage(1)
                ' Cria um Canvas para desenhar na página
                Dim canvas As New PdfCanvas(page)

                ' Define a posição para inserir o texto no canto inferior direito
                Dim pageWidth As Single = pdfDocument.GetDefaultPageSize().GetWidth() ' Largura da página
                Dim marginRight As Single = 10 ' Margem direita
                Dim posX As Single = pageWidth - marginRight ' Posição X no canto direito
                Dim posY As Single = 1 ' Posição Y no rodapé

                canvas.BeginText()
                canvas.SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(), 12)
                canvas.SetTextMatrix(posX, posY) ' Define a posição inicial do texto
                canvas.ShowText(TipoIdentidicado & " " & Identificado & " - Qtde. " & QtdeTotal & " - Acab. " & Acabamento & " - Emissão " & Date.Now).ToString()
                canvas.EndText()

                ' Fecha o documento PDF
                pdfDocument.Close()

                ' Pode adicionar um log ou mensagem de sucesso se necessário
                ' Console.WriteLine("Texto inserido com sucesso no arquivo: " & outputPdf)
            Catch ex As Exception
                ' Em caso de erro, exibe uma mensagem
                '  Console.WriteLine("Erro ao processar o PDF: " & ex.Message)

                'caminhoArquivoDestino = Path.GetFullPath(caminhoArquivoDestino)
                'File.Delete(caminhoArquivoDestino)

                'Origem = Path.GetFullPath(Origem)
                'caminhoArquivoDestino = Path.GetFullPath(caminhoArquivoDestino)

                'File.Copy(Origem, caminhoArquivoDestino, True)

                cl_BancoDados.CopiarArquivoInteligente(Origem, caminhoArquivoDestino)
            Finally

            End Try

        End If

    End Sub

    Public Sub EditarPDFParaOsFormatoLynx(Origem As String, caminhoArquivoDestino As String, novoNomeArquivo As String, QtdeTotal As String, Identificado As String, Acabamento As String, TipoIdentidicado As String)

        Origem = System.Text.RegularExpressions.Regex.Replace(Origem, ".sldprt$|.sldasm$", ".pdf", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        Origem = Path.GetFullPath(Origem)

        If System.IO.File.Exists(Origem) Then
            Try
                Dim pdfReader As New PdfReader(Origem)
                Dim pdfWriter As New PdfWriter(caminhoArquivoDestino)
                Dim pdfDocument As New PdfDocument(pdfReader, pdfWriter)
                Dim page As PdfPage = pdfDocument.GetPage(1)
                Dim canvas As New PdfCanvas(page)

                ' Tamanho da página
                Dim largura As Single = pdfDocument.GetDefaultPageSize().GetWidth()
                Dim altura As Single = pdfDocument.GetDefaultPageSize().GetHeight()

                ' Fonte padrão
                Dim fonte = iText.Kernel.Font.PdfFontFactory.CreateFont()

                canvas.BeginText()
                canvas.SetFontAndSize(fonte, 8)

                ' 🧾 5. Data de emissão no canto inferior direito
                canvas.SetTextMatrix(900, 15)
                canvas.ShowText(Date.Now.Date)

                ' 🧾 3. Qtde no meio da página
                canvas.SetTextMatrix(972, 15)
                canvas.ShowText(QtdeTotal.ToString)

                ' 🧾 2. Identificado no topo direito
                canvas.SetTextMatrix(1022, 15)
                canvas.ShowText(Identificado.ToString)

                ' 🧾 4. Acabamento no canto inferior esquerdo
                canvas.SetTextMatrix(1085, 15)
                canvas.ShowText(Acabamento.ToString)

                ' 🧾 1. TipoIdentificado no topo esquerdo
                canvas.SetTextMatrix(1200, 15)
                canvas.ShowText(TipoIdentidicado.ToString)

                canvas.EndText()

                pdfDocument.Close()
            Catch ex As Exception

                'Origem = Path.GetFullPath(Origem)
                'caminhoArquivoDestino = Path.GetFullPath(caminhoArquivoDestino)

                'File.Delete(caminhoArquivoDestino)
                'File.Copy(Origem, caminhoArquivoDestino, True)

                cl_BancoDados.CopiarArquivoInteligente(Origem, caminhoArquivoDestino)

            End Try
        End If

    End Sub

    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <returns></returns>

    ' Função que verifica se o arquivo está em uso
    Private Function IsFileInUse(filePath As String) As Boolean
        Try
            ' Tenta abrir o arquivo com exclusividade
            Using fs As New FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
                Return False ' O arquivo não está em uso
            End Using
        Catch ex As IOException
            ' O arquivo está em uso, retorna True
            Return True
        End Try
    End Function

    ' Função para tentar fechar o arquivo, liberando-o para uso
    Private Sub CloseFile(filePath As String)
        Try
            ' Tenta fechar o arquivo, liberando-o para uso
            Using fs As New FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
                fs.Close() ' Fecha explicitamente o arquivo
            End Using
            Console.WriteLine("Arquivo fechado com sucesso.")
        Catch ex As Exception
            ' Caso haja falha ao tentar fechar o arquivo
            Console.WriteLine("Erro ao tentar fechar o arquivo: " & ex.Message)
        End Try
    End Sub

    Private Function ConsolidarMateriais(ByVal dgv As DataGridView) As System.Data.DataTable
        ' Cria um novo DataTable para armazenar os dados consolidados

        dtConsolidado.Columns.Clear()
        dtConsolidado.Rows.Clear()

        dtConsolidado.Columns.Add("IdMaterial")
        dtConsolidado.Columns.Add("QtdeTotal") ' Ou use Decimal se for um valor decimal
        dtConsolidado.Columns.Add("EnderecoArquivo") ' Adicione outras colunas conforme necessário
        dtConsolidado.Columns.Add("material")
        dtConsolidado.Columns.Add("espessura")
        dtConsolidado.Columns.Add("txtItemEstoque")

        ' Dicionário para armazenar a soma de QtdeTotal por IdMaterial
        Dim materialDictionary As New Dictionary(Of String, DataRow)()

        ' Itera pelas linhas do DataGridView
        For Each row As DataGridViewRow In dgv.Rows
            If Not row.IsNewRow Then
                Dim IdMaterial As String = row.Cells("IdMaterial").Value.ToString()
                Dim qtdeTotal As Double = Convert.ToInt32(row.Cells("QtdeTotal").Value)
                Dim EnderecoArquivo As String = row.Cells("EnderecoArquivo").Value.ToString()
                Dim material As String = row.Cells("material").Value.ToString()
                Dim espessura As String = row.Cells("espessura").Value.ToString()
                Dim txtItemEstoque As String = row.Cells("txtItemEstoque").Value.ToString()

                If materialDictionary.ContainsKey(IdMaterial) Then
                    ' Se o material já existe, soma a quantidade
                    materialDictionary(IdMaterial)("QtdeTotal") += qtdeTotal
                Else
                    ' Se não existe, cria uma nova linha no DataTable
                    Dim newRow As DataRow = dtConsolidado.NewRow()
                    newRow("IdMaterial") = IdMaterial
                    newRow("QtdeTotal") = qtdeTotal
                    newRow("EnderecoArquivo") = EnderecoArquivo ' Adicione outros campos conforme necessário
                    newRow("material") = material
                    newRow("espessura") = espessura
                    newRow("txtItemEstoque") = txtItemEstoque

                    dtConsolidado.Rows.Add(newRow)
                    materialDictionary.Add(IdMaterial, newRow)

                End If
            End If

        Next

        Return dtConsolidado

    End Function

    Private Sub btnAplicarAcabamento_Click(sender As Object, e As EventArgs) Handles btnAplicarAcabamento.Click

        If DGVListaMaterialSW.Rows.Count > 0 Then

            For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

                If Convert.ToBoolean(DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value) = True Then

                    Try

                        cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Acabamento", cboOpcoesAcabamento.Text.ToUpper.Trim, "IDOrdemServicoItem", DGVListaMaterialSW.Rows(i).Cells("IDOrdemServicoItem").Value.ToString)

                        DGVListaMaterialSW.Rows(i).Cells("Acabamento").Value = cboOpcoesAcabamento.Text.ToUpper.Trim

                        DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False
                    Catch ex As Exception
                        '  MsgBox(ex.Message)
                    Finally
                    End Try
                End If

            Next
        End If

    End Sub

    Private Sub MarcarTodosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MarcarTodosToolStripMenuItem.Click

        For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

            If DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False Then

                DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = True

            End If

        Next

    End Sub

    Private Sub DesmarcarTodosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DesmarcarTodosToolStripMenuItem.Click

        For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

            If DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = True Then

                DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False

            End If

        Next

    End Sub

    Private Sub InverterSeleçãoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InverterSeleçãoToolStripMenuItem.Click

        For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

            If DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False Then

                DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = True

            ElseIf DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = True Then

                DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False

            End If

        Next

    End Sub

    Private Sub DGVListaMaterialSW_Click(sender As Object, e As EventArgs) Handles DGVListaMaterialSW.Click
        Try

            If DGVListaMaterialSW.CurrentRow.Cells("dgvSelecao").Value = False Then

                DGVListaMaterialSW.CurrentRow.Cells("dgvSelecao").Value = True

            ElseIf DGVListaMaterialSW.CurrentRow.Cells("dgvSelecao").Value = True Then

                DGVListaMaterialSW.CurrentRow.Cells("dgvSelecao").Value = False

            End If

            OrdemServico.CodMatFabricante = DGVListaMaterialSW.CurrentRow.Cells("CodMatFabricante").Value.ToString

            OrdemServico.DescDetal = DGVListaMaterialSW.CurrentRow.Cells("DescDetal").Value.ToString
            OrdemServico.DescResumo = DGVListaMaterialSW.CurrentRow.Cells("DescResumo").Value.ToString




        Catch ex As Exception
        Finally

        End Try

    End Sub

    Private Sub txtPesqNumeroDesenho_TextChanged(sender As Object, e As EventArgs) Handles txtPesqNumeroDesenho.TextChanged
        TimerDGVListaMaterialSW.Enabled = True
    End Sub

    Private Sub txtPesqTipoDesenho_TextChanged(sender As Object, e As EventArgs) Handles txtPesqTipoDesenho.TextChanged
        TimerDGVListaMaterialSW.Enabled = True
    End Sub

    Private Sub txtPesqAcabamentoDesenho_TextChanged(sender As Object, e As EventArgs) Handles txtPesqAcabamentoDesenho.TextChanged
        TimerDGVListaMaterialSW.Enabled = True
    End Sub

    Private Sub AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem.Click

        Try

            If OrdemServico.Liberado_Engenharia <> "" Then

                MsgBox("Ordem de Serviço já Liberada para Produção, não pode mais ser modificada!", vbCritical, "Atenção")

                Exit Sub
            Else

                Dim novaqtde As Double

                novaqtde = InputBox("Informe a Nova Quantidade", "Alteração de Quantidade", DGVListaMaterialSW.CurrentRow.Cells("QtdeTotal").Value.ToString)

                If IsNumeric(novaqtde) Then

                    Dim Peso, AreaPintura As String

                    Peso = (DGVListaMaterialSW.CurrentRow.Cells("Peso").Value / DGVListaMaterialSW.CurrentRow.Cells("QtdeTotal").Value) * novaqtde

                    AreaPintura = (DGVListaMaterialSW.CurrentRow.Cells("AreaPintura").Value / DGVListaMaterialSW.CurrentRow.Cells("QtdeTotal").Value) * novaqtde

                    cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "QtdeTotal", novaqtde, "IDOrdemServicoItem", DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoItem").Value.ToString)
                    cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "qtde", novaqtde, "IDOrdemServicoItem", DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoItem").Value.ToString)

                    cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "AreaPintura", AreaPintura, "IDOrdemServicoItem", DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoItem").Value.ToString)

                    cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Peso", Peso, "IDOrdemServicoItem", DGVListaMaterialSW.CurrentRow.Cells("IDOrdemServicoItem").Value.ToString)

                    DGVListaMaterialSW.CurrentRow.Cells("QtdeTotal").Value = novaqtde
                    DGVListaMaterialSW.CurrentRow.Cells("qtde").Value = novaqtde

                    DGVListaMaterialSW.CurrentRow.Cells("AreaPintura").Value = AreaPintura

                    DGVListaMaterialSW.CurrentRow.Cells("Peso").Value = Peso

                    DGVListaMaterialSW.CurrentRow.Cells("QtdeTotal").Style.BackColor = Color.LightGreen

                    DGVListaMaterialSW.CurrentRow.Cells("qtde").Style.BackColor = Color.LightGreen

                    DGVListaMaterialSW.CurrentRow.Cells("AreaPintura").Style.BackColor = Color.LightGreen

                    DGVListaMaterialSW.CurrentRow.Cells("Peso").Style.BackColor = Color.LightGreen
                Else

                    MsgBox("O valor informado não e um numero Valido")

                End If

            End If
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub ImprimirDesenhoPDFSelecionadoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImprimirDesenhoPDFSelecionadoToolStripMenuItem.Click

        For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

            If DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = True Then

                Try

                    Dim ArquivoPdf As String = DGVListaMaterialSW.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

                    ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
                    ArquivoPdf = Path.ChangeExtension(ArquivoPdf, ".PDF")

                    If File.Exists(ArquivoPdf) Then

                        Impressora.Imprimirarquivo(ArquivoPdf)

                    End If

                    DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False
                Catch ex As Exception

                    DGVListaMaterialSW.Rows(i).DefaultCellStyle.BackColor = Color.LightPink
                    DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False
                Finally

                End Try

            End If

        Next

    End Sub

    Private Sub AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem.Click

        Dim resultado As MsgBoxResult = MessageBox.Show("Deseja Atualizar os arquivos da OS, esta ação ira apagar todos os arquivo da pasta da Ordem de Serviço " & OrdemServico.IdOrdemServico, "Atualização", MessageBoxButtons.YesNo)

        If resultado = DialogResult.Yes Then

            ' Verifica e obtém o valor da célula "Liberado_Engenharia"
            If dgvos.CurrentRow.Cells("Liberado_Engenharia") IsNot Nothing AndAlso dgvos.CurrentRow.Cells("Liberado_Engenharia").Value IsNot DBNull.Value Then
                OrdemServico.Liberado_Engenharia = dgvos.CurrentRow.Cells("Liberado_Engenharia").Value.ToString()
            Else
                OrdemServico.Liberado_Engenharia = String.Empty ' Valor padrão em caso de ausência
            End If

            If OrdemServico.Liberado_Engenharia = "S" Then

                MsgBox("OS Já liberada, não e possivel liberar novamente!", vbInformation, "Atenção")

                Exit Sub
            Else

                Try

                    If DGVListaMaterialSW.Rows.Count > 0 Then

                        Dim diretorio As String = OrdemServico.EnderecoOrdemServico

                        LimparDiretorio(diretorio & "\PDF")
                        LimparDiretorio(diretorio & "\DXF")
                        LimparDiretorio(diretorio & "\DFT")
                        LimparDiretorio(diretorio & "\LXDS")

                        'OrdemServico.IDOrdemServicoItem = DGVListaMaterialSW.Rows(1).Cells("IdOrdemServicoItem").Value

                        ImportarLXDSParaOS(DGVListaMaterialSW, "DXF", ProgressBarProcessoLiberacaoOrdemServico, "")
                        ImportarLXDSParaOS(DGVListaMaterialSW, "PDF", ProgressBarProcessoLiberacaoOrdemServico, "IdOrdemservicoItem")
                        ImportarLXDSParaOS(DGVListaMaterialSW, "DFT", ProgressBarProcessoLiberacaoOrdemServico, "")
                        ImportarLXDSParaOS(DGVListaMaterialSW, "LXDS", ProgressBarProcessoLiberacaoOrdemServico, "")

                        MsgBox("Documentos enviado com sucesso!", vbInformation, "Atenção")
                    Else

                        MsgBox("Não ha dados a serem atualizado", vbCritical, "Atenção!")

                    End If
                Catch ex As Exception
                    MsgBox(ex.Message & " ERRO: Transferencia de arquivo - Atualização de documento da OS")
                Finally

                End Try

                'Me.lblOrdemServicoAtiva.Text = ""

                'TimerDGVListaMaterialSW.Enabled = True
                'TimerFiltroPecaAtivaOS.Enabled = True

            End If
        Else

            MsgBox("Operação Cancelada", vbCritical, "Atenção")

        End If

    End Sub

    '    Private Sub TimerFiltroPecaAtivaOS_Tick(sender As Object, e As EventArgs) Handles TimerFiltroPecaAtivaOS.Tick

    '        DGVTimerFiltroPecaAtivaOS.DataSource = cl_BancoDados.CarregarDados("SELECT IdOrdemServico,
    'IDOrdemServicoItem,
    'Projeto, Tag, IdMaterial,
    'CodMatFabricante, DescResumo, DescDetal,
    'UPPER(EnderecoArquivo) AS EnderecoArquivo
    'FROM  " & ComplementoTipoBanco & "ordemservicoitem
    'where (Estatus = 'A') AND (D_E_L_E_T_E <> '*' OR D_E_L_E_T_E IS NULL)
    'AND (ORDEMSERVICOITEMFINALIZADO = '' OR ORDEMSERVICOITEMFINALIZADO IS NULL)
    'And  CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'")

    '        '	lblNUmeroDocumentoAtivo.Text = DadosArquivoCorrente.NomeArquivoSemExtensao

    '        'edson 20-01-2025
    '        'para verificar a necessidade processamento na abertura de cada arquivo
    '        'For Each col As DataGridViewColumn In DGVTimerFiltroPecaAtivaOS.Columns
    '        '    If col.Width > 350 Then
    '        '        col.Width = 351
    '        '    End If
    '        'Next

    '        TimerFiltroPecaAtivaOS.Enabled = False

    '    End Sub

    '    Private Sub DGVTimerFiltroPecaAtivaOS_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs)

    '        cl_BancoDados.FormatarDataGridView(DGVTimerFiltroPecaAtivaOS, "SIM")

    '        For i As Integer = 0 To DGVTimerFiltroPecaAtivaOS.Rows.Count - 1

    '            Dim valorEnderecoArquivo As String = If(DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("EnderecoArquivo").Value, "").ToString()

    '            ' Verifica se a string ".SLDASM" está contida na célula e se "ProdutoPrincipal" é "SIM" (ignora maiúsculas/minúsculas)
    '            If valorEnderecoArquivo.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then
    '                ' Define a imagem na coluna "dgvIconeItemOS" se for .SLDASM
    '                DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("dgvTipoDesenhoAtualizacaoItemOs").Value = My.Resources.IcopneMontagemSW ' Substitua pelo seu ícone
    '            ElseIf valorEnderecoArquivo.IndexOf(".SLDPRT", StringComparison.OrdinalIgnoreCase) >= 0 Then
    '                ' Define outra imagem se for .SLDPRT
    '                DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("dgvTipoDesenhoAtualizacaoItemOs").Value = My.Resources.IcopneMontagemPRT

    '            End If

    '            DGVTimerFiltroPecaAtivaOS.Rows(i).Cells("dgvSelecaoAtualizacaoItemOs").Value = True

    '        Next

    '    End Sub

    '    Private Sub DGVTimerFiltroPecaAtivaOS_Click(sender As Object, e As EventArgs)

    '        If DGVTimerFiltroPecaAtivaOS.CurrentRow.Cells("dgvSelecaoAtualizacaoItemOs").Value = False Then
    '            DGVTimerFiltroPecaAtivaOS.CurrentRow.Cells("dgvSelecaoAtualizacaoItemOs").Value = True
    '        Else
    '            DGVTimerFiltroPecaAtivaOS.CurrentRow.Cells("dgvSelecaoAtualizacaoItemOs").Value = False

    '        End If

    '    End Sub

    '    Private Sub btnAtualizarDadosItemOs_Click(sender As Object, e As EventArgs)

    '        If DGVTimerFiltroPecaAtivaOS.Rows.Count > 0 Then

    '            Dim result As DialogResult = MessageBox.Show("Deseja Realmente atualizar os itens selecionado das OS'?", "Atualização os Itens da OS", MessageBoxButtons.YesNo)

    '            If result = DialogResult.Yes Then

    '                AtualisarItensOrdemServico()

    '                TimerDGVListaMaterialSW.Enabled = True

    '            Else

    '                MsgBox("A atualização será cancelda!", vbCritical, "Atenção")

    '            End If

    '        End If

    '    End Sub

    Private Sub GeralExcelDaOSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GeralExcelDaOSToolStripMenuItem.Click

        Dim principal As String = ""

        If DGVListaMaterialSW.Rows.Count >= 0 Then

            For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

                'Verifica se há um produto principal para Ordem de serviço
                Try

                    principal = DGVListaMaterialSW.Rows(i).Cells("ProdutoPrincipal").Value.ToString

                    If principal = "SIM" Then
                        '  principal = DGVListaMaterialSW.Rows(i).Cells("ProdutoPrincipal").Value.ToString
                        ProdutoPrincipal = DGVListaMaterialSW.Rows(i).Cells("CodMatFabricante").Value.ToString

                        Exit For

                    ElseIf principal = "" Then

                        principal = ""
                        ProdutoPrincipal = ""
                        MsgBox("Para gerar a Lista de material e obrigatorio que o produto 'Principal'da Ordem de Serviço já esteja selecionado.", vbInformation, "Atenção")
                        Exit Sub
                    End If
                Catch ex As Exception

                    principal = ""
                    ProdutoPrincipal = ""
                    MsgBox("Para gerar a Lista de material e obrigatorio que o produto 'Principal'da Ordem de Serviço já esteja selecionado.", vbInformation, "Atenção")

                    Exit Sub

                End Try

            Next

        End If

        Try

            txtPesqNumeroDesenho.Clear()

            txtPesqTipoDesenho.Clear()

            txtPesqAcabamentoDesenho.Clear()

            TimerDGVListaMaterialSW.Enabled = True

            If My.Settings.BancoDadosAtivo = "mettapaineis" Then

                'PadraoMetta.ExportarOrdemServicoPadraoMettaAntigo(DGVListaMaterialSW, ProgressBarProcessoLiberacaoOrdemServico, OrdemServico.EnderecoOrdemServico, Me.txtDescricao.Text.Trim.ToUpper, dgvos, True, DGVListaMaterialSWMaterial)
                PadraoMetta.ExportarOrdemServicoPadraoMettaAntigo(DGVListaMaterialSW, ProgressBarProcessoLiberacaoOrdemServico, OrdemServico.EnderecoOrdemServico, Me.txtDescricao.Text.Trim.ToUpper, dgvos, True, DGVListaMaterialSW)
            Else
                TemplatesExcel.ExportarOrdemServicoPadrao(DGVListaMaterialSW, ProgressBarProcessoLiberacaoOrdemServico, OrdemServico.EnderecoOrdemServico, Me.txtDescricao.Text.Trim.ToUpper, dgvos, DGVListaMaterialSWMateriais)

            End If
        Catch ex As Exception

            MsgBox("Erro ao gerar o Excel da OS: " & ex.Message, MsgBoxStyle.Critical, "Erro")
        Finally

        End Try

    End Sub

    Private Sub ConfiguraçãoToolStripMenuItem1_Click(sender As Object, e As EventArgs)

        If InputBox("Senha de acesso", "Administrador", "") = "99678982" Then
            Dim OpenfileConfiguracao As New OpenFileDialog

            ' Configura o diálogo para aceitar somente arquivos de texto
            OpenfileConfiguracao.Filter = "Arquivos de Texto (*.txt)|*.txt|Todos os arquivos (*.*)|*.*"
            OpenfileConfiguracao.FilterIndex = 1

            ' Exibe o diálogo e verifica se o usuário selecionou um arquivo
            If OpenfileConfiguracao.ShowDialog() = DialogResult.OK Then
                Dim caminhoArquivo As String = OpenfileConfiguracao.FileName

                ' Verifica se o arquivo existe
                If File.Exists(caminhoArquivo) Then
                    Try
                        ' Variáveis para armazenar os parâmetros
                        Dim endereco, usuario, banco, senha As String
                        Dim EnderecoPastaRaizOS, EnderecoTemplateExcel, CopiaBancoDados As String
                        Dim EnderecoPastaRaizRomaneio, EnderecoTemplateExcelRomaneio, ParametroExportarDXF As String

                        ' Codificação utilizada na leitura do arquivo
                        Dim codificacao As Encoding = Encoding.GetEncoding("ISO-8859-1") ' Ajustável conforme o arquivo

                        ' Lê o arquivo linha por linha
                        Dim parametrosEncontrados As New Dictionary(Of String, String)
                        Using leitor As New StreamReader(caminhoArquivo, codificacao)
                            While Not leitor.EndOfStream
                                Dim linha As String = leitor.ReadLine()?.Trim()

                                ' Ignorar linhas em branco ou mal formadas
                                If String.IsNullOrEmpty(linha) OrElse Not linha.Contains(";") Then Continue While

                                Dim partes = linha.Split(";"c)
                                If partes.Length = 2 Then
                                    Dim chave = partes(0).Trim()
                                    Dim valor = partes(1).Trim()

                                    ' Armazena no dicionário
                                    If Not parametrosEncontrados.ContainsKey(chave) Then
                                        parametrosEncontrados(chave) = valor
                                    End If
                                End If
                            End While
                        End Using

                        ' Atribui valores ao My.Settings
                        Dim parametrosNecessarios = {"endereco", "Usuario", "Banco", "Senha", "EnderecoPastaRaizOS", "EnderecoTemplateExcelOrdemServico", "ParametroExportarDXF"}
                        For Each param In parametrosNecessarios
                            If Not parametrosEncontrados.ContainsKey(param) Then
                                MsgBox($"Erro: Parâmetro '{param}' não encontrado no arquivo de configuração!", MsgBoxStyle.Critical)
                                Return
                            End If
                        Next

                        My.Settings.MySqlBancoDados = parametrosEncontrados("Banco")
                        My.Settings.MysqlEndereco = parametrosEncontrados("endereco")
                        My.Settings.MysqlUsuario = parametrosEncontrados("Usuario")
                        My.Settings.MysqlSenha = parametrosEncontrados("Senha")
                        My.Settings.BancoDadosAtivo = parametrosEncontrados("Banco")

                        My.Settings.EnderecoPastaRaizOS = parametrosEncontrados("EnderecoPastaRaizOS")
                        My.Settings.EnderecoTemplateExcel = parametrosEncontrados("EnderecoTemplateExcelOrdemServico")
                        My.Settings.ParametroExportarDXF = parametrosEncontrados("ParametroExportarDXF")

                        ' Salva as configurações
                        My.Settings.Save()

                        MsgBox("Configurações carregadas com sucesso!", MsgBoxStyle.Information)
                    Catch ex As Exception
                        MsgBox($"Erro ao processar o arquivo de configuração: {ex.Message}", MsgBoxStyle.Critical)
                    End Try
                Else
                    MsgBox("Arquivo selecionado não encontrado!", MsgBoxStyle.Exclamation)
                End If
            End If
        Else
            MsgBox("Senha inválida!", MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub TrocarFormatoA3ToolStripMenuItem_Click(sender As Object, e As EventArgs)

        If File.Exists(My.Settings.EnderecoNovoFormatoA3) = False Then

            MsgBox("O Arquivo padrão deve ser selecionado ante de executar a Operação!", vbCritical, "Atenção")
        Else

            Try

                ' Dim swModel As ModelDoc2
                Dim swModelDocExt As ModelDocExtension
                Dim swDrawing As DrawingDoc
                Dim fileName As String
                Dim status As Boolean
                ' Dim errors As Integer
                '  Dim warnings As Integer
                Dim sheetNameArray As Object
                Dim sheetNames(1) As String
                ' Dim options As Integer
                Dim fileerror As Integer

                Dim filewarning As Integer

                '  Dim lRetVal As Integer
                ' Dim ResolvedValOut As String
                ' Dim wasResolved As Boolean

                IntanciaSolidWorks.ConectarSolidWorks()

                swModel = swapp.ActiveDoc

                swModel.Visible = True

                swModelDocExt = swModel.Extension

                fileName = swModel.GetPathName.ToString
                'MsgBox(fileName.ToString)

                swModel = swapp.OpenDoc6(fileName.ToString, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_LoadModel, True, fileerror, filewarning)

                ' swModel = swapp.OpenDoc6(fileName, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_Silent, "", errors, warnings)
                swModelDocExt = swModel.Extension
                swDrawing = swModel
                sheetNames(0) = "Sheet2"
                sheetNames(1) = "Sheet3"
                sheetNameArray = sheetNames
                swDrawing.SetSheetsSelected(sheetNameArray)
                status = swDrawing.SetupSheet6("Sheet3", swDwgPaperSizes_e.swDwgPapersUserDefined, swDwgTemplates_e.swDwgTemplateCustom, 0, 0, True, My.Settings.EnderecoNovoFormatoA3.ToString, 0.385, 0.277, "Default", True, 0, 0, 0, 0, 0, 0)

                swModel.ForceRebuild3(True)
                swModel.ViewZoomtofit2()

                ' Atualiza a exibição gráfica antes de salvar para garantir a miniatura
                swModel.GraphicsRedraw2()

                ' Salva o arquivo com as opções de salvamento padrão e com a miniatura
                swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)

                'swApparq.CloseDoc(swModel.GetTitle)
            Catch ex As Exception
            Finally
            End Try

        End If

    End Sub

    Private Sub DGVMontaPeca_DataError(sender As Object, e As DataGridViewDataErrorEventArgs)
        Try
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub txtAuthor_DoubleClick(sender As Object, e As EventArgs) Handles txtAuthor.DoubleClick

        Try

            Me.txtAuthor.Text = Usuario.Sigla.ToString
        Catch ex As Exception
        Finally

        End Try

    End Sub

    Private Sub LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem.Click

        If OrdemServico.IdOrdemServico = Nothing Then

            MsgBox("Não há Ordem de Serviço selecionada", vbCritical, "Atenção")
        Else

            If dgvos.CurrentRow.Cells("Liberado_Engenharia").Value.ToString <> "S" Then

                OrdemServico.EnderecoOrdemServico = dgvos.CurrentRow.Cells("Endereco").Value.ToString

                Dim resultado As MsgBoxResult = MessageBox.Show("Deseja limpar a Ordem de Serviço, esta ação ira apagar todos os arquivo da pasta da Ordem de Serviço " & OrdemServico.IdOrdemServico, "Exclusão", MessageBoxButtons.YesNo)

                If resultado = DialogResult.Yes Then



                    Dim sql As String = "DELETE FROM ordemservicoitem WHERE idordemservico = @id"

                    Dim parametros As New List(Of MySqlParameter) From {
              New MySqlParameter("@id", OrdemServico.IdOrdemServico)
                }

                    cl_BancoDados.SalvarParametros(sql, parametros)



                    '  cl_BancoDados.AlteracaoEspecificaDelete("ordemservicoitem", "idordemservico", OrdemServico.IdOrdemServico)

                    ' cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "D_E_L_E_T_E", "*", "IdOrdemServico", OrdemServico.IdOrdemServico)

                    Dim diretorio As String = OrdemServico.EnderecoOrdemServico

                    LimparDiretorio(diretorio)

                    Me.lblOrdemServicoAtiva.Text = ""

                    TimerDGVListaMaterialSW.Enabled = True
                    TimerFiltroPecaAtivaOS.Enabled = True
                Else

                    MsgBox("Operação Cancelada", vbCritical, "Atenção")

                End If
            Else

                MsgBox("Esta operação não e valida a OS: " & OrdemServico.EnderecoOrdemServico & ", já foi liberada anteriormente!", vbInformation, "Atenção")

            End If

        End If

        cl_CalculoBancoDados.CalcularordemservicoitemFatorOS_TAG_PROJETO()

    End Sub

    'Sub LimparDiretorio(ByVal diretorio As String)
    '	' Verifica se o diretório existe
    '	If Directory.Exists(diretorio) Then
    '		' Limpa todos os arquivos no diretório
    '		For Each arquivo As String In Directory.GetFiles(diretorio)
    '			File.Delete(arquivo)
    '		Next

    '		' Limpa todos os subdiretórios, exceto aqueles chamados "Projeto"
    '		For Each subdiretorio As String In Directory.GetDirectories(diretorio)
    '			' Verifica se o nome do subdiretório é "Projeto"
    '			If Path.GetFileName(subdiretorio).Equals("DXF", StringComparison.OrdinalIgnoreCase) Or
    '					Path.GetFileName(subdiretorio).Equals("PDF", StringComparison.OrdinalIgnoreCase) Or
    '					Path.GetFileName(subdiretorio).Equals("DFT", StringComparison.OrdinalIgnoreCase) Or
    '					Path.GetFileName(subdiretorio).Equals("PUNC", StringComparison.OrdinalIgnoreCase) Or
    '					Path.GetFileName(subdiretorio).Equals("LASER", StringComparison.OrdinalIgnoreCase) Then
    '				LimparDiretorio(subdiretorio)

    '			End If
    '		Next
    '	End If

    'End Sub

    Sub LimparDiretorio(ByVal diretorio As String)
        ' Verifica se o diretório existe
        If Directory.Exists(diretorio) Then
            ' Remove o atributo somente leitura do diretório
            Dim dirInfo As New DirectoryInfo(diretorio)
            dirInfo.Attributes = dirInfo.Attributes And Not FileAttributes.ReadOnly

            ' Limpa todos os arquivos no diretório
            For Each arquivo As String In Directory.GetFiles(diretorio)
                ' Remove o atributo somente leitura antes de excluir
                Dim fileInfo As New FileInfo(arquivo)
                fileInfo.Attributes = fileInfo.Attributes And Not FileAttributes.ReadOnly
                File.Delete(arquivo)
            Next

            ' Limpa todos os subdiretórios, exceto aqueles chamados "Projeto"
            For Each subdiretorio As String In Directory.GetDirectories(diretorio)
                ' Verifica se o nome do subdiretório corresponde a um dos nomes especificados
                If Path.GetFileName(subdiretorio).Equals("DXF", StringComparison.OrdinalIgnoreCase) Or
               Path.GetFileName(subdiretorio).Equals("PDF", StringComparison.OrdinalIgnoreCase) Or
               Path.GetFileName(subdiretorio).Equals("DFT", StringComparison.OrdinalIgnoreCase) Or
               Path.GetFileName(subdiretorio).Equals("PUNC", StringComparison.OrdinalIgnoreCase) Or
               Path.GetFileName(subdiretorio).Equals("LASER", StringComparison.OrdinalIgnoreCase) Then

                    ' Remove o atributo somente leitura do subdiretório antes de limpar
                    Dim subDirInfo As New DirectoryInfo(subdiretorio)
                    subDirInfo.Attributes = subDirInfo.Attributes And Not FileAttributes.ReadOnly

                    LimparDiretorio(subdiretorio)
                End If
            Next

        End If
    End Sub

    Private Sub BuscarFormatoA3ToolStripMenuItem_Click(sender As Object, e As EventArgs)

        ' Configura o filtro para apenas arquivos com extensão .slddrt
        OpenFileDialog1.Filter = "SolidWorks Drawing Templates A3 (*.slddrt)|*.slddrt"
        ' Mostra o OpenFileDialog
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            My.Settings.EnderecoNovoFormatoA3 = OpenFileDialog1.FileName

            ' Salva as configurações
            My.Settings.Save()
        End If

    End Sub

    Private Sub BUSCRAToolStripMenuItem_Click(sender As Object, e As EventArgs)

        ' Configura o filtro para apenas arquivos com extensão .slddrt
        OpenFileDialog1.Filter = "SolidWorks Drawing Templates A4 (*.slddrt)|*.slddrt"

        ' Mostra o OpenFileDialog
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            My.Settings.EnderecoNovoFormatoA4 = OpenFileDialog1.FileName

            ' Salva as configurações
            My.Settings.Save()
        End If

    End Sub

    Private Sub TrocarForrmatoA4ToolStripMenuItem_Click(sender As Object, e As EventArgs)

        If File.Exists(My.Settings.EnderecoNovoFormatoA3) = False Then

            MsgBox("O Arquivo padrão deve ser selecionado ante de executar a Operação!", vbCritical, "Atenção")
        Else

            Try

                '  Dim swModel As ModelDoc2
                Dim swModelDocExt As ModelDocExtension
                Dim swDrawing As DrawingDoc
                Dim fileName As String
                Dim status As Boolean
                ' Dim errors As Integer
                ' Dim warnings As Integer
                Dim sheetNameArray As Object
                Dim sheetNames(1) As String
                ' Dim options As Integer
                Dim fileerror As Integer

                Dim filewarning As Integer

                ' Dim lRetVal As Integer
                ' Dim ResolvedValOut As String
                ' Dim wasResolved As Boolean

                IntanciaSolidWorks.ConectarSolidWorks()

                swModel = swapp.ActiveDoc

                swModel.Visible = True

                swModelDocExt = swModel.Extension

                fileName = swModel.GetPathName.ToString
                'MsgBox(fileName.ToString)

                swModel = swapp.OpenDoc6(fileName.ToString, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_LoadModel, True, fileerror, filewarning)

                ' swModel = swapp.OpenDoc6(fileName, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_Silent, "", errors, warnings)
                swModelDocExt = swModel.Extension
                swDrawing = swModel
                sheetNames(0) = "Sheet2"
                sheetNames(1) = "Sheet3"
                sheetNameArray = sheetNames
                swDrawing.SetSheetsSelected(sheetNameArray)
                status = swDrawing.SetupSheet6("Sheet3", swDwgPaperSizes_e.swDwgPaperA4size, swDwgTemplates_e.swDwgTemplateCustom, 0.21, 0.297, True, My.Settings.EnderecoNovoFormatoA4.ToString, 0.21, 0.297, "Default", True, 0, 0, 0, 0, 0, 0)

                swModel.ForceRebuild3(True)
                swModel.ViewZoomtofit2()

                ' Atualiza a exibição gráfica antes de salvar para garantir a miniatura
                swModel.GraphicsRedraw2()

                ' Salva o arquivo com as opções de salvamento padrão e com a miniatura
                swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)

                'swApparq.CloseDoc(swModel.GetTitle)
            Catch ex As Exception
            Finally

            End Try

        End If

    End Sub

    Private Sub txtPesqCriadoPor_DoubleClick(sender As Object, e As EventArgs) Handles txtPesqCriadoPor.DoubleClick

        Try

            Me.txtPesqCriadoPor.Text = Usuario.Login
        Catch ex As Exception
        Finally

        End Try

    End Sub

    Private Sub txtPesqCriadoPor_TextChanged(sender As Object, e As EventArgs) Handles txtPesqCriadoPor.TextChanged

        Timerdgvos.Enabled = True

    End Sub

    Private Sub ConfExportarArquivoParaOSToolStripMenuItem_Click(sender As Object, e As EventArgs)

        ExportarParaOS.ShowDialog()

    End Sub

    Private Sub CancelarAFabricaçãoDaOSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelarAFabricaçãoDaOSToolStripMenuItem.Click

        If Usuario.NomeCompleto <> dgvos.CurrentRow.Cells("CriadoPor").Value.ToString() Then

            Dim resultUsuario As DialogResult = MessageBox.Show("O Usuario: " & dgvos.CurrentRow.Cells("CriadoPor").Value.ToString() &
                 " foi o Criador desta OS, mesmo assim você gostaria de Excluir a OS Selecionada", "Exclusão Ordem de Serviço", MessageBoxButtons.YesNo)

            If resultUsuario = DialogResult.No Then

                Exit Sub

            End If

        End If
        If OrdemServico.IdOrdemServico.ToString = Nothing Or OrdemServico.IdOrdemServico.ToString = "" Then

            MsgBox("Não há OS Selecionada!", vbCritical, "Atenção")

            Exit Sub

        End If

        Dim result As DialogResult = MessageBox.Show("Deseja Realmente Excluir/Cancelar a Ordem de Serviço: " & OrdemServico.IdOrdemServico, "Cancelando Ordem de Serviço", MessageBoxButtons.YesNo)

        Dim totalExecutado As Integer

        Try

            Convert.ToInt32(cl_BancoDados.RetornaCampoDaPesquisa("SELECT  count(idplanodecorte) +
				   count(CorteTotalExecutado) + count(DobraTotalExecutado)+ count(SoldaTotalExecutado) +
				   count(PinturaTotalExecutado) +  count(MontagemTotalExecutado) as totalExecutado
				   FROM  " & ComplementoTipoBanco & "ordemservicoitem where IdOrdemServico  ='" & OrdemServico.IdOrdemServico & "'
				   and (idplanodecorte > 0) AND (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '');", "totalExecutado"))

            totalExecutado = VCampo0
        Catch ex As Exception

            totalExecutado = 0
        Finally

        End Try

        If result = DialogResult.Yes Then

            If totalExecutado > 0 Then

                Dim dtTabelaPlanoCorte As New System.Data.DataTable()

                dtTabelaPlanoCorte = cl_BancoDados.CarregarDados("SELECT  idplanodecorte, CodMatFabricante
					 FROM  " & ComplementoTipoBanco & "ordemservicoitem where IdOrdemServico  = '" & OrdemServico.IdOrdemServico & "' and (idplanodecorte > 0) AND (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '');")

                Dim MessagemItens As String

                For I As Integer = 0 To dtTabelaPlanoCorte.Rows.Count - 1

                    MessagemItens = MessagemItens & "PlanoCorte = " & dtTabelaPlanoCorte.Rows(I).Item("idplanodecorte").ToString &
                    " Numero Desenho: = " & dtTabelaPlanoCorte.Rows(I).Item("CodMatFabricante").ToString & vbCrLf

                Next

                MsgBox("A OS Numero: " & OrdemServico.IdOrdemServico & " contem processos em andamento, por este motivo não pode ser cancelada, ver plano de corte's: " & vbCrLf & MessagemItens, vbCritical, "Atenção!")

                Exit Sub

            End If


            'Excluir a OS
            '  cl_BancoDados.AlteracaoEspecificaDelete("ordemservico", "IdOrdemServico", OrdemServico.IdOrdemServico)
            Dim sqlordemservico As String = "DELETE FROM ordemservico WHERE idordemservico = @id"
            Dim parametros As New List(Of MySqlParameter) From {
              New MySqlParameter("@id", OrdemServico.IdOrdemServico)
                }

            cl_BancoDados.SalvarParametros(sqlordemservico, parametros)




            ' cl_BancoDados.AlteracaoEspecificaDelete("ordemservicoitem", "IdOrdemServico", OrdemServico.IdOrdemServico)
            Dim sqlordemservicoitem As String = "DELETE FROM ordemservicoitem WHERE idordemservico = @id"
            Dim parametros1 As New List(Of MySqlParameter) From {
              New MySqlParameter("@id", OrdemServico.IdOrdemServico)
                }

            cl_BancoDados.SalvarParametros(sqlordemservicoitem, parametros1)


            Dim diretorio As String = OrdemServico.EnderecoOrdemServico
            LimparDiretorio(diretorio)

            'Marca a OS como Excluida
            'cl_BancoDados.AlteracaoEspecifica("ordemservico", "D_E_L_E_T_E", "*", "IdOrdemServico", OrdemServico.IdOrdemServico)
            'cl_BancoDados.AlteracaoEspecifica("ordemservico", "UsuarioD_E_L_E_T_E", Usuario.NomeCompleto, "IdOrdemServico", OrdemServico.IdOrdemServico)
            'cl_BancoDados.AlteracaoEspecifica("ordemservico", "DataD_E_L_E_T_E", Date.Now, "IdOrdemServico", OrdemServico.IdOrdemServico)

            'cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "D_E_L_E_T_E", "*", "IdOrdemServico", OrdemServico.IdOrdemServico)
            'cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "UsuarioD_E_L_E_T_E", Usuario.NomeCompleto, "IdOrdemServico", OrdemServico.IdOrdemServico)
            'cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "DataD_E_L_E_T_E", Date.Now, "IdOrdemServico", OrdemServico.IdOrdemServico)

            dgvos.CurrentRow.Cells("dgvStatus").Value = My.Resources.Sem_Incone
            dgvos.Rows.Remove(dgvos.CurrentRow)

            'Timerdgvos.Enabled = True
            TimerDGVListaMaterialSW.Enabled = True
            TimerFiltroPecaAtivaOS.Enabled = True

        ElseIf result = DialogResult.No Then

            MsgBox("Operação Cancelada", vbCritical, "Atenção")

        End If

        cl_CalculoBancoDados.CalcularordemservicoitemFatorOS_TAG_PROJETO()

    End Sub

    Private Sub btnAssociarMaterial_Click(sender As Object, e As EventArgs)

        If String.IsNullOrWhiteSpace(DadosArquivoCorrente.NomeArquivoSemExtensao) Then

            MessageBox.Show("Não há desenho ativo para associar material.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Exit Sub
        Else
            Using formMateriaisAlmoxarifado As New frmMateriaisAlmoxarifado ' MateriaisAlmoxarifado

                cl_BancoDados.RetornaCampoDaPesquisa("Select IdMaterial from  " & ComplementoTipoBanco & "material where CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'", "IdMaterial")

                DadosArquivoCorrente.IdMaterial = VCampo0

                formMateriaisAlmoxarifado.ShowDialog()

                TimerMontaPeca.Enabled = True

            End Using

        End If

    End Sub

    Private Sub btnConverterListaDXFPDF_Click(sender As Object, e As EventArgs)

        If dgvDataGridBOM.Rows.Count > 0 Then

            Dim arquivoSLDDRW As String

            ' Exibe a caixa de mensagem com um aviso e opções Sim e Não
            Dim result As DialogResult = MessageBox.Show("Esta operação irá processar todos os itens do Grid, abrindo os desenhos e atualizando os dados cadastrais. Este procedimento pode levar algum tempo. Você deseja prosseguir?", "Atualizar Dados", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then

                IntanciaSolidWorks.ConectarSolidWorks()

                ProgressBarListaSW.Minimum = 0
                ProgressBarListaSW.Maximum = (dgvDataGridBOM.Rows.Count - 1) * 2

                dgvDataGridBOM.SuspendLayout()

                For i As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

                    Try

                        DadosArquivoCorrente.EnderecoArquivo = dgvDataGridBOM.Rows(i).Cells("EnderecoArquivo").Value.ToString

                        DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(DadosArquivoCorrente.EnderecoArquivo)

                        If chkConverterPDF.Checked Then

                            ' Verifica se EnderecoArquivo não é nulo ou vazio antes de realizar a substituição
                            If Not String.IsNullOrEmpty(DadosArquivoCorrente.EnderecoArquivo) Then

                                Dim enderecoArquivoUpper As String = DadosArquivoCorrente.EnderecoArquivo.ToUpper()

                                ' Converte o caminho do arquivo para .SLDDRW se ele terminar com .SLDPRT ou .SLDASM
                                arquivoSLDDRW = enderecoArquivoUpper
                                If enderecoArquivoUpper.EndsWith(".SLDPRT") OrElse enderecoArquivoUpper.EndsWith(".SLDASM") Then
                                    arquivoSLDDRW = arquivoSLDDRW.Replace(".SLDPRT", ".SLDDRW").Replace(".SLDASM", ".SLDDRW")
                                End If

                                If File.Exists(arquivoSLDDRW) Then
                                    OpenDocumentAndWait(arquivoSLDDRW, True, swModel)
                                    DadosArquivoCorrente.ExportToPDF(swModel, arquivoSLDDRW, False)
                                    ' swapp.CloseDoc(arquivoSLDDRW)
                                    dgvDataGridBOM.Rows(i).Cells("dgvIconePDF").Value = My.Resources.ficheiro_pdf
                                Else

                                    '  DadosArquivoCorrente.ExportToPDF(swModel, arquivoSLDDRW, False)
                                    dgvDataGridBOM.Rows(i).Cells("dgvIconePDF").Value = My.Resources.Sem_Incone

                                End If

                            End If

                        End If

                        If chkConverterDXF.Checked Then

                            If DadosArquivoCorrente.EnderecoArquivo.ToUpper.EndsWith(".SLDPRT") Then

                                OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)

                                Dim enderecoArquivo As String = DadosArquivoCorrente.EnderecoArquivo

                                ' Verifica se EnderecoArquivo não é nulo e o arquivo realmente existe antes de tentar exportar
                                If Not String.IsNullOrEmpty(enderecoArquivo) AndAlso File.Exists(enderecoArquivo) Then
                                    DadosArquivoCorrente.ExportDXF(swModel, True, True)
                                    dgvDataGridBOM.Rows(i).Cells("DGVIconeDXF").Value = My.Resources.arquivo_dxf
                                Else
                                    dgvDataGridBOM.Rows(i).Cells("DGVIconeDXF").Value = My.Resources.Sem_Incone

                                End If
                            Else

                                dgvDataGridBOM.Rows(i).Cells("DGVIconeDXF").Value = My.Resources.Sem_Incone

                            End If

                        End If

                        swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                        cl_BancoDados.FecharArquivoMemoria()
                        IntanciaSolidWorks.LiberarRecurso(swModel)
                        IntanciaSolidWorks.LiberarRecurso(swPart)

                        dgvDataGridBOM.Rows(i).Cells("CodMatFabricante").Style.BackColor = Color.LightGreen

                        ProgressBarListaSW.Value = i
                    Catch ex As Exception

                        dgvDataGridBOM.Rows(i).Cells("CodMatFabricante").Style.BackColor = Color.LightPink

                        '  MsgBox(ex.Message)

                        Continue For

                    End Try

                Next

                MsgBox("Processo de Atualização Finalizado com sucesso!", vbInformation, "Informação")

                ProgressBarListaSW.Value = 0

                chkConverterPDF.Checked = False
                chkConverterDXF.Checked = False
            Else

                MsgBox("Processo cancelado!", vbInformation, "Informação")

            End If

            'dgvDataGridBOM.ResumeLayout()
        End If

        If dgvDataGridBOM.Rows.Count > 0 Then

            For a As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

                Try

                    If dgvDataGridBOM.Rows(a).Cells("EnderecoArquivo").Value.ToString().ToLower().EndsWith(".sldprt") OrElse
                  dgvDataGridBOM.Rows(a).Cells("EnderecoArquivo").Value.ToString().ToLower().EndsWith(".sldasm") Then

                        swapp.CloseDoc(dgvDataGridBOM.Rows(a).Cells("EnderecoArquivo").Value.ToString())

                    End If
                Catch ex As Exception
                    Continue For
                End Try

            Next
        End If

    End Sub

    Private Sub InformeOTituloPadrãoDoProdutoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InformeOTituloPadrãoDoProdutoToolStripMenuItem.Click

        Try

            TituloPadraoProduto = InputBox("Informe o título padrão do produto a ser cadastrado/atualizado.", "Atualização", DadosArquivoCorrente.NomeArquivoSemExtensao)
        Catch ex As Exception
        Finally

        End Try

    End Sub

    Function GetCutLists(ByVal model As ModelDoc2) As List(Of Feature)

        IntanciaSolidWorks.ConectarSolidWorks()
        ' swApparq = CreateObject("SldWorks.Application")

        swModel = swapp.ActiveDoc
        'swModel = swApparq.ActiveDoc

        ' Lista para armazenar as CutLists
        Dim swCutListFeats As New List(Of Feature)
        Dim swFeat As Feature
        Dim swBodyFolder As BodyFolder
        Dim vBodies As Object
        Dim swBody As Body2

        ' Obter o primeiro recurso do modelo
        swFeat = model.FirstFeature

        ' Armazenar as features de CutListFolder a serem excluídas
        Dim cutListFoldersToDelete As New List(Of Feature)

        ' Primeiro, percorrer todas as features e identificar as Cut List Folders
        Do While swFeat IsNot Nothing
            ' Verifica se o tipo do recurso é uma pasta de Cut List
            If swFeat.GetTypeName2() = "CutListFolder" Then
                ' Armazenar a referência da feature CutListFolder para exclusão
                ' Desativa a atualização automática da lista de corte

                cutListFoldersToDelete.Add(swFeat)
            End If
            swFeat = swFeat.GetNextFeature()
        Loop

        ' Agora excluir apenas as features que são CutListFolder
        For Each cutListFeat As Feature In cutListFoldersToDelete
            cutListFeat.Select2(False, 0)
            model.EditDelete()
        Next

        Try

            ' Forçar a recriação das listas de cortes
            Dim featMgr As FeatureManager = model.FeatureManager
            featMgr.UpdateCutList()

            ' Obter o primeiro recurso novamente após a recriação das listas de cortes
            swFeat = model.FirstFeature

            ' Loop para obter as novas Cut List Folders criadas
            Do While swFeat IsNot Nothing
                ' Verifica se o tipo do recurso é uma pasta de Cut List
                If swFeat.GetTypeName2() = "CutListFolder" Then
                    swBodyFolder = swFeat.GetSpecificFeature2()
                    vBodies = swBodyFolder.GetBodies()

                    ' Verifica se há corpos na Cut List e se são de chapa metálica
                    If vBodies IsNot Nothing AndAlso vBodies.Length > 0 Then
                        swBody = CType(vBodies(0), Body2)

                        ' Verifica se o corpo é de chapa metálica
                        If swBody.IsSheetMetal() Then
                            swCutListFeats.Add(swFeat)
                        End If
                    End If
                End If
                ' Avança para o próximo recurso
                swFeat = swFeat.GetNextFeature()
            Loop

            ' Retornar a lista de Cut Lists
            Return swCutListFeats
        Catch ex As Exception
            ' MsgBox(ex.Message)
        End Try

    End Function

    Private Sub OPTEstoqueSim_Click(sender As Object, e As EventArgs) Handles OPTEstoqueSim.Click

        Try

            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then

                DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtItemEstoque", "SIM", "SIM")
                swModel.SaveSilent()

                cl_BancoDados.AlteracaoEspecifica("material", "txtItemEstoque", "SIM", "CodMatFabricante", DadosArquivoCorrente.NomeArquivoSemExtensao)

            End If
        Catch ex As Exception
        Finally

        End Try

    End Sub

    Private Sub OPTEstoqueNao_Click(sender As Object, e As EventArgs) Handles OPTEstoqueNao.Click

        ' Verifique se o swModel foi aberto com sucesso
        If Not swModel Is Nothing Then

            DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtItemEstoque", "NÃO", "NÃO")
            swModel.SaveSilent()
            cl_BancoDados.AlteracaoEspecifica("material", "txtItemEstoque", "NÃO", "CodMatFabricante", DadosArquivoCorrente.NomeArquivoSemExtensao)

        End If

    End Sub

    Private Sub optProcessoSoldagemSim_Click(sender As Object, e As EventArgs) Handles optProcessoSoldagemSim.Click

        ' Verifique se o swModel foi aberto com sucesso
        If Not swModel Is Nothing Then

            DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtSoldagem", "SIM", "SIM")
            swModel.SaveSilent()

        End If

    End Sub

    Private Sub optProcessoSoldagemNao_Click(sender As Object, e As EventArgs) Handles optProcessoSoldagemNao.Click

        ' Verifique se o swModel foi aberto com sucesso
        If Not swModel Is Nothing Then

            DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtSoldagem", "NÃO", "NÃO")
            swModel.SaveSilent()

        End If

    End Sub

    Private Sub AtualizarDesenhoPeloDiretorioToolStripMenuItem_Click(sender As Object, e As EventArgs)

        Dim result As DialogResult

        result = MessageBox.Show("Escolha uma opção:  SIM      1 - Atualiza a versão do SolidWork e Salva no banco de dados" & vbCrLf _
                                                      & "NÃO      2 - Somente Atualiza a versão do SolidWorks sem Salvar no Banco de dados" & vbCrLf _
                                                      & "Cancelar 3 -Finaliza a opração sem faze nenhuma alteralção", "Atualizar Versão",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1)

        If result = DialogResult.Yes Then

            ' Opção 1 - Atualizar versão do SW e Salvar no banco
            ' Processar todos os desenhos no diretório selecionado e suas subpastas
            ProcessAllFiles(True)

        ElseIf result = DialogResult.No Then
            ' Opção 2 - Somente atualizar versão do SW
            ' Processar todos os desenhos no diretório selecionado e suas subpastas
            ProcessAllFiles(False)

        ElseIf result = DialogResult.Cancel Then
            ' Opção 3 - Cancelar
            MessageBox.Show("A operação foi cancelada.", "Cancelar")

        End If

    End Sub

    Private Function SelectFolder() As String
        Using folderBrowser As New FolderBrowserDialog()
            folderBrowser.Description = "Selecione o diretório com os desenhos"
            folderBrowser.ShowNewFolderButton = False

            If folderBrowser.ShowDialog() = DialogResult.OK Then
                Return folderBrowser.SelectedPath
            End If
        End Using

        Return String.Empty
    End Function

    ' Função para abrir, salvar e fechar todos os arquivos .sldprt em um diretório escolhido pelo usuário e suas subpastas
    ' Função para abrir, salvar e fechar todos os arquivos .sldprt e .sldasm em um diretório escolhido pelo usuário e suas subpastas
    Public Sub ProcessAllFiles(ByVal SalvarBanco As Boolean)

        Dim folderPath As String = ""

        ' Seleciona a pasta usando OpenFileDialog
        Using openFileDialog As New OpenFileDialog
            openFileDialog.CheckFileExists = False
            openFileDialog.CheckPathExists = True
            openFileDialog.ValidateNames = False
            openFileDialog.FileName = "Selecione uma pasta"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                ' Remove o nome fictício para obter o caminho correto
                folderPath = IO.Path.GetDirectoryName(openFileDialog.FileName)
            Else
                MsgBox("Nenhum diretório selecionado.")
                Return
            End If
        End Using

        If String.IsNullOrEmpty(folderPath) Then
            MsgBox("Nenhum diretório selecionado.")
            Return
        End If

        Try
            ' Obter todos os arquivos no diretório e subpastas
            Dim allFiles As String() = IO.Directory.GetFiles(folderPath, "*.*", IO.SearchOption.AllDirectories)

            ' Filtrar arquivos .sldprt e .sldasm (sem distinção de maiúsculas/minúsculas)
            Dim files As IEnumerable(Of String) = allFiles.Where(Function(f) f.EndsWith(".sldprt", StringComparison.OrdinalIgnoreCase) OrElse f.EndsWith(".sldasm", StringComparison.OrdinalIgnoreCase))

            For Each filePath In files

                Try

                    ' Verifica se o arquivo é somente leitura
                    If (IO.File.GetAttributes(filePath) And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly Then
                        ' Pula esse arquivo
                        Continue For
                    End If

                    ' Determina o tipo de documento
                    Dim docType As swDocumentTypes_e
                    If filePath.EndsWith(".sldprt", StringComparison.OrdinalIgnoreCase) Then
                        docType = swDocumentTypes_e.swDocPART
                    Else
                        docType = swDocumentTypes_e.swDocASSEMBLY
                    End If

                    ' Abre o arquivo
                    Dim swModel As ModelDoc2 = swapp.OpenDoc6(filePath, docType, 0, "", Nothing, Nothing)

                    ' IntanciaSolidWorks.ConectarSolidWorks()
                    ' swApparq = CreateObject("SldWorks.Application")

                    ' If swModel IsNot Nothing Then
                    ' Processa o arquivo
                    DadosArquivoCorrente.ArquivoCorrente(swModel, chkBoxProcessos)

                    If docType = swDocumentTypes_e.swDocPART Then
                        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtmontagem", "1", "1")
                        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txttipodesenho", "CHAPARIA", "CHAPARIA")
                    ElseIf docType = swDocumentTypes_e.swDocASSEMBLY Then
                        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtmontagem", "1", "1")
                        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txttipodesenho", "MONTAGEM", "MONTAGEM")
                    End If

                    swModel.Save()

                    If SalvarBanco Then

                        DadosArquivoCorrente.PalavraChave = filePath

                        SalvarArquivoCorrente(swModel)
                    End If

                    swapp.CloseDoc(filePath)
                    ' Fechar o documento
                    ' swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                    ' cl_BancoDados.FecharArquivoMemoria()
                    ' IntanciaSolidWorks.LiberarRecurso(swModel)
                Catch ex As Exception

                    Continue For

                End Try

                Threading.Thread.Sleep(CInt(My.Settings.TempoRespostaServidor))

            Next

            MsgBox("Processamento concluído!")
        Catch ex As Exception
            MsgBox("Erro: " & ex.Message)
        Finally
            ' Adicione qualquer lógica de limpeza aqui, se necessário
        End Try

    End Sub

    Private Sub GerarArquivoEmDXFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GerarArquivoEmDXFToolStripMenuItem.Click

        If DGVListaMaterialSW.Rows.Count > 0 Then

            ' Exibe a caixa de mensagem com um aviso e opções Sim e Não
            Dim result As DialogResult = MessageBox.Show("Esta operação irá processar todos os itens do Grid, Convertento em DXF. Este procedimento pode levar algum tempo. Você deseja prosseguir?", "Atualizar Dados", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then

                IntanciaSolidWorks.ConectarSolidWorks()

                ProgressBarProcessoLiberacaoOrdemServico.Minimum = 0
                ProgressBarProcessoLiberacaoOrdemServico.Maximum = DGVListaMaterialSW.Rows.Count - 1

                '  dgvDataGridBOM.SuspendLayout()

                For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

                    Try

                        '  If DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = True Then

                        DadosArquivoCorrente.EnderecoArquivo = DGVListaMaterialSW.Rows(i).Cells("EnderecoArquivo").Value.ToString

                        DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(DadosArquivoCorrente.EnderecoArquivo)

                        OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)

                        Dim enderecoArquivo As String = DadosArquivoCorrente.EnderecoArquivo

                        ' Verifica se EnderecoArquivo não é nulo e o arquivo realmente existe antes de tentar exportar
                        If Not String.IsNullOrEmpty(enderecoArquivo) AndAlso File.Exists(enderecoArquivo) Then
                            DadosArquivoCorrente.ExportDXF(swModel, False, True)

                        End If

                        swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                        cl_BancoDados.FecharArquivoMemoria()
                        IntanciaSolidWorks.LiberarRecurso(swModel)
                        IntanciaSolidWorks.LiberarRecurso(swPart)

                        Dim enredecoDxf As String

                        ' Altera a extensão do arquivo para .DXF, independentemente de ser .SLDPRT ou .sldprt
                        enredecoDxf = Path.ChangeExtension(DadosArquivoCorrente.EnderecoArquivo, ".DXF")

                        ' Verifica se o arquivo DXF existe
                        If File.Exists(enredecoDxf) Then
                            DGVListaMaterialSW.Rows(i).Cells("DGVDXF").Value = My.Resources.arquivo_dxf
                        Else
                            DGVListaMaterialSW.Rows(i).Cells("DGVDXF").Value = My.Resources.Sem_Incone
                        End If
                        ProgressBarProcessoLiberacaoOrdemServico.Value = i

                        ' DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False

                        '   End If
                    Catch ex As Exception
                        Continue For
                    End Try
                Next

                MsgBox("Processo de Atualização Finalizado com sucesso!", vbInformation, "Informação")

                ProgressBarProcessoLiberacaoOrdemServico.Value = 0

            End If

        End If

    End Sub

    Private Sub GerarArquivoEmPDFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GerarArquivoEmPDFToolStripMenuItem.Click

        If DGVListaMaterialSW.Rows.Count > 0 Then

            ' Exibe a caixa de mensagem com um aviso e opções Sim e Não
            Dim result As DialogResult = MessageBox.Show("Esta operação irá processar todos os itens do Grid, Convertento em PDF. Este procedimento pode levar algum tempo. Você deseja prosseguir?", "Atualizar Dados", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then

                '   IntanciaSolidWorks.ConectarSolidWorks()

                ProgressBarProcessoLiberacaoOrdemServico.Minimum = 0
                ProgressBarProcessoLiberacaoOrdemServico.Maximum = DGVListaMaterialSW.Rows.Count - 1

                '  dgvDataGridBOM.SuspendLayout()

                For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

                    ' If DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = True Then

                    Try

                        DadosArquivoCorrente.EnderecoArquivo = DGVListaMaterialSW.Rows(i).Cells("EnderecoArquivo").Value.ToString
                        DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(DadosArquivoCorrente.EnderecoArquivo)
                        OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)

                        ' Verifica se EnderecoArquivo não é nulo ou vazio antes de realizar a substituição
                        If Not String.IsNullOrEmpty(DadosArquivoCorrente.EnderecoArquivo) Then
                            Dim enderecoArquivoUpper As String = DadosArquivoCorrente.EnderecoArquivo.ToUpper()

                            ' Converte o caminho do arquivo para .SLDDRW se ele terminar com .SLDPRT ou .SLDASM

                            If enderecoArquivoUpper.EndsWith(".SLDPRT") OrElse enderecoArquivoUpper.EndsWith(".SLDASM") Then
                                enderecoArquivoUpper = enderecoArquivoUpper.Replace(".SLDPRT", ".SLDDRW").Replace(".SLDASM", ".SLDDRW")
                            End If
                            If File.Exists(enderecoArquivoUpper) Then
                                OpenDocumentAndWait(enderecoArquivoUpper, True, swModel)
                                DadosArquivoCorrente.ExportToPDF(swModel, enderecoArquivoUpper, False)
                                ' swapp.CloseDoc(arquivoSLDDRW)
                            Else

                                DadosArquivoCorrente.ExportToPDF(swModel, enderecoArquivoUpper, False)

                            End If

                        End If

                        swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                        cl_BancoDados.FecharArquivoMemoria()
                        IntanciaSolidWorks.LiberarRecurso(swModel)
                        IntanciaSolidWorks.LiberarRecurso(swPart)

                        Dim enredecoPDF As String

                        ' Altera a extensão do arquivo para .PDF, independentemente de ser .SLDPRT, .sldprt, .SLDASM ou .sldasm
                        enredecoPDF = Path.ChangeExtension(DadosArquivoCorrente.EnderecoArquivo, ".PDF")

                        ' Verifica se o arquivo PDF existe
                        If File.Exists(enredecoPDF) Then
                            DGVListaMaterialSW.Rows(i).Cells("DGVPDF").Value = My.Resources.ficheiro_pdf
                        Else
                            DGVListaMaterialSW.Rows(i).Cells("DGVPDF").Value = My.Resources.Sem_Incone
                        End If
                        ProgressBarProcessoLiberacaoOrdemServico.Value = i
                    Catch ex As Exception
                        Continue For

                    End Try

                    ' DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False

                Next
                MsgBox("Processo de Atualização Finalizado com sucesso!", vbInformation, "Informação")
                ProgressBarProcessoLiberacaoOrdemServico.Value = 0
            End If

        End If

    End Sub

    Private Sub AbrirDWRDaLinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirDWRDaLinhaSelecionadaToolStripMenuItem.Click
        Try
            Dim ArquivoDXF As String = DGVListaMaterialSW.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoDXF = Path.ChangeExtension(ArquivoDXF, ".SLDDRW")

            ' Obtém o caminho completo
            ArquivoDXF = Path.GetFullPath(ArquivoDXF)
            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoDXF) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoDXF)

                    p.Start()
                    p.WaitForExit()

                    DGVListaMaterialSW.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub BuscarArquivosNosDiretorioToolStripMenuItem_Click(sender As Object, e As EventArgs)

        Arquivos.Show()

    End Sub

    Private Sub btnAtualizarProjeto_Click(sender As Object, e As EventArgs)

        Try

            cl_BancoDados.ComboBoxDataSet("projetos", "idProjeto", "Projeto", cboProjeto, " WHERE Liberado = 'S'")
            cl_BancoDados.ComboBoxDataSet("Acabamento", "idAcabamento", "DescAcabamento", cboOpcoesAcabamento, "")
            '  cl_BancoDados.ComboBoxDataSet("Acabamento", "idAcabamento", "DescAcabamento", cboAcabamentoArvore, "")

            '  cl_BancoDados.ComboBoxDataSet("familia", "idfamilia", "Descfamilia", cboTipoDesenho, "")
            '  cl_BancoDados.ComboBoxDataSet("familia", "idfamilia", "Descfamilia", cboTipoDesenhoArvore, "")

            '	cl_BancoDados.ComboBoxDataSet("tipoproduto", "idtipoproduto", "tipoproduto", cboTitulo, "")
            '  cl_BancoDados.ComboBoxDataSet("tipoproduto", "idtipoproduto", "tipoproduto", cboTituloArvore, "")
        Catch ex As Exception
        Finally

        End Try

    End Sub

    Private Sub btnLimparOS_Click(sender As Object, e As EventArgs)

        Timerdgvos.Enabled = True
        DGVListaMaterialSW.DataSource = Nothing
        DGVListaMaterialSW.Refresh()

    End Sub

    Private Sub AlterarOFatorMultipçlicadorDaOSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlterarOFatorMultipçlicadorDaOSToolStripMenuItem.Click

        If OrdemServico.Liberado_Engenharia = "S" Then

            MsgBox("Ordem de Serviço já Liberada para Produção, não pode mais ser modificada!", vbCritical, "Atenção")

            Exit Sub

        End If

        If DGVListaMaterialSW.Rows.Count <= 0 Then

            MsgBox("Não há itens na Ordem de Serviço!", vbInformation, "Atenção")

            Exit Sub

        End If

        Try

            OrdemServico.Fator = InputBox("Informe o Valor Multiplicador para fabricação, o Valor informado como
                                              padrão é a quantidade de
                                        conjuntos solicitados pelo PCP no ato do cadasro da Tag", "Fator de Multiplicação", lbltxtSaldoTag.Text)

            If OrdemServico.Fator <= 0 Then

                MsgBox("A operação foi cancelada. O valor informado não é válido!", vbInformation, "Atenção")
                Exit Sub

            End If

            If IsNumeric(OrdemServico.Fator) And OrdemServico.Fator > 0 Then

                cl_BancoDados.AbrirBanco()
                cl_CalculoBancoDados.CalcularordemservicoitemFator(OrdemServico.IdOrdemServico, OrdemServico.Fator)
                cl_BancoDados.FecharBanco()
                TimerDGVListaMaterialSW.Enabled = True

                dgvos.CurrentRow.Cells("Fator").Value = OrdemServico.Fator

                Dim diretorio As String = OrdemServico.EnderecoOrdemServico

                LimparDiretorio(diretorio & "\PDF")
                LimparDiretorio(diretorio & "\DXF")
                LimparDiretorio(diretorio & "\DFT")
                LimparDiretorio(diretorio & "\LXDS")

                ' cl_BancoDados.AlteracaoEspecifica("OrdemServico", "Fator", Replace(OrdemServico.Fator, ".", ","), "IdOrdemServico", OrdemServico.IdOrdemServico)

                Me.lblFator.Text = OrdemServico.Fator
                MsgBox("Fator alterado com sucesso!", vbInformation, "Atenção")
            Else
                MsgBox("O valor informado não e um numero Valido")
            End If
        Catch ex As Exception
        Finally
        End Try

        cl_CalculoBancoDados.CalcularordemservicoitemFatorOS_TAG_PROJETO()

    End Sub

    Private Sub CriarUmCopiaDaOSSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CriarUmCopiaDaOSSelecionadaToolStripMenuItem.Click

        Dim NovoIdOrdemServicoDB As Integer

        Dim result As DialogResult = MessageBox.Show("Deseja criar um nova OS, com base na OS: " & Me.lblOrdemServicoAtiva.Text, "Criando Copia da OS corrente", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then

            If OrdemServico.IdOrdemServico = 0 Or OrdemServico.IdOrdemServico.ToString = "" Or OrdemServico.IdOrdemServico = Nothing Then
                MsgBox("Nao uma OS valida!", vbCritical)
                Exit Sub
            End If

            If DGVListaMaterialSW.Rows.Count <= 0 Then
                MsgBox("Nao há itens a serem copiados", vbCritical, "Atenção")
                Exit Sub

            End If

            If My.Settings.EnderecoPastaRaizOS.ToString = "" And System.IO.Directory.Exists(My.Settings.EnderecoPastaRaizOS) = False Then

                MsgBox("O endereço onde será criado a pasta da Ordem de Serviço  não foi informado!")
                Exit Sub
            Else

                OrdemServico.Projeto = Me.cboProjeto.Text.ToUpper
                OrdemServico.Tag = Me.cboTag.Text.ToUpper
                OrdemServico.Descricao = Me.txtDescricao.Text.ToUpper & "Esta e um OS copia da OS de referencia Numero: " & OrdemServico.IdOrdemServico
                OrdemServico.CriadoPor = Usuario.NomeCompleto
                OrdemServico.DataCriacao = Date.Now
                OrdemServico.Estatus = "A".ToUpper
                OrdemServico.idProjeto = OrdemServico.idProjeto
                OrdemServico.idTag = OrdemServico.idTag
                OrdemServico.DescEmpresa = txtCliente.Text

                Dim idosRetono As String

                Try
                    cl_BancoDados.RetornaCampoDaPesquisa("SELECT max(IdOrdemServico)  as NovoIdOrdemServico FROM  " & ComplementoTipoBanco & "ordemservico", "NovoIdOrdemServico")
                    NovoIdOrdemServicoDB = Convert.ToInt32(VCampo0) + 1
                    NovoIdOrdemServico = cl_BancoDados.FormatarPara5Caracteres(NovoIdOrdemServicoDB.ToString())
                Catch ex As Exception

                    ' Em caso de erro, atribuir "00001" como valor inicial
                    NovoIdOrdemServico = "00001"

                End Try

                OrdemServico.EnderecoOrdemServico = Replace((My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico).ToString.ToUpper, "\", "##")

                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico)
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\DXF")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PDF")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\DFT")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PUNC")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\LASER")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\Projeto")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PEÇAS DE ESTOQUE")
                System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\LXDS")

                cl_BancoDados.Salvar("insert into ordemservico(idProjeto,
Projeto,
idTag,
Tag,
Descricao,
Fator,
EnderecoOrdemServico,
CriadoPor,
DataCriacao,
Estatus,
D_E_L_E_T_E,
DescEmpresa) values
('" & OrdemServico.idProjeto & "','" _
       & OrdemServico.Projeto & "','" _
       & OrdemServico.idTag & "','" _
       & OrdemServico.Tag & "','" _
       & OrdemServico.Descricao & "','" _
        & OrdemServico.Fator & "','" _
       & OrdemServico.EnderecoOrdemServico & "','" _
       & OrdemServico.CriadoPor.ToString().ToUpper() & "','" _
       & OrdemServico.DataCriacao & "','" _
       & OrdemServico.Estatus & "','','" _
       & OrdemServico.DescEmpresa & "')")

            End If

            ProgressBarProcessoLiberacaoOrdemServico.Minimum = 0
            ProgressBarProcessoLiberacaoOrdemServico.Maximum = DGVListaMaterialSW.Rows.Count

            If DGVListaMaterialSW.Rows.Count > 0 Then

                Dim fator As String

                For A As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

                    Try
                        OrdemServico.CodMatFabricante = DGVListaMaterialSW.Rows(A).Cells("CodMatFabricante").Value.ToString.ToUpper
                    Catch ex As Exception
                        OrdemServico.CodMatFabricante = ""
                    End Try

                    Try
                        OrdemServico.DescResumo = DGVListaMaterialSW.Rows(A).Cells("DescResumo").Value.ToString.ToUpper
                    Catch ex As Exception
                        OrdemServico.DescResumo = ""
                    End Try

                    Try
                        OrdemServico.DescDetal = DGVListaMaterialSW.Rows(A).Cells("DescDetal").Value.ToString.ToUpper
                    Catch ex As Exception

                        OrdemServico.DescDetal = ""
                    End Try

                    Try
                        OrdemServico.Autor = DGVListaMaterialSW.Rows(A).Cells("Autor").Value.ToString.ToUpper
                    Catch ex As Exception

                        OrdemServico.Autor = ""
                    End Try

                    Try
                        OrdemServico.Palavrachave = DGVListaMaterialSW.Rows(A).Cells("Palavrachave").Value.ToString.ToUpper
                    Catch ex As Exception

                        OrdemServico.Palavrachave = ""
                    End Try

                    Try
                        OrdemServico.Notas = DGVListaMaterialSW.Rows(A).Cells("Notas").Value.ToString.ToUpper
                    Catch ex As Exception

                        OrdemServico.Notas = ""
                    End Try

                    Try
                        OrdemServico.Espessura = DGVListaMaterialSW.Rows(A).Cells("Espessura").Value.ToString
                    Catch ex As Exception

                        OrdemServico.Espessura = ""
                    End Try

                    Try
                        OrdemServico.NumeroDobras = DGVListaMaterialSW.Rows(A).Cells("NumeroDobras").Value.ToString
                    Catch ex As Exception

                        OrdemServico.NumeroDobras = ""
                    End Try

                    If DGVListaMaterialSW.Rows(A).Cells("EnderecoArquivo").Value.ToString().IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then

                        OrdemServico.Unidade = "CONJ"
                        OrdemServico.UnidadeSW = "CONJ"
                    Else

                        OrdemServico.Unidade = "PC"
                        OrdemServico.UnidadeSW = "PC"

                    End If

                    OrdemServico.ValorSW = ""

                    Try
                        OrdemServico.Altura = Replace(DGVListaMaterialSW.Rows(A).Cells("Altura").Value.ToString, ".", ",")
                    Catch ex As Exception

                        OrdemServico.Altura = ""
                    End Try

                    Try
                        OrdemServico.Largura = Replace(DGVListaMaterialSW.Rows(A).Cells("Largura").Value.ToString, ".", ",")
                    Catch ex As Exception

                        OrdemServico.Largura = ""

                    End Try

                    OrdemServico.DtCad = ""
                    OrdemServico.UsuarioCriacao = ""
                    OrdemServico.UsuarioAlteracao = ""
                    OrdemServico.DtAlteracao = ""

                    Try
                        OrdemServico.EnderecoArquivo = DGVListaMaterialSW.Rows(A).Cells("EnderecoArquivo").Value.ToString.ToUpper
                    Catch ex As Exception

                        OrdemServico.EnderecoArquivo = ""

                    End Try

                    Try
                        OrdemServico.MaterialSW = DGVListaMaterialSW.Rows(A).Cells("MaterialSW").Value.ToString.ToUpper
                    Catch ex As Exception

                        OrdemServico.MaterialSW = ""

                    End Try

                    Try
                        OrdemServico.qtde = DGVListaMaterialSW.Rows(A).Cells("qtde").Value.ToString
                    Catch ex As Exception

                        OrdemServico.qtde = 0

                    End Try

                    Try
                        OrdemServico.AreaPintura = Replace(DGVListaMaterialSW.Rows(A).Cells("AreaPintura").Value.ToString, ".", ",")
                        OrdemServico.AreaPintura = OrdemServico.AreaPintura
                    Catch ex As Exception

                        OrdemServico.AreaPintura = ""

                    End Try

                    Try
                        OrdemServico.AreaPinturaUnitario = Replace(DGVListaMaterialSW.Rows(A).Cells("AreaPintura").Value.ToString, ".", ",")
                    Catch ex As Exception

                        OrdemServico.AreaPinturaUnitario = ""

                    End Try

                    Try
                        OrdemServico.Peso = Replace(DGVListaMaterialSW.Rows(A).Cells("Peso").Value.ToString, ".", ",")

                        OrdemServico.Peso = OrdemServico.Peso
                    Catch ex As Exception

                        OrdemServico.Peso = 0
                    End Try

                    Try
                        OrdemServico.PesoUnitario = Replace(DGVListaMaterialSW.Rows(A).Cells("Peso").Value.ToString, ".", ",")
                    Catch ex As Exception

                        OrdemServico.PesoUnitario = 0
                    End Try

                    Try
                        OrdemServico.txtSoldagem = DGVListaMaterialSW.Rows(A).Cells("txtSoldagem").Value.ToString
                    Catch ex As Exception

                        OrdemServico.txtSoldagem = ""

                    End Try

                    Try
                        OrdemServico.QtdeTotal = Replace(DGVListaMaterialSW.Rows(A).Cells("QtdeTotal").Value.ToString, ".", ",")
                        'OrdemServico.QtdeTotal = OrdemServico.qtde * Fator
                    Catch ex As Exception

                        OrdemServico.QtdeTotal = 0

                    End Try

                    Try
                        OrdemServico.txtTipoDesenho = DGVListaMaterialSW.Rows(A).Cells("txtTipoDesenho").Value.ToString
                    Catch ex As Exception

                        OrdemServico.txtTipoDesenho = ""

                    End Try

                    Try
                        OrdemServico.txtCorte = DGVListaMaterialSW.Rows(A).Cells("txtCorte").Value.ToString
                    Catch ex As Exception

                        OrdemServico.txtCorte = ""

                    End Try

                    Try
                        OrdemServico.txtDobra = DGVListaMaterialSW.Rows(A).Cells("txtDobra").Value.ToString
                    Catch ex As Exception

                        OrdemServico.txtDobra = ""

                    End Try

                    Try
                        OrdemServico.txtSolda = DGVListaMaterialSW.Rows(A).Cells("txtSolda").Value.ToString
                    Catch ex As Exception

                        OrdemServico.txtSolda = ""

                    End Try

                    Try
                        OrdemServico.txtPintura = DGVListaMaterialSW.Rows(A).Cells("txtPintura").Value.ToString
                    Catch ex As Exception

                        OrdemServico.txtPintura = ""

                    End Try

                    Try
                        OrdemServico.txtMontagem = DGVListaMaterialSW.Rows(A).Cells("txtMontagem").Value.ToString
                    Catch ex As Exception

                        OrdemServico.txtMontagem = ""

                    End Try

                    Try
                        OrdemServico.Comprimentocaixadelimitadora = DGVListaMaterialSW.Rows(A).Cells("Comprimentocaixadelimitadora").Value.ToString
                    Catch ex As Exception

                        OrdemServico.Comprimentocaixadelimitadora = ""

                    End Try

                    Try
                        OrdemServico.Larguracaixadelimitadora = DGVListaMaterialSW.Rows(A).Cells("Larguracaixadelimitadora").Value.ToString
                    Catch ex As Exception

                        OrdemServico.Larguracaixadelimitadora = ""

                    End Try

                    Try
                        OrdemServico.Espessuracaixadelimitadora = DGVListaMaterialSW.Rows(A).Cells("Espessuracaixadelimitadora ").Value.ToString
                    Catch ex As Exception

                        OrdemServico.Espessuracaixadelimitadora = ""

                    End Try

                    Try
                        OrdemServico.txtItemEstoque = DGVListaMaterialSW.Rows(A).Cells("txtItemEstoque").Value.ToString.ToUpper
                    Catch ex As Exception

                        OrdemServico.txtItemEstoque = ""

                    End Try

                    Try
                        OrdemServico.txtAcabamento = DGVListaMaterialSW.Rows(A).Cells("Acabamento").Value.ToString.ToUpper
                    Catch ex As Exception

                        OrdemServico.txtAcabamento = ""

                    End Try

                    Try
                        fator = DGVListaMaterialSW.Rows(A).Cells("fator").Value.ToString
                    Catch ex As Exception

                        fator = 0

                    End Try

                    Try
                        OrdemServico.ProdutoPrincipal = DGVListaMaterialSW.Rows(A).Cells("ProdutoPrincipal").Value.ToString
                    Catch ex As Exception

                        OrdemServico.ProdutoPrincipal = ""

                    End Try

                    OrdemServico.IdOrdemServico = NovoIdOrdemServicoDB

                    ProgressBarProcessoLiberacaoOrdemServico.Value = A

                    Dim query As String = "INSERT INTO ordemservicoitem (
							IdOrdemServico, idProjeto, Projeto, idTag, Tag,
							ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal,
							Autor, Palavrachave, Notas, Espessura, AreaPintura,
							NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura,
							Largura, CodMatFabricante, DtCad, UsuarioCriacao,
							UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW,
							QtdeTotal,CriadoPor,
							DataCriacao, Estatus, Acabamento, D_E_L_E_T_E, fator, qtde,
							txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda,
							txtPintura, txtMontagem, tttxtCorte, tttxtDobra, tttxtSolda,
							tttxtPintura, tttxtMontagem, Comprimentocaixadelimitadora,
							Larguracaixadelimitadora, Espessuracaixadelimitadora,
							AreaPinturaUnitario, PesoUnitario, txtItemEstoque,OrdemServicoItemFinalizado,ProdutoPrincipal
						   ) VALUES (
							@IdOrdemServico, @idProjeto, @Projeto, @idTag, @Tag,
							@ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal,
							@Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura,
							@NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura,
							@Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao,
							@UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW,
							@QtdeTotal, @CriadoPor,
							@DataCriacao, @Estatus, @Acabamento, @D_E_L_E_T_E, @fator, @qtde,
							@txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda,
							@txtPintura, @txtMontagem, @tttxtCorte, @tttxtDobra, @tttxtSolda,
							@tttxtPintura, @tttxtMontagem, @Comprimentocaixadelimitadora,
							@Larguracaixadelimitadora, @Espessuracaixadelimitadora,
							@AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque,@OrdemServicoItemFinalizado,@ProdutoPrincipal
						   );"

                    Using command As New MySqlCommand(query, myconect)
                        ' Adicionando os parâmetros
                        command.Parameters.AddWithValue("@IdOrdemServico", OrdemServico.IdOrdemServico)
                        command.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
                        command.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                        command.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
                        command.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                        command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.Estatus)
                        command.Parameters.AddWithValue("@IdMaterial", OrdemServico.IdMaterial)
                        command.Parameters.AddWithValue("@DescResumo", OrdemServico.DescResumo)
                        command.Parameters.AddWithValue("@DescDetal", OrdemServico.DescDetal)
                        command.Parameters.AddWithValue("@Autor", OrdemServico.Autor)
                        command.Parameters.AddWithValue("@Palavrachave", OrdemServico.Palavrachave)
                        command.Parameters.AddWithValue("@Notas", OrdemServico.Notas)
                        command.Parameters.AddWithValue("@Espessura", OrdemServico.Espessura)
                        command.Parameters.AddWithValue("@AreaPintura", OrdemServico.AreaPintura)
                        command.Parameters.AddWithValue("@NumeroDobras", OrdemServico.NumeroDobras)
                        command.Parameters.AddWithValue("@Peso", Replace(OrdemServico.Peso, ",", "."))
                        command.Parameters.AddWithValue("@Unidade", OrdemServico.Unidade)
                        command.Parameters.AddWithValue("@UnidadeSW", OrdemServico.UnidadeSW)
                        command.Parameters.AddWithValue("@ValorSW", Replace(OrdemServico.ValorSW, ",", "."))
                        command.Parameters.AddWithValue("@Altura", Replace(OrdemServico.Altura, ",", "."))
                        command.Parameters.AddWithValue("@Largura", Replace(OrdemServico.Largura, ",", "."))
                        command.Parameters.AddWithValue("@CodMatFabricante", OrdemServico.CodMatFabricante)
                        command.Parameters.AddWithValue("@DtCad", "")
                        command.Parameters.AddWithValue("@UsuarioCriacao", "")
                        command.Parameters.AddWithValue("@UsuarioAlteracao", "")
                        command.Parameters.AddWithValue("@DtAlteracao", "")
                        command.Parameters.AddWithValue("@EnderecoArquivo", OrdemServico.EnderecoArquivo)
                        command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSW)
                        command.Parameters.AddWithValue("@QtdeTotal", Replace(OrdemServico.QtdeTotal, ",", "."))
                        'command.Parameters.AddWithValue("@QtdeProduzida", "")
                        'command.Parameters.AddWithValue("@QtdeFaltante", "")
                        command.Parameters.AddWithValue("@CriadoPor", Usuario.NomeCompleto.ToString)
                        command.Parameters.AddWithValue("@DataCriacao", Date.Now)
                        command.Parameters.AddWithValue("@Estatus", "A")
                        command.Parameters.AddWithValue("@Acabamento", OrdemServico.txtAcabamento)
                        command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                        command.Parameters.AddWithValue("@fator", OrdemServico.Fator)
                        command.Parameters.AddWithValue("@qtde", OrdemServico.qtde)
                        command.Parameters.AddWithValue("@txtSoldagem", OrdemServico.txtSoldagem)
                        command.Parameters.AddWithValue("@txtTipoDesenho", OrdemServico.txtTipoDesenho)
                        command.Parameters.AddWithValue("@txtCorte", OrdemServico.txtCorte)
                        command.Parameters.AddWithValue("@txtDobra", OrdemServico.txtDobra)
                        command.Parameters.AddWithValue("@txtSolda", OrdemServico.txtSolda)
                        command.Parameters.AddWithValue("@txtPintura", OrdemServico.txtPintura)
                        command.Parameters.AddWithValue("@txtMontagem", OrdemServico.txtMontagem)
                        command.Parameters.AddWithValue("@tttxtCorte", OrdemServico.tttxtCorte)
                        command.Parameters.AddWithValue("@tttxtDobra", OrdemServico.tttxtDobra)
                        command.Parameters.AddWithValue("@tttxtSolda", OrdemServico.tttxtSolda)
                        command.Parameters.AddWithValue("@tttxtPintura", OrdemServico.tttxtPintura)
                        command.Parameters.AddWithValue("@tttxtMontagem", OrdemServico.tttxtMontagem)
                        command.Parameters.AddWithValue("@Comprimentocaixadelimitadora", Replace(OrdemServico.Comprimentocaixadelimitadora, ",", "."))
                        command.Parameters.AddWithValue("@Larguracaixadelimitadora", Replace(OrdemServico.Larguracaixadelimitadora, ",", "."))
                        command.Parameters.AddWithValue("@Espessuracaixadelimitadora", Replace(OrdemServico.Espessuracaixadelimitadora, ",", "."))
                        command.Parameters.AddWithValue("@AreaPinturaUnitario", Replace(OrdemServico.AreaPinturaUnitario, ",", "."))
                        command.Parameters.AddWithValue("@PesoUnitario", Replace(OrdemServico.PesoUnitario, ",", "."))
                        command.Parameters.AddWithValue("@txtItemEstoque", OrdemServico.txtItemEstoque)
                        command.Parameters.AddWithValue("@ProdutoPrincipal", OrdemServico.ProdutoPrincipal)
                        command.Parameters.AddWithValue("@OrdemServicoItemFinalizado", "")

                        ' Abrir conexão e executar comando

                        Try
                            command.ExecuteNonQuery()
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try

                    End Using

                Next

            End If

            Timerdgvos.Enabled = True
            TimerDGVListaMaterialSW.Enabled = True

            For i As Integer = 0 To dgvos.Rows.Count = 1

                If NovoIdOrdemServicoDB = dgvos.Rows(i).Cells("IdOrdemServico").Value Then

                    dgvos.Rows(i).Selected = True
                    dgvos.Rows(i).DefaultCellStyle.BackColor = Color.Yellow ' Define a cor de fundo para destacar a linha
                    dgvos.FirstDisplayedScrollingRowIndex = i ' Rola o DataGridView até a linha selecionada

                    Exit Sub

                End If

            Next

            ProgressBarProcessoLiberacaoOrdemServico.Value = 0

            MsgBox("Operação finalizada com sucesso, os itens foram inseridos na OS: " & OrdemServico.IdOrdemServico & "!," & vbCrLf &
                              " porem não foram importados os aquivos para OS.", vbInformation, "Criando OS")

        End If

        cl_CalculoBancoDados.CalcularordemservicoitemFatorOS_TAG_PROJETO()

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)

        ' Verifique se o modelo foi aberto com sucesso
        If Not swModel Is Nothing Then

            ' Usa Select Case para diferenciar o tipo do documento
            If swModel.GetType() = swDocumentTypes_e.swDocPART Then

                Dim result As DialogResult = MessageBox.Show("Deseja Realmente Inserir o desenho corrente na OS: " & Me.lblOrdemServicoAtiva.Text, "Inserção de Item na OS", MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then

                    ' Dim rnc As String

                    Dim novaqtde As String = 0

                    Dim PecaNova As Boolean = False

                    novaqtde = InputBox("Informe a quantidade total de peças a serem fabricadas", "Quantidade Total", 1)

                    ' Verifica se o usuário clicou em "Cancelar" (Fator será uma string vazia)
                    If novaqtde = "" Or novaqtde <= 0 Then

                        MsgBox("A Operação foi cancelda", vbInformation, "Atenação")

                        Exit Sub ' Sai do procedimento
                    End If

                    If DGVListaMaterialSW.Rows.Count > 0 Then

                        ' If DGVListaMaterialSW.Rows.Count >= 1 Then

                        For b As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

                            If DadosArquivoCorrente.NomeArquivoSemExtensao = DGVListaMaterialSW.Rows(b).Cells("CodMatFabricante").Value.ToString Then

                                Dim Peso, AreaPintura, fator, IDOrdemServicoItem As String

                                IDOrdemServicoItem = DGVListaMaterialSW.Rows(b).Cells("IDOrdemServicoItem").Value.ToString

                                Try
                                    Peso = DadosArquivoCorrente.Massa
                                    Peso = Replace((Peso), ".", ",")
                                    Peso = Peso * novaqtde
                                    ' Peso = Replace(Peso, ",", ".")
                                Catch ex As Exception
                                    Peso = 0
                                End Try

                                fator = DGVListaMaterialSW.Rows(b).Cells("fator").Value

                                Try

                                    AreaPintura = DadosArquivoCorrente.AreaPintura
                                    AreaPintura = AreaPintura * novaqtde
                                    ' AreaPintura = Replace(AreaPintura, ",", ".")
                                Catch ex As Exception
                                    AreaPintura = 0
                                End Try

                                'AreaPintura = (DGVListaMaterialSW.Rows(b).Cells("AreaPinturaUnitaria").Value * novaqtde)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "QtdeTotal", novaqtde, "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "qtde", novaqtde, "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "AreaPintura", Replace((AreaPintura), ",", "."), "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Peso", Replace((Peso), ",", "."), "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "Fator", fator, "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "PesoUnitario", Replace((DadosArquivoCorrente.Massa), ",", "."), "IDOrdemServicoItem", IDOrdemServicoItem)

                                cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "AreaPinturaUnitario", Replace((DadosArquivoCorrente.AreaPintura), ",", "."), "IDOrdemServicoItem", IDOrdemServicoItem)

                                DGVListaMaterialSW.Rows(b).Cells("QtdeTotal").Value = novaqtde

                                DGVListaMaterialSW.Rows(b).Cells("qtde").Value = novaqtde

                                DGVListaMaterialSW.Rows(b).Cells("AreaPintura").Value = Replace((AreaPintura), ",", ".")

                                DGVListaMaterialSW.Rows(b).Cells("Peso").Value = Replace((Peso), ",", ".")

                                DGVListaMaterialSW.Rows(b).Cells("AreaPinturaUnitario").Value = Replace((DadosArquivoCorrente.AreaPintura), ",", ".")

                                DGVListaMaterialSW.Rows(b).Cells("PesoUnitario").Value = Replace((DadosArquivoCorrente.Massa), ",", ".")

                                DGVListaMaterialSW.Rows(b).Cells("Fator").Value = fator

                                DGVListaMaterialSW.Rows(b).Cells("QtdeTotal").Style.BackColor = Color.LightGreen

                                DGVListaMaterialSW.Rows(b).Cells("qtde").Style.BackColor = Color.LightGreen

                                DGVListaMaterialSW.Rows(b).Cells("AreaPintura").Style.BackColor = Color.LightGreen

                                DGVListaMaterialSW.Rows(b).Cells("Peso").Style.BackColor = Color.LightGreen

                                DGVListaMaterialSW.Rows(b).Cells("Fator").Style.BackColor = Color.LightGreen

                                DGVListaMaterialSW.Rows(b).Cells("AreaPinturaUnitario").Style.BackColor = Color.LightGreen

                                DGVListaMaterialSW.Rows(b).Cells("PesoUnitario").Style.BackColor = Color.LightGreen

                                PecaNova = False

                                DGVListaMaterialSW.Rows(b).Selected = True
                                DGVListaMaterialSW.Rows(b).DefaultCellStyle.BackColor = Color.Yellow ' Define a cor de fundo para destacar a linha
                                DGVListaMaterialSW.FirstDisplayedScrollingRowIndex = b ' Rola o DataGridView até a linha selecionada

                                Exit Sub

                            End If

                            PecaNova = True

                        Next
                    Else

                        PecaNova = True

                        If PecaNova = True Then

                            Try
                                OrdemServico.CodMatFabricante = DadosArquivoCorrente.NomeArquivoSemExtensao
                            Catch ex As Exception
                                OrdemServico.CodMatFabricante = ""
                            End Try

                            Try
                                OrdemServico.DescResumo = DadosArquivoCorrente.Titulo
                            Catch ex As Exception
                                OrdemServico.DescResumo = ""
                            End Try

                            Try
                                OrdemServico.DescDetal = DadosArquivoCorrente.AssuntoSubiTitulo
                            Catch ex As Exception

                                OrdemServico.DescDetal = ""
                            End Try

                            Try
                                OrdemServico.Autor = DadosArquivoCorrente.Author
                            Catch ex As Exception

                                OrdemServico.Autor = ""
                            End Try

                            Try
                                OrdemServico.Palavrachave = DadosArquivoCorrente.PalavraChave
                            Catch ex As Exception

                                OrdemServico.Palavrachave = ""
                            End Try

                            Try
                                OrdemServico.Notas = DadosArquivoCorrente.Comentarios
                            Catch ex As Exception

                                OrdemServico.Notas = ""
                            End Try

                            Try
                                OrdemServico.Espessura = DadosArquivoCorrente.Espessura
                            Catch ex As Exception

                                OrdemServico.Espessura = ""
                            End Try

                            Try
                                OrdemServico.NumeroDobras = DadosArquivoCorrente.NumeroDobras
                            Catch ex As Exception

                                OrdemServico.NumeroDobras = ""
                            End Try

                            Try
                                OrdemServico.EnderecoArquivo = DadosArquivoCorrente.EnderecoArquivo.ToString.ToUpper
                            Catch ex As Exception
                                OrdemServico.EnderecoArquivo = ""
                            End Try

                            Try
                                OrdemServico.MaterialSW = DadosArquivoCorrente.material.ToString.ToUpper
                            Catch ex As Exception
                                OrdemServico.MaterialSW = ""
                            End Try

                            If DadosArquivoCorrente.EnderecoArquivo.ToString.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then

                                OrdemServico.Unidade = "CONJ"
                                OrdemServico.UnidadeSW = "CONJ"
                            Else

                                OrdemServico.Unidade = "PC"
                                OrdemServico.UnidadeSW = "PC"

                            End If

                            OrdemServico.DtCad = ""
                            OrdemServico.UsuarioCriacao = ""
                            OrdemServico.UsuarioAlteracao = ""
                            OrdemServico.DtAlteracao = ""

                            Try
                                OrdemServico.qtde = novaqtde
                            Catch ex As Exception

                                OrdemServico.qtde = novaqtde

                            End Try

                            Try

                                DadosArquivoCorrente.AreaPintura = Replace(DadosArquivoCorrente.AreaPintura, ".", ",")
                                OrdemServico.AreaPintura = DadosArquivoCorrente.AreaPintura * novaqtde
                                OrdemServico.AreaPintura = Replace(OrdemServico.AreaPintura, ".", ",")
                            Catch ex As Exception

                                OrdemServico.AreaPintura = ""

                            End Try

                            Try

                                ' OrdemServico.Peso = DadosArquivoCorrente.Massa
                                DadosArquivoCorrente.Massa = Replace(DadosArquivoCorrente.Massa, ".", ",")
                                OrdemServico.Peso = DadosArquivoCorrente.Massa * novaqtde
                                OrdemServico.Peso = Replace(OrdemServico.Peso, ",", ".")
                            Catch ex As Exception

                                OrdemServico.Peso = 0
                            End Try

                            Try
                                OrdemServico.PesoUnitario = DadosArquivoCorrente.Massa
                                OrdemServico.PesoUnitario = Replace(DadosArquivoCorrente.Massa, ",", ",")
                            Catch ex As Exception

                                OrdemServico.PesoUnitario = 0
                            End Try

                            Try
                                OrdemServico.txtSoldagem = DadosArquivoCorrente.soldagem
                            Catch ex As Exception

                                OrdemServico.txtSoldagem = ""

                            End Try

                            Try
                                OrdemServico.QtdeTotal = novaqtde
                            Catch ex As Exception

                                OrdemServico.QtdeTotal = 0

                            End Try

                            Try
                                OrdemServico.txtTipoDesenho = DadosArquivoCorrente.TipoDesenho
                            Catch ex As Exception

                                OrdemServico.txtTipoDesenho = ""

                            End Try

                            Try
                                OrdemServico.txtCorte = DadosArquivoCorrente.Corte
                            Catch ex As Exception

                                OrdemServico.txtCorte = ""

                            End Try

                            Try
                                OrdemServico.txtDobra = DadosArquivoCorrente.Dobra
                            Catch ex As Exception

                                OrdemServico.txtDobra = ""

                            End Try

                            Try
                                OrdemServico.txtSolda = DadosArquivoCorrente.Solda
                            Catch ex As Exception

                                OrdemServico.txtSolda = ""

                            End Try

                            Try
                                OrdemServico.txtPintura = DadosArquivoCorrente.Pintura
                            Catch ex As Exception

                                OrdemServico.txtPintura = ""

                            End Try

                            Try
                                OrdemServico.txtMontagem = DadosArquivoCorrente.Montagem
                            Catch ex As Exception

                                OrdemServico.txtMontagem = ""

                            End Try

                            Try
                                OrdemServico.Comprimentocaixadelimitadora = DadosArquivoCorrente.Alturacaixadelimitadora
                            Catch ex As Exception

                                OrdemServico.Comprimentocaixadelimitadora = ""

                            End Try

                            Try
                                OrdemServico.Larguracaixadelimitadora = DadosArquivoCorrente.Larguracaixadelimitadora
                            Catch ex As Exception

                                OrdemServico.Larguracaixadelimitadora = ""

                            End Try

                            Try
                                OrdemServico.Espessuracaixadelimitadora = DadosArquivoCorrente.Profundidadeaixadelimitadora
                            Catch ex As Exception

                                OrdemServico.Espessuracaixadelimitadora = ""

                            End Try

                            Try
                                OrdemServico.txtItemEstoque = DadosArquivoCorrente.ItemEstoque
                            Catch ex As Exception

                                OrdemServico.txtItemEstoque = ""

                            End Try

                            Try
                                OrdemServico.txtAcabamento = DadosArquivoCorrente.Acabamento
                            Catch ex As Exception

                                OrdemServico.txtAcabamento = ""

                            End Try

                            OrdemServico.PesoUnitario = Replace(DadosArquivoCorrente.Massa, ",", ".")

                            OrdemServico.AreaPinturaUnitario = Replace(DadosArquivoCorrente.AreaPintura, ",", ".")

                            ImportarPDFParaOSIndividual(OrdemServico.EnderecoArquivo, novaqtde)

                            Dim query As String = "INSERT INTO ordemservicoitem (
							IdOrdemServico, idProjeto, Projeto, idTag, Tag,
							ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal,
							Autor, Palavrachave, Notas, Espessura, AreaPintura,
							NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura,
							Largura, CodMatFabricante, DtCad, UsuarioCriacao,
							UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW,
							QtdeTotal, CriadoPor,
							DataCriacao, Estatus, Acabamento, D_E_L_E_T_E, fator, qtde,
							txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda,
							txtPintura, txtMontagem, tttxtCorte, tttxtDobra, tttxtSolda,
							tttxtPintura, tttxtMontagem, Comprimentocaixadelimitadora,
							Larguracaixadelimitadora, Espessuracaixadelimitadora,
							AreaPinturaUnitario, PesoUnitario, txtItemEstoque
						   ) VALUES (
							@IdOrdemServico, @idProjeto, @Projeto, @idTag, @Tag,
							@ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal,
							@Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura,
							@NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura,
							@Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao,
							@UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW,
							@QtdeTotal, @CriadoPor,
							@DataCriacao, @Estatus, @Acabamento, @D_E_L_E_T_E, @fator, @qtde,
							@txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda,
							@txtPintura, @txtMontagem, @tttxtCorte, @tttxtDobra, @tttxtSolda,
							@tttxtPintura, @tttxtMontagem, @Comprimentocaixadelimitadora,
							@Larguracaixadelimitadora, @Espessuracaixadelimitadora,
							@AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque
						   );"

                            Using command As New MySqlCommand(query, myconect)
                                ' Adicionando os parâmetros
                                command.Parameters.AddWithValue("@IdOrdemServico", OrdemServico.IdOrdemServico)
                                command.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
                                command.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                                command.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
                                command.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                                command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.Estatus)
                                command.Parameters.AddWithValue("@IdMaterial", OrdemServico.IdMaterial)
                                command.Parameters.AddWithValue("@DescResumo", OrdemServico.DescResumo)
                                command.Parameters.AddWithValue("@DescDetal", OrdemServico.DescDetal)
                                command.Parameters.AddWithValue("@Autor", OrdemServico.Autor)
                                command.Parameters.AddWithValue("@Palavrachave", OrdemServico.Palavrachave)
                                command.Parameters.AddWithValue("@Notas", OrdemServico.Notas)
                                command.Parameters.AddWithValue("@Espessura", OrdemServico.Espessura)
                                command.Parameters.AddWithValue("@AreaPintura", OrdemServico.AreaPintura)
                                command.Parameters.AddWithValue("@NumeroDobras", OrdemServico.NumeroDobras)
                                command.Parameters.AddWithValue("@Peso", Replace(OrdemServico.Peso, ",", "."))
                                command.Parameters.AddWithValue("@Unidade", OrdemServico.Unidade)
                                command.Parameters.AddWithValue("@UnidadeSW", OrdemServico.UnidadeSW)
                                command.Parameters.AddWithValue("@ValorSW", OrdemServico.ValorSW)
                                command.Parameters.AddWithValue("@Altura", OrdemServico.Altura)
                                command.Parameters.AddWithValue("@Largura", OrdemServico.Largura)
                                command.Parameters.AddWithValue("@CodMatFabricante", OrdemServico.CodMatFabricante)
                                command.Parameters.AddWithValue("@DtCad", "")
                                command.Parameters.AddWithValue("@UsuarioCriacao", "")
                                command.Parameters.AddWithValue("@UsuarioAlteracao", "")
                                command.Parameters.AddWithValue("@DtAlteracao", "")
                                command.Parameters.AddWithValue("@EnderecoArquivo", OrdemServico.EnderecoArquivo)
                                command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSW)
                                command.Parameters.AddWithValue("@QtdeTotal", Replace(OrdemServico.QtdeTotal, ",", "."))
                                'command.Parameters.AddWithValue("@QtdeProduzida", "")
                                'command.Parameters.AddWithValue("@QtdeFaltante", "")
                                command.Parameters.AddWithValue("@CriadoPor", Usuario.NomeCompleto.ToString)
                                command.Parameters.AddWithValue("@DataCriacao", Date.Now)
                                command.Parameters.AddWithValue("@Estatus", "A")
                                command.Parameters.AddWithValue("@Acabamento", OrdemServico.txtAcabamento)
                                command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                                command.Parameters.AddWithValue("@fator", 1)
                                command.Parameters.AddWithValue("@qtde", Replace(OrdemServico.qtde, ",", "."))
                                command.Parameters.AddWithValue("@txtSoldagem", OrdemServico.txtSoldagem)
                                command.Parameters.AddWithValue("@txtTipoDesenho", OrdemServico.txtTipoDesenho)
                                command.Parameters.AddWithValue("@txtCorte", OrdemServico.txtCorte)
                                command.Parameters.AddWithValue("@txtDobra", OrdemServico.txtDobra)
                                command.Parameters.AddWithValue("@txtSolda", OrdemServico.txtSolda)
                                command.Parameters.AddWithValue("@txtPintura", OrdemServico.txtPintura)
                                command.Parameters.AddWithValue("@txtMontagem", OrdemServico.txtMontagem)
                                command.Parameters.AddWithValue("@tttxtCorte", OrdemServico.tttxtCorte)
                                command.Parameters.AddWithValue("@tttxtDobra", OrdemServico.tttxtDobra)
                                command.Parameters.AddWithValue("@tttxtSolda", OrdemServico.tttxtSolda)
                                command.Parameters.AddWithValue("@tttxtPintura", OrdemServico.tttxtPintura)
                                command.Parameters.AddWithValue("@tttxtMontagem", OrdemServico.tttxtMontagem)
                                command.Parameters.AddWithValue("@Comprimentocaixadelimitadora", OrdemServico.Comprimentocaixadelimitadora)
                                command.Parameters.AddWithValue("@Larguracaixadelimitadora", OrdemServico.Larguracaixadelimitadora)
                                command.Parameters.AddWithValue("@Espessuracaixadelimitadora", OrdemServico.Espessuracaixadelimitadora)
                                command.Parameters.AddWithValue("@AreaPinturaUnitario", Replace(OrdemServico.AreaPinturaUnitario, ",", "."))
                                command.Parameters.AddWithValue("@PesoUnitario", Replace(OrdemServico.PesoUnitario, ",", "."))
                                command.Parameters.AddWithValue("@txtItemEstoque", OrdemServico.txtItemEstoque)

                                ' Abrir conexão e executar comando

                                'If cl_BancoDados.AbrirBanco = False Then
                                '    cl_BancoDados.AbrirBanco()

                                'End If
                                'myconect.Open()
                                command.ExecuteNonQuery()
                            End Using
                            TimerDGVListaMaterialSW.Enabled = True
                        End If

                        PecaNova = False

                    End If

                    ' PecaNova = True

                    If PecaNova = True Then

                        Try
                            OrdemServico.CodMatFabricante = DadosArquivoCorrente.NomeArquivoSemExtensao
                        Catch ex As Exception
                            OrdemServico.CodMatFabricante = ""
                        End Try

                        Try
                            OrdemServico.DescResumo = DadosArquivoCorrente.Titulo
                        Catch ex As Exception
                            OrdemServico.DescResumo = ""
                        End Try

                        Try
                            OrdemServico.DescDetal = DadosArquivoCorrente.AssuntoSubiTitulo
                        Catch ex As Exception

                            OrdemServico.DescDetal = ""
                        End Try

                        Try
                            OrdemServico.Autor = DadosArquivoCorrente.Author
                        Catch ex As Exception

                            OrdemServico.Autor = ""
                        End Try

                        Try
                            OrdemServico.Palavrachave = DadosArquivoCorrente.PalavraChave
                        Catch ex As Exception

                            OrdemServico.Palavrachave = ""
                        End Try

                        Try
                            OrdemServico.Notas = DadosArquivoCorrente.Comentarios
                        Catch ex As Exception

                            OrdemServico.Notas = ""
                        End Try

                        Try
                            OrdemServico.Espessura = DadosArquivoCorrente.Espessura
                        Catch ex As Exception

                            OrdemServico.Espessura = ""
                        End Try

                        Try
                            OrdemServico.NumeroDobras = DadosArquivoCorrente.NumeroDobras
                        Catch ex As Exception

                            OrdemServico.NumeroDobras = ""
                        End Try

                        Try
                            OrdemServico.EnderecoArquivo = DadosArquivoCorrente.EnderecoArquivo.ToString.ToUpper
                        Catch ex As Exception
                            OrdemServico.EnderecoArquivo = ""
                        End Try

                        Try
                            OrdemServico.MaterialSW = DadosArquivoCorrente.material.ToString.ToUpper
                        Catch ex As Exception
                            OrdemServico.MaterialSW = ""
                        End Try

                        If DadosArquivoCorrente.EnderecoArquivo.ToString.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then

                            OrdemServico.Unidade = "CONJ"
                            OrdemServico.UnidadeSW = "CONJ"
                        Else

                            OrdemServico.Unidade = "PC"
                            OrdemServico.UnidadeSW = "PC"

                        End If

                        OrdemServico.DtCad = ""
                        OrdemServico.UsuarioCriacao = ""
                        OrdemServico.UsuarioAlteracao = ""
                        OrdemServico.DtAlteracao = ""

                        Try
                            OrdemServico.qtde = novaqtde
                        Catch ex As Exception

                            OrdemServico.qtde = novaqtde

                        End Try

                        Try

                            DadosArquivoCorrente.AreaPintura = Replace(DadosArquivoCorrente.AreaPintura, ".", ",")
                            OrdemServico.AreaPintura = DadosArquivoCorrente.AreaPintura * novaqtde
                            OrdemServico.AreaPintura = Replace(OrdemServico.AreaPintura, ".", ",")
                        Catch ex As Exception

                            OrdemServico.AreaPintura = ""

                        End Try

                        Try

                            ' OrdemServico.Peso = DadosArquivoCorrente.Massa
                            DadosArquivoCorrente.Massa = Replace(DadosArquivoCorrente.Massa, ".", ",")
                            OrdemServico.Peso = DadosArquivoCorrente.Massa * novaqtde
                            OrdemServico.Peso = Replace(OrdemServico.Peso, ",", ".")
                        Catch ex As Exception

                            OrdemServico.Peso = 0
                        End Try

                        Try
                            OrdemServico.PesoUnitario = DadosArquivoCorrente.Massa
                            OrdemServico.PesoUnitario = Replace(DadosArquivoCorrente.Massa, ",", ",")
                        Catch ex As Exception

                            OrdemServico.PesoUnitario = 0
                        End Try

                        Try
                            OrdemServico.txtSoldagem = DadosArquivoCorrente.soldagem
                        Catch ex As Exception

                            OrdemServico.txtSoldagem = ""

                        End Try

                        Try
                            OrdemServico.QtdeTotal = novaqtde
                        Catch ex As Exception

                            OrdemServico.QtdeTotal = 0

                        End Try

                        Try
                            OrdemServico.txtTipoDesenho = DadosArquivoCorrente.TipoDesenho
                        Catch ex As Exception

                            OrdemServico.txtTipoDesenho = ""

                        End Try

                        Try
                            OrdemServico.txtCorte = DadosArquivoCorrente.Corte
                        Catch ex As Exception

                            OrdemServico.txtCorte = ""

                        End Try

                        Try
                            OrdemServico.txtDobra = DadosArquivoCorrente.Dobra
                        Catch ex As Exception

                            OrdemServico.txtDobra = ""

                        End Try

                        Try
                            OrdemServico.txtSolda = DadosArquivoCorrente.Solda
                        Catch ex As Exception

                            OrdemServico.txtSolda = ""

                        End Try

                        Try
                            OrdemServico.txtPintura = DadosArquivoCorrente.Pintura
                        Catch ex As Exception

                            OrdemServico.txtPintura = ""

                        End Try

                        Try
                            OrdemServico.txtMontagem = DadosArquivoCorrente.Montagem
                        Catch ex As Exception

                            OrdemServico.txtMontagem = ""

                        End Try

                        Try
                            OrdemServico.Comprimentocaixadelimitadora = DadosArquivoCorrente.Alturacaixadelimitadora
                        Catch ex As Exception

                            OrdemServico.Comprimentocaixadelimitadora = ""

                        End Try

                        Try
                            OrdemServico.Larguracaixadelimitadora = DadosArquivoCorrente.Larguracaixadelimitadora
                        Catch ex As Exception

                            OrdemServico.Larguracaixadelimitadora = ""

                        End Try

                        Try
                            OrdemServico.Espessuracaixadelimitadora = DadosArquivoCorrente.Profundidadeaixadelimitadora
                        Catch ex As Exception

                            OrdemServico.Espessuracaixadelimitadora = ""

                        End Try

                        Try
                            OrdemServico.txtItemEstoque = DadosArquivoCorrente.ItemEstoque
                        Catch ex As Exception

                            OrdemServico.txtItemEstoque = ""

                        End Try

                        Try
                            OrdemServico.txtAcabamento = DadosArquivoCorrente.Acabamento
                        Catch ex As Exception

                            OrdemServico.txtAcabamento = ""

                        End Try

                        OrdemServico.PesoUnitario = Replace(DadosArquivoCorrente.Massa, ",", ".")

                        OrdemServico.AreaPinturaUnitario = Replace(DadosArquivoCorrente.AreaPintura, ",", ".")

                        '''''ImportarPDFParaOSIndividual(OrdemServico.EnderecoArquivo, novaqtde)

                        Dim query As String = "INSERT INTO ordemservicoitem (
							IdOrdemServico, idProjeto, Projeto, idTag, Tag,
							ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal,
							Autor, Palavrachave, Notas, Espessura, AreaPintura,
							NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW, Altura,
							Largura, CodMatFabricante, DtCad, UsuarioCriacao,
							UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW,
							QtdeTotal, CriadoPor,
							DataCriacao, Estatus, Acabamento, D_E_L_E_T_E, fator, qtde,
							txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda,
							txtPintura, txtMontagem, tttxtCorte, tttxtDobra, tttxtSolda,
							tttxtPintura, tttxtMontagem, Comprimentocaixadelimitadora,
							Larguracaixadelimitadora, Espessuracaixadelimitadora,
							AreaPinturaUnitario, PesoUnitario, txtItemEstoque
						   ) VALUES (
							@IdOrdemServico, @idProjeto, @Projeto, @idTag, @Tag,
							@ESTATUS_OrdemServico, @IdMaterial, @DescResumo, @DescDetal,
							@Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura,
							@NumeroDobras, @Peso, @Unidade, @UnidadeSW, @ValorSW, @Altura,
							@Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao,
							@UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW,
							@QtdeTotal,@CriadoPor,
							@DataCriacao, @Estatus, @Acabamento, @D_E_L_E_T_E, @fator, @qtde,
							@txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda,
							@txtPintura, @txtMontagem, @tttxtCorte, @tttxtDobra, @tttxtSolda,
							@tttxtPintura, @tttxtMontagem, @Comprimentocaixadelimitadora,
							@Larguracaixadelimitadora, @Espessuracaixadelimitadora,
							@AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque
						   );"

                        Using command As New MySqlCommand(query, myconect)
                            ' Adicionando os parâmetros
                            command.Parameters.AddWithValue("@IdOrdemServico", OrdemServico.IdOrdemServico)
                            command.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
                            command.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                            command.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
                            command.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                            command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.Estatus)
                            command.Parameters.AddWithValue("@IdMaterial", OrdemServico.IdMaterial)
                            command.Parameters.AddWithValue("@DescResumo", OrdemServico.DescResumo)
                            command.Parameters.AddWithValue("@DescDetal", OrdemServico.DescDetal)
                            command.Parameters.AddWithValue("@Autor", OrdemServico.Autor)
                            command.Parameters.AddWithValue("@Palavrachave", OrdemServico.Palavrachave)
                            command.Parameters.AddWithValue("@Notas", OrdemServico.Notas)
                            command.Parameters.AddWithValue("@Espessura", OrdemServico.Espessura)
                            command.Parameters.AddWithValue("@AreaPintura", OrdemServico.AreaPintura)
                            command.Parameters.AddWithValue("@NumeroDobras", OrdemServico.NumeroDobras)
                            command.Parameters.AddWithValue("@Peso", Replace(OrdemServico.Peso, ",", "."))
                            command.Parameters.AddWithValue("@Unidade", OrdemServico.Unidade)
                            command.Parameters.AddWithValue("@UnidadeSW", OrdemServico.UnidadeSW)
                            command.Parameters.AddWithValue("@ValorSW", OrdemServico.ValorSW)
                            command.Parameters.AddWithValue("@Altura", OrdemServico.Altura)
                            command.Parameters.AddWithValue("@Largura", OrdemServico.Largura)
                            command.Parameters.AddWithValue("@CodMatFabricante", OrdemServico.CodMatFabricante)
                            command.Parameters.AddWithValue("@DtCad", "")
                            command.Parameters.AddWithValue("@UsuarioCriacao", "")
                            command.Parameters.AddWithValue("@UsuarioAlteracao", "")
                            command.Parameters.AddWithValue("@DtAlteracao", "")
                            command.Parameters.AddWithValue("@EnderecoArquivo", OrdemServico.EnderecoArquivo)
                            command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSW)
                            command.Parameters.AddWithValue("@QtdeTotal", Replace(OrdemServico.QtdeTotal, ",", "."))
                            'command.Parameters.AddWithValue("@QtdeProduzida", "")
                            'command.Parameters.AddWithValue("@QtdeFaltante", "")
                            command.Parameters.AddWithValue("@CriadoPor", Usuario.NomeCompleto.ToString)
                            command.Parameters.AddWithValue("@DataCriacao", Date.Now)
                            command.Parameters.AddWithValue("@Estatus", "A")
                            command.Parameters.AddWithValue("@Acabamento", OrdemServico.txtAcabamento)
                            command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                            command.Parameters.AddWithValue("@fator", 1)
                            command.Parameters.AddWithValue("@qtde", Replace(OrdemServico.qtde, ",", "."))
                            command.Parameters.AddWithValue("@txtSoldagem", OrdemServico.txtSoldagem)
                            command.Parameters.AddWithValue("@txtTipoDesenho", OrdemServico.txtTipoDesenho)
                            command.Parameters.AddWithValue("@txtCorte", OrdemServico.txtCorte)
                            command.Parameters.AddWithValue("@txtDobra", OrdemServico.txtDobra)
                            command.Parameters.AddWithValue("@txtSolda", OrdemServico.txtSolda)
                            command.Parameters.AddWithValue("@txtPintura", OrdemServico.txtPintura)
                            command.Parameters.AddWithValue("@txtMontagem", OrdemServico.txtMontagem)
                            command.Parameters.AddWithValue("@tttxtCorte", OrdemServico.tttxtCorte)
                            command.Parameters.AddWithValue("@tttxtDobra", OrdemServico.tttxtDobra)
                            command.Parameters.AddWithValue("@tttxtSolda", OrdemServico.tttxtSolda)
                            command.Parameters.AddWithValue("@tttxtPintura", OrdemServico.tttxtPintura)
                            command.Parameters.AddWithValue("@tttxtMontagem", OrdemServico.tttxtMontagem)
                            command.Parameters.AddWithValue("@Comprimentocaixadelimitadora", OrdemServico.Comprimentocaixadelimitadora)
                            command.Parameters.AddWithValue("@Larguracaixadelimitadora", OrdemServico.Larguracaixadelimitadora)
                            command.Parameters.AddWithValue("@Espessuracaixadelimitadora", OrdemServico.Espessuracaixadelimitadora)
                            command.Parameters.AddWithValue("@AreaPinturaUnitario", Replace(OrdemServico.AreaPinturaUnitario, ",", "."))
                            command.Parameters.AddWithValue("@PesoUnitario", Replace(OrdemServico.PesoUnitario, ",", "."))
                            command.Parameters.AddWithValue("@txtItemEstoque", OrdemServico.txtItemEstoque)

                            ' Abrir conexão e executar comando

                            'If cl_BancoDados.AbrirBanco = False Then
                            '    cl_BancoDados.AbrirBanco()

                            'End If
                            'myconect.Open()
                            command.ExecuteNonQuery()
                        End Using
                        TimerDGVListaMaterialSW.Enabled = True
                    End If

                End If

            End If

        End If

    End Sub

    Private Sub GerarPDFDasLinhasSelecionadasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GerarPDFDasLinhasSelecionadasToolStripMenuItem.Click

        If DGVListaMaterialSW.Rows.Count > 0 Then

            ' Exibe a caixa de mensagem com um aviso e opções Sim e Não
            Dim result As DialogResult = MessageBox.Show("Esta operação irá processar todos os itens do Grid, Convertento em PDF. Este procedimento pode levar algum tempo. Você deseja prosseguir?", "Atualizar Dados", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then

                '   IntanciaSolidWorks.ConectarSolidWorks()

                ProgressBarProcessoLiberacaoOrdemServico.Minimum = 0
                ProgressBarProcessoLiberacaoOrdemServico.Maximum = DGVListaMaterialSW.Rows.Count - 1

                '  dgvDataGridBOM.SuspendLayout()

                Dim novaqtde As String = 0

                For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

                    If DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = True Then

                        Try

                            DadosArquivoCorrente.EnderecoArquivo = DGVListaMaterialSW.Rows(i).Cells("EnderecoArquivo").Value.ToString
                            DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(DadosArquivoCorrente.EnderecoArquivo)
                            ' novaqtde = DGVListaMaterialSW.Rows(i).Cells("QtdeTotal").Value.ToString

                            OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)

                            ' Verifica se EnderecoArquivo não é nulo ou vazio antes de realizar a substituição
                            If Not String.IsNullOrEmpty(DadosArquivoCorrente.EnderecoArquivo) Then

                                Dim enderecoArquivoUpper As String = DadosArquivoCorrente.EnderecoArquivo.ToUpper()

                                ' Converte o caminho do arquivo para .SLDDRW se ele terminar com .SLDPRT ou .SLDASM

                                If enderecoArquivoUpper.EndsWith(".SLDPRT") OrElse enderecoArquivoUpper.EndsWith(".SLDASM") Then
                                    enderecoArquivoUpper = enderecoArquivoUpper.Replace(".SLDPRT", ".SLDDRW").Replace(".SLDASM", ".SLDDRW")
                                End If

                                If File.Exists(enderecoArquivoUpper) Then
                                    OpenDocumentAndWait(enderecoArquivoUpper, True, swModel)
                                    DadosArquivoCorrente.ExportToPDF(swModel, enderecoArquivoUpper, False)
                                    ' swapp.CloseDoc(arquivoSLDDRW)
                                Else

                                    DadosArquivoCorrente.ExportToPDF(swModel, enderecoArquivoUpper, False)

                                End If

                            End If

                            swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                            cl_BancoDados.FecharArquivoMemoria()
                            IntanciaSolidWorks.LiberarRecurso(swModel)
                            IntanciaSolidWorks.LiberarRecurso(swPart)

                            Dim enredecoPDF As String

                            ' Altera a extensão do arquivo para .PDF, independentemente de ser .SLDPRT, .sldprt, .SLDASM ou .sldasm
                            enredecoPDF = Path.ChangeExtension(DadosArquivoCorrente.EnderecoArquivo, ".PDF")

                            ' Verifica se o arquivo PDF existe
                            If File.Exists(enredecoPDF) Then
                                DGVListaMaterialSW.Rows(i).Cells("DGVPDF").Value = My.Resources.ficheiro_pdf
                            Else
                                DGVListaMaterialSW.Rows(i).Cells("DGVPDF").Value = My.Resources.Sem_Incone
                            End If

                            ProgressBarProcessoLiberacaoOrdemServico.Value = i
                        Catch ex As Exception
                            Continue For

                        End Try

                        DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False

                        '  ImportarPDFParaOSIndividual(DadosArquivoCorrente.EnderecoArquivo, novaqtde)

                    End If

                Next

                MsgBox("Processo de Atualização Finalizado com sucesso!", vbInformation, "Informação")

                ProgressBarProcessoLiberacaoOrdemServico.Value = 0

            End If

        End If

    End Sub

    Private Sub GerarDXFDasLinhasSelecionadasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GerarDXFDasLinhasSelecionadasToolStripMenuItem.Click

        If DGVListaMaterialSW.Rows.Count > 0 Then

            ' Exibe a caixa de mensagem com um aviso e opções Sim e Não
            Dim result As DialogResult = MessageBox.Show("Esta operação irá processar todos os itens do Grid, Convertento em DXF. Este procedimento pode levar algum tempo. Você deseja prosseguir?", "Atualizar Dados", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then

                IntanciaSolidWorks.ConectarSolidWorks()

                ProgressBarProcessoLiberacaoOrdemServico.Minimum = 0
                ProgressBarProcessoLiberacaoOrdemServico.Maximum = DGVListaMaterialSW.Rows.Count - 1

                '  dgvDataGridBOM.SuspendLayout()

                For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

                    '  Try

                    If Convert.ToBoolean(DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value) = True Then

                        DadosArquivoCorrente.EnderecoArquivo = DGVListaMaterialSW.Rows(i).Cells("EnderecoArquivo").Value.ToString

                        DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(DadosArquivoCorrente.EnderecoArquivo)

                        OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)

                        Dim enderecoArquivo As String = DadosArquivoCorrente.EnderecoArquivo

                        ' Verifica se EnderecoArquivo não é nulo e o arquivo realmente existe antes de tentar exportar
                        If Not String.IsNullOrEmpty(enderecoArquivo) AndAlso File.Exists(enderecoArquivo) Then
                            DadosArquivoCorrente.ExportDXF(swModel, False, True)
                        End If

                        swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                        cl_BancoDados.FecharArquivoMemoria()
                        IntanciaSolidWorks.LiberarRecurso(swModel)
                        IntanciaSolidWorks.LiberarRecurso(swPart)

                        Dim enredecoDxf As String

                        ' Altera a extensão do arquivo para .DXF, independentemente de ser .SLDPRT ou .sldprt
                        enredecoDxf = Path.ChangeExtension(DadosArquivoCorrente.EnderecoArquivo, ".DXF")

                        ' Verifica se o arquivo DXF existe
                        If File.Exists(enredecoDxf) Then
                            DGVListaMaterialSW.Rows(i).Cells("DGVDXF").Value = My.Resources.arquivo_dxf
                        Else
                            DGVListaMaterialSW.Rows(i).Cells("DGVDXF").Value = My.Resources.Sem_Incone
                        End If
                        ProgressBarProcessoLiberacaoOrdemServico.Value = i

                        DGVListaMaterialSW.Rows(i).Cells("dgvSelecao").Value = False

                    End If

                    'Catch ex As Exception
                    '    Continue For

                    'End Try

                Next
                MsgBox("Processo de Atualização Finalizado com sucesso!", vbInformation, "Informação")
                ProgressBarProcessoLiberacaoOrdemServico.Value = 0
            End If
        End If

    End Sub

    Private Sub CancelarLiberaçãoDaOSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelarLiberaçãoDaOSToolStripMenuItem.Click

        If OrdemServico.Liberado_Engenharia = "" Then
            MsgBox("A OS não está liberada!", vbCritical, "Atenção")
            Exit Sub
        End If

        If OrdemServico.IdOrdemServico.ToString = Nothing Or OrdemServico.IdOrdemServico.ToString = "" Or OrdemServico.IdOrdemServico.ToString <= 0 Then

            MsgBox("Não há OS Selecionada!", vbCritical, "Atenção")
            Exit Sub
        End If
        Dim result As DialogResult = MessageBox.Show("Deseja Realmente Cancelar a Liberação da Ordem de Serviço: " & OrdemServico.IdOrdemServico, "Cancelando Ordem de Serviço", MessageBoxButtons.YesNo)
        Dim totalExecutado As Integer
        Try

            cl_BancoDados.RetornaCampoDaPesquisa("SELECT  count(idplanodecorte) +
				   count(CorteTotalExecutado) + count(DobraTotalExecutado)+ count(SoldaTotalExecutado) +
				   count(PinturaTotalExecutado) +  count(MontagemTotalExecutado) as totalExecutado
				   FROM  " & ComplementoTipoBanco & "ordemservicoitem where IdOrdemServico  ='" & OrdemServico.IdOrdemServico & "'
				   and (idplanodecorte > 0) AND (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '');", "totalExecutado")
            totalExecutado = Convert.ToInt32(VCampo0)
        Catch ex As Exception

            totalExecutado = 0
        Finally

        End Try

        If result = DialogResult.Yes Then

            If totalExecutado > 0 Then

                Dim dtTabelaPlanoCorte As New System.Data.DataTable()

                dtTabelaPlanoCorte = cl_BancoDados.CarregarDados("SELECT  idplanodecorte, CodMatFabricante
					 FROM  " & ComplementoTipoBanco & "ordemservicoitem where IdOrdemServico  = '" & OrdemServico.IdOrdemServico & "' and (idplanodecorte > 0) AND (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '');")

                Dim MessagemItens As String

                For I As Integer = 0 To dtTabelaPlanoCorte.Rows.Count - 1

                    MessagemItens = MessagemItens & "PlanoCorte = " & dtTabelaPlanoCorte.Rows(I).Item("idplanodecorte").ToString &
                    " Numero Desenho: = " & dtTabelaPlanoCorte.Rows(I).Item("CodMatFabricante").ToString & vbCrLf

                Next

                MsgBox("A OS Numero: " & OrdemServico.IdOrdemServico & " contem processos em andamento, por este motivo não pode ser cancelada, ver plano de corte's: " & vbCrLf & MessagemItens, vbCritical, "Atenção!")

                Exit Sub

            End If

            OrdemServico.EnderecoOrdemServico = dgvos.CurrentRow.Cells("Endereco").Value.ToString

            '       cl_BancoDados.AlteracaoEspecifica("ordemservicoitem", "D_E_L_E_T_E", "*", "IdOrdemServico", OrdemServico.IdOrdemServico)

            Dim diretorio As String = OrdemServico.EnderecoOrdemServico

            LimparDiretorio(diretorio & "\PDF")
            LimparDiretorio(diretorio & "\DXF")
            LimparDiretorio(diretorio & "\DFT")
            LimparDiretorio(diretorio & "\LXDS")

            cl_BancoDados.Salvar("Update ordemservico set Liberado_Engenharia = '',
						Data_Liberacao_Engenharia = ''
						where IdOrdemServico = '" & OrdemServico.IdOrdemServico & "'")

            cl_BancoDados.Salvar("Update ordemservicoitem set Liberado_Engenharia = '',
						Data_Liberacao_Engenharia = ''
						where IdOrdemServico = '" & OrdemServico.IdOrdemServico & "'")

            'OrdemServico.SaldoTag
            'OrdemServico.QtdeLiberada
            'OrdemServico.Fator
            cl_BancoDados.RetornaCampoDaPesquisa("Select TipoLiberacaoOrdemServico from ordemservico where idordemservico = '" & OrdemServico.IdOrdemServico & "'", "TipoLiberacaoOrdemServico")

            Dim total As String = VCampo0

            If total.ToString = "Total" Then

                cl_BancoDados.Salvar("Update tags set QtdeLiberada = '" & (OrdemServico.QtdeLiberada - OrdemServico.Fator) & "',
                                                  SaldoTag = '" & (OrdemServico.SaldoTag + OrdemServico.Fator) & "'
												where IdTag = '" & OrdemServico.idTag & "'")

                cl_BancoDados.VerificaSaldoTag(OrdemServico.idTag)

                Me.lbltxtQtdeTag.Text = OrdemServico.QtdeTag
                Me.lbltxtQtdeLiberada.Text = OrdemServico.QtdeLiberada
                Me.lbltxtSaldoTag.Text = OrdemServico.SaldoTag

            End If

            ' Timerdgvos.Enabled = True

            dgvos.CurrentRow.Cells("Liberado_Engenharia").Value = ""
            dgvos.CurrentRow.Cells("Data_Liberacao_Engenharia").Value = ""
            dgvos.CurrentRow.Cells("dgvStatus").Value = My.Resources.atencao
            'dgvos.Refresh()

            If Usuario.EnviarEmailLiberacaoOS <> "" Then

                Dim resultado As MsgBoxResult = MessageBox.Show("Deseja enviar o e-mail para o PCP, de cancelamento da Ordem de Serviço: " & OrdemServico.IdOrdemServico, "Cancelamento", MessageBoxButtons.YesNo)

                If resultado = DialogResult.Yes Then

                    ClasseEmail.EmailCancelamentoOS()

                End If

            End If

            TimerFiltroPecaAtivaOS.Enabled = True
        Else

            MsgBox("Esta operação não é valida para OS: " & OrdemServico.IdOrdemServico & ", há processos executados!", vbCritical, "Atenção")

        End If

        cl_CalculoBancoDados.CalcularordemservicoitemFatorOS_TAG_PROJETO()

    End Sub

    Private Sub TrocarParaFormato4ADeitadoToolStripMenuItem_Click(sender As Object, e As EventArgs)

        If File.Exists(My.Settings.EnderecoNovoFormatoA3) = False Then

            MsgBox("O Arquivo padrão deve ser selecionado ante de executar a Operação!", vbCritical, "Atenção")
        Else

            Try

                '  Dim swModel As ModelDoc2
                Dim swModelDocExt As ModelDocExtension
                Dim swDrawing As DrawingDoc
                Dim fileName As String
                Dim status As Boolean
                ' Dim errors As Integer
                ' Dim warnings As Integer
                Dim sheetNameArray As Object
                Dim sheetNames(1) As String
                ' Dim options As Integer
                Dim fileerror As Integer

                Dim filewarning As Integer

                ' Dim lRetVal As Integer
                ' Dim ResolvedValOut As String
                ' Dim wasResolved As Boolean

                IntanciaSolidWorks.ConectarSolidWorks()

                swModel = swapp.ActiveDoc

                swModel.Visible = True

                swModelDocExt = swModel.Extension

                fileName = swModel.GetPathName.ToString
                'MsgBox(fileName.ToString)

                swModel = swapp.OpenDoc6(fileName.ToString, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_LoadModel, True, fileerror, filewarning)

                Dim currentSheetScale As Double
                Dim scaleStatus As Boolean

                ' Obtém a escala da folha atual
                scaleStatus = swDrawing.GetCurrentSheetScale(currentSheetScale)

                If scaleStatus Then
                    ' Configura a nova folha mantendo a escala atual
                    status = swDrawing.SetupSheet6(
                                       "Sheet3",
                                       swDwgPaperSizes_e.swDwgPaperA4size,
                                       swDwgTemplates_e.swDwgTemplateCustom,
                                       0.297, 0.21,
                                       True,
                                       My.Settings.EnderecoNovoFormatoA4.ToString,
                                       0.297, 0.21,
                                       "Default",
                                       True,
                                       currentSheetScale,
                                       currentSheetScale,
                                       0, 0, 0, 0)
                Else
                    MsgBox("Falha ao obter a escala atual da folha.")
                End If

                swModel.ForceRebuild3(True)
                swModel.ViewZoomtofit2()

                ' Atualiza a exibição gráfica antes de salvar para garantir a miniatura
                swModel.GraphicsRedraw2()

                ' Salva o arquivo com as opções de salvamento padrão e com a miniatura
                swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)

                'swApparq.CloseDoc(swModel.GetTitle)
            Catch ex As Exception
            Finally
            End Try

        End If

    End Sub

    Private Sub BiscarFormatoA4DeitadoToolStripMenuItem_Click(sender As Object, e As EventArgs)

        ' Configura o filtro para apenas arquivos com extensão .slddrt
        OpenFileDialog1.Filter = "SolidWorks Drawing Templates A4 (*.slddrt)|*.slddrt"

        ' Mostra o OpenFileDialog
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            My.Settings.EnderecoNovoFormatoA4Deitado = OpenFileDialog1.FileName

            ' Salva as configurações
            My.Settings.Save()
        End If

    End Sub

    Private Sub btnPendencias_Click(sender As Object, e As EventArgs) Handles btnPendencias.Click

        Try

                'Verifique se o modelo foi aberto com sucesso
                If Not swModel Is Nothing Then



                PendenciasRNC.ShowDialog()

                End If

            Catch ex As Exception
        Finally

        End Try

    End Sub

    Dim DescricaoFinalizacao As String

    Dim FormatadgvDataGridBOM As Boolean
    Private Sub dgvDataGridBOM_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvDataGridBOM.DataBindingComplete

        If FormatadgvDataGridBOM = False Then

            If dgvDataGridBOM IsNot Nothing AndAlso dgvDataGridBOM.Rows IsNot Nothing AndAlso dgvDataGridBOM.Rows.Count > 0 Then

                cl_BancoDados.FormatarDataGridView(dgvDataGridBOM, "SIM")

            End If

        End If
        FormatadgvDataGridBOM = True


    End Sub

    Private Sub AtualizarIcones()

        If DGVListaMaterialSW.Rows.Count > 0 Then

            For Each row As DataGridViewRow In DGVListaMaterialSW.Rows
                Dim valorEnderecoArquivo As String = If(row.Cells("EnderecoArquivo").Value, "").ToString()
                Dim valorProdutoPrincipal As String = If(row.Cells("ProdutoPrincipal").Value, "").ToString()

                ' Verifica se a string ".SLDASM" está contida na célula e se "ProdutoPrincipal" é "SIM" (ignora maiúsculas/minúsculas)
                If valorEnderecoArquivo.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 AndAlso
           valorProdutoPrincipal.IndexOf("SIM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                    row.Cells("dgvIconeItemOS").Value = My.Resources.IconeswPrincipal ' Substitua pelo seu ícone
                ElseIf valorEnderecoArquivo.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                    ' Define a imagem na coluna "dgvIconeItemOS" se for .SLDASM
                    row.Cells("dgvIconeItemOS").Value = My.Resources.IcopneMontagemSW ' Substitua pelo seu ícone
                ElseIf valorEnderecoArquivo.IndexOf(".SLDPRT", StringComparison.OrdinalIgnoreCase) >= 0 Then
                    ' Define outra imagem se for .SLDPRT
                    row.Cells("dgvIconeItemOS").Value = My.Resources.IcopneMontagemPRT
                Else
                    row.Cells("dgvIconeItemOS").Value = My.Resources.material_escolar_32
                End If

                ' Desconsidera a verificação de arquivo de mecanização
                If valorEnderecoArquivo.IndexOf(".pdf", StringComparison.OrdinalIgnoreCase) >= 0 Then

                    row.Cells("dgvIconePDF").Value = My.Resources.pdf ' Substitua pelo seu ícone
                Else
                    row.Cells("dgvIconePDF").Value = Nothing
                End If

                ' Desconsidera a verificação de arquivo de mecanização
                If valorEnderecoArquivo.IndexOf(".dxf", StringComparison.OrdinalIgnoreCase) >= 0 Then

                    row.Cells("DGVIconeDXF").Value = My.Resources.pdf ' Substitua pelo seu ícone
                Else
                    row.Cells("DGVIconeDXF").Value = Nothing
                End If

            Next

        End If

    End Sub

    Private Sub chkBoxTipoDesenho_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles chkBoxTipoDesenho.ItemCheck

        ' Obtem o CheckedListBox
        chkBoxTipoDesenho = CType(sender, CheckedListBox)

        ' Se o item está sendo marcado, desmarque os outros
        If e.NewValue = CheckState.Checked Then

            Try
                DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txttipodesenho", chkBoxTipoDesenho.Text, chkBoxTipoDesenho.Text)

                swModel.SaveSilent()
            Catch ex As Exception
                '   MsgBox(ex.Message)
            Finally

            End Try

            For i As Integer = 0 To chkBoxTipoDesenho.Items.Count - 1
                ' Apenas desmarque itens diferentes do que foi alterado
                If i <> e.Index Then
                    chkBoxTipoDesenho.SetItemChecked(i, False)
                End If
            Next
        End If

    End Sub

    Private Sub chkBoxAcabamento_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles chkBoxAcabamento.ItemCheck

        ' Obtem o CheckedListBox
        chkBoxAcabamento = CType(sender, CheckedListBox)

        ' Se o item está sendo marcado, desmarque os outros
        If e.NewValue = CheckState.Checked Then

            Try
                DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtacabamento",
                                                                chkBoxAcabamento.Text, chkBoxAcabamento.Text)

                swModel.SaveSilent()
            Catch ex As Exception
                '   MsgBox(ex.Message)
            Finally

            End Try

            For i As Integer = 0 To chkBoxAcabamento.Items.Count - 1
                ' Apenas desmarque itens diferentes do que foi alterado
                If i <> e.Index Then
                    chkBoxAcabamento.SetItemChecked(i, False)
                End If
            Next
        End If

    End Sub

    Public Sub DesvincularEAdicionarItem(combo As ComboBox, novoItem As String)
        ' Armazena os itens do ComboBox em uma lista temporária
        Dim listaItens As New List(Of String)

        ' Se o ComboBox estiver vinculado a uma fonte de dados, desvincula
        If combo.DataSource IsNot Nothing Then
            Dim dt As DataTable = TryCast(combo.DataSource, DataTable)
            If dt IsNot Nothing Then
                ' Copia os itens do DataTable antes de desvincular
                For Each row As DataRow In dt.Rows
                    listaItens.Add(row(combo.DisplayMember).ToString())
                Next
            Else
                ' Se for outro tipo de fonte de dados, apenas desvincula
                listaItens.AddRange(combo.Items.Cast(Of Object)().Select(Function(i) i.ToString()))
            End If
            combo.DataSource = Nothing
        Else
            ' Caso já não tenha um DataSource, apenas copia os itens atuais
            listaItens.AddRange(combo.Items.Cast(Of Object)().Select(Function(i) i.ToString()))
        End If

        ' Adiciona o novo item apenas se não existir (ignorando maiúsculas/minúsculas)
        If Not listaItens.Any(Function(i) i.Equals(novoItem, StringComparison.OrdinalIgnoreCase)) Then
            listaItens.Add(novoItem)
        End If

        ' Atualiza o ComboBox com os itens armazenados
        combo.Items.Clear()
        combo.Items.AddRange(listaItens.ToArray())
    End Sub

    Private Sub AtualizarDesenhoPeloDiretorioToolStripMenuItem1_Click(sender As Object, e As EventArgs)

        Dim result As DialogResult

        result = MessageBox.Show("Escolha uma opção:  SIM      1 - Atualiza a versão do SolidWork e Salva no banco de dados" & vbCrLf _
                                                      & "NÃO      2 - Somente Atualiza a versão do SolidWorks sem Salvar no Banco de dados" & vbCrLf _
                                                      & "Cancelar 3 -Finaliza a opração sem faze nenhuma alteralção", "Atualizar Versão",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1)

        If result = DialogResult.Yes Then

            ' Opção 1 - Atualizar versão do SW e Salvar no banco
            ' Processar todos os desenhos no diretório selecionado e suas subpastas
            ProcessAllFiles(True)

        ElseIf result = DialogResult.No Then
            ' Opção 2 - Somente atualizar versão do SW
            ' Processar todos os desenhos no diretório selecionado e suas subpastas
            ProcessAllFiles(False)

        ElseIf result = DialogResult.Cancel Then
            ' Opção 3 - Cancelar
            MessageBox.Show("A operação foi cancelada.", "Cancelar")

        End If
    End Sub

    Private Sub BuscarArquivosNosDiretorioToolStripMenuItem1_Click(sender As Object, e As EventArgs)

        Arquivos.Show()

    End Sub

    Private Sub UsarFormatoA3ToolStripMenuItem_Click(sender As Object, e As EventArgs)

        If File.Exists(My.Settings.EnderecoNovoFormatoA3) = False Then

            MsgBox("O Arquivo padrão deve ser selecionado ante de executar a Operação!", vbCritical, "Atenção")
        Else

            Try

                ' Dim swModel As ModelDoc2
                Dim swModelDocExt As ModelDocExtension
                Dim swDrawing As DrawingDoc
                Dim fileName As String
                Dim status As Boolean
                ' Dim errors As Integer
                '  Dim warnings As Integer
                Dim sheetNameArray As Object
                Dim sheetNames(1) As String
                ' Dim options As Integer
                Dim fileerror As Integer

                Dim filewarning As Integer

                '  Dim lRetVal As Integer
                ' Dim ResolvedValOut As String
                ' Dim wasResolved As Boolean

                IntanciaSolidWorks.ConectarSolidWorks()

                swModel = swapp.ActiveDoc

                swModel.Visible = True

                swModelDocExt = swModel.Extension

                fileName = swModel.GetPathName.ToString
                'MsgBox(fileName.ToString)

                swModel = swapp.OpenDoc6(fileName.ToString, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_LoadModel, True, fileerror, filewarning)

                ' swModel = swapp.OpenDoc6(fileName, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_Silent, "", errors, warnings)
                swModelDocExt = swModel.Extension
                swDrawing = swModel
                sheetNames(0) = "Sheet2"
                sheetNames(1) = "Sheet3"
                sheetNameArray = sheetNames
                swDrawing.SetSheetsSelected(sheetNameArray)
                status = swDrawing.SetupSheet6("Sheet3", swDwgPaperSizes_e.swDwgPapersUserDefined, swDwgTemplates_e.swDwgTemplateCustom, 0, 0, True, My.Settings.EnderecoNovoFormatoA3.ToString, 0.385, 0.277, "Default", True, 0, 0, 0, 0, 0, 0)

                swModel.ForceRebuild3(True)
                swModel.ViewZoomtofit2()

                ' Atualiza a exibição gráfica antes de salvar para garantir a miniatura
                swModel.GraphicsRedraw2()

                ' Salva o arquivo com as opções de salvamento padrão e com a miniatura
                swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)

                'swApparq.CloseDoc(swModel.GetTitle)
            Catch ex As Exception
            Finally
            End Try

        End If
    End Sub

    Private Sub UsarFormatoA4ToolStripMenuItem_Click(sender As Object, e As EventArgs)

        If File.Exists(My.Settings.EnderecoNovoFormatoA4) = False Then

            MsgBox("O Arquivo padrão deve ser selecionado ante de executar a Operação!", vbCritical, "Atenção")
        Else

            Try

                '  Dim swModel As ModelDoc2
                Dim swModelDocExt As ModelDocExtension
                Dim swDrawing As DrawingDoc
                Dim fileName As String
                Dim status As Boolean
                ' Dim errors As Integer
                ' Dim warnings As Integer
                Dim sheetNameArray As Object
                Dim sheetNames(1) As String
                ' Dim options As Integer
                Dim fileerror As Integer

                Dim filewarning As Integer

                ' Dim lRetVal As Integer
                ' Dim ResolvedValOut As String
                ' Dim wasResolved As Boolean

                IntanciaSolidWorks.ConectarSolidWorks()

                swModel = swapp.ActiveDoc

                swModel.Visible = True

                swModelDocExt = swModel.Extension

                fileName = swModel.GetPathName.ToString
                'MsgBox(fileName.ToString)

                swModel = swapp.OpenDoc6(fileName.ToString, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_LoadModel, True, fileerror, filewarning)

                ' swModel = swapp.OpenDoc6(fileName, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_Silent, "", errors, warnings)
                swModelDocExt = swModel.Extension
                swDrawing = swModel
                sheetNames(0) = "Sheet2"
                sheetNames(1) = "Sheet3"
                sheetNameArray = sheetNames
                swDrawing.SetSheetsSelected(sheetNameArray)
                status = swDrawing.SetupSheet6("Sheet3", swDwgPaperSizes_e.swDwgPaperA4size, swDwgTemplates_e.swDwgTemplateCustom, 0.21, 0.297, True, My.Settings.EnderecoNovoFormatoA4.ToString, 0.21, 0.297, "Default", True, 0, 0, 0, 0, 0, 0)

                swModel.ForceRebuild3(True)
                swModel.ViewZoomtofit2()

                ' Atualiza a exibição gráfica antes de salvar para garantir a miniatura
                swModel.GraphicsRedraw2()

                ' Salva o arquivo com as opções de salvamento padrão e com a miniatura
                swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)

                'swApparq.CloseDoc(swModel.GetTitle)
            Catch ex As Exception
            Finally

            End Try

        End If
    End Sub

    Private Sub UsarFornatoA4DToolStripMenuItem_Click(sender As Object, e As EventArgs)

        If File.Exists(My.Settings.EnderecoNovoFormatoA4Deitado) = False Then

            MsgBox("O Arquivo padrão deve ser selecionado ante de executar a Operação!", vbCritical, "Atenção")
        Else

            Try
                Dim swModelDocExt As ModelDocExtension
                Dim swDrawing As DrawingDoc
                Dim fileName As String
                Dim status As Boolean
                Dim sheetNameArray As Object
                Dim sheetNames(1) As String
                Dim fileerror As Integer

                Dim filewarning As Integer

                IntanciaSolidWorks.ConectarSolidWorks()

                swModel = swapp.ActiveDoc

                swModel.Visible = True

                swModelDocExt = swModel.Extension

                fileName = swModel.GetPathName.ToString
                'MsgBox(fileName.ToString)

                swModel = swapp.OpenDoc6(fileName.ToString, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_LoadModel, True, fileerror, filewarning)

                Dim currentSheetScale As Double
                Dim scaleStatus As Boolean

                ' Obtém a escala da folha atual
                scaleStatus = swDrawing.GetCurrentSheetScale(currentSheetScale)

                If scaleStatus Then
                    ' Configura a nova folha mantendo a escala atual
                    status = swDrawing.SetupSheet6(
                                       "Sheet3",
                                       swDwgPaperSizes_e.swDwgPaperA4size,
                                       swDwgTemplates_e.swDwgTemplateCustom,
                                       0.297, 0.21,
                                       True,
                                       My.Settings.EnderecoNovoFormatoA4.ToString,
                                       0.297, 0.21,
                                       "Default",
                                       True,
                                       currentSheetScale,
                                       currentSheetScale,
                                       0, 0, 0, 0)
                Else
                    MsgBox("Falha ao obter a escala atual da folha.")
                End If

                swModel.ForceRebuild3(True)
                swModel.ViewZoomtofit2()

                ' Atualiza a exibição gráfica antes de salvar para garantir a miniatura
                swModel.GraphicsRedraw2()

                ' Salva o arquivo com as opções de salvamento padrão e com a miniatura
                swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)

                'swApparq.CloseDoc(swModel.GetTitle)
            Catch ex As Exception
            Finally
            End Try

        End If
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click

        OrdemServico.IdOrdemServico = Nothing
        OrdemServico.Projeto = Nothing
        OrdemServico.Tag = Nothing
        OrdemServico.Descricao = Nothing
        OrdemServico.Estatus = Nothing
        OrdemServico.idTag = Nothing
        OrdemServico.idProjeto = Nothing
        OrdemServico.DescEmpresa = Nothing
        TSBSalvarOrdemServico.Enabled = True
        OrdemServico.DataPrevisao = Nothing
        OrdemServico.Liberado_Engenharia = Nothing

        Me.lblOrdemServicoAtiva.Text = ""

        Me.cboProjeto.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboTag.DropDownStyle = ComboBoxStyle.DropDownList

        Me.cboProjeto.Text = ""
        Me.cboTag.Text = ""
        Me.txtCliente.Clear()
        Me.txtDescricaoTag.Clear()
        Me.txtDescricao.Clear()
        Me.cboProjeto.Enabled = True
        Me.cboTag.Enabled = True

        Me.cboProjeto.Focus()

        TimerDGVListaMaterialSW.Enabled = True


    End Sub

    Private _salvandoOS As Boolean = False

    Private Sub TSBSalvarOrdemServico_Click(sender As Object, e As EventArgs) Handles TSBSalvarOrdemServico.Click

        If OrdemServico.Liberado_Engenharia <> "" Then

            MsgBox("Ordem de serviço já liberada, não e possivel fazer alterações!", vbCritical, "Atenção")

            Exit Sub

        End If



        If _salvandoOS Then Exit Sub

        ' 1) Validação de usuário logado
        If String.IsNullOrWhiteSpace(Usuario.NomeCompleto) Then
            MsgBox("Você não está logado com um usuário válido. Não é possível criar OS.", vbCritical, "Atenção")
            Exit Sub
        End If

        ' 2) Validação básica de campos obrigatórios
        If String.IsNullOrWhiteSpace(Me.txtDescricao.Text) Then
            MsgBox("Descrição é obrigatória.", vbExclamation, "Atenção")
            Exit Sub
        End If

        ' 3) Leitura segura dos IDs dos comboboxes
        Dim idTag As Integer
        Dim idProjeto As Integer

        If Not TryGetSelectedInt(Me.cboTag, idTag) OrElse idTag <= 0 Then
            MsgBox("Selecione uma Tag válida.", vbExclamation, "Atenção")
            Exit Sub
        End If

        If Not TryGetSelectedInt(Me.cboProjeto, idProjeto) OrElse idProjeto <= 0 Then
            MsgBox("Selecione um Projeto válido.", vbExclamation, "Atenção")
            Exit Sub
        End If

        ' (Opcional) se houver data de entrega em algum controle:
        'Dim prevDataEntrega As DateTime? = Nothing
        'If DateTime.TryParse(Me.dtpPrevEntrega.Text, Nothing) Then prevDataEntrega = Me.dtpPrevEntrega.Value

        _salvandoOS = True
        TSBSalvarOrdemServico.Enabled = False
        Cursor.Current = Cursors.WaitCursor

        Try
            ' 4) Atribuições no objeto de domínio
            OrdemServico.Descricao = Me.txtDescricao.Text.Trim()
            OrdemServico.idTag = idTag
            OrdemServico.idProjeto = idProjeto
            'OrdemServico.PrevDataEntrega = prevDataEntrega

            ' 5) Pausa timers que podem competir com o salvamento (evita reentrância)
            Dim t1State = Timerdgvos.Enabled
            Dim t2State = TimerDGVListaMaterialSW.Enabled
            Timerdgvos.Enabled = False
            TimerDGVListaMaterialSW.Enabled = False

            Try
                ' 6) Chamada da rotina existente (mantida)
                OrdemServico.CriarOsCompleta(dgvos, Timerdgvos, TimerDGVListaMaterialSW)

                ' (Opcional) feedback de sucesso
                'MsgBox("Ordem de Serviço criada com sucesso.", vbInformation, "OK")
            Finally
                ' 7) Restaura timers ao estado anterior
                Timerdgvos.Enabled = t1State
                TimerDGVListaMaterialSW.Enabled = t2State
            End Try
        Catch ex As Exception
            ' Mostra a mensagem real para facilitar suporte
            MsgBox("Erro ao criar OS: " & ex.Message, vbCritical, "Erro")
        Finally
            Cursor.Current = Cursors.Default
            TSBSalvarOrdemServico.Enabled = True
            _salvandoOS = False
        End Try
    End Sub

    ' =============================
    ' Helper para obter SelectedValue como Integer com segurança
    ' =============================
    Private Function TryGetSelectedInt(cbo As ComboBox, ByRef valueOut As Integer) As Boolean
        valueOut = 0
        If cbo Is Nothing Then Return False

        Dim v As Object = Nothing
        Try
            v = cbo.SelectedValue
        Catch
            ' Se o DataSource mudou durante o acesso
            Return False
        End Try

        If v Is Nothing OrElse Convert.IsDBNull(v) Then Return False

        ' Quando o DataSource está em edição, às vezes vem DataRowView
        If TypeOf v Is DataRowView Then
            Dim drv = DirectCast(v, DataRowView)
            ' Tenta pela DisplayMember/ValueMember
            Dim colName = If(String.IsNullOrWhiteSpace(cbo.ValueMember), cbo.DisplayMember, cbo.ValueMember)
            If Not String.IsNullOrWhiteSpace(colName) AndAlso drv.Row.Table.Columns.Contains(colName) Then
                Dim raw = drv(colName)
                If raw IsNot Nothing AndAlso Not Convert.IsDBNull(raw) Then
                    Return Integer.TryParse(Convert.ToString(raw), valueOut)
                End If
            End If
            Return False
        End If

        ' Normal: SelectedValue é escalar
        Return Integer.TryParse(Convert.ToString(v), valueOut)
    End Function

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click

        AtualziarDadosSinco()

    End Sub

    Private Sub AtualziarDadosSinco()

        ' --- medição opcional de desempenho ---
        Dim sw As Stopwatch = Stopwatch.StartNew()

        ' Guarda UI
        Cursor.Current = Cursors.WaitCursor
        Me.Enabled = False

        Try
            ' 1) Normaliza TipoConexao
            Dim tipo As String = If(My.Settings.TipoConexao, "").Trim().ToUpperInvariant()
            If tipo <> "SQL" AndAlso tipo <> "MYSQL" Then
                Throw New ApplicationException("My.Settings.TipoConexao inválido. Use 'SQL' ou 'MYSQL'.")
            End If

            ' 2) Prefixo do banco (ComplementoTipoBanco) pode vir vazio para SQL Server.
            '    Mantém sua lógica: só usa quando você já passa (ex.: [BDENG].[dbo].).
            Dim prefixo As String = If(ComplementoTipoBanco, String.Empty)

            ' 3) Carregar combos conforme tipo de conexão
            If tipo = "SQL" Then
                ' Projeto (usa View SZ1010)
                SafeComboBoxFill(
                Sub()
                    cl_BancoDados.ComboBoxDataSet("[View_SZ1010_GESTAO]", "Z1_NUM", "Z1_NUM", cboProjeto, "", "")
                End Sub, "cboProjeto (SQL)")

                ' Acabamento
                SafeComboBoxFill(
                Sub()
                    cl_BancoDados.ComboBoxDataSet("[Tratamento]", "Id_Tratamento", "tratamento", cboOpcoesAcabamento, "", "[BDENG].[dbo].")
                End Sub, "cboOpcoesAcabamento (SQL)")

                ' CheckedListBox Família / Acabamento (com prefixo definido para SQL)
                SafeCheckedListFill($"SELECT DescFamilia FROM {prefixo}familia WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') ORDER BY DescFamilia", chkBoxTipoDesenho, "chkBoxTipoDesenho (SQL)")
                SafeCheckedListFill($"SELECT DescAcabamento FROM {prefixo}acabamento WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') ORDER BY DescAcabamento", chkBoxAcabamento, "chkBoxAcabamento (SQL)")

            ElseIf tipo = "MYSQL" Then
                ' Projeto
                SafeComboBoxFill(
                Sub()
                    cl_BancoDados.ComboBoxDataSet("projetos", "idProjeto", "Projeto", cboProjeto, " WHERE (D_E_L_E_T_E Is NULL Or D_E_L_E_T_E = '') AND (Finalizado = '' OR Finalizado Is NULL) AND (Liberado = 'S')")
                End Sub, "cboProjeto (MySQL)")

                ' Acabamento
                SafeComboBoxFill(
                Sub()
                    cl_BancoDados.ComboBoxDataSet("acabamento", "IdAcabamento", "DescAcabamento", cboOpcoesAcabamento, "WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '')")
                End Sub, "cboOpcoesAcabamento (MySQL)")

                ' CheckedListBox Família / Acabamento (prefixo para MySQL se você usa)
                SafeCheckedListFill($"SELECT DescFamilia FROM {prefixo}familia WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') ORDER BY DescFamilia", chkBoxTipoDesenho, "chkBoxTipoDesenho (MySQL)")
                SafeCheckedListFill($"SELECT DescAcabamento FROM {prefixo}acabamento WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') ORDER BY DescAcabamento", chkBoxAcabamento, "chkBoxAcabamento (MySQL)")
            End If

            ' 4) Processos de fabricação (comum aos dois)
            SafeCheckedListFill($"SELECT ProcessoFabricacao FROM {prefixo}processofabricacao WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') ORDER BY ProcessoFabricacao", chkBoxProcessos, "chkBoxProcessos")

            ' 5) Versão na StatusStrip (sem estourar em ambientes sem AssemblyInfo)
            Dim version As Version = Nothing
            Try
                version = Reflection.Assembly.GetExecutingAssembly().GetName().Version
            Catch
                ' ignora
            End Try
            Dim versaoTxt As String = If(version IsNot Nothing, version.ToString(), "v?")
            If tslVersaoSistema IsNot Nothing Then
                tslVersaoSistema.Text = $"{My.Settings.BancoDadosAtivo}: {versaoTxt}"
            End If
        Catch ex As Exception
            ' Log detalhado + mensagem amigável
            Debug.WriteLine($"AtualziarDadosSinco: {ex}")
            MessageBox.Show("Falha ao atualizar dados do SINCO: " & ex.Message, "Atualização de dados", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Enabled = True
            Cursor.Current = Cursors.Default
            sw.Stop()
            Debug.WriteLine($"AtualziarDadosSinco concluído em {sw.ElapsedMilliseconds} ms")
        End Try
    End Sub

    ' Executa com proteção contra exceção e nulos
    Private Sub SafeComboBoxFill(action As System.Action, alvo As String)
        If action Is Nothing Then Exit Sub
        Try
            action.Invoke()
            ' Garante SelectedIndex válido se houver items
            Dim cbo = TryCast(GetAlvoControl(alvo), ComboBox)
            If cbo IsNot Nothing AndAlso cbo.Items IsNot Nothing AndAlso cbo.Items.Count > 0 AndAlso cbo.SelectedIndex < 0 Then
                cbo.SelectedIndex = 0
            End If
        Catch ex As Exception
            Debug.WriteLine($"SafeComboBoxFill[{alvo}]: {ex.Message}")
        End Try
    End Sub

    Private Sub SafeCheckedListFill(sql As String, chk As CheckedListBox, alvo As String)
        If chk Is Nothing OrElse String.IsNullOrWhiteSpace(sql) Then Exit Sub
        Try
            ' sua função já preenche a lista; apenas limpa antes por segurança
            chk.Items.Clear()
            PreencherCheckedListBox(sql, chk)
        Catch ex As Exception
            Debug.WriteLine($"SafeCheckedListFill[{alvo}]: {ex.Message}")
        End Try
    End Sub

    ' Utilitário opcional caso queira debugar um alvo específico por nome
    Private Function GetAlvoControl(alvo As String) As Control
        ' Ex: "cboProjeto (SQL)" → tenta achar "cboProjeto"
        If String.IsNullOrWhiteSpace(alvo) Then Return Nothing
        Dim nome = alvo.Split(" "c)(0).Trim()
        Return Me.Controls.Find(nome, True).FirstOrDefault()
    End Function

    'Conforme novos processos de fabricação são inseridos os dados na tabela ordemservicoitem são criado e marcado conforme o cadastro de material
    Public Function SalvarProcessoOrdemServicoItem(CodMatFabricante As String)

        ' 1) Carrega a lista de processos válidos (não deletados)
        Dim sqlProc As String =
        "SELECT ProcessoFabricacao " &
        "FROM " & ComplementoTipoBanco & "processofabricacao " &
        "WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') " &
        "ORDER BY ProcessoFabricacao;"

        Dim tabelaProcesso As DataTable = cl_BancoDados.CarregarDados(sqlProc)
        If tabelaProcesso Is Nothing OrElse tabelaProcesso.Rows.Count = 0 Then
            Return Nothing ' nada a fazer
        End If

        ' 2) Recupera o último IdOrdemServicoItem (mesma lógica que você usava)
        cl_BancoDados.RetornaCampoDaPesquisa(
        "SELECT MAX(IdOrdemServicoItem) AS IdOrdemServicoItem FROM " & ComplementoTipoBanco & "ordemservicoitem",
        "IdOrdemServicoItem")
        Dim IdOrdItem As String = VCampo0
        If String.IsNullOrWhiteSpace(IdOrdItem) Then
            Return Nothing ' sem item para atualizar
        End If

        ' 3) Monta a lista de nomes de colunas do material (txt<NOMESEMESPACO>)
        Dim colunasMaterial As New List(Of String)
        For Each r As DataRow In tabelaProcesso.Rows
            Dim proc As String = Convert.ToString(r("ProcessoFabricacao"))
            If Not String.IsNullOrWhiteSpace(proc) Then
                Dim nomeCol As String = "txt" & proc.Replace(" ", "")
                colunasMaterial.Add(nomeCol)
            End If
        Next
        If colunasMaterial.Count = 0 Then Return Nothing

        ' 4) Decide o delimitador de coluna conforme o tipo de banco
        Dim tipo As String = If(My.Settings.TipoConexao, "").Trim().ToUpperInvariant()
        Dim QL As Func(Of String, String) ' quote-left/right para nome de coluna
        If tipo = "MYSQL" Then
            QL = Function(c) "`" & c & "`"
        Else
            QL = Function(c) "[" & c & "]"
        End If

        ' 5) Monta SELECT único com todas as colunas de interesse
        Dim colsSelect As String = String.Join(",", colunasMaterial.Select(Function(c) QL(c)))
        ' Sanitiza valor (se não puder usar parâmetro)
        Dim cod As String = UCase(If(CodMatFabricante, "").Trim()).Replace("'", "''")

        Dim sqlMat As String =
        "SELECT " & colsSelect & " " &
        "FROM " & ComplementoTipoBanco & "material " &
        "WHERE CodMatFabricante = '" & cod & "';"

        Dim tabelaMaterial As DataTable = cl_BancoDados.CarregarDados(sqlMat)
        If tabelaMaterial Is Nothing OrElse tabelaMaterial.Rows.Count = 0 Then
            ' sem material correspondente → nada a marcar
            Return Nothing
        End If

        Dim linhaMat As DataRow = tabelaMaterial.Rows(0)

        ' 6) Atualiza cada processo no ordemservicoitem conforme o cadastro do material (valor "1" ativa; senão limpa)
        For Each nomeCol In colunasMaterial
            ' Se por algum motivo a coluna não veio no SELECT (ex.: coluna faltante), ignora com segurança
            If Not tabelaMaterial.Columns.Contains(nomeCol) Then Continue For

            Dim valor As String = ""
            Dim v As Object = linhaMat(nomeCol)
            If v IsNot Nothing AndAlso Not Convert.IsDBNull(v) Then
                Dim s As String = Convert.ToString(v).Trim()
                If s = "1" Then valor = "1" ' só "1" marca; qualquer outro valor limpa
            End If

            ' Atualiza coluna dinâmica no item
            ' Mantive sua API AlteracaoEspecifica(tabela, coluna, valor, chave, id)
            cl_BancoDados.AlteracaoEspecifica(ComplementoTipoBanco & "ordemservicoitem", nomeCol, valor, "IdOrdemServicoItem", IdOrdItem)
        Next

        Return Nothing
    End Function

    ' Flag para evitar reentrância
    Private _atualizandoVersao As Boolean = False

    Private Async Sub AtualizarDesenhoPeloDiretorioToolStripMenuItem1_Click_1(sender As Object, e As EventArgs)

        If _atualizandoVersao Then Exit Sub
        _atualizandoVersao = True

        ' Se o menu veio habilitado, desabilita temporariamente
        Dim itemMenu As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
        If itemMenu IsNot Nothing Then itemMenu.Enabled = False

        ' (Opcional) pausar timers que possam interferir durante o processamento
        Dim t1State As Boolean = False, t2State As Boolean = False
        Try
            If Me.components IsNot Nothing Then
                ' ajuste caso use timers com estes nomes
                If Me.GetType().GetField("Timerdgvos", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic) IsNot Nothing Then
                    Dim t = TryCast(Me.GetType().GetField("Timerdgvos", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(Me), System.Windows.Forms.Timer)
                    If t IsNot Nothing Then t1State = t.Enabled : t.Enabled = False
                End If
                If Me.GetType().GetField("TimerDGVListaMaterialSW", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic) IsNot Nothing Then
                    Dim t = TryCast(Me.GetType().GetField("TimerDGVListaMaterialSW", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(Me), System.Windows.Forms.Timer)
                    If t IsNot Nothing Then t2State = t.Enabled : t.Enabled = False
                End If
            End If
        Catch
            ' silencioso — timers são opcionais
        End Try

        Cursor.Current = Cursors.WaitCursor

        Try
            Dim result As DialogResult = MessageBox.Show(
            "Escolha uma opção:" & vbCrLf &
            "SIM      1 - Atualiza a versão do SolidWorks e salva no banco de dados" & vbCrLf &
            "NÃO      2 - Somente atualiza a versão do SolidWorks (não salva no banco)" & vbCrLf &
            "CANCELAR 3 - Finaliza a operação sem fazer alterações",
            "Atualizar Versão", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            Select Case result
                Case DialogResult.Yes
                    ' Opção 1 – Atualiza e salva
                    Await Task.Run(Sub() ProcessAllFiles(True))

                    MessageBox.Show("Atualização concluída e salva no banco.", "Concluído",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)

                Case DialogResult.No
                    ' Opção 2 – Só atualiza
                    Await Task.Run(Sub() ProcessAllFiles(False))

                    MessageBox.Show("Atualização concluída (sem salvar no banco).", "Concluído",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)

                Case DialogResult.Cancel
                    ' Opção 3 – Cancelar
                    MessageBox.Show("A operação foi cancelada.", "Cancelar",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Select
        Catch ex As Exception
            ' Mostra detalhe útil para suporte
            MessageBox.Show("Falha ao atualizar versão dos desenhos: " & ex.Message,
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor.Current = Cursors.Default
            ' Restaura timers, se existirem
            Try
                If Me.GetType().GetField("Timerdgvos", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic) IsNot Nothing Then
                    Dim t = TryCast(Me.GetType().GetField("Timerdgvos", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(Me), System.Windows.Forms.Timer)
                    If t IsNot Nothing Then t.Enabled = t1State
                End If
                If Me.GetType().GetField("TimerDGVListaMaterialSW", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic) IsNot Nothing Then
                    Dim t = TryCast(Me.GetType().GetField("TimerDGVListaMaterialSW", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(Me), System.Windows.Forms.Timer)
                    If t IsNot Nothing Then t.Enabled = t2State
                End If
            Catch
            End Try

            If itemMenu IsNot Nothing Then itemMenu.Enabled = True
            _atualizandoVersao = False
        End Try
    End Sub

    Private Sub BuscarArquivosNosDiretorioToolStripMenuItem1_Click_1(sender As Object, e As EventArgs)

        Arquivos.Show()

    End Sub

    Private Function SafeConvertToDouble(value As String) As Double
        Dim result As Double = -1 ' Valor padrão em caso de erro
        If Not String.IsNullOrEmpty(value) Then
            Try
                value = value.Replace("%", "") ' Remove o % se existir
                result = Convert.ToDouble(value)
            Catch ex As Exception
                ' Loga erro de conversão
                Console.WriteLine("Erro de conversão: " & ex.Message)
                result = -1 ' Valor padrão em caso de erro
            End Try
        End If
        Return result
    End Function

    Private Sub TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem.Click


        Try
            ' 1) Linha corrente válida?
            If dgvos.CurrentRow Is Nothing Then
                MsgBox("Selecione uma Ordem de Serviço na lista.", vbExclamation, "Atenção")
                Exit Sub
            End If

            ' 2) IdOrdemServico (Object -> Integer)
            Dim vId As Object = dgvos.CurrentRow.Cells("IdOrdemServico").Value
            Dim idOs As Integer
            If vId Is Nothing OrElse Convert.IsDBNull(vId) OrElse Not Integer.TryParse(Convert.ToString(vId), idOs) OrElse idOs <= 0 Then
                MsgBox("OS inválida. Selecione um registro válido.", vbExclamation, "Atenção")
                Exit Sub
            End If

            ' 3) Status de liberação da engenharia
            Dim vLib As Object = Nothing
            If dgvos.Columns.Contains("Liberado_Engenharia") Then
                vLib = dgvos.CurrentRow.Cells("Liberado_Engenharia").Value
            Else
                ' fallback: usa o que estiver no objeto OrdemServico (se já populado)
                vLib = OrdemServico.Liberado_Engenharia
            End If
            Dim liberadoEng As String = If(Convert.IsDBNull(vLib) OrElse vLib Is Nothing, "", Convert.ToString(vLib)).Trim().ToUpperInvariant()

            ' 4) Confirmação
            Dim pergunta As String =
            $"Deseja realmente transformar a OS {idOs} em Produto Padrão?" & vbCrLf &
            "O PCP poderá liberar para produção sem consultar a engenharia."
            Dim result As DialogResult = MessageBox.Show(pergunta, "Conversão de OS em Produto Padrão", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

            If result <> DialogResult.Yes Then Exit Sub

            ' 5) Regras de negócio: só permite se Engenharia liberou
            If liberadoEng <> "S" Then
                MsgBox("Para converter a OS em Produto Padrão, ela deve estar LIBERADA pela Engenharia.", vbInformation, "Atenção")
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor

            ' 6) Persistência (mantendo sua API)
            '    Tenta usar o id lido (idOs) para evitar inconsistência visual no grid.
            cl_BancoDados.AlteracaoEspecifica("ordemservico", "ProdutoPadrao", "SIM", "IdOrdemServico", OrdemServico.IdOrdemServico)


            For i As Integer = 0 To DGVListaMaterialSW.Rows.Count - 1

                OrdemServico.ProdutoPrincipal = DGVListaMaterialSW.Rows(i).Cells("ProdutoPrincipal").Value.ToString

                If OrdemServico.ProdutoPrincipal <> "" Then

                    OrdemServico.CodMatFabricante = DGVListaMaterialSW.Rows(i).Cells("CodMatFabricante").Value.ToString

                    OrdemServico.DescResumo = DGVListaMaterialSW.Rows(i).Cells("DescResumo").Value.ToString

                    OrdemServico.DescDetal = DGVListaMaterialSW.Rows(i).Cells("DescDetal").Value.ToString

                    cl_BancoDados.RetornaCampoDaPesquisa("Select EnderecoImagem from material where CodMatFabricante = '" & OrdemServico.CodMatFabricante & "'", "EnderecoImagem")

                    cl_BancoDados.AlteracaoEspecifica("ordemservico", "EnderecoImagem", VCampo0, "idordemservico", OrdemServico.IdOrdemServico)

                    cl_BancoDados.AlteracaoEspecifica("material", "ProdutoPrincipal", "SIM", "CodMatFabricante", OrdemServico.CodMatFabricante)

                    cl_BancoDados.AlteracaoEspecifica("ordemservico", "CodDesenhoProduto", OrdemServico.CodMatFabricante, "idordemservico", OrdemServico.IdOrdemServico)

                    cl_BancoDados.AlteracaoEspecifica("ordemservico", "DescricaoProduto", OrdemServico.DescResumo & "-" & OrdemServico.DescDetal, "idordemservico", OrdemServico.IdOrdemServico)

                    Exit Sub

                End If

            Next

            ' 7) Feedback
            '  MsgBox("OK! Vá ao SINCO para completar o cadastro e a configuração do produto.", vbInformation, "Produto Padrão")

            ' 8) (Opcional) Reflete no grid atual se houver coluna "ProdutoPadrao"

            If dgvos.Columns.Contains("ProdutoPadrao") Then
                dgvos.CurrentRow.Cells("ProdutoPadrao").Value = "SIM"
                ' dgvos.CurrentRow.DefaultCellStyle.BackColor = Color.LightGreen
            End If


        Catch ex As Exception
            MsgBox("Falha ao converter OS em Produto Padrão: " & ex.Message, vbCritical, "Erro")
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub txPesqCodDesenhoProduto1_TextChanged(sender As Object, e As EventArgs)
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txPesqCodDesenhoProduto2_TextChanged(sender As Object, e As EventArgs)
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txPesqCodDesenhoProduto3_TextChanged(sender As Object, e As EventArgs)
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txPesqCodDesenhoProduto4_TextChanged(sender As Object, e As EventArgs)
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txtPesqCodOmie1_TextChanged(sender As Object, e As EventArgs)
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txtPesqCodOmie2_TextChanged(sender As Object, e As EventArgs)
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txtPesqCodOmie3_TextChanged(sender As Object, e As EventArgs)
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txtPesqCodOmie4_TextChanged(sender As Object, e As EventArgs)
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txtPesqDescricaoProduto1_TextChanged(sender As Object, e As EventArgs)
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txtPesqDescricaoProduto2_TextChanged(sender As Object, e As EventArgs)
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txtPesqDescricaoProduto3_TextChanged(sender As Object, e As EventArgs)
        TimerProdutos.Enabled = True
    End Sub

    Private Sub txtPesqDescricaoProduto4_TextChanged(sender As Object, e As EventArgs)
        TimerProdutos.Enabled = True
    End Sub

    Dim idOrdemServidoProduto As Integer

    Private Sub chkBoxAcabamento_Click(sender As Object, e As EventArgs) Handles chkBoxAcabamento.Click

        Try

            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then

                DadosArquivoCorrente.Acabamento = Me.chkBoxAcabamento.Text
                DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtacabamento", DadosArquivoCorrente.Acabamento, DadosArquivoCorrente.Acabamento)

                cl_BancoDados.AlteracaoEspecifica("material", "txtacabamento", DadosArquivoCorrente.Acabamento, "CodMatFabricante", DadosArquivoCorrente.NomeArquivoSemExtensao)

                swModel.SaveSilent()

            End If
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub chkBoxTipoDesenho_Click(sender As Object, e As EventArgs) Handles chkBoxTipoDesenho.Click

        Try

            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then

                DadosArquivoCorrente.TipoDesenho = chkBoxTipoDesenho.Text
                DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txttipodesenho", DadosArquivoCorrente.TipoDesenho, DadosArquivoCorrente.TipoDesenho)

                cl_BancoDados.AlteracaoEspecifica("material", "txttipodesenho", DadosArquivoCorrente.TipoDesenho, "CodMatFabricante", DadosArquivoCorrente.NomeArquivoSemExtensao)

                swModel.SaveSilent()

            End If
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click

        Try

            Dim ArquivoPdf As String = dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoPdf = Path.ChangeExtension(ArquivoPdf, ".PDF")

            ' Obtém o caminho completo
            ArquivoPdf = Path.GetFullPath(ArquivoPdf)

            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoPdf) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoPdf)

                    p.Start()
                    p.WaitForExit()

                    dgvDataGridBOM.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click

        Try

            Dim ArquivoDXF As String = dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoDXF = Path.ChangeExtension(ArquivoDXF, ".DXF")

            ' Obtém o caminho completo
            ArquivoDXF = Path.GetFullPath(ArquivoDXF)
            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoDXF) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoDXF)

                    p.Start()
                    p.WaitForExit()

                    dgvDataGridBOM.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub AbrirLXDSDaLinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirLXDSDaLinhaSelecionadaToolStripMenuItem.Click

        Try

            Dim ArquivoLXDS As String = dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoLXDS = Path.ChangeExtension(ArquivoLXDS, ".LXDS")

            ' Obtém o caminho completo
            ArquivoLXDS = Path.GetFullPath(ArquivoLXDS)
            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoLXDS) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoLXDS)

                    p.Start()
                    p.WaitForExit()

                    dgvDataGridBOM.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub chkVerificarPDF_DoubleClick(sender As Object, e As EventArgs) Handles chkVerificarPDF.DoubleClick

        Try

            Dim ArquivoPdf As String = DadosArquivoCorrente.EnderecoArquivo ' dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoPdf = Path.ChangeExtension(ArquivoPdf, ".PDF")

            ' Obtém o caminho completo
            ArquivoPdf = Path.GetFullPath(ArquivoPdf)

            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoPdf) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoPdf)

                    p.Start()
                    p.WaitForExit()

                    dgvDataGridBOM.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub chkVerificarDXF_DoubleClick(sender As Object, e As EventArgs) Handles chkVerificarDXF.DoubleClick

        Try

            Dim ArquivoDXF As String = DadosArquivoCorrente.EnderecoArquivo  ' dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoDXF = Path.ChangeExtension(ArquivoDXF, ".DXF")

            ' Obtém o caminho completo
            ArquivoDXF = Path.GetFullPath(ArquivoDXF)
            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoDXF) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoDXF)

                    p.Start()
                    p.WaitForExit()

                    dgvDataGridBOM.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub chkVerificarLXDS_DoubleClick(sender As Object, e As EventArgs) Handles chkVerificarLXDS.DoubleClick

        Try

            Dim ArquivoLXDS As String = DadosArquivoCorrente.EnderecoArquivo  'dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoLXDS = Path.ChangeExtension(ArquivoLXDS, ".LXDS")

            ' Obtém o caminho completo
            ArquivoLXDS = Path.GetFullPath(ArquivoLXDS)
            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoLXDS) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoLXDS)

                    p.Start()
                    p.WaitForExit()

                    dgvDataGridBOM.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs)

        Dim TipoDesenhoMarcado, AcabamentoMarcado As String
        TipoDesenhoMarcado = chkBoxTipoDesenho.Text
        AcabamentoMarcado = chkBoxAcabamento.Text

        AtualziarDadosSinco()

        If swModel Is Nothing Then

            Exit Sub
        Else

            For i As Integer = 0 To chkBoxAcabamento.Items.Count - 1
                Try
                    If chkBoxAcabamento.Items(i).ToString() = AcabamentoMarcado.ToString() Then
                        chkBoxAcabamento.SetItemChecked(i, True)
                        Exit For
                    End If
                Catch ex As Exception
                    Continue For
                End Try
            Next

            For i As Integer = 0 To chkBoxTipoDesenho.Items.Count - 1
                Try

                    If chkBoxTipoDesenho.Items(i).ToString() = TipoDesenhoMarcado.ToString() Then
                        chkBoxTipoDesenho.SetItemChecked(i, True)
                        Exit For
                    End If
                Catch ex As Exception
                    Continue For
                End Try
            Next

            Try

                'edson 04/03/2025
                AtualizaTela(swModel, chkBoxProcessos)
            Catch ex As Exception
            Finally
            End Try

        End If

    End Sub

    Dim swAssembly As AssemblyDoc
    Dim componentCounts As New Dictionary(Of String, Integer) ' Dicionário para contar quantidades
    Dim componentList As New List(Of String) ' Lista para armazenar os componentes e quantidades

    Private Sub dgvDataGridBOM_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvDataGridBOM.DataError
        Try
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub ToolStripButton10_Click(sender As Object, e As EventArgs)

        Dim Pasta As New FolderBrowserDialog With {
        .Description = "Selecione uma pasta",
        .ShowNewFolderButton = True
    }

        ' Verifica se o usuário selecionou uma pasta válida
        If Pasta.ShowDialog() <> DialogResult.OK OrElse String.IsNullOrEmpty(Pasta.SelectedPath) Then
            MessageBox.Show("Nenhuma pasta selecionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim directoryPath As String = Pasta.SelectedPath
        Dim files As String() = Directory.GetFiles(directoryPath, "*.sldprt")

        ' Verifica se há arquivos na pasta
        If files.Length = 0 Then
            MessageBox.Show("Nenhum arquivo .sldprt encontrado na pasta selecionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim swApp As SolidWorks.Interop.sldworks.SldWorks = Nothing
        Dim swModel As ModelDoc2 = Nothing

        Try
            ' Inicializa o SolidWorks
            swApp = CType(CreateObject("SldWorks.Application"), SolidWorks.Interop.sldworks.SldWorks)
            swApp.Visible = True

            For Each filePath In files
                swModel = swApp.OpenDoc6(filePath, swDocumentTypes_e.swDocPART, swOpenDocOptions_e.swOpenDocOptions_Silent, "", 0, 0)
                swModel = swApp.ActiveDoc

                If swModel IsNot Nothing Then
                    Dim feature As Feature = swModel.FirstFeature()
                    Dim isSheetMetal As Boolean = False
                    Dim swSheetMetal As SheetMetalFeatureData = Nothing

                    ' Percorre as features do modelo
                    Do While feature IsNot Nothing
                        If feature.GetTypeName2() = "SheetMetal" Then
                            swSheetMetal = TryCast(feature.GetDefinition(), SheetMetalFeatureData)
                            If swSheetMetal IsNot Nothing Then
                                isSheetMetal = True ' Confirma que o arquivo é um Sheet Metal
                                Exit Do
                            End If
                        End If
                        feature = feature.GetNextFeature()
                    Loop

                    ' Se for Sheet Metal, aplica as alterações
                    If isSheetMetal AndAlso swSheetMetal IsNot Nothing Then
                        Dim fileName As String = Path.GetFileNameWithoutExtension(filePath) ' Nome do arquivo sem extensão

                        ' Define "Assunto" como o nome do arquivo
                        swModel.SummaryInfo(swSummInfoField_e.swSumInfoSubject) = fileName

                        ' Define "Título" como o caminho completo do arquivo
                        swModel.SummaryInfo(swSummInfoField_e.swSumInfoTitle) = filePath

                        Dim swModelExt As ModelDocExtension = swModel.Extension
                        If swModelExt IsNot Nothing Then
                            ' Definição da cor em RGB normalizado (valores entre 0 e 1)
                            ' Verde-claro (RGB: 144, 238, 144) -> Normalizado: (0.564, 0.933, 0.564)
                            ' O último valor de cada grupo (0.0) indica "Sem Transparência"
                            Dim color As Object = {0.564, 0.933, 0.564, 0.0,  ' Ambiente
                           0.564, 0.933, 0.564, 0.0,  ' Difuso
                           0.564, 0.933, 0.564, 0.0}  ' Especular

                            ' Aplica a cor na configuração atual sem transparência
                            swModelExt.SetMaterialPropertyValues(color, swInConfigurationOpts_e.swThisConfiguration, True)

                            ' Atualiza a peça
                            'swModel.ForceRebuild3(False)

                        End If

                        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                        If TypeOf swSheetMetal Is SheetMetalFeatureData Then
                            ' Modifica o Fator K para 0,5 e ajusta o raio de dobra
                            Dim espessura As Double = swSheetMetal.Thickness * 1000
                            swSheetMetal.BendRadius = espessura
                            swSheetMetal.KFactor = 0.5

                            ' Aplica as mudanças e salva
                            ' Aplica as mudanças e salva
                            Try
                                Dim result As Boolean = feature.ModifyDefinition(swSheetMetal, swModel, Nothing)
                                If result Then
                                    swModel.ForceRebuild3(True)
                                    swModel.GraphicsRedraw2()
                                    swModel.Save()
                                Else
                                    MessageBox.Show("Falha ao modificar a definição da feature.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                            Catch ex As Exception
                                MessageBox.Show("Erro ao modificar a definição da feature: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End Try
                        Else
                            MessageBox.Show("swSheetMetal não é do tipo SheetMetalFeatureData.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    End If

                    swApp.CloseDoc(filePath)
                Else
                    'Console.WriteLine("Erro ao abrir arquivo: " & filePath)
                End If
            Next

            MessageBox.Show("Processo concluído com sucesso!", "Concluído", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Erro durante o processamento: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try

    End Sub

    Private Sub chkBoxProcessos_Click(sender As Object, e As EventArgs) Handles chkBoxProcessos.Click

        Try

            ' Verifique se o swModel foi aberto com sucesso
            If Not swModel Is Nothing Then

                ' Obtém o índice da linha selecionada
                Dim selectedIndex As Integer = chkBoxProcessos.SelectedIndex

                ' Verifica se há um item selecionado
                If selectedIndex <> -1 Then
                    ' Obtém o nome do processo sem espaços
                    Dim Processo As String = "txt" & Replace(chkBoxProcessos.Items(selectedIndex).ToString(), " ", "")

                    ' Verifica se o item está marcado corretamente
                    If chkBoxProcessos.GetItemChecked(selectedIndex) Then
                        ' Criar a propriedade com valor "1" se estiver marcado
                        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, Processo, "", "")
                        ' Atualiza o banco de dados
                        'edson23/04/2025    cl_BancoDados.AlteracaoEspecifica("material", Processo, "", "CodMatFabricante", DadosArquivoCorrente.NomeArquivoSemExtensao)
                    Else
                        ' Criar a propriedade vazia se não estiver marcado
                        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, Processo, "1", "1")
                        ' Atualiza o banco de dados com valor vazio
                        'edson23/04/2025  cl_BancoDados.AlteracaoEspecifica("material", Processo, "1", "CodMatFabricante", DadosArquivoCorrente.NomeArquivoSemExtensao)
                    End If
                Else
                    MessageBox.Show("Selecione um item antes de continuar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

            End If
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub tsbAjuda_Click(sender As Object, e As EventArgs)

        Dim url As String = "https://www.loom.com/share/e6fd1772bf0d4572ada1bb05b3a3cc0d?sid=789b0e1f-5b35-48f7-82d5-ed955f6559e0"

        Try
            ' Abre a URL no navegador padrão
            Process.Start(url)
        Catch ex As Exception
            ' Tratar erro, caso o processo falhe
            MessageBox.Show("Não foi possível abrir a URL: " & ex.Message)
        End Try

    End Sub



    Dim Formatadgvos As Boolean = False
    Private Sub dgvos_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvos.DataBindingComplete


        If dgvos Is Nothing OrElse dgvos.Rows Is Nothing Then Exit Sub



            ' Configuração estrutural (uma vez)
            PrepararColunasDgv()



        ' Visibilidades seguras
        OcultarColunas("ENDERECO", "idTag", "idProjeto", "Estatus",
                   "DataPrevisao", "Data_Liberacao_Engenharia",
                   "Liberado_Engenharia", "ProdutoPadrao", "idEmpresa")

            FixarColuna("idProjeto")





    End Sub

    ''' ' Coloque no Form (campo para evitar reconfigurar a cada bind)
    Private _gridConfigured As Boolean = False

    Private Sub PrepararColunasDgv()
        If _gridConfigured Then Exit Sub

        ' Garante que a coluna de status exista e seja de imagem
        Dim colStatus As DataGridViewImageColumn = TryCast(dgvos.Columns("dgvStatus"), DataGridViewImageColumn)
        If colStatus Is Nothing Then
            colStatus = New DataGridViewImageColumn() With {
                .Name = "dgvStatus",
                .HeaderText = "Status",
                .ImageLayout = DataGridViewImageCellLayout.Zoom,
                .Width = 28,
                .ReadOnly = True
            }
            ' Opcional: fixa como primeira coluna
            dgvos.Columns.Insert(0, colStatus)
        End If

        ' Ajustes gerais de estilo (uma vez só)
        dgvos.AutoGenerateColumns = True
        dgvos.RowHeadersVisible = False
        dgvos.AllowUserToAddRows = False
        dgvos.AllowUserToDeleteRows = False
        dgvos.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        _gridConfigured = True
    End Sub

    Private Sub OcultarColunas(ParamArray nomes() As String)
        For Each nome In nomes
            If dgvos.Columns.Contains(nome) Then
                dgvos.Columns(nome).Visible = False
            End If
        Next
    End Sub

    Private Sub FixarColuna(nome As String)
        If dgvos.Columns.Contains(nome) Then
            dgvos.Columns(nome).Frozen = True
        End If
    End Sub

    ' >>> NOVO: Usa CellFormatting para renderizar a imagem sem percorrer todas as linhas
    Private Sub dgvos_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvos.CellFormatting
        If dgvos.Columns(e.ColumnIndex).Name = "dgvStatus" Then
            ' Lê o valor da coluna de origem com segurança (pode ser DBNull/Nothing)
            Dim valor As Object = Nothing
            If dgvos.Columns.Contains("Liberado_Engenharia") Then
                valor = dgvos.Rows(e.RowIndex).Cells("Liberado_Engenharia").Value
            End If

            Dim texto As String = If(valor Is Nothing OrElse valor Is DBNull.Value, "", Convert.ToString(valor))
            e.Value = If(texto.Equals("S", StringComparison.OrdinalIgnoreCase),
                         My.Resources.verificado1,
                         My.Resources.atencao)
            e.FormattingApplied = True
        End If
    End Sub

    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tbsAjudaLeituraBOM_Click(sender As Object, e As EventArgs)

        AjudaLynx.Show()

        'Dim url As String = "https://www.loom.com/share/cc8db41b208e482f9d0c69c7bae6ecb3?sid=9dc82fd3-aa65-44e9-82d3-3cf7494bd509"

        'Try
        '    ' Abre a URL no navegador padrão
        '    Process.Start(url)
        'Catch ex As Exception
        '    ' Tratar erro, caso o processo falhe
        '    MessageBox.Show("Não foi possível abrir a URL: " & ex.Message)
        'End Try

    End Sub

    Private Sub ToolStripButton10_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton10.Click
        AjudaLynx.Show()

        'Dim url As String = "https://www.loom.com/share/197ffb2c9d6b485fbc6174a67ff6badc?sid=14756853-f898-48c1-8ce4-59d404b821ae"

    End Sub

    Private Sub tsbAjuda_Click_1(sender As Object, e As EventArgs) Handles tsbAjuda.Click
        AjudaLynx.Show()
    End Sub

    Private Sub ToolStripButton11_Click(sender As Object, e As EventArgs) Handles ToolStripButton11.Click
        AjudaLynx.Show()
    End Sub

    Private Sub TSBAssociarMaterial_Click_1(sender As Object, e As EventArgs) Handles TSBAssociarMaterial.Click

        If swModel Is Nothing Then

            Exit Sub
        Else

            'Usa Select Case para diferenciar o tipo do documento
            If swModel.GetType() = swDocumentTypes_e.swDocPART Or swModel.GetType() = swDocumentTypes_e.swDocASSEMBLY Then

                '  DadosArquivoCorrente.AtualizaDesenho(swModel)

                If String.IsNullOrWhiteSpace(DadosArquivoCorrente.NomeArquivoSemExtensao) Then

                    MessageBox.Show("Não há desenho ativo para associar material.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    Exit Sub
                Else

                    If My.Settings.TipoConexao = "MYSQL" Then

                        Using formMateriaisAlmoxarifado As New frmMateriaisAlmoxarifado ' MateriaisAlmoxarifado

                            cl_BancoDados.RetornaCampoDaPesquisa("Select IdMaterial from  " & ComplementoTipoBanco & "material where CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'", "IdMaterial")

                            Try

                                DadosArquivoCorrente.IdMaterial = VCampo0
                            Catch ex As Exception

                                MsgBox("O desenho corrente não esta salvo no banco de dados, ou seja não esta pronto para associr material ao desenho corrente!", vbInformation, "Atenção")

                            End Try

                            MateriaisAlmoxarifado.ShowDialog()

                            TimerMontaPeca.Enabled = True

                        End Using

                    End If

                End If

            End If

        End If
    End Sub

    Private Sub ToolStripButton8_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton8.Click

        Try

            Dim ArquivoDXF As String = DadosArquivoCorrente.EnderecoArquivo  ' dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoDXF = Path.ChangeExtension(ArquivoDXF, ".DXF")

            ' Obtém o caminho completo
            ArquivoDXF = Path.GetFullPath(ArquivoDXF)
            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoDXF) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoDXF)

                    p.Start()
                    ' p.WaitForExit()

                    dgvDataGridBOM.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            ' MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try
    End Sub

    Private Sub TSBConverterPDF_Click_1(sender As Object, e As EventArgs) Handles TSBConverterPDF.Click

        Try

            ' Verifique se o modelo foi aberto com sucesso
            If Not swModel Is Nothing Then

                ' Usa Select Case para diferenciar o tipo do documento
                If swModel.GetType() = swDocumentTypes_e.swDocDRAWING Then

                    Try

                        IntanciaSolidWorks.ConectarSolidWorks()
                        'swApparq = CreateObject("SldWorks.Application")

                        swModel = swapp.ActiveDoc
                        'swModel = swApparq.ActiveDoc

                        swModel.Visible = True

                        swModelDocExt = swModel.Extension

                        If DadosArquivoCorrente.Bloqueado = "S" Then

                            MsgBox("O arquivo esta bloqueado para conversão em PDF", vbInformation, "Conversão em PDF não permitida")

                            Exit Sub

                        End If

                        DadosArquivoCorrente.ExportToPDF(swModel, swModel.GetPathName.ToString, True)

                        chkVerificarPDF.Checked = True
                    Catch ex As Exception

                        MsgBox(ex.Message & " o arquivo não e valido para conversão em PDF")
                    Finally

                    End Try

                    Vem_de_Onde = "PDF"
                    TimerAviso.Enabled = True

                    ' MsgBox("O arquivo foi exportado com sucesso", vbInformation, "Conversão em PDF concluida com sucesso")

                End If

            End If
        Catch ex As Exception
        Finally

        End Try
    End Sub

    Private Sub tsbConverterDXF_Click_1(sender As Object, e As EventArgs) Handles tsbConverterDXF.Click
        Try

            'Verifique se o modelo foi aberto com sucesso
            If Not swModel Is Nothing Then

                'Usa Select Case para diferenciar o tipo do documento
                If swModel.GetType() = swDocumentTypes_e.swDocPART Then

                    IntanciaSolidWorks.ConectarSolidWorks()

                    swModel = swapp.ActiveDoc
                    'swModel = swApparq.ActiveDoc

                    swModel.Visible = True

                    swModelDocExt = swModel.Extension

                    If DadosArquivoCorrente.Bloqueado = "S" Then

                        MsgBox("O arquivo esta bloqueado para conversão em DXF", vbInformation, "Conversão em DXF não permitida")
                        Exit Sub

                    End If

                    Try

                        ' DadosArquivoCorrente.ExportDXF3D(swapp, swModel, False)

                        ' If DadosArquivoCorrente.ExportDXFFerramentaConformacao(swModel, True, True) Then
                        If DadosArquivoCorrente.ExportDXF2(swModel, True, True) = True Then

                            chkVerificarDXF.Checked = True
                            chkVerificarLXDS.Checked = False
                        Else

                            chkVerificarDXF.Checked = False
                            chkVerificarLXDS.Checked = False

                        End If

                        '  Me.cboTitulo.Text = DadosArquivoCorrente.Titulo

                        ' DadosArquivoCorrente.ExportSheetMetalBlankToDXF(swModel.GetPathName, Path.ChangeExtension(swModel.GetPathName, ".dxf"))
                    Catch ex As Exception

                        MsgBox(ex.Message & " o arquivo não e valido para conversão em dxf")
                    Finally

                    End Try
                End If

                Vem_de_Onde = "DXF"
                TimerAviso.Enabled = True
                ' MsgBox("O arquivo foi exportado com sucesso", vbInformation, "Conversão em DXF concluida com sucesso")

            End If
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub tsbSalvar_Click_1(sender As Object, e As EventArgs) Handles tsbSalvar.Click

        Try

            Me.ValidateChildren()

            IntanciaSolidWorks.ConectarSolidWorks()

            If swModel Is Nothing Then

                Exit Sub

            Else

                swModel.Save()

                Dim peloMenosUmSelecionado As Boolean = False

                For i As Integer = 0 To chkBoxProcessos.Items.Count - 1
                    If chkBoxProcessos.GetItemChecked(i) Then
                        Dim itemSelecionado As Object = chkBoxProcessos.Items(i)
                        Dim textoProcesso As String = itemSelecionado.ToString()

                        'Verificar o inicio do nome do arquivo com valor numerico sempre marcar o setor de corte
                        'desde que tenha espessura e material.

                        peloMenosUmSelecionado = True

                        SALVAR = True

                        If SALVAR = True Then
                            Exit For
                        End If

                    End If

                Next

                If Not peloMenosUmSelecionado Then
                    MessageBox.Show("Selecione pelo menos um processo de fabricação antes de continuar.",
                       "Atenção",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Warning)
                    SALVAR = False
                    Exit Sub
                End If

                peloMenosUmSelecionado = False

                For Each itemSelecionado As Object In chkBoxTipoDesenho.CheckedItems
                    Dim textoProcesso As String = itemSelecionado.ToString()

                    peloMenosUmSelecionado = True

                    If SALVAR Then
                        Exit For
                    End If
                Next

                If Not peloMenosUmSelecionado Then
                    MessageBox.Show("Selecione pelo menos o tipo de fabricação antes de continuar.",
                       "Atenção",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Warning)
                    SALVAR = False
                    Exit Sub
                End If

                DadosArquivoCorrente.SalvarPrintDoModelo(My.Settings.EnderecoImagens & "\" & DadosArquivoCorrente.NomeArquivoSemExtensao & ".png", swapp.ActiveDoc)

                DadosArquivoCorrente.EnderecoImagem = (My.Settings.EnderecoImagens & "\" & DadosArquivoCorrente.NomeArquivoSemExtensao & ".png")

                If SALVAR = True Then

                    Vem_de_Onde = "Salvar"

                    TimerAviso.Enabled = True

                End If

            End If

        Catch ex As Exception

        Finally

        End Try

    End Sub

    Private Sub tsBLerDados_Click_1(sender As Object, e As EventArgs) Handles tsBLerDados.Click

        Try

            IntanciaSolidWorks.ConectarSolidWorks()
            ' swApparq = CreateObject("SldWorks.Application")

            swModel = swapp.ActiveDoc
            'swModel = swApparq.ActiveDoc

            If swModel Is Nothing Then

                Exit Sub

            Else


                swModel.Save()

                DadosArquivoCorrente.PercorrerPropriedadesDaListaDeCorte(swModel)

                'dados da caixa delimitadora
                DadosArquivoCorrente.LerDadosCaixaDelimitadora(swModel)

                'If vemdalista = False Then

                DadosArquivoCorrente.ExcluirCaixaDelimitadora(swModel)

                'edson 04/03/2025
                AtualizaTela(swModel, chkBoxProcessos)

                ' btnPendencias.Enabled = DadosArquivoCorrente.VerificarProcessodaPecaCorrente(swModel, True)

                Vem_de_Onde = "Leitura"
                TimerAviso.Enabled = True

                '  swModel.Save()

            End If


            'DadosArquivoCorrente.AtualizaDesenho(swModel)
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub ConfExportarArquivoParaOSToolStripMenuItem1_Click_1(sender As Object, e As EventArgs) Handles ConfExportarArquivoParaOSToolStripMenuItem1.Click

        ExportarParaOS.ShowDialog()

    End Sub

    Private Sub ToolStripButton5_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton5.Click

        If DescarregarLynx = False Then

            MsgBox("O Lynx esta desativado!, para fazer a Leitura da BOM o Lynx deve esta ativado.")

            Exit Sub
        Else

            vemdalista = True

            Cursor.Current = Cursors.WaitCursor

            Try

                TabelaViewMontaPeca = cl_BancoDados.CarregarDados("SELECT * FROM  " & ComplementoTipoBanco & "viewmontapeca where D_E_L_E_T_E <> '' OR D_E_L_E_T_E IS NULL")

                ' Conectar ao SolidWorks
                IntanciaSolidWorks.ConectarSolidWorks()

                ' Obter o documento ativo
                swModel = swapp.ActiveDoc

                ' Verificar se o documento foi aberto
                If swModel Is Nothing Then
                    MsgBox("Nenhum documento ativo no SolidWorks.", vbCritical, "Erro")
                    Exit Sub
                End If

                ' Definir modelo como visível
                'swModel.Visible = True
                swModelDocExt = swModel.Extension

                ' Verifica o tipo de documento (Desenho)
                If swModel.GetType() = swDocumentTypes_e.swDocDRAWING Then

                    ' Obter o caminho completo do arquivo
                    DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(swModel.GetPathName().ToUpper())

                    ' Alterar a extensão para SLDASM
                    DadosArquivoCorrente.EnderecoArquivo = Path.ChangeExtension(DadosArquivoCorrente.EnderecoArquivo, "SLDASM")

                    DadosArquivoCorrente.ExportToPDF(swModel, DadosArquivoCorrente.EnderecoArquivo, True)
                    iconePDF = My.Resources.ficheiro_pdf

                    ' Verificar se o arquivo SLDASM existe
                    If File.Exists(DadosArquivoCorrente.EnderecoArquivo) Then
                        OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)
                    Else
                        MsgBox("O desenho de detalhamento não pertence a um desenho de conjunto!", vbCritical, "Atenção")
                        Exit Sub
                    End If

                    If chkTrocarFormato.Checked = True Then

                        DadosArquivoCorrente.TrocarFormatoA3(swModel)

                    End If

                End If

                ' Processar PART ou ASSEMBLY
                If swModel.GetType() = swDocumentTypes_e.swDocPART Or swModel.GetType() = swDocumentTypes_e.swDocASSEMBLY Then

                    If DadosArquivoCorrente.VerificarProcessodaPecaCorrente(swModel, False) = False Then

                        DadosArquivoCorrente.rnc = "S"
                        iconeAtencao = My.Resources.atencao
                    Else

                        DadosArquivoCorrente.rnc = ""
                        iconeAtencao = My.Resources.verificado

                    End If

                    If DadosArquivoCorrente.Corte = "" And
                                    DadosArquivoCorrente.Dobra = "" And
                                    DadosArquivoCorrente.Solda = "" And
                                    DadosArquivoCorrente.Pintura = "" Then
                        DadosArquivoCorrente.Montagem = "1"

                        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtmontagem", "1", "1")

                    End If

                    If DadosArquivoCorrente.TipoDesenho = "" Then

                        DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "txtipodesenho", "CHAPARIA", "CHAPARIA")

                    End If

                    DadosArquivoCorrente.rnc = ""

                    FormatarColunaIconeDGVListaBom(DadosArquivoCorrente.EnderecoArquivo)

                    qtdePecaLm = 1

                    DadosArquivoCorrente.Corte = ""

                    ' Preencher o DataGridView com os dados da peça
                    dgvDataGridBOM.Rows.Add(My.Resources.Sem_Incone,
                                        iconeDXF,
                                        iconePDF,
                                        iconeTipoArquivo,
                                    iconeAtencao,
                                    DadosArquivoCorrente.IdMaterial,
                                    DadosArquivoCorrente.NomeArquivoSemExtensao,
                                    DadosArquivoCorrente.Titulo,
                                    DadosArquivoCorrente.AssuntoSubiTitulo,
                                    DadosArquivoCorrente.Author,
                                    DadosArquivoCorrente.PalavraChave,
                                    DadosArquivoCorrente.Comentarios,
                                    DadosArquivoCorrente.Espessura,
                                    DadosArquivoCorrente.ComprimentoBlank,
                                    DadosArquivoCorrente.LarguraBlank,
                                    DadosArquivoCorrente.material,
                                    DadosArquivoCorrente.AreaPintura,
                                    DadosArquivoCorrente.NumeroDobras,
                                    DadosArquivoCorrente.Massa,
                                    DadosArquivoCorrente.EnderecoArquivo,
                                    DadosArquivoCorrente.Acabamento,
                                    DadosArquivoCorrente.soldagem,
                                    DadosArquivoCorrente.TipoDesenho,
                                    DadosArquivoCorrente.Corte,
                                    DadosArquivoCorrente.Dobra,
                                    DadosArquivoCorrente.Solda,
                                    DadosArquivoCorrente.Pintura,
                                    DadosArquivoCorrente.Montagem,
                                    DadosArquivoCorrente.rnc,
                                    DadosArquivoCorrente.Alturacaixadelimitadora,
                                    DadosArquivoCorrente.Larguracaixadelimitadora,
                                    DadosArquivoCorrente.Profundidadeaixadelimitadora,
                                    DadosArquivoCorrente.ItemEstoque,
                                    qtdePecaLm, DadosArquivoCorrente.Bloqueado,
                                    "")

                    iconeDXF = My.Resources.Sem_Incone
                    iconePDF = My.Resources.Sem_Incone
                    iconeTipoArquivo = My.Resources.Sem_Incone
                    iconeAtencao = My.Resources.Sem_Incone

                    ' Ler dados da view de montagem
                    LerDadosViewMontaPeca()

                    Try

                        ' Fechar o documento
                        swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                        cl_BancoDados.FecharArquivoMemoria()
                        IntanciaSolidWorks.LiberarRecurso(swModel)
                    Catch ex As Exception
                    Finally

                    End Try

                    ' Processar a lista de material
                    ProcessarListaMaterial(swModel)

                End If
            Catch ex As Exception
                MsgBox("Erro: " & ex.Message, vbCritical, "Erro")
                ' Fechar o documento
                swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                cl_BancoDados.FecharArquivoMemoria()
                IntanciaSolidWorks.LiberarRecurso(swModel)
            Finally
            End Try

        End If

        vemdalista = False

    End Sub

    Private Sub ToolStripButton2_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Try

            Dim ArquivoLXDS As String = DadosArquivoCorrente.EnderecoArquivo  'dgvDataGridBOM.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoLXDS = Path.ChangeExtension(ArquivoLXDS, ".LXDS")

            ' Obtém o caminho completo
            ArquivoLXDS = Path.GetFullPath(ArquivoLXDS)
            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoLXDS) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoLXDS)

                    p.Start()
                    'p.WaitForExit()

                    dgvDataGridBOM.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            '  MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try
    End Sub

    Private Sub tsbInserirNaOS_Click_1(sender As Object, e As EventArgs) Handles tsbInserirNaOS.Click


        If swModel Is Nothing Then

            Exit Sub

        End If


        Try
            If OrdemServico Is Nothing OrElse String.IsNullOrWhiteSpace(OrdemServico.IdOrdemServico) = True Or OrdemServico.IdOrdemServico = 0 Then
                MessageBox.Show("Não há ordem de serviço selecionada!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            ' Continua o fluxo normal aqui...

        Catch ex As Exception
            MessageBox.Show($"Erro ao validar a ordem de serviço: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Verifique se o modelo foi aberto com sucesso
        ' If swModel Is Nothing Then


        If OrdemServico.Liberado_Engenharia <> "" Then

                MsgBox("Ordem de Serviço " & OrdemServico.IdOrdemServico & " já Liberada para Produção, não pode mais ser modificada!", vbCritical, "Atenção")

                Exit Sub
            Else
                ' Usa Select Case para diferenciar o tipo do documento
                If swModel.GetType() = swDocumentTypes_e.swDocPART Then

                    Dim result As DialogResult = MessageBox.Show("Deseja Realmente Inserir o desenho corrente na OS: " & Me.lblOrdemServicoAtiva.Text, "Inserção de Item na OS", MessageBoxButtons.YesNo)

                    If result = DialogResult.Yes Then

                        ' Dim rnc As String

                        Dim novaqtde As String = 0

                        Dim PecaNova As Boolean = False

                        novaqtde = InputBox("Informe a quantidade total de peças a serem fabricadas", "Quantidade Total", 1)

                        ' Verifica se o usuário clicou em "Cancelar" (Fator será uma string vazia)
                        If novaqtde = "" Or novaqtde <= 0 Then

                            MsgBox("A Operação foi cancelda", vbInformation, "Atenação")

                            Exit Sub ' Sai do procedimento
                        End If

                        Dim DATAATUAL As String = Date.Now

                        Dim query As String = "INSERT INTO  " & ComplementoTipoBanco & " ordemservicoitem (
	IdOrdemServico, IdProjeto, Projeto, IdTag, Tag,
    Estatus_OrdemServico, DescResumo, DescDetal,
    Autor, PalavraChave, Notas, Espessura, AreaPintura,
    NumeroDobras, Peso, Unidade, UnidadeSW, Altura,
    Largura, CodMatFabricante, DtCad, UsuarioCriacao,
    UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW,
    QtdeTotal, CriadoPor,
    DataCriacao, Estatus, Acabamento, D_E_L_E_T_E, Fator, Qtde,
    txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda,
    txtPintura, txtMontagem, ComprimentoCaixaDelimitadora,
    LarguraCaixaDelimitadora, EspessuraCaixaDelimitadora,
    AreaPinturaUnitario, PesoUnitario, txtItemEstoque, DataPrevisao,
    sttxtCorte, sttxtDobra, sttxtSolda, sttxtPintura, sttxtMontagem
   ) VALUES (
	@IdOrdemServico, @IdProjeto, @Projeto, @IdTag, @Tag,
	@Estatus_OrdemServico, @DescResumo, @DescDetal,
	@Autor, @PalavraChave, @Notas, @Espessura, @AreaPintura,
	@NumeroDobras, @Peso, @Unidade, @UnidadeSW, @Altura,
	@Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao,
	@UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW,
	@QtdeTotal,@CriadoPor,
	@DataCriacao, @Estatus, @Acabamento, @D_E_L_E_T_E, @Fator, @Qtde,
	@txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda,
	@txtPintura, @txtMontagem, @ComprimentoCaixaDelimitadora,
	@LarguraCaixaDelimitadora, @EspessuraCaixaDelimitadora,
	@AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque, @DataPrevisao,
    @sttxtCorte, @sttxtDobra, @sttxtSolda, @sttxtPintura, @sttxtMontagem)"

                        Try

                            ' Inicia a transação
                            '  transaction = myconect.BeginTransaction()

                            cl_BancoDados.AbrirBanco()

                            Using command As New MySqlCommand(query, myconect)

                                ' Valores numéricos convertidos corretamente
                                Dim areaPinturaValor As Double = Convert.ToDouble(DadosArquivoCorrente.AreaPintura, CultureInfo.InvariantCulture)
                                Dim massaValor As Double = Convert.ToDouble(DadosArquivoCorrente.Massa, CultureInfo.InvariantCulture)
                                Dim qtdeValor As Double = Convert.ToDouble(novaqtde, CultureInfo.InvariantCulture)
                                Dim areaPinturaUnitarioValor As Double = Convert.ToDouble(DadosArquivoCorrente.AreaPintura, CultureInfo.InvariantCulture)
                                Dim pesoUnitarioValor As Double = massaValor * qtdeValor

                                command.Parameters.AddWithValue("@IdOrdemServico", OrdemServico.IdOrdemServico)
                                command.Parameters.AddWithValue("@IdProjeto", OrdemServico.idProjeto)
                                command.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                                command.Parameters.AddWithValue("@IdTag", OrdemServico.idTag)
                                command.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                                command.Parameters.AddWithValue("@Estatus_OrdemServico", OrdemServico.Estatus)

                                command.Parameters.AddWithValue("@DescResumo", DadosArquivoCorrente.AssuntoSubiTitulo)
                                command.Parameters.AddWithValue("@DescDetal", DadosArquivoCorrente.Titulo)
                                command.Parameters.AddWithValue("@Autor", DadosArquivoCorrente.Author)
                                command.Parameters.AddWithValue("@PalavraChave", DadosArquivoCorrente.PalavraChave)
                                command.Parameters.AddWithValue("@Notas", DadosArquivoCorrente.Comentarios)
                                command.Parameters.AddWithValue("@Espessura", DadosArquivoCorrente.Espessura)
                                command.Parameters.AddWithValue("@AreaPintura", areaPinturaValor * qtdeValor)
                                command.Parameters.AddWithValue("@NumeroDobras", DadosArquivoCorrente.NumeroDobras)
                                command.Parameters.AddWithValue("@Peso", Replace(massaValor, ",", "."))

                                command.Parameters.AddWithValue("@Unidade", "PC")
                                If DadosArquivoCorrente.material IsNot Nothing Then
                                    command.Parameters.AddWithValue("@UnidadeSW", DadosArquivoCorrente.material)
                                Else
                                    command.Parameters.AddWithValue("@UnidadeSW", DBNull.Value)
                                End If

                                command.Parameters.AddWithValue("@Altura", "")
                                command.Parameters.AddWithValue("@Largura", "")
                                command.Parameters.AddWithValue("@CodMatFabricante", DadosArquivoCorrente.NomeArquivoComExtensao)
                                command.Parameters.AddWithValue("@DtCad", DATAATUAL)
                                command.Parameters.AddWithValue("@UsuarioCriacao", Usuario.NomeCompleto)
                                command.Parameters.AddWithValue("@UsuarioAlteracao", "")
                                command.Parameters.AddWithValue("@DtAlteracao", "")
                                command.Parameters.AddWithValue("@EnderecoArquivo", DadosArquivoCorrente.EnderecoArquivo)
                                command.Parameters.AddWithValue("@MaterialSW", DadosArquivoCorrente.material)
                                command.Parameters.AddWithValue("@QtdeTotal", Replace(qtdeValor, ",", "."))
                                ' command.Parameters.AddWithValue("@QtdeProduzida", "")
                                ' command.Parameters.AddWithValue("@QtdeFaltante", "")
                                command.Parameters.AddWithValue("@CriadoPor", Usuario.NomeCompleto)
                                command.Parameters.AddWithValue("@DataCriacao", DATAATUAL)
                                command.Parameters.AddWithValue("@Estatus", "A")
                                command.Parameters.AddWithValue("@Acabamento", DadosArquivoCorrente.Acabamento)
                                command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                                command.Parameters.AddWithValue("@Fator", Replace(qtdeValor, ",", "."))
                                command.Parameters.AddWithValue("@Qtde", Replace(qtdeValor, ",", "."))

                                command.Parameters.AddWithValue("@txtSoldagem", DadosArquivoCorrente.soldagem)
                                command.Parameters.AddWithValue("@txtTipoDesenho", DadosArquivoCorrente.TipoDesenho)
                                command.Parameters.AddWithValue("@txtCorte", DadosArquivoCorrente.Corte)
                                command.Parameters.AddWithValue("@txtDobra", DadosArquivoCorrente.Dobra)
                                command.Parameters.AddWithValue("@txtSolda", DadosArquivoCorrente.Solda)
                                command.Parameters.AddWithValue("@txtPintura", DadosArquivoCorrente.Pintura)
                                command.Parameters.AddWithValue("@txtMontagem", DadosArquivoCorrente.Montagem)

                                command.Parameters.AddWithValue("@ComprimentoCaixaDelimitadora", DadosArquivoCorrente.ComprimentoBlank)
                                command.Parameters.AddWithValue("@LarguraCaixaDelimitadora", DadosArquivoCorrente.Larguracaixadelimitadora)
                                command.Parameters.AddWithValue("@EspessuraCaixaDelimitadora", DadosArquivoCorrente.Profundidadeaixadelimitadora)

                                command.Parameters.AddWithValue("@AreaPinturaUnitario", Replace(areaPinturaUnitarioValor, ",", "."))
                                command.Parameters.AddWithValue("@PesoUnitario", Replace(pesoUnitarioValor, ",", "."))
                                command.Parameters.AddWithValue("@txtItemEstoque", OrdemServico.txtItemEstoque)

                                If OrdemServico.DataPrevisao IsNot Nothing Then
                                    command.Parameters.AddWithValue("@DataPrevisao", OrdemServico.DataPrevisao)
                                Else
                                    command.Parameters.AddWithValue("@DataPrevisao", DBNull.Value)
                                End If

                                command.Parameters.AddWithValue("@sttxtCorte", "")
                                command.Parameters.AddWithValue("@sttxtDobra", "")
                                command.Parameters.AddWithValue("@sttxtSolda", "")
                                command.Parameters.AddWithValue("@sttxtPintura", "")
                                command.Parameters.AddWithValue("@sttxtMontagem", "")

                                command.ExecuteNonQuery()

                            End Using

                            cl_BancoDados.FecharBanco()

                            'Inserir a função para carregar o material.
                            MaterialPecasAvulca(DadosArquivoCorrente.NomeArquivoSemExtensao, novaqtde)

                        Catch ex As Exception

                            MsgBox(ex.Message)
                        Finally

                        End Try

                        TimerDGVListaMaterialSW.Enabled = True

                    End If

                End If

            End If

        'End If
    End Sub



    Public Function MaterialPecasAvulca(ByVal codmatFabricante As String, entrada As String)

        Try

            Dim dtmontapeca As System.Data.DataTable =
                             cl_BancoDados.CarregarDados("SELECT * FROM viewmontapeca
                                                                   WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') AND 
                                                                         (NomeArquivoSemExtensao = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "')")

            If dtmontapeca IsNot Nothing AndAlso dtmontapeca.Rows.Count > 0 Then

                For Each linha As DataRow In dtmontapeca.Rows    '   cada item do montapeca será criado na nova ordem se servico

                    Dim PecaQtde, PesoUnitario As String
                    Dim QtdeTotal As Double
                    Dim Peso As Double

                    entrada = entrada.Replace(".", ",")

                    Try


                        PecaQtde = linha("PecaQtde").ToString
                    Catch ex As Exception
                        PecaQtde = 0
                    End Try

                    PecaQtde = PecaQtde.Replace(".", ",")

                    QtdeTotal = CDbl(entrada) * CDbl(PecaQtde)

                    Try
                        Peso = CDbl(linha("Peso").ToString.Replace(".", ","))

                    Catch ex As Exception
                        Peso = 0
                    End Try

                    Peso = CDbl(Peso) * CDbl(PecaQtde)

                    Try
                        PesoUnitario = linha("Peso").ToString

                        If PesoUnitario = "" Then

                            PesoUnitario = 0
                        End If
                    Catch ex As Exception
                        PesoUnitario = 0
                    End Try


                    ' BancoDados.RetornaCampoDaPesquisa("SELECT NOW() AS DataHoraAtual", "DataHoraAtual")
                    ' dataatual = CampoV1

                    Dim query As String = "
                            INSERT INTO ordemservicoitem (
                                IdOrdemServico, IdProjeto, Projeto, IdTag, Tag,
                                DescTag,ESTATUS_OrdemServico, IdMaterial, DescResumo, DescDetal,
                                Autor, Palavrachave, Notas, Espessura, AreaPintura,
                                NumeroDobras, Peso, Unidade, UnidadeSW, ValorSW,
                                Altura,Largura, CodMatFabricante, DtCad, UsuarioCriacao,
                                UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW,QtdeTotal,
                                CriadoPor,DataCriacao, Estatus, Acabamento, D_E_L_E_T_E, 
                                Fator, qtde,txtSoldagem, txtTipoDesenho, txtCorte,
                                txtDobra, txtSolda, txtPintura, txtMontagem, TttxtCorte,
                                TttxtDobra, TttxtSolda,TttxtPintura, TttxtMontagem, CorteTotalExecutar,
                                DobraTotalExecutar, SoldaTotalExecutar,PinturaTotalExecutar, MontagemTotalExecutar, Comprimentocaixadelimitadora,
                                Larguracaixadelimitadora, Espessuracaixadelimitadora, AreaPinturaUnitario, PesoUnitario, txtItemEstoque,
                                DataPrevisao,Liberado_Engenharia, DATA_LIBERACAO_ENGENHARIA, OrdemServicoItemFinalizado, sttxtCorte,            
                                sttxtDobra, sttxtSolda, sttxtPintura, sttxtMontagem, ProdutoPrincipal,
                                EnderecoArquivoItemOrdemServico, IdEmpresa, DescEmpresa,NumeroOpOmie
                            ) value (                           
                                '" & OrdemServico.IdOrdemServico & "',
                                '" & OrdemServico.idProjeto & "',
                                '" & OrdemServico.Projeto & "',
                                '" & OrdemServico.idTag & "',
                                '" & OrdemServico.Tag & "',
                                '" & OrdemServico.DescTag & "', 
                                '', -- ESTATUS_OrdemServico
                                0, 
                                '', -- DescResumo
                                '" & linha("DescDetal").ToString & "', 
                                '', --   Autor
                                '', --   Palavrachave
                                '', --  Notas,
                                '', --  Espessura
                                0, --  AreaPintura
                                0, --  NumeroDobras
                                " & Peso.ToString.Replace(",", ".") & ", --  Peso
                                '" & linha("Unidade").ToString & "', --  Unidade
                                '', --   UnidadeSW
                                '', --   ValorSW
                                '', --      Altura
                                '', --  Largura
                                '" & linha("CodMatFabricante").ToString & "',
                               '', --  DtCad
                               '" & Usuario.NomeCompleto & "', --  UsuarioCriacao
                               '', --  UsuarioAlteracao
                               '', --  DtAlteracao
                               '', -- EnderecoArquivo
                               '',
                               '" & QtdeTotal.ToString.Replace(",", ".") & "', --  QtdeTotal   30
                               '" & Usuario.NomeCompleto & "', --  CriadoPor
                               '" & Date.Now & "', --  DataCriacao
                               '', --  
                               '', --  Acabamento,
                               '', --   D_E_L_E_T_E
                               '1', 
                               " & PecaQtde.Replace(",", ".") & ", 
                               '', --  txtSoldagem
                               'MATERIAL', --  txtTipoDesenho
                               '', --   txtCorte
                               '', --  txtDobra
                               '', --   txtSolda
                               '', --  txtPintura
                               '', --   txtMontagem
                               '', --  TttxtCorte
                               '', --   ttxtdobra 
                               '', --   ttxtsolda 
                               '', --   ttxtpintura
                               '', --  ttxtmontagem
                               0, -- CorteTotalExecutar   50
                               0, -- DobraTotalExecutar
                               0, --  SoldaTotalExecutar
                               0, -- PinturaTotalExecutar
                               0, --  MontagemTotalExecutar
                               '', -- Comprimentocaixadelimitadora
                               '', -- Larguracaixadelimitadora
                               '', --  Espessuracaixadelimitadora
                               0, --   AreaPinturaUnitario
                               " & PesoUnitario & ", --  PesoUnitario
                               '', --    txtItemEstoque
                               '" & OrdemServico.DataPrevisao & "',-- DataPrevisao
                               '',-- Liberado_Engenharia
                               '',-- DATA_LIBERACAO_ENGENHARIA
                               '', --   OrdemServicoItemFinalizado
                               '', --   sttxtCorte
                               '', --   sttxtdobra
                               '', --  - sttxtsolda
                               '', --   sttxtpintura
                               '', --   sttxtmontagem
                               '', --   ProdutoPrincipal    70
                               '', --   EnderecoArquivoItemOrdemServico
                               '" & OrdemServico.idempresa & "',
                               '" & OrdemServico.DescEmpresa & "',
                               '');"

                    cl_BancoDados.AbrirBanco()

                    Using da As New MySqlCommand(query, myconect)

                        da.ExecuteNonQuery()

                    End Using

                    cl_BancoDados.FecharBanco()


                Next

            End If

        Catch ex As Exception

        Finally

        End Try



    End Function

    Private Sub ConfiguraçãoToolStripMenuItem2_Click_1(sender As Object, e As EventArgs) Handles ConfiguraçãoToolStripMenuItem2.Click
        If InputBox("Senha de acesso", "Administrador", "") = "99678982" Then
            Dim OpenfileConfiguracao As New OpenFileDialog

            ' Configura o diálogo para aceitar somente arquivos de texto
            OpenfileConfiguracao.Filter = "Arquivos de Texto (*.txt)|*.txt|Todos os arquivos (*.*)|*.*"
            OpenfileConfiguracao.FilterIndex = 1

            ' Exibe o diálogo e verifica se o usuário selecionou um arquivo
            If OpenfileConfiguracao.ShowDialog() = DialogResult.OK Then
                Dim caminhoArquivo As String = OpenfileConfiguracao.FileName

                ' Verifica se o arquivo existe
                If File.Exists(caminhoArquivo) Then
                    Try
                        ' Variáveis para armazenar os parâmetros
                        Dim endereco As String = ""
                        Dim usuario As String = ""
                        Dim banco As String = ""
                        Dim senha As String = ""
                        Dim EnderecoPastaRaizOS As String = ""
                        Dim CopiaBancoDados As String = ""
                        Dim EnderecoPastaRaizRomaneio As String = ""
                        Dim Enderecoplanodecorte As String = ""
                        Dim EnderecoTemplateExcelOrdemServico As String = ""
                        Dim EnderecoTemplateExcelRomaneio As String = ""
                        Dim templateplanodecorte As String = ""
                        Dim ParametroExportarDXF As String = ""
                        Dim EnderecoPastaRaizRNC As String = ""
                        Dim EnderecoTemplateExcelRNC As String = ""
                        Dim EnderecoImagens As String = ""
                        Dim ProgramaRM As String = ""
                        Dim TravarEmissaoOS As String = ""
                        Dim PlanilhaModeloOmie As String

                        ' Codificação utilizada na leitura do arquivo
                        Dim codificacao As Encoding = Encoding.GetEncoding("ISO-8859-1") ' Ajustável conforme o arquivo

                        ' Lê o arquivo linha por linha
                        Dim parametrosEncontrados As New Dictionary(Of String, String)
                        Using leitor As New StreamReader(caminhoArquivo, codificacao)
                            While Not leitor.EndOfStream
                                Dim linha As String = leitor.ReadLine()?.Trim()

                                ' Ignorar linhas em branco ou mal formadas
                                If String.IsNullOrEmpty(linha) OrElse Not linha.Contains(";") Then Continue While

                                Dim partes = linha.Split(";"c)
                                If partes.Length = 2 Then
                                    Dim chave = partes(0).Trim()
                                    Dim valor = partes(1).Trim()

                                    ' Armazena no dicionário
                                    If Not parametrosEncontrados.ContainsKey(chave) Then
                                        parametrosEncontrados(chave) = valor
                                    End If
                                End If
                            End While
                        End Using

                        ' Atribui valores ao My.Settings
                        Dim parametrosNecessarios = {"endereco", "Usuario", "Banco", "Senha", "EnderecoPastaRaizOS", "EnderecoTemplateExcelOrdemServico", "ParametroExportarDXF", "EnderecoImagens", "ProgramaRM", "TravarEmissaoOS", "PlanilhaModeloOmie"}
                        For Each param In parametrosNecessarios
                            If Not parametrosEncontrados.ContainsKey(param) Then
                                MsgBox($"Erro: Parâmetro '{param}' não encontrado no arquivo de configuração!", MsgBoxStyle.Critical)
                                Return
                            End If
                        Next

                        My.Settings.MySqlBancoDados = parametrosEncontrados("Banco")
                        My.Settings.MysqlEndereco = parametrosEncontrados("endereco")
                        My.Settings.MysqlUsuario = parametrosEncontrados("Usuario")
                        My.Settings.MysqlSenha = parametrosEncontrados("Senha")
                        My.Settings.BancoDadosAtivo = parametrosEncontrados("Banco")
                        My.Settings.EnderecoImagens = parametrosEncontrados("EnderecoImagens")

                        My.Settings.EnderecoPastaRaizOS = parametrosEncontrados("EnderecoPastaRaizOS")
                        My.Settings.EnderecoTemplateExcel = parametrosEncontrados("EnderecoTemplateExcelOrdemServico")
                        My.Settings.ParametroExportarDXF = parametrosEncontrados("ParametroExportarDXF")
                        My.Settings.ProgramaRM = parametrosEncontrados("ProgramaRM")
                        My.Settings.TravarEmissaoOS = parametrosEncontrados("TravarEmissaoOS")
                        My.Settings.PlanilhaModeloOmie = parametrosEncontrados("PlanilhaModeloOmie")

                        'My.Settings.SQLServerProtheus = parametrosEncontrados("SQLServerProtheus")

                        ' Salva as configurações
                        My.Settings.Save()

                        MsgBox("Configurações carregadas com sucesso!", MsgBoxStyle.Information)

                        ' Obtém a instância do SolidWorks se estiver aberta
                        swapp = TryCast(Marshal.GetActiveObject("SldWorks.Application"), SldWorks)

                        If swapp IsNot Nothing Then
                            '	Console.WriteLine("SolidWorks encontrado! Fechando...")

                            ' Fecha o SolidWorks
                            swapp.ExitApp()

                            ' Libera a referência da memória
                            Marshal.ReleaseComObject(swapp)
                            swapp = Nothing

                        End If
                    Catch ex As Exception
                        MsgBox($"Erro ao processar o arquivo de configuração: {ex.Message}", MsgBoxStyle.Critical)
                    End Try
                Else
                    MsgBox("Arquivo selecionado não encontrado!", MsgBoxStyle.Exclamation)
                End If
            End If
        Else
            MsgBox("Senha inválida!", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub TsbInspecaoQualidade_Click_1(sender As Object, e As EventArgs) Handles TsbInspecaoQualidade.Click
        Try

            If swModel Is Nothing Then

                Exit Sub
            Else

                If DadosArquivoCorrente.NomeArquivoSemExtensao.ToString <> "" Then

                    cl_BancoDados.RetornaCampoDaPesquisa("Select CodMatFabricante from material where CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'", "CodMatFabricante")

                    If VCampo0 = "" Then

                        MsgBox("O Arquivo ainda não foi salvo no sistema, o cadastro deve ser executado antes da execução do processo de controle da qualidade", vbInformation, "Atenção")

                        Exit Sub
                    Else

                        InspecaoQualidade.ShowDialog()

                    End If

                End If

            End If
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub ToolStripButton4_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        dgvDataGridBOM.DataSource = Nothing
        dgvDataGridBOM.Rows.Clear()
        dgvDataGridBOM.Refresh()
    End Sub

    Private Sub tsbProcessaoListaMaterialBOM_Click_1(sender As Object, e As EventArgs) Handles tsbProcessaoListaMaterialBOM.Click

        Cursor.Current = Cursors.WaitCursor

        Dim RowCount As Integer = dgvDataGridBOM.Rows.Count

        If RowCount > 0 Then

            dgvDataGridBOM.SuspendLayout()

            ProgressBarListaSW.Minimum = 0
            ProgressBarListaSW.Maximum = RowCount

            '  Dim arquivoSLDDRW As String

            ' Exibe a caixa de mensagem com um aviso e opções Sim e Não
            Dim result As DialogResult = MessageBox.Show("Esta operação irá processar todos os itens do Grid, abrindo os desenhos e atualizando os dados cadastrais. Este procedimento pode levar algum tempo. Você deseja prosseguir?", "Atualizar Dados", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then

                Dim bloqueado As String

                For i As Integer = 0 To RowCount - 1

                    Dim row As DataGridViewRow = dgvDataGridBOM.Rows(i)
                    ' Dim bloqueado As String = ""

                    Try
                        bloqueado = row.Cells("bloqueado").Value?.ToString()
                    Catch
                        bloqueado = ""
                    End Try

                    If bloqueado = "S" Then Continue For

                    Try
                        Dim enderecoOriginal As String = row.Cells("EnderecoArquivo").Value?.ToString()
                        DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(enderecoOriginal)

                        '26/07
                        '  If File.Exists(DadosArquivoCorrente.EnderecoArquivo) Then
                        OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)
                        ' End If

                        ' ========== DXF ==========
                        If chkConverterDXF.Checked = True And enderecoOriginal.EndsWith(".SLDPRT") Then

                            If DadosArquivoCorrente.EnderecoArquivo.ToUpper().EndsWith(".SLDPRT") Then

                                'If DadosArquivoCorrente.ExportDXFFerramentaConformacao(swModel, True, True) Then

                                If DadosArquivoCorrente.ExportDXF2(swModel, True, True) Then
                                    row.Cells("DGVIconeDXF").Value = My.Resources.arquivo_dxf
                                Else

                                    row.Cells("DGVIconeDXF").Value = My.Resources.Sem_Incone

                                End If

                                ' End If
                                ' End If

                            End If

                        End If

                        Dim enderecoArquivoUpper As String = DadosArquivoCorrente.EnderecoArquivo.ToUpper()
                        Dim arquivoSLDDRW As String = enderecoArquivoUpper

                        If enderecoArquivoUpper.EndsWith(".SLDPRT") OrElse enderecoArquivoUpper.EndsWith(".SLDASM") Then
                            arquivoSLDDRW = arquivoSLDDRW.Replace(".SLDPRT", ".SLDDRW").Replace(".SLDASM", ".SLDDRW")
                        End If

                        If File.Exists(arquivoSLDDRW) Then

                            ' If choBloqueaArquivoExistente.Checked = False Then

                            If chkConverterPDF.Checked = True Or chkTrocarFormato.Checked = True Then

                                OpenDocumentAndWait(arquivoSLDDRW, True, swModel)

                                If chkTrocarFormato.Checked = True Then

                                    DadosArquivoCorrente.TrocarFormatoA3(swModel)

                                End If

                                ' ========== PDF ==========
                                If chkConverterPDF.Checked = True Then

                                    If DadosArquivoCorrente.ExportToPDF(swModel, arquivoSLDDRW, False) Then

                                        row.Cells("dgvIconePDF").Value = My.Resources.ficheiro_pdf
                                    Else
                                        row.Cells("dgvIconePDF").Value = My.Resources.Sem_Incone
                                    End If

                                End If
                            End If

                            ' End If

                        End If

                        ' ' Atualização de estilo e barra de progresso
                        row.Cells("CodMatFabricante").Style.BackColor = Color.LightGreen
                        ProgressBarListaSW.Value = i

                        swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                        cl_BancoDados.FecharArquivoMemoria()
                        IntanciaSolidWorks.LiberarRecurso(swModel)
                        IntanciaSolidWorks.LiberarRecurso(swPart)
                    Catch
                        row.Cells("CodMatFabricante").Style.BackColor = Color.LightPink
                        Continue For
                    Finally
                    End Try

                    FormatarColunaIconeDGVListaBom(DadosArquivoCorrente.EnderecoArquivo)
                    row.Cells("DGVIconeDXF").Value = iconeDXF
                    row.Cells("dgvIconePDF").Value = iconePDF

                Next

                ' ====== Limpeza final de documentos SLDPRT/SLDASM ======
                If RowCount > 0 Then
                    For a As Integer = 0 To RowCount - 1
                        Try
                            Dim row As DataGridViewRow = dgvDataGridBOM.Rows(a)
                            Dim enderecoArquivo As String = row.Cells("EnderecoArquivo").Value?.ToString().ToLower()

                            If enderecoArquivo.EndsWith(".sldprt") OrElse enderecoArquivo.EndsWith(".sldasm") OrElse enderecoArquivo.EndsWith(".slddrw") Then
                                swapp.CloseDoc(enderecoArquivo)
                                row.Cells("CodMatFabricante").Style.BackColor = Color.White
                            End If
                        Catch
                            Continue For
                        End Try
                    Next
                End If

                dgvDataGridBOM.ResumeLayout()
                GC.Collect()
                GC.WaitForPendingFinalizers()

                MsgBox("Processo de Atualização Finalizado com sucesso!", vbInformation, "Informação")
                ProgressBarListaSW.Value = 0
                chkConverterPDF.Checked = False
                chkConverterDXF.Checked = False

                Cursor.Current = Cursors.Default

            End If

        End If

    End Sub

    Private Sub ToolStripButton7_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton7.Click

        Try

            If OrdemServico.IdOrdemServico = 0 Or OrdemServico.IdOrdemServico = Nothing Then
                MsgBox("Nenhuma Ordem de Serviço Ativa para Inserção de Itens!", vbCritical, "Atenção")
                Exit Sub
            End If

            If OrdemServico.Liberado_Engenharia.ToString <> "" Then
                MsgBox("Ordem de Serviço " & OrdemServico.IdOrdemServico & " já Liberada para Produção, não pode mais ser modificada!", vbCritical, "Atenção")
                Exit Sub
            End If

            If dgvDataGridBOM.Rows.Count <= 0 Then

                Exit Sub

            End If

            Cursor.Current = Cursors.WaitCursor

            ' Dim verificaRnc As Boolean = False

            Dim result As DialogResult = MessageBox.Show("Deseja Realmente Inserir os itens da lista BOM na OS: " & Me.lblOrdemServicoAtiva.Text, "Inserção de Itens da OS", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then

                Dim rnc As String

                Dim Fator As Double
                '  Try

                'cl_BancoDados.ProcessoAmdamento(dgvDataGridBOM.Rows.Count, "Inserindo itens na Ordem de serviço")

                ProgressBarListaSW.Minimum = 0

                ProgressBarListaSW.Maximum = dgvDataGridBOM.Rows.Count

                Dim rncValue As String

                If dgvDataGridBOM.Rows.Count > 0 Then

                    For b As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

                        '  Dim cellValue As String = dgvDataGridBOM.Rows(b).Cells("RNC").Value.ToString
                        rncValue = dgvDataGridBOM.Rows(b).Cells("RNC").Value.ToString 'If(cellValue IsNot Nothing AndAlso Not IsDBNull(cellValue), cellValue.ToString().Trim().ToUpper(), "")

                        If rncValue = "S" Then

                            dgvDataGridBOM.Rows(b).DefaultCellStyle.BackColor = Color.LightSalmon

                            MsgBox("Na lista, há peças com RNC pendente; para prosseguir com o processo de liberação, é necessário remover a peça da lista ou resolver a RNC.", vbCritical, "Atenção")

                            ' verificaRnc = True
                            Exit Sub ' Sai do loop se encontrar uma peça com RNC

                        End If
                        ' ContadorBarradeProgresso = b
                        ProgressBarListaSW.Value = b

                    Next

                    'If rncValue <> True Then

                    '    MsgBox("Na lista, há peças com RNC pendente; para prosseguir com o processo de liberação, é necessário remover a peça da lista ou resolver a RNC.", vbCritical, "Atenção")

                    '    ProgressBarListaSW.Value = 0

                    '    Exit Sub

                    'End If

                End If

                ProgressBarListaSW.Minimum = 0

                Try
                    ' Recupera o valor do saldo da tag, com fallback padrão
                    cl_BancoDados.RetornaCampoDaPesquisa(
            "SELECT SaldoTag FROM " & ComplementoTipoBanco & " tags WHERE IdProjeto = '" & OrdemServico.idProjeto & "' AND IdTag = '" & OrdemServico.idTag & "'",
            "SaldoTag")

                    Dim resultado As Object = VCampo0
                    OrdemServico.Fator = If(resultado IsNot Nothing AndAlso IsNumeric(resultado), Convert.ToInt32(resultado), 1)
                Catch ex As Exception
                    OrdemServico.Fator = 1
                End Try

                ' Solicita ao usuário o valor do fator de multiplicação
                Dim inputFator As String = InputBox(
        "Informe o Valor Multiplicador para fabricação." & vbCrLf &
        "O valor padrão é a quantidade de conjuntos solicitado pelo PCP no ato do cadastro da Tag.",
        "Fator de Multiplicação",
        OrdemServico.Fator.ToString())

                ' Valida a entrada
                If Not Integer.TryParse(inputFator, OrdemServico.Fator) OrElse OrdemServico.Fator <= 0 Then
                    MsgBox("O Fator de Multiplicação deve ser um número inteiro maior que zero.", vbCritical, "Atenção")
                    Exit Sub
                End If

                ' Atualiza o valor no banco
                cl_BancoDados.AlteracaoEspecifica("OrdemServico", "Fator", OrdemServico.Fator, "IdOrdemServico", OrdemServico.IdOrdemServico)

                ' Atualiza a célula da grid
                Try
                    dgvos.CurrentRow.Cells("Fator").Value = OrdemServico.Fator
                Catch ex As Exception
                    ' Em caso de erro, define 1 por padrão
                    dgvos.CurrentRow.Cells("Fator").Value = 1
                End Try

                '' Verifica pendência de RNC
                'If verificaRnc = True Then
                '    MsgBox("Há itens com RNC em aberto ou cadastro incompleto. Favor verificar os itens em vermelho.", vbCritical, "Atenção")
                '    Exit Sub
                'End If

                Dim query As String = "INSERT INTO  " & ComplementoTipoBanco & " ordemservicoitem (
	IdOrdemServico, IdProjeto, Projeto, IdTag, Tag,
    Estatus_OrdemServico, DescResumo, DescDetal,
    Autor, PalavraChave, Notas, Espessura, AreaPintura,
    NumeroDobras, Peso, Unidade, UnidadeSW, Altura,
    Largura, CodMatFabricante, DtCad, UsuarioCriacao,
    UsuarioAlteracao, DtAlteracao, EnderecoArquivo, MaterialSW,
    QtdeTotal, CriadoPor,
    DataCriacao, Estatus, Acabamento, D_E_L_E_T_E, Fator, Qtde,
    txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda,
    txtPintura, txtMontagem, ComprimentoCaixaDelimitadora,
    LarguraCaixaDelimitadora, EspessuraCaixaDelimitadora,
    AreaPinturaUnitario, PesoUnitario, txtItemEstoque, DataPrevisao,
    sttxtCorte, sttxtDobra, sttxtSolda, sttxtPintura, sttxtMontagem,descempresa,IdEmpresa,DescTag
   ) VALUES (
	@IdOrdemServico, @IdProjeto, @Projeto, @IdTag, @Tag,
	@Estatus_OrdemServico, @DescResumo, @DescDetal,
	@Autor, @PalavraChave, @Notas, @Espessura, @AreaPintura,
	@NumeroDobras, @Peso, @Unidade, @UnidadeSW, @Altura,
	@Largura, @CodMatFabricante, @DtCad, @UsuarioCriacao,
	@UsuarioAlteracao, @DtAlteracao, @EnderecoArquivo, @MaterialSW,
	@QtdeTotal,  @CriadoPor,
	@DataCriacao, @Estatus, @Acabamento, @D_E_L_E_T_E, @Fator, @Qtde,
	@txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda,
	@txtPintura, @txtMontagem, @ComprimentoCaixaDelimitadora,
	@LarguraCaixaDelimitadora, @EspessuraCaixaDelimitadora,
	@AreaPinturaUnitario, @PesoUnitario, @txtItemEstoque, @DataPrevisao,
    @sttxtCorte, @sttxtDobra, @sttxtSolda, @sttxtPintura, @sttxtMontagem,@descempresa,@IdEmpresa,@DescTag)"

                ProgressBarListaSW.Minimum = 0
                ProgressBarListaSW.Maximum = dgvDataGridBOM.Rows.Count

                cl_BancoDados.AbrirBanco()

                If dgvDataGridBOM.Rows.Count > 0 AndAlso OrdemServico.IdOrdemServico <> 0 Then

                    For A As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

                        Try

                            If dgvDataGridBOM.Rows(A).Cells("CodMatFabricante").Value.ToString <> "" Or dgvDataGridBOM.Rows(A).Cells("CodMatFabricante").Value.ToString <> Nothing Then

                                OrdemServico.EnderecoArquivo = GetCellStringValue(dgvDataGridBOM, A, "EnderecoArquivo")
                                OrdemServico.CodMatFabricante = GetCellStringValue(dgvDataGridBOM, A, "CodMatFabricante")
                                OrdemServico.DescResumo = GetCellStringValue(dgvDataGridBOM, A, "DescResumo")
                                OrdemServico.DescDetal = GetCellStringValue(dgvDataGridBOM, A, "DescDetal")
                                OrdemServico.Autor = GetCellStringValue(dgvDataGridBOM, A, "Autor")
                                OrdemServico.Palavrachave = GetCellStringValue(dgvDataGridBOM, A, "Palavrachave")
                                OrdemServico.Notas = GetCellStringValue(dgvDataGridBOM, A, "Notas")
                                OrdemServico.MaterialSW = GetCellStringValue(dgvDataGridBOM, A, "material")
                                OrdemServico.txtSoldagem = GetCellStringValue(dgvDataGridBOM, A, "txtSoldagem", False)
                                OrdemServico.txtTipoDesenho = GetCellStringValue(dgvDataGridBOM, A, "txtTipoDesenho", False)
                                OrdemServico.txtCorte = GetCellStringValue(dgvDataGridBOM, A, "txtCorte", False)
                                OrdemServico.txtDobra = GetCellStringValue(dgvDataGridBOM, A, "txtDobra", False)
                                OrdemServico.txtSolda = GetCellStringValue(dgvDataGridBOM, A, "txtSolda", False)
                                OrdemServico.txtPintura = GetCellStringValue(dgvDataGridBOM, A, "txtPintura", False)
                                OrdemServico.txtMontagem = GetCellStringValue(dgvDataGridBOM, A, "txtMontagem", False)
                                OrdemServico.txtItemEstoque = GetCellStringValue(dgvDataGridBOM, A, "txtItemEstoque")
                                OrdemServico.txtAcabamento = GetCellStringValue(dgvDataGridBOM, A, "Acabamento")
                                OrdemServico.bloqueado = GetCellStringValue(dgvDataGridBOM, A, "bloqueado")

                                OrdemServico.Espessura = GetCellStringValue(dgvDataGridBOM, A, "Espessura", False)
                                OrdemServico.NumeroDobras = GetCellStringValue(dgvDataGridBOM, A, "NumeroDobras", False)
                                OrdemServico.Altura = GetCellStringValue(dgvDataGridBOM, A, "Altura", False)
                                OrdemServico.Largura = GetCellStringValue(dgvDataGridBOM, A, "Largura", False)
                                OrdemServico.Comprimentocaixadelimitadora = GetCellNumberValue(dgvDataGridBOM, A, "Comprimentocaixalimitadora")
                                OrdemServico.Larguracaixadelimitadora = GetCellNumberValue(dgvDataGridBOM, A, "Larguracaixadelimitadora")
                                OrdemServico.Espessuracaixadelimitadora = GetCellNumberValue(dgvDataGridBOM, A, "Espessuracaixadelimitadora")

                                Try
                                    OrdemServico.AreaPintura = Convert.ToDecimal(Replace(dgvDataGridBOM.Rows(A).Cells("AreaPintura").Value.ToString, ".", ","))
                                Catch
                                    OrdemServico.AreaPintura = 0
                                End Try

                                Try
                                    OrdemServico.qtde = Convert.ToDecimal(Replace(dgvDataGridBOM.Rows(A).Cells("qtde").Value.ToString, ".", ","))
                                Catch
                                    OrdemServico.qtde = 0
                                End Try

                                Try
                                    OrdemServico.Peso = Convert.ToDecimal(Replace(dgvDataGridBOM.Rows(A).Cells("Peso").Value.ToString, ".", ","))
                                Catch
                                    OrdemServico.Peso = 0
                                End Try

                                ' Obtém o valor da célula "Unidade" de forma segura
                                Dim unidadeCell As Object = Nothing
                                If dgvDataGridBOM.Rows(A).Cells("Unidade").Value IsNot Nothing Then
                                    unidadeCell = dgvDataGridBOM.Rows(A).Cells("Unidade").Value
                                End If

                                OrdemServico.Unidade = If(unidadeCell IsNot Nothing, unidadeCell.ToString().Trim(), "")

                                ' Se estiver vazio, define o padrão baseado no tipo de arquivo
                                If String.IsNullOrEmpty(OrdemServico.Unidade) Then
                                    Dim endereco As String = GetCellStringValue(dgvDataGridBOM, A, "EnderecoArquivo", False).Trim()

                                    If Not String.IsNullOrEmpty(endereco) AndAlso endereco.ToUpper().Contains(".SLDASM") Then
                                        OrdemServico.Unidade = "CONJ"
                                        OrdemServico.UnidadeSW = "CONJ"
                                    Else
                                        OrdemServico.Unidade = "PC"
                                        OrdemServico.UnidadeSW = "PC"
                                    End If
                                End If

                                'contas''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                Try

                                    OrdemServico.PesoUnitario = OrdemServico.Peso * OrdemServico.qtde

                                    OrdemServico.AreaPinturaUnitario = OrdemServico.AreaPintura * OrdemServico.qtde


                                    OrdemServico.AreaPintura = OrdemServico.AreaPintura * OrdemServico.qtde * OrdemServico.Fator 'Area pintura total

                                    OrdemServico.Peso = OrdemServico.Peso * OrdemServico.qtde * OrdemServico.Fator 'peso Total

                                    OrdemServico.QtdeTotal = OrdemServico.qtde * OrdemServico.Fator

                                Catch ex As Exception

                                End Try

                                Dim vERIFICAprocesso As String

                                vERIFICAprocesso = Replace((OrdemServico.txtCorte) & (OrdemServico.txtDobra) & (OrdemServico.txtSolda) & (OrdemServico.txtPintura) & (OrdemServico.txtMontagem), " ", "")

                                If vERIFICAprocesso = "0" Or vERIFICAprocesso = "" Then

                                    DadosArquivoCorrente.Montagem = "1"

                                End If

                                Dim DATAATUAL As String = Date.Now

                                If My.Settings.TipoConexao = "MYSQL" Then

                                    '  Dim transaction As MySqlTransaction = Nothing

                                    Try

                                        ' Inicia a transação
                                        '  transaction = myconect.BeginTransaction()

                                        Using command As New MySqlCommand(query, myconect)

                                            ' Adicionando os parâmetros
                                            command.Parameters.AddWithValue("@IdOrdemServico", OrdemServico.IdOrdemServico)
                                            command.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
                                            command.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                                            command.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
                                            command.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                                            command.Parameters.AddWithValue("@ESTATUS_OrdemServico", OrdemServico.Estatus)

                                            If OrdemServico.IdMaterial IsNot Nothing Then
                                                command.Parameters.AddWithValue("@IdMaterial", OrdemServico.IdMaterial)
                                            Else
                                                command.Parameters.AddWithValue("@IdMaterial", DBNull.Value)
                                            End If

                                            ' command.Parameters.AddWithValue("@IdMaterial", OrdemServico.IdMaterial)
                                            command.Parameters.AddWithValue("@DescResumo", OrdemServico.DescResumo)
                                            command.Parameters.AddWithValue("@DescDetal", OrdemServico.DescDetal)
                                            command.Parameters.AddWithValue("@Autor", OrdemServico.Autor)
                                            command.Parameters.AddWithValue("@Palavrachave", OrdemServico.Palavrachave)
                                            command.Parameters.AddWithValue("@Notas", OrdemServico.Notas)
                                            command.Parameters.AddWithValue("@Espessura", OrdemServico.Espessura)
                                            command.Parameters.AddWithValue("@AreaPintura", OrdemServico.AreaPintura)

                                            If OrdemServico.NumeroDobras.ToString = Nothing Then

                                                OrdemServico.NumeroDobras = 0

                                            End If

                                            command.Parameters.AddWithValue("@NumeroDobras", OrdemServico.NumeroDobras)
                                            command.Parameters.AddWithValue("@Peso", OrdemServico.Peso.ToString.Replace(",", "."))
                                            command.Parameters.AddWithValue("@Unidade", OrdemServico.Unidade)
                                            command.Parameters.AddWithValue("@UnidadeSW", OrdemServico.UnidadeSW)
                                            command.Parameters.AddWithValue("@Altura", OrdemServico.Altura)
                                            command.Parameters.AddWithValue("@Largura", OrdemServico.Largura)
                                            command.Parameters.AddWithValue("@CodMatFabricante", OrdemServico.CodMatFabricante)
                                            command.Parameters.AddWithValue("@DtCad", DATAATUAL)
                                            command.Parameters.AddWithValue("@UsuarioCriacao", Usuario.NomeCompleto)
                                            command.Parameters.AddWithValue("@UsuarioAlteracao", "")
                                            command.Parameters.AddWithValue("@DtAlteracao", "")
                                            command.Parameters.AddWithValue("@EnderecoArquivo", OrdemServico.EnderecoArquivo)
                                            command.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSW)
                                            command.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal.ToString.Replace(",", "."))
                                            'command.Parameters.AddWithValue("@QtdeProduzida", 0)
                                            ' command.Parameters.AddWithValue("@QtdeFaltante", 0)
                                            command.Parameters.AddWithValue("@CriadoPor", Usuario.NomeCompleto.ToString)
                                            command.Parameters.AddWithValue("@DataCriacao", DATAATUAL)
                                            command.Parameters.AddWithValue("@Estatus", "A")
                                            command.Parameters.AddWithValue("@Acabamento", OrdemServico.txtAcabamento)
                                            command.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                                            command.Parameters.AddWithValue("@fator", OrdemServico.Fator.ToString.Replace(",", "."))
                                            command.Parameters.AddWithValue("@qtde", OrdemServico.qtde.ToString.Replace(",", "."))
                                            command.Parameters.AddWithValue("@txtSoldagem", OrdemServico.txtSoldagem)
                                            command.Parameters.AddWithValue("@txtTipoDesenho", OrdemServico.txtTipoDesenho)
                                            command.Parameters.AddWithValue("@txtCorte", OrdemServico.txtCorte)
                                            command.Parameters.AddWithValue("@txtDobra", OrdemServico.txtDobra)
                                            command.Parameters.AddWithValue("@txtSolda", OrdemServico.txtSolda)
                                            command.Parameters.AddWithValue("@txtPintura", OrdemServico.txtPintura)
                                            command.Parameters.AddWithValue("@txtMontagem", OrdemServico.txtMontagem)
                                            command.Parameters.AddWithValue("@Comprimentocaixadelimitadora", OrdemServico.Comprimentocaixadelimitadora)
                                            command.Parameters.AddWithValue("@Larguracaixadelimitadora", OrdemServico.Larguracaixadelimitadora)
                                            command.Parameters.AddWithValue("@Espessuracaixadelimitadora", OrdemServico.Espessuracaixadelimitadora)
                                            command.Parameters.AddWithValue("@AreaPinturaUnitario", OrdemServico.AreaPinturaUnitario.ToString.Replace(",", "."))
                                            command.Parameters.AddWithValue("@PesoUnitario", OrdemServico.PesoUnitario.ToString.Replace(",", "."))
                                            command.Parameters.AddWithValue("@txtItemEstoque", OrdemServico.txtItemEstoque)
                                            command.Parameters.AddWithValue("@sttxtCorte", "")
                                            command.Parameters.AddWithValue("@sttxtDobra", "")
                                            command.Parameters.AddWithValue("@sttxtSolda", "")
                                            command.Parameters.AddWithValue("@sttxtPintura", "")
                                            command.Parameters.AddWithValue("@sttxtMontagem", "")
                                            command.Parameters.AddWithValue("@DescEmpresa", OrdemServico.DescEmpresa)
                                            command.Parameters.AddWithValue("@idempresa", OrdemServico.idempresa)
                                            command.Parameters.AddWithValue("@desctag", OrdemServico.DescTag)

                                            If OrdemServico.DataPrevisao IsNot Nothing Then

                                                command.Parameters.AddWithValue("@DataPrevisao", OrdemServico.DataPrevisao)

                                            Else

                                                command.Parameters.AddWithValue("@DataPrevisao", DBNull.Value)

                                            End If

                                            command.ExecuteNonQuery()

                                            '  SalvarProcessoOrdemServicoItem(OrdemServico.CodMatFabricante)
                                            ' Confirma a transação
                                            '  transaction.Commit()

                                        End Using
                                    Catch ex As Exception
                                        ' Se algo der errado, cancela todas as alterações
                                        '  If transaction IsNot Nothing Then
                                        ' transaction.Rollback()
                                        ' End If
                                        MsgBox("Erro ao ler o arquivo: " & OrdemServico.EnderecoArquivo & " linha: " & A)
                                        Continue For

                                    End Try

                                ElseIf My.Settings.TipoConexao = "SQL" Then

                                    Using commandSQL As New SqlCommand(query, myconectSQL)

                                        commandSQL.Parameters.AddWithValue("@IdOrdemServico", OrdemServico.IdOrdemServico)
                                        commandSQL.Parameters.AddWithValue("@IdProjeto", OrdemServico.idProjeto)
                                        commandSQL.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                                        commandSQL.Parameters.AddWithValue("@IdTag", OrdemServico.idTag)
                                        commandSQL.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                                        commandSQL.Parameters.AddWithValue("@Estatus_OrdemServico", OrdemServico.Estatus)

                                        If OrdemServico.IdMaterial IsNot Nothing Then
                                            commandSQL.Parameters.AddWithValue("@IdMaterial", OrdemServico.IdMaterial)
                                        Else
                                            commandSQL.Parameters.AddWithValue("@IdMaterial", DBNull.Value)
                                        End If

                                        ' commandSQL.Parameters.AddWithValue("@IdMaterial", OrdemServico.IdMaterial)
                                        commandSQL.Parameters.AddWithValue("@DescResumo", OrdemServico.DescResumo)
                                        commandSQL.Parameters.AddWithValue("@DescDetal", OrdemServico.DescDetal)
                                        commandSQL.Parameters.AddWithValue("@Autor", OrdemServico.Autor)
                                        commandSQL.Parameters.AddWithValue("@PalavraChave", OrdemServico.Palavrachave)
                                        commandSQL.Parameters.AddWithValue("@Notas", OrdemServico.Notas)
                                        commandSQL.Parameters.AddWithValue("@Espessura", OrdemServico.Espessura)
                                        commandSQL.Parameters.AddWithValue("@AreaPintura", OrdemServico.AreaPintura)
                                        commandSQL.Parameters.AddWithValue("@NumeroDobras", OrdemServico.NumeroDobras)
                                        commandSQL.Parameters.AddWithValue("@Peso", OrdemServico.Peso.ToString.Replace(",", "."))
                                        commandSQL.Parameters.AddWithValue("@Unidade", OrdemServico.Unidade)
                                        commandSQL.Parameters.AddWithValue("@UnidadeSW", OrdemServico.UnidadeSW)
                                        ' commandSQL.Parameters.AddWithValue("@ValorSW", OrdemServico.ValorSW)
                                        commandSQL.Parameters.AddWithValue("@Altura", OrdemServico.Altura)
                                        commandSQL.Parameters.AddWithValue("@Largura", OrdemServico.Largura)
                                        commandSQL.Parameters.AddWithValue("@CodMatFabricante", OrdemServico.CodMatFabricante)
                                        commandSQL.Parameters.AddWithValue("@DtCad", DATAATUAL)
                                        commandSQL.Parameters.AddWithValue("@UsuarioCriacao", Usuario.NomeCompleto)
                                        commandSQL.Parameters.AddWithValue("@UsuarioAlteracao", "")
                                        commandSQL.Parameters.AddWithValue("@DtAlteracao", "")
                                        commandSQL.Parameters.AddWithValue("@EnderecoArquivo", OrdemServico.EnderecoArquivo)
                                        commandSQL.Parameters.AddWithValue("@MaterialSW", OrdemServico.MaterialSW)
                                        commandSQL.Parameters.AddWithValue("@QtdeTotal", OrdemServico.QtdeTotal.ToString.Replace(",", "."))
                                        ' commandSQL.Parameters.AddWithValue("@QtdeProduzida", "")
                                        ' commandSQL.Parameters.AddWithValue("@QtdeFaltante", "")
                                        commandSQL.Parameters.AddWithValue("@CriadoPor", Usuario.NomeCompleto.ToString)
                                        commandSQL.Parameters.AddWithValue("@DataCriacao", DATAATUAL)
                                        commandSQL.Parameters.AddWithValue("@Estatus", "A")
                                        commandSQL.Parameters.AddWithValue("@Acabamento", OrdemServico.txtAcabamento)
                                        commandSQL.Parameters.AddWithValue("@D_E_L_E_T_E", "")
                                        commandSQL.Parameters.AddWithValue("@Fator", OrdemServico.Fator.ToString.Replace(",", "."))
                                        commandSQL.Parameters.AddWithValue("@Qtde", OrdemServico.qtde.ToString.Replace(",", "."))
                                        commandSQL.Parameters.AddWithValue("@txtSoldagem", OrdemServico.txtSoldagem)
                                        commandSQL.Parameters.AddWithValue("@txtTipoDesenho", OrdemServico.txtTipoDesenho)
                                        commandSQL.Parameters.AddWithValue("@txtCorte", OrdemServico.txtCorte)
                                        commandSQL.Parameters.AddWithValue("@txtDobra", OrdemServico.txtDobra)
                                        commandSQL.Parameters.AddWithValue("@txtSolda", OrdemServico.txtSolda)
                                        commandSQL.Parameters.AddWithValue("@txtPintura", OrdemServico.txtPintura)
                                        commandSQL.Parameters.AddWithValue("@txtMontagem", OrdemServico.txtMontagem)
                                        'commandSQL.Parameters.AddWithValue("@tttxtCorte", OrdemServico.tttxtCorte)
                                        'commandSQL.Parameters.AddWithValue("@tttxtDobra", OrdemServico.tttxtDobra)
                                        'commandSQL.Parameters.AddWithValue("@tttxtSolda", OrdemServico.tttxtSolda)
                                        'commandSQL.Parameters.AddWithValue("@tttxtPintura", OrdemServico.tttxtPintura)
                                        'commandSQL.Parameters.AddWithValue("@tttxtMontagem", OrdemServico.tttxtMontagem)
                                        commandSQL.Parameters.AddWithValue("@ComprimentoCaixaDelimitadora", OrdemServico.Comprimentocaixadelimitadora)
                                        commandSQL.Parameters.AddWithValue("@LarguraCaixaDelimitadora", OrdemServico.Larguracaixadelimitadora)
                                        commandSQL.Parameters.AddWithValue("@EspessuraCaixaDelimitadora", OrdemServico.Espessuracaixadelimitadora)
                                        commandSQL.Parameters.AddWithValue("@AreaPinturaUnitario", OrdemServico.AreaPinturaUnitario.ToString.Replace(",", "."))
                                        commandSQL.Parameters.AddWithValue("@PesoUnitario", OrdemServico.PesoUnitario.ToString.Replace(",", "."))
                                        commandSQL.Parameters.AddWithValue("@txtItemEstoque", OrdemServico.txtItemEstoque)
                                        If OrdemServico.DataPrevisao IsNot Nothing Then
                                            commandSQL.Parameters.AddWithValue("@DataPrevisao", OrdemServico.DataPrevisao)
                                        Else
                                            commandSQL.Parameters.AddWithValue("@DataPrevisao", DBNull.Value)
                                        End If

                                        commandSQL.Parameters.AddWithValue("@sttxtCorte", "")
                                        commandSQL.Parameters.AddWithValue("@sttxtDobra", "")
                                        commandSQL.Parameters.AddWithValue("@sttxtSolda", "")
                                        commandSQL.Parameters.AddWithValue("@sttxtPintura", "")
                                        commandSQL.Parameters.AddWithValue("@sttxtMontagem", "")

                                        'commandSQL.Parameters.AddWithValue("@sttxtCorte", "")
                                        'commandSQL.Parameters.AddWithValue("@CorteTotalExecutado", "")
                                        'commandSQL.Parameters.AddWithValue("@CorteTotalExecutar", "")
                                        'commandSQL.Parameters.AddWithValue("@sttxtDobra", "")
                                        'commandSQL.Parameters.AddWithValue("@DobraTotalExecutado", "")
                                        'commandSQL.Parameters.AddWithValue("@DobraTotalExecutar", "")
                                        'commandSQL.Parameters.AddWithValue("@sttxtSolda", "")
                                        'commandSQL.Parameters.AddWithValue("@SoldaTotalExecutado", "")
                                        'commandSQL.Parameters.AddWithValue("@SoldaTotalExecutar", "")
                                        'commandSQL.Parameters.AddWithValue("@sttxtPintura", "")
                                        'commandSQL.Parameters.AddWithValue("@PinturaTotalExecutado", "")
                                        'commandSQL.Parameters.AddWithValue("@PinturaTotalExecutar", "")
                                        'commandSQL.Parameters.AddWithValue("@sttxtMontagem", "")
                                        'commandSQL.Parameters.AddWithValue("@MontagemTotalExecutado", "")
                                        'commandSQL.Parameters.AddWithValue("@MontagemTotalExecutar", "")
                                        'commandSQL.Parameters.AddWithValue("@OrdemServicoItemFinalizado", "")
                                        'commandSQL.Parameters.AddWithValue("@IdPlanodecorte", "")

                                        Try
                                            commandSQL.ExecuteNonQuery()
                                        Catch ex As SqlException
                                            '  InputBox("", "", ex.Message)
                                        End Try

                                        ' commandSQL.ExecuteNonQuery()

                                        SalvarProcessoOrdemServicoItem(OrdemServico.CodMatFabricante)

                                    End Using

                                End If

                            End If

                            ProgressBarListaSW.Value = A
                        Catch ex As Exception

                            MsgBox(ex.Message & " ERRO ao ler o arquivo: " & OrdemServico.EnderecoArquivo, MsgBoxStyle.Critical, "Atenção")

                            Continue For

                        End Try

                        ProgressBarListaSW.Value = A

                    Next A

                    'Salva o total de itens na tabela Ordem de servico

                End If

                cl_BancoDados.FecharBanco()

                MessageBox.Show("Operação finalizada com sucesso, os itens foram inseridos na OS!")

                ProgressBarListaSW.Value = 0

                TimerDGVListaMaterialSW.Enabled = True
            Else

                MsgBox("Operação cancelada, os itens não serão inseridos na OS!", vbCritical, "Atenção")

            End If

            ' Retornar o cursor ao normal
            Cursor.Current = Cursors.Default
        Catch ex As Exception
        Finally

        End Try

    End Sub

    Private Function GetCellStringValue(dgv As DataGridView, rowIndex As Integer, columnName As String, Optional toUpper As Boolean = True) As String
        Try
            Dim value = dgv.Rows(rowIndex).Cells(columnName).Value?.ToString()
            If String.IsNullOrWhiteSpace(value) Then Return ""
            Return If(toUpper, value.ToUpper(), value)
        Catch
            Return ""
        End Try
    End Function


    'Private Function GetCellStringValue(dgv As DataGridView, rowIndex As Integer, columnName As String, Optional toUpper As Boolean = True) As String
    '    Try
    '        Dim value = dgv.Rows(rowIndex).Cells(columnName).Value?.ToString()
    '        If String.IsNullOrWhiteSpace(value) Then Return ""

    '        ' Remove caracteres que podem causar problemas em SQL
    '        ' Aspas, ponto e vírgula, comentário, barra invertida, %, #
    '        value = Regex.Replace(value, "['"";--/\*%\#=]", "")

    '        ' Converte para maiúscula se necessário
    '        Return If(toUpper, value.ToUpper(), value)
    '    Catch
    '        Return ""
    '    End Try
    'End Function


    Private Function GetCellNumberValue(dgv As DataGridView, rowIndex As Integer, columnName As String) As String
        Try
            Dim value = dgv.Rows(rowIndex).Cells(columnName).Value?.ToString()
            If String.IsNullOrWhiteSpace(value) Then Return ""
            Return Replace(value, ",", ".")
        Catch
            Return ""
        End Try
    End Function

    Private Function SafeGetString(cellValue As Object) As String
        Try
            Return If(cellValue IsNot Nothing, cellValue.ToString().ToUpper(), "")
        Catch
            Return ""
        End Try
    End Function

    Private Function SafeGetDouble(cellValue As Object) As Double
        Try
            Return Convert.ToDouble(Replace(cellValue.ToString(), ",", "."))
        Catch
            Return 0
        End Try
    End Function

    Private Sub TsbAtualizarBOM_Click_1(sender As Object, e As EventArgs) Handles TsbAtualizarBOM.Click

        Try

            AtualizarIcones()
        Catch ex As Exception
        Finally

        End Try

    End Sub

    Private Sub txtNomeArquivo_Click(sender As Object, e As EventArgs) Handles txtNomeArquivo.Click

        Try

            txtNomeArquivo.SelectAll()

            Clipboard.SetText(txtNomeArquivo.Text) ' Copia o texto para a área de transferência
        Catch ex As Exception
        Finally

        End Try

    End Sub

    ' Campo para evitar reentrância (quando mudamos o Checked por código)
    Private _toggling As Boolean = False

    Private Sub chkAtualizacao_CheckedChanged(sender As Object, e As EventArgs) Handles chkAtualizacao.CheckedChanged
        If _toggling Then Exit Sub

        ' Validação básica
        If swModel Is Nothing Then
            _toggling = True
            chkAtualizacao.Checked = False
            _toggling = False
            MessageBox.Show("Nenhum documento do SolidWorks está aberto.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Novo valor pretendido (S/N) com base no estado do CheckBox
        Dim novoValor As String = If(chkAtualizacao.Checked, "S", "N")
        Dim valorAnterior As String = If(String.IsNullOrEmpty(DadosArquivoCorrente.Bloqueado), "N", DadosArquivoCorrente.Bloqueado)

        ' Travar UI durante operação
        Cursor = Cursors.WaitCursor
        chkAtualizacao.Enabled = False

        Try
            ' 1) Atualiza o modelo em memória
            DadosArquivoCorrente.Bloqueado = novoValor
            AplicarBloqueioUI(novoValor) ' já atualiza texto/ícone

            ' 2) Propriedade no arquivo do SolidWorks
            DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "Bloqueado", novoValor, novoValor)

            ' 3) Banco de dados (ajuste se sua rotina AlteracaoEspecifica aceitar Date diretamente ou prefira padrão ISO)
            Dim dataIso As String = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
            cl_BancoDados.AlteracaoEspecifica("material", "Bloqueado", novoValor, "CodMatFabricante", DadosArquivoCorrente.NomeArquivoSemExtensao)
            cl_BancoDados.AlteracaoEspecifica("material", "UsuarioBloqueado", Usuario.NomeCompleto, "CodMatFabricante", DadosArquivoCorrente.NomeArquivoSemExtensao)
            cl_BancoDados.AlteracaoEspecifica("material", "DataBloqueado", dataIso, "CodMatFabricante", DadosArquivoCorrente.NomeArquivoSemExtensao)

            ' 4) Salva o arquivo sem prompt
            swModel.SaveSilent()
        Catch ex As Exception
            ' Falhou: volta UI/estado para o anterior
            _toggling = True
            chkAtualizacao.Checked = (valorAnterior = "S")
            _toggling = False
            AplicarBloqueioUI(valorAnterior)
            DadosArquivoCorrente.Bloqueado = valorAnterior

            MessageBox.Show("Não foi possível alterar o estado de bloqueio." & Environment.NewLine &
                        ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            chkAtualizacao.Enabled = True
            Cursor = Cursors.Default
        End Try
    End Sub

    ' Centraliza atualização visual do CheckBox
    Private Sub AplicarBloqueioUI(flag As String)
        If flag = "S" Then
            chkAtualizacao.Image = My.Resources.bloqueado
            chkAtualizacao.Text = "Bloqueado"
            chkAtualizacao.ForeColor = Color.Firebrick
        Else
            chkAtualizacao.Image = My.Resources.desbloqueado
            chkAtualizacao.Text = "Desbloqueado"
            chkAtualizacao.ForeColor = Color.ForestGreen
        End If
        ' dica de ferramenta (opcional)
        ' chkAtualizacao.ToolTipTe = If(flag = "S", "O arquivo está bloqueado para alterações.", "O arquivo está desbloqueado.")
    End Sub

    Private Sub AtualizarDesenhoPeloDiretorioToolStripMenuItem1_Click_2(sender As Object, e As EventArgs) Handles AtualizarDesenhoPeloDiretorioToolStripMenuItem1.Click
        ' Mensagem mais clara para o usuário
        Dim msg As String =
        "Selecione a ação desejada:" & vbCrLf & vbCrLf &
        "✔ SIM       → Atualiza a versão do SolidWorks e SALVA no banco de dados" & vbCrLf &
        "✔ NÃO       → Somente atualiza a versão do SolidWorks (NÃO salva no banco)" & vbCrLf &
        "✔ CANCELAR  → Fecha sem realizar nenhuma alteração"

        Dim result As DialogResult = MessageBox.Show(
        msg,
        "Atualizar Versão do SolidWorks",
        MessageBoxButtons.YesNoCancel,
        MessageBoxIcon.Question,
        MessageBoxDefaultButton.Button1
    )

        Dim salvarNoBanco As Boolean

        Select Case result
            Case DialogResult.Yes
                ' Opção 1 - Atualiza e salva no banco
                salvarNoBanco = True
            Case DialogResult.No
                ' Opção 2 - Atualiza sem salvar no banco
                salvarNoBanco = False
            Case Else
                ' Cancelado
                MessageBox.Show("Operação cancelada.", "Cancelar", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
        End Select

        Try
            ' Chama sua rotina principal (ela já pede a pasta e processa recursivo)
            ProcessAllFiles(salvarNoBanco)
        Catch ex As Exception
            MessageBox.Show("Falha durante a atualização: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub txtPalavraChave_DoubleClick(sender As Object, e As EventArgs) Handles txtPalavraChave.DoubleClick

        Me.txtPalavraChave.Text = DadosArquivoCorrente.EnderecoArquivo.ToUpper

    End Sub

    Private Sub ToolStripButton9_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton9.Click

        AtualziarDadosSinco()

    End Sub

    Private Sub UsarFormatoA3ToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles UsarFormatoA3ToolStripMenuItem.Click

        DadosArquivoCorrente.TrocarFormatoA3(swModel)

    End Sub

    Private Sub TrocarFormatoA3PeloDiretorioSelecionadoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TrocarFormatoA3PeloDiretorioSelecionadoToolStripMenuItem.Click

        Dim folderPath As String = ""

        ' Seleciona a pasta usando OpenFileDialog
        Using openFileDialog As New OpenFileDialog
            openFileDialog.CheckFileExists = False
            openFileDialog.CheckPathExists = True
            openFileDialog.ValidateNames = False
            openFileDialog.FileName = "Selecione uma pasta"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                ' Remove o nome fictício para obter o caminho correto
                folderPath = IO.Path.GetDirectoryName(openFileDialog.FileName)

                If Directory.Exists(folderPath) Then
                    ' Executa a ação passando o diretório selecionado
                    DadosArquivoCorrente.TrocarFormatoA3EmTodosOsDesenhos(folderPath, Nothing)
                Else
                    MessageBox.Show("O diretório selecionado não é válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("Nenhum diretório foi selecionado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        End Using

    End Sub

    Private Sub tsbTrocarFormato_Click(sender As Object, e As EventArgs) Handles tsbTrocarFormato.Click

        If DadosArquivoCorrente.EnderecoArquivo.ToString = "" Then
            MsgBox("Nenhum arquivo selecionado!", vbCritical, "Atenção")
        Else

            Dim RowCount As Integer = dgvDataGridBOM.Rows.Count
            If RowCount > 0 Then

                For i As Integer = 0 To RowCount - 1

                    Dim row As DataGridViewRow = dgvDataGridBOM.Rows(i)

                    Dim enderecoOriginal As String = row.Cells("EnderecoArquivo").Value?.ToString()
                    DadosArquivoCorrente.EnderecoArquivo = Path.GetFullPath(enderecoOriginal)

                    If File.Exists(DadosArquivoCorrente.EnderecoArquivo) Then
                        OpenDocumentAndWait(DadosArquivoCorrente.EnderecoArquivo, False, swModel)
                    End If

                    Dim enderecoArquivoUpper As String = DadosArquivoCorrente.EnderecoArquivo.ToUpper()
                    Dim arquivoSLDDRW As String = enderecoArquivoUpper

                    If enderecoArquivoUpper.EndsWith(".SLDPRT") OrElse enderecoArquivoUpper.EndsWith(".SLDASM") Then
                        arquivoSLDDRW = arquivoSLDDRW.Replace(".SLDPRT", ".SLDDRW").Replace(".SLDASM", ".SLDDRW")
                    End If

                    If File.Exists(arquivoSLDDRW) Then
                        OpenDocumentAndWait(arquivoSLDDRW, True, swModel)
                        DadosArquivoCorrente.TrocarFormatoA3(swModel)

                        If chkConverterPDF.Checked = True Then
                            If DadosArquivoCorrente.ExportToPDF(swModel, arquivoSLDDRW, False) Then
                                row.Cells("dgvIconePDF").Value = My.Resources.ficheiro_pdf
                            Else
                                row.Cells("dgvIconePDF").Value = My.Resources.Sem_Incone
                            End If

                        End If

                        swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                        swapp.CloseDoc(arquivoSLDDRW)
                        row.Cells("dgvIconePDF").Value = My.Resources.verificado1
                    Else

                        row.Cells("dgvIconePDF").Value = My.Resources.atencao

                    End If

                Next

            End If

        End If

        If dgvDataGridBOM.Rows.Count > 0 Then

            For a As Integer = 0 To dgvDataGridBOM.Rows.Count - 1

                Try

                    If a <> 1 Then 'Não fechao o primeiro arquivo do datagrid view da BOM

                        If dgvDataGridBOM.Rows(a).Cells("EnderecoArquivo").ToString().ToLower().EndsWith(".sldprt") OrElse
                  dgvDataGridBOM.Rows(a).Cells("EnderecoArquivo").ToString().ToLower().EndsWith(".sldasm") Then

                            swapp.CloseDoc(dgvDataGridBOM.Rows(a).Cells("EnderecoArquivo").ToString())

                        End If

                    End If
                Catch ex As Exception

                    Continue For

                End Try

                ProgressBarListaSW.Value = a

            Next

            ProgressBarListaSW.Value = 0

        End If

    End Sub

    Private Sub Painel_Leitura_Dados_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        If vemdalista = False Then

            ReorganizarCheckboxes()

            If Me.Width > 662 Then

                FlowLayoutPanel1.Height = chkAtualizacao.Height * 2

            ElseIf Me.Width > 332 Then

                FlowLayoutPanel1.Height = chkAtualizacao.Height * 2 * 2

            ElseIf Me.Width <= 181 Then

                FlowLayoutPanel1.Height = chkAtualizacao.Height * 3 * 2

            End If
        End If

    End Sub

    Private Sub txtAuthor_Validated(sender As Object, e As EventArgs) Handles txtAuthor.Validated

        ' Verifique se o swModel foi aberto com sucesso
        If Not swModel Is Nothing Then

            DadosArquivoCorrente.Author = Me.txtAuthor.Text

            swModel.SummaryInfo(swSummInfoField_e.swSumInfoAuthor) = DadosArquivoCorrente.Author

            'cl_BancoDados.AlteracaoEspecifica("material", "Autor", Me.txtAuthor.Text, "CodMatFabricante", DadosArquivoCorrente.NomeArquivoSemExtensao)
            'edson23/04/2025 swModel.SaveSilent()
        End If

    End Sub

    Private Sub txtVerificado_Validated(sender As Object, e As EventArgs) Handles txtVerificado.Validated

        ' Verifique se o swModel foi aberto com sucesso
        If Not swModel Is Nothing Then

            DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "Verificado", txtVerificado.Text, txtVerificado.Text)
            DadosArquivoCorrente.Verificado = txtVerificado.Text
            'edson23/04/2025 swModel.SaveSilent()

        End If

    End Sub

    Private Sub txtAprovado_Validated(sender As Object, e As EventArgs) Handles txtAprovado.Validated
        ' Verifique se o swModel foi aberto com sucesso
        If Not swModel Is Nothing Then

            DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "Aprovado", txtAprovado.Text, txtAprovado.Text)
            DadosArquivoCorrente.Aprovado = txtAprovado.Text
            'edson23/04/2025 swModel.SaveSilent()
        End If

    End Sub

    Private Sub txtPalavraChave_Validated(sender As Object, e As EventArgs) Handles txtPalavraChave.Validated

        ' Verifique se o swModel foi aberto com sucesso
        If Not swModel Is Nothing Then

            DadosArquivoCorrente.PalavraChave = Me.txtPalavraChave.Text

            swModel.SummaryInfo(swSummInfoField_e.swSumInfoKeywords) = DadosArquivoCorrente.PalavraChave 'Me.txtPalavraChave.Text

            'edson23/04/2025 swModel.SaveSilent()

        End If

    End Sub

    Private Sub txtComentarios_Validated(sender As Object, e As EventArgs) Handles txtComentarios.Validated
        ' Verifique se o swModel foi aberto com sucesso
        If Not swModel Is Nothing Then

            DadosArquivoCorrente.Comentarios = txtComentarios.Text

            swModel.SummaryInfo(swSummInfoField_e.swSumInfoComment) = DadosArquivoCorrente.Comentarios 'Me.txtAssuntoSubiTitulo.Text
            'edson23/04/2025 swModel.SaveSilent()

        End If

    End Sub

    Private Sub txtTitulo_Validated(sender As Object, e As EventArgs) Handles txtTitulo.Validated

        ' Verifique se o swModel foi aberto com sucesso
        If Not swModel Is Nothing Then

            DadosArquivoCorrente.Titulo = Me.txtTitulo.Text

            swModel.SummaryInfo(swSummInfoField_e.swSumInfoTitle) = DadosArquivoCorrente.Titulo
            'edson23/04/2025 swModel.SaveSilent()
        End If

    End Sub

    Private Sub txtAssuntoSubiTitulo_Validated(sender As Object, e As EventArgs) Handles txtAssuntoSubiTitulo.Validated

        ' Verifique se o swModel foi aberto com sucesso
        If Not swModel Is Nothing Then

            DadosArquivoCorrente.AssuntoSubiTitulo = txtAssuntoSubiTitulo.Text

            swModel.SummaryInfo(swSummInfoField_e.swSumInfoSubject) = DadosArquivoCorrente.AssuntoSubiTitulo 'Me.txtAssuntoSubiTitulo.Text
            'edson23/04/2025 swModel.SaveSilent()

        End If

    End Sub

    Private Sub TimerMedidor_Tick(sender As Object, e As EventArgs) Handles TimerMedidordgvMedidorNumeroOS.Tick

        dgvMedidorNumeroOS.DataSource = cl_BancoDados.CarregarDados("SELECT * from kpiviewnumeroosliberadasporprojetista WHERE Projetista = '" & Usuario.NomeCompleto & "'")

        dgvTimerMedidordgvMedidorPesoProducao.DataSource = cl_BancoDados.CarregarDados("SELECT
    CriadoPor,
    MesAno_Liberacao,
    MaterialSW,
    txtTipoDesenho,
    ROUND(Total_Peso, 2) AS Total_Peso
FROM
    kpiviewpesoproducaoporprojetistamesmaterial WHERE CriadoPor = '" & Usuario.NomeCompleto & "'")

        dgvTimerMedidordgvMedidorProjetistaOSMes.DataSource = cl_BancoDados.CarregarDados("SELECT CriadoPor,
MesAno_Liberacao,
Total_IdsOrdemServico,
ROUND(Total_Peso, 2) AS Total_Peso
FROM
kpiviewalimentacaoproducaoprojetista WHERE CriadoPor = '" & Usuario.NomeCompleto & "'")

        TimerMedidordgvMedidorNumeroOS.Enabled = False

    End Sub

    Private Sub dgvMedidorNumeroOS_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvMedidorNumeroOS.DataBindingComplete

        If dgvMedidorNumeroOS IsNot Nothing AndAlso dgvMedidorNumeroOS.Rows IsNot Nothing AndAlso dgvMedidorNumeroOS.Rows.Count > 0 Then

            cl_BancoDados.FormatarDataGridView(dgvMedidorNumeroOS, "SIM")

        End If

    End Sub

    Private Sub dgvTimerMedidordgvMedidorPesoProducao_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvTimerMedidordgvMedidorPesoProducao.DataBindingComplete

        If dgvMedidorNumeroOS IsNot Nothing AndAlso dgvMedidorNumeroOS.Rows IsNot Nothing AndAlso dgvMedidorNumeroOS.Rows.Count > 0 Then

            cl_BancoDados.FormatarDataGridView(dgvMedidorNumeroOS, "SIM")
        End If

    End Sub

    Private Sub TimerResetBotao_Tick(sender As Object, e As EventArgs) Handles TimerResetBotao.Tick

        TimerResetBotao.Stop() ' Para o timer
        btnPendencias.BackColor = CorOriginal
        btnPendencias.Text = TextoOriginal
        btnPendencias.Image = ImagemOriginal

    End Sub

    Private Async Sub FaçaUmaAnalizeTecnicaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FaçaUmaAnalizeTecnicaToolStripMenuItem.Click

        frmAI.txtConversa.Font = New Font("Segoe UI Emoji", 10)

        Dim prompt As String = AjudaAI.DataGridViewToJson(dgvDataGridBOM)

        Try

            Dim resposta As String = Await AjudaAI.ObterRespostaGPT(prompt, Agente_AnaliseBom)

            Invoke(Sub()
                       frmAI.txtConversa.AppendText("GPT: " & resposta & Environment.NewLine & Environment.NewLine & Environment.NewLine)

                   End Sub)
        Catch ex As Exception
            Invoke(Sub()
                       frmAI.txtConversa.AppendText("Erro ao obter resposta: " & ex.Message & Environment.NewLine & Environment.NewLine)
                   End Sub)
        End Try

        Try

            ' frmAI.txtConversa.SelectionStart = txtConversa.TextLength
            frmAI.txtConversa.ScrollToCaret()
            frmAI.txtConversa.ResumeLayout()
        Catch ex As Exception
        Finally
        End Try

        frmAI.Show()

    End Sub

    Private Sub choBloqueaArquivoExistente_CheckedChanged(sender As Object, e As EventArgs) Handles choBloqueaArquivoExistente.CheckedChanged

        If choBloqueaArquivoExistente.Checked = True Then

            BloqueaArquivoExistente = True
        Else

            BloqueaArquivoExistente = False

        End If

    End Sub

    Private Sub btnDadosUsuario_Click(sender As Object, e As EventArgs) Handles btnDadosUsuario.Click

        TimerMedidordgvMedidorNumeroOS.Enabled = True

    End Sub

    Private Sub DGVListaMaterialSWMateriais_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DGVListaMaterialSWMateriais.DataBindingComplete

        If DGVListaMaterialSWMateriais IsNot Nothing AndAlso DGVListaMaterialSWMateriais.Rows IsNot Nothing AndAlso DGVListaMaterialSWMateriais.Rows.Count > 0 Then

            cl_BancoDados.FormatarDataGridView(DGVListaMaterialSWMateriais, "SIM")
        End If

    End Sub

    Private Sub TimerAviso_Tick(sender As Object, e As EventArgs) Handles TimerAviso.Tick

        TimerAviso.Stop() ' Evita reentrância

        ' If Vem_de_Onde = "Salvar" Then

        Task.Run(Sub()

                     Dim resultado As Boolean = DadosArquivoCorrente.AtualizaDesenho(swModel)

                     Invoke(Sub()
                                If resultado = True Then
                                    btnPendencias.BackColor = Color.Green
                                    btnPendencias.Text = "Atualizado"
                                    btnPendencias.Image = My.Resources.verificado
                                    ' Inicia timer para restaurar o estado natural
                                    TimerResetBotao.Interval = 1000
                                    TimerResetBotao.Start()
                                Else
                                    btnPendencias.BackColor = CorOriginal
                                    btnPendencias.Text = TextoOriginal
                                    btnPendencias.Image = ImagemOriginal
                                End If

                            End Sub)
                 End Sub)

        'ElseIf Vem_de_Onde = "DXF" Then

        '    btnPendencias.BackColor = Color.Green
        '    btnPendencias.Text = "DXF Salvo com Sucesso!"
        '    btnPendencias.Image = My.Resources.verificado
        '    TimerResetBotao.Interval = 1000
        '    TimerResetBotao.Start()

        'ElseIf Vem_de_Onde = "PDF" Then

        '    btnPendencias.BackColor = Color.Green
        '    btnPendencias.Text = "PDF Salvo com Sucesso!"
        '    btnPendencias.Image = My.Resources.verificado
        '    TimerResetBotao.Interval = 1000
        '    TimerResetBotao.Start()

        'ElseIf Vem_de_Onde = "Leitura" Then

        '    btnPendencias.BackColor = Color.Green
        '    btnPendencias.Text = "Leitura de Dados executada com Sucesso!"
        '    btnPendencias.Image = My.Resources.verificado
        '    TimerResetBotao.Interval = 1000
        '    TimerResetBotao.Start()

        'End If

    End Sub

    Private Sub TimerdgvPlanejamento_Tick(sender As Object, e As EventArgs) Handles TimerdgvPlanejamentoProjetista.Tick

        Dim sql As String

        If chkMostraProjetoTagsFinalizadas.Checked = True Then

            sql = ""
        Else

            sql = " and (t.RealizadoFinalEngenharia = '' or t.RealizadoFinalEngenharia is null )"

        End If

        If Me.txtPesqProjetoProjetista.TextLength > 0 Then

            sql += " and t.projeto like '%" & Me.txtPesqProjetoProjetista.Text & "%'"

        End If

        If Me.txtPesqTagProjetista.TextLength > 0 Then

            sql += " and t.tag like '%" & Me.txtPesqTagProjetista.Text & "%'"

        End If

        If chkProjetista.Checked = True Then

            sql += " and (t.ProjetistaPlanejado = '" & Usuario.NomeCompleto & "')"
        Else

            sql += " and (t.ProjetistaPlanejado = '" & Usuario.NomeCompleto & "' OR t.ProjetistaPlanejado = '' OR t.ProjetistaPlanejado IS NULL)"

        End If

        dgvPlanejamentoProjetista.DataSource = cl_BancoDados.CarregarDados("SELECT
    t.IdTag,
    o.idordemservico,
    t.Projeto,
    t.Tag,
    t.DescTag,
    t.ProjetistaPlanejado,
    t.PlanejadoinicioEngenharia,
    t.RealizadoinicioEngenharia,
    t.PlanejadoFinalEngenharia,
    t.RealizadoFinalEngenharia,
    t.ProjetistaRealizado,
    t.CaminhoIsometrico
FROM tags AS t
LEFT JOIN ordemservico AS o
       ON o.idtag = t.idtag
WHERE (t.D_E_L_E_T_E = '' OR t.D_E_L_E_T_E IS NULL) and
            (t.finalizado = '' or t.Finalizado is null) " & sql & "  ORDER BY t.Tag")

        TimerdgvPlanejamentoProjetista.Enabled = False
        dgvPlanejamentoProjetista.Columns("IdTag").Visible = False
        dgvPlanejamentoProjetista.Columns("CaminhoIsometrico").Visible = False

    End Sub

    Private Sub dgvPlanejamentoProjetista_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvPlanejamentoProjetista.DataBindingComplete

        cl_BancoDados.FormatarDataGridView(dgvMedidorNumeroOS, "SIM")

        Dim Plastart, Plastop, PlanejadoEng As String

        '  cl_BancoDados.FormatarDataGridView(dgvPlanejamentoProjetista, "SIM")

        If dgvPlanejamentoProjetista IsNot Nothing AndAlso dgvPlanejamentoProjetista.Rows IsNot Nothing AndAlso dgvPlanejamentoProjetista.Rows.Count > 0 Then

            ' --- Atualiza o grid ---
            For Each row As DataGridViewRow In dgvPlanejamentoProjetista.Rows
                If row.IsNewRow Then Continue For

                ' ====== 1) ÍCONE DE PDF ======
                Dim caminhoIso As String = If(row.Cells("CaminhoIsometrico").Value, "").ToString().Trim()
                If caminhoIso = "" Then
                    row.Cells("dgvcaminhoPDF").Value = My.Resources.Sem_Incone
                Else
                    row.Cells("dgvcaminhoPDF").Value = My.Resources.isometrico
                End If

                ' ====== 2) LEITURA DE DATAS ======
                Dim dtPlanIni As Date, temPlanIni As Boolean =
                    TryParsePtBR(row.Cells("PlanejadoinicioEngenharia").Value, dtPlanIni)

                Dim dtRealIni As Date, temRealIni As Boolean =
                    TryParsePtBR(row.Cells("RealizadoinicioEngenharia").Value, dtRealIni)

                Dim dtPlanFim As Date, temPlanFim As Boolean =
                    TryParsePtBR(row.Cells("PlanejadoFinalEngenharia").Value, dtPlanFim)

                Dim dtRealFim As Date, temRealFim As Boolean =
                    TryParsePtBR(row.Cells("RealizadoFinalEngenharia").Value, dtRealFim)

                ' ====== 3) LÓGICA DOS ÍCONES ======
                Dim icone As System.Drawing.Image = My.Resources.Sem_Incone

                ' 1) Finalizado
                If temRealFim Then
                    icone = My.Resources.verificado

                    ' 2) Em progresso (início real sem final real)
                ElseIf temRealIni AndAlso Not temRealFim Then
                    icone = My.Resources.start

                    ' 3) Atrasado para iniciar (planejado já passou e ainda não iniciou)
                ElseIf temPlanIni AndAlso Not temRealIni AndAlso dtPlanIni.Date <= Date.Today Then
                    icone = My.Resources.atencao
                End If

                ' (Opcional) Atraso de finalização: Planejado final passou sem final real
                If temPlanFim AndAlso Not temRealFim AndAlso dtPlanFim.Date < Date.Today Then
                    icone = My.Resources.atencao
                End If

                ' ====== 4) APLICA ÍCONE FINAL ======
                row.Cells("dgvEngpro").Value = icone
            Next
            'PlanejadoinicioEngenharia,
            'RealizadoinicioEngenharia,
            'PlanejadoFinalEngenharia,
            'RealizadoFinalEngenharia,

        End If

    End Sub

    ' --- Helper para conversão segura de datas no formato pt-BR ---
    Private Function TryParsePtBR(ByVal valor As Object, ByRef dt As Date) As Boolean
        dt = Date.MinValue
        If valor Is Nothing OrElse valor Is DBNull.Value Then Return False

        Dim s As String = valor.ToString().Trim()
        If s = "" Then Return False

        Dim fmts() As String = {
        "dd/MM/yyyy", "d/M/yyyy",
        "dd-MM-yyyy", "d-M-yyyy",
        "yyyy-MM-dd", "yyyy/MM/dd",
        "dd.MM.yyyy", "d.M.yyyy"
    }
        Dim ptBR = Globalization.CultureInfo.GetCultureInfo("pt-BR")

        Return Date.TryParseExact(s, fmts, ptBR, Globalization.DateTimeStyles.None, dt) _
           OrElse Date.TryParse(s, ptBR, Globalization.DateTimeStyles.None, dt)
    End Function

    Private Sub MarcarIncioDaExecuçãoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MarcarIncioDaExecuçãoToolStripMenuItem.Click

        Dim idtag As Integer = 0

        Try
            If dgvPlanejamentoProjetista.CurrentRow IsNot Nothing AndAlso
               dgvPlanejamentoProjetista.CurrentRow.Cells("idTag").Value IsNot Nothing Then

                idtag = Convert.ToInt32(dgvPlanejamentoProjetista.CurrentRow.Cells("idTag").Value)
            End If
        Catch ex As Exception
            idtag = 0
            Exit Sub
        End Try

        Dim resultado As MsgBoxResult

        resultado = MsgBox("Confirma o início do processo de engenharia do Projeto: " & dgvPlanejamentoProjetista.CurrentRow.Cells("Projeto").Value.ToString & " - para a Tag: " &
                       dgvPlanejamentoProjetista.CurrentRow.Cells("Tag").Value.ToString & " ?", vbYesNo + vbQuestion, "Atenção")

        If resultado.Yes Then

            If idtag <> 0 Then
                ' Atualiza banco
                If dgvPlanejamentoProjetista.CurrentRow.Cells("ProjetistaPlanejado").Value.ToString = "" Then

                    dgvPlanejamentoProjetista.CurrentRow.Cells("ProjetistaPlanejado").Value = Usuario.NomeCompleto

                    cl_BancoDados.AlteracaoEspecifica("tags", "ProjetistaPlanejado", Usuario.NomeCompleto, "IdTag", idtag)

                End If

                If dgvPlanejamentoProjetista.CurrentRow.Cells("PlanejadoInicioEngenharia").Value.ToString = "" Then

                    dgvPlanejamentoProjetista.CurrentRow.Cells("PlanejadoInicioEngenharia").Value = Date.Now.Date.ToString("dd/MM/yyyy")

                    cl_BancoDados.AlteracaoEspecifica("tags", "PlanejadoInicioEngenharia", Now.Date.ToString("dd/MM/yyyy"), "IdTag", idtag)

                End If

                If dgvPlanejamentoProjetista.CurrentRow.Cells("PlanejadoFinalEngenharia").Value.ToString = "" Then

                    dgvPlanejamentoProjetista.CurrentRow.Cells("PlanejadoFinalEngenharia").Value = Date.Now.Date.ToString("dd/MM/yyyy")

                    cl_BancoDados.AlteracaoEspecifica("tags", "PlanejadoFinalEngenharia", Now.Date.ToString("dd/MM/yyyy"), "IdTag", idtag)

                End If

                cl_BancoDados.AlteracaoEspecifica("tags", "ProjetistaRealizado", Usuario.NomeCompleto, "IdTag", idtag)
                cl_BancoDados.AlteracaoEspecifica("tags", "RealizadoinicioEngenharia", DateTime.Now.ToString, "IdTag", idtag)

                ' Atualiza visual
                dgvPlanejamentoProjetista.CurrentRow.Cells("ProjetistaRealizado").Value = Usuario.NomeCompleto
                dgvPlanejamentoProjetista.CurrentRow.Cells("RealizadoinicioEngenharia").Value = DateTime.Now.ToString '.ToString("dd/MM/yyyy")

                MsgBox("Processo de engenharia inicializado!", MsgBoxStyle.Information, "Atenção")
            Else
                MsgBox("ID da Tag inválido ou não identificado.", vbCritical, "Atenção")
            End If

        End If

    End Sub

    Private Sub MarcarFinalizaçãoDoProjetoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MarcarFinalizaçãoDoProjetoToolStripMenuItem.Click

        Dim idtag As Integer = 0

        Try
            If dgvPlanejamentoProjetista.CurrentRow IsNot Nothing AndAlso
               dgvPlanejamentoProjetista.CurrentRow.Cells("idTag").Value IsNot Nothing Then

                idtag = Convert.ToInt32(dgvPlanejamentoProjetista.CurrentRow.Cells("idTag").Value)
            End If
        Catch ex As Exception
            idtag = 0
            Exit Sub
        End Try

        Dim resultado As MsgBoxResult

        resultado = MsgBox("Confirma á finalização do processo de engenharia do Projeto: " & dgvPlanejamentoProjetista.CurrentRow.Cells("Projeto").Value.ToString & " - para a Tag: " &
                       dgvPlanejamentoProjetista.CurrentRow.Cells("Tag").Value.ToString & " ?", vbYesNo + vbQuestion, "Atenção")

        If resultado.Yes Then

            If idtag <> 0 Then
                ' Atualiza dados no banco
                cl_BancoDados.AlteracaoEspecifica("tags", "ProjetistaRealizado", Usuario.NomeCompleto, "IdTag", idtag)
                cl_BancoDados.AlteracaoEspecifica("tags", "RealizadoFinalEngenharia", DateTime.Now.ToString, "IdTag", idtag)

                ' Atualiza grid visual
                dgvPlanejamentoProjetista.CurrentRow.Cells("ProjetistaRealizado").Value = Usuario.NomeCompleto
                dgvPlanejamentoProjetista.CurrentRow.Cells("RealizadoFinalEngenharia").Value = DateTime.Now.ToString   '.ToString("dd/MM/yyyy")

                If My.Settings.BancoDadosAtivo = "alfatec2" Then

                    Dim corpoEmail As String = "Olá Eduardo Souza," & vbCrLf & vbCrLf &
"Informo que a atividade atribuída foi concluída com sucesso." & vbCrLf & vbCrLf &
"Detalhes da atividade:" & vbCrLf &
"Projeto; " & OrdemServico.Projeto & vbCrLf &
"Tag; " & OrdemServico.Tag & vbCrLf &
"Projetista; " & Usuario.NomeCompleto & vbCrLf &
"Data de Conclusão; " & DateTime.Now.ToString("dd/MM/yyyy HH:mm") & vbCrLf & vbCrLf &
"Por favor, revise e, se necessário, encaminhe para as próximas etapas do processo." & vbCrLf & vbCrLf &
"Atenciosamente," & Usuario.NomeCompleto & vbCrLf &
"Equipe de Engenharia"

                    ClasseEmail.EmailAlfatec("eduardo@alfatec.ind.br", "Eduardo Souza", corpoEmail, "Finalização do Projeto/Tag:" & OrdemServico.Projeto & " - " & OrdemServico.Tag)

                End If

            End If
        Else
            MsgBox("Não foi possível identificar a Tag selecionada!", vbCritical, "Atenção")
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        TimerdgvPlanejamentoProjetista.Enabled = True

    End Sub

    Private Sub ToolStripTextBox1_TextChanged(sender As Object, e As EventArgs) Handles ToolStripTextBox1.TextChanged

        cl_BancoDados.ComboBoxDataSet("projetos", "idProjeto", "Projeto",
                                      cboProjeto, " WHERE (D_E_L_E_T_E Is NULL Or D_E_L_E_T_E = '') and
                                      (Finalizado = '' OR Finalizado Is NULL) and (Liberado = 'S') and Projeto like '%" & ToolStripTextBox1.Text & "%'")

    End Sub

    Private Sub ToolStripButton6_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton6.Click

        If DescarregarLynx = True Then

            DescarregarLynx = False

            Panel1.Enabled = False

            ToolStripButton6.Image = My.Resources.desativado
        Else

            DescarregarLynx = True

            Panel1.Enabled = True

            ToolStripButton6.Image = My.Resources.ICONE_FOREST

        End If

    End Sub

    Private Sub tspOpcaoSalvamentoAUTOMADICO_Click(sender As Object, e As EventArgs) Handles tspOpcaoSalvamentoAUTOMADICO.Click

        If My.Settings.AtualizaCadastroComLeituraBOM = "NÃO" Then

            My.Settings.AtualizaCadastroComLeituraBOM = "SIM"

            tspOpcaoSalvamentoAUTOMADICO.Image = My.Resources.marcado
        Else

            My.Settings.AtualizaCadastroComLeituraBOM = "NÃO"

            tspOpcaoSalvamentoAUTOMADICO.Image = My.Resources.desmarcado

        End If
        ' Salva as configurações
        My.Settings.Save()

    End Sub

    Private Sub ToolStripButton6_MouseUp(sender As Object, e As MouseEventArgs) Handles ToolStripButton6.MouseUp

        ToolStripButton6.ToolTipText = "O Lynx esta no estado de: " & DescarregarLynx

    End Sub

    Private Sub tspOpcaoSalvamentoAUTOMADICO_MouseUp(sender As Object, e As MouseEventArgs) Handles tspOpcaoSalvamentoAUTOMADICO.MouseUp

        tspOpcaoSalvamentoAUTOMADICO.ToolTipText = "O Lynx esta a opção de salvamento automatico na Leitura da BOM marcado com: " & My.Settings.AtualizaCadastroComLeituraBOM

    End Sub

    Private Sub dgvPlanejamentoProjetista_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvPlanejamentoProjetista.DataError
        Try
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub TimerdgvOrdemservico_Tick(sender As Object, e As EventArgs) Handles TimerdgvOrdemservico.Tick

        ' Atualiza o DataGridView com os dados mais recentes
        dgvOrdemservico.DataSource = cl_BancoDados.CarregarDados("SELECT Projeto,
Tag,
idordemservico as OS,
codmatfabricante,
QtdeTotal,
DescResumo,
DescDetal,
EnderecoArquivo,
TxtTipoDesenho as Tipo_Desenho,
ORDEMSERVICOITEMFINALIZADO FROM viewordemservicoitem WHERE codmatfabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "' and
idordemservico like '%" & Me.txtOS.Text & "%' and
Tag like '%" & Me.TXTTag.Text & "%' and
Projeto like '%" & Me.TXTPROJETO.Text & "%' order by Projeto, tag,idordemservico ")
        ' Formata o DataGridView
        '  cl_BancoDados.FormatarDataGridView(dgvOrdemservico, "SIM")
        ' Desativa o timer para evitar atualizações contínuas
        TimerdgvOrdemservico.Enabled = False

        dgvOrdemservico.Columns("EnderecoArquivo").Visible = False

    End Sub

    Private Sub txtOS_TextChanged(sender As Object, e As EventArgs) Handles txtOS.TextChanged
        TimerdgvOrdemservico.Enabled = True
    End Sub

    Private Sub TXTPROJETO_TextChanged(sender As Object, e As EventArgs) Handles TXTPROJETO.TextChanged
        TimerdgvOrdemservico.Enabled = True
    End Sub

    Private Sub TXTTag_TextChanged(sender As Object, e As EventArgs) Handles TXTTag.TextChanged
        TimerdgvOrdemservico.Enabled = True
    End Sub

    Private Sub txtNomeArquivo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeArquivo.TextChanged
        TimerdgvOrdemservico.Enabled = True
    End Sub

    Private Sub dgvOrdemservico_DoubleClick(sender As Object, e As EventArgs) Handles dgvOrdemservico.DoubleClick

        Dim ArquivoListaBom As String = dgvOrdemservico.CurrentRow.Cells("EnderecoArquivo").Value.ToString

        ' Obtém o caminho completo
        ArquivoListaBom = Path.GetFullPath(ArquivoListaBom)

        ' Verifica se o arquivo existe e o abre
        If File.Exists(ArquivoListaBom) Then
            Process.Start(ArquivoListaBom)

        End If

    End Sub

    Private Sub chkMostraProjetoTagsFinalizadas_CheckedChanged(sender As Object, e As EventArgs) Handles chkMostraProjetoTagsFinalizadas.CheckedChanged

        TimerdgvPlanejamentoProjetista.Enabled = True

    End Sub

    Private Sub dgvPlanejamentoProjetista_DoubleClick(sender As Object, e As EventArgs) Handles dgvPlanejamentoProjetista.DoubleClick

        Dim ArquivoListaBom As String = dgvPlanejamentoProjetista.CurrentRow.Cells("CaminhoIsometrico").Value.ToString

        ' Obtém o caminho completo
        ArquivoListaBom = Path.GetFullPath(ArquivoListaBom)

        ' Verifica se o arquivo existe e o abre
        If File.Exists(ArquivoListaBom) Then
            Process.Start(ArquivoListaBom)

        End If

        dgvPlanejamentoProjetista.CurrentRow.DefaultCellStyle.BackColor = Color.BlueViolet

    End Sub

    Private Async Sub txtComentarios_DoubleClick(sender As Object, e As EventArgs) Handles txtComentarios.DoubleClick

        Dim prompt As String = Me.txtPalavraChave.Text

        Dim PerfuldoAgenteComentarios As String = "Você é projetista de equipamentos da empresa " & My.Settings.BancoDadosAtivo & ". Com base nos caminhos dos arquivos no servidor, elabore uma descrição técnica detalhada do equipamento.
Essa descrição será utilizada pelo setor comercial para facilitar a localização por meio de um sistema interno de busca textual, sejam sucinto não alucine e não utilize palavras complicadas.
Apenas retorne a descrição gerada, com no máximo 200 caracteres. Não inclua justificativas, explicações ou qualquer outro conteúdo além do texto descritivo."

        '  Dim prompt As String = Me.txtPalavraChave.Text

        Try
            Cursor.Current = Cursors.WaitCursor ' Cursor de espera
            Dim resposta As String = Await AjudaAI.ObterRespostaGPT(prompt, PerfuldoAgenteComentarios)
            Invoke(Sub()
                       Me.txtComentarios.Text = resposta
                   End Sub)
        Catch ex As Exception
            Invoke(Sub()
                       Me.txtComentarios.Text = ("Erro ao obter resposta: " & ex.Message & Environment.NewLine & Environment.NewLine)
                   End Sub)
        Finally
            Cursor.Current = Cursors.Default ' Volta ao cursor normal

        End Try
        Try
        Catch ex As Exception
        End Try

    End Sub

    Private Async Sub txtTitulo_DoubleClick(sender As Object, e As EventArgs) Handles txtTitulo.DoubleClick

        Dim prompt As String = Me.txtComentarios.Text

        Dim PerfuldoAgenteTitulo As String = "Você é projetista de equipamentos da empresa " & My.Settings.BancoDadosAtivo & ". Com base nos caminhos dos arquivos no servidor, elabore um título técnico objetivo e descritivo que identifique claramente o equipamento.
Esse título será utilizado pelo setor comercial para facilitar a localização por meio de um sistema interno de busca textual, sejam sucinto não alucine e não utilize palavras complicadas.
Retorne apenas o título gerado, com no máximo 100 caracteres. Não inclua descrições, explicações ou qualquer outro conteúdo."

        '  Dim prompt As String = Me.txtPalavraChave.Text

        Try

            Cursor.Current = Cursors.WaitCursor ' Cursor de espera

            Dim resposta As String = Await AjudaAI.ObterRespostaGPT(prompt, PerfuldoAgenteTitulo)
            Invoke(Sub()
                       Me.txtTitulo.Text = resposta
                   End Sub)
        Catch ex As Exception
            Invoke(Sub()
                       Me.txtTitulo.Text = ("Erro ao obter resposta: " & ex.Message & Environment.NewLine & Environment.NewLine)
                   End Sub)
        Finally
            Cursor.Current = Cursors.Default ' Volta ao cursor normal

        End Try
        Try
        Catch ex As Exception
        End Try

    End Sub

    Private Async Sub txtAssuntoSubiTitulo_DoubleClick(sender As Object, e As EventArgs) Handles txtAssuntoSubiTitulo.DoubleClick

        Dim prompt As String = Me.txtTitulo.Text

        Dim PerfuldoAgenteSubTitulo As String = "Você é projetista de equipamentos da empresa " & My.Settings.BancoDadosAtivo & ". Com base nos caminhos dos arquivos no servidor, elabore um subtítulo técnico complementar ao título, fornecendo informações adicionais como aplicação, função ou diferencial técnico do equipamento.
Esse subtítulo será utilizado pelo setor comercial para facilitar a localização por meio de um sistema interno de busca textual, sejam sucinto não alucine e não utilize palavras complicadas.
Retorne apenas o subtítulo gerado, com no máximo 150 caracteres. Não inclua descrições extensas, explicações ou qualquer outro conteúdo."

        Try
            Cursor.Current = Cursors.WaitCursor ' Cursor de espera
            Dim resposta As String = Await AjudaAI.ObterRespostaGPT(prompt, PerfuldoAgenteSubTitulo)
            Invoke(Sub()
                       Me.txtAssuntoSubiTitulo.Text = resposta
                   End Sub)
        Catch ex As Exception
            Invoke(Sub()
                       Me.txtAssuntoSubiTitulo.Text = ("Erro ao obter resposta: " & ex.Message & Environment.NewLine & Environment.NewLine)
                   End Sub)
        Finally
            Cursor.Current = Cursors.Default ' Volta ao cursor normal

        End Try
        Try
        Catch ex As Exception
        End Try

    End Sub

    Private Sub chkProjetista_CheckedChanged(sender As Object, e As EventArgs) Handles chkProjetista.CheckedChanged

        TimerdgvPlanejamentoProjetista.Enabled = True

    End Sub

    Private Sub BuscarListaDeMaterialNoFAPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BuscarListaDeMaterialNoFAPToolStripMenuItem.Click

        Dim ObjetoExcel As Microsoft.Office.Interop.Excel.Application

        Dim pasta1 As Microsoft.Office.Interop.Excel.Workbook

        Dim planilha As Microsoft.Office.Interop.Excel.Worksheet

        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.Filter = "Arquivos Excel (*.xlsx)|*.xlsx|Todos os arquivos (*.*)|*.*"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = openFileDialog1.FileName

            ObjetoExcel = CreateObject("Excel.application")

            Try
                pasta1 = ObjetoExcel.Workbooks.Open(filePath)
            Catch ex As Exception
                MsgBox("A planilha não foi encontrada, favor indicar o caminho correto nas configurações")
                Exit Sub
            End Try

            planilha = pasta1.ActiveSheet

            Dim linha As Integer = 11

            Try


                ' Ler valores da coluna A até encontrar uma célula vazia
                Do While Not String.IsNullOrEmpty(planilha.Range("A" & linha).Value)

                    DadosArquivoCorrente.NomeArquivoSemExtensao = planilha.Range("B" & linha).Value.ToString()
                    DadosArquivoCorrente.Titulo = planilha.Range("B" & linha).Value.ToString()
                    DadosArquivoCorrente.AssuntoSubiTitulo = planilha.Range("B" & linha).Value.ToString()

                    DadosArquivoCorrente.ComprimentoBlank = ""
                    DadosArquivoCorrente.LarguraBlank = ""

                    DadosArquivoCorrente.material = planilha.Range("C" & linha).Value.ToString()

                    If DadosArquivoCorrente.material.Contains("20") Then

                        DadosArquivoCorrente.material = "AISI 202 ESC"

                    End If

                    If DadosArquivoCorrente.material.Contains("30") Then

                        DadosArquivoCorrente.material = "AISI 304 ESC"

                    End If





                    DadosArquivoCorrente.EnderecoArquivo = "SLDPRT"

                    DadosArquivoCorrente.Espessura = planilha.Range("D" & linha).Value.ToString().Replace(",", ".")

                    If DadosArquivoCorrente.material.Contains("20") And (DadosArquivoCorrente.Espessura = "0,6" Or DadosArquivoCorrente.Espessura = "0.6") Then

                        DadosArquivoCorrente.Espessura = "0.5"

                    End If

                    If (DadosArquivoCorrente.Espessura = "0,95" Or DadosArquivoCorrente.Espessura = "0.95") Then

                        DadosArquivoCorrente.Espessura = "1.0"

                    End If

                    If DadosArquivoCorrente.material.Contains("AL") Then

                        DadosArquivoCorrente.material = "ALUMINIO"
                        DadosArquivoCorrente.Espessura = "0.7"


                    End If

                    If DadosArquivoCorrente.Espessura = "1" Then

                        DadosArquivoCorrente.Espessura = "1.0"

                    End If

                    If DadosArquivoCorrente.Espessura = "2" Then

                        DadosArquivoCorrente.Espessura = "2.0"

                    End If

                    qtdePecaLm = planilha.Range("E" & linha).Value.ToString()

                    DadosArquivoCorrente.TipoDesenho = "CHAPARIA"
                    DadosArquivoCorrente.Corte = "1"
                    DadosArquivoCorrente.Dobra = "1"
                    DadosArquivoCorrente.Solda = "1"
                    DadosArquivoCorrente.Pintura = "1"
                    DadosArquivoCorrente.Montagem = "1"
                    DadosArquivoCorrente.rnc = ""

                    DadosArquivoCorrente.Alturacaixadelimitadora = ""
                    DadosArquivoCorrente.Larguracaixadelimitadora = ""
                    DadosArquivoCorrente.Profundidadeaixadelimitadora = ""
                    DadosArquivoCorrente.ItemEstoque = "NÃO"
                    DadosArquivoCorrente.Bloqueado = ""

                    ' Preencher o DataGridView com os dados da peça
                    dgvDataGridBOM.Rows.Add(My.Resources.Sem_Incone,
                                        iconeDXF,
                                        iconePDF,
                                        iconeTipoArquivo,
                                    iconeAtencao,
                                    DadosArquivoCorrente.IdMaterial,
                                    DadosArquivoCorrente.NomeArquivoSemExtensao,
                                    DadosArquivoCorrente.Titulo,
                                    DadosArquivoCorrente.AssuntoSubiTitulo,
                                    DadosArquivoCorrente.Author,
                                    DadosArquivoCorrente.PalavraChave,
                                    DadosArquivoCorrente.Comentarios,
                                    DadosArquivoCorrente.Espessura,
                                    DadosArquivoCorrente.ComprimentoBlank,
                                    DadosArquivoCorrente.LarguraBlank,
                                    DadosArquivoCorrente.material,
                                    DadosArquivoCorrente.AreaPintura,
                                    DadosArquivoCorrente.NumeroDobras,
                                    DadosArquivoCorrente.Massa,
                                    DadosArquivoCorrente.EnderecoArquivo,
                                    DadosArquivoCorrente.Acabamento,
                                    DadosArquivoCorrente.soldagem,
                                    DadosArquivoCorrente.TipoDesenho,
                                    DadosArquivoCorrente.Corte,
                                    DadosArquivoCorrente.Dobra,
                                    DadosArquivoCorrente.Solda,
                                    DadosArquivoCorrente.Pintura,
                                    DadosArquivoCorrente.Montagem,
                                    DadosArquivoCorrente.rnc,
                                    DadosArquivoCorrente.Alturacaixadelimitadora,
                                    DadosArquivoCorrente.Larguracaixadelimitadora,
                                    DadosArquivoCorrente.Profundidadeaixadelimitadora,
                                    DadosArquivoCorrente.ItemEstoque,
                                    qtdePecaLm, DadosArquivoCorrente.Bloqueado,
                                    "")

                    linha += 1

                Loop

            Catch ex As Exception
            Finally
            End Try

            pasta1.Close(False)
            ObjetoExcel.Application.Visible = False

            MsgBox("Dados importados com Sucesso!!!", vbInformation, "Atenção!!!!")

        End If

    End Sub

    Private Sub mnudgvDataGridBOM_Opening(sender As Object, e As CancelEventArgs) Handles mnudgvDataGridBOM.Opening

        If My.Settings.BancoDadosAtivo = "alfatec2" Or My.Settings.BancoDadosAtivo = "lynxlocal" Then

            InserirMaterialPeloFAPToolStripMenuItem.Enabled = True
            LocarCadasttroDeDesenhoPeloNumweroDoDocumentoToolStripMenuItem.Enabled = True

        End If

    End Sub

    Private Sub LocarCadasttroDeDesenhoPeloNumweroDoDocumentoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LocarCadasttroDeDesenhoPeloNumweroDoDocumentoToolStripMenuItem.Click

        ' Valida a lista do SW
        If DGVListaMaterialSW Is Nothing OrElse DGVListaMaterialSW.Rows.Count = 0 Then
            MsgBox("Não há dados na lista de materiais do SolidWorks, favor carregar a lista primeiro.", vbCritical, "Atenção")
            Exit Sub
        End If

        ' Valida a grade alvo
        If dgvDataGridBOM Is Nothing OrElse dgvDataGridBOM.Rows.Count = 0 Then Exit Sub
        If Not dgvDataGridBOM.Columns.Contains("codmatfabricante") Then Exit Sub
        If Not dgvDataGridBOM.Columns.Contains("EnderecoArquivo") Then Exit Sub

        ' Percorre as LINHAS (não as colunas)
        For i As Integer = 0 To dgvDataGridBOM.Rows.Count - 1
            Try
                Dim row = dgvDataGridBOM.Rows(i)
                If row Is Nothing OrElse row.IsNewRow Then Continue For

                Dim objCod = row.Cells("codmatfabricante").Value
                Dim cod As String = If(objCod Is Nothing OrElse objCod Is DBNull.Value, "", objCod.ToString().Trim())

                If Not String.IsNullOrWhiteSpace(cod) Then
                    ' Escapa aspas simples para evitar erro de SQL (mantendo sua função atual)
                    Dim codSql As String = cod.Replace("'", "''")

                    cl_BancoDados.RetornaCampoDaPesquisa(
                        "SELECT enderecoarquivo FROM material WHERE codmatfabricante = '" & codSql & "'",
                        "enderecoarquivo"
                    )

                    DadosArquivoCorrente.EnderecoArquivo = If(VCampo0 Is Nothing, "", VCampo0.ToString().Trim())

                    If Not String.IsNullOrWhiteSpace(DadosArquivoCorrente.EnderecoArquivo) Then
                        row.Cells("EnderecoArquivo").Value = DadosArquivoCorrente.EnderecoArquivo
                    End If
                End If
            Catch ex As Exception
                ' Continua o loop mesmo em caso de erro pontual
                ' (Opcional) Logar ex.Message
            End Try
        Next

        MsgBox("Processo concluído!", vbInformation, "Atenção")

    End Sub

    Private Sub btnAtualizarDadosOrdemServicoItens_Click(sender As Object, e As EventArgs) Handles btnAtualizarDadosOrdemServicoItens.Click
        Try
            ' 1) Coleta os IDs válidos do grid
            Dim ids As New List(Of Integer)

            For Each row As DataGridViewRow In dgvOrdemservico.Rows
                If row.IsNewRow Then Continue For

                Dim v = row.Cells("idordemservicoitem").Value
                Dim id As Integer
                If v IsNot Nothing AndAlso Integer.TryParse(v.ToString(), id) AndAlso id > 0 Then
                    ids.Add(id)
                End If
            Next

            If ids.Count = 0 Then
                MessageBox.Show("Nenhum IdOrdemServicoItem válido encontrado no grid.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ' 2) Monta a SQL de UPDATE (JOIN material -> ordemservicoitem)
            '    ATENÇÃO: adicione/remova campos conforme sua necessidade
            Dim sb As New StringBuilder()
            sb.AppendLine("UPDATE ordemservicoitem o")
            sb.AppendLine("JOIN material m ON m.IdMaterial = o.IdMaterial")
            sb.AppendLine("SET")
            sb.AppendLine("  o.DescResumo                    = m.DescResumo,")
            sb.AppendLine("  o.DescDetal                     = m.DescDetal,")
            sb.AppendLine("  o.Autor                         = m.Autor,")
            sb.AppendLine("  o.Palavrachave                  = m.Palavrachave,")
            sb.AppendLine("  o.Notas                         = m.Notas,")
            sb.AppendLine("  o.Espessura                     = m.Espessura,")
            sb.AppendLine("  o.AreaPintura                   = m.AreaPintura,")
            sb.AppendLine("  o.NumeroDobras                  = m.NumeroDobras,")
            sb.AppendLine("  o.Peso                          = m.Peso,")
            sb.AppendLine("  o.Unidade                       = m.Unidade,")
            sb.AppendLine("  o.UnidadeSW                     = m.UnidadeSW,")
            sb.AppendLine("  o.ValorSW                       = m.ValorSW,")
            sb.AppendLine("  o.Altura                        = m.Altura,")
            sb.AppendLine("  o.Largura                       = m.Largura,")
            sb.AppendLine("  o.CodMatFabricante              = m.CodMatFabricante,")
            sb.AppendLine("  o.DtCad                         = m.DtCad,")
            sb.AppendLine("  o.UsuarioCriacao                = m.UsuarioCriacao,")
            sb.AppendLine("  o.UsuarioAlteracao              = m.UsuarioAlteracao,")
            sb.AppendLine("  o.DtAlteracao                   = m.DtAlteracao,")
            sb.AppendLine("  o.EnderecoArquivo               = m.EnderecoArquivo,")
            sb.AppendLine("  o.MaterialSW                    = m.MaterialSW,")
            sb.AppendLine("  o.txtSoldagem                   = m.txtSoldagem,")
            sb.AppendLine("  o.txtTipoDesenho                = m.txtTipoDesenho,")
            sb.AppendLine("  o.txtCorte                      = m.txtCorte,")
            sb.AppendLine("  o.txtDobra                      = m.txtDobra,")
            sb.AppendLine("  o.txtSolda                      = m.txtSolda,")
            sb.AppendLine("  o.txtPintura                    = m.txtPintura,")
            sb.AppendLine("  o.txtMontagem                   = m.txtMontagem,")
            sb.AppendLine("  o.Comprimentocaixadelimitadora  = m.Comprimentocaixadelimitadora,")
            sb.AppendLine("  o.Larguracaixadelimitadora      = m.Larguracaixadelimitadora,")
            sb.AppendLine("  o.Espessuracaixadelimitadora    = m.Espessuracaixadelimitadora")

            sb.AppendLine("WHERE o.IdOrdemServicoItem IN (" & String.Join(",", ids) & ");")

            Dim sql As String = sb.ToString()

            ' 3) Executa
            cl_BancoDados.Salvar(sql)

            MessageBox.Show("Dados atualizados com sucesso para " & ids.Count & " itens.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Falha ao atualizar: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtPesqProjetoProjetista_TextChanged(sender As Object, e As EventArgs) Handles txtPesqProjetoProjetista.TextChanged

        TimerdgvPlanejamentoProjetista.Enabled = True

    End Sub

    Private Sub txtPesqTagProjetista_TextChanged(sender As Object, e As EventArgs) Handles txtPesqTagProjetista.TextChanged

        TimerdgvPlanejamentoProjetista.Enabled = True

    End Sub

    Private Sub dgvPlanejamentoProjetista_Click(sender As Object, e As EventArgs) Handles dgvPlanejamentoProjetista.Click

        OrdemServico.Projeto = dgvPlanejamentoProjetista.CurrentRow.Cells("Projeto").Value.ToString

        OrdemServico.Tag = dgvPlanejamentoProjetista.CurrentRow.Cells("Tag").Value.ToString

    End Sub

    Private Sub AtualizaDesenhoDeDetalhamentoAjusrandoAReferenciaToolStripMenuItem_Click(sender As Object, e As EventArgs)

        Dim ok = SwRelink.RepararReferenciasPorBase(swapp, "G:\Meu Drive\04-Arquivos SolidWorks\6113_GUIA_DE_BANDEJA_DIREITA.SLDDRW")

    End Sub

    Public Function ExtrairValor(ByVal texto As String) As String
        ' Verifica se a string contém o caractere "#" e "x"
        If String.IsNullOrEmpty(texto) OrElse Not texto.Contains("#") OrElse Not texto.Contains("x") Then
            Return String.Empty
        End If

        Try
            ' Remove tudo antes de "#" e depois pega até o primeiro "x"
            Dim parte As String = texto.Substring(texto.IndexOf("#"c) + 1)
            parte = parte.Substring(0, parte.IndexOf("x"c))

            ' Remove espaços extras
            Return parte.Trim()
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Function ExtrairSegundoValor(ByVal texto As String) As String
        ' Validação básica
        If String.IsNullOrEmpty(texto) OrElse texto.Count(Function(c) c = "x"c) < 2 Then
            Return String.Empty
        End If

        Try
            ' Localiza o primeiro "x"
            Dim primeiroX As Integer = texto.IndexOf("x"c)

            ' Pega o texto após o primeiro "x"
            Dim restante As String = texto.Substring(primeiroX + 1).Trim()

            ' Agora encontra o próximo "x" na parte restante
            Dim segundoX As Integer = restante.IndexOf("x"c)

            ' Extrai o valor entre os dois "x"
            Dim resultado As String = restante.Substring(0, segundoX).Trim()

            Return resultado
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Function ExtrairTerceiroValor(ByVal texto As String) As String
        ' Validação: precisa conter pelo menos dois "x"
        If String.IsNullOrEmpty(texto) OrElse texto.Count(Function(c) c = "x"c) < 2 Then
            Return String.Empty
        End If

        Try
            ' Localiza o primeiro "x"
            Dim primeiroX As Integer = texto.IndexOf("x"c)

            ' Corta a string depois do primeiro "x"
            Dim restante As String = texto.Substring(primeiroX + 1).Trim()

            ' Localiza o segundo "x" na parte restante
            Dim segundoX As Integer = restante.IndexOf("x"c)

            ' Agora corta a string depois do segundo "x"
            Dim parteFinal As String = restante.Substring(segundoX + 1).Trim()

            ' Retorna apenas o valor final (ex: 1634)
            Return parteFinal
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Private Sub BuscarListaDePeçasAvulçasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BuscarListaDePeçasAvulçasToolStripMenuItem.Click

        Dim ObjetoExcel As Microsoft.Office.Interop.Excel.Application

        Dim pasta1 As Microsoft.Office.Interop.Excel.Workbook

        Dim planilha As Microsoft.Office.Interop.Excel.Worksheet

        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.Filter = "Arquivos Excel (*.xlsx)|*.xlsx|Todos os arquivos (*.*)|*.*"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = openFileDialog1.FileName

            ObjetoExcel = CreateObject("Excel.application")

            Try
                pasta1 = ObjetoExcel.Workbooks.Open(filePath)
            Catch ex As Exception
                MsgBox("A planilha não foi encontrada, favor indicar o caminho correto nas configurações")
                Exit Sub
            End Try

            planilha = pasta1.ActiveSheet

            Dim linha As Integer = 2

            ' Ler valores da coluna A até encontrar uma célula vazia
            Do While Not String.IsNullOrEmpty(planilha.Range("A" & linha).Value)

                DadosArquivoCorrente.NomeArquivoSemExtensao = planilha.Range("B" & linha).Value.ToString()
                DadosArquivoCorrente.Titulo = planilha.Range("B" & linha).Value.ToString()
                DadosArquivoCorrente.AssuntoSubiTitulo = planilha.Range("B" & linha).Value.ToString()

                Dim entrada As String = planilha.Range("D" & linha).Value.ToString()
                Dim PrimeiroValor As String = ExtrairValor(entrada)
                Dim segundoValor As String = ExtrairSegundoValor(entrada)
                Dim TerceiroValor As String = ExtrairTerceiroValor(entrada)

                DadosArquivoCorrente.Espessura = PrimeiroValor.Replace(",", ".") ' planilha.Range("D" & linha).Value.ToString().Replace(",", ".")
                DadosArquivoCorrente.ComprimentoBlank = segundoValor.Replace(",", ".")
                DadosArquivoCorrente.LarguraBlank = TerceiroValor.Replace(",", ".")

                DadosArquivoCorrente.material = planilha.Range("E" & linha).Value.ToString()

                DadosArquivoCorrente.EnderecoArquivo = "SLDPRT"

                qtdePecaLm = planilha.Range("A" & linha).Value.ToString().Replace(",", ".")

                DadosArquivoCorrente.TipoDesenho = "CHAPARIA"
                DadosArquivoCorrente.Corte = "1"
                DadosArquivoCorrente.Dobra = "1"
                DadosArquivoCorrente.Solda = "1"
                DadosArquivoCorrente.Pintura = "1"
                DadosArquivoCorrente.Montagem = "1"
                DadosArquivoCorrente.rnc = ""

                DadosArquivoCorrente.Alturacaixadelimitadora = ""
                DadosArquivoCorrente.Larguracaixadelimitadora = ""
                DadosArquivoCorrente.Profundidadeaixadelimitadora = ""
                DadosArquivoCorrente.ItemEstoque = "NÃO"
                DadosArquivoCorrente.Bloqueado = ""

                iconeDXF = My.Resources.Sem_Incone
                iconePDF = My.Resources.Sem_Incone
                iconeTipoArquivo = My.Resources.Sem_Incone
                iconeAtencao = My.Resources.Sem_Incone

                ' Preencher o DataGridView com os dados da peça
                dgvDataGridBOM.Rows.Add(My.Resources.Sem_Incone,
                                        iconeDXF,
                                        iconePDF,
                                        iconeTipoArquivo,
                                    iconeAtencao,
                                    DadosArquivoCorrente.IdMaterial,
                                    DadosArquivoCorrente.NomeArquivoSemExtensao,
                                    DadosArquivoCorrente.Titulo,
                                    DadosArquivoCorrente.AssuntoSubiTitulo,
                                    DadosArquivoCorrente.Author,
                                    DadosArquivoCorrente.PalavraChave,
                                    DadosArquivoCorrente.Comentarios,
                                    DadosArquivoCorrente.Espessura,
                                    DadosArquivoCorrente.ComprimentoBlank,
                                    DadosArquivoCorrente.LarguraBlank,
                                    DadosArquivoCorrente.material,
                                    DadosArquivoCorrente.AreaPintura,
                                    DadosArquivoCorrente.NumeroDobras,
                                    DadosArquivoCorrente.Massa,
                                    DadosArquivoCorrente.EnderecoArquivo,
                                    DadosArquivoCorrente.Acabamento,
                                    DadosArquivoCorrente.soldagem,
                                    DadosArquivoCorrente.TipoDesenho,
                                    DadosArquivoCorrente.Corte,
                                    DadosArquivoCorrente.Dobra,
                                    DadosArquivoCorrente.Solda,
                                    DadosArquivoCorrente.Pintura,
                                    DadosArquivoCorrente.Montagem,
                                    DadosArquivoCorrente.rnc,
                                    DadosArquivoCorrente.Alturacaixadelimitadora,
                                    DadosArquivoCorrente.Larguracaixadelimitadora,
                                    DadosArquivoCorrente.Profundidadeaixadelimitadora,
                                    DadosArquivoCorrente.ItemEstoque,
                                    qtdePecaLm, DadosArquivoCorrente.Bloqueado,
                                    "")

                linha += 1

            Loop

            pasta1.Close(False)
            ObjetoExcel.Application.Visible = False

            MsgBox("Dados importados com Sucesso!!!", vbInformation, "Atenção!!!!")

        End If

    End Sub


    Dim FormataDGVListaMaterialSW As Boolean = False

    Private Sub DGVListaMaterialSW_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DGVListaMaterialSW.DataBindingComplete



        If DGVListaMaterialSW IsNot Nothing AndAlso DGVListaMaterialSW.Rows IsNot Nothing AndAlso DGVListaMaterialSW.Rows.Count > 0 Then



            'Cursor.Current = Cursors.WaitCursor

            For Each row As DataGridViewRow In DGVListaMaterialSW.Rows

                    iconeDXF = My.Resources.Sem_Incone
                    iconePDF = My.Resources.Sem_Incone
                    iconeTipoArquivo = My.Resources.Sem_Incone
                    iconeAtencao = My.Resources.Sem_Incone

                    Dim valorEnderecoArquivo As String = If(row.Cells("EnderecoArquivo").Value, "").ToString()
                    Dim valorProdutoPrincipal As String = If(row.Cells("ProdutoPrincipal").Value, "").ToString()
                    Dim EnderecoArquivo As String = If(row.Cells("EnderecoArquivo").Value, "").ToString()

                    If valorEnderecoArquivo.IndexOf(".SLDASM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                        ' Define a imagem na coluna "dgvIconeItemOS" se for .SLDASM
                        row.Cells("dgvIconeItemOS").Value = My.Resources.IcopneMontagemSW ' Substitua pelo seu ícone

                    End If

                    If valorProdutoPrincipal.IndexOf("SIM", StringComparison.OrdinalIgnoreCase) >= 0 Then
                        row.Cells("dgvIconeItemOS").Value = My.Resources.IconeswPrincipal ' Substitua pelo seu ícone

                    ElseIf valorEnderecoArquivo.IndexOf(".SLDPRT", StringComparison.OrdinalIgnoreCase) >= 0 Then
                        ' Define outra imagem se for .SLDPRT
                        row.Cells("dgvIconeItemOS").Value = My.Resources.IcopneMontagemPRT

                    ElseIf EnderecoArquivo.ToString = "" Then

                        row.Cells("dgvIconeItemOS").Value = My.Resources.sinco_Diversos 'My.Resources.material_escolar_32

                    End If

                    Dim dxf, pdf As String

                    ' Dim enderecoArquivo As String = DGVListaMaterialSW.Rows(i).Cells("EnderecoArquivo").Value.ToString()

                    ' Verifica se o arquivo é uma peça (.SLDPRT) ou uma montagem (.SLDASM) e altera para .dxf
                    If valorEnderecoArquivo.EndsWith(".SLDPRT", StringComparison.OrdinalIgnoreCase) OrElse
               valorEnderecoArquivo.EndsWith(".SLDASM", StringComparison.OrdinalIgnoreCase) Then
                        dxf = Path.ChangeExtension(valorEnderecoArquivo, ".dxf")

                        ' Verifica se o arquivo DXF existe
                        If File.Exists(dxf) Then
                            row.Cells("DGVDXF").Value = My.Resources.arquivo_dxf
                        Else
                            row.Cells("DGVDXF").Value = My.Resources.Sem_Incone
                        End If
                    End If

                    ' Altera para .pdf
                    If valorEnderecoArquivo.EndsWith(".SLDPRT", StringComparison.OrdinalIgnoreCase) OrElse
               valorEnderecoArquivo.EndsWith(".SLDASM", StringComparison.OrdinalIgnoreCase) Then
                        pdf = Path.ChangeExtension(valorEnderecoArquivo, ".pdf")

                        ' Verifica se o arquivo PDF existe
                        If File.Exists(pdf) Then
                            row.Cells("DGVPDF").Value = My.Resources.ficheiro_pdf
                        Else
                            row.Cells("DGVPDF").Value = My.Resources.Sem_Incone
                        End If
                    End If


                    If valorEnderecoArquivo = "" Then
                        row.Cells("DGVPDF").Value = My.Resources.Sem_Incone
                        row.Cells("DGVDXF").Value = My.Resources.Sem_Incone
                    End If

                Next

            If FormataDGVListaMaterialSW = False Then
                DGVListaMaterialSW.Columns("IdOrdemServicoItem").Visible = False
                DGVListaMaterialSW.Columns("IdOrdemServico").Visible = False
                DGVListaMaterialSW.Columns("Projeto").Visible = False
                DGVListaMaterialSW.Columns("Tag").Visible = False
                DGVListaMaterialSW.Columns("Estatus_OrdemServico").Visible = False
                DGVListaMaterialSW.Columns("IdMaterial").Visible = False
                DGVListaMaterialSW.Columns("QtdeTotal").Visible = True
                DGVListaMaterialSW.Columns("CriadoPor").Visible = False
                DGVListaMaterialSW.Columns("DataCriacao").Visible = False
                DGVListaMaterialSW.Columns("Estatus").Visible = False
                DGVListaMaterialSW.Columns("Acabamento").Visible = True
                DGVListaMaterialSW.Columns("D_E_L_E_T_E").Visible = False
                DGVListaMaterialSW.Columns("OrdemServicoItemFinalizado").Visible = False
                DGVListaMaterialSW.Columns("IdEmpresa").Visible = False
                DGVListaMaterialSW.Columns("idProjeto").Visible = False
                DGVListaMaterialSW.Columns("IdTag").Visible = False
                DGVListaMaterialSW.Columns("DescResumo").Visible = True
                DGVListaMaterialSW.Columns("DescDetal").Visible = True
                DGVListaMaterialSW.Columns("Autor").Visible = False
                DGVListaMaterialSW.Columns("Palavrachave").Visible = False
                DGVListaMaterialSW.Columns("Notas").Visible = False
                DGVListaMaterialSW.Columns("Espessura").Visible = True
                DGVListaMaterialSW.Columns("AreaPintura").Visible = False
                DGVListaMaterialSW.Columns("NumeroDobras").Visible = False
                DGVListaMaterialSW.Columns("Peso").Visible = False
                DGVListaMaterialSW.Columns("Unidade").Visible = False
                DGVListaMaterialSW.Columns("UnidadeSW").Visible = False
                DGVListaMaterialSW.Columns("ValorSW").Visible = False
                DGVListaMaterialSW.Columns("Altura").Visible = False
                DGVListaMaterialSW.Columns("Largura").Visible = False
                DGVListaMaterialSW.Columns("CodMatFabricante").Visible = True
                DGVListaMaterialSW.Columns("DtCad").Visible = False
                DGVListaMaterialSW.Columns("UsuarioCriacao").Visible = False
                DGVListaMaterialSW.Columns("UsuarioAlteracao").Visible = False
                DGVListaMaterialSW.Columns("DtAlteracao").Visible = False
                DGVListaMaterialSW.Columns("MaterialSW").Visible = False
                DGVListaMaterialSW.Columns("Fator").Visible = True
                DGVListaMaterialSW.Columns("qtde").Visible = True
                DGVListaMaterialSW.Columns("txtSoldagem").Visible = False
                DGVListaMaterialSW.Columns("txtTipoDesenho").Visible = False
                DGVListaMaterialSW.Columns("txtCorte").Visible = False
                DGVListaMaterialSW.Columns("txtDobra").Visible = False
                DGVListaMaterialSW.Columns("txtSolda").Visible = False
                DGVListaMaterialSW.Columns("txtPintura").Visible = False
                DGVListaMaterialSW.Columns("txtMontagem").Visible = False
                DGVListaMaterialSW.Columns("QtdeRomaneio").Visible = False
                DGVListaMaterialSW.Columns("Liberado_Engenharia").Visible = False
                DGVListaMaterialSW.Columns("Data_Liberacao_Engenharia").Visible = False
                DGVListaMaterialSW.Columns("descempresa").Visible = False
                DGVListaMaterialSW.Columns("ProdutoPrincipal").Visible = False
                DGVListaMaterialSW.Columns("RNC").Visible = False
                DGVListaMaterialSW.Columns("Comprimentocaixadelimitadora").Visible = False
                DGVListaMaterialSW.Columns("Larguracaixadelimitadora").Visible = False
                DGVListaMaterialSW.Columns("Espessuracaixadelimitadora").Visible = False
                DGVListaMaterialSW.Columns("txtItemEstoque").Visible = False
                DGVListaMaterialSW.Columns("AreaPinturaUnitario").Visible = False
                DGVListaMaterialSW.Columns("PesoUnitario").Visible = False
                DGVListaMaterialSW.Columns("DataPrevisao").Visible = False
                DGVListaMaterialSW.Columns("EnderecoArquivoItemOrdemServico").Visible = False
                DGVListaMaterialSW.Columns("EnderecoArquivo").Visible = False

                Cursor.Current = Cursors.Default
            End If
        End If

        FormataDGVListaMaterialSW = True


    End Sub

    Private Sub DGVListaMaterialSW_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVListaMaterialSW.CellContentClick

    End Sub

    Private Sub dgvos_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvos.CellContentClick

    End Sub

    Private Sub InserirNumeroOPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InserirNumeroOPToolStripMenuItem.Click


        Dim NumeroOpOmie As String

        ' Exibe o InputBox com valor padrão
        NumeroOpOmie = InputBox("Informe o número da Ordem de Produção do OMIE",
                        "OMIE",
                        Me.dgvos.CurrentRow.Cells("NumeroOpOmie").Value.ToString)

        ' --- Tratamento de saída do InputBox ---
        If NumeroOpOmie Is Nothing Then
            ' Usuário clicou em Cancelar
            MessageBox.Show("Operação cancelada pelo usuário.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub

        ElseIf String.IsNullOrWhiteSpace(NumeroOpOmie) Then
            ' Usuário deixou o campo vazio e apertou OK
            MessageBox.Show("O número da OP do OMIE não pode estar vazio.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        '' --- (Opcional) Validação adicional ---
        '' Se quiser garantir que seja numérico, por exemplo:
        'If Not IsNumeric(NumeroOpOmie) Then
        '    MessageBox.Show("Informe apenas números válidos para a OP do OMIE.", "Erro de entrada", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End If

        ' --- Continua o fluxo normal ---
        Me.dgvos.CurrentRow.Cells("NumeroOpOmie").Value = NumeroOpOmie
        MessageBox.Show("Número da OP do OMIE atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)



        Try


            '  Dim NumeroOpOmie As String
            NumeroOpOmie = InputBox("Informe o Numero da Ordem de Produção do OMIE", "OMIE", Me.dgvos.CurrentRow.Cells("NumeroOpOmie").Value.ToString)


            cl_BancoDados.AlteracaoEspecifica("ordemservico", "NumeroOPOmie", NumeroOpOmie, "IdOrdemServico", OrdemServico.IdOrdemServico)
            cl_BancoDados.AlteracaoEspecifica("ordemservicoITEM", "NumeroOPOmie", NumeroOpOmie, "IdOrdemServico", OrdemServico.IdOrdemServico)


            Me.dgvos.CurrentRow.Cells("NumeroOpOmie").Value = NumeroOpOmie

        Catch ex As Exception
        Finally

        End Try

    End Sub

    Private Sub mnuDGVListaMaterialSW_Opening(sender As Object, e As CancelEventArgs) Handles mnuDGVListaMaterialSW.Opening

    End Sub

    Private Sub InserirMaterialPeloFAPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InserirMaterialPeloFAPToolStripMenuItem.Click

    End Sub

    Private Sub dgvDataGridBOM_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDataGridBOM.CellContentClick

    End Sub

    Private Sub mnudgvos_Opening(sender As Object, e As CancelEventArgs) Handles mnudgvos.Opening

    End Sub

    Private Sub txtPalavraChave_TextChanged(sender As Object, e As EventArgs) Handles txtPalavraChave.TextChanged

    End Sub
End Class

Public Module SwRelink

    ' Exemplo de uso:
    ' Dim ok = RepararReferenciasPorBase(swApp, "C:\pasta\00002.SLDDRW")

    Public Function RepararReferenciasPorBase(swApp As SldWorks, drawingPath As String) As Boolean
        If swApp Is Nothing Then Throw New ArgumentNullException(NameOf(swApp))
        If String.IsNullOrWhiteSpace(drawingPath) OrElse Not File.Exists(drawingPath) Then
            Throw New FileNotFoundException("Desenho não encontrado.", drawingPath)
        End If

        Dim err As Integer = 0, warn As Integer = 0

        ' 1) Abre o .SLDDRW silenciosamente
        Dim swDrw As ModelDoc2 = swApp.OpenDoc6(
            drawingPath,
            CInt(swDocumentTypes_e.swDocDRAWING),
            CInt(swOpenDocOptions_e.swOpenDocOptions_Silent),
            "",
            err,
            warn
        )
        If swDrw Is Nothing Then
            Console.WriteLine("Falha ao abrir: " & drawingPath)
            Return False
        End If

        Try
            Dim pasta As String = Path.GetDirectoryName(drawingPath)
            Dim baseNome As String = Path.GetFileNameWithoutExtension(drawingPath)

            ' 2) Descobre o NOVO alvo (mesmo nome do desenho)
            Dim alvoPrt As String = Path.Combine(pasta, baseNome & ".SLDPRT")
            Dim alvoAsm As String = Path.Combine(pasta, baseNome & ".SLDASM")
            Dim novoAlvo As String = Nothing

            If File.Exists(alvoPrt) Then
                novoAlvo = alvoPrt
            ElseIf File.Exists(alvoAsm) Then
                novoAlvo = alvoAsm
            Else
                novoAlvo = ProcurarEquivalente(pasta, baseNome)
            End If

            If String.IsNullOrEmpty(novoAlvo) Then
                Console.WriteLine("Nenhum .SLDPRT/.SLDASM encontrado para '" & baseNome & "'")
                Return False
            End If

            ' 3) Dependências atuais do desenho
            Dim swExt As ModelDocExtension = swDrw.Extension
            Dim depsObj As Object = swExt.GetDependencies(True, True, False, False, False)

            Dim deps() As String
            If depsObj Is Nothing Then
                deps = New String() {}
            Else
                deps = CType(depsObj, String())
            End If

            ' Filtra apenas peças/conjuntos
            Dim listaFiltrada As New List(Of String)
            For Each p In deps
                If String.IsNullOrWhiteSpace(p) Then Continue For
                Dim e As String = Path.GetExtension(p)
                If e Is Nothing Then Continue For
                e = e.ToUpperInvariant()
                If e = ".SLDPRT" OrElse e = ".SLDASM" Then
                    If Not listaFiltrada.Contains(p, StringComparer.OrdinalIgnoreCase) Then
                        listaFiltrada.Add(p)
                    End If
                End If
            Next

            If listaFiltrada.Count = 0 Then
                Console.WriteLine("Nenhuma referência 3D encontrada.")
                Return False
            End If

            ' 4) (Opcional) abre o alvo — ajuda em algumas versões
            Dim alvoDoc As ModelDoc2 = Nothing
            Dim abrimosAlvo As Boolean = False
            Try
                alvoDoc = swApp.GetOpenDocumentByName(Path.GetFileName(novoAlvo))
                If alvoDoc Is Nothing Then
                    Dim tipoAlvo As Integer
                    If Path.GetExtension(novoAlvo).Equals(".SLDASM", StringComparison.OrdinalIgnoreCase) Then
                        tipoAlvo = CInt(swDocumentTypes_e.swDocASSEMBLY)
                    Else
                        tipoAlvo = CInt(swDocumentTypes_e.swDocPART)
                    End If
                    alvoDoc = swApp.OpenDoc6(
                        novoAlvo,
                        tipoAlvo,
                        CInt(swOpenDocOptions_e.swOpenDocOptions_Silent Or swOpenDocOptions_e.swOpenDocOptions_ReadOnly),
                        "",
                        err,
                        warn
                    )
                    If alvoDoc IsNot Nothing Then abrimosAlvo = True
                End If
            Catch
            End Try

            Dim houveTroca As Boolean = False

            ' 5) Troca cada referência pelo novo alvo
            For Each refAntiga In listaFiltrada
                If String.Equals(refAntiga, novoAlvo, StringComparison.OrdinalIgnoreCase) Then Continue For
                If TrocarReferencia(swExt, refAntiga, novoAlvo) Then
                    houveTroca = True
                Else
                    Console.WriteLine("Aviso: ReplaceReferencedDocument retornou False para: " & refAntiga)
                End If
            Next

            ' 6) Rebuild e salvar
            If houveTroca Then
                Try : swDrw.ForceRebuild3(True) : Catch : End Try
                swDrw.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_Silent), err, warn)
                Console.WriteLine("Referências atualizadas e desenho salvo.")
            Else
                Console.WriteLine("Nenhuma troca necessária.")
            End If

            ' fecha o alvo que abrimos só para a operação
            If abrimosAlvo AndAlso alvoDoc IsNot Nothing Then
                Try : swApp.CloseDoc(alvoDoc.GetTitle()) : Catch : End Try
            End If

            Return houveTroca
        Finally
            Try : swApp.CloseDoc(swDrw.GetTitle()) : Catch : End Try
        End Try
    End Function

    ' ---- Chama ReplaceReferencedDocument(oldPath, newPath) com fallback ----
    Private Function TrocarReferencia(swExt As ModelDocExtension, oldPath As String, newPath As String) As Boolean
        Try
            Dim ok As Boolean = swExt.ReplaceReferencedDocument(oldPath, newPath)
            Console.WriteLine("ReplaceReferencedDocument: " & oldPath & " -> " & newPath & " | OK=" & ok)
            Return ok
        Catch ex As MissingMemberException
            ' fallback para variações late-bound
            Try
                Dim ok2 As Boolean = CBool(CallByName(swExt, "ReplaceReferencedDocument", CallType.Method, oldPath, newPath))
                Console.WriteLine("(CallByName) ReplaceReferencedDocument: " & oldPath & " -> " & newPath & " | OK=" & ok2)
                Return ok2
            Catch ex2 As Exception
                Console.WriteLine("Falha no CallByName: " & ex2.Message)
                Return False
            End Try
        Catch ex As Exception
            Console.WriteLine("Erro em ReplaceReferencedDocument: " & ex.Message)
            Return False
        End Try
    End Function

    ' ---- Busca equivalente por nome base (tolerante a maiúsc/minúsc) ----
    Private Function ProcurarEquivalente(pasta As String, baseNome As String) As String
        If String.IsNullOrWhiteSpace(pasta) OrElse Not Directory.Exists(pasta) Then Return Nothing
        Try
            For Each f In Directory.EnumerateFiles(pasta, "*.sldprt")
                If String.Equals(Path.GetFileNameWithoutExtension(f), baseNome, StringComparison.OrdinalIgnoreCase) Then
                    Return f
                End If
            Next
            For Each f In Directory.EnumerateFiles(pasta, "*.sldasm")
                If String.Equals(Path.GetFileNameWithoutExtension(f), baseNome, StringComparison.OrdinalIgnoreCase) Then
                    Return f
                End If
            Next
        Catch
        End Try
        Return Nothing
    End Function

End Module