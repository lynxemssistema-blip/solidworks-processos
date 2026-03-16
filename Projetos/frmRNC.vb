Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class frmRNC

    Dim DescricaoFinalizacao As String

    Private Sub TimerdgvDados_Tick(sender As Object, e As EventArgs) Handles TimerdgvDados.Tick

        Dim sql As String

        If chkConcluidas.Checked = False Then

            sql = " (Estatus <> 'FINALIZADA' OR  Estatus =  '' OR  Estatus  IS NULL ) AND "

        ElseIf chkConcluidas.Checked = True Then

            sql = "" '" (Estatus = 'FINALIZADA' OR  Estatus =  '' OR  Estatus  IS NULL ) AND "

        End If

        dgvDados.DataSource = cl_BancoDados.CarregarDados("SELECT idordemservicoitempendencia, IDOrdemServicoItem,
IdOrdemServico, CodMatFabricante, DescricaoPendencia,
Estatus, UltimaPendencia FROM  " & ComplementoTipoBanco & "ordemservicoitempendencia
where " & sql & "CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'")

        TimerdgvDados.Enabled = False

    End Sub

    Private Sub frmRNC_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TimerdgvDados.Enabled = True

        'Atualiza tabela ordemservico

        cl_BancoDados.Salvar("Update OrdemServico
SET qtdernc = (
    Select Case COUNT(*)
    From ordemservicoitempendencia
    Where ordemservicoitempendencia.idordemservico = OrdemServico.IdOrdemServico And (D_E_L_E_T_E = '' OR D_E_L_E_T_E IS NULL)
);")

        cl_BancoDados.Salvar("Update OrdemServico
SET qtderncfinalizada = (
    Select Case COUNT(*)
    From ordemservicoitempendencia
    Where ordemservicoitempendencia.idordemservico = OrdemServico.IdOrdemServico
      And (ordemservicoitempendencia.D_E_L_E_T_E = '' OR ordemservicoitempendencia.D_E_L_E_T_E IS NULL)
      And ordemservicoitempendencia.finalizada = 'C'
);")

        cl_BancoDados.Salvar("Update OrdemServico
SET qtderncPENDENTE = (
    Select Case COUNT(*)
    From ordemservicoitempendencia
    Where ordemservicoitempendencia.idordemservico = OrdemServico.IdOrdemServico
      And (ordemservicoitempendencia.D_E_L_E_T_E = '' OR ordemservicoitempendencia.D_E_L_E_T_E IS NULL)
      And (ordemservicoitempendencia.finalizada = '' OR ordemservicoitempendencia.finalizada IS NULL)
);")

        'Atualiza tabela Tags

        cl_BancoDados.Salvar("UPDATE tags
SET qtdernc = (
    SELECT COUNT(*)
    FROM ordemservicoitempendencia
    WHERE ordemservicoitempendencia.idtag = tags.idtag and (D_E_L_E_T_E = '' OR D_E_L_E_T_E IS NULL)
);")

        cl_BancoDados.Salvar("UPDATE tags
SET qtderncfinalizada = (
    SELECT COUNT(*)
    FROM ordemservicoitempendencia
    WHERE ordemservicoitempendencia.idtag = tags.idtag
      AND (ordemservicoitempendencia.D_E_L_E_T_E = '' OR ordemservicoitempendencia.D_E_L_E_T_E IS NULL)
      AND ordemservicoitempendencia.finalizada = 'C'
);")

        cl_BancoDados.Salvar("UPDATE tags
SET qtderncPENDENTE = (
    SELECT COUNT(*)
    FROM ordemservicoitempendencia
    WHERE ordemservicoitempendencia.idtag = tags.idtag
      AND (ordemservicoitempendencia.D_E_L_E_T_E = '' OR ordemservicoitempendencia.D_E_L_E_T_E IS NULL)
      AND (ordemservicoitempendencia.finalizada = '' OR ordemservicoitempendencia.finalizada IS NULL)

);")

        'Atualiza tabela Projetos

        cl_BancoDados.Salvar("UPDATE projetos
SET qtdernc = (
    SELECT COUNT(*)
    FROM ordemservicoitempendencia
    WHERE ordemservicoitempendencia.idprojeto = projetos.idprojeto and (D_E_L_E_T_E = '' OR D_E_L_E_T_E IS NULL)
);")

        cl_BancoDados.Salvar("UPDATE projetos
SET qtderncfinalizada = (
    SELECT COUNT(*)
    FROM ordemservicoitempendencia
    WHERE ordemservicoitempendencia.idprojeto = projetos.idprojeto
      AND (ordemservicoitempendencia.D_E_L_E_T_E = '' OR ordemservicoitempendencia.D_E_L_E_T_E IS NULL)
      AND ordemservicoitempendencia.finalizada = 'C'
);")

        cl_BancoDados.Salvar("UPDATE projetos
SET qtderncPENDENTE = (
    SELECT COUNT(*)
    FROM ordemservicoitempendencia
    WHERE ordemservicoitempendencia.idprojeto = projetos.idprojeto
      AND (ordemservicoitempendencia.D_E_L_E_T_E = '' OR ordemservicoitempendencia.D_E_L_E_T_E IS NULL)
      AND (ordemservicoitempendencia.finalizada = '' OR ordemservicoitempendencia.finalizada IS NULL)

);")

    End Sub

    Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click
        Me.Close()

    End Sub

    Private Sub btnFinalizarRNC_Click(sender As Object, e As EventArgs) Handles btnFinalizarRNC.Click

        Dim resultado As DialogResult = MessageBox.Show("Tem certeza que deseja marcar as pendencias do desenho como finalizadas na lista de pendências?", "Confirmação", MessageBoxButtons.YesNo)

        If resultado = DialogResult.No Then

            Exit Sub
        End If

        OrdemServicoItemPendencia.DescricaoFinalizacao = InputBox("Descrição da Finalização da RNC - Lista de Pendência:", "Descrição", "RNC - Lista de Pendência Finalizada").ToString.ToUpper

        If OrdemServicoItemPendencia.DescricaoFinalizacao.ToString <> "" Then

            ' Me.txtDescricaoPendencia.Clear()
            If dgvDados.Rows.Count > 0 Then

                For I As Integer = 0 To dgvDados.Rows.Count - 1

                    If dgvDados.Rows(I).Cells("dgvSelecao").Value = True And dgvDados.Rows(I).Cells("Estatus").Value.ToString <> "FINALIZADA" Then

                        If DadosArquivoCorrente.NomeArquivoSemExtensao = dgvDados.Rows(I).Cells("CodMatFabricante").Value.ToString Then

                            ' dgvDados.Rows(I).Cells("DescricaoFinalizacao").Value = OrdemServicoItemPendencia.DescricaoFinalizacao
                            OrdemServicoItemPendencia.idordemservicoitempendencia = dgvDados.Rows(I).Cells("idordemservicoitempendencia").Value.ToString

                            UpdateDadosFinalizarPendencia()

                            dgvDados.Rows(I).Cells("dgvSelecao").Value = False
                            dgvDados.Rows(I).Cells("dgvIconeestatus").Value = My.Resources.verificado
                            'dgvDados.Rows(I).Cells("Estatus").Value = "FINALIZADA"

                        End If

                    End If

                Next

            End If

            cl_BancoDados.RetornaCampoDaPesquisa("SELECT count(idordemservicoitempendencia) as qtdernc
                    FROM  " & ComplementoTipoBanco & "ordemservicoitempendencia where
                              (Estatus <> 'FINALIZADA' OR  Estatus =  '' OR  Estatus  IS NULL)
                    and CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "';", "qtdernc")

            DadosArquivoCorrente.rnc = VCampo0

            If DadosArquivoCorrente.rnc.ToString = "0" Or DadosArquivoCorrente.rnc.ToString = "" Or DadosArquivoCorrente.rnc = Nothing Then

                'MyTaskPanelHost.TimerdgvDesenhos.Enabled = True

                If MyTaskPanelHost.dgvDataGridBOM.Rows.Count > 0 Then
                    For I As Integer = 0 To MyTaskPanelHost.dgvDataGridBOM.Rows.Count - 1

                        If MyTaskPanelHost.dgvDataGridBOM.Rows(I).Cells("CodMatFabricante").Value.ToString = DadosArquivoCorrente.NomeArquivoSemExtensao Then
                            MyTaskPanelHost.dgvDataGridBOM.Rows(I).Cells("dgvIconeRNC").Value = My.Resources.verificado1
                            ' MyTaskPanelHost.dgvDataGridBOM.Rows(I).Cells("rnc").Value = ""

                        End If
                    Next
                End If

                Try

                    MyTaskPanelHost.btnPendencias.Image = My.Resources.verificado1
                    cl_BancoDados.AlteracaoEspecifica("material", "RNC", "", "CodMatFabricante", DadosArquivoCorrente.NomeArquivoSemExtensao)
                    DadosArquivoCorrente.rnc = ""
                    MyTaskPanelHost.btnPendencias.Enabled = False
                Catch ex As Exception
                Finally
                End Try
            Else

                MyTaskPanelHost.btnPendencias.Image = My.Resources.atencao
                MsgBox("Há RNC em aberto para o desenho corrente", vbCritical, "Atenção")
                DadosArquivoCorrente.rnc = "1"
                MyTaskPanelHost.btnPendencias.Enabled = True

            End If
        Else

            MsgBox("A descrição da finalização da pendência e obrigatoria", vbCritical, "Atenção")

        End If

    End Sub

    Public Sub UpdateDadosFinalizarPendencia()

        Try

            Dim query As String = "UPDATE ordemservicoitempendencia SET
                              UsuarioProjeto = @UsuarioProjeto,
                              DataAcertoProjeto = @DataAcertoProjeto,
                              DescricaoFinalizacao = @DescricaoFinalizacao,
                              Estatus = @Estatus
                              WHERE idordemservicoitempendencia = @idordemservicoitempendencia"

            If myconect.State = myconect.State.Closed Then
                myconect.Open()
            End If

            Using cmd As New MySqlCommand(query, myconect)
                ' Defina os valores para os parâmetros
                cmd.Parameters.AddWithValue("@UsuarioProjeto", Usuario.NomeCompleto)
                cmd.Parameters.AddWithValue("@DataAcertoProjeto", Date.Now.Date)
                cmd.Parameters.AddWithValue("@Estatus", "FINALIZADA")
                cmd.Parameters.AddWithValue("@DescricaoFinalizacao", OrdemServicoItemPendencia.DescricaoFinalizacao)
                cmd.Parameters.AddWithValue("@idordemservicoitempendencia", OrdemServicoItemPendencia.idordemservicoitempendencia)

                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            ' Tratar exceções aqui conforme necessário
        End Try
    End Sub

    Private Sub dgvDados_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvDados.DataBindingComplete

        cl_BancoDados.FormatarDataGridView(dgvDados, "SIM")

        Try

            For I As Integer = 0 To dgvDados.Rows.Count - 1

                If dgvDados.Rows(I).Cells("Estatus").Value = "FINALIZADA" Then

                    dgvDados.Rows(I).Cells("dgvIconeestatus").Value = My.Resources.verificado
                Else

                    dgvDados.Rows(I).Cells("dgvIconeestatus").Value = My.Resources.atencao

                End If

            Next
        Catch ex As Exception
        Finally

        End Try

    End Sub

    Private Sub frmRNC_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        If DadosArquivoCorrente.rnc = "" Then

            MyTaskPanelHost.btnPendencias.Enabled = False
            MyTaskPanelHost.btnPendencias.Image = My.Resources.atencao
            MyTaskPanelHost.btnPendencias.Refresh()

        End If

    End Sub

    Private Sub chkConcluidas_CheckedChanged(sender As Object, e As EventArgs) Handles chkConcluidas.CheckedChanged

        TimerdgvDados.Enabled = True

    End Sub

    Private Sub dgvDados_Click(sender As Object, e As EventArgs) Handles dgvDados.Click

        If dgvDados.CurrentRow.Cells("dgvSelecao").Value = False Then
            dgvDados.CurrentRow.Cells("dgvSelecao").Value = True
        Else
            dgvDados.CurrentRow.Cells("dgvSelecao").Value = False

        End If

    End Sub

End Class