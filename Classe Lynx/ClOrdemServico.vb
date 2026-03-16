Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class CLOrdemServico

    Public IdOrdemServico As Integer
    Public IDOrdemServicoItem As Integer
    Public Projeto As String
    Public Tag As String
    Public idProjeto As String
    Public idTag As String
    Public DescTag As String
    Public Descricao As String
    Public DescEmpresa As String
    Public EnderecoOrdemServico As String
    Public CriadoPor As String
    Public DataCriacao As Date
    Public Estatus As String
    Public IdMaterial As String
    Public DescResumo As String
    Public DescDetal As String
    Public Autor As String
    Public Palavrachave As String
    Public Notas As String
    Public Espessura As String
    Public AreaPintura As Double
    Public AreaPinturaUnitario As Double
    Public NumeroDobras As String
    Public Peso As Double
    Public PesoUnitario As Double
    Public Unidade As String
    Public UnidadeSW As String
    Public ValorSW As String
    Public Altura As String
    Public Largura As String
    Public CodMatFabricante As String
    Public DtCad As String
    Public UsuarioCriacao As String
    Public UsuarioAlteracao As String
    Public DtAlteracao As String
    Public EnderecoArquivo As String
    Public MaterialSW As String
    Public QtdeTotal As Double
    Public qtde As Double
    Public txtSoldagem As String
    Public txtTipoDesenho As String
    Public txtCorte As String
    Public txtDobra As String
    Public txtSolda As String
    Public txtPintura As String
    Public txtMontagem As String
    Public txtAcabamento As String
    Public tttxtCorte As String
    Public tttxtDobra As String
    Public tttxtSolda As String
    Public tttxtPintura As String
    Public tttxtMontagem As String
    Public DataPrevisao As String
    Public ProdutoPrincipal As String
    Public Fator As String
    Public idempresa As String

    Public QtdeTag As Integer
    Public QtdeLiberada As Integer
    Public SaldoTag As Integer

    Public Liberado_Engenharia As String
    Public Data_Liberacao_Engenharia As String
    Public IdOSReferencia As String

    Public Comprimentocaixadelimitadora As String
    Public Larguracaixadelimitadora As String
    Public Espessuracaixadelimitadora As String
    Public txtItemEstoque As String

    Public RNC As String

    Public ProdutoPadrao As String
    Public CodDesenhoProduto As String
    Public CodOmie As String
    Public DescricaoProduto As String
    Public EnderecoFichaTecnica As String
    Public EnderecoIsometrico As String
    Public ProdutoCriadoPor As String
    Public DataCriacaoProduto As String

    Public bloqueado As String

    Public Function CriarOsCompleta(ByVal DgvGrid As DataGridView, ByVal timerDgvOS As Timer, ByVal timerDgvOSiTEM As Timer) As Boolean

        If My.Settings.EnderecoPastaRaizOS.ToString = "" And System.IO.Directory.Exists(My.Settings.EnderecoPastaRaizOS) = False Then

            MsgBox("O endereço onde será criado a pasta da Ordem de Serviço  não foi informado!")
            Exit Function
        Else

            Try

                If Tag = "" Or Projeto = "" Or Descricao = "" Then

                    MsgBox("O Projeto, Tag e ou uma descrição devem ser informados", vbInformation, "Atenção")
                Else

                    ''''''' OrdemServico.idProjeto = Nothing
                    ''''''OrdemServico.Projeto = Projeto
                    '''''''   OrdemServico.idTag = Nothing
                    ''''''OrdemServico.Tag = Tag.ToUpper
                    ''''''OrdemServico.Descricao = Descricao.ToUpper
                    OrdemServico.CriadoPor = Usuario.NomeCompleto
                    OrdemServico.DataCriacao = Date.Now.ToString("dd/MM/yyyy")
                    OrdemServico.Estatus = "A".ToUpper
                    ''''''OrdemServico.idProjeto = idProjeto
                    ''''''OrdemServico.idTag = idTag
                    ''''''OrdemServico.DescEmpresa = DescEmpresa
                    ''''''OrdemServico.Liberado_Engenharia = ""
                    ''''''OrdemServico.Data_Liberacao_Engenharia = DescEmpresa
                    ''''''OrdemServico.IdOSReferencia = ""

                    If OrdemServico.IdOrdemServico = Nothing Or
                        OrdemServico.IdOrdemServico.ToString = "" Or
                        OrdemServico.IdOrdemServico = 0 Then

                        Dim idosRetono As Integer

                        Try
                            VCampo0 = ""
                            cl_BancoDados.RetornaCampoDaPesquisa("SELECT IdOrdemServico from  " & ComplementoTipoBanco & "ordemservico where IdOrdemServico = '" & OrdemServico.IdOrdemServico & "'", "IdOrdemServico")

                            idosRetono = Convert.ToInt32(VCampo0)
                        Catch ex As Exception
                            idosRetono = 0
                        Finally
                        End Try

                        If idosRetono = 0 Then

                            Try
                                VCampo0 = ""
                                cl_BancoDados.RetornaCampoDaPesquisa("SELECT max(IdOrdemServico)  as NovoIdOrdemServico FROM  " & ComplementoTipoBanco & "ordemservico", "NovoIdOrdemServico")

                                Dim NovoIdOrdemServicoDB As Integer = Convert.ToInt32(VCampo0) + 1

                                NovoIdOrdemServico = cl_BancoDados.FormatarPara5Caracteres(NovoIdOrdemServicoDB.ToString())
                            Catch ex As Exception

                                ' Em caso de erro, atribuir "00001" como valor inicial
                                NovoIdOrdemServico = "00001"

                            End Try

                            OrdemServico.EnderecoOrdemServico = (My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico).ToString.ToUpper

                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico)
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\DXF")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PDF")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\DFT")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PUNC")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\LASER")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\Projeto")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\PEÇAS DE ESTOQUE")
                            System.IO.Directory.CreateDirectory(My.Settings.EnderecoPastaRaizOS & "\OS_" & NovoIdOrdemServico & "\LXDS")

                            OrdemServico.Liberado_Engenharia = ""
                            OrdemServico.Data_Liberacao_Engenharia = ""
                            OrdemServico.IdOSReferencia = OrdemServico.IdOrdemServico

                            Try

                                cl_BancoDados.RetornaCampoDaPesquisa("Select DataPrevisao from  " & ComplementoTipoBanco & "tags where idTag = '" & OrdemServico.idTag & "'", "DataPrevisao")

                                OrdemServico.DataPrevisao = VCampo0
                            Catch ex As Exception
                                OrdemServico.DataPrevisao = ""
                            End Try

                            SalvarOrdeMServicoBanco()

                            timerDgvOS.Enabled = True

                            MsgBox("Ordem de Serviço Criada com sucesso!")

                        End If
                    Else

                        'Altera dos dados da Ordem de serviço
                        cl_BancoDados.Salvar("update ordemservico set Descricao = '" & OrdemServico.Descricao & "',
Projeto = '" & OrdemServico.Projeto & "',
Tag = '" & OrdemServico.Tag & "',
idProjeto = '" & OrdemServico.idProjeto & "',
idTag = '" & OrdemServico.idTag & "',
Fator = '" & OrdemServico.Fator & "',
DataPrevisao = '" & OrdemServico.DataPrevisao & "',
DescEmpresa = '" & OrdemServico.DescEmpresa & "',
idEmpresa = '" & OrdemServico.idempresa & "',
desctag = '" & OrdemServico.DescTag & "'
where IdOrdemServico = '" & OrdemServico.IdOrdemServico & "';
update  " & ComplementoTipoBanco & "ordemservicoitem set Projeto = '" & OrdemServico.Projeto & "',
Tag = '" & OrdemServico.Tag & "',
idProjeto = '" & OrdemServico.idProjeto & "',
DataPrevisao = '" & OrdemServico.DataPrevisao & "',
idTag = '" & OrdemServico.idTag & "',
DescEmpresa = '" & OrdemServico.DescEmpresa & "',
idEmpresa = '" & OrdemServico.idempresa & "',
desctag = '" & OrdemServico.DescTag & "'
where IdOrdemServico = '" & OrdemServico.IdOrdemServico & "'") ' and idProjeto = '" & OrdemServico.idProjeto & "' and idTag = '" & OrdemServico.idTag & "'")

                        'inicio teste query unica 25/09/2025

                        '''                        'Altera dos dados da Ordem de serviço
                        '''                        cl_BancoDados.Salvar("update ordemservico set Descricao = '" & OrdemServico.Descricao & "',
                        '''Projeto = '" & OrdemServico.Projeto & "',
                        '''Tag = '" & OrdemServico.Tag & "',
                        '''idProjeto = '" & OrdemServico.idProjeto & "',
                        '''idTag = '" & OrdemServico.idTag & "',
                        '''Fator = '" & OrdemServico.Fator & "',
                        '''DataPrevisao = '" & OrdemServico.DataPrevisao & "',
                        '''DescEmpresa = '" & OrdemServico.DescEmpresa & "',
                        '''idEmpresa = '" & OrdemServico.idempresa & "',
                        '''desctag = '" & OrdemServico.DescTag & "'
                        '''where IdOrdemServico = '" & OrdemServico.IdOrdemServico & "'")

                        '''                        'Altera dos dados dos itens da Ordem de serviço
                        '''                        cl_BancoDados.Salvar("update  " & ComplementoTipoBanco & "ordemservicoitem set Projeto = '" & OrdemServico.Projeto & "',
                        '''Tag = '" & OrdemServico.Tag & "',
                        '''idProjeto = '" & OrdemServico.idProjeto & "',
                        '''DataPrevisao = '" & OrdemServico.DataPrevisao & "',
                        '''idTag = '" & OrdemServico.idTag & "',
                        '''DescEmpresa = '" & OrdemServico.DescEmpresa & "',
                        '''idEmpresa = '" & OrdemServico.idempresa & "',
                        '''desctag = '" & OrdemServico.DescTag & "'
                        '''where IdOrdemServico = '" & OrdemServico.IdOrdemServico & "'") ' and idProjeto = '" & OrdemServico.idProjeto & "' and idTag = '" & OrdemServico.idTag & "'")

                        ', , Descricao, Estatus, , , ,  FROM ordemservico o;

                        DgvGrid.CurrentRow.Cells("Projeto").Value = OrdemServico.Projeto

                        DgvGrid.CurrentRow.Cells("Tag").Value = OrdemServico.Tag

                        DgvGrid.CurrentRow.Cells("idProjeto").Value = OrdemServico.idProjeto

                        DgvGrid.CurrentRow.Cells("idTag").Value = OrdemServico.idTag

                        DgvGrid.CurrentRow.Cells("Descricao").Value = OrdemServico.Descricao

                        DgvGrid.CurrentRow.Cells("DescEmpresa").Value = OrdemServico.DescEmpresa

                        DgvGrid.CurrentRow.Cells("DataPrevisao").Value = OrdemServico.DataPrevisao

                        MsgBox("Ordem de serviço alterada com sucesso!")

                    End If

                End If

                timerDgvOSiTEM.Enabled = True
            Catch ex As Exception

                MsgBox("Erro ao criar a Ordem de Serviço" & ex.Message)
            Finally
            End Try

        End If

    End Function

    '    Public Function TotaisPecasOrdemServico(ByVal IdOrdemServico As Integer)

    '        cl_BancoDados.RetornaCampoDaPesquisa("SELECT
    '  SUM(CASE WHEN txtCorte = '1' THEN QtdeTotal ELSE 0 END) AS CorteTotalExecutar,
    '  SUM(CASE WHEN txtDobra = '1' THEN QtdeTotal ELSE 0 END) AS DobraTotalExecutar,
    '  SUM(CASE WHEN txtSolda = '1' THEN QtdeTotal ELSE 0 END) AS SoldaTotalExecutar,
    '  SUM(CASE WHEN txtPintura = '1' THEN QtdeTotal ELSE 0 END) AS PinturaTotalExecutar,
    '  SUM(CASE WHEN txtMontagem = '1' AND ProdutoPrincipal = 'SIM' THEN QtdeTotal ELSE 0 END) AS MontagemTotalExecutar,
    '  count(CASE WHEN txtTipoDesenho = 'CHAPARIA' THEN QtdeTotal ELSE 0 END) AS QtdeTotalItens,
    '  SUM(CASE WHEN txtTipoDesenho = 'CHAPARIA' THEN QtdeTotal ELSE 0 END) AS QtdeTotalPecas,
    '  SUM(areapintura) AS areapinturatotal,
    '  SUM(peso) AS pesototal
    'FROM ordemservicoitem
    'WHERE (D_E_L_E_T_E IS NULL OR D_E_L_E_T_E = '')
    '  AND IdOrdemServico = '" & OrdemServico.IdOrdemServico & "'",
    '                                             "CorteTotalExecutar",
    '                                             "DobraTotalExecutar",
    '                                             "SoldaTotalExecutar",
    '                                             "PinturaTotalExecutar",
    '                                             "MontagemTotalExecutar",
    '                                             "QtdeTotalItens",
    '                                             "QtdeTotalPecas",
    '                                             "areapinturatotal",
    '                                             "pesototal")

    '        cl_BancoDados.Salvar("Update ordemservico set Liberado_Engenharia = 'S',
    '                Data_Liberacao_Engenharia = '" & Date.Now & "',
    '                CorteTotalExecutar = '" & VCampo0 & "',
    '                 DobraTotalExecutar = '" & VCampo1 & "',
    '                  SoldaTotalExecutar = '" & VCampo2 & "',
    '                   PinturaTotalExecutar = '" & VCampo3 & "',
    '                    MontagemTotalExecutar = '" & VCampo4 & "',
    '                    QtdeTotalItens = '" & VCampo5 & "',
    '                    QtdeTotalPecas = '" & VCampo6 & "',
    '                    areapinturatotal = '" & VCampo7 & "',
    '                    pesototal = '" & VCampo8 & "'
    '						where IdOrdemServico = '" & IdOrdemServico & "'")

    '    End Function

    Public Function SalvarOrdeMServicoBanco()

        cl_BancoDados.AbrirBanco()

        If My.Settings.TipoConexao = "MYSQL" Then

            Try

                Dim cmd As New MySqlCommand("insert into  " & ComplementoTipoBanco & "ordemservico
    (idProjeto, Projeto, idTag, Tag, Descricao, EnderecoOrdemServico,
    CriadoPor, DataCriacao, Estatus, D_E_L_E_T_E, Liberado_Engenharia,
    Data_Liberacao_Engenharia, IdOSReferencia, DescEmpresa, DataPrevisao, Fator,idempresa,DescTag)
    values
    (@idProjeto, @Projeto, @idTag, @Tag, @Descricao, @EnderecoOrdemServico,
    @CriadoPor, @DataCriacao, @Estatus, @D_E_L_E_T_E, @Liberado_Engenharia,
    @Data_Liberacao_Engenharia, @IdOSReferencia, @DescEmpresa, @DataPrevisao, @Fator,@idempresa,@DescTag)", myconect)

                cmd.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
                cmd.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                cmd.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
                cmd.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                cmd.Parameters.AddWithValue("@Descricao", OrdemServico.Descricao)
                cmd.Parameters.AddWithValue("@EnderecoOrdemServico", OrdemServico.EnderecoOrdemServico)
                cmd.Parameters.AddWithValue("@CriadoPor", If(String.IsNullOrEmpty(OrdemServico.CriadoPor), DBNull.Value, OrdemServico.CriadoPor.ToUpper().ToString).ToString)
                cmd.Parameters.AddWithValue("@DataCriacao", OrdemServico.DataCriacao.ToString("dd/MM/yyyy"))
                cmd.Parameters.AddWithValue("@Estatus", OrdemServico.Estatus)
                cmd.Parameters.AddWithValue("@D_E_L_E_T_E", "")  ' ou tratar conforme necessário
                cmd.Parameters.AddWithValue("@Liberado_Engenharia", OrdemServico.Liberado_Engenharia)
                cmd.Parameters.AddWithValue("@Data_Liberacao_Engenharia", OrdemServico.Data_Liberacao_Engenharia)
                cmd.Parameters.AddWithValue("@IdOSReferencia", OrdemServico.IdOSReferencia)
                cmd.Parameters.AddWithValue("@DescEmpresa", OrdemServico.DescEmpresa)
                cmd.Parameters.AddWithValue("@DataPrevisao", OrdemServico.DataPrevisao)
                cmd.Parameters.AddWithValue("@Fator", OrdemServico.Fator)
                cmd.Parameters.AddWithValue("@idempresa", OrdemServico.idempresa)
                cmd.Parameters.AddWithValue("@DescTag", OrdemServico.DescTag)

                cmd.ExecuteNonQuery()
            Catch ex As Exception

                MsgBox(ex.Message)
            Finally

            End Try

        ElseIf My.Settings.TipoConexao = "SQL" Then

            Try

                Dim cmd As New SqlCommand("insert into " & ComplementoTipoBanco & " ordemservico
    (idProjeto, Projeto, idTag, Tag, Descricao, EnderecoOrdemServico,
    CriadoPor, DataCriacao, Estatus, D_E_L_E_T_E, Liberado_Engenharia,
    Data_Liberacao_Engenharia, IdOSReferencia, DescEmpresa, DataPrevisao)
    values
    (@idProjeto, @Projeto, @idTag, @Tag, @Descricao, @EnderecoOrdemServico,
    @CriadoPor, @DataCriacao, @Estatus, @D_E_L_E_T_E, @Liberado_Engenharia,
    @Data_Liberacao_Engenharia, @IdOSReferencia, @DescEmpresa, @DataPrevisao)", myconectSQL)

                cmd.Parameters.AddWithValue("@idProjeto", OrdemServico.idProjeto)
                cmd.Parameters.AddWithValue("@Projeto", OrdemServico.Projeto)
                cmd.Parameters.AddWithValue("@idTag", OrdemServico.idTag)
                cmd.Parameters.AddWithValue("@Tag", OrdemServico.Tag)
                cmd.Parameters.AddWithValue("@Descricao", OrdemServico.Descricao)
                cmd.Parameters.AddWithValue("@EnderecoOrdemServico", OrdemServico.EnderecoOrdemServico)
                cmd.Parameters.AddWithValue("@CriadoPor", If(String.IsNullOrEmpty(OrdemServico.CriadoPor), DBNull.Value, OrdemServico.CriadoPor.ToUpper()))
                cmd.Parameters.AddWithValue("@DataCriacao", OrdemServico.DataCriacao)
                cmd.Parameters.AddWithValue("@Estatus", OrdemServico.Estatus)
                cmd.Parameters.AddWithValue("@D_E_L_E_T_E", "")  ' ou tratar conforme necessário
                cmd.Parameters.AddWithValue("@Liberado_Engenharia", OrdemServico.Liberado_Engenharia)
                cmd.Parameters.AddWithValue("@Data_Liberacao_Engenharia", OrdemServico.Data_Liberacao_Engenharia)
                cmd.Parameters.AddWithValue("@IdOSReferencia", OrdemServico.IdOSReferencia)
                cmd.Parameters.AddWithValue("@DescEmpresa", OrdemServico.DescEmpresa)
                cmd.Parameters.AddWithValue("@DataPrevisao", OrdemServico.DataPrevisao)

                cmd.ExecuteNonQuery()
            Catch ex As Exception

                MsgBox(ex.Message)
            Finally

            End Try

        End If

        cl_BancoDados.FecharBanco()

    End Function

End Class

Public Class CLOrdemServicoItem

    Public IDOrdemServicoItem As Integer
    Public IdOrdemServico As Integer
    Public Projeto As String
    Public Tag As String
    Public ESTATUS_OrdemServico As String
    Public IdMaterial As Integer
    Public QtdeTotal As Double
    Public CriadoPor As String
    Public DataCriacao As String
    Public Estatus As String
    Public Acabamento As String
    Public PrevDataEntrega As String

End Class

Public Class CLOrdemServicoItemPendencia

    Public idordemservicoitempendencia As Integer
    Public IDOrdemServicoItem As Integer
    Public IdOrdemServico As Integer
    Public IdMaterial As Integer
    Public DescricaoPendencia As String
    Public DescricaoFinalizacao As String
    Public Usuario As String
    Public DataCriacao As String
    Public D_E_L_E_T_E As String
    Public UsuarioProjeto As String
    Public DataAcertoProjet As String
    Public estatu As String

End Class

Public Class clProjeto

    Public idProjeto As String
    Public Projeto As String
    Public DescProjeto As String
    Public Responsavel As String
    Public DescEmpresa As String
    Public DataEntrada As String
    Public DataPrevisao As String
    Public DataTermino As String
    Public TotalProjeto As String
    Public StatusProj As String
    Public D_E_L_E_T_E As String
    Public DescStatus As String
    Public IdEmpresa As String
    Public liberado As String
    Public UsuarioD_E_L_E_T_E As String
    Public DataD_E_L_E_T_E As String

End Class