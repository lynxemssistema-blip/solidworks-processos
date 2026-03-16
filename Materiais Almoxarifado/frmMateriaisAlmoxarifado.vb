Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class frmMateriaisAlmoxarifado

    Dim QtdeEntrada As Double

    Dim Peso As String = ""
    Dim TotalValor As String = ""
    Dim IdMaterial As Integer
    Dim Valor As String = ""
    Dim Unidade As String = ""
    Dim vICMS As String = ""
    Dim vIPI As String = ""
    Dim PercIPI As String = ""
    Dim PercICMS As String = ""

    ' Verifica se LarguraBlank e ComprimentoBlank são nulos antes de usá-los
    Dim larguraBlank As Double = Replace(DadosArquivoCorrente.LarguraBlank, ".", ",")

    Dim comprimentoBlank As Double = Replace(DadosArquivoCorrente.ComprimentoBlank, ".", ",")
    '  Dim Peso As Double = Replace(DadosArquivoCorrente.Massa, ".", ",")

    Private Sub TimerDgvMaterial_Tick(sender As Object, e As EventArgs) Handles TimerDgvMaterial.Tick

        Dim query As String = "SELECT IdMaterial, CodMatFabricante,NumeroRP,
DescResumo, DescDetal,
Peso, Unidade,
CodigoJuridicoMat,
PercIPI, vIPI,
PercICMS, vICMS,
TotalValor, Unidade,EnderecoImagem
    FROM  " & ComplementoTipoBanco & "material
    WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') AND (PecaManuFat IS NULL or PecaManuFat <> 'S')  
     and (NumeroRP LIKE '%" & txtNumeroEPR.Text & "%')
    and DescDetal LIKE '%" & TxtPesqDesc1.Text & "%'
  AND DescDetal LIKE '%" & TxtPesqDesc2.Text & "%'
  AND DescDetal LIKE '%" & TxtPesqDesc3.Text & "%'
  AND CodigoJuridicoMat LIKE '%" & TxtPesqJuridico.Text & "%'
  AND CodMatFabricante LIKE '%" & TxtPesqCod.Text & "%'
  ORDER BY DescDetal limit 100;"

        dgvMaterial.DataSource = cl_BancoDados.CarregarDados(query)

        TimerDgvMaterial.Enabled = False

        lblTitulo.Text = DadosArquivoCorrente.NomeArquivoSemExtensao

    End Sub

    Dim TaxaUtilizacao As Double = 0

    Private Sub frmMateriaisAlmoxarifado_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        cl_BancoDados.ComboBoxDataSet("medida", "IdMedida", "TipoMedida", Me.cboUnidadeUtilizacao, "", "")

        Me.lblTitulo.BackColor = System.Drawing.ColorTranslator.FromHtml("#32423D")
        TimerDgvMaterial.Enabled = True
        Try

            ' Preenchendo as dimensões da largura e comprimento em metros quadrados
            'Me.txtLarguram2.Text = DadosArquivoCorrente.LarguraBlank
            'Me.txtComprimentom2.Text = DadosArquivoCorrente.ComprimentoBlank
            'Me.txtPesoMaterial.Text = DadosArquivoCorrente.Massa
            'Me.txtEspessura.Text = DadosArquivoCorrente.Espessura

            Try
                ' Largura
                If DadosArquivoCorrente.LarguraBlank IsNot Nothing Then
                    Me.txtLarguram2.Text = DadosArquivoCorrente.LarguraBlank.ToString().Trim()
                Else
                    Me.txtLarguram2.Text = "0"
                End If

                ' Comprimento
                If DadosArquivoCorrente.ComprimentoBlank IsNot Nothing Then
                    Me.txtComprimentom2.Text = DadosArquivoCorrente.ComprimentoBlank.ToString().Trim()
                Else
                    Me.txtComprimentom2.Text = "0"
                End If

                ' Peso
                If DadosArquivoCorrente.Massa IsNot Nothing Then
                    Me.txtPesoMaterial.Text = DadosArquivoCorrente.Massa.ToString().Trim()
                Else
                    Me.txtPesoMaterial.Text = "0"
                End If

                ' Espessura
                If DadosArquivoCorrente.Espessura IsNot Nothing Then
                    Me.txtEspessura.Text = DadosArquivoCorrente.Espessura.ToString().Trim()
                Else
                    Me.txtEspessura.Text = "0"
                End If
            Catch ex As Exception
                MessageBox.Show("Erro ao carregar os dados do arquivo: " & ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Catch ex As Exception
        Finally
        End Try

        TimerMontaPeca.Enabled = True

        If My.Settings.ProgramaRM = "Protheus#" Then

            ProtheusToolStripMenuItem.Enabled = True

        ElseIf My.Settings.ProgramaRM = "Omie#" Then

            OmieToolStripMenuItem.Enabled = True

        ElseIf My.Settings.ProgramaRM = "MegaSenior#" Then

            MegaSeniorToolStripMenuItem.Enabled = True

        ElseIf My.Settings.ProgramaRM = "Lynx#" Then

            MegaSeniorToolStripMenuItem.Enabled = True
            ProtheusToolStripMenuItem.Enabled = True
            OmieToolStripMenuItem.Enabled = True

        End If

    End Sub

    Private Sub TxtPesqCod_TextChanged(sender As Object, e As EventArgs) Handles TxtPesqCod.TextChanged
        TimerDgvMaterial.Enabled = True
    End Sub

    Private Sub TxtPesqDesc1_TextChanged(sender As Object, e As EventArgs) Handles TxtPesqDesc1.TextChanged
        TimerDgvMaterial.Enabled = True
    End Sub

    Private Sub TxtPesqDesc2_TextChanged(sender As Object, e As EventArgs) Handles TxtPesqDesc2.TextChanged
        TimerDgvMaterial.Enabled = True
    End Sub

    Private Sub TxtPesqDesc3_TextChanged(sender As Object, e As EventArgs) Handles TxtPesqDesc3.TextChanged
        TimerDgvMaterial.Enabled = True
    End Sub

    Private Sub TxtPesqJuridico_TextChanged(sender As Object, e As EventArgs) Handles TxtPesqJuridico.TextChanged
        TimerDgvMaterial.Enabled = True
    End Sub

    Private Sub btnAssociarMaterialM2_Click(sender As Object, e As EventArgs) Handles btnAssociarMaterialM2.Click

        If estatus = True Then
            If Double.TryParse(txtFatorUtilizacao.Text, QtdeEntrada) Then

                SalvarMaterial()

                TimerMontaPeca.Enabled = True

                estatus = False

            End If
        Else

            MsgBox("O material não esta pronto para ser adicionado no produto", vbCritical, "Atenção")

        End If

    End Sub

    Private Function SalvarMaterial()

        Try

            If DadosArquivoCorrente.NomeArquivoSemExtensao <> "" Or IdMaterial > 0 Then

                Dim dt As DataTable

                dt = cl_BancoDados.CarregarDados("Select CodMatFabricante, IdMontaPeca, idmaterialpeca, DescDetal,
                                                  IdMaterial, DescDetal,
                                                  DescFamilia, CodMatFabricante, CodigoJuridicoMat,
                                                  d_e_l_e_t_e, Valor, pecaqtde, Peso
                                                  From  " & ComplementoTipoBanco & "viewmontapeca1
                                                  Where CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao.Trim & "'
                                                  And (d_e_l_e_t_e IS NOT NULL or d_e_l_e_t_e is null)
                                                  order by DescDetal")

                For i As Integer = 0 To dt.Rows.Count - 1

                    If dt.Rows(i)("IdMaterial") = IdMaterial Then

                        MsgBox("Este material já esta lançado para este desenho, você podera excluir a lançar a nova quantidade!", vbInformation, "Atenção")

                        Exit Function

                    End If

                Next

                DadosArquivoCorrente.SalvarMaterialDesenho(DadosArquivoCorrente.NomeArquivoSemExtensao.Trim, "0",
                                                     IdMaterial,
                                                     QtdeEntrada.ToString.Replace(",", "."),
                                                     DadosArquivoCorrente.IdMaterial,
                                                     Peso.ToString.Replace(",", "."),
                                                     TotalValor.ToString.Replace(",", "."),
                                                     Usuario.NomeCompleto,
                                                     Date.Now,
                                                     Me.lblvICMSCalculado.Text.ToString.Replace(",", "."),
                                                     Me.lblvIPICalculado.Text.ToString.Replace(",", "."),
                                                     Me.lblPercIPICalculado.Text.ToString.Replace(",", "."),
                                                     Me.lblPercICMSCalculado.Text.ToString.Replace(",", "."),
                                                     Me.cboUnidadeUtilizacao.Text)

                MsgBox("material Inserido com Sucesso!", vbInformation, "Salvamento com Sucesso!")

                MyTaskPanelHost.TimerMontaPeca.Enabled = True

            End If
        Catch ex As Exception
        Finally

        End Try

    End Function

    Private Sub CalcularMaterial()

        If DadosArquivoCorrente.IdMaterial <> 0 Then

            Try
                If dgvMaterial.CurrentRow IsNot Nothing Then
                    IdMaterial = If(IsDBNull(dgvMaterial.CurrentRow.Cells("IdMaterial").Value), 0, Convert.ToInt32(dgvMaterial.CurrentRow.Cells("IdMaterial").Value))
                    TotalValor = If(IsDBNull(dgvMaterial.CurrentRow.Cells("TotalValor").Value), 0, Convert.ToDouble(dgvMaterial.CurrentRow.Cells("TotalValor").Value))
                    Peso = If(IsDBNull(dgvMaterial.CurrentRow.Cells("Peso").Value), 0, Convert.ToDouble(dgvMaterial.CurrentRow.Cells("Peso").Value))
                    Valor = If(IsDBNull(dgvMaterial.CurrentRow.Cells("TotalValor").Value), 0, Convert.ToDouble(dgvMaterial.CurrentRow.Cells("TotalValor").Value))
                    'Unidade =ells("Unidade").Value.ToString())

                    Try

                        Try

                            PictureBox1.ImageLocation = My.Settings.EnderecoImagens & IdMaterial & ".png"
                            PictureBox1.Refresh()
                        Catch ex As Exception

                            PictureBox1.ImageLocation = My.Settings.EnderecoImagens & IdMaterial & ".jpeg"
                            PictureBox1.Refresh()

                        End Try
                    Catch ex As Exception
                        PictureBox1.ImageLocation = ""
                    Finally
                    End Try
                Else
                    PictureBox1.ImageLocation = ""
                    PictureBox1.Refresh()
                    IdMaterial = 0
                    TotalValor = 0
                    Peso = 0
                    Valor = 0
                    Unidade = ""
                End If
            Catch ex As Exception
                MessageBox.Show("Erro ao obter dados do material: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                IdMaterial = 0
                TotalValor = 0
                Peso = 0
                Valor = 0
                Unidade = ""
            End Try

            ' Me.txtPesoMaterial.Text = Peso
            'Me.txtValorMaterial.Text = TotalValor

            Try

                txtValorCalculado.Text = (TotalValor * TaxaUtilizacao).ToString("F2")
                txtPesoCalculado.Text = (Peso * TaxaUtilizacao).ToString("F2")
                ' txtUnidade.Text = Unidade

                TotalValor = txtValorCalculado.Text
                Peso = txtPesoCalculado.Text
                '  Unidade = txtUnidade.Text
            Catch ex As Exception

                txtValorCalculado.Text = ""
            Finally

            End Try
        Else
            ' Caso o valor não seja um número válido, QtdeEntrada permanece 0
            MsgBox("Valor inválido. Por favor, insira um número válido.", vbExclamation, "Atenção")

        End If
    End Sub

    Private Sub dgvMaterial_Click(sender As Object, e As EventArgs) Handles dgvMaterial.Click
        Try

            Try
                IdMaterial = dgvMaterial.CurrentRow.Cells("IdMAterial").Value.ToString()
            Catch ex As Exception
                IdMaterial = 0
            Finally

            End Try

            Try

                PictureBox1.Image = Image.FromFile(dgvMaterial.CurrentRow.Cells("EnderecoImagem").Value.ToString())
            Catch ax As Exception
                PictureBox1.Image = Nothing
                PictureBox1.Refresh()
            Finally
            End Try

            Me.lblDescDetal.Text = If(IsDBNull(dgvMaterial.CurrentRow.Cells("DescDetal").Value.ToString), "", dgvMaterial.CurrentRow.Cells("DescDetal").Value.ToString())
            Me.lblCodMatFabricante.Text = If(IsDBNull(dgvMaterial.CurrentRow.Cells("CodMatFabricante").Value.ToString), "", dgvMaterial.CurrentRow.Cells("CodMatFabricante").Value.ToString())
            Me.lblCodigoJuridicoMat.Text = If(IsDBNull(dgvMaterial.CurrentRow.Cells("CodigoJuridicoMat").Value.ToString), "0", dgvMaterial.CurrentRow.Cells("CodigoJuridicoMat").Value.ToString())
            Me.lblPeso.Text = If(IsDBNull(dgvMaterial.CurrentRow.Cells("Peso").Value.ToString), 0, dgvMaterial.CurrentRow.Cells("Peso").Value.ToString)
            Me.lblNumeroRP.Text = If(IsDBNull(dgvMaterial.CurrentRow.Cells("NumeroRP").Value.ToString), "", dgvMaterial.CurrentRow.Cells("NumeroRP").Value.ToString())
            Me.lblUnidade.Text = If(IsDBNull(dgvMaterial.CurrentRow.Cells("Unidade").Value.ToString), "", dgvMaterial.CurrentRow.Cells("Unidade").Value.ToString())
            Me.lblVALOR.Text = If(IsDBNull(dgvMaterial.CurrentRow.Cells("TotalValor").Value.ToString), 0, dgvMaterial.CurrentRow.Cells("TotalValor").Value.ToString())
            Me.lblvICMS.Text = If(IsDBNull(dgvMaterial.CurrentRow.Cells("vICMS").Value.ToString), 0, dgvMaterial.CurrentRow.Cells("vICMS").Value.ToString())
            Me.lblvIPI.Text = If(IsDBNull(dgvMaterial.CurrentRow.Cells("vIPI").Value.ToString), 0, dgvMaterial.CurrentRow.Cells("vIPI").Value.ToString())
            Me.lblPercIPI.Text = If(IsDBNull(dgvMaterial.CurrentRow.Cells("PercIPI").Value.ToString), 0, dgvMaterial.CurrentRow.Cells("PercIPI").Value.ToString())
            Me.lblPercICMS.Text = If(IsDBNull(dgvMaterial.CurrentRow.Cells("PercICMS").Value.ToString), 0, dgvMaterial.CurrentRow.Cells("PercICMS").Value.ToString())

            cboUnidadeUtilizacao.Text = If(IsDBNull(dgvMaterial.CurrentRow.Cells("Unidade").Value.ToString), "", dgvMaterial.CurrentRow.Cells("Unidade").Value.ToString())

            ' TotalValor As Double = 0


            Try
                Valor = Convert.ToDouble(Me.lblVALOR.Text.Replace(".", ","))
            Catch ex As Exception
                Valor = 0
            End Try

            Try
                vICMS = Convert.ToDouble(lblvICMS.Text.Replace(".", ","))
            Catch ex As Exception
                vICMS = 0
            End Try


            Try
                vIPI = Convert.ToDouble(lblvIPI.Text.Replace(".", ","))
            Catch ex As Exception
                vIPI = 0
            End Try


            Try
                PercIPI = Convert.ToDouble(lblPercIPI.Text.Replace(".", ","))
            Catch ex As Exception
                PercIPI = 0
            End Try


            Try
                PercICMS = Convert.ToDouble(lblPercICMS.Text.Replace(".", ","))
            Catch ex As Exception
                PercICMS = 0
            End Try


            Try
                Peso = Convert.ToDouble(Me.lblPeso.Text.Replace(".", ","))
            Catch ex As Exception
                Peso = 0
            End Try



        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub frmMateriaisAlmoxarifado_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        ' Obter o documento ativo
        swModel = swapp.ActiveDoc
        DadosArquivoCorrente.EnderecoArquivo = swModel.GetPathName().ToUpper()
        DadosArquivoCorrente.NomeArquivoComExtensao = Path.GetFileName(DadosArquivoCorrente.EnderecoArquivo).ToUpper()
        DadosArquivoCorrente.NomeArquivoSemExtensao = Path.GetFileNameWithoutExtension(DadosArquivoCorrente.EnderecoArquivo)

        MyTaskPanelHost.TimerMontaPeca.Enabled = True

    End Sub

    Private Sub dgvMaterial_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvMaterial.DataError

        Try
        Catch ex As Exception
        Finally

        End Try
    End Sub

    Private Sub frmMateriaisAlmoxarifado_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        cl_BancoDados.SaveColumnOrder(dgvMaterial)

    End Sub

    'Private Sub btnCalcular_Click(sender As Object, e As EventArgs) Handles btnCalculadora.Click

    '    Try
    '        Process.Start("calc.exe")
    '    Catch ex As Exception
    '        MessageBox.Show("Erro ao abrir a calculadora: " & ex.Message)
    '    End Try

    'End Sub

    Private Sub TimerMontaPeca_Tick(sender As Object, e As EventArgs) Handles TimerMontaPeca.Tick

        DGVMontaPeca.DataSource = cl_BancoDados.CarregarDados("Select IdMontaPeca, 
NomeArquivoSemExtensao,
NumeroRP, 
DescDetal,
Unidade, 
Peso, 
PecaQtde, 
Valor,
PERCipicalculado AS ICMS,
vicmscalculado as Rlv_ICMS, 
vipicalculado as Rls_IPI,
PERCicmscalculado AS IPI 
From  " & ComplementoTipoBanco & " viewmontapeca
Where NomeArquivoSemExtensao = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'
And (D_E_L_E_T_E = '' or D_E_L_E_T_E is null)
order by DescDetal")

        cl_BancoDados.RetornaCampoDaPesquisa("Select NomeArquivoSemExtensao,
sum(Peso) As Peso,
sum(PecaQtde) As PecaQtde,
sum(Valor) As Valor,
sum(vicmscalculado) As vicmscalculado,
sum(vipicalculado) As vipicalculado,
PERCicmscalculado,
PERCipicalculado
From  viewmontapeca
Where NomeArquivoSemExtensao = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'
And (D_E_L_E_T_E = '' or D_E_L_E_T_E is null) Group By NomeArquivoSemExtensao",
                                             "NomeArquivoSemExtensao",'0v
                                             "Peso", '1v
                                             "PecaQtde", 'v2
                                             "Valor", 'v3
                                             "vicmscalculado", 'v4
                                             "vipicalculado", 'v5
                                             "PERCicmscalculado", 'v6
                                             "PERCipicalculado") 'v7

        lblPesoCalculadoTotal.Text = VCampo1
        lblVALORCalculadoTotal.Text = VCampo3
        lblvICMSCalculadoTotal.Text = VCampo4
        lblvIPICalculadoTotal.Text = VCampo5

        TimerMontaPeca.Enabled = False

    End Sub

    Private Sub DGVMontaPeca_Click(sender As Object, e As EventArgs) Handles DGVMontaPeca.Click
        'IdMontaPeca = DGVMontaPeca.CurrentRow.Cells("IdMontaPeca").Value

        ' Verifica se a linha atual não é nula e se a célula "IdMontaPeca" contém um valor válido
        If DGVMontaPeca.CurrentRow IsNot Nothing AndAlso
           DGVMontaPeca.CurrentRow.Cells("IdMontaPeca").Value IsNot Nothing Then

            ' Tenta converter o valor da célula para o tipo esperado (por exemplo, Integer)
            Dim valorComoString As String = DGVMontaPeca.CurrentRow.Cells("IdMontaPeca").Value.ToString()
            ' Dim idMontaPeca As Integer

            If Integer.TryParse(valorComoString, IdMontaPeca) Then
                IdMontaPeca = IdMontaPeca
            Else
                ' Trate o caso onde a conversão falha, se necessário
                MessageBox.Show("O valor da célula não é um número válido.")
            End If
            'Else
            '    ' Trate o caso onde a célula ou a linha atual são nulas
            '    MessageBox.Show("Nenhuma linha selecionada ou célula 'IdMontaPeca' está vazia.")

        End If

    End Sub

    Private Sub DGVMontaPeca_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DGVMontaPeca.DataBindingComplete

        cl_BancoDados.FormatarDataGridView(DGVMontaPeca, "SIM")

    End Sub

    Private Sub ProtheusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProtheusToolStripMenuItem.Click

        If cl_BancoDados.AbrirBancoSqlServerProtheus() = True Then

            MaterialProtheus.ShowDialog()
        Else

            MsgBox("Não há acesso ao banco de dados Totvs")

        End If

    End Sub

    Private Sub dgvMaterial_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvMaterial.DataBindingComplete

        cl_BancoDados.FormatarDataGridView(dgvMaterial, "SIM")

    End Sub

    Private Async Sub OmieToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OmieToolStripMenuItem.Click

        MaterialOmie.ShowDialog()

    End Sub

    Private Sub MegaSeniorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MegaSeniorToolStripMenuItem.Click

        MsgBox("Não há acesso ao banco de dados Mega - Senior")

    End Sub

    Async Function PesquisarImagemGoogleAsync(searchTerm As String) As Task(Of String)
        Dim url As String = "https://www.google.com/search?q=" & Uri.EscapeDataString(searchTerm) & "&tbm=isch"
        Dim html As String = String.Empty

        Try
            Using webClient As New WebClient()
                webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36")
                html = Await webClient.DownloadStringTaskAsync(url)
            End Using

            ' Extrai o URL da imagem usando uma expressão regular (FRÁGIL!)
            Dim match As Match = Regex.Match(html, "<img.*?src=""(data:image.+?,.+?|http.+?)""", RegexOptions.IgnoreCase)

            If match.Success Then
                Return match.Groups(1).Value
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            MessageBox.Show("Erro na pesquisa da imagem: " & ex.Message)
            Return String.Empty
        End Try
    End Function

    Private Async Sub BuscarImagensNoGoogleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BuscarImagensNoGoogleToolStripMenuItem.Click

        Dim searchTerm As String = dgvMaterial.CurrentRow.Cells("DescDetal").Value.ToString
        Dim imageUrl As String = Await PesquisarImagemGoogleAsync(searchTerm)

        If Not String.IsNullOrEmpty(imageUrl) Then
            Try
                ' Carrega a imagem usando um WebClient
                Using webClient As New WebClient()
                    PictureBox1.ImageLocation = imageUrl
                End Using
            Catch ex As Exception
                MessageBox.Show("Erro ao carregar a imagem: " & ex.Message)
            End Try
        Else
            MessageBox.Show("Nenhuma imagem encontrada para o termo de pesquisa.")
        End If

    End Sub

    Private Sub SalvarAImagemComoReferenciaDoMaterialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalvarAImagemComoReferenciaDoMaterialToolStripMenuItem.Click

        Dim caminhoDestino As String

        '  If PictureBox1.Image IsNot Nothing Then
        Try
            caminhoDestino = IO.Path.Combine(My.Settings.EnderecoImagens, IdMaterial & ".jpg")
            PictureBox1.Image.Save(caminhoDestino, Imaging.ImageFormat.Jpeg)
            '  MessageBox.Show("Imagem salva com sucesso em: " & caminhoDestino, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ' MessageBox.Show("Erro ao salvar a imagem: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        'End If

        cl_BancoDados.AlteracaoEspecifica("material", "EnderecoImagem", caminhoDestino, "IdMAterial", IdMaterial)

        dgvMaterial.CurrentRow.Cells("EnderecoImagem").Value = caminhoDestino

    End Sub

    Private Sub BuscarImagemNaBibliotecaDoSistemaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BuscarImagemNaBibliotecaDoSistemaToolStripMenuItem.Click

        Dim openDialog As New OpenFileDialog()
        openDialog.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
        openDialog.Title = "Selecione uma imagem"
        openDialog.InitialDirectory = My.Settings.EnderecoImagens '"C:\MinhasImagens"

        If openDialog.ShowDialog() = DialogResult.OK Then
            Dim caminhoImagem As String = openDialog.FileName

            ' Exibe a imagem no PictureBox
            PictureBox1.Image = Image.FromFile(caminhoImagem)
        End If
    End Sub

    Private Sub ExcluirALinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcluirALinhaSelecionadaToolStripMenuItem.Click

        Dim IdMontaPeca As String

        Try

            IdMontaPeca = DGVMontaPeca.CurrentRow.Cells("IdMontaPeca").Value.ToString
        Catch ex As Exception
            IdMontaPeca = 0
        Finally

        End Try

        If IdMontaPeca = 0 Then

            Exit Sub

        End If


        Dim NumeroRP As String

        Try

            NumeroRP = DGVMontaPeca.CurrentRow.Cells("NumeroRP").Value.ToString & "-" & DGVMontaPeca.CurrentRow.Cells("DescDetal").Value.ToString
        Catch ex As Exception
            NumeroRP = ""
        Finally

        End Try

        If NumeroRP = "" Then

            Exit Sub

        End If


        Dim result As DialogResult = MessageBox.Show("Você está removendo do Desenho: " &
                                                     DadosArquivoCorrente.NomeArquivoSemExtensao & " o material: " & NumeroRP, "Exclusão de Material", MessageBoxButtons.YesNo)

        Dim totalExecutado As Integer

        If result = DialogResult.Yes Then

            Dim sql As String = "DELETE FROM montapeca WHERE idMontaPeca = @id"

            Dim parametros As New List(Of MySqlParameter) From {
              New MySqlParameter("@id", IdMontaPeca)
                }

            cl_BancoDados.SalvarParametros(sql, parametros)


            TimerMontaPeca.Enabled = True


        ElseIf result = DialogResult.No Then

            MsgBox("Operação Cancelada", vbCritical, "Atenção")

        End If

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click

        Dim IdMaterial, CdoMatFabricante As String

        Try

            IdMaterial = DGVMontaPeca.CurrentRow.Cells("IdMaterial").Value.ToString
        Catch ex As Exception
            IdMaterial = 0
        Finally

        End Try

        If IdMaterial = 0 Then

            Exit Sub

        End If

        Try

            CdoMatFabricante = DGVMontaPeca.CurrentRow.Cells("CdoMatFabricante").Value.ToString
        Catch ex As Exception
            CdoMatFabricante = 0
        Finally

        End Try

        If CdoMatFabricante = 0 Then

            Exit Sub

        End If



        Dim result As DialogResult = MessageBox.Show("Deseja Realmente Excluir o material: " & CdoMatFabricante, "Exclusão de Material", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then

            cl_BancoDados.AlteracaoEspecificaDelete("material", "IdMaterial", IdMaterial)
            ' cl_BancoDados.AlteracaoEspecifica("material", "UsuarioD_E_L_E_T_E", Usuario.NomeCompleto, "IdMaterial", IdMaterial)
            ' cl_BancoDados.AlteracaoEspecifica("material", "DataD_E_L_E_T_E", Date.Now.Date, "IdMaterial", IdMaterial)

            TimerMontaPeca.Enabled = True

        ElseIf result = DialogResult.No Then

            MsgBox("Operação Cancelada", vbCritical, "Atenção")

        End If

    End Sub



    Private Sub btnCalcular_Click_1(sender As Object, e As EventArgs) Handles btnCalcular.Click

        ' Validação da unidade de utilização
        If String.IsNullOrWhiteSpace(cboUnidadeUtilizacao.Text) Then
            MsgBox("Selecione uma unidade de utilização.", vbExclamation, "Atenção")
            Exit Sub
        End If

        Dim fatorUtilizacao As Double

        Try
            fatorUtilizacao = txtFatorUtilizacao.Text.Replace(".", ",").Trim()

            fatorUtilizacao = Math.Round(fatorUtilizacao, 3)


        Catch ex As Exception
            fatorUtilizacao = 0
        End Try



        Try
            ' Aplicando fator validado
            ' TaxaUtilizacao = fatorUtilizacao
            '   QtdeEntrada = fatorUtilizacao

            ' Cálculos principais
            Dim valorCalculado As Double = CDbl((Valor.Replace(".", ","))) * CDbl(fatorUtilizacao)
            Dim icmsCalculado As Double = CDbl((vICMS.Replace(".", ","))) * CDbl(fatorUtilizacao)
            Dim ipiCalculado As Double = CDbl((vIPI.Replace(".", ","))) * CDbl(fatorUtilizacao)

            ' Exibição formatada dos valores
            lblVALORCalculado.Text = valorCalculado
            TotalValor = valorCalculado

            lblvICMSCalculado.Text = icmsCalculado
            lblvIPICalculado.Text = ipiCalculado
            lblPercIPICalculado.Text = PercIPI
            lblPercICMSCalculado.Text = PercICMS

            ' Cálculo do peso baseado na unidade
            If cboUnidadeUtilizacao.Text = "KG" Then
                lblPesoCalculado.Text = fatorUtilizacao

                Me.lblQtde.Text = fatorUtilizacao
            Else
                Dim pesoCalculado As Double

                Try
                    pesoCalculado = CDbl(Peso.Replace(".", ",")) * CDbl(fatorUtilizacao.ToString.Replace(".", ","))

                    lblPesoCalculado.Text = pesoCalculado
                    Peso = pesoCalculado
                    Me.lblQtde.Text = fatorUtilizacao
                Catch ex As Exception
                    fatorUtilizacao = 0
                    Peso = 0
                    pesoCalculado = 0
                End Try

            End If

            estatus = True ' Finaliza com sucesso
        Catch ex As Exception
            MsgBox("Erro durante o cálculo. Verifique os dados inseridos e tente novamente.", vbCritical, "Erro")
        End Try
    End Sub




    Private Sub btnCalculaPeso_Click(sender As Object, e As EventArgs) Handles btnCalculaPeso.Click


        Try


            Dim V_espessura, V_Largura, V_Comprimento, V_Peso, Peso As Double

            V_espessura = CDbl(txtEspessura.Text.Replace(".", ","))
            V_Largura = CDbl(txtLarguram2.Text.Replace(".", ","))
            V_Comprimento = CDbl(txtComprimentom2.Text.Replace(".", ","))
            V_Peso = CDbl(0.00000793)

            Peso = (V_espessura * V_Largura * V_Comprimento * V_Peso)
            Peso = Math.Round(Peso, 3)

            txtFatorUtilizacao.Text = Peso

        Catch ex As Exception
        Finally
        End Try


        '' Verifica se LarguraBlank e ComprimentoBlank são nulos antes de usá-los
        'Dim larguraBlank As Double = Replace(DadosArquivoCorrente.LarguraBlank, ".", ",")
        'Dim comprimentoBlank As Double = Replace(DadosArquivoCorrente.ComprimentoBlank, ".", ",")
        'Dim Peso As Double = Replace(DadosArquivoCorrente.Massa, ".", ",")

    End Sub


    Private Sub btnatualizar_Click(sender As Object, e As EventArgs) Handles btnatualizar.Click
        TimerMontaPeca.Enabled = True
    End Sub
    Private Sub txtNumeroEPR_TextChanged(sender As Object, e As EventArgs) Handles txtNumeroEPR.TextChanged
        TimerDgvMaterial.Enabled = True
    End Sub

End Class