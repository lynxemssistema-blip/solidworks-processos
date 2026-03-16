Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports SolidWorks.Interop.sldworks

Public Module Module1

    ' Public cn As New mysqlConnection  'Variavel do banco de Dados
    ' Public myCmd As New SqlCommand  'Variavel da conexão e execução do comando do banco de dados
    ' Public dr As SqlDataReader

    Public cl_BancoDados As New ClBancoDados
    Public cl_CalculoBancoDados As New CalculoBancoDados
    '  Public BancoDadosInstancia As BancoDadosClasse

    Public banco As BancoDadosClasse = BancoDadosClasse.GetInstance()

    Public myconect As New MySqlConnection
    Public mycomand As New MySqlCommand

    Public myconectAccess As New OleDbConnection
    Public mycomandAccess As New OleDbCommand

    Public myconectSQL As New SqlConnection
    Public mycomandSQL As New SqlCommand

    'Public My.Settings.TipoConexao As String 'SQL - MYSQL - ACCESS

    Public BancoCliente As String

    Public ComplementoTipoBanco As String

    Public FormatargRID As DgvPerfHelper

    Public DescarregarLynx As Boolean = True

    Public ProdutoPrincipal As String

    Public conexao As String

    Public sModelName As String
    Public sPathName As String

    Public config As Configuration
    Public swapp As SldWorks
    'Public swApparq As New Object

    Public instance As ISldWorks
    Public swModel As ModelDoc2 'usado no arquivo corrente
    Public swModelComp As ModelDoc2 'usado na estrutura de arvore do Projeto

    Public swPart As PartDoc
    Public swDocEvent As DocumentEventHandler

    Public swModelDocExt As ModelDocExtension
    Public swCustProp As CustomPropertyManager
    Public MyMassProp As MassProperty

    Public swAssembly As AssemblyDoc = Nothing
    Public swFeature As Feature = Nothing

    Public swDrawing As DrawingDoc = Nothing

    'Public bomTableFeature As Feature
    'Public bomTable As BomTable = Nothing
    Public rowCount As Integer

    Public colCount As Integer
    Public selection As Integer
    Public selectedType As String

    ' Public swDraw As Object
    'Public swFeat As Feature
    Public swBomFeat As BomTable

    Public vTableArr As Object
    Public vTable As Object
    Public vConfigArray As Object
    Public vConfig As Object
    Public ConfigName As String
    Public swTable As TableAnnotation

    Public DadosArquivoCorrente As New ClDadosArquivoCorrente

    Public MyTaskPanelHost As New Painel_Leitura_Dados

    Public OrdemServico As New CLOrdemServico

    Public BarramentoProcessamento As New frmProcessamentovb

    Public ContadorBarradeProgresso As Integer

    Public Projeto As New clProjeto

    Public OrdemServicoItemPendencia As New CLOrdemServicoItemPendencia

    Public QualidadeSGQ As New ClQualidadeSGQ

    Public colunas() As String

    Public qtdePecaLm As Double

    Public InserirNovoTabelas As Boolean = False

    Public IntanciaSolidWorks As New ClSolidWorks

    Public NovoIdOrdemServico As String  'Recebe o novo id da nova ordem de serviço

    Public TemplatesExcel As New ClExcel

    Public Impressora As New ClImpressao

    Public Usuario As New clUsuario

    Public ClasseOutlook As New ClOutlook

    Public EntradaLogin As New frmLogin
    Public ExportarParaOS As New frmExportarParaOS
    Public MateriaisAlmoxarifado As New frmMateriaisAlmoxarifado
    Public Arquivos As New FrmBuscarProgramas

    ' Public Projeto As New frmProjetos
    Public PendenciasRNC As New frmRNC

    Public CriaProdutos As New frmCriaProdutos
    Public InspecaoQualidade As New frmInspecaoQualidade
    Public OpcaoLiberacaoOrdemServico As New frmOpcaoLiberacaoOrdemServico

    Public AjudaAI As New clAjudaAI

    Public frmAI As New frmIAAnaliseTecnica

    Public MaterialProtheus As New frmMateriaisProtheus
    Public MaterialOmie As New frmMateriaisOmie

    Public AjudaLynx As New FrmAjudaLynx

    Public TelaCarregamento As New frmTelaCarregamento

    Public ClSwAddin As New SwAddin

    ' Public Omie As New ClOmie

    Public dtDesenhos As System.Data.DataTable
    Public TabelaViewMontaPeca As System.Data.DataTable
    Public TabelaRnc As System.Data.DataTable

    Public IdMontaPeca As Integer

    Public TituloPadraoProduto As String

    Public HoraServidor As String

    Public ClasseEmail As New clEmail

    '*****************************************************
    'CLASSES ESPECIFICAS DE CLIENTES

    Public PadraoMetta As New clPadraoMetta

    Public pdfsinco As New clPdf ' Trabalhando com arquivos PDF

    Public DescricaoFinalizacaoPendencia As String

    Public ExtensaoArquivoCorrente As String

    Public EnviarEmailLiberacaoOS As String

    Public TipoLiberacaoOrdemServico As String

    Public IdMAterial As String

    Public VCampo0 As String = ""
    Public VCampo1 As String = ""
    Public VCampo2 As String = ""
    Public VCampo3 As String = ""
    Public VCampo4 As String = ""
    Public VCampo5 As String = ""
    Public VCampo6 As String = ""
    Public VCampo7 As String = ""
    Public VCampo8 As String = ""
    Public VCampo9 As String = ""
    Public VCampo10 As String = ""

    Public estatus As Boolean = False

    Public BloqueaArquivoExistente As Boolean = False

    Public vemdalista As Boolean = False

    Public IdEmpresa As String = ""
    Public descempresa As String = ""

End Module