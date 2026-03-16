Imports System.Collections.Generic
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Runtime.InteropServices
Imports System.Security.AccessControl
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports SolidWorks.Interop.sldworks

Public Class ClBancoDados

    Private Const MaxRetry As Integer = 3
    Private keepAliveTimer As System.Windows.Forms.Timer

    ' Método para abrir conexão com o banco de dados
    Public Function AbrirBanco() As Boolean

        ' KillMinhasConexoesMySQL()

        If My.Settings.TipoConexao = "MYSQL" Then

            'conexao = "Server=alfatec2.mysql.uhserver.com;database=alfatec2;uid=alfateccozinhas;pwd=jHAzhFG848@yN@U;
            '        Max Pool Size=50;Connection Timeout=600;Connection Lifetime=3600;CharSet=utf8;"

            If My.Settings.MySqlBancoDados = "xx" Then

                BuscarArquivoconf()
            Else

                conexao = "Server=" & My.Settings.MysqlEndereco & ";database=" & My.Settings.MySqlBancoDados & ";uid=" & My.Settings.MysqlUsuario & ";pwd=" & My.Settings.MysqlSenha & ";
                    Max Pool Size=50;Connection Timeout=600;Connection Lifetime=3600;CharSet=utf8;"
            End If

        ElseIf My.Settings.TipoConexao = "SQL" Then

            conexao = "Persist Security Info = False;User ID = " & My.Settings.MysqlUsuario & ";
                 Password=" & My.Settings.MysqlSenha & ";
                 MultipleActiveResultSets=true;Initial Catalog=" & My.Settings.MySqlBancoDados & ";Data Source=" & My.Settings.MysqlEndereco & ";"

            'My.Settings.BancoDadosAtivo = My.Settings.BancoDadosAtivo.ToString

        ElseIf My.Settings.TipoConexao = "ACCESS" Then

            ''''''''' Se estiver usando um banco .mdb antigo, use o provider Jet
            conexao = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\caminho_do_banco\seu_banco.mdb;Persist Security Info=False;"

        End If

        Try
            Select Case My.Settings.TipoConexao.ToUpper()
                Case "MYSQL"
                    conexao = conexao
                    myconect = New MySqlConnection(conexao)
                    myconect.Open()

                Case "SQL"
                    conexao = conexao
                    myconectSQL = New SqlConnection(conexao)
                    myconectSQL.Open()

                Case "ACCESS"
                    conexao = conexao
                    myconectAccess = New OleDbConnection(conexao)
                    myconectAccess.Open()

                Case Else
                    Throw New Exception("Tipo de banco de dados inválido.")
            End Select

            ' Inicializar mecanismo de keep-alive
            StartKeepAlive()
            Return True
        Catch ex As Exception
            '   MsgBox($"Erro ao abrir banco: {ex.Message}")
            ' Inicializar mecanismo de keep-alive
            ' StartKeepAlive()

            BuscarArquivoconf()

        End Try

    End Function

    Private Sub BuscarArquivoconf()

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
                    Dim parametrosNecessarios = {"endereco", "Usuario", "Banco", "Senha", "EnderecoPastaRaizOS", "EnderecoTemplateExcelOrdemServico", "ParametroExportarDXF", "EnderecoImagens", "ProgramaRM"}
                    For Each param In parametrosNecessarios
                        If Not parametrosEncontrados.ContainsKey(param) Then
                            MsgBox($"Erro: Parâmetro '{param}' não encontrado no arquivo de configuração!", MsgBoxStyle.Critical)
                            'Return
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
                Else
                    MsgBox("Arquivo selecionado não encontrado!", MsgBoxStyle.Exclamation)
                End If
            End If
        Else
            MsgBox("Senha inválida!", MsgBoxStyle.Exclamation)
        End If

    End Sub

    Public Sub KillMinhasConexoesMySQL()
        Dim connectionString As String = conexao

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' Obter o nome do host local (pode ser necessário ajustar para IP)
                Dim localHost As String = Dns.GetHostName()
                Dim localIP As String = Dns.GetHostEntry(localHost).AddressList.First(Function(ip) ip.AddressFamily = Net.Sockets.AddressFamily.InterNetwork).ToString()

                ' Obter todas as conexões da máquina local
                Dim sql As String = "SELECT ID FROM information_schema.PROCESSLIST WHERE HOST LIKE @hostPattern AND ID <> CONNECTION_ID();"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@hostPattern", localIP & ":%")

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        Dim idsToKill As New List(Of Integer)

                        While reader.Read()
                            idsToKill.Add(reader.GetInt32("ID"))
                        End While

                        reader.Close()

                        ' Matar conexões encontradas
                        For Each id As Integer In idsToKill
                            Using killCmd As New MySqlCommand("KILL " & id, conn)
                                killCmd.ExecuteNonQuery()
                                Console.WriteLine("Conexão encerrada: " & id)
                            End Using
                        Next
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("Erro ao tentar encerrar conexões: " & ex.Message)
        End Try
    End Sub

    Public Function ProcessoAmdamento(ByVal ValorMax As Integer, ByVal Titulo As String) As Boolean

        BarramentoProcessamento.Show()
        BarramentoProcessamento.ProgressBarProcessamento.Minimum = 0
        BarramentoProcessamento.ProgressBarProcessamento.Maximum = ValorMax

        For i As Integer = 0 To ValorMax

            ContadorBarradeProgresso = i
            BarramentoProcessamento.ProgressBarProcessamento.Value = Math.Min(i, ValorMax)
            BarramentoProcessamento.lblTituloProcessamento.Text = Titulo & ": " & Convert.ToString((i / ValorMax) * 100) & " %"
            Application.DoEvents() ' Libera a thread de UI para redesenhar a barra

        Next

        BarramentoProcessamento.ProgressBarProcessamento.Value = 0
        ContadorBarradeProgresso = 0

    End Function

    Public Function AbrirBancoSqlServerProtheus() As Boolean

        Try

            conexao = "Persist Security Info = False;User ID = engenharia;Password=Engenhari@003498;MultipleActiveResultSets=true;Initial Catalog=MP12OFICIAL;Data Source=192.168.163.22;"
            My.Settings.BancoDadosAtivo = "Amc Soluçoes"

            conexao = conexao
            myconectSQL = New SqlConnection(conexao)
            myconectSQL.Open()

            AbrirBancoSqlServerProtheus = Convert.ToBoolean(True)
        Catch ex As Exception
            AbrirBancoSqlServerProtheus = Convert.ToBoolean(False)
        End Try

    End Function

    ' Método para manter a conexão ativa
    Private Sub StartKeepAlive()
        If keepAliveTimer Is Nothing Then
            keepAliveTimer = New System.Windows.Forms.Timer() With {
                .Interval = 90000 ' 90 segundos
            }
            AddHandler keepAliveTimer.Tick, AddressOf PingDatabase
            keepAliveTimer.Start()
        End If
    End Sub

    ' Método para testar a conexão periodicamente

    Private Sub PingDatabase(sender As Object, e As EventArgs)

        For i As Integer = 1 To MaxRetry
            Try
                Select Case My.Settings.TipoConexao.ToUpper()
                    Case "MYSQL"
                        If myconect IsNot Nothing AndAlso myconect.State = ConnectionState.Open Then
                            Dim command As New MySqlCommand("SELECT 1", myconect)
                            command.ExecuteScalar()
                        Else
                            AbrirBanco()
                        End If

                    Case "SQL"
                        If myconectSQL IsNot Nothing AndAlso myconectSQL.State = ConnectionState.Open Then
                            Dim command As New SqlCommand("SELECT 1", myconectSQL)
                            command.ExecuteScalar()
                        Else
                            AbrirBanco()
                        End If
                End Select
                Exit For ' Sai do loop caso a verificação seja bem-sucedida
            Catch ex As Exception
                ' MsgBox("Erro no keep-alive: " & ex.Message, MsgBoxStyle.Exclamation)
                If i = MaxRetry Then Throw ' Repassa o erro após atingir o máximo de tentativas
            End Try
        Next
    End Sub

    ' Método para fechar a conexão corretamente
    Public Sub FecharBanco()
        Try
            If myconect IsNot Nothing AndAlso myconect.State = ConnectionState.Open Then
                myconect.Close()
            End If

            If myconectSQL IsNot Nothing AndAlso myconectSQL.State = ConnectionState.Open Then
                myconectSQL.Close()
            End If

            If myconectAccess IsNot Nothing AndAlso myconectAccess.State = ConnectionState.Open Then
                myconectAccess.Close()
            End If

            If keepAliveTimer IsNot Nothing Then
                keepAliveTimer.Stop()
                keepAliveTimer.Dispose()
                keepAliveTimer = Nothing
            End If

            KillMinhasConexoesMySQL()
        Catch ex As Exception
            MsgBox("Erro ao fechar banco: " & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Public Function ObterDiaDaSemana(ByVal data As Date) As String

        ' ObterDiaDaSemana = data.DayOfWeek.ToString()

        Dim diasDaSemana As String() = {"Domingo", "Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira", "Sábado"}
        Dim diaDaSemana As Integer = data.DayOfWeek
        ObterDiaDaSemana = diasDaSemana(diaDaSemana)

    End Function

    Public Function CriarGrupoDataGridView(ByVal ObjDataGrid As DataGridView, ByVal NomeColunaDataGrid As String)

        Try

            Dim currentGroup As String = ""
            Dim fontNegrito As New Font(ObjDataGrid.Font, FontStyle.Bold) ' Crie uma fonte em negrito
            Dim isFirstInGroup As Boolean = True

            For Each row As DataGridViewRow In ObjDataGrid.Rows
                Dim osmValue As String = row.Cells(NomeColunaDataGrid).Value.ToString()

                If osmValue <> currentGroup Then
                    currentGroup = osmValue
                    isFirstInGroup = True
                    row.DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    isFirstInGroup = False
                End If

                ' Se for a primeira linha de um novo grupo, aplique o estilo de fonte negrito
                If isFirstInGroup Then
                    For Each cell As DataGridViewCell In row.Cells
                        cell.Style.Font = fontNegrito
                    Next
                End If
            Next
        Catch ex As Exception

        End Try

    End Function

    'Public Function arquivoConfiguracao() As Boolean

    '    If InputBox("Senha de acesso", "Administrador", "") = "99678982" Then
    '        Dim OpenfileConfiguracao As New OpenFileDialog

    '        ' Configura o diálogo para aceitar somente arquivos de texto
    '        OpenfileConfiguracao.Filter = "Arquivos de Texto (*.txt)|*.txt|Todos os arquivos (*.*)|*.*"
    '        OpenfileConfiguracao.FilterIndex = 1

    '        ' Exibe o diálogo e verifica se o usuário selecionou um arquivo
    '        If OpenfileConfiguracao.ShowDialog() = DialogResult.OK Then
    '            Dim caminhoArquivo As String = OpenfileConfiguracao.FileName

    '            ' Verifica se o arquivo existe
    '            If File.Exists(caminhoArquivo) Then
    '                Try
    '                    ' Variáveis para armazenar os parâmetros
    '                    Dim endereco, usuario, banco, senha As String
    '                    Dim EnderecoPastaRaizOS, EnderecoTemplateExcel, CopiaBancoDados As String
    '                    Dim EnderecoPastaRaizRomaneio, EnderecoTemplateExcelRomaneio, ParametroExportarDXF, TipoConexao As String

    '                    ' Codificação utilizada na leitura do arquivo
    '                    Dim codificacao As Encoding = Encoding.GetEncoding("ISO-8859-1") ' Ajustável conforme o arquivo

    '                    ' Lê o arquivo linha por linha
    '                    Dim parametrosEncontrados As New Dictionary(Of String, String)
    '                    Using leitor As New StreamReader(caminhoArquivo, codificacao)
    '                        While Not leitor.EndOfStream
    '                            Dim linha As String = leitor.ReadLine()?.Trim()

    '                            ' Ignorar linhas em branco ou mal formadas
    '                            If String.IsNullOrEmpty(linha) OrElse Not linha.Contains(";") Then Continue While

    '                            Dim partes = linha.Split(";"c)
    '                            If partes.Length = 2 Then
    '                                Dim chave = partes(0).Trim()
    '                                Dim valor = partes(1).Trim()

    '                                ' Armazena no dicionário
    '                                If Not parametrosEncontrados.ContainsKey(chave) Then
    '                                    parametrosEncontrados(chave) = valor
    '                                End If
    '                            End If
    '                        End While
    '                    End Using

    '                    ' Atribui valores ao My.Settings
    '                    Dim parametrosNecessarios = {"endereco", "Usuario", "Banco", "Senha", "EnderecoPastaRaizOS", "EnderecoTemplateExcelOrdemServico", "ParametroExportarDXF", "TipoConexao"}
    '                    For Each param In parametrosNecessarios
    '                        If Not parametrosEncontrados.ContainsKey(param) Then
    '                            MsgBox($"Erro: Parâmetro '{param}' não encontrado no arquivo de configuração!", MsgBoxStyle.Critical)

    '                        End If
    '                    Next

    '                    My.Settings.MySqlBancoDados = parametrosEncontrados("Banco")
    '                    My.Settings.MysqlEndereco = parametrosEncontrados("endereco")
    '                    My.Settings.MysqlUsuario = parametrosEncontrados("Usuario")
    '                    My.Settings.MysqlSenha = parametrosEncontrados("Senha")
    '                    My.Settings.BancoDadosAtivo = parametrosEncontrados("Banco")

    '                    My.Settings.EnderecoPastaRaizOS = parametrosEncontrados("EnderecoPastaRaizOS")
    '                    My.Settings.EnderecoTemplateExcel = parametrosEncontrados("EnderecoTemplateExcelOrdemServico")
    '                    My.Settings.ParametroExportarDXF = parametrosEncontrados("ParametroExportarDXF")
    '                    My.Settings.TipoConexao = parametrosEncontrados("TipoConexao")

    '                    ' Salva as configurações
    '                    My.Settings.Save()

    '                    MsgBox("Configurações carregadas com sucesso!", MsgBoxStyle.Information)

    '                    ' Dim swApp As SldWorks.SldWorks = Nothing
    '                    swapp = CType(GetObject(, "SldWorks.Application"), SolidWorks.Interop.sldworks.SldWorks)

    '                    swapp.ExitApp()
    '                    swapp = Nothing

    '                    ' Aguarda 5 segundos antes de reabrir
    '                    System.Threading.Thread.Sleep(5000)

    '                    ' Reabre o SolidWorks
    '                    Dim swAppNew As SolidWorks.Interop.sldworks.SldWorks = CType(CreateObject("SldWorks.Application"), SolidWorks.Interop.sldworks.SldWorks)
    '                    swAppNew.Visible = True

    '                    ' Carregar o suplemento novamente
    '                    swapp.LoadAddIn("SwLynx_4._1")

    '                    cl_BancoDados.AbrirBanco()

    '                Catch ex As Exception
    '                    MsgBox($"Erro ao processar o arquivo de configuração: {ex.Message}", MsgBoxStyle.Critical)
    '                End Try
    '            Else
    '                MsgBox("Arquivo selecionado não encontrado!", MsgBoxStyle.Exclamation)
    '            End If
    '        End If
    '    Else
    '        MsgBox("Senha inválida!", MsgBoxStyle.Exclamation)
    '    End If

    'End Function

    Public Sub arquivoConfiguracao()

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
                        Dim parametrosNecessarios = {"endereco", "Usuario", "Banco", "Senha", "EnderecoPastaRaizOS", "EnderecoTemplateExcelOrdemServico", "ParametroExportarDXF", "EnderecoImagens", "ProgramaRM"}
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

    ' Função para carregar dados em um DataTable
    Public Function CarregarDados(ByVal query As String) As DataTable

        Dim dtTabela As New System.Data.DataTable()

        Try

            cl_BancoDados.AbrirBanco()

            If My.Settings.TipoConexao = "MYSQL" Then
                Try
                    ' Cria um adaptador de dados
                    Using adaptador As New MySqlDataAdapter(query, myconect)
                        ' Preenche o DataTable com os dados da consulta
                        adaptador.Fill(dtTabela)

                    End Using
                Catch ex As MySqlException
                    Debug.WriteLine("[CarregarDados] Erro MySQL: " & ex.Message)
                Catch ex As Exception
                    Debug.WriteLine("[CarregarDados] Erro inesperado: " & ex.Message)
                End Try

            ElseIf My.Settings.TipoConexao = "ACCESS" Then
                Try
                    ' Cria um adaptador de dados para Access
                    Using adaptador As New OleDbDataAdapter(query, myconectAccess)
                        ' Preenche o DataTable com os dados da consulta
                        adaptador.Fill(dtTabela)
                    End Using
                Catch ex As OleDbException
                    Debug.WriteLine("[CarregarDados] Erro Access: " & ex.Message)
                Catch ex As Exception
                    Debug.WriteLine("[CarregarDados] Erro inesperado: " & ex.Message)
                End Try

            End If

            Return dtTabela
        Catch ex As Exception
            Debug.WriteLine("[CarregarDados] Erro geral: " & ex.Message)
        Finally
            cl_BancoDados.FecharBanco()

        End Try

    End Function


    Public Function CarregarDadosCompleto(ByVal idOrdemServico As Integer,
                                      ByVal numeroDesenho As String,
                                      ByVal acabamento As String) As DataTable
        Dim dt As New DataTable()

        Dim sql As String =
"SELECT
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
FROM ordemservicoitem
WHERE
    D_E_L_E_T_E <> '*'
    AND IdOrdemServico = @IdOrdemServico
    AND CodMatFabricante LIKE CONCAT('%', @CodMatFabricante, '%')
    AND Acabamento LIKE CONCAT('%', @Acabamento, '%')
ORDER BY IdOrdemServicoItem;"

        Try
            ' Using conexao As New MySqlConnection(myconect)
            'conexao.Open()
            cl_BancoDados.AbrirBanco()

            Using cmd As New MySqlCommand(sql, myconect)
                ' Parâmetros (parametrização segura e cacheável)
                cmd.Parameters.AddWithValue("@IdOrdemServico", idOrdemServico)
                cmd.Parameters.AddWithValue("@CodMatFabricante", numeroDesenho)
                cmd.Parameters.AddWithValue("@Acabamento", acabamento)

                Using da As New MySqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
            ' End Using

            cl_BancoDados.FecharBanco()

        Catch ex As MySqlException
            MsgBox("Erro MySQL: " & ex.Message, vbCritical, "Erro MySQL")
        Catch ex As Exception
            MsgBox("Erro geral: " & ex.Message, vbCritical, "Erro Geral")
        End Try

        Return dt
    End Function


    ' Função para carregar dados em um DataTable
    Public Function CarregarDadosSqlServer(ByVal query As String) As DataTable

        Dim dtTabela As New System.Data.DataTable()

        Try

            cl_BancoDados.AbrirBanco()

            If cl_BancoDados.AbrirBancoSqlServerProtheus() = True Then
                Try
                    ' Cria um adaptador de dados
                    Using adaptador As New SqlDataAdapter(query, myconectSQL)
                        ' Preenche o DataTable com os dados da consulta
                        adaptador.Fill(dtTabela)

                    End Using
                Catch ex As SqlException
                    MessageBox.Show("Erro ao carregar dados: " & ex.Message)
                Catch ex As Exception
                    MessageBox.Show("Erro inesperado: " & ex.Message)
                End Try

            End If

            Return dtTabela
        Catch ex As Exception
            Debug.WriteLine("[CarregarDadosSqlServer] Erro geral: " & ex.Message)
        Finally
            cl_BancoDados.FecharBanco()
        End Try

    End Function

    Public Sub Salvar(ByVal funcaosql As String)

        Try

            cl_BancoDados.AbrirBanco()

            If My.Settings.TipoConexao = "MYSQL" Then

                Try
                    Using mycomand As New MySqlCommand(funcaosql, myconect)
                        Dim rowsAffected As Integer = mycomand.ExecuteNonQuery()
                    End Using
                Catch ex As MySqlException
                    Debug.WriteLine("[Salvar] Erro MySQL: " & ex.Message)
                Catch ex As Exception
                    Debug.WriteLine("[Salvar] Erro inesperado (MySQL): " & ex.Message)
                End Try

            ElseIf My.Settings.TipoConexao = "SQL" Then

                Try
                    Using mycomand As New SqlCommand(funcaosql, myconectSQL)
                        Dim rowsAffected As Integer = mycomand.ExecuteNonQuery()
                    End Using
                Catch ex As SqlException
                    Debug.WriteLine("[Salvar] Erro SQL Server: " & ex.Message)
                Catch ex As Exception
                    Debug.WriteLine("[Salvar] Erro inesperado (SQL): " & ex.Message)
                End Try

            ElseIf My.Settings.TipoConexao = "ACCESS" Then
                Try
                    If myconectAccess.State = ConnectionState.Closed Then
                        myconectAccess.Open()
                    End If

                    Using mycomand As New OleDbCommand(funcaosql, myconectAccess)
                        Dim rowsAffected As Integer = mycomand.ExecuteNonQuery()
                    End Using
                Catch ex As OleDbException
                    Debug.WriteLine("[Salvar] Erro Access: " & ex.Message)
                Catch ex As Exception
                    Debug.WriteLine("[Salvar] Erro inesperado (Access): " & ex.Message)
                Finally
                    If myconectAccess IsNot Nothing AndAlso myconectAccess.State = ConnectionState.Open Then
                        myconectAccess.Close()
                    End If
                End Try

            End If

            cl_BancoDados.FecharBanco()
        Catch ex As Exception
            Debug.WriteLine("[Salvar] Erro geral: " & ex.Message)
        Finally
            cl_BancoDados.FecharBanco()
        End Try

    End Sub


    Public Sub SalvarParametros(ByVal sql As String, ByVal parametros As List(Of MySqlParameter))
        'Using conn As New MySqlConnection()
        'conn.Open()

        cl_BancoDados.AbrirBanco()
        Using cmd As New MySqlCommand(sql, myconect)
            For Each p In parametros
                cmd.Parameters.Add(p)
            Next
            cmd.ExecuteNonQuery()
        End Using

        cl_BancoDados.FecharBanco()
        ' End Using
    End Sub


    ' Função para salvar a ordem das colunas de um DataGridView específico
    Public Sub SaveColumnOrder(grid As DataGridView)
        Dim columnOrder As New List(Of String)

        ' Percorre todas as colunas e salva a posição
        For Each column As DataGridViewColumn In grid.Columns
            columnOrder.Add($"{column.Name}:{column.DisplayIndex}")
        Next

        ' Dicionário para armazenar configurações de vários grids
        Dim savedOrders As New Dictionary(Of String, String)

        ' Se houver dados já salvos, carrega para preservar outros grids
        If Not String.IsNullOrEmpty(My.Settings.OrdemColunasDataGridView) Then
            For Each entry As String In My.Settings.OrdemColunasDataGridView.Split("|"c)
                Dim parts As String() = entry.Split("="c)
                If parts.Length = 2 Then
                    savedOrders(parts(0)) = parts(1)
                End If
            Next
        End If

        ' Atualiza ou adiciona a configuração do grid atual
        savedOrders(grid.Name) = String.Join(",", columnOrder)

        ' Salva de volta no My.Settings
        My.Settings.OrdemColunasDataGridView = String.Join("|", savedOrders.Select(Function(kv) $"{kv.Key}={kv.Value}"))
        My.Settings.Save()
    End Sub

    ' Função para carregar a ordem das colunas de um DataGridView específico
    Public Sub LoadColumnOrder(grid As DataGridView)
        If String.IsNullOrEmpty(My.Settings.OrdemColunasDataGridView) Then Exit Sub

        Dim savedOrders As New Dictionary(Of String, String)

        ' Carrega configurações salvas
        For Each entry As String In My.Settings.OrdemColunasDataGridView.Split("|"c)
            Dim parts As String() = entry.Split("="c)
            If parts.Length = 2 Then
                savedOrders(parts(0)) = parts(1)
            End If
        Next

        ' Se não houver configuração para esse grid, sai
        If Not savedOrders.ContainsKey(grid.Name) Then Exit Sub

        Dim columnOrder As String() = savedOrders(grid.Name).Split(","c)

        ' Aplica a ordem salva
        For Each colInfo As String In columnOrder
            Dim colParts As String() = colInfo.Split(":"c)
            If colParts.Length = 2 Then
                Dim colName As String = colParts(0)
                Dim colIndex As Integer
                If Integer.TryParse(colParts(1), colIndex) Then
                    Dim col As DataGridViewColumn = grid.Columns(colName)
                    If col IsNot Nothing Then col.DisplayIndex = colIndex
                End If
            End If
        Next
    End Sub

    Function ComboBoxDataSet(ByVal Tabela As String, ByVal Campo_Id As String, ByVal CampoDescricao As String, ByVal ObjComboBox As ComboBox, ByVal consulta As String, Optional NomeBancoCliente As String = "") As Boolean

        Try

            cl_BancoDados.AbrirBanco()

            If My.Settings.TipoConexao = "MYSQL" Then
                ' Construção da consulta SQL com parâmetros
                Dim sqlStr As String = $"SELECT {Campo_Id}, UPPER({CampoDescricao}) AS {CampoDescricao} FROM {ComplementoTipoBanco & Tabela} {consulta} ORDER BY {CampoDescricao}"

                ' Criação do adaptador e do dataset
                Using da As New MySqlDataAdapter(sqlStr, myconect)

                    Dim ds As New DataSet()
                    da.Fill(ds, Tabela)

                    ' Configuração do ComboBox
                    ObjComboBox.DataSource = ds.Tables(Tabela)
                    ObjComboBox.DisplayMember = CampoDescricao.ToUpper()
                    ObjComboBox.ValueMember = Campo_Id

                    Return True
                End Using

            ElseIf My.Settings.TipoConexao = "SQL" Then
                ' Construção da consulta SQL com parâmetros
                Dim sqlStr As String = $"SELECT {Campo_Id}, UPPER(TRIM({CampoDescricao})) AS {CampoDescricao} FROM {NomeBancoCliente & Tabela} {consulta} ORDER BY {NomeBancoCliente & Tabela & "." & CampoDescricao}"

                ' Criação do adaptador e do dataset
                Using da As New SqlDataAdapter(sqlStr, myconectSQL)

                    Dim ds As New DataSet()
                    da.Fill(ds, Tabela)

                    ' Configuração do ComboBox
                    ObjComboBox.DataSource = ds.Tables(Tabela)
                    ObjComboBox.DisplayMember = CampoDescricao.ToUpper()
                    ObjComboBox.ValueMember = Campo_Id

                    Return True
                End Using

            ElseIf My.Settings.TipoConexao = "ACCESS" Then

                ' Construção da consulta SQL com parâmetros
                Dim sqlStr As String = $"SELECT {Campo_Id}, UPPER({CampoDescricao}) AS {CampoDescricao} FROM {ComplementoTipoBanco & Tabela} {consulta} ORDER BY {CampoDescricao}"

                ' Criação do adaptador e do dataset
                Using da As New OleDbDataAdapter(sqlStr, myconectAccess)
                    Dim ds As New DataSet()
                    da.Fill(ds, Tabela)

                    ' Configuração do ComboBox
                    ObjComboBox.DataSource = ds.Tables(Tabela)
                    ObjComboBox.DisplayMember = CampoDescricao.ToUpper()
                    ObjComboBox.ValueMember = Campo_Id

                    Return True
                End Using

            End If

            cl_BancoDados.FecharBanco()
        Catch ex As Exception
            Debug.WriteLine("[ComboBoxDataSet] Erro: " & ex.Message)
            Return False
        Finally
            cl_BancoDados.FecharBanco()
        End Try

    End Function

    ' Função refatorada para ser assíncrona
    Async Function ComboBoxDataSetAsync(ByVal Tabela As String, ByVal Campo_Id As String, ByVal CampoDescricao As String, ByVal ObjComboBox As ComboBox, ByVal consulta As String) As Task(Of Boolean)

        Try
            cl_BancoDados.AbrirBanco()

            ' Construção da consulta SQL com parâmetros
            Dim sqlStr As String = $"SELECT {Campo_Id}, UPPER({CampoDescricao}) AS {CampoDescricao} FROM {ComplementoTipoBanco & Tabela} {consulta} ORDER BY {CampoDescricao}"

            '' Certifique-se de que o banco de dados esteja aberto
            'If cl_BancoDados.AbrirBanco = False Then
            '    ' Tenta abrir o banco de dados
            '    cl_BancoDados.AbrirBanco()
            'End If

            ' Criação do comando SQL e adaptador
            Using comando As New MySqlCommand(sqlStr, myconect)
                ' Abre a conexão do banco de dados de forma assíncrona
                Using reader As MySqlDataReader = Await comando.ExecuteReaderAsync()

                    ' Criação de um DataTable para armazenar os dados
                    Dim dt As New DataTable()
                    dt.Load(reader)

                    ' Configuração do ComboBox
                    ObjComboBox.DataSource = dt
                    ObjComboBox.DisplayMember = CampoDescricao.ToUpper()
                    ObjComboBox.ValueMember = Campo_Id

                    Return True
                End Using
            End Using

            cl_BancoDados.FecharBanco()
        Catch ex As Exception
            Debug.WriteLine("[ComboBoxDataSetAsync] Erro: " & ex.Message)
            Return False
        Finally
            cl_BancoDados.FecharBanco()
        End Try

    End Function

    Function ChekListBoxDataSet(ByVal Tabela As String, ByVal Campo_Id As String, ByVal CampoDescricao As String, ByRef ObjCheckedListBox As CheckedListBox, ByVal consulta As String) As Boolean

        Try

            cl_BancoDados.AbrirBanco()

            ' Construção da consulta SQL com parâmetros
            Dim sqlStr As String = $"SELECT {Campo_Id}, UPPER({CampoDescricao}) AS {CampoDescricao} FROM {ComplementoTipoBanco & Tabela} {consulta} ORDER BY {CampoDescricao}"

            If My.Settings.TipoConexao = "MYSQL" Then

                ' Criação do adaptador e do dataset
                Using da As New MySqlDataAdapter(sqlStr, myconect)
                    Dim ds As New DataSet()
                    da.Fill(ds, Tabela)

                    ' Limpa o CheckedListBox antes de adicionar os itens
                    ObjCheckedListBox.Items.Clear()

                    ' Percorre os dados e adiciona ao CheckedListBox
                    For Each row As DataRow In ds.Tables(Tabela).Rows
                        ' Adiciona cada item ao CheckedListBox com o texto sendo o CampoDescricao
                        ObjCheckedListBox.Items.Add(row(CampoDescricao).ToString(), False) ' Inicialmente desmarcado (False)
                    Next

                    Return True
                End Using

            ElseIf My.Settings.TipoConexao = "SQL" Then

                ' Criação do adaptador e do dataset
                Using da As New SqlDataAdapter(sqlStr, myconectSQL)
                    Dim ds As New DataSet()
                    da.Fill(ds, Tabela)

                    ' Limpa o CheckedListBox antes de adicionar os itens
                    ObjCheckedListBox.Items.Clear()

                    ' Percorre os dados e adiciona ao CheckedListBox
                    For Each row As DataRow In ds.Tables(Tabela).Rows
                        ' Adiciona cada item ao CheckedListBox com o texto sendo o CampoDescricao
                        ObjCheckedListBox.Items.Add(row(CampoDescricao).ToString(), False) ' Inicialmente desmarcado (False)
                    Next

                    Return True
                End Using

            End If

            cl_BancoDados.FecharBanco()
        Catch ex As Exception
            Debug.WriteLine("[ChekListBoxDataSet] Erro: " & ex.Message)
            Return False
        Finally
            cl_BancoDados.FecharBanco()
        End Try

    End Function

    Public Function SelecionarArquivoPDF() As String
        ' Cria o diálogo de seleção de arquivo
        Using dialog As New OpenFileDialog()
            dialog.Filter = "Arquivos PDF (*.pdf)|*.pdf" ' Filtra apenas arquivos PDF
            dialog.Title = "Selecione um arquivo PDF"
            dialog.Multiselect = False ' Permite selecionar apenas um arquivo

            ' Exibe a caixa de diálogo e verifica se o usuário clicou em OK
            If dialog.ShowDialog() = DialogResult.OK Then
                Return dialog.FileName ' Retorna o caminho do arquivo selecionado
            Else
                Return String.Empty ' Retorna vazio se o usuário cancelar
            End If
        End Using
    End Function

    Public Function CopiarArquivoInteligente(origem As String, destino As String) As Boolean

        Try

            origem = Path.GetFullPath(origem)
            destino = Path.GetFullPath(destino)

            Dim caminhosParaTentar As New List(Of String)

            ' Caminho original
            caminhosParaTentar.Add(origem)

            ' Nome do arquivo com espaço → %
            Dim pastaOrigem As String = Path.GetDirectoryName(origem)
            Dim nomeArquivo As String = Path.GetFileName(origem)

            If nomeArquivo.Contains(" ") Then
                Dim nomeComPorcento = nomeArquivo.Replace(" ", "%")
                caminhosParaTentar.Add(Path.Combine(pastaOrigem, nomeComPorcento))
            End If

            ' Nome do arquivo com % → espaço
            If nomeArquivo.Contains("%") Then
                Dim nomeComEspaco = nomeArquivo.Replace("%", " ")
                caminhosParaTentar.Add(Path.Combine(pastaOrigem, nomeComEspaco))
            End If

            ' Tenta todos os caminhos
            For Each caminho In caminhosParaTentar.Distinct()
                If File.Exists(caminho) Then
                    File.Copy(caminho, destino, True)
                    Return True
                End If
            Next

            MessageBox.Show("Arquivo não encontrado após múltiplas tentativas:" & vbCrLf &
                        String.Join(vbCrLf, caminhosParaTentar))
            Return False
        Catch ex As Exception
            MessageBox.Show("Erro ao copiar arquivo: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function RetornaCampoDaPesquisa(ByVal Valor_Para_Pesquisa As String,
                                           ByVal Campo0 As String,
                                           Optional Campo1 As String = "",
                                           Optional Campo2 As String = "",
                                           Optional Campo3 As String = "",
                                           Optional Campo4 As String = "",
                                           Optional Campo5 As String = "",
                                           Optional Campo6 As String = "",
                                           Optional Campo7 As String = "",
                                           Optional Campo8 As String = "",
                                           Optional Campo9 As String = "",
                                           Optional Campo10 As String = "")
        Try


            VCampo0 = ""
            VCampo1 = ""
            VCampo2 = ""
            VCampo3 = ""
            VCampo4 = ""
            VCampo5 = ""
            VCampo6 = ""
            VCampo7 = ""
            VCampo8 = ""
            VCampo9 = ""
            VCampo10 = ""


            cl_BancoDados.AbrirBanco()

            If My.Settings.TipoConexao = "MYSQL" Then

                Try

                    ' Criação do comando SQL
                    Dim daMysql As New MySqlCommand(Valor_Para_Pesquisa, myconect)

                    ' Execução da consulta e leitura dos dados
                    Using drMysql As MySqlDataReader = daMysql.ExecuteReader()
                        If drMysql.HasRows Then
                            drMysql.Read()

                            Try
                                VCampo0 = drMysql(Campo0).ToString()
                            Catch ex As Exception
                                VCampo0 = ""
                            End Try

                            ' drMysql(Campo_A_Retornar).ToString()

                            Try
                                VCampo1 = drMysql(Campo1).ToString()
                            Catch ex As Exception
                                VCampo1 = ""
                            End Try

                            Try
                                VCampo2 = drMysql(Campo2).ToString()
                            Catch ex As Exception
                                VCampo2 = ""
                            End Try

                            Try
                                VCampo3 = drMysql(Campo3).ToString()
                            Catch ex As Exception
                                VCampo3 = ""
                            End Try

                            Try
                                VCampo4 = drMysql(Campo4).ToString()
                            Catch ex As Exception
                                VCampo4 = ""
                            End Try

                            Try
                                VCampo5 = drMysql(Campo5).ToString()
                            Catch ex As Exception
                                VCampo5 = ""
                            End Try

                            Try
                                VCampo6 = drMysql(Campo5).ToString()
                            Catch ex As Exception
                                VCampo6 = ""
                            End Try

                            Try
                                VCampo7 = drMysql(Campo5).ToString()
                            Catch ex As Exception
                                VCampo7 = ""
                            End Try

                            Try
                                VCampo8 = drMysql(Campo5).ToString()
                            Catch ex As Exception
                                VCampo8 = ""
                            End Try

                            Try
                                VCampo9 = drMysql(Campo5).ToString()
                            Catch ex As Exception
                                VCampo9 = ""
                            End Try

                            Try
                                VCampo10 = drMysql(Campo5).ToString()
                            Catch ex As Exception
                                VCampo10 = ""
                            End Try
                        Else
                            Return Nothing
                        End If
                    End Using
                Catch ex As Exception

                    ClasseEmail.EmailTratamentoErro(ex.Message)
                Finally
                    ' Log do erro e retorno de Nothing em caso de falha
                    ' MessageBox.Show("Erro ao executar a pesquisa: " & ex.Message)
                    'Return Nothing
                End Try

            ElseIf My.Settings.TipoConexao = "SQL" Then

                Try

                    ' Criação do comando SQL
                    Dim dasql As New SqlCommand(Valor_Para_Pesquisa, myconectSQL)

                    ' Execução da consulta e leitura dos dados
                    Using drsql As SqlDataReader = dasql.ExecuteReader()
                        If drsql.HasRows Then
                            drsql.Read()
                            VCampo0 = drsql(Campo0).ToString()
                            ' drMysql(Campo_A_Retornar).ToString()

                            Try
                                VCampo1 = drsql(Campo1).ToString()
                            Catch ex As Exception
                                VCampo1 = ""
                            End Try

                            Try
                                VCampo2 = drsql(Campo2).ToString()
                            Catch ex As Exception
                                VCampo2 = ""
                            End Try

                            Try
                                VCampo3 = drsql(Campo3).ToString()
                            Catch ex As Exception
                                VCampo3 = ""
                            End Try

                            Try
                                VCampo4 = drsql(Campo4).ToString()
                            Catch ex As Exception
                                VCampo4 = ""
                            End Try

                            Try
                                VCampo5 = drsql(Campo5).ToString()
                            Catch ex As Exception
                                VCampo5 = ""
                            End Try

                            Try
                                VCampo6 = drsql(Campo5).ToString()
                            Catch ex As Exception
                                VCampo6 = ""
                            End Try

                            Try
                                VCampo7 = drsql(Campo5).ToString()
                            Catch ex As Exception
                                VCampo7 = ""
                            End Try
                        Else
                            Return Nothing
                        End If
                    End Using
                Catch ex As Exception

                    ClasseEmail.EmailTratamentoErro(ex.Message)
                Finally

                End Try

            ElseIf My.Settings.TipoConexao = "SQLCLIENTE" Then
                Try

                    ' Criação do comando SQL
                    Dim dasql As New SqlCommand(Valor_Para_Pesquisa, myconectSQL)

                    ' Execução da consulta e leitura dos dados
                    Using drsql As SqlDataReader = dasql.ExecuteReader()
                        If drsql.HasRows Then
                            drsql.Read()

                            Try
                                VCampo1 = drsql(Campo1).ToString()
                            Catch ex As Exception
                                VCampo1 = ""
                            End Try

                            Try
                                VCampo2 = drsql(Campo2).ToString()
                            Catch ex As Exception
                                VCampo2 = ""
                            End Try

                            Try
                                VCampo3 = drsql(Campo3).ToString()
                            Catch ex As Exception
                                VCampo3 = ""
                            End Try

                            Try
                                VCampo4 = drsql(Campo4).ToString()
                            Catch ex As Exception
                                VCampo4 = ""
                            End Try

                            Try
                                VCampo5 = drsql(Campo5).ToString()
                            Catch ex As Exception
                                VCampo5 = ""
                            End Try

                            Try
                                VCampo6 = drsql(Campo5).ToString()
                            Catch ex As Exception
                                VCampo6 = ""
                            End Try

                            Try
                                VCampo7 = drsql(Campo5).ToString()
                            Catch ex As Exception
                                VCampo7 = ""
                            End Try
                        Else
                            Return Nothing
                        End If
                    End Using
                Catch ex As Exception
                    ClasseEmail.EmailTratamentoErro(ex.Message)
                    Return Nothing
                End Try
            ElseIf My.Settings.TipoConexao = "ACCESS" Then

                Try
                    ' Criação do comando SQL para Access
                    Dim daAccess As New OleDbCommand(Valor_Para_Pesquisa, myconectAccess)

                    ' Verifica se a conexão está fechada e abre se necessário
                    If myconectAccess.State = ConnectionState.Closed Then
                        myconectAccess.Open()
                    End If

                    ' Execução da consulta e leitura dos dados
                    Using drAccess As OleDbDataReader = daAccess.ExecuteReader()
                        If drAccess.HasRows Then
                            drAccess.Read()

                            Try
                                VCampo1 = drAccess(Campo1).ToString()
                            Catch ex As Exception
                                VCampo1 = ""
                            End Try

                            Try
                                VCampo2 = drAccess(Campo2).ToString()
                            Catch ex As Exception
                                VCampo2 = ""
                            End Try

                            Try
                                VCampo3 = drAccess(Campo3).ToString()
                            Catch ex As Exception
                                VCampo3 = ""
                            End Try

                            Try
                                VCampo4 = drAccess(Campo4).ToString()
                            Catch ex As Exception
                                VCampo4 = ""
                            End Try

                            Try
                                VCampo5 = drAccess(Campo5).ToString()
                            Catch ex As Exception
                                VCampo5 = ""
                            End Try

                            Try
                                VCampo6 = drAccess(Campo5).ToString()
                            Catch ex As Exception
                                VCampo6 = ""
                            End Try

                            Try
                                VCampo7 = drAccess(Campo5).ToString()
                            Catch ex As Exception
                                VCampo7 = ""
                            End Try
                        Else
                            Return Nothing
                        End If
                    End Using
                Catch ex As Exception
                    ClasseEmail.EmailTratamentoErro(ex.Message)
                    Return Nothing
                Finally
                    ' Fecha a conexão, se estiver aberta
                    If myconectAccess.State = ConnectionState.Open Then
                        myconectAccess.Close()
                    End If
                End Try

            End If

            cl_BancoDados.FecharBanco()
        Catch ex As Exception
        Finally

            cl_BancoDados.FecharBanco()
        End Try

    End Function

    Public Function VerificaSaldoTag(ByVal IdTag As String) As String

        Try

            VerificaSaldoTag = 0

            cl_BancoDados.AbrirBanco()

            If My.Settings.TipoConexao = "MYSQL" Then

                Try

                    ' Criação do comando SQL
                    Dim daMysql As New MySqlCommand("SELECT QtdeTag, SaldoTag,QtdeLiberada,desctag,DataPrevisao FROM  " & ComplementoTipoBanco & "tags where IdTag = '" & IdTag & "'", myconect)

                    ' Execução da consulta e leitura dos dados
                    Using drMysql As MySqlDataReader = daMysql.ExecuteReader()

                        If drMysql.HasRows Then
                            drMysql.Read()
                            OrdemServico.QtdeTag = Convert.ToInt32(drMysql("QtdeTag").ToString())
                            OrdemServico.SaldoTag = Convert.ToInt32(drMysql("SaldoTag").ToString())
                            OrdemServico.QtdeLiberada = Convert.ToInt32(drMysql("QtdeLiberada").ToString())
                            OrdemServico.Descricao = drMysql("desctag").ToString()
                            OrdemServico.DataPrevisao = drMysql("DataPrevisao").ToString()

                        End If

                    End Using
                Catch ex As Exception

                    ClasseEmail.EmailTratamentoErro(ex.Message)
                Finally

                End Try

            End If

            cl_BancoDados.FecharBanco()
        Catch ex As Exception
        Finally
            cl_BancoDados.FecharBanco()

        End Try

    End Function

    Public Function ValidarEmail(email As String) As Boolean
        Dim pattern As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
        Dim regex As New Regex(pattern)
        Return regex.IsMatch(email)
    End Function

    'Private ReadOnly _existCacheDxfPdf As New ConcurrentDictionary(Of String, (HasDxf As Boolean, HasPdf As Boolean))(StringComparer.OrdinalIgnoreCase)
    'Private _prewarmVersion As Integer = 0 ' invalida execuções antigas quando a fonte de dados muda

    ' (opcional) cache de ícones por linha – só se quiser
    ' P

    Public Sub FormatarDataGridView(dataGridView As DataGridView, GradeCores As String)
        ' Ajustar automaticamente o tamanho das colunas
        '  dataGridView.AutoResizeColumns()

        ' Percorrer todas as colunas e ajustar o formato do conteúdo conforme necessário
        'For Each column As DataGridViewColumn In dataGridView.Columns
        '    If column.ValueType = GetType(DateTime) Then
        '        ' Se a coluna contém datas, definir o formato de exibição para um formato legível
        '        column.DefaultCellStyle.Format = "dd/MM/yyyy"
        '    ElseIf column.ValueType = GetType(Decimal) Then
        '        ' Se a coluna contém números decimais, definir o formato de exibição para um formato legível
        '        column.DefaultCellStyle.Format = "N2"
        '    ElseIf column.ValueType = GetType(Double) OrElse column.ValueType = GetType(Single) Then
        '        ' Se a coluna contém números de ponto flutuante, definir o formato de exibição para um formato legível
        '        column.DefaultCellStyle.Format = "F2"
        '    End If

        '    column.ReadOnly = True

        '    column.HeaderText = StrConv(column.HeaderText, VbStrConv.ProperCase)

        'Next

        With dataGridView

            'cobeçalho
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Geist UI", 8, FontStyle.Italic) ' Define a fonte e negrito
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter   ' Centraliza o texto
            .ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White
            '.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#32423D") '#32423D,#425750
            .ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#6F7170") '#32423D,#425750

            .EnableHeadersVisualStyles = False ' Permite personalizar o cabeçalho
            'Permite quebra de linha
            .ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True

            'Se quiser ajustar a altura manualmente ao invés de deixar automático:
            .ColumnHeadersHeight = 75 ' ou o valor que quiser
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
            ' .AutoResizeRows(DataGridViewAutoSizeRowsMode.None)

            If GradeCores = "SIM" Then
                'Dados
                .DefaultCellStyle.Font = New System.Drawing.Font("Geist UI", 8, FontStyle.Italic)
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft  ' Centraliza o texto

                ' Estilo de linha de grade (as linhas de divisão)
                .CellBorderStyle = DataGridViewCellBorderStyle.Single   ' Define o estilo da borda das células (pode ser: None, Single, etc.)
                .GridColor = System.Drawing.Color.Gray  ' Cor das linhas de grade

                ' Formatar as linhas de divisão horizontais
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single ' Estilo da borda das linhas de cabeçalho

                ' Formatar as linhas de divisão verticais
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None  ' Estilo da borda do cabeçalho de coluna

                ' Formatar a grade de divisão entre as células de dados
                .EnableHeadersVisualStyles = False ' Desativa a aparência do cabeçalho para customizar
                .AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#F9F8F3")
                .DefaultCellStyle.SelectionBackColor = System.Drawing.ColorTranslator.FromHtml("#6F7170") 'System.Drawing.Color.LightBlue ' Cor de fundo ao selecionar a célula
                .DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White ' Cor da fonte ao selecionar a célula

                'seleção da linha
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                ' .ClearSelection()
                ' .CurrentCell = Nothing

                'Oculta a coluna a esquerda antes da coluna de dados
                .RowHeadersVisible = False

            End If

        End With

    End Sub

    Public Function AlteracaoEspecifica(tabela As String, campoTabela As String, NovoValor As String, campo_id As String, Valor_id As String) As Boolean

        Try

            cl_BancoDados.AbrirBanco()

            If My.Settings.TipoConexao = "MYSQL" Then

                Try
                    AlteracaoEspecifica = False

                    ' Construção da consulta SQL usando parâmetros para prevenir SQL Injection
                    Dim sql As String = $"UPDATE {tabela} SET {campoTabela} = @NovoValor WHERE {campo_id} = @Valor_id"

                    ' Criação do comando SQL com o uso de parâmetros
                    Using mycomand As New MySqlCommand(sql.ToLower(), myconect)
                        mycomand.Parameters.AddWithValue("@NovoValor", NovoValor)
                        mycomand.Parameters.AddWithValue("@Valor_id", Valor_id)

                        ' Execução do comando SQL
                        mycomand.ExecuteNonQuery()
                    End Using

                    AlteracaoEspecifica = True
                Catch ex As Exception
                    '   MsgBox($"Erro ao atualizar o registro: {ex.Message}", vbCritical, "Erro")
                    AlteracaoEspecifica = False
                Finally
                End Try

            ElseIf My.Settings.TipoConexao = "SQL" Then

                Try
                    AlteracaoEspecifica = False

                    ' Construção da consulta SQL usando parâmetros para prevenir SQL Injection
                    Dim sql As String = $"UPDATE {tabela} SET {campoTabela} = @NovoValor WHERE {campo_id} = @Valor_id"

                    ' Criação do comando SQL com o uso de parâmetros
                    Using mycomand As New SqlCommand(sql.ToLower(), myconectSQL)
                        mycomand.Parameters.AddWithValue("@NovoValor", NovoValor)
                        mycomand.Parameters.AddWithValue("@Valor_id", Valor_id)

                        ' Execução do comando SQL
                        mycomand.ExecuteNonQuery()
                    End Using

                    AlteracaoEspecifica = True
                Catch ex As Exception
                    '   MsgBox($"Erro ao atualizar o registro: {ex.Message}", vbCritical, "Erro")
                    AlteracaoEspecifica = False
                Finally
                End Try

            ElseIf My.Settings.TipoConexao = "ACCESS" Then

                Try

                    ' Construção da consulta SQL usando parâmetros para prevenir SQL Injection
                    Dim sql As String = $"UPDATE {tabela} SET {campoTabela} = @NovoValor WHERE {campo_id} = @Valor_id"

                    ' Verifica se a conexão está fechada e abre se necessário
                    If myconectAccess.State = ConnectionState.Closed Then
                        myconectAccess.Open()
                    End If

                    ' Criação do comando SQL com o uso de parâmetros para Access
                    Using mycomand As New OleDbCommand(sql, myconectAccess)
                        mycomand.Parameters.AddWithValue("@NovoValor", NovoValor)
                        mycomand.Parameters.AddWithValue("@Valor_id", Valor_id)
                        AlteracaoEspecifica = True
                        ' Execução do comando SQL
                        mycomand.ExecuteNonQuery()
                    End Using
                Catch ex As Exception
                    ' MsgBox($"Erro ao atualizar o registro: {ex.Message}", vbCritical, "Erro")
                    AlteracaoEspecifica = False
                Finally
                    ' Fecha a conexão, se estiver aberta
                    If myconectAccess.State = ConnectionState.Open Then
                        myconectAccess.Close()
                    End If
                End Try

            End If

            Return AlteracaoEspecifica

            cl_BancoDados.FecharBanco()
        Catch ex As Exception
        Finally
            cl_BancoDados.FecharBanco()

        End Try

    End Function

    Public Function AlteracaoEspecificaDelete(tabela As String, campo_id As Integer, Valor_id As String) As Boolean

        Try

            cl_BancoDados.AbrirBanco()

            ' Construção da consulta SQL usando parâmetros para prevenir SQL Injection
            Dim sql As String = $"UPDATE {tabela} SET D_E_L_E_T_E = @D_E_L_E_T_E,
                                                          UsuarioD_E_L_E_T_E = @UsuarioD_E_L_E_T_E,
                                                           DataD_E_L_E_T_E = @DataD_E_L_E_T_E
                                                           WHERE {campo_id} = @Valor_id"

            If My.Settings.TipoConexao = "MYSQL" Then

                ' Criação do comando SQL com o uso de parâmetros
                Using mycomand As New MySqlCommand(sql.ToLower(), myconect)
                    mycomand.Parameters.AddWithValue("@D_E_L_E_T_E", "*")
                    mycomand.Parameters.AddWithValue("@UsuarioD_E_L_E_T_E", Usuario.NomeCompleto)
                    mycomand.Parameters.AddWithValue("@DataD_E_L_E_T_E", Date.Now.Date)

                    ' Execução do comando SQL
                    mycomand.ExecuteNonQuery()

                End Using

            ElseIf My.Settings.TipoConexao = "SQL" Then

                ' Criação do comando SQL com o uso de parâmetros
                Using mycomand As New SqlCommand(sql.ToLower(), myconectSQL)
                    mycomand.Parameters.AddWithValue("@D_E_L_E_T_E", "*")
                    mycomand.Parameters.AddWithValue("@UsuarioD_E_L_E_T_E", Usuario.NomeCompleto)
                    mycomand.Parameters.AddWithValue("@DataD_E_L_E_T_E", Date.Now.Date)

                    ' Execução do comando SQL
                    mycomand.ExecuteNonQuery()
                End Using

            ElseIf My.Settings.TipoConexao = "ACCESS" Then

                ' Criação do comando SQL com o uso de parâmetros para Access
                Using mycomand As New OleDbCommand(sql, myconectAccess)
                    mycomand.Parameters.AddWithValue("@D_E_L_E_T_E", "*")
                    mycomand.Parameters.AddWithValue("@UsuarioD_E_L_E_T_E", Usuario.NomeCompleto)
                    mycomand.Parameters.AddWithValue("@DataD_E_L_E_T_E", Date.Now.Date)

                    mycomand.ExecuteNonQuery()
                End Using

            End If
            Return True

            cl_BancoDados.FecharBanco()
        Catch ex As Exception
        Finally
            cl_BancoDados.FecharBanco()

        End Try

    End Function

    Public Function AlteracaoEspecificaDadosOS(ByVal IDOrdemServicoItem As String, ByVal QtdeTotal As String, AreaPintura As String, ByVal Peso As String, ByVal Fator As String) As Boolean
        Try

            cl_BancoDados.AbrirBanco()

            If My.Settings.TipoConexao = "MYSQL" Then

                Try
                    AlteracaoEspecificaDadosOS = False

                    ' Construção da consulta SQL usando parâmetros para prevenir SQL Injection
                    Dim sql As String = $"UPDATE ordemservicoitem SET QtdeTotal = @QtdeTotal,
                                                                          AreaPintura = @AreaPintura,
                                                                          Peso = @Peso,
                                                                          Fator = @Fator
                                                                          WHERE IDOrdemServicoItem = @IDOrdemServicoItem"

                    ' Criação do comando SQL com o uso de parâmetros
                    Using mycomand As New MySqlCommand(sql.ToLower(), myconect)
                        mycomand.Parameters.AddWithValue("@QtdeTotal", Replace(QtdeTotal, ",", "."))
                        mycomand.Parameters.AddWithValue("@AreaPintura", Replace(AreaPintura, ",", "."))
                        mycomand.Parameters.AddWithValue("@Peso", Replace(Peso, ",", "."))
                        mycomand.Parameters.AddWithValue("@Fator", Replace(Fator, ",", "."))
                        mycomand.Parameters.AddWithValue("@IDOrdemServicoItem", IDOrdemServicoItem)
                        ' Execução do comando SQL
                        mycomand.ExecuteNonQuery()
                    End Using

                    AlteracaoEspecificaDadosOS = True
                Catch ex As Exception
                    '   MsgBox($"Erro ao atualizar o registro: {ex.Message}", vbCritical, "Erro")
                Finally
                End Try

            ElseIf My.Settings.TipoConexao = "MYSQL" Then

                Try
                    AlteracaoEspecificaDadosOS = False

                    ' Construção da consulta SQL usando parâmetros para prevenir SQL Injection
                    Dim sql As String = $"UPDATE ordemservicoitem SET QtdeTotal = @QtdeTotal,
                                                                          AreaPintura = @AreaPintura,
                                                                          Peso = @Peso
                                                                          WHERE IDOrdemServicoItem = @IDOrdemServicoItem"

                    ' Criação do comando SQL com o uso de parâmetros
                    Using mycomand As New SqlCommand(sql.ToLower(), myconectSQL)
                        mycomand.Parameters.AddWithValue("@QtdeTotal", QtdeTotal)
                        mycomand.Parameters.AddWithValue("@AreaPintura", AreaPintura)
                        mycomand.Parameters.AddWithValue("@Peso", Peso)
                        mycomand.Parameters.AddWithValue("@IDOrdemServicoItem", IDOrdemServicoItem)
                        ' Execução do comando SQL
                        mycomand.ExecuteNonQuery()
                    End Using

                    AlteracaoEspecificaDadosOS = True
                Catch ex As Exception
                    '   MsgBox($"Erro ao atualizar o registro: {ex.Message}", vbCritical, "Erro")
                Finally
                End Try
            ElseIf My.Settings.TipoConexao = "ACCESS" Then

            End If

            cl_BancoDados.FecharBanco()
        Catch ex As Exception
        Finally
            cl_BancoDados.FecharBanco()

        End Try

    End Function

    Public Function AlteracaoEspecificaDadosOSProduto(ByVal IdOrdemServico As String) As Boolean

        Try

            cl_BancoDados.AbrirBanco()

            If My.Settings.TipoConexao = "MYSQL" Then

                Try
                    AlteracaoEspecificaDadosOSProduto = False

                    ' Construção da consulta SQL usando parâmetros para prevenir SQL Injection
                    Dim sql As String = $"UPDATE ordemservico SET ProdutoPadrao = @ProdutoPadrao,
                                                                          CodDesenhoProduto = @CodDesenhoProduto,
                                                                          CodOmie = @CodOmie,
                                                                          DescricaoProduto = @DescricaoProduto,
                                                                          EnderecoFichaTecnica = @EnderecoFichaTecnica,
                                                                          EnderecoIsometrico = @EnderecoIsometrico,
                                                                          ProdutoCriadoPor = @ProdutoCriadoPor,
                                                                          DataCriacaoProduto = @DataCriacaoProduto
                                                                          WHERE IdOrdemServico = @IdOrdemServico"

                    ' Criação do comando SQL com o uso de parâmetros
                    Using mycomand As New MySqlCommand(sql.ToLower(), myconect)
                        mycomand.Parameters.AddWithValue("@ProdutoPadrao", OrdemServico.CodDesenhoProduto)
                        mycomand.Parameters.AddWithValue("@CodDesenhoProduto", OrdemServico.CodDesenhoProduto)
                        mycomand.Parameters.AddWithValue("@CodOmie", OrdemServico.CodOmie)
                        mycomand.Parameters.AddWithValue("@DescricaoProduto", OrdemServico.DescricaoProduto)
                        mycomand.Parameters.AddWithValue("@EnderecoFichaTecnica", OrdemServico.EnderecoFichaTecnica)
                        mycomand.Parameters.AddWithValue("@EnderecoIsometrico", OrdemServico.EnderecoIsometrico)
                        mycomand.Parameters.AddWithValue("@ProdutoCriadoPor", OrdemServico.ProdutoCriadoPor)
                        mycomand.Parameters.AddWithValue("@DataCriacaoProduto", OrdemServico.DataCriacaoProduto)
                        mycomand.Parameters.AddWithValue("@IdOrdemServico", IdOrdemServico)
                        ' Execução do comando SQL
                        mycomand.ExecuteNonQuery()
                    End Using

                    'AlteracaoEspecificaDadosOS = True
                Catch ex As Exception
                    '   MsgBox($"Erro ao atualizar o registro: {ex.Message}", vbCritical, "Erro")
                Finally
                End Try

            ElseIf My.Settings.TipoConexao = "SQL" Then

                Try
                    AlteracaoEspecificaDadosOSProduto = False

                    ' Construção da consulta SQL usando parâmetros para prevenir SQL Injection
                    Dim sql As String = $"UPDATE ordemservico SET ProdutoPadrao = @ProdutoPadrao,
                                                                          CodDesenhoProduto = @CodDesenhoProduto,
                                                                          CodOmie = @CodOmie,
                                                                          DescricaoProduto = @DescricaoProduto,
                                                                          EnderecoFichaTecnica = @EnderecoFichaTecnica,
                                                                          EnderecoIsometrico = @EnderecoIsometrico,
                                                                          ProdutoCriadoPor = @ProdutoCriadoPor,
                                                                          DataCriacaoProduto = @DataCriacaoProduto
                                                                          WHERE IdOrdemServico = @IdOrdemServico"

                    ' Criação do comando SQL com o uso de parâmetros
                    Using mycomand As New SqlCommand(sql.ToLower(), myconectSQL)
                        mycomand.Parameters.AddWithValue("@ProdutoPadrao", OrdemServico.ProdutoPadrao)
                        mycomand.Parameters.AddWithValue("@CodDesenhoProduto", OrdemServico.CodDesenhoProduto)
                        mycomand.Parameters.AddWithValue("@CodOmie", OrdemServico.CodOmie)
                        mycomand.Parameters.AddWithValue("@DescricaoProduto", OrdemServico.DescricaoProduto)
                        mycomand.Parameters.AddWithValue("@EnderecoFichaTecnica", OrdemServico.EnderecoFichaTecnica)
                        mycomand.Parameters.AddWithValue("@EnderecoIsometrico", OrdemServico.EnderecoIsometrico)
                        mycomand.Parameters.AddWithValue("@ProdutoCriadoPor", OrdemServico.ProdutoCriadoPor)
                        mycomand.Parameters.AddWithValue("@DataCriacaoProduto", OrdemServico.DataCriacaoProduto)
                        mycomand.Parameters.AddWithValue("@IdOrdemServico", IdOrdemServico)
                        ' Execução do comando SQL
                        mycomand.ExecuteNonQuery()
                    End Using

                    'AlteracaoEspecificaDadosOS = True
                Catch ex As Exception
                    '   MsgBox($"Erro ao atualizar o registro: {ex.Message}", vbCritical, "Erro")
                Finally
                End Try

            ElseIf My.Settings.TipoConexao = "ACCESS" Then

            End If

            cl_BancoDados.FecharBanco()
        Catch ex As Exception
        Finally
            cl_BancoDados.FecharBanco()
        End Try

    End Function

    Function FormatarPara5Caracteres(numero As String) As String
        Return numero.PadLeft(5, "0")
    End Function

    Function FormatarPara7Caracteres(numero As String) As String
        Return numero.PadLeft(7, "0")
    End Function

    Function FormatarPara6Caracteres(numero As String) As String
        Return numero.PadLeft(6, "0")
    End Function

    Public Function FecharArquivoMemoria() As Boolean
        Try
            ' Verifica se o objeto swapp foi inicializado
            If swapp IsNot Nothing Then
                ' Verifica se há um documento aberto
                If Not String.IsNullOrEmpty(DadosArquivoCorrente.EnderecoArquivo) Then
                    ' Fecha o documento aberto
                    swapp.CloseDoc(DadosArquivoCorrente.EnderecoArquivo)
                End If

                ' Libera o objeto COM do SolidWorks
                System.Runtime.InteropServices.Marshal.ReleaseComObject(swapp)
                swapp = Nothing
            End If

            ' Força a coleta de lixo e aguarda a finalização dos objetos
            GC.Collect()
            GC.WaitForPendingFinalizers()

            Return True ' Indica que o arquivo foi fechado com sucesso
        Catch ex As Exception
            ' Log de erro opcional ou tratamento de exceção
            ' MsgBox("Erro ao fechar o arquivo: " & ex.Message)
            Return False ' Indica que houve um erro ao fechar o arquivo
        End Try
    End Function

    Public Sub ProcessarArquivosDGV(ByVal ObjDgvGrdi As DataGridView)
        ' Iterar sobre as linhas do DataGridView
        For Each row As DataGridViewRow In ObjDgvGrdi.Rows
            ' Pular linhas vazias ou não válidas
            If row.IsNewRow Then Continue For

            Dim enderecoArquivo As String = Convert.ToString(row.Cells("EnderecoArquivo").Value)
            If String.IsNullOrEmpty(enderecoArquivo) Then Continue For

            ' Cache das células para evitar múltiplas buscas
            Dim celulaDXF = row.Cells("DGVDXF")
            Dim celulaPDF = row.Cells("DGVPDF")

            ' Verificar extensões
            Dim extensao As String = Path.GetExtension(enderecoArquivo)?.ToUpperInvariant()
            If extensao = ".SLDPRT" OrElse extensao = ".SLDASM" Then
                ' Processar arquivo DXF
                Dim caminhoDXF = Path.ChangeExtension(enderecoArquivo, ".dxf")
                celulaDXF.Value = If(File.Exists(caminhoDXF), My.Resources.arquivo_dxf, My.Resources.Sem_Incone)

                ' Processar arquivo PDF
                Dim caminhoPDF = Path.ChangeExtension(enderecoArquivo, ".pdf")
                celulaPDF.Value = If(File.Exists(caminhoPDF), My.Resources.ficheiro_pdf, My.Resources.Sem_Incone)
            Else
                ' Resetar células se a extensão não for válida
                celulaDXF.Value = My.Resources.Sem_Incone
                celulaPDF.Value = My.Resources.Sem_Incone
            End If
        Next
    End Sub

    Public Function converteStringParaDouble(ByVal valor As String) As Double
        Dim valorConvertido As Double = 0.0 ' Inicializa com 0.0 como valor padrão

        Try
            ' Verifica se a string não está vazia
            If Not String.IsNullOrEmpty(valor) Then
                ' Substitui vírgulas por pontos (se necessário) antes da conversão
                valor = valor.Replace(",", ".")

                ' Converte a string para Double usando a cultura invariável (garante ponto como separador decimal)
                valorConvertido = Convert.ToDouble(valor, CultureInfo.InvariantCulture)
            End If
        Catch ex As Exception
            ' Em caso de erro, o valor será 0.0
            valorConvertido = 0.0
        End Try

        ' Retorna o valor convertido
        Return valorConvertido
    End Function

    Public Sub TornarPastaSomenteLeitura(caminho As String)
        If Directory.Exists(caminho) Then
            Dim atributos As FileAttributes = File.GetAttributes(caminho)
            File.SetAttributes(caminho, atributos Or FileAttributes.ReadOnly)
            '  MessageBox.Show("Pasta marcada como somente leitura.")
            ' Else
            '  MessageBox.Show("Pasta não encontrada.")
        End If
    End Sub

    Public Sub RemoverSomenteLeitura(caminho As String)
        If Directory.Exists(caminho) Then
            Dim atributos As FileAttributes = File.GetAttributes(caminho)
            File.SetAttributes(caminho, atributos And Not FileAttributes.ReadOnly)
            '  MessageBox.Show("Atributo somente leitura removido.")
            ' Else
            '  MessageBox.Show("Pasta não encontrada.")
        End If
    End Sub

    'Exemplo VB.NET: Bloquear edição de arquivos dentro da pasta
    Public Sub BloquearEscritaNaPasta(caminho As String)
        If Directory.Exists(caminho) Then
            Dim dirInfo As New DirectoryInfo(caminho)
            Dim dirSecurity As DirectorySecurity = dirInfo.GetAccessControl()

            ' Remove permissão de gravação para todos
            dirSecurity.AddAccessRule(New FileSystemAccessRule(
                "Everyone",
                FileSystemRights.Write Or FileSystemRights.Delete Or FileSystemRights.CreateFiles,
                InheritanceFlags.ContainerInherit Or InheritanceFlags.ObjectInherit,
                PropagationFlags.None,
                AccessControlType.Deny))

            dirInfo.SetAccessControl(dirSecurity)

            ' MessageBox.Show("Gravação bloqueada com sucesso.")
            ' Else
            '  MessageBox.Show("Pasta não encontrada.")
        End If
    End Sub

    ' Para permitir novamente (reverter bloqueio):
    Public Sub PermitirEscritaNaPasta(caminho As String)
        If Directory.Exists(caminho) Then
            Dim dirInfo As New DirectoryInfo(caminho)
            Dim dirSecurity As DirectorySecurity = dirInfo.GetAccessControl()

            dirSecurity.RemoveAccessRule(New FileSystemAccessRule(
            "Everyone",
            FileSystemRights.Write Or FileSystemRights.Delete Or FileSystemRights.CreateFiles,
            InheritanceFlags.ContainerInherit Or InheritanceFlags.ObjectInherit,
            PropagationFlags.None,
            AccessControlType.Deny))

            dirInfo.SetAccessControl(dirSecurity)

            'MessageBox.Show("Gravação liberada.")
            ' Else
            ' MessageBox.Show("Pasta não encontrada.")
        End If
    End Sub

    Public Sub SalvarDadosMaterial(DescDetal, Peso, Unidade, CodMatFabricante, CodigoJuridicoMat,
                                                    TotalValor, DescFamilia, PercIPI, vIPI, PercICMS,
                                                vICMS, vLiquido, D_E_L_E_T_E, txtTipoDesenho, NumeroRP,
                                                UsuarioCriacao, DtCad, Comprimentocaixadelimitadora,
                                               Larguracaixadelimitadora, Espessuracaixadelimitadora,
                                               Altura, Largura, Profundidade, PesoMaisEmbalagem)

        Dim query As String = "INSERT INTO " & ComplementoTipoBanco & "material (" &
"DescDetal, Peso, Unidade, CodMatFabricante, CodigoJuridicoMat, TotalValor, DescFamilia, PercIPI, vIPI, PercICMS, vICMS, vLiquido, " &
"D_E_L_E_T_E, txtTipoDesenho, NumeroRP, UsuarioCriacao, DtCad, PecaManuFat, Comprimentocaixadelimitadora, Larguracaixadelimitadora, " &
"Espessuracaixadelimitadora, Altura, Largura, Profundidade, PesoMaisEmbalagem) VALUES (" &
"@DescDetal, @Peso, @Unidade, @CodMatFabricante, @CodigoJuridicoMat, @TotalValor, @DescFamilia, @PercIPI, @vIPI, @PercICMS, @vICMS, @vLiquido, " &
"@D_E_L_E_T_E, @txtTipoDesenho, @NumeroRP, @UsuarioCriacao, @DtCad, @PecaManuFat, @Comprimentocaixadelimitadora, @Larguracaixadelimitadora, " &
"@Espessuracaixadelimitadora, @Altura, @Largura, @Profundidade, @PesoMaisEmbalagem) ON DUPLICATE KEY UPDATE " &
"DescDetal = VALUES(DescDetal), Peso = VALUES(Peso), Unidade = VALUES(Unidade), CodMatFabricante = VALUES(CodMatFabricante), " &
"CodigoJuridicoMat = VALUES(CodigoJuridicoMat), TotalValor = VALUES(TotalValor), DescFamilia = VALUES(DescFamilia), PercIPI = VALUES(PercIPI), " &
"vIPI = VALUES(vIPI), PercICMS = VALUES(PercICMS), vICMS = VALUES(vICMS), vLiquido = VALUES(vLiquido), D_E_L_E_T_E = VALUES(D_E_L_E_T_E), " &
"txtTipoDesenho = VALUES(txtTipoDesenho), NumeroRP = VALUES(NumeroRP), UsuarioCriacao = VALUES(UsuarioCriacao), DtCad = VALUES(DtCad), " &
"PecaManuFat = VALUES(PecaManuFat), Comprimentocaixadelimitadora = VALUES(Comprimentocaixadelimitadora), " &
"Larguracaixadelimitadora = VALUES(Larguracaixadelimitadora), Espessuracaixadelimitadora = VALUES(Espessuracaixadelimitadora), " &
"Altura = VALUES(Altura), Largura = VALUES(Largura), Profundidade = VALUES(Profundidade), PesoMaisEmbalagem = VALUES(PesoMaisEmbalagem)"

        Using da As New MySqlCommand(query, myconect)
            ' Defina os valores para os parâmetros
            da.Parameters.AddWithValue("@DescDetal", DescDetal)
            da.Parameters.AddWithValue("@Peso", Peso)
            da.Parameters.AddWithValue("@Unidade", Unidade)
            da.Parameters.AddWithValue("@CodMatFabricante", CodMatFabricante)
            da.Parameters.AddWithValue("@CodigoJuridicoMat", CodigoJuridicoMat)
            da.Parameters.AddWithValue("@TotalValor", TotalValor)
            da.Parameters.AddWithValue("@DescFamilia", DescFamilia)
            da.Parameters.AddWithValue("@PercIPI", PercIPI)
            da.Parameters.AddWithValue("@vIPI", vIPI)
            da.Parameters.AddWithValue("@PercICMS", PercICMS)
            da.Parameters.AddWithValue("@vICMS", vICMS)
            da.Parameters.AddWithValue("@vLiquido", vLiquido)
            da.Parameters.AddWithValue("@D_E_L_E_T_E", D_E_L_E_T_E)
            da.Parameters.AddWithValue("@NumeroRP", NumeroRP)
            da.Parameters.AddWithValue("@txtTipoDesenho", txtTipoDesenho)
            da.Parameters.AddWithValue("@UsuarioCriacao", UsuarioCriacao)
            da.Parameters.AddWithValue("@DtCad", DtCad)
            da.Parameters.AddWithValue("@PecaManuFat", "")

            da.Parameters.AddWithValue("@Comprimentocaixadelimitadora", Comprimentocaixadelimitadora)
            da.Parameters.AddWithValue("@Larguracaixadelimitadora", Larguracaixadelimitadora)
            da.Parameters.AddWithValue("@Espessuracaixadelimitadora", Espessuracaixadelimitadora)
            da.Parameters.AddWithValue("@Altura", Altura)
            da.Parameters.AddWithValue("@Largura", Largura)
            da.Parameters.AddWithValue("@Profundidade", Profundidade)
            da.Parameters.AddWithValue("@PesoMaisEmbalagem", PesoMaisEmbalagem)

            da.ExecuteNonQuery()

        End Using

    End Sub

End Class

Public Class CalculoBancoDados

    Public Function CalcularordemservicoitemFator(ByVal IdOrdemServico As Integer, ByVal Fator As Double) As Boolean
        Try

            ' 1) Atualiza os itens da OS (recalcula pelos unitários x fator)
            Dim query As String =
"UPDATE ordemservicoitem
    SET
        Fator        = @Fator,
        AreaPintura  = ROUND(AreaPinturaUnitario * @Fator, 4),
        Peso         = ROUND(PesoUnitario        * @Fator, 4),
        QtdeTotal    = ROUND(Qtde                * @Fator, 4)
    WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '') and (enderecoarquivo <> '')
      AND IdOrdemServico = @Id;"

            Using cmd As New MySqlCommand(query, myconect)
                cmd.Parameters.Add("@Fator", MySqlDbType.Decimal).Value = Fator
                cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = IdOrdemServico

                cmd.ExecuteNonQuery()

            End Using

            ' 2) Atualiza o cabeçalho da ordemservico com os agregados dos itens
            query =
"UPDATE ordemservico os
    LEFT JOIN (
        SELECT
            IdOrdemServico,
            COALESCE(SUM(AreaPintura), 0) AS AreaPinturaTotal,
            COALESCE(SUM(Peso),        0) AS PesoTotal,
            COUNT(*)                       AS QtdeTotalItens,
            COALESCE(SUM(QtdeTotal),   0) AS QtdeTotalPecas
        FROM ordemservicoitem
        WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '')  and (enderecoarquivo <> '')
          AND IdOrdemServico = @Id
    ) agg ON agg.IdOrdemServico = os.IdOrdemServico
    SET
        os.Fator            = @Fator,
        os.AreaPinturaTotal = ROUND(agg.AreaPinturaTotal, 2),
        os.PesoTotal        = ROUND(agg.PesoTotal, 2),
        os.QtdeTotalItens   = COALESCE(agg.QtdeTotalItens, 0),
        os.QtdeTotalPecas   = COALESCE(agg.QtdeTotalPecas, 0)
    WHERE os.IdOrdemServico = @Id
      AND (os.D_E_L_E_T_E IS NULL OR os.D_E_L_E_T_E = '');"

            Try

                Using cmd2 As New MySqlCommand(query, myconect)
                    cmd2.Parameters.Add("@Fator", MySqlDbType.Decimal).Value = Fator
                    cmd2.Parameters.Add("@Id", MySqlDbType.Int32).Value = IdOrdemServico

                    cmd2.ExecuteNonQuery()

                End Using
            Catch ex As Exception
            Finally

            End Try

            Return True
        Catch

            Return False
        End Try
    End Function

    Public Function CalcularordemservicoitemFatorOS_TAG_PROJETO() As Boolean

        Try

            ' cl_BancoDados.AbrirBanco()

            Dim query As String = "UPDATE ordemservico os
LEFT JOIN (
    SELECT
        osi.IdOrdemServico,

        COUNT(osi.IdOrdemServicoItem) AS QtdeTotalItens,
        COALESCE(SUM(CASE WHEN osi.ordemservicoitemfinalizado = 'C' THEN 1 ELSE 0 END), 0) AS QtdeItensExecutados,
        COALESCE(SUM(osi.QtdeTotal), 0) AS QtdeTotalPecas,
        COALESCE(SUM(CASE WHEN osi.ordemservicoitemfinalizado = 'C' THEN osi.QtdeTotal ELSE 0 END), 0) AS QtdePecasExecutadas,

        COALESCE(SUM(CASE WHEN osi.txtCorte = '1' THEN osi.QtdeTotal ELSE 0 END), 0) AS CorteTotalExecutar,
        COALESCE(SUM(osi.CorteTotalExecutado), 0) AS CorteTotalExecutado,

        COALESCE(SUM(CASE WHEN osi.txtDobra = '1' THEN osi.QtdeTotal ELSE 0 END), 0) AS DobraTotalExecutar,
        COALESCE(SUM(osi.DobraTotalExecutado), 0) AS DobraTotalExecutado,

        COALESCE(SUM(CASE WHEN osi.txtSolda = '1' THEN osi.QtdeTotal ELSE 0 END), 0) AS SoldaTotalExecutar,
        COALESCE(SUM(osi.SoldaTotalExecutado), 0) AS SoldaTotalExecutado,

        COALESCE(SUM(CASE WHEN osi.txtPintura = '1' THEN osi.QtdeTotal ELSE 0 END), 0) AS PinturaTotalExecutar,
        COALESCE(SUM(osi.PinturaTotalExecutado), 0) AS PinturaTotalExecutado,

        COALESCE(SUM(CASE WHEN osi.txtMontagem = '1' AND osi.ProdutoPrincipal = 'SIM'
                          THEN osi.QtdeTotal ELSE 0 END), 0) AS MontagemTotalExecutar,

        COALESCE(SUM(osi.MontagemTotalExecutado), 0) AS MontagemTotalExecutado,

        COALESCE(SUM(osi.AreaPintura), 0) AS AreaPinturaTotal,
        COALESCE(SUM(osi.Peso), 0) AS PesoTotal

    FROM ordemservicoitem osi
    WHERE (osi.D_E_L_E_T_E IS NULL OR osi.D_E_L_E_T_E = '')
      AND (osi.EnderecoArquivo IS NOT NULL AND osi.EnderecoArquivo <> '')
      AND (osi.Liberado_Engenharia = 'S')
    GROUP BY osi.IdOrdemServico
) itens ON itens.IdOrdemServico = os.IdOrdemServico
SET
    os.QtdeTotalItens          = COALESCE(itens.QtdeTotalItens, 0),
    os.QtdeItensExecutados     = COALESCE(itens.QtdeItensExecutados, 0),
    os.QtdeTotalPecas          = COALESCE(itens.QtdeTotalPecas, 0),
    os.QtdePecasExecutadas     = COALESCE(itens.QtdePecasExecutadas, 0),

    os.CorteTotalExecutar      = COALESCE(itens.CorteTotalExecutar, 0),
    os.CorteTotalExecutado     = COALESCE(itens.CorteTotalExecutado, 0),

    os.DobraTotalExecutar      = COALESCE(itens.DobraTotalExecutar, 0),
    os.DobraTotalExecutado     = COALESCE(itens.DobraTotalExecutado, 0),

    os.SoldaTotalExecutar      = COALESCE(itens.SoldaTotalExecutar, 0),
    os.SoldaTotalExecutado     = COALESCE(itens.SoldaTotalExecutado, 0),

    os.PinturaTotalExecutar    = COALESCE(itens.PinturaTotalExecutar, 0),
    os.PinturaTotalExecutado   = COALESCE(itens.PinturaTotalExecutado, 0),

    os.MontagemTotalExecutar   = COALESCE(itens.MontagemTotalExecutar, 0),
    os.MontagemTotalExecutado  = COALESCE(itens.MontagemTotalExecutado, 0),

    os.AreaPinturaTotal        = ROUND(COALESCE(itens.AreaPinturaTotal, 0), 2),
    os.PesoTotal               = ROUND(COALESCE(itens.PesoTotal, 0), 2),

    os.CortePercentual         = COALESCE(ROUND((itens.CorteTotalExecutado / NULLIF(itens.CorteTotalExecutar, 0)) * 100, 2), 0),
    os.DobraPercentual         = COALESCE(ROUND((itens.DobraTotalExecutado / NULLIF(itens.DobraTotalExecutar, 0)) * 100, 2), 0),
    os.SoldaPercentual         = COALESCE(ROUND((itens.SoldaTotalExecutado / NULLIF(itens.SoldaTotalExecutar, 0)) * 100, 2), 0),
    os.PinturaPercentual       = COALESCE(ROUND((itens.PinturaTotalExecutado / NULLIF(itens.PinturaTotalExecutar, 0)) * 100, 2), 0),
    os.MontagemPercentual      = COALESCE(ROUND((itens.MontagemTotalExecutado / NULLIF(itens.MontagemTotalExecutar, 0)) * 100, 2), 0),

    os.PercentualPecas   = COALESCE(ROUND((itens.QtdePecasExecutadas / NULLIF(itens.QtdeTotalPecas, 0)) * 100, 2), 0),
    os.PercentualItens      = COALESCE(ROUND((itens.QtdeItensExecutados / NULLIF(itens.QtdeTotalItens, 0)) * 100, 2), 0) where os.idordemservico = " & OrdemServico.IdOrdemServico & "
UPDATE ordemservicoitem osi
SET
    CortePercentual = ROUND((osi.CorteTotalExecutado / osi.QtdeTotal) * 100, 2),
    DobraPercentual = ROUND((osi.DobraTotalExecutado / osi.QtdeTotal) * 100, 2),
    SoldaPercentual = ROUND((osi.SoldaTotalExecutado / osi.QtdeTotal) * 100, 2),
    PinturaPercentual = ROUND((osi.PinturaTotalExecutado / osi.QtdeTotal) * 100, 2),
    MontagemPercentual = ROUND((osi.MontagemTotalExecutado / osi.QtdeTotal) * 100, 2)
WHERE
    (osi.D_E_L_E_T_E IS NULL OR osi.D_E_L_E_T_E = '') and  osi.idordemservico = " & OrdemServico.IdOrdemServico & ";
UPDATE tags t
                    LEFT JOIN (
SELECT
    os.IdTag AS IdTag,

    COUNT(DISTINCT os.IdOrdemServico)                                            AS Total_OS,
    COALESCE(SUM(os.QtdeTotalPecas), 0)                                          AS Total_Pecas,

    -- Quantidade de OS executadas (distintas)
    COALESCE(COUNT(DISTINCT CASE
        WHEN os.ordemservicofinalizado = 'C' THEN os.IdOrdemServico
    END), 0)                                                                     AS QtdeOSExecutadas,

    -- Quantidade de peças executadas (soma nas OS finalizadas)
    COALESCE(SUM(CASE
        WHEN os.ordemservicofinalizado = 'C' THEN os.QtdeTotalPecas ELSE 0
    END), 0)                                                                     AS QtdePecasExecutadas,

    COALESCE(SUM(os.CorteTotalExecutar),     0)                                  AS CorteTotalExecutar,
    COALESCE(SUM(os.CorteTotalExecutado),    0)                                  AS CorteTotalExecutado,

    COALESCE(SUM(os.DobraTotalExecutar),     0)                                  AS DobraTotalExecutar,
    COALESCE(SUM(os.DobraTotalExecutado),    0)                                  AS DobraTotalExecutado,

    COALESCE(SUM(os.SoldaTotalExecutar),     0)                                  AS SoldaTotalExecutar,
    COALESCE(SUM(os.SoldaTotalExecutado),    0)                                  AS SoldaTotalExecutado,

    COALESCE(SUM(os.PinturaTotalExecutar),   0)                                  AS PinturaTotalExecutar,
    COALESCE(SUM(os.PinturaTotalExecutado),  0)                                  AS PinturaTotalExecutado,

    COALESCE(SUM(os.MontagemTotalExecutar),  0)                                  AS MontagemTotalExecutar,
    COALESCE(SUM(os.MontagemTotalExecutado), 0)                                  AS MontagemTotalExecutado
FROM ordemservico AS os
WHERE (os.D_E_L_E_T_E IS NULL OR os.D_E_L_E_T_E = '')
  AND os.Liberado_Engenharia = 'S'
GROUP BY os.IdTag
  ) resumo ON resumo.IdTag = t.IdTag
 SET
     t.QtdeOS                   = COALESCE(resumo.Total_OS, 0),
     t.QtdeOSExecutadas         = COALESCE(resumo.QtdeOSexecutadas, 0),
     t.QtdePecasOS              = COALESCE(resumo.Total_Pecas, 0),
     t.QtdePecasExecutadas      = COALESCE(resumo.QtdePecasexecutadas, 0),
     t.CorteTotalExecutar       = COALESCE(resumo.CorteTotalExecutar, 0),
     t.CorteTotalExecutado      = COALESCE(resumo.CorteTotalExecutado, 0),
     t.DobraTotalExecutar       = COALESCE(resumo.DobraTotalExecutar, 0),
     t.DobraTotalExecutado      = COALESCE(resumo.DobraTotalExecutado, 0),
     t.SoldaTotalExecutar       = COALESCE(resumo.SoldaTotalExecutar, 0),
     t.SoldaTotalExecutado      = COALESCE(resumo.SoldaTotalExecutado, 0),
     t.PinturaTotalExecutar     = COALESCE(resumo.PinturaTotalExecutar, 0),
     t.PinturaTotalExecutado    = COALESCE(resumo.PinturaTotalExecutado, 0),
     t.MontagemTotalExecutar    = COALESCE(resumo.MontagemTotalExecutar, 0),
     t.MontagemTotalExecutado   = COALESCE(resumo.MontagemTotalExecutado, 0),

      t.CortePercentual = COALESCE(ROUND((resumo.CorteTotalExecutado / NULLIF(resumo.CorteTotalExecutar, 0)) * 100, 2), 0),
     t.DobraPercentual = COALESCE(ROUND((resumo.DobraTotalExecutado / NULLIF(resumo.DobraTotalExecutar, 0)) * 100, 2), 0),
     t.SoldaPercentual = COALESCE(ROUND((resumo.SoldaTotalExecutado / NULLIF(resumo.SoldaTotalExecutar, 0)) * 100, 2), 0),
     t.PinturaPercentual = COALESCE(ROUND((resumo.PinturaTotalExecutado / NULLIF(resumo.PinturaTotalExecutar, 0)) * 100, 2), 0),
     t.MontagemPercentual = COALESCE(ROUND((resumo.MontagemTotalExecutado / NULLIF(resumo.MontagemTotalExecutar, 0)) * 100, 2), 0),
     t.PercentualOS   = COALESCE(ROUND((resumo.QtdeOSExecutadas / NULLIF(resumo.Total_OS, 0)) * 100, 2), 0),
    t.PercentualPecas      = COALESCE(ROUND((resumo.QtdePecasExecutadas / NULLIF(resumo.Total_Pecas, 0)) * 100, 2), 0);
UPDATE projetos p
LEFT JOIN (
    SELECT
        t.IdProjeto,
        COUNT(*) AS Total_Tags,
        COALESCE(SUM(t.QtdePecasOS), 0) AS Total_Pecas,

      COALESCE(count(CASE WHEN t.finalizado = 'C' THEN t.IdTag ELSE 0 END), 0) AS QtdeTagsExecutadas,
      COALESCE(SUM(CASE WHEN t.finalizado = 'C' THEN t.QtdePecasOS ELSE 0 END), 0) AS QtdePecasExecutadas,
        COALESCE(SUM(t.CorteTotalExecutar),    0) AS CorteTotalExecutar,
        COALESCE(SUM(t.CorteTotalExecutado),   0) AS CorteTotalExecutado,

        COALESCE(SUM(t.DobraTotalExecutar),    0) AS DobraTotalExecutar,
        COALESCE(SUM(t.DobraTotalExecutado),   0) AS DobraTotalExecutado,

        COALESCE(SUM(t.SoldaTotalExecutar),    0) AS SoldaTotalExecutar,
        COALESCE(SUM(t.SoldaTotalExecutado),   0) AS SoldaTotalExecutado,

        COALESCE(SUM(t.PinturaTotalExecutar),  0) AS PinturaTotalExecutar,
        COALESCE(SUM(t.PinturaTotalExecutado), 0) AS PinturaTotalExecutado,

        COALESCE(SUM(t.MontagemTotalExecutar), 0) AS MontagemTotalExecutar,
        COALESCE(SUM(t.MontagemTotalExecutado),0) AS MontagemTotalExecutado
    FROM tags t
    WHERE (t.D_E_L_E_T_E IS NULL OR t.D_E_L_E_T_E = '')
    GROUP BY t.IdProjeto
) resumo ON resumo.IdProjeto = p.IdProjeto
SET

    p.QtdeTagsExecutadas      = COALESCE(resumo.QtdeTagsExecutadas, 0),
    p.QtdePecasTags           = COALESCE(resumo.Total_Pecas, 0),
    p.QtdePecasExecutadas     = COALESCE(resumo.QtdePecasExecutadas, 0),
    p.CorteTotalExecutar      = COALESCE(resumo.CorteTotalExecutar, 0),
    p.CorteTotalExecutado     = COALESCE(resumo.CorteTotalExecutado, 0),
    p.DobraTotalExecutar      = COALESCE(resumo.DobraTotalExecutar, 0),
    p.DobraTotalExecutado     = COALESCE(resumo.DobraTotalExecutado, 0),
    p.SoldaTotalExecutar      = COALESCE(resumo.SoldaTotalExecutar, 0),
    p.SoldaTotalExecutado     = COALESCE(resumo.SoldaTotalExecutado, 0),
    p.PinturaTotalExecutar    = COALESCE(resumo.PinturaTotalExecutar, 0),
    p.PinturaTotalExecutado   = COALESCE(resumo.PinturaTotalExecutado, 0),
    p.MontagemTotalExecutar   = COALESCE(resumo.MontagemTotalExecutar, 0),
    p.MontagemTotalExecutado  = COALESCE(resumo.MontagemTotalExecutado, 0),

    p.DobraPercentual = COALESCE(ROUND((resumo.DobraTotalExecutado / NULLIF(resumo.DobraTotalExecutar, 0)) * 100, 2), 0),
    p.SoldaPercentual = COALESCE(ROUND((resumo.SoldaTotalExecutado / NULLIF(resumo.SoldaTotalExecutar, 0)) * 100, 2), 0),
    p.PinturaPercentual = COALESCE(ROUND((resumo.PinturaTotalExecutado / NULLIF(resumo.PinturaTotalExecutar, 0)) * 100, 2), 0),
    p.MontagemPercentual = COALESCE(ROUND((resumo.MontagemTotalExecutado / NULLIF(resumo.MontagemTotalExecutar, 0)) * 100, 2), 0),
    p.PercentualTags   = COALESCE(ROUND((resumo.QtdeTagsExecutadas / NULLIF(resumo.Total_Tags, 0)) * 100, 2), 0),
    p.PercentualPecas      = COALESCE(ROUND((resumo.QtdePecasExecutadas / NULLIF(resumo.Total_Pecas, 0)) * 100, 2), 0); "

            Try

                cl_BancoDados.AbrirBanco()

                Using cmd2 As New MySqlCommand(query, myconect)

                    cmd2.ExecuteNonQuery()

                End Using

                cl_BancoDados.FecharBanco()
            Catch ex As Exception
            Finally

            End Try

            'cl_BancoDados.FecharBanco()
        Catch ex As Exception
        Finally
            cl_BancoDados.FecharBanco()
        End Try

    End Function

    ' antes de apagar ver resultado da função acima 25/09/2025
    '    Public Function CalcularordemservicoitemFatorOS_TAG_PROJETO() As Boolean

    '        Try

    '            cl_BancoDados.AbrirBanco()

    '            Dim sqlordemservico As String = "UPDATE ordemservico os
    'LEFT JOIN (
    '    SELECT
    '        osi.IdOrdemServico,

    '        COUNT(osi.IdOrdemServicoItem) AS QtdeTotalItens,
    '        COALESCE(SUM(CASE WHEN osi.ordemservicoitemfinalizado = 'C' THEN 1 ELSE 0 END), 0) AS QtdeItensExecutados,
    '        COALESCE(SUM(osi.QtdeTotal), 0) AS QtdeTotalPecas,
    '        COALESCE(SUM(CASE WHEN osi.ordemservicoitemfinalizado = 'C' THEN osi.QtdeTotal ELSE 0 END), 0) AS QtdePecasExecutadas,

    '        COALESCE(SUM(CASE WHEN osi.txtCorte = '1' THEN osi.QtdeTotal ELSE 0 END), 0) AS CorteTotalExecutar,
    '        COALESCE(SUM(osi.CorteTotalExecutado), 0) AS CorteTotalExecutado,

    '        COALESCE(SUM(CASE WHEN osi.txtDobra = '1' THEN osi.QtdeTotal ELSE 0 END), 0) AS DobraTotalExecutar,
    '        COALESCE(SUM(osi.DobraTotalExecutado), 0) AS DobraTotalExecutado,

    '        COALESCE(SUM(CASE WHEN osi.txtSolda = '1' THEN osi.QtdeTotal ELSE 0 END), 0) AS SoldaTotalExecutar,
    '        COALESCE(SUM(osi.SoldaTotalExecutado), 0) AS SoldaTotalExecutado,

    '        COALESCE(SUM(CASE WHEN osi.txtPintura = '1' THEN osi.QtdeTotal ELSE 0 END), 0) AS PinturaTotalExecutar,
    '        COALESCE(SUM(osi.PinturaTotalExecutado), 0) AS PinturaTotalExecutado,

    '        COALESCE(SUM(CASE WHEN osi.txtMontagem = '1' AND osi.ProdutoPrincipal = 'SIM'
    '                          THEN osi.QtdeTotal ELSE 0 END), 0) AS MontagemTotalExecutar,

    '        COALESCE(SUM(osi.MontagemTotalExecutado), 0) AS MontagemTotalExecutado,

    '        COALESCE(SUM(osi.AreaPintura), 0) AS AreaPinturaTotal,
    '        COALESCE(SUM(osi.Peso), 0) AS PesoTotal

    '    FROM ordemservicoitem osi
    '    WHERE (osi.D_E_L_E_T_E IS NULL OR osi.D_E_L_E_T_E = '')
    '      AND (osi.EnderecoArquivo IS NOT NULL AND osi.EnderecoArquivo <> '')
    '      AND (osi.Liberado_Engenharia = 'S')
    '    GROUP BY osi.IdOrdemServico
    ') itens ON itens.IdOrdemServico = os.IdOrdemServico
    'SET
    '    os.QtdeTotalItens          = COALESCE(itens.QtdeTotalItens, 0),
    '    os.QtdeItensExecutados     = COALESCE(itens.QtdeItensExecutados, 0),
    '    os.QtdeTotalPecas          = COALESCE(itens.QtdeTotalPecas, 0),
    '    os.QtdePecasExecutadas     = COALESCE(itens.QtdePecasExecutadas, 0),

    '    os.CorteTotalExecutar      = COALESCE(itens.CorteTotalExecutar, 0),
    '    os.CorteTotalExecutado     = COALESCE(itens.CorteTotalExecutado, 0),

    '    os.DobraTotalExecutar      = COALESCE(itens.DobraTotalExecutar, 0),
    '    os.DobraTotalExecutado     = COALESCE(itens.DobraTotalExecutado, 0),

    '    os.SoldaTotalExecutar      = COALESCE(itens.SoldaTotalExecutar, 0),
    '    os.SoldaTotalExecutado     = COALESCE(itens.SoldaTotalExecutado, 0),

    '    os.PinturaTotalExecutar    = COALESCE(itens.PinturaTotalExecutar, 0),
    '    os.PinturaTotalExecutado   = COALESCE(itens.PinturaTotalExecutado, 0),

    '    os.MontagemTotalExecutar   = COALESCE(itens.MontagemTotalExecutar, 0),
    '    os.MontagemTotalExecutado  = COALESCE(itens.MontagemTotalExecutado, 0),

    '    os.AreaPinturaTotal        = ROUND(COALESCE(itens.AreaPinturaTotal, 0), 2),
    '    os.PesoTotal               = ROUND(COALESCE(itens.PesoTotal, 0), 2),

    '    os.CortePercentual         = COALESCE(ROUND((itens.CorteTotalExecutado / NULLIF(itens.CorteTotalExecutar, 0)) * 100, 2), 0),
    '    os.DobraPercentual         = COALESCE(ROUND((itens.DobraTotalExecutado / NULLIF(itens.DobraTotalExecutar, 0)) * 100, 2), 0),
    '    os.SoldaPercentual         = COALESCE(ROUND((itens.SoldaTotalExecutado / NULLIF(itens.SoldaTotalExecutar, 0)) * 100, 2), 0),
    '    os.PinturaPercentual       = COALESCE(ROUND((itens.PinturaTotalExecutado / NULLIF(itens.PinturaTotalExecutar, 0)) * 100, 2), 0),
    '    os.MontagemPercentual      = COALESCE(ROUND((itens.MontagemTotalExecutado / NULLIF(itens.MontagemTotalExecutar, 0)) * 100, 2), 0),

    '    os.PercentualPecas   = COALESCE(ROUND((itens.QtdePecasExecutadas / NULLIF(itens.QtdeTotalPecas, 0)) * 100, 2), 0),
    '    os.PercentualItens      = COALESCE(ROUND((itens.QtdeItensExecutados / NULLIF(itens.QtdeTotalItens, 0)) * 100, 2), 0) where os.idordemservico = " & OrdemServico.IdOrdemServico & " ; "

    '            Using cmd2 As New MySqlCommand(sqlordemservico, myconect)

    '                Try

    '                    cmd2.ExecuteNonQuery()
    '                Catch ex As Exception
    '                    ' MsgBox(ex.Message)
    '                Finally
    '                End Try

    '            End Using
    '            ' cl_BancoDados.FecharBanco()

    '            Dim sqlordemservicoitemPercentual As String = "UPDATE ordemservicoitem osi
    'SET
    '    CortePercentual = ROUND((osi.CorteTotalExecutado / osi.QtdeTotal) * 100, 2),
    '    DobraPercentual = ROUND((osi.DobraTotalExecutado / osi.QtdeTotal) * 100, 2),
    '    SoldaPercentual = ROUND((osi.SoldaTotalExecutado / osi.QtdeTotal) * 100, 2),
    '    PinturaPercentual = ROUND((osi.PinturaTotalExecutado / osi.QtdeTotal) * 100, 2),
    '    MontagemPercentual = ROUND((osi.MontagemTotalExecutado / osi.QtdeTotal) * 100, 2)
    'WHERE
    '    (osi.D_E_L_E_T_E IS NULL OR osi.D_E_L_E_T_E = '') and  osi.idordemservico = " & OrdemServico.IdOrdemServico & ";"

    '            ' cl_BancoDados.AbrirBanco()
    '            Using cmd2 As New MySqlCommand(sqlordemservicoitemPercentual, myconect)

    '                cmd2.ExecuteNonQuery()

    '            End Using

    '            ' cl_BancoDados.FecharBanco()

    '            Dim sqltag As String = "UPDATE tags t
    '                    LEFT JOIN (
    'SELECT
    '    os.IdTag AS IdTag,

    '    COUNT(DISTINCT os.IdOrdemServico)                                            AS Total_OS,
    '    COALESCE(SUM(os.QtdeTotalPecas), 0)                                          AS Total_Pecas,

    '    -- Quantidade de OS executadas (distintas)
    '    COALESCE(COUNT(DISTINCT CASE
    '        WHEN os.ordemservicofinalizado = 'C' THEN os.IdOrdemServico
    '    END), 0)                                                                     AS QtdeOSExecutadas,

    '    -- Quantidade de peças executadas (soma nas OS finalizadas)
    '    COALESCE(SUM(CASE
    '        WHEN os.ordemservicofinalizado = 'C' THEN os.QtdeTotalPecas ELSE 0
    '    END), 0)                                                                     AS QtdePecasExecutadas,

    '    COALESCE(SUM(os.CorteTotalExecutar),     0)                                  AS CorteTotalExecutar,
    '    COALESCE(SUM(os.CorteTotalExecutado),    0)                                  AS CorteTotalExecutado,

    '    COALESCE(SUM(os.DobraTotalExecutar),     0)                                  AS DobraTotalExecutar,
    '    COALESCE(SUM(os.DobraTotalExecutado),    0)                                  AS DobraTotalExecutado,

    '    COALESCE(SUM(os.SoldaTotalExecutar),     0)                                  AS SoldaTotalExecutar,
    '    COALESCE(SUM(os.SoldaTotalExecutado),    0)                                  AS SoldaTotalExecutado,

    '    COALESCE(SUM(os.PinturaTotalExecutar),   0)                                  AS PinturaTotalExecutar,
    '    COALESCE(SUM(os.PinturaTotalExecutado),  0)                                  AS PinturaTotalExecutado,

    '    COALESCE(SUM(os.MontagemTotalExecutar),  0)                                  AS MontagemTotalExecutar,
    '    COALESCE(SUM(os.MontagemTotalExecutado), 0)                                  AS MontagemTotalExecutado
    'FROM ordemservico AS os
    'WHERE (os.D_E_L_E_T_E IS NULL OR os.D_E_L_E_T_E = '')
    '  AND os.Liberado_Engenharia = 'S'
    'GROUP BY os.IdTag
    '  ) resumo ON resumo.IdTag = t.IdTag
    ' SET
    '     t.QtdeOS                   = COALESCE(resumo.Total_OS, 0),
    '     t.QtdeOSExecutadas         = COALESCE(resumo.QtdeOSexecutadas, 0),
    '     t.QtdePecasOS              = COALESCE(resumo.Total_Pecas, 0),
    '     t.QtdePecasExecutadas      = COALESCE(resumo.QtdePecasexecutadas, 0),
    '     t.CorteTotalExecutar       = COALESCE(resumo.CorteTotalExecutar, 0),
    '     t.CorteTotalExecutado      = COALESCE(resumo.CorteTotalExecutado, 0),
    '     t.DobraTotalExecutar       = COALESCE(resumo.DobraTotalExecutar, 0),
    '     t.DobraTotalExecutado      = COALESCE(resumo.DobraTotalExecutado, 0),
    '     t.SoldaTotalExecutar       = COALESCE(resumo.SoldaTotalExecutar, 0),
    '     t.SoldaTotalExecutado      = COALESCE(resumo.SoldaTotalExecutado, 0),
    '     t.PinturaTotalExecutar     = COALESCE(resumo.PinturaTotalExecutar, 0),
    '     t.PinturaTotalExecutado    = COALESCE(resumo.PinturaTotalExecutado, 0),
    '     t.MontagemTotalExecutar    = COALESCE(resumo.MontagemTotalExecutar, 0),
    '     t.MontagemTotalExecutado   = COALESCE(resumo.MontagemTotalExecutado, 0),

    '      t.CortePercentual = COALESCE(ROUND((resumo.CorteTotalExecutado / NULLIF(resumo.CorteTotalExecutar, 0)) * 100, 2), 0),
    '     t.DobraPercentual = COALESCE(ROUND((resumo.DobraTotalExecutado / NULLIF(resumo.DobraTotalExecutar, 0)) * 100, 2), 0),
    '     t.SoldaPercentual = COALESCE(ROUND((resumo.SoldaTotalExecutado / NULLIF(resumo.SoldaTotalExecutar, 0)) * 100, 2), 0),
    '     t.PinturaPercentual = COALESCE(ROUND((resumo.PinturaTotalExecutado / NULLIF(resumo.PinturaTotalExecutar, 0)) * 100, 2), 0),
    '     t.MontagemPercentual = COALESCE(ROUND((resumo.MontagemTotalExecutado / NULLIF(resumo.MontagemTotalExecutar, 0)) * 100, 2), 0),
    '     t.PercentualOS   = COALESCE(ROUND((resumo.QtdeOSExecutadas / NULLIF(resumo.Total_OS, 0)) * 100, 2), 0),
    '    t.PercentualPecas      = COALESCE(ROUND((resumo.QtdePecasExecutadas / NULLIF(resumo.Total_Pecas, 0)) * 100, 2), 0)"

    '            ' cl_BancoDados.AbrirBanco()

    '            Using cmd2 As New MySqlCommand(sqltag, myconect)

    '                cmd2.ExecuteNonQuery()

    '            End Using

    '            'cl_BancoDados.FecharBanco()

    '            Dim sqlprojeto As String = "UPDATE projetos p
    'LEFT JOIN (
    '    SELECT
    '        t.IdProjeto,
    '        COUNT(*) AS Total_Tags,
    '        COALESCE(SUM(t.QtdePecasOS), 0) AS Total_Pecas,

    '      COALESCE(count(CASE WHEN t.finalizado = 'C' THEN t.IdTag ELSE 0 END), 0) AS QtdeTagsExecutadas,
    '      COALESCE(SUM(CASE WHEN t.finalizado = 'C' THEN t.QtdePecasOS ELSE 0 END), 0) AS QtdePecasExecutadas,
    '        COALESCE(SUM(t.CorteTotalExecutar),    0) AS CorteTotalExecutar,
    '        COALESCE(SUM(t.CorteTotalExecutado),   0) AS CorteTotalExecutado,

    '        COALESCE(SUM(t.DobraTotalExecutar),    0) AS DobraTotalExecutar,
    '        COALESCE(SUM(t.DobraTotalExecutado),   0) AS DobraTotalExecutado,

    '        COALESCE(SUM(t.SoldaTotalExecutar),    0) AS SoldaTotalExecutar,
    '        COALESCE(SUM(t.SoldaTotalExecutado),   0) AS SoldaTotalExecutado,

    '        COALESCE(SUM(t.PinturaTotalExecutar),  0) AS PinturaTotalExecutar,
    '        COALESCE(SUM(t.PinturaTotalExecutado), 0) AS PinturaTotalExecutado,

    '        COALESCE(SUM(t.MontagemTotalExecutar), 0) AS MontagemTotalExecutar,
    '        COALESCE(SUM(t.MontagemTotalExecutado),0) AS MontagemTotalExecutado
    '    FROM tags t
    '    WHERE (t.D_E_L_E_T_E IS NULL OR t.D_E_L_E_T_E = '')
    '    GROUP BY t.IdProjeto
    ') resumo ON resumo.IdProjeto = p.IdProjeto
    'SET

    '    p.QtdeTagsExecutadas      = COALESCE(resumo.QtdeTagsExecutadas, 0),
    '    p.QtdePecasTags           = COALESCE(resumo.Total_Pecas, 0),
    '    p.QtdePecasExecutadas     = COALESCE(resumo.QtdePecasExecutadas, 0),
    '    p.CorteTotalExecutar      = COALESCE(resumo.CorteTotalExecutar, 0),
    '    p.CorteTotalExecutado     = COALESCE(resumo.CorteTotalExecutado, 0),
    '    p.DobraTotalExecutar      = COALESCE(resumo.DobraTotalExecutar, 0),
    '    p.DobraTotalExecutado     = COALESCE(resumo.DobraTotalExecutado, 0),
    '    p.SoldaTotalExecutar      = COALESCE(resumo.SoldaTotalExecutar, 0),
    '    p.SoldaTotalExecutado     = COALESCE(resumo.SoldaTotalExecutado, 0),
    '    p.PinturaTotalExecutar    = COALESCE(resumo.PinturaTotalExecutar, 0),
    '    p.PinturaTotalExecutado   = COALESCE(resumo.PinturaTotalExecutado, 0),
    '    p.MontagemTotalExecutar   = COALESCE(resumo.MontagemTotalExecutar, 0),
    '    p.MontagemTotalExecutado  = COALESCE(resumo.MontagemTotalExecutado, 0),

    '    p.DobraPercentual = COALESCE(ROUND((resumo.DobraTotalExecutado / NULLIF(resumo.DobraTotalExecutar, 0)) * 100, 2), 0),
    '    p.SoldaPercentual = COALESCE(ROUND((resumo.SoldaTotalExecutado / NULLIF(resumo.SoldaTotalExecutar, 0)) * 100, 2), 0),
    '    p.PinturaPercentual = COALESCE(ROUND((resumo.PinturaTotalExecutado / NULLIF(resumo.PinturaTotalExecutar, 0)) * 100, 2), 0),
    '    p.MontagemPercentual = COALESCE(ROUND((resumo.MontagemTotalExecutado / NULLIF(resumo.MontagemTotalExecutar, 0)) * 100, 2), 0),
    '    p.PercentualTags   = COALESCE(ROUND((resumo.QtdeTagsExecutadas / NULLIF(resumo.Total_Tags, 0)) * 100, 2), 0),
    '    p.PercentualPecas      = COALESCE(ROUND((resumo.QtdePecasExecutadas / NULLIF(resumo.Total_Pecas, 0)) * 100, 2), 0); "

    '            Try

    '                '  cl_BancoDados.AbrirBanco()

    '                Using cmd2 As New MySqlCommand(sqlprojeto, myconect)

    '                    cmd2.ExecuteNonQuery()

    '                End Using

    '                '  cl_BancoDados.FecharBanco()

    '            Catch ex As Exception

    '            Finally

    '            End Try

    '            cl_BancoDados.FecharBanco()

    '        Catch ex As Exception
    '        Finally
    '            cl_BancoDados.FecharBanco()
    '        End Try

    '    End Function

End Class

Public Class BancoDadosClasse
    Private Shared _instance As BancoDadosClasse
    Private Shared myconect As MySqlConnection
    Private Shared ReadOnly LockObj As New Object()

    ' Construtor privado
    Private Sub New()
        Dim conexaoString As String = "Server=lynxlocal.mysql.uhserver.com;database=lynxlocal;uid=lynxlocal;pwd=jHAzhFG848@yN@U;Max Pool Size=50;Connection Timeout=60;Connection Lifetime=30;CharSet=utf8;"
        myconect = New MySqlConnection(conexaoString)
        My.Settings.BancoDadosAtivo = "lynxlocal"
    End Sub

    ' Singleton para obter a instância
    Public Shared Function GetInstance() As BancoDadosClasse
        If _instance Is Nothing Then
            SyncLock LockObj
                If _instance Is Nothing Then
                    _instance = New BancoDadosClasse()
                End If
            End SyncLock
        End If
        Return _instance
    End Function

    ' Método para abrir o banco
    Public Function AbrirBanco() As Boolean
        Dim tentativas As Integer = 0
        Dim maxTentativas As Integer = 3

        While tentativas < maxTentativas
            Try
                If myconect.State = ConnectionState.Closed OrElse myconect.State = ConnectionState.Broken Then
                    myconect.Open()
                End If

                AbrirBanco = True
                Exit While
            Catch ex As MySqlException
                tentativas += 1
                If tentativas >= maxTentativas Then
                    MessageBox.Show("Erro ao conectar ao banco de dados após várias tentativas: " & ex.Message)
                    AbrirBanco = False
                    Exit While
                End If
            End Try
        End While

        Return AbrirBanco
    End Function

    ' Obter conexão
    Public Function ObterConexao() As MySqlConnection
        If myconect.State = ConnectionState.Closed OrElse myconect.State = ConnectionState.Broken Then
            myconect.Open()

        End If
        Return myconect
    End Function

End Class