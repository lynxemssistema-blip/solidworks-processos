Imports System.Windows.Forms

Public Class frmInspecaoQualidade

    Private Sub frmInspecaoQualidade_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.lblCodMatFabricante.Text = DadosArquivoCorrente.NomeArquivoSemExtensao
        TimerdgvDados.Enabled = True

    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        If DadosArquivoCorrente.NomeArquivoSemExtensao = "" Then

            MsgBox("Selecione um Desenho!", vbInformation, "Atenção")

            Exit Sub

        End If

        If cl_BancoDados.RetornaCampoDaPesquisa("Select CodMatFabricante from material where CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'", "CodMatFabricante").ToString <> "" Then

            QualidadeSGQ.CodMatFabricante = DadosArquivoCorrente.NomeArquivoSemExtensao
            QualidadeSGQ.NomeCota = Me.CboNomeCota.Text.ToUpper
            QualidadeSGQ.TipoCota = Me.CboTipoCota.Text.ToUpper
            QualidadeSGQ.ValorCota = Me.txtValorCota.Text
            QualidadeSGQ.DataCriacao = Date.Now.Date
            QualidadeSGQ.CriadoPor = Usuario.NomeCompleto
            QualidadeSGQ.Revisao = "0"

            If QualidadeSGQ.idDimencionalReferencia = 0 Then

                QualidadeSGQ.SalvarDados()

                MsgBox("Dados inseridos com sucesso!", vbInformation, "Atenção")
            Else

                QualidadeSGQ.UpdateDados()

                MsgBox("Dados Alterados com sucesso!", vbInformation, "Atenção")

            End If
        Else

            MsgBox("Desenho não Cadastrado!", vbInformation, "Atenção")
            Exit Sub

        End If

        TimerdgvDados.Enabled = True

    End Sub

    Private Sub dgvDados_Click(sender As Object, e As EventArgs) Handles dgvDados.Click

        QualidadeSGQ.idDimencionalReferencia = dgvDados.CurrentRow.Cells("idDimencionalReferencia").Value.ToString
        Me.lblidDimencionalReferencia.Text = QualidadeSGQ.idDimencionalReferencia
        ' Me.lblCodMatFabricante.Text = dgvDados.CurrentRow.Cells("CodMatFabricante").Value.ToString
        Me.CboNomeCota.Text = dgvDados.CurrentRow.Cells("NomeCota").Value.ToString
        Me.CboTipoCota.Text = dgvDados.CurrentRow.Cells("TipoCota").Value.ToString
        Me.txtValorCota.Text = dgvDados.CurrentRow.Cells("ValorCota").Value.ToString

        QualidadeSGQ.Revisao = "0"

    End Sub

    Private Sub TimerdgvDados_Tick(sender As Object, e As EventArgs) Handles TimerdgvDados.Tick

        dgvDados.DataSource = cl_BancoDados.CarregarDados("Select * from dimencionalreferencia where CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'")

        TimerdgvDados.Enabled = False

        cl_BancoDados.FormatarDataGridView(dgvDados, "SIM")

    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click

        LimparCampos()

    End Sub

    Private Sub LimparCampos()

        Me.lblidDimencionalReferencia.Text = ""
        ' Me.lblCodMatFabricante.Text = dgvDados.CurrentRow.Cells("CodMatFabricante").Value.ToString
        Me.CboNomeCota.Text = ""
        ' Me.CboTipoCota.Text = ""
        Me.txtValorCota.Clear()
        QualidadeSGQ.Revisao = "0"

        QualidadeSGQ.idDimencionalReferencia = 0
        QualidadeSGQ.CodMatFabricante = ""
        QualidadeSGQ.NomeCota = ""
        QualidadeSGQ.TipoCota = ""
        QualidadeSGQ.ValorCota = ""
        QualidadeSGQ.DataCriacao = ""
        QualidadeSGQ.CriadoPor = ""
        QualidadeSGQ.Revisao = "0"

    End Sub

    Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click

        Me.Close()

    End Sub

    Private Sub dgvDados_CellContentClick(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles dgvDados.CellContentClick

    End Sub

    Private Sub dgvDados_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvDados.DataBindingComplete
        Try
        Catch ex As Exception
        Finally
        End Try
    End Sub

End Class