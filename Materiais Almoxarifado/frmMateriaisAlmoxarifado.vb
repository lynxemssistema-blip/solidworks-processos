Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports System.Windows.Forms.DataVisualization.Charting

Public Class frmMateriaisAlmoxarifado

    Dim QtdeEntrada As Double = 0

    Dim Peso As Double = 0
    Dim TotalValor As Double = 0
    Dim IdMaterial As Integer = 0
    Dim Valor As Double = 0
    Dim Unidade As String = ""

    Private Sub TimerDgvMaterial_Tick(sender As Object, e As EventArgs) Handles TimerDgvMaterial.Tick

        Dim query As String = " SELECT IdMaterial, CodMatFabricante,
DescResumo, DescDetal, 
Peso, Unidade, 
CodigoJuridicoMat, 
PercIPI, vIPI, 
PercICMS, vICMS, 
TotalValor, Unidade
    FROM  " & ComplementoTipoBanco & "material
    WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') AND (PecaManuFat IS NULL or PecaManuFat <> 'S')
    and DescDetal LIKE '%" & TxtPesqDesc1.Text & "%'
  AND DescDetal LIKE '%" & TxtPesqDesc2.Text & "%'
  AND DescDetal LIKE '%" & TxtPesqDesc3.Text & "%'
  AND CodigoJuridicoMat LIKE '%" & TxtPesqJuridico.Text & "%'
  AND CodMatFabricante LIKE '%" & TxtPesqCod.Text & "%'
  ORDER BY DescDetal limit 500;"

        dgvMaterial.DataSource = cl_BancoDados.CarregarDados(query)



        TimerDgvMaterial.Enabled = False



    End Sub


    Dim TaxaUtilizacao As Double = 0

    Private Sub frmMateriaisAlmoxarifado_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        TimerDgvMaterial.Enabled = True

        If dgvMaterial.Rows.Count < 0 Then

            MsgBox("Não há materiais na lista de material para ser selecionado", vbInformation, "Atenção")
        Else
            ' Verifica se LarguraBlank e ComprimentoBlank são nulos antes de usá-los
            Dim larguraBlank As Double = Replace(DadosArquivoCorrente.LarguraBlank, ".", ",")
            Dim comprimentoBlank As Double = Replace(DadosArquivoCorrente.ComprimentoBlank, ".", ",")
            Dim Peso As Double = Replace(DadosArquivoCorrente.Massa, ".", ",")

            Dim AreaCalculadaBlank As Double = 0

            Dim AreaChapaM As Double = 1.2 * 3 * 0.95

            ' Preenchendo as dimensões da largura e comprimento em metros quadrados
            Me.txtLarguram2.Text = DadosArquivoCorrente.LarguraBlank
            Me.txtComprimentom2.Text = DadosArquivoCorrente.ComprimentoBlank
            Me.txtPesoMaterial.Text = DadosArquivoCorrente.Massa


            ' Calculando o valor total de m² e verificando se os valores são válidos
            ' Calculando o valor total de m² e verificando se os valores são válidos
            If larguraBlank > 0 And comprimentoBlank > 0 Then
                AreaCalculadaBlank = (larguraBlank / 1000) * (comprimentoBlank / 1000)

                TaxaUtilizacao = AreaCalculadaBlank / AreaChapaM

                txtm2Total.Text = TaxaUtilizacao.ToString("F2")

            Else

                ' Calculando o comprimento em metros lineares e verificando se o comprimento é válido
                If comprimentoBlank > 0 Then
                    Me.txtComprimentom2.Text = (comprimentoBlank / 1000).ToString("F2")
                    txtm2Total.Text = (comprimentoBlank / 1000).ToString("F2")
                Else
                    Me.txtComprimentom2.Text = "0.00"

                End If

            End If

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

        If Double.TryParse(txtm2Total.Text, QtdeEntrada) Then


            SalvarMaterial()

        End If



    End Sub

    Private Function SalvarMaterial()

        Try

            If (DadosArquivoCorrente.IdMaterial <> 0) And (IdMaterial > 0) Then

                Dim dt As DataTable

                dt = cl_BancoDados.CarregarDados("Select CodMatFabricante, IdMontaPeca, idmaterialpeca, descdetal,
IdMaterial, DescDetal,
DescFamilia, CodMatFabricante, CodigoJuridicoMat,
d_e_l_e_t_e, Valor, pecaqtde, Peso
From  " & ComplementoTipoBanco & "viewmontapeca1
Where CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao.Trim & "'
And (d_e_l_e_t_e IS NOT NULL or d_e_l_e_t_e is null)
order by descdetal")

                For i As Integer = 0 To dt.Rows.Count - 1

                    If dt.Rows(i)("IdMaterial") = IdMaterial Then

                        MsgBox("Este material já esta lançado para este desenho, você podera excluir a lançar a nova quantidade!", vbInformation, "Atenção")

                        Exit Function

                    End If

                Next






                DadosArquivoCorrente.SalvarMaterialDesenho(DadosArquivoCorrente.NomeArquivoSemExtensao.Trim, "0",
                                                     IdMaterial,
                                                     QtdeEntrada,
                                                     DadosArquivoCorrente.IdMaterial,
                                                     Peso,
                                                     TotalValor,
                                                     Usuario.NomeCompleto,
                                                     Date.Now)

                    'cl_BancoDados.Salvar("insert into montapeca (CodMatFabricante,TipoPeca,IdMaterial, PecaQtde, IdMaterialPeca, Peso, Valor, UsuarioCriacao, DataCriacao)
                    '                                                         values ('" & DadosArquivoCorrente.NomeArquivoSemExtensao.Trim & "','0','" & IdMaterial & "','" _
                    '                                                                     & Replace(QtdeEntrada, ",", ".") & "','" _
                    '                                                                     & DadosArquivoCorrente.IdMaterial & "','" & Replace(Peso, ",", ".") & "','" _
                    '                                                                     & Replace(TotalValor, ",", ".") & "','" _
                    '                                                                     & Usuario.NomeCompleto & "','" & Date.Now & "')")


                    MsgBox("material Inserido com Sucesso!", vbInformation, "Salvamento com Sucesso!")

                    MyTaskPanelHost.TimerMontaPeca.Enabled = True


                End If

        Catch ex As Exception

        Finally

        End Try

    End Function

    Private Sub dgvMaterial_Click(sender As Object, e As EventArgs) Handles dgvMaterial.Click

        If DadosArquivoCorrente.IdMaterial <> 0 Then

            Try
                If dgvMaterial.CurrentRow IsNot Nothing Then
                    IdMaterial = If(IsDBNull(dgvMaterial.CurrentRow.Cells("IdMaterial").Value), 0, Convert.ToInt32(dgvMaterial.CurrentRow.Cells("IdMaterial").Value))
                    TotalValor = If(IsDBNull(dgvMaterial.CurrentRow.Cells("TotalValor").Value), 0, Convert.ToDouble(dgvMaterial.CurrentRow.Cells("TotalValor").Value))
                    Peso = If(IsDBNull(dgvMaterial.CurrentRow.Cells("Peso").Value), 0, Convert.ToDouble(dgvMaterial.CurrentRow.Cells("Peso").Value))
                    Valor = If(IsDBNull(dgvMaterial.CurrentRow.Cells("TotalValor").Value), 0, Convert.ToDouble(dgvMaterial.CurrentRow.Cells("TotalValor").Value))
                    Unidade = If(dgvMaterial.CurrentRow.Cells("Unidade").Value Is Nothing OrElse IsDBNull(dgvMaterial.CurrentRow.Cells("Unidade").Value), "", dgvMaterial.CurrentRow.Cells("Unidade").Value.ToString())
                Else
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


            Me.txtPesoMaterial.Text = Peso
            Me.txtValorMaterial.Text = TotalValor

            Try


                txtValorCalculado.Text = (TotalValor * TaxaUtilizacao).ToString("F2")
                txtPesoCalculado.Text = (Peso * TaxaUtilizacao).ToString("F2")
                txtUnidade.Text = Unidade

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

    Private Sub dgvMaterial_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMaterial.CellContentClick

    End Sub
End Class