Imports System.Collections.Generic
Imports System.Windows.Forms

Public Class frmMateriaisOmie

    Private Async Sub frmMateriaisOmie_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub dgvMaterialOmie_CellContentClick(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles dgvMaterialOmie.CellContentClick

        If dgvMaterialOmie.CurrentRow.Cells("dgvSelecao").Value = True Then

            dgvMaterialOmie.CurrentRow.Cells("dgvSelecao").Value = False
        Else
            dgvMaterialOmie.CurrentRow.Cells("dgvSelecao").Value = True

        End If

    End Sub

    Private Sub dgvMaterialOmie_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvMaterialOmie.DataBindingComplete

        cl_BancoDados.FormatarDataGridView(dgvMaterialOmie, "SIM")

    End Sub

    Private Async Sub TimerdgvMaterialProtheus_Tick(sender As Object, e As EventArgs) Handles TimerdgvMaterialProtheus.Tick

        Try

            Dim omie As New ClOmie()

            Dim filtros As New List(Of String)

            If Not String.IsNullOrWhiteSpace(txtPesqDescricao01.Text) Then
                filtros.Add($"descricao LIKE '%{txtPesqDescricao01.Text}%'")
            End If

            If Not String.IsNullOrWhiteSpace(txtPesqDescricao02.Text) Then
                filtros.Add($"descricao LIKE '%{txtPesqDescricao02.Text}%'")
            End If

            If Not String.IsNullOrWhiteSpace(txtPesqDescricao03.Text) Then
                filtros.Add($"descricao LIKE '%{txtPesqDescricao03.Text}%'")
            End If

            'If Not String.IsNullOrWhiteSpace(txtPesqcodigo_produto.Text) Then
            '    filtros.Add($"codigo LIKE '%{txtPesqcodigo_produto.Text}%'")
            'End If

            If Not String.IsNullOrWhiteSpace(txtPesqcodigo_produto.Text) Then
                filtros.Add($"codigo_produto LIKE '%{txtPesqcodigo_produto.Text}%'")
            End If

            Dim dtFinalOmie As DataTable = Await omie.CarregarProdutosOmieAsyncDataTable(Me.txtPesqDescricao01.Text, Me.txtPesqDescricao02.Text, Me.txtPesqDescricao03.Text, Me.txtPesqcodigo_produto.Text)

            ' Aplica o filtro, se houver

            If filtros.Count > 0 Then
                dtFinalOmie.DefaultView.RowFilter = String.Join(" AND ", filtros)
            End If

            dgvMaterialOmie.DataSource = dtFinalOmie

            TimerdgvMaterialProtheus.Enabled = False
        Catch ex As Exception
        Finally
        End Try
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

    Private Sub txtPesqcodigo_TextChanged(sender As Object, e As EventArgs)
        TimerdgvMaterialProtheus.Enabled = True
    End Sub

    Private Sub txtPesqcodigo_produto_TextChanged(sender As Object, e As EventArgs) Handles txtPesqcodigo_produto.TextChanged
        TimerdgvMaterialProtheus.Enabled = True
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Hide()
    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        txtPesqDescricao01.Clear()
        txtPesqDescricao02.Clear()
        txtPesqDescricao03.Clear()

        txtPesqcodigo_produto.Clear()

    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        cl_BancoDados.AbrirBancoSqlServerProtheus()

        For i As Integer = 0 To dgvMaterialOmie.Rows.Count - 1

            If dgvMaterialOmie.Rows(i).Cells("dgvSelecao").Value = True Then

                Dim vlipi, vlicms As Double

                If Double.TryParse(Me.lblB1_IPI.Text, vlipi) Then
                    vlipi = Convert.ToDouble(dgvMaterialOmie.Rows(i).Cells("B1_UPRC").Value / 100 * Me.lblB1_IPI.Text)
                Else
                    vlipi = 0.0
                End If

                If Double.TryParse(Me.lblB1_PICM.Text, vlicms) Then
                    vlicms = Convert.ToDouble(dgvMaterialOmie.Rows(i).Cells("B1_UPRC").Value / 100 * Me.lblB1_PICM.Text)
                Else
                    vlicms = 0.0
                End If

                cl_BancoDados.SalvarDadosMaterial(dgvMaterialOmie.Rows(i).Cells("descricao").Value.ToString.Trim.ToUpper,
                                                            dgvMaterialOmie.Rows(i).Cells("peso_liq").Value,
                                                            dgvMaterialOmie.Rows(i).Cells("unidade").Value.ToString.Trim.ToUpper,
                                                            dgvMaterialOmie.Rows(i).Cells("codigo_produto").Value.ToString.Trim.ToUpper,
                                                            dgvMaterialOmie.Rows(i).Cells("codigo").Value.ToString.Trim.ToUpper,
                                                            Replace(dgvMaterialOmie.Rows(i).Cells("valor_unitario").Value, ".", ","),
                                                            "MATERIAL",
                                                            "",'ipi
                                                            "",'valor ipi
                                                            "", 'icms
                                                            "", 'valor icms
                                                            Replace(dgvMaterialOmie.Rows(i).Cells("valor_unitario").Value, ".", ","),
                                                            Nothing, "MATERIAL",
                                                            dgvMaterialOmie.Rows(i).Cells("codigo").Value.ToString.Trim.ToUpper,
                                                            Usuario.NomeCompleto,
                                                            Date.Now.Date.ToString("dd/mm/yyyy"), ' dataatual,
                                                            "", 'txtProdundidadeEmbalagem.Text,
                                                            "", 'txtLarguraEmbalagem.Text,
                                                            "", 'txtAlturaEmbalagem.Text,
                                                            "", 'txtAltura.Text,
                                                            "", 'txtLargura.Text,
                                                            "", 'txtProfundidade.Text,
                                                            "") 'txtPesoMaisEmbalagem.Text)

                dgvMaterialOmie.Rows(i).Cells("dgvSelecao").Value = False

            End If
        Next

        MateriaisAlmoxarifado.TimerDgvMaterial.Enabled = True

    End Sub

    Private Sub dgvMaterialOmie_Click(sender As Object, e As EventArgs) Handles dgvMaterialOmie.Click

        Me.lblcodigo.Text = dgvMaterialOmie.CurrentRow.Cells("codigo").Value.ToString.Trim.ToUpper
        Me.lblcodigo_produto.Text = dgvMaterialOmie.CurrentRow.Cells("codigo_produto").Value.ToString.Trim.ToUpper
        'Me.lblB1_ZFABRIC.Text = dgvMateridgvMaterialOmiealProtheus.CurrentRow.Cells("B1_ZFABRIC").Value.ToString.Trim.ToUpper
        ' Me.lblB1_UPRC.Text = Replace(dgvMaterialOmie.CurrentRow.Cells("B1_UPRC").Value, ".", ",")
        Me.lblB1_UM.Text = dgvMaterialOmie.CurrentRow.Cells("unidade").Value.ToString.Trim.ToUpper
        'Me.lblB1_TIPO.Text = dgvMaterialOmie.CurrentRow.Cells("B1_TIPO").Value.ToString.Trim.ToUpper
        'Me.lblB1_PICM.Text = Replace(dgvMaterialOmie.CurrentRow.Cells("B1_PICM").Value, ".", ",")
        'Me.lblB1_IPI.Text = Replace(dgvMaterialOmie.CurrentRow.Cells("B1_IPI").Value, ".", ",")
        Me.lblB1_DESC.Text = dgvMaterialOmie.CurrentRow.Cells("descricao").Value.ToString.Trim.ToUpper
        Me.lblB1_UPRC.Text = Replace(dgvMaterialOmie.CurrentRow.Cells("valor_unitario").Value, ".", ",")
        Me.lblB1_PESO.Text = Replace(dgvMaterialOmie.CurrentRow.Cells("peso_liq").Value, ".", ",")

    End Sub

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        TimerdgvMaterialProtheus.Enabled = True

        Dim omie As New ClOmie()
        '   omie.CarregarProdutosOmieAsync(dgvMaterialOmie)

        Dim dt As DataTable = Await omie.CarregarProdutosOmieAsyncDataTable(Me.txtPesqDescricao01.Text, Me.txtPesqDescricao02.Text, Me.txtPesqDescricao03.Text, Me.txtPesqcodigo_produto.Text)

        dt.DefaultView.RowFilter = $"descricao LIKE '%{txtPesqDescricao01.Text}%'"

        dgvMaterialOmie.DataSource = dt

    End Sub

End Class