Imports System.Windows.Forms

Public Class frmMateriaisProtheus

    Private Sub TimerdgvMaterialProtheus_Tick(sender As Object, e As EventArgs) Handles TimerdgvMaterialProtheus.Tick

        cl_BancoDados.AbrirBancoSqlServerProtheus()

        ' Desabilita o botão ou mostra algum loading se quiser
        ' chkTpoFiltro.Enabled = False
        Dim query As String

        ' Dim filtroTop As String = If(chkTpoFiltro.Checked, "TOP (100)", String.Empty)
        query = "SELECT DISTINCT TOP (500) rtrim([B1_COD]) as B1_COD
      ,[B1_MSBLQL] AS B1_MSBLQL
      ,[B1_ZUSRBLQ] AS ACAO_BLOQUEIO
      ,rtrim([B1_DESC]) as B1_DESC
      ,rtrim([B1_ZREF]) as B1_ZREF
      ,rtrim([B1_POSIPI]) as B1_POSIPI
      ,rtrim([B1_CODITE]) as B1_CODITE
      ,rtrim([B1_CODFABR]) as B1_CODFABR
      ,rtrim([B1_ZFABRIC]) as B1_ZFABRIC
      ,rtrim([B1_TIPO]) as B1_TIPO
      ,rtrim([B1_UM]) as B1_UM
      ,rtrim([B1_PESO]) as B1_PESO
      ,rtrim([B1_IPI]) as B1_IPI
      ,rtrim([B1_PICM]) as B1_PICM
      ,rtrim([B1_UPRC]) as B1_UPRC
      ,rtrim([B1_PRV1]) as B1_PRV1
      FROM [MP12OFICIAL].[dbo].[SB1010]
	  WHERE [MP12OFICIAL].[dbo].[SB1010].D_E_L_E_T_ <> '*' and  B1_ZREF like '%" & Me.txtPesqCodFabricante.Text & "%'" _
 & " and B1_DESC like  '%" & Me.txtPesqDescricao01.Text & "%'" _
 & " and B1_DESC like  '%" & Me.txtPesqDescricao02.Text & "%'" _
 & " and B1_DESC like  '%" & Me.txtPesqDescricao03.Text & "%'" _
 & " and B1_TIPO like '%" & Me.txtPesqTipo.Text & "%'" _
 & " and B1_COD like '%" & Me.txtPesqCodProtheus.Text & "%'" _
 & " and B1_ZFABRIC like '%" & Me.txtPesqFabricante.Text & "%'"
        '& " and B1_GRUPO like '%" & Me.TxtPesqGrupo.Text & "%'"

        dgvMaterialProtheus.DataSource = cl_BancoDados.CarregarDadosSqlServer(query)

        TimerdgvMaterialProtheus.Enabled = False

    End Sub

    Private Sub frmMateriaisProtheusa_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TimerdgvMaterialProtheus.Enabled = True

    End Sub

    Private Sub txtPesqCodProtheus_TextChanged(sender As Object, e As EventArgs) Handles txtPesqCodProtheus.TextChanged
        TimerdgvMaterialProtheus.Enabled = True
    End Sub

    Private Sub txtPesqCodFabricante_TextChanged(sender As Object, e As EventArgs) Handles txtPesqCodFabricante.TextChanged
        TimerdgvMaterialProtheus.Enabled = True
    End Sub

    Private Sub txtPesqDescricao01_TextChanged(sender As Object, e As EventArgs) Handles txtPesqDescricao01.TextChanged
        TimerdgvMaterialProtheus.Enabled = True
    End Sub

    Private Sub txtPesqDescricao02_TextChanged(sender As Object, e As EventArgs) Handles txtPesqDescricao02.TextChanged
        TimerdgvMaterialProtheus.Enabled = True
    End Sub

    Private Sub txtPesqDescricao03_TextChanged(sender As Object, e As EventArgs) Handles txtPesqDescricao03.TextChanged
        TimerdgvMaterialProtheus.Enabled = True
    End Sub

    Private Sub txtPesqFabricante_TextChanged(sender As Object, e As EventArgs) Handles txtPesqFabricante.TextChanged
        TimerdgvMaterialProtheus.Enabled = True
    End Sub

    Private Sub txtPesqTipo_TextChanged(sender As Object, e As EventArgs) Handles txtPesqTipo.TextChanged
        TimerdgvMaterialProtheus.Enabled = True
    End Sub

    Private Sub TxtPesqGrupo_TextChanged(sender As Object, e As EventArgs)
        TimerdgvMaterialProtheus.Enabled = True
    End Sub

    Private Sub dgvMaterialProtheus_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMaterialProtheus.CellContentClick
        If dgvMaterialProtheus.CurrentRow.Cells("dgvSelecao").Value = True Then

            dgvMaterialProtheus.CurrentRow.Cells("dgvSelecao").Value = False
        Else
            dgvMaterialProtheus.CurrentRow.Cells("dgvSelecao").Value = True

        End If

    End Sub

    Private Sub dgvMaterialProtheus_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvMaterialProtheus.DataBindingComplete

        cl_BancoDados.FormatarDataGridView(dgvMaterialProtheus, "SIM")

    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        'cl_BancoDados.AbrirBancoSqlServerProtheus()
        Try

            cl_BancoDados.AbrirBanco()

            For i As Integer = 0 To dgvMaterialProtheus.Rows.Count - 1

                If dgvMaterialProtheus.Rows(i).Cells("dgvSelecao").Value = True Then

                    Dim vlipi, vlicms As Double

                    If Double.TryParse(Me.lblB1_IPI.Text, vlipi) Then
                        vlipi = Convert.ToDouble(dgvMaterialProtheus.Rows(i).Cells("B1_UPRC").Value / 100 * Me.lblB1_IPI.Text)
                    Else
                        vlipi = 0.0
                    End If

                    If Double.TryParse(Me.lblB1_PICM.Text, vlicms) Then
                        vlicms = Convert.ToDouble(dgvMaterialProtheus.Rows(i).Cells("B1_UPRC").Value / 100 * Me.lblB1_PICM.Text)
                    Else
                        vlicms = 0.0
                    End If

                    cl_BancoDados.SalvarDadosMaterial(dgvMaterialProtheus.Rows(i).Cells("B1_DESC").Value.ToString.Trim.ToUpper,
                                                                dgvMaterialProtheus.Rows(i).Cells("B1_PESO").Value,
                                                                dgvMaterialProtheus.Rows(i).Cells("B1_UM").Value.ToString.Trim.ToUpper,
                                                                dgvMaterialProtheus.Rows(i).Cells("B1_ZREF").Value.ToString.Trim.ToUpper,
                                                                dgvMaterialProtheus.Rows(i).Cells("B1_ZFABRIC").Value.ToString.Trim.ToUpper,
                                                                Replace(dgvMaterialProtheus.Rows(i).Cells("B1_UPRC").Value, ".", ","),
                                                                "MATERIAL",
                                                                Replace(dgvMaterialProtheus.Rows(i).Cells("B1_IPI").Value, ".", ","),
                                                                vlipi,'valor ipi
                                                                Replace(dgvMaterialProtheus.Rows(i).Cells("B1_PICM").Value, ".", ","),
                                                                vlicms,
                                                                Replace(dgvMaterialProtheus.Rows(i).Cells("B1_PRV1").Value, ".", ","),
                                                                Nothing, "MATERIAL",
                                                                dgvMaterialProtheus.Rows(i).Cells("B1_COD").Value.ToString.Trim.ToUpper,
                                                                Usuario.NomeCompleto,
                                                                Date.Now.Date.ToString("dd/mm/yyyy"), ' dataatual,
                                                                "", 'txtProdundidadeEmbalagem.Text,
                                                                "", 'txtLarguraEmbalagem.Text,
                                                                "", 'txtAlturaEmbalagem.Text,
                                                                "", 'txtAltura.Text,
                                                                "", 'txtLargura.Text,
                                                                "", 'txtProfundidade.Text,
                                                                "") 'txtPesoMaisEmbalagem.Text)

                    dgvMaterialProtheus.Rows(i).Cells("dgvSelecao").Value = False

                End If
            Next

            cl_BancoDados.FecharBanco()

            MateriaisAlmoxarifado.TimerDgvMaterial.Enabled = True
        Catch ex As Exception
        Finally
            cl_BancoDados.FecharBanco()

        End Try

    End Sub

    Private Sub dgvMaterialProtheus_Click(sender As Object, e As EventArgs) Handles dgvMaterialProtheus.Click

        Me.lblCodProtheus.Text = dgvMaterialProtheus.CurrentRow.Cells("B1_COD").Value.ToString.Trim.ToUpper
        Me.lblB1_ZREF.Text = dgvMaterialProtheus.CurrentRow.Cells("B1_ZREF").Value.ToString.Trim.ToUpper
        Me.lblB1_ZFABRIC.Text = dgvMaterialProtheus.CurrentRow.Cells("B1_ZFABRIC").Value.ToString.Trim.ToUpper
        Me.lblB1_UPRC.Text = Replace(dgvMaterialProtheus.CurrentRow.Cells("B1_UPRC").Value, ".", ",")
        Me.lblB1_UM.Text = dgvMaterialProtheus.CurrentRow.Cells("B1_UM").Value.ToString.Trim.ToUpper
        Me.lblB1_TIPO.Text = dgvMaterialProtheus.CurrentRow.Cells("B1_TIPO").Value.ToString.Trim.ToUpper
        Me.lblB1_PICM.Text = Replace(dgvMaterialProtheus.CurrentRow.Cells("B1_PICM").Value, ".", ",")
        Me.lblB1_IPI.Text = Replace(dgvMaterialProtheus.CurrentRow.Cells("B1_IPI").Value, ".", ",")
        Me.lblB1_DESC.Text = dgvMaterialProtheus.CurrentRow.Cells("B1_DESC").Value.ToString.Trim.ToUpper
        Me.lblB1_UPRC.Text = Replace(dgvMaterialProtheus.CurrentRow.Cells("B1_UPRC").Value, ".", ",")
        Me.lblB1_PESO.Text = Replace(dgvMaterialProtheus.CurrentRow.Cells("B1_PESO").Value, ".", ",")

        Dim vlipi, vlicms As Double

        If Double.TryParse(Me.lblB1_IPI.Text, vlipi) Then
            vlipi = Convert.ToDouble(lblB1_UPRC.Text / 100 * Me.lblB1_IPI.Text)
        Else
            vlipi = 0.0
        End If

        If Double.TryParse(Me.lblB1_PICM.Text, vlicms) Then
            vlicms = Convert.ToDouble(lblB1_UPRC.Text / 100 * Me.lblB1_PICM.Text)
        Else
            vlicms = 0.0
        End If

        Me.lblB1_PICM_Valor.Text = vlicms
        Me.lblB1_IPI_Valor.Text = vlipi

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Hide()
    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Me.txtPesqCodFabricante.Clear()
        txtPesqDescricao01.Clear()
        txtPesqDescricao02.Clear()
        txtPesqDescricao03.Clear()
        txtPesqTipo.Clear()
        txtPesqCodProtheus.Clear()
        txtPesqFabricante.Clear()

    End Sub

End Class