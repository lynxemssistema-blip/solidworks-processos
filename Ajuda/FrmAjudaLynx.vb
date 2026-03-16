Imports System.Windows.Forms

Public Class FrmAjudaLynx

    Dim url As String

    Private Async Sub FrmAjudaLynx_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DataGridView1.Rows.Add("--------    Lynx - SINCO Dados Principais   --------", "--------Videos das funções--------")
        DataGridView1.Rows.Add("Entendendo o Cadastro de Desenhos", "https://www.loom.com/share/e6fd1772bf0d4572ada1bb05b3a3cc0d?sid=dcfd454f-d4af-4099-b9e2-da3cde56bee0")
        DataGridView1.Rows.Add("Associando Materiais na peças/Desenhos", "https://www.loom.com/share/13776a533bbc4e4186f2ccd153d4fbf3?sid=a662bf24-3644-41a5-8e7d-baa6b898ecf7")
        DataGridView1.Rows.Add("Adcionando Processo de Fabricação", "https://www.loom.com/share/21bcde8413d54a8c8a795568491feb4f?sid=25562237-e142-473c-b6df-9eafdc5bf4fd")
        DataGridView1.Rows.Add("Tipo de Desenho e Organização", "https://www.loom.com/share/1cd3c19689f3489eb27f332a618cc902?sid=26b22478-5f8e-4e84-8209-f49afdb2ad02")
        DataGridView1.Rows.Add("Configurações de Parametros dentro do Solid Works", "https://www.loom.com/share/dd54f9f856034cf3b6157e10b02ed9d9?sid=16e3cae6-89f2-417a-a88a-9a97cc890cbc")
        DataGridView1.Rows.Add("Acabamento de Peças e ou Conjuntos", "https://www.loom.com/share/5e255d9ac78646ce91bb8112f290f040?sid=1a229b59-84b8-4481-b73f-72cb787bf8c4")
        DataGridView1.Rows.Add("", "")

        DataGridView1.Rows.Add("--------    Lynx - SINCO Ordem de Serviço   --------", "--------Videos das funções--------")
        DataGridView1.Rows.Add("Criação da Ordem de Serviço", "https://www.loom.com/share/197ffb2c9d6b485fbc6174a67ff6badc?sid=a26e01ae-6af2-490b-b6da-8f622e1af087")
        DataGridView1.Rows.Add("Detalhamento da Liberação da Ordem de Serviço", "https://www.loom.com/share/93774690a9dd442e831baeb2dd7c22c7?sid=d168a23c-58f5-43dd-b3e1-0bbf9b8f3a05")
        DataGridView1.Rows.Add("Cadastro de Projeto/Tag", "https://www.loom.com/share/88ac8a6076e04bb9a09fb4594f3e7e34?sid=d9df7ca1-fd08-4aa3-a1cb-23616b35d924")
        DataGridView1.Rows.Add("", "")

        DataGridView1.Rows.Add("--------    Lynx - SINCO Lista de Material SW   --------", "--------Videos das funções--------")
        DataGridView1.Rows.Add("Entendendo a aba BOM Lista de Material", "https://www.loom.com/share/cc8db41b208e482f9d0c69c7bae6ecb3?sid=aec62ff4-56cd-4b3f-834d-17947bb6a8bc")
        DataGridView1.Rows.Add("", "")

        DataGridView1.Rows.Add("-------- SINCO Lista de Tarefas   --------", "--------Videos das funções--------")
        DataGridView1.Rows.Add("Entendendo o Cadastro é a lista de tarefas-01", "https://www.loom.com/share/4838ab90e6a445a88768a99c475e8a4f?sid=af571d2e-9b38-442e-8bd5-80b7bc597ad5")
        DataGridView1.Rows.Add("Entendendo o Cadastro é a lista de tarefas-Quem usa? Quando usar?", "https://www.loom.com/share/ee4a6b084c364c30b25ddcd31352bc40?sid=b0398f35-c67a-42cd-8379-f8e36bd816fd")
        DataGridView1.Rows.Add("", "")

        ' cl_BancoDados.FormatarDataGridView(DataGridView1, "SIM")

    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick

        Dim url As String = DataGridView1.CurrentRow.Cells("txtEnderecoAjuda").Value.ToString

        Try
            ' Abre a URL no navegador padrão
            Process.Start(url)
        Catch ex As Exception
            ' Tratar erro, caso o processo falhe
            ' MessageBox.Show("Não foi possível abrir a URL: " & ex.Message)
        End Try

    End Sub

    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete

        cl_BancoDados.FormatarDataGridView(DataGridView1, "SIM")

    End Sub

End Class