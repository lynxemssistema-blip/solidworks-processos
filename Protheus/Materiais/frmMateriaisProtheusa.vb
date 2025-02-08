
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms


Public Class frmMateriaisProtheusa

    Private Sub TimerdgvMaterialProtheus_Tick(sender As Object, e As EventArgs) Handles TimerdgvMaterialProtheus.Tick


        TipoBanco = "SQL"


        Dim TopFiltro As String = "TOP 500"

        If BancoProtheus.AbriBanco = False Then

            BancoProtheus.AbriBanco()

        End If

        dgvMaterialProtheus.DataSource = cl_BancoDados.CarregarDados("SELECT DISTINCT " & TopFiltro & " rtrim([B1_COD]) as B1_COD
      ,[B1_MSBLQL] AS B1_MSBLQL
      ,[B1_ZUSRBLQ] AS ACAO_BLOQUEIO
      ,rtrim([B1_DESC]) as B1_DESC
      ,rtrim([B1_ZREF]) as B1_ZREF
      ,rtrim([B1_CODFABR]) as B1_CODFABR
      ,rtrim([B1_ZFABRIC]) as B1_ZFABRIC
      ,rtrim([B1_UM]) as B1_UM
      ,rtrim([B1_PRV1]) as B1_PRV1
      FROM [MP12OFICIAL].[dbo].[SB1010] 
	  WHERE [MP12OFICIAL].[dbo].[SB1010].D_E_L_E_T_ <> '*' and  B1_ZREF like '%" & Me.txtFabricante.Text & "%'" _
 & " and B1_DESC like  '%" & Me.txtDescricao1.Text & "%'" _
 & " and B1_DESC like  '%" & Me.txtDescricao2.Text & "%'" _
 & " and B1_DESC like  '%" & Me.txtDescricao3.Text & "%'" _
 & " and B1_COD like '%" & Me.txtCodProtheus.Text & "%'" _
 & " and B1_ZFABRIC like '%" & Me.txtFabricante.Text & "%'")


        Dim Bloqueada As String

        For i As Integer = 0 To dgvMaterialProtheus.Rows.Count - 1

            Try

                Bloqueada = Trim(dgvMaterialProtheus.Rows(i).Cells("B1_MSBLQL").Value).ToString

            Catch ex As Exception

                Bloqueada = Nothing

            End Try

            If Bloqueada.ToString <> "1" Then

                dgvMaterialProtheus.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
                dgvMaterialProtheus.Rows(i).Cells("B1_MSBLQL").Value = "OK"

            Else


                dgvMaterialProtheus.Rows(i).DefaultCellStyle.BackColor = Color.LightGray
                dgvMaterialProtheus.Rows(i).Cells("B1_MSBLQL").Value = "NOK"

            End If

        Next


        TimerdgvMaterialProtheus.Enabled = False

    End Sub

    Private Sub frmMateriaisProtheusa_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        TipoBanco = "SQL"

        TimerdgvMaterialProtheus.Enabled = True




    End Sub

    Private Sub frmMateriaisProtheusa_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        TipoBanco = "MYSQL"

    End Sub

    Private Sub dgvMaterialProtheus_CellContentClick(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles dgvMaterialProtheus.CellContentClick

    End Sub

    Private Sub dgvMaterialProtheus_Click(sender As Object, e As EventArgs) Handles dgvMaterialProtheus.Click

    End Sub

    Private Sub dgvMaterialProtheus_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvMaterialProtheus.DataError

    End Sub

    Private Sub btnAssociarMaterialM2_Click(sender As Object, e As EventArgs) Handles btnAssociarMaterialM2.Click

    End Sub
End Class