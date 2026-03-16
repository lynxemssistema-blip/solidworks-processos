Public Class frmTelaCarregamento

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs)

    End Sub

    Private Sub frmTelaCarregamento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = 100
        ProgressBar1.Value = 0
        lblStatus.Text = "Aguardando..."

    End Sub

    Private Sub bgWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgWorker1.DoWork
        For i As Integer = 1 To MyTaskPanelHost.dgvDataGridBOM.RowCount '100
            Threading.Thread.Sleep(50) ' Simula trabalho pesado
            bgWorker1.ReportProgress(i)
        Next
    End Sub

    Private Sub bgWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgWorker1.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        lblStatus.Text = $"Processando... {e.ProgressPercentage}%"
    End Sub

    Private Sub bgWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgWorker1.RunWorkerCompleted
        lblStatus.Text = "Concluído!"
        MsgBox("Processo finalizado.")

    End Sub

End Class