Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class clUsuario

    Public idUsuario, NomeCompleto, idSetor, Setor, TipoUsuario, Login, Senha, email, Descricao, Foto, txtCorte,
                           txtDobra, txtSolda, txtPintura, txtMontagem, txtAlmoxarifado, CriadoPor, DataCadastro,
                           D_E_L_E_T_E, MapaProducao, Romaneio, OrdemServico, SolidWorks, Sigla As String

    Public Function RetornaDadosUsuario(ByVal LoginEntrada As String, ByVal SenhaEntrada As String) As Boolean

        cl_BancoDados.AbrirBanco()
        ' Verifica se o banco de dados não está aberto
        If My.Settings.TipoConexao = "MYSQL" Then

            Try

                Using da As New MySqlCommand("SELECT * FROM  " & ComplementoTipoBanco & "usuario WHERE Login = @Login AND Senha = @Senha", myconect)
                    ' Adicionar parâmetros para evitar SQL Injection
                    da.Parameters.AddWithValue("@Login", LoginEntrada)
                    da.Parameters.AddWithValue("@Senha", SenhaEntrada)

                    ' Executar o comando e abrir o DataReader
                    Using dr As MySqlDataReader = da.ExecuteReader()
                        ' Verificar se há linhas retornadas
                        If dr.HasRows Then
                            ' Ler a primeira linha retornada
                            dr.Read()

                            ' Preencher as variáveis com os valores retornados
                            idUsuario = dr("idUsuario").ToString()
                            NomeCompleto = dr("NomeCompleto").ToString()
                            idSetor = dr("idSetor").ToString()
                            Setor = dr("Setor").ToString()
                            TipoUsuario = dr("TipoUsuario").ToString()
                            Login = dr("Login").ToString()
                            Senha = dr("Senha").ToString()
                            email = dr("email").ToString()
                            Descricao = dr("Descricao").ToString()
                            txtCorte = dr("txtCorte").ToString()
                            txtDobra = dr("txtDobra").ToString()
                            txtSolda = dr("txtSolda").ToString()
                            txtPintura = dr("txtPintura").ToString()
                            txtMontagem = dr("txtMontagem").ToString()
                            txtAlmoxarifado = dr("txtAlmoxarifado").ToString()
                            CriadoPor = dr("CriadoPor").ToString()
                            DataCadastro = dr("DataCadastro").ToString()
                            D_E_L_E_T_E = dr("D_E_L_E_T_E").ToString()
                            MapaProducao = dr("MapaProducao").ToString()
                            Romaneio = dr("Romaneio").ToString()
                            OrdemServico = dr("OrdemServico").ToString()
                            SolidWorks = dr("SolidWorks").ToString()
                            Sigla = dr("Sigla").ToString()

                            RetornaDadosUsuario = True
                        Else
                            RetornaDadosUsuario = False
                        End If
                    End Using
                End Using
            Catch ex As Exception
            Finally

            End Try

        ElseIf My.Settings.TipoConexao = "SQL" Then

            Try

                Dim query As String = "SELECT * FROM  " & ComplementoTipoBanco & "usuario WHERE Login = @Login AND Senha = @Senha"

                Using da As New SqlCommand(query, myconectSQL)
                    ' Adicionar parâmetros para evitar SQL Injection
                    da.Parameters.AddWithValue("@Login", LoginEntrada.ToUpper)
                    da.Parameters.AddWithValue("@Senha", SenhaEntrada.ToUpper)

                    ' Executar o comando e abrir o DataReader
                    Using dr As SqlDataReader = da.ExecuteReader()
                        ' Verificar se há linhas retornadas
                        If dr.HasRows Then
                            ' Ler a primeira linha retornada
                            dr.Read()

                            ' Preencher as variáveis com os valores retornados
                            idUsuario = dr("idUsuario").ToString()
                            NomeCompleto = dr("NomeCompleto").ToString()
                            idSetor = dr("idSetor").ToString()
                            Setor = dr("Setor").ToString()
                            TipoUsuario = dr("TipoUsuario").ToString()
                            Login = dr("Login").ToString()
                            Senha = dr("Senha").ToString()
                            email = dr("email").ToString()
                            Descricao = dr("Descricao").ToString()
                            txtCorte = dr("txtCorte").ToString()
                            txtDobra = dr("txtDobra").ToString()
                            txtSolda = dr("txtSolda").ToString()
                            txtPintura = dr("txtPintura").ToString()
                            txtMontagem = dr("txtMontagem").ToString()
                            txtAlmoxarifado = dr("txtAlmoxarifado").ToString()
                            CriadoPor = dr("CriadoPor").ToString()
                            DataCadastro = dr("DataCadastro").ToString()
                            D_E_L_E_T_E = dr("D_E_L_E_T_E").ToString()
                            MapaProducao = dr("MapaProducao").ToString()
                            Romaneio = dr("Romaneio").ToString()
                            OrdemServico = dr("OrdemServico").ToString()
                            SolidWorks = dr("SolidWorks").ToString()
                            Sigla = dr("Sigla").ToString()

                            RetornaDadosUsuario = True
                        Else
                            RetornaDadosUsuario = False
                        End If
                    End Using
                End Using
            Catch ex As Exception
            Finally

            End Try

        End If

        cl_BancoDados.FecharBanco()

    End Function

    Public idConfigucacaoSistema, Bancoendereco, Bancousuario, banco, Bancosenha,
        EnderecoPastaRaizOS, EnderecoTemplateExcel, CopiaBancoDados, EnderecoPastaRaizRomaneio,
        EnderecoTemplateExcelRomaneio, ParametroExportarDXF, EnderecoNovoFormatoA3,
        EnderecoNovoFormatoA4, DataCriacao, Usuario, PermitirPecastxtcorte0noPlanoCorte,
        PermitirVerLiberado_EngenharianoPlanoCorte, EnviarEmailLiberacaoOS As String

    Public Function RetornaDadosConfiguracao() As Boolean

        cl_BancoDados.AbrirBanco()
        If My.Settings.TipoConexao = "MYSQL" Then

            Try

                Using da As New MySqlCommand("SELECT * FROM  " & ComplementoTipoBanco & "configucacaosistema WHERE idconfigucacaosistema = 1 and d_e_l_e_t_e <> '*' or d_e_l_e_t_e is null", myconect)
                    ' Adicionar parâmetros para evitar SQL Injection

                    ' Executar o comando e abrir o DataReader
                    Using dr As MySqlDataReader = da.ExecuteReader()
                        ' Verificar se há linhas retornadas
                        If dr.HasRows Then
                            ' Ler a primeira linha retornada
                            dr.Read()

                            ' Preencher as variáveis com os valores retornados
                            EnviarEmailLiberacaoOS = dr("EnviarEmailLiberacaoOS").ToString()

                            RetornaDadosConfiguracao = True

                            MyTaskPanelHost.TimerdgvPlanejamentoProjetista.Enabled = True
                        Else
                            EnviarEmailLiberacaoOS = ""
                            RetornaDadosConfiguracao = False
                        End If
                    End Using
                End Using
            Catch ex As Exception
            Finally

            End Try

        ElseIf My.Settings.TipoConexao = "SQL" Then

        End If
        cl_BancoDados.FecharBanco()

    End Function

    'Public Function SalvarOrdeMServicoBanco()

    '    If My.Settings.TipoConexao = "MYSQL" Then

    '        Try

    '            Dim cmd As New MySqlCommand("insert into  " & ComplementoTipoBanco & "ordemservico
    '(idProjeto, Projeto, idTag, Tag, Descricao, EnderecoOrdemServico,
    'CriadoPor, DataCriacao, Estatus, D_E_L_E_T_E, Liberado_Engenharia,
    'Data_Liberacao_Engenharia, IdOSReferencia, DescEmpresa, DataPrevisao, Fator)
    'values
    '(@idProjeto, @Projeto, @idTag, @Tag, @Descricao, @EnderecoOrdemServico,
    '@CriadoPor, @DataCriacao, @Estatus, @D_E_L_E_T_E, @Liberado_Engenharia,
    '@Data_Liberacao_Engenharia, @IdOSReferencia, @DescEmpresa, @DataPrevisao, @Fator)", myconect)

    '            cmd.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
    '            cmd.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
    '            cmd.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
    '            cmd.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
    '            cmd.Parameters.AddWithValue("@Descricao", OrdemServico.Descricao)
    '            cmd.Parameters.AddWithValue("@EnderecoOrdemServico", OrdemServico.EnderecoOrdemServico)
    '            cmd.Parameters.AddWithValue("@CriadoPor", If(String.IsNullOrEmpty(OrdemServico.CriadoPor), DBNull.Value, OrdemServico.CriadoPor.ToUpper().ToString).ToString)
    '            cmd.Parameters.AddWithValue("@DataCriacao", OrdemServico.DataCriacao.ToString("dd/MM/yyyy"))
    '            cmd.Parameters.AddWithValue("@Estatus", OrdemServico.Estatus)
    '            cmd.Parameters.AddWithValue("@D_E_L_E_T_E", "")  ' ou tratar conforme necessário
    '            cmd.Parameters.AddWithValue("@Liberado_Engenharia", OrdemServico.Liberado_Engenharia)
    '            cmd.Parameters.AddWithValue("@Data_Liberacao_Engenharia", OrdemServico.Data_Liberacao_Engenharia)
    '            cmd.Parameters.AddWithValue("@IdOSReferencia", OrdemServico.IdOSReferencia)
    '            cmd.Parameters.AddWithValue("@DescEmpresa", OrdemServico.DescEmpresa)
    '            cmd.Parameters.AddWithValue("@DataPrevisao", OrdemServico.DataPrevisao)
    '            cmd.Parameters.AddWithValue("@Fator", OrdemServico.Fator)

    '            cmd.ExecuteNonQuery()

    '        Catch ex As Exception

    '            MsgBox(ex.Message)
    '        Finally

    '        End Try

    '    End If

End Class