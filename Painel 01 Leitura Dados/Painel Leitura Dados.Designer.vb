<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Painel_Leitura_Dados
    Inherits System.Windows.Forms.UserControl

    'O UserControl substitui o descarte para limpar a lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Painel_Leitura_Dados))
        Me.tpgPrincipal = New System.Windows.Forms.TabControl()
        Me.tpgFolhaDados = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkAtualizacao = New System.Windows.Forms.CheckBox()
        Me.txtAprovado = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.chkBoxTipoDesenho = New System.Windows.Forms.CheckedListBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.lblProfundidadeTotalCaixaDelimitadora = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblAlturaTotalCaixaDelimitadora = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblPeso = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblMaterial = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblLarguraTotalCaixaDelimitadora = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblAreaPintura = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblNumeroDobra = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lblComprimento = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblLargura = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblEspessura = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtTitulo = New System.Windows.Forms.TextBox()
        Me.btnPendencias = New System.Windows.Forms.Button()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.chkBoxAcabamento = New System.Windows.Forms.CheckedListBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkBoxProcessos = New System.Windows.Forms.CheckedListBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.optProcessoSoldagemSim = New System.Windows.Forms.RadioButton()
        Me.optProcessoSoldagemNao = New System.Windows.Forms.RadioButton()
        Me.txtAuthor = New System.Windows.Forms.TextBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.OPTEstoqueSim = New System.Windows.Forms.RadioButton()
        Me.OPTEstoqueNao = New System.Windows.Forms.RadioButton()
        Me.txtVerificado = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.chkVerificarLXDS = New System.Windows.Forms.CheckBox()
        Me.txtAssuntoSubiTitulo = New System.Windows.Forms.TextBox()
        Me.chkVerificarPDF = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkVerificarDFT = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkVerificarDXF = New System.Windows.Forms.CheckBox()
        Me.txtComentarios = New System.Windows.Forms.TextBox()
        Me.txtPalavraChave = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BnPrincipal = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.tspOpcaoSalvamentoAUTOMADICO = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton()
        Me.tsbAjuda = New System.Windows.Forms.ToolStripButton()
        Me.tsBLerDados = New System.Windows.Forms.ToolStripButton()
        Me.tsbSalvar = New System.Windows.Forms.ToolStripButton()
        Me.tsbConverterDXF = New System.Windows.Forms.ToolStripButton()
        Me.TSBConverterPDF = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton()
        Me.tsbFerramentas = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripSeparator22 = New System.Windows.Forms.ToolStripSeparator()
        Me.AtualizarDesenhoPeloDiretorioToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TrocarFormatoA3PeloDiretorioSelecionadoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsarFormatoA3ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsarFormatoA4ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsarFornatoA4DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsbConfiguracoes = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ConfiguraçãoToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfExportarArquivoParaOSToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator21 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSBAssociarMaterial = New System.Windows.Forms.ToolStripButton()
        Me.tsbInserirNaOS = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator23 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator24 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator25 = New System.Windows.Forms.ToolStripSeparator()
        Me.TsbInspecaoQualidade = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator39 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton9 = New System.Windows.Forms.ToolStripButton()
        Me.txtNomeArquivo = New System.Windows.Forms.ToolStripTextBox()
        Me.tslVersaoSistema = New System.Windows.Forms.ToolStripLabel()
        Me.ProgresseBarPrincipal = New System.Windows.Forms.ToolStripProgressBar()
        Me.lblResumo = New System.Windows.Forms.ToolStripLabel()
        Me.tpgBom = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.dgvDataGridBOM = New System.Windows.Forms.DataGridView()
        Me.DGVIconeLXDS = New System.Windows.Forms.DataGridViewImageColumn()
        Me.DGVIconeDXF = New System.Windows.Forms.DataGridViewImageColumn()
        Me.dgvIconePDF = New System.Windows.Forms.DataGridViewImageColumn()
        Me.mnudgvDataGridBOM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripSeparator34 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator33 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator35 = New System.Windows.Forms.ToolStripSeparator()
        Me.AbrirLXDSDaLinhaSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator36 = New System.Windows.Forms.ToolStripSeparator()
        Me.FaçaUmaAnalizeTecnicaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator31 = New System.Windows.Forms.ToolStripSeparator()
        Me.BuscarListaDeMaterialNoFAPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LocarCadasttroDeDesenhoPeloNumweroDoDocumentoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarListaDePeçasAvulçasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.chkTrocarFormato = New System.Windows.Forms.CheckBox()
        Me.choBloqueaArquivoExistente = New System.Windows.Forms.CheckBox()
        Me.chkConverterPDF = New System.Windows.Forms.CheckBox()
        Me.chkConverterDXF = New System.Windows.Forms.CheckBox()
        Me.BindingNavigator2 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripButton11 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator26 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator28 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbProcessaoListaMaterialBOM = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator27 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator37 = New System.Windows.Forms.ToolStripSeparator()
        Me.TsbAtualizarBOM = New System.Windows.Forms.ToolStripButton()
        Me.tsbTrocarFormato = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator38 = New System.Windows.Forms.ToolStripSeparator()
        Me.ProgressBarListaSW = New System.Windows.Forms.ToolStripProgressBar()
        Me.lblOrdemServicoAtiva = New System.Windows.Forms.ToolStripLabel()
        Me.tpgOrdemServico = New System.Windows.Forms.TabPage()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.TabControlOS = New System.Windows.Forms.TabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DGVListaMaterialSW = New System.Windows.Forms.DataGridView()
        Me.dgvSelecao = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgvIconeItemOS = New System.Windows.Forms.DataGridViewImageColumn()
        Me.dgvDXF = New System.Windows.Forms.DataGridViewImageColumn()
        Me.dgvPDF = New System.Windows.Forms.DataGridViewImageColumn()
        Me.mnuDGVListaMaterialSW = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.MarcarTodosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DesmarcarTodosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InverterSeleçãoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator()
        Me.AbrirDXFDaLinhaSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AbrirDWRDaLinhaSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DesmarcarComoConjuntoPrincipalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ImprimirDesenhoPDFSelecionadoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator18 = New System.Windows.Forms.ToolStripSeparator()
        Me.GerarPDFDasLinhasSelecionadasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator19 = New System.Windows.Forms.ToolStripSeparator()
        Me.GerarDXFDasLinhasSelecionadasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.DGVListaMaterialSWMateriais = New System.Windows.Forms.DataGridView()
        Me.txtPesqNumeroDesenho = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.cboOpcoesAcabamento = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtPesqAcabamentoDesenho = New System.Windows.Forms.TextBox()
        Me.btnAplicarAcabamento = New System.Windows.Forms.Button()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtPesqTipoDesenho = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lbltxtSaldoTag = New System.Windows.Forms.Label()
        Me.lbltxtQtdeLiberada = New System.Windows.Forms.Label()
        Me.lbltxtQtdeTag = New System.Windows.Forms.Label()
        Me.dgvos = New System.Windows.Forms.DataGridView()
        Me.dgvStatus = New System.Windows.Forms.DataGridViewImageColumn()
        Me.mnudgvos = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AbrirPastaDaOrdemDeServiçoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CancelarLiberaçãoDaOSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.AlterarOFatorMultipçlicadorDaOSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.GeralExcelDaOSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator17 = New System.Windows.Forms.ToolStripSeparator()
        Me.CancelarAFabricaçãoDaOSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.GerarArquivoEmDXFToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.GerarArquivoEmPDFToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator16 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator20 = New System.Windows.Forms.ToolStripSeparator()
        Me.CriarUmCopiaDaOSSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator30 = New System.Windows.Forms.ToolStripSeparator()
        Me.InserirMaterialPeloFAPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InserirNumeroOPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblFator = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.chkMostraLiberadasPelaEngenharia = New System.Windows.Forms.CheckBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtPesqCriadoPor = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtDescricao = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtCliente = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cboProjeto = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cboTag = New System.Windows.Forms.ComboBox()
        Me.txtDescricaoTag = New System.Windows.Forms.TextBox()
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripButton10 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.TSBSalvarOrdemServico = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ProgressBarProcessoLiberacaoOrdemServico = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripSeparator29 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.dgvTimerMedidordgvMedidorProjetistaOSMes = New System.Windows.Forms.DataGridView()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.dgvTimerMedidordgvMedidorPesoProducao = New System.Windows.Forms.DataGridView()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.dgvMedidorNumeroOS = New System.Windows.Forms.DataGridView()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.btnDadosUsuario = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.txtPesqTagProjetista = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtPesqProjetoProjetista = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.chkProjetista = New System.Windows.Forms.CheckBox()
        Me.chkMostraProjetoTagsFinalizadas = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.dgvPlanejamentoProjetista = New System.Windows.Forms.DataGridView()
        Me.dgvEngpro = New System.Windows.Forms.DataGridViewImageColumn()
        Me.dgvcaminhoPDF = New System.Windows.Forms.DataGridViewImageColumn()
        Me.mnuTimerdgvPlanejamentoProjetista = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MarcarIncioDaExecuçãoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MarcarFinalizaçãoDoProjetoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tpbOrdemServico = New System.Windows.Forms.TabPage()
        Me.btnAtualizarDadosOrdemServicoItens = New System.Windows.Forms.Button()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.dgvOrdemservico = New System.Windows.Forms.DataGridView()
        Me.dgvEng = New System.Windows.Forms.DataGridViewImageColumn()
        Me.txtOS = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.TXTTag = New System.Windows.Forms.TextBox()
        Me.TXTPROJETO = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.mnuDGVMontaPeca = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExcluirOMaterialDoDesenhoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnubtnListaMaterial = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.InformeOTituloPadrãoDoProdutoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimerMontaPeca = New System.Windows.Forms.Timer(Me.components)
        Me.Timerdgvos = New System.Windows.Forms.Timer(Me.components)
        Me.TimerDGVListaMaterialSW = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTipAjuda = New System.Windows.Forms.ToolTip(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.TimerHoraServidor = New System.Windows.Forms.Timer(Me.components)
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.TimerProdutoItens = New System.Windows.Forms.Timer(Me.components)
        Me.TimerFiltroPecaAtivaOS = New System.Windows.Forms.Timer(Me.components)
        Me.TimerProdutos = New System.Windows.Forms.Timer(Me.components)
        Me.TimerpcpAgrupamentoProjeto = New System.Windows.Forms.Timer(Me.components)
        Me.TimerMedidordgvMedidorNumeroOS = New System.Windows.Forms.Timer(Me.components)
        Me.TimerAviso = New System.Windows.Forms.Timer(Me.components)
        Me.TimerdgvPlanejamentoProjetista = New System.Windows.Forms.Timer(Me.components)
        Me.TimerOrdemSevico = New System.Windows.Forms.Timer(Me.components)
        Me.TimerdgvOrdemservico = New System.Windows.Forms.Timer(Me.components)
        Me.tpgPrincipal.SuspendLayout()
        Me.tpgFolhaDados.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        CType(Me.BnPrincipal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BnPrincipal.SuspendLayout()
        Me.tpgBom.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.dgvDataGridBOM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnudgvDataGridBOM.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        CType(Me.BindingNavigator2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator2.SuspendLayout()
        Me.tpgOrdemServico.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.TabControlOS.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DGVListaMaterialSW, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuDGVListaMaterialSW.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.DGVListaMaterialSWMateriais, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.dgvos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnudgvos.SuspendLayout()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgvTimerMedidordgvMedidorProjetistaOSMes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTimerMedidordgvMedidorPesoProducao, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        CType(Me.dgvMedidorNumeroOS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.dgvPlanejamentoProjetista, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuTimerdgvPlanejamentoProjetista.SuspendLayout()
        Me.tpbOrdemServico.SuspendLayout()
        CType(Me.dgvOrdemservico, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuDGVMontaPeca.SuspendLayout()
        Me.mnubtnListaMaterial.SuspendLayout()
        Me.SuspendLayout()
        '
        'tpgPrincipal
        '
        Me.tpgPrincipal.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tpgPrincipal.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tpgPrincipal.Controls.Add(Me.tpgFolhaDados)
        Me.tpgPrincipal.Controls.Add(Me.tpgBom)
        Me.tpgPrincipal.Controls.Add(Me.tpgOrdemServico)
        Me.tpgPrincipal.Controls.Add(Me.TabPage1)
        Me.tpgPrincipal.Controls.Add(Me.TabPage4)
        Me.tpgPrincipal.Controls.Add(Me.tpbOrdemServico)
        Me.tpgPrincipal.Location = New System.Drawing.Point(3, 5)
        Me.tpgPrincipal.Name = "tpgPrincipal"
        Me.tpgPrincipal.SelectedIndex = 0
        Me.tpgPrincipal.Size = New System.Drawing.Size(656, 776)
        Me.tpgPrincipal.TabIndex = 0
        '
        'tpgFolhaDados
        '
        Me.tpgFolhaDados.Controls.Add(Me.Panel1)
        Me.tpgFolhaDados.Controls.Add(Me.BnPrincipal)
        Me.tpgFolhaDados.Location = New System.Drawing.Point(4, 25)
        Me.tpgFolhaDados.Name = "tpgFolhaDados"
        Me.tpgFolhaDados.Padding = New System.Windows.Forms.Padding(3)
        Me.tpgFolhaDados.Size = New System.Drawing.Size(648, 747)
        Me.tpgFolhaDados.TabIndex = 0
        Me.tpgFolhaDados.Text = "Dados Principais"
        Me.tpgFolhaDados.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkAtualizacao)
        Me.Panel1.Controls.Add(Me.txtAprovado)
        Me.Panel1.Controls.Add(Me.GroupBox4)
        Me.Panel1.Controls.Add(Me.GroupBox5)
        Me.Panel1.Controls.Add(Me.txtTitulo)
        Me.Panel1.Controls.Add(Me.btnPendencias)
        Me.Panel1.Controls.Add(Me.GroupBox10)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.GroupBox8)
        Me.Panel1.Controls.Add(Me.txtAuthor)
        Me.Panel1.Controls.Add(Me.GroupBox7)
        Me.Panel1.Controls.Add(Me.txtVerificado)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.chkVerificarLXDS)
        Me.Panel1.Controls.Add(Me.txtAssuntoSubiTitulo)
        Me.Panel1.Controls.Add(Me.chkVerificarPDF)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.chkVerificarDFT)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.chkVerificarDXF)
        Me.Panel1.Controls.Add(Me.txtComentarios)
        Me.Panel1.Controls.Add(Me.txtPalavraChave)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(642, 690)
        Me.Panel1.TabIndex = 44
        '
        'chkAtualizacao
        '
        Me.chkAtualizacao.Image = Global.SwLynx_4._1.My.Resources.Resources.desbloqueado
        Me.chkAtualizacao.Location = New System.Drawing.Point(11, 12)
        Me.chkAtualizacao.Name = "chkAtualizacao"
        Me.chkAtualizacao.Size = New System.Drawing.Size(140, 26)
        Me.chkAtualizacao.TabIndex = 0
        Me.chkAtualizacao.Text = "Desbloqueado"
        Me.chkAtualizacao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkAtualizacao.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTipAjuda.SetToolTip(Me.chkAtualizacao, "Quandop esta opção esta marcada não e possivel gerar arquivos PDF's/DXF's pelo Ly" &
        "nx")
        Me.chkAtualizacao.UseVisualStyleBackColor = True
        '
        'txtAprovado
        '
        Me.txtAprovado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAprovado.Location = New System.Drawing.Point(367, 53)
        Me.txtAprovado.Name = "txtAprovado"
        Me.txtAprovado.Size = New System.Drawing.Size(78, 20)
        Me.txtAprovado.TabIndex = 5
        Me.ToolTipAjuda.SetToolTip(Me.txtAprovado, "Autor: Sigla do projetista responsável pela criação da peça.")
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.chkBoxTipoDesenho)
        Me.GroupBox4.Location = New System.Drawing.Point(166, 478)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox4.Size = New System.Drawing.Size(200, 210)
        Me.GroupBox4.TabIndex = 28
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Tipo Desenho"
        Me.ToolTipAjuda.SetToolTip(Me.GroupBox4, "Tipo de Desenho: Indicação da categoria do desenho (peça usinada, chaparia, etc.)" &
        ". *" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'chkBoxTipoDesenho
        '
        Me.chkBoxTipoDesenho.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkBoxTipoDesenho.CheckOnClick = True
        Me.chkBoxTipoDesenho.FormattingEnabled = True
        Me.chkBoxTipoDesenho.Location = New System.Drawing.Point(8, 17)
        Me.chkBoxTipoDesenho.Margin = New System.Windows.Forms.Padding(2)
        Me.chkBoxTipoDesenho.Name = "chkBoxTipoDesenho"
        Me.chkBoxTipoDesenho.ScrollAlwaysVisible = True
        Me.chkBoxTipoDesenho.Size = New System.Drawing.Size(186, 154)
        Me.chkBoxTipoDesenho.Sorted = True
        Me.chkBoxTipoDesenho.TabIndex = 16
        Me.ToolTipAjuda.SetToolTip(Me.chkBoxTipoDesenho, "Tipo de Desenho: Indicação da categoria do desenho (peça usinada, chaparia, etc.)" &
        ". *" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.lblProfundidadeTotalCaixaDelimitadora)
        Me.GroupBox5.Controls.Add(Me.Label6)
        Me.GroupBox5.Controls.Add(Me.lblAlturaTotalCaixaDelimitadora)
        Me.GroupBox5.Controls.Add(Me.Label7)
        Me.GroupBox5.Controls.Add(Me.lblPeso)
        Me.GroupBox5.Controls.Add(Me.Label14)
        Me.GroupBox5.Controls.Add(Me.lblMaterial)
        Me.GroupBox5.Controls.Add(Me.Label13)
        Me.GroupBox5.Controls.Add(Me.lblLarguraTotalCaixaDelimitadora)
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Controls.Add(Me.lblAreaPintura)
        Me.GroupBox5.Controls.Add(Me.Label12)
        Me.GroupBox5.Controls.Add(Me.lblNumeroDobra)
        Me.GroupBox5.Controls.Add(Me.Label23)
        Me.GroupBox5.Controls.Add(Me.lblComprimento)
        Me.GroupBox5.Controls.Add(Me.Label22)
        Me.GroupBox5.Controls.Add(Me.lblLargura)
        Me.GroupBox5.Controls.Add(Me.Label21)
        Me.GroupBox5.Controls.Add(Me.lblEspessura)
        Me.GroupBox5.Controls.Add(Me.Label17)
        Me.GroupBox5.Location = New System.Drawing.Point(5, 302)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox5.Size = New System.Drawing.Size(158, 243)
        Me.GroupBox5.TabIndex = 29
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Dados técnicos:"
        Me.ToolTipAjuda.SetToolTip(Me.GroupBox5, "Processo – Lista de Corte: Dados coletados diretamente do SolidWorks. *" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'lblProfundidadeTotalCaixaDelimitadora
        '
        Me.lblProfundidadeTotalCaixaDelimitadora.AutoSize = True
        Me.lblProfundidadeTotalCaixaDelimitadora.BackColor = System.Drawing.Color.Transparent
        Me.lblProfundidadeTotalCaixaDelimitadora.Location = New System.Drawing.Point(64, 216)
        Me.lblProfundidadeTotalCaixaDelimitadora.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblProfundidadeTotalCaixaDelimitadora.Name = "lblProfundidadeTotalCaixaDelimitadora"
        Me.lblProfundidadeTotalCaixaDelimitadora.Size = New System.Drawing.Size(19, 13)
        Me.lblProfundidadeTotalCaixaDelimitadora.TabIndex = 25
        Me.lblProfundidadeTotalCaixaDelimitadora.Text = "00"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(32, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(31, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Esp.:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAlturaTotalCaixaDelimitadora
        '
        Me.lblAlturaTotalCaixaDelimitadora.AutoSize = True
        Me.lblAlturaTotalCaixaDelimitadora.BackColor = System.Drawing.Color.Transparent
        Me.lblAlturaTotalCaixaDelimitadora.Location = New System.Drawing.Point(64, 172)
        Me.lblAlturaTotalCaixaDelimitadora.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblAlturaTotalCaixaDelimitadora.Name = "lblAlturaTotalCaixaDelimitadora"
        Me.lblAlturaTotalCaixaDelimitadora.Size = New System.Drawing.Size(19, 13)
        Me.lblAlturaTotalCaixaDelimitadora.TabIndex = 24
        Me.lblAlturaTotalCaixaDelimitadora.Text = "00"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(29, 41)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(34, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Larg.:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPeso
        '
        Me.lblPeso.AutoSize = True
        Me.lblPeso.BackColor = System.Drawing.Color.Transparent
        Me.lblPeso.Location = New System.Drawing.Point(64, 132)
        Me.lblPeso.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPeso.Name = "lblPeso"
        Me.lblPeso.Size = New System.Drawing.Size(19, 13)
        Me.lblPeso.TabIndex = 23
        Me.lblPeso.Text = "00"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(4, 89)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(59, 13)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "Nº Dobras:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMaterial
        '
        Me.lblMaterial.AutoSize = True
        Me.lblMaterial.BackColor = System.Drawing.Color.Transparent
        Me.lblMaterial.Location = New System.Drawing.Point(64, 153)
        Me.lblMaterial.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblMaterial.Name = "lblMaterial"
        Me.lblMaterial.Size = New System.Drawing.Size(19, 13)
        Me.lblMaterial.TabIndex = 22
        Me.lblMaterial.Text = "00"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(16, 110)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(47, 13)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "Area M²:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLarguraTotalCaixaDelimitadora
        '
        Me.lblLarguraTotalCaixaDelimitadora.AutoSize = True
        Me.lblLarguraTotalCaixaDelimitadora.BackColor = System.Drawing.Color.Transparent
        Me.lblLarguraTotalCaixaDelimitadora.Location = New System.Drawing.Point(64, 193)
        Me.lblLarguraTotalCaixaDelimitadora.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblLarguraTotalCaixaDelimitadora.Name = "lblLarguraTotalCaixaDelimitadora"
        Me.lblLarguraTotalCaixaDelimitadora.Size = New System.Drawing.Size(19, 13)
        Me.lblLarguraTotalCaixaDelimitadora.TabIndex = 21
        Me.lblLarguraTotalCaixaDelimitadora.Text = "00"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(22, 65)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(40, 13)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Comp.:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAreaPintura
        '
        Me.lblAreaPintura.AutoSize = True
        Me.lblAreaPintura.BackColor = System.Drawing.Color.Transparent
        Me.lblAreaPintura.Location = New System.Drawing.Point(64, 110)
        Me.lblAreaPintura.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblAreaPintura.Name = "lblAreaPintura"
        Me.lblAreaPintura.Size = New System.Drawing.Size(19, 13)
        Me.lblAreaPintura.TabIndex = 20
        Me.lblAreaPintura.Text = "00"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(13, 132)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(52, 13)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "Kg/Peso:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNumeroDobra
        '
        Me.lblNumeroDobra.AutoSize = True
        Me.lblNumeroDobra.BackColor = System.Drawing.Color.Transparent
        Me.lblNumeroDobra.Location = New System.Drawing.Point(64, 89)
        Me.lblNumeroDobra.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblNumeroDobra.Name = "lblNumeroDobra"
        Me.lblNumeroDobra.Size = New System.Drawing.Size(19, 13)
        Me.lblNumeroDobra.TabIndex = 19
        Me.lblNumeroDobra.Text = "00"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(26, 172)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(37, 13)
        Me.Label23.TabIndex = 7
        Me.Label23.Text = "Altura:"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblComprimento
        '
        Me.lblComprimento.AutoSize = True
        Me.lblComprimento.BackColor = System.Drawing.Color.Transparent
        Me.lblComprimento.Location = New System.Drawing.Point(64, 65)
        Me.lblComprimento.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblComprimento.Name = "lblComprimento"
        Me.lblComprimento.Size = New System.Drawing.Size(19, 13)
        Me.lblComprimento.TabIndex = 18
        Me.lblComprimento.Text = "00"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(17, 193)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(46, 13)
        Me.Label22.TabIndex = 8
        Me.Label22.Text = "Largura:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLargura
        '
        Me.lblLargura.AutoSize = True
        Me.lblLargura.BackColor = System.Drawing.Color.Transparent
        Me.lblLargura.Location = New System.Drawing.Point(64, 41)
        Me.lblLargura.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblLargura.Name = "lblLargura"
        Me.lblLargura.Size = New System.Drawing.Size(19, 13)
        Me.lblLargura.TabIndex = 17
        Me.lblLargura.Text = "00"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(15, 216)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(50, 13)
        Me.Label21.TabIndex = 9
        Me.Label21.Text = "Profund.:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblEspessura
        '
        Me.lblEspessura.AutoSize = True
        Me.lblEspessura.BackColor = System.Drawing.Color.Transparent
        Me.lblEspessura.Location = New System.Drawing.Point(64, 20)
        Me.lblEspessura.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblEspessura.Name = "lblEspessura"
        Me.lblEspessura.Size = New System.Drawing.Size(19, 13)
        Me.lblEspessura.TabIndex = 16
        Me.lblEspessura.Text = "00"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(16, 153)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(46, 13)
        Me.Label17.TabIndex = 7
        Me.Label17.Text = "material:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTitulo
        '
        Me.txtTitulo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTitulo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTitulo.Location = New System.Drawing.Point(75, 163)
        Me.txtTitulo.Name = "txtTitulo"
        Me.txtTitulo.Size = New System.Drawing.Size(475, 20)
        Me.txtTitulo.TabIndex = 8
        Me.ToolTipAjuda.SetToolTip(Me.txtTitulo, "Título: Linha de produto e/ou família de aplicação da peça. *")
        '
        'btnPendencias
        '
        Me.btnPendencias.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPendencias.Enabled = False
        Me.btnPendencias.Image = Global.SwLynx_4._1.My.Resources.Resources.atencao
        Me.btnPendencias.Location = New System.Drawing.Point(165, 6)
        Me.btnPendencias.Margin = New System.Windows.Forms.Padding(2)
        Me.btnPendencias.Name = "btnPendencias"
        Me.btnPendencias.Size = New System.Drawing.Size(474, 39)
        Me.btnPendencias.TabIndex = 23
        Me.btnPendencias.Text = " Pendências"
        Me.btnPendencias.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTipAjuda.SetToolTip(Me.btnPendencias, "Lista as RNC's da peça corrente.")
        Me.btnPendencias.UseVisualStyleBackColor = True
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.chkBoxAcabamento)
        Me.GroupBox10.Location = New System.Drawing.Point(166, 224)
        Me.GroupBox10.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox10.Size = New System.Drawing.Size(200, 246)
        Me.GroupBox10.TabIndex = 20
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Acabamento"
        Me.ToolTipAjuda.SetToolTip(Me.GroupBox10, "Acabamento: Tipo de acabamento padrão da peça ou produto, podendo ser alterado na" &
        " Ordem de Serviço (OS)." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'chkBoxAcabamento
        '
        Me.chkBoxAcabamento.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkBoxAcabamento.CheckOnClick = True
        Me.chkBoxAcabamento.FormattingEnabled = True
        Me.chkBoxAcabamento.Location = New System.Drawing.Point(4, 17)
        Me.chkBoxAcabamento.Margin = New System.Windows.Forms.Padding(2)
        Me.chkBoxAcabamento.Name = "chkBoxAcabamento"
        Me.chkBoxAcabamento.ScrollAlwaysVisible = True
        Me.chkBoxAcabamento.Size = New System.Drawing.Size(192, 199)
        Me.chkBoxAcabamento.Sorted = True
        Me.chkBoxAcabamento.TabIndex = 18
        Me.ToolTipAjuda.SetToolTip(Me.chkBoxAcabamento, "Acabamento: Tipo de acabamento padrão da peça ou produto, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "podendo ser alterado " &
        "na Ordem de Serviço (OS)." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.chkBoxProcessos)
        Me.GroupBox1.Location = New System.Drawing.Point(370, 224)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(265, 460)
        Me.GroupBox1.TabIndex = 38
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Processos"
        Me.ToolTipAjuda.SetToolTip(Me.GroupBox1, "Processo: Indicação dos processos necessários para a fabricação da peça ou montag" &
        "em. *")
        '
        'chkBoxProcessos
        '
        Me.chkBoxProcessos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkBoxProcessos.CheckOnClick = True
        Me.chkBoxProcessos.FormattingEnabled = True
        Me.chkBoxProcessos.Location = New System.Drawing.Point(4, 15)
        Me.chkBoxProcessos.Margin = New System.Windows.Forms.Padding(2)
        Me.chkBoxProcessos.Name = "chkBoxProcessos"
        Me.chkBoxProcessos.ScrollAlwaysVisible = True
        Me.chkBoxProcessos.Size = New System.Drawing.Size(257, 409)
        Me.chkBoxProcessos.Sorted = True
        Me.chkBoxProcessos.TabIndex = 18
        Me.ToolTipAjuda.SetToolTip(Me.chkBoxProcessos, "Selecione os Setores/Processos de fabricação do desenho corrente, esta seleção ir" &
        "a definir" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "no SINCO a forma e os processo de controle de produção.")
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(309, 55)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 13)
        Me.Label10.TabIndex = 43
        Me.Label10.Text = "Aprovado:"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.optProcessoSoldagemSim)
        Me.GroupBox8.Controls.Add(Me.optProcessoSoldagemNao)
        Me.GroupBox8.Location = New System.Drawing.Point(87, 224)
        Me.GroupBox8.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox8.Size = New System.Drawing.Size(76, 65)
        Me.GroupBox8.TabIndex = 16
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Soldagem:"
        Me.ToolTipAjuda.SetToolTip(Me.GroupBox8, "Soldagem: Especificação para desenhos de conjuntos cuja montagem será realizada p" &
        "or soldagem." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'optProcessoSoldagemSim
        '
        Me.optProcessoSoldagemSim.AutoSize = True
        Me.optProcessoSoldagemSim.Location = New System.Drawing.Point(4, 17)
        Me.optProcessoSoldagemSim.Margin = New System.Windows.Forms.Padding(2)
        Me.optProcessoSoldagemSim.Name = "optProcessoSoldagemSim"
        Me.optProcessoSoldagemSim.Size = New System.Drawing.Size(44, 17)
        Me.optProcessoSoldagemSim.TabIndex = 13
        Me.optProcessoSoldagemSim.TabStop = True
        Me.optProcessoSoldagemSim.Text = "SIM"
        Me.optProcessoSoldagemSim.UseVisualStyleBackColor = True
        '
        'optProcessoSoldagemNao
        '
        Me.optProcessoSoldagemNao.AutoSize = True
        Me.optProcessoSoldagemNao.Checked = True
        Me.optProcessoSoldagemNao.Location = New System.Drawing.Point(4, 37)
        Me.optProcessoSoldagemNao.Margin = New System.Windows.Forms.Padding(2)
        Me.optProcessoSoldagemNao.Name = "optProcessoSoldagemNao"
        Me.optProcessoSoldagemNao.Size = New System.Drawing.Size(48, 17)
        Me.optProcessoSoldagemNao.TabIndex = 14
        Me.optProcessoSoldagemNao.TabStop = True
        Me.optProcessoSoldagemNao.Text = "NÃO"
        Me.optProcessoSoldagemNao.UseVisualStyleBackColor = True
        '
        'txtAuthor
        '
        Me.txtAuthor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAuthor.Location = New System.Drawing.Point(56, 53)
        Me.txtAuthor.Name = "txtAuthor"
        Me.txtAuthor.Size = New System.Drawing.Size(78, 20)
        Me.txtAuthor.TabIndex = 3
        Me.ToolTipAjuda.SetToolTip(Me.txtAuthor, "Autor: Sigla do projetista responsável pela criação da peça." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "De um duplo clik pa" &
        "ra carregar a sigla do usuário logado.")
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.OPTEstoqueSim)
        Me.GroupBox7.Controls.Add(Me.OPTEstoqueNao)
        Me.GroupBox7.Location = New System.Drawing.Point(7, 224)
        Me.GroupBox7.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox7.Size = New System.Drawing.Size(72, 65)
        Me.GroupBox7.TabIndex = 15
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Estoque:"
        Me.ToolTipAjuda.SetToolTip(Me.GroupBox7, "Estoque: Identificação das peças definidas como KANBAN." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " ")
        '
        'OPTEstoqueSim
        '
        Me.OPTEstoqueSim.AutoSize = True
        Me.OPTEstoqueSim.Location = New System.Drawing.Point(4, 17)
        Me.OPTEstoqueSim.Margin = New System.Windows.Forms.Padding(2)
        Me.OPTEstoqueSim.Name = "OPTEstoqueSim"
        Me.OPTEstoqueSim.Size = New System.Drawing.Size(44, 17)
        Me.OPTEstoqueSim.TabIndex = 13
        Me.OPTEstoqueSim.TabStop = True
        Me.OPTEstoqueSim.Text = "SIM"
        Me.OPTEstoqueSim.UseVisualStyleBackColor = True
        '
        'OPTEstoqueNao
        '
        Me.OPTEstoqueNao.AutoSize = True
        Me.OPTEstoqueNao.Checked = True
        Me.OPTEstoqueNao.Location = New System.Drawing.Point(4, 37)
        Me.OPTEstoqueNao.Margin = New System.Windows.Forms.Padding(2)
        Me.OPTEstoqueNao.Name = "OPTEstoqueNao"
        Me.OPTEstoqueNao.Size = New System.Drawing.Size(48, 17)
        Me.OPTEstoqueNao.TabIndex = 14
        Me.OPTEstoqueNao.TabStop = True
        Me.OPTEstoqueNao.Text = "NÃO"
        Me.OPTEstoqueNao.UseVisualStyleBackColor = True
        '
        'txtVerificado
        '
        Me.txtVerificado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtVerificado.Location = New System.Drawing.Point(207, 53)
        Me.txtVerificado.Name = "txtVerificado"
        Me.txtVerificado.Size = New System.Drawing.Size(78, 20)
        Me.txtVerificado.TabIndex = 4
        Me.ToolTipAjuda.SetToolTip(Me.txtVerificado, "Autor: Sigla do projetista responsável pela criação da peça.")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Author:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(148, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 41
        Me.Label9.Text = "Verificado:"
        '
        'chkVerificarLXDS
        '
        Me.chkVerificarLXDS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkVerificarLXDS.Image = Global.SwLynx_4._1.My.Resources.Resources.CYPCUT
        Me.chkVerificarLXDS.Location = New System.Drawing.Point(556, 174)
        Me.chkVerificarLXDS.Name = "chkVerificarLXDS"
        Me.chkVerificarLXDS.Size = New System.Drawing.Size(79, 35)
        Me.chkVerificarLXDS.TabIndex = 22
        Me.chkVerificarLXDS.Text = "LXDS"
        Me.chkVerificarLXDS.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkVerificarLXDS.UseVisualStyleBackColor = True
        '
        'txtAssuntoSubiTitulo
        '
        Me.txtAssuntoSubiTitulo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAssuntoSubiTitulo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAssuntoSubiTitulo.Location = New System.Drawing.Point(74, 194)
        Me.txtAssuntoSubiTitulo.Name = "txtAssuntoSubiTitulo"
        Me.txtAssuntoSubiTitulo.Size = New System.Drawing.Size(475, 20)
        Me.txtAssuntoSubiTitulo.TabIndex = 9
        Me.ToolTipAjuda.SetToolTip(Me.txtAssuntoSubiTitulo, "Subtítulo: Nome da peça e sua aplicação específica. *")
        '
        'chkVerificarPDF
        '
        Me.chkVerificarPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkVerificarPDF.Image = Global.SwLynx_4._1.My.Resources.Resources.pdf
        Me.chkVerificarPDF.Location = New System.Drawing.Point(556, 64)
        Me.chkVerificarPDF.Name = "chkVerificarPDF"
        Me.chkVerificarPDF.Size = New System.Drawing.Size(79, 35)
        Me.chkVerificarPDF.TabIndex = 12
        Me.chkVerificarPDF.Text = "PDF"
        Me.chkVerificarPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkVerificarPDF.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(21, 197)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "SubTitulo:"
        '
        'chkVerificarDFT
        '
        Me.chkVerificarDFT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkVerificarDFT.Image = Global.SwLynx_4._1.My.Resources.Resources.DFT
        Me.chkVerificarDFT.Location = New System.Drawing.Point(556, 101)
        Me.chkVerificarDFT.Name = "chkVerificarDFT"
        Me.chkVerificarDFT.Size = New System.Drawing.Size(79, 35)
        Me.chkVerificarDFT.TabIndex = 20
        Me.chkVerificarDFT.Text = "DFT"
        Me.chkVerificarDFT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkVerificarDFT.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(39, 166)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Titulo:"
        '
        'chkVerificarDXF
        '
        Me.chkVerificarDXF.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkVerificarDXF.Image = Global.SwLynx_4._1.My.Resources.Resources.dxf
        Me.chkVerificarDXF.Location = New System.Drawing.Point(556, 137)
        Me.chkVerificarDXF.Name = "chkVerificarDXF"
        Me.chkVerificarDXF.Size = New System.Drawing.Size(79, 35)
        Me.chkVerificarDXF.TabIndex = 13
        Me.chkVerificarDXF.Text = "DXF"
        Me.chkVerificarDXF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkVerificarDXF.UseVisualStyleBackColor = True
        '
        'txtComentarios
        '
        Me.txtComentarios.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtComentarios.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtComentarios.Location = New System.Drawing.Point(74, 110)
        Me.txtComentarios.Multiline = True
        Me.txtComentarios.Name = "txtComentarios"
        Me.txtComentarios.Size = New System.Drawing.Size(475, 45)
        Me.txtComentarios.TabIndex = 7
        Me.ToolTipAjuda.SetToolTip(Me.txtComentarios, "Comentários: Incluir o máximo de informações relevantes sobre o desenho e suas ap" &
        "licações.")
        '
        'txtPalavraChave
        '
        Me.txtPalavraChave.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPalavraChave.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPalavraChave.Location = New System.Drawing.Point(109, 80)
        Me.txtPalavraChave.Name = "txtPalavraChave"
        Me.txtPalavraChave.Size = New System.Drawing.Size(442, 20)
        Me.txtPalavraChave.TabIndex = 6
        Me.ToolTipAjuda.SetToolTip(Me.txtPalavraChave, "Palavras-chave: Termos isolados para facilitar pesquisas dinâmicas.")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Palavras Chaves:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Comentários:"
        '
        'BnPrincipal
        '
        Me.BnPrincipal.AddNewItem = Nothing
        Me.BnPrincipal.AutoSize = False
        Me.BnPrincipal.CountItem = Nothing
        Me.BnPrincipal.DeleteItem = Nothing
        Me.BnPrincipal.ImageScalingSize = New System.Drawing.Size(30, 30)
        Me.BnPrincipal.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BnPrincipal.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tspOpcaoSalvamentoAUTOMADICO, Me.ToolStripButton6, Me.tsbAjuda, Me.tsBLerDados, Me.tsbSalvar, Me.tsbConverterDXF, Me.TSBConverterPDF, Me.ToolStripButton2, Me.ToolStripButton8, Me.tsbFerramentas, Me.tsbConfiguracoes, Me.TSBAssociarMaterial, Me.tsbInserirNaOS, Me.ToolStripSeparator23, Me.ToolStripSeparator24, Me.ToolStripSeparator25, Me.TsbInspecaoQualidade, Me.ToolStripSeparator39, Me.ToolStripButton9, Me.txtNomeArquivo, Me.tslVersaoSistema, Me.ProgresseBarPrincipal, Me.lblResumo})
        Me.BnPrincipal.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.BnPrincipal.Location = New System.Drawing.Point(3, 3)
        Me.BnPrincipal.MoveFirstItem = Nothing
        Me.BnPrincipal.MoveLastItem = Nothing
        Me.BnPrincipal.MoveNextItem = Nothing
        Me.BnPrincipal.MovePreviousItem = Nothing
        Me.BnPrincipal.Name = "BnPrincipal"
        Me.BnPrincipal.PositionItem = Nothing
        Me.BnPrincipal.Size = New System.Drawing.Size(642, 51)
        Me.BnPrincipal.TabIndex = 39
        Me.BnPrincipal.Text = "BindingNavigator1"
        '
        'tspOpcaoSalvamentoAUTOMADICO
        '
        Me.tspOpcaoSalvamentoAUTOMADICO.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tspOpcaoSalvamentoAUTOMADICO.Image = Global.SwLynx_4._1.My.Resources.Resources.desmarcado
        Me.tspOpcaoSalvamentoAUTOMADICO.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tspOpcaoSalvamentoAUTOMADICO.Name = "tspOpcaoSalvamentoAUTOMADICO"
        Me.tspOpcaoSalvamentoAUTOMADICO.Size = New System.Drawing.Size(34, 48)
        Me.tspOpcaoSalvamentoAUTOMADICO.Text = "Opção de Salvamento Automatico"
        Me.tspOpcaoSalvamentoAUTOMADICO.ToolTipText = "Status da opção de salvamento automatico na leitura da BOM"
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton6.Image = Global.SwLynx_4._1.My.Resources.Resources.ICONE_FOREST1
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(34, 48)
        Me.ToolStripButton6.Text = "Ativa e desativa a função Base do Lynx"
        '
        'tsbAjuda
        '
        Me.tsbAjuda.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbAjuda.Image = Global.SwLynx_4._1.My.Resources.Resources.Ajuda___Copia
        Me.tsbAjuda.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbAjuda.Name = "tsbAjuda"
        Me.tsbAjuda.Size = New System.Drawing.Size(34, 48)
        Me.tsbAjuda.Text = "ToolStripButton10"
        Me.tsbAjuda.ToolTipText = "Video de Treinamento da Tela de Dados Principais"
        '
        'tsBLerDados
        '
        Me.tsBLerDados.CheckOnClick = True
        Me.tsBLerDados.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsBLerDados.Image = Global.SwLynx_4._1.My.Resources.Resources.leitura
        Me.tsBLerDados.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBLerDados.Name = "tsBLerDados"
        Me.tsBLerDados.Size = New System.Drawing.Size(34, 48)
        Me.tsBLerDados.Text = "Ler Dados"
        Me.tsBLerDados.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBLerDados.ToolTipText = "Leitura de dados do Arquivo Corrente"
        '
        'tsbSalvar
        '
        Me.tsbSalvar.CheckOnClick = True
        Me.tsbSalvar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSalvar.Image = Global.SwLynx_4._1.My.Resources.Resources.sinco_salva___Copia
        Me.tsbSalvar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSalvar.Name = "tsbSalvar"
        Me.tsbSalvar.Size = New System.Drawing.Size(34, 48)
        Me.tsbSalvar.Text = "Salvar"
        Me.tsbSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbSalvar.ToolTipText = "Salva os dados do Arquivo Corrente"
        '
        'tsbConverterDXF
        '
        Me.tsbConverterDXF.CheckOnClick = True
        Me.tsbConverterDXF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbConverterDXF.Image = Global.SwLynx_4._1.My.Resources.Resources.sinco_DXF
        Me.tsbConverterDXF.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbConverterDXF.Name = "tsbConverterDXF"
        Me.tsbConverterDXF.Size = New System.Drawing.Size(34, 48)
        Me.tsbConverterDXF.Text = "DXF"
        Me.tsbConverterDXF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbConverterDXF.ToolTipText = "Se for um arquivo part do tipo Chapa irá converter o blank em dxf, caso haja arqu" &
    "ivo lxds o mesmo será apagado"
        '
        'TSBConverterPDF
        '
        Me.TSBConverterPDF.CheckOnClick = True
        Me.TSBConverterPDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBConverterPDF.Image = Global.SwLynx_4._1.My.Resources.Resources.sinco_PDF
        Me.TSBConverterPDF.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBConverterPDF.Name = "TSBConverterPDF"
        Me.TSBConverterPDF.Size = New System.Drawing.Size(34, 48)
        Me.TSBConverterPDF.Text = "PDF"
        Me.TSBConverterPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TSBConverterPDF.ToolTipText = "So o Arquivo Corrente for um Detalhamento irá converter em PDF"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = Global.SwLynx_4._1.My.Resources.Resources.CYPCUT
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(34, 48)
        Me.ToolStripButton2.Text = "Abrir Arquivo LXDS"
        Me.ToolStripButton2.ToolTipText = "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Abrir Arquivo LXDS, Normalmente e um extenção do CypCut, corte a L" &
    "aser."
        Me.ToolStripButton2.Visible = False
        '
        'ToolStripButton8
        '
        Me.ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton8.Image = Global.SwLynx_4._1.My.Resources.Resources.arquivo_dxf
        Me.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton8.Name = "ToolStripButton8"
        Me.ToolStripButton8.Size = New System.Drawing.Size(34, 48)
        Me.ToolStripButton8.Text = "Abrir Arquivo DXF"
        Me.ToolStripButton8.ToolTipText = "Dica de Uso" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Abrir Arquivo DXF, abre os arquivos em DXF no seu programa padrão de" &
    " edição. "
        Me.ToolStripButton8.Visible = False
        '
        'tsbFerramentas
        '
        Me.tsbFerramentas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbFerramentas.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator22, Me.AtualizarDesenhoPeloDiretorioToolStripMenuItem1, Me.TrocarFormatoA3PeloDiretorioSelecionadoToolStripMenuItem, Me.UsarFormatoA3ToolStripMenuItem, Me.UsarFormatoA4ToolStripMenuItem, Me.UsarFornatoA4DToolStripMenuItem})
        Me.tsbFerramentas.Image = Global.SwLynx_4._1.My.Resources.Resources.ferramentas
        Me.tsbFerramentas.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbFerramentas.Name = "tsbFerramentas"
        Me.tsbFerramentas.Size = New System.Drawing.Size(43, 48)
        Me.tsbFerramentas.Text = "Ferramentas"
        Me.tsbFerramentas.ToolTipText = "Ferramentas do Lynx"
        '
        'ToolStripSeparator22
        '
        Me.ToolStripSeparator22.Name = "ToolStripSeparator22"
        Me.ToolStripSeparator22.Size = New System.Drawing.Size(310, 6)
        '
        'AtualizarDesenhoPeloDiretorioToolStripMenuItem1
        '
        Me.AtualizarDesenhoPeloDiretorioToolStripMenuItem1.Name = "AtualizarDesenhoPeloDiretorioToolStripMenuItem1"
        Me.AtualizarDesenhoPeloDiretorioToolStripMenuItem1.Size = New System.Drawing.Size(313, 22)
        Me.AtualizarDesenhoPeloDiretorioToolStripMenuItem1.Text = "Cadastrar/Atualizar SINCO pelo Diretorio"
        '
        'TrocarFormatoA3PeloDiretorioSelecionadoToolStripMenuItem
        '
        Me.TrocarFormatoA3PeloDiretorioSelecionadoToolStripMenuItem.Name = "TrocarFormatoA3PeloDiretorioSelecionadoToolStripMenuItem"
        Me.TrocarFormatoA3PeloDiretorioSelecionadoToolStripMenuItem.Size = New System.Drawing.Size(313, 22)
        Me.TrocarFormatoA3PeloDiretorioSelecionadoToolStripMenuItem.Text = "Trocar Formato A3 Pelo Diretorio Selecionado"
        '
        'UsarFormatoA3ToolStripMenuItem
        '
        Me.UsarFormatoA3ToolStripMenuItem.Name = "UsarFormatoA3ToolStripMenuItem"
        Me.UsarFormatoA3ToolStripMenuItem.Size = New System.Drawing.Size(313, 22)
        Me.UsarFormatoA3ToolStripMenuItem.Text = "Usar Formato A3"
        '
        'UsarFormatoA4ToolStripMenuItem
        '
        Me.UsarFormatoA4ToolStripMenuItem.Name = "UsarFormatoA4ToolStripMenuItem"
        Me.UsarFormatoA4ToolStripMenuItem.Size = New System.Drawing.Size(313, 22)
        Me.UsarFormatoA4ToolStripMenuItem.Text = "Usar Formato A4"
        '
        'UsarFornatoA4DToolStripMenuItem
        '
        Me.UsarFornatoA4DToolStripMenuItem.Name = "UsarFornatoA4DToolStripMenuItem"
        Me.UsarFornatoA4DToolStripMenuItem.Size = New System.Drawing.Size(313, 22)
        Me.UsarFornatoA4DToolStripMenuItem.Text = "Usar Fornato A4D"
        '
        'tsbConfiguracoes
        '
        Me.tsbConfiguracoes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbConfiguracoes.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfiguraçãoToolStripMenuItem2, Me.ConfExportarArquivoParaOSToolStripMenuItem1, Me.ToolStripSeparator21})
        Me.tsbConfiguracoes.Image = Global.SwLynx_4._1.My.Resources.Resources.sinco_configurações
        Me.tsbConfiguracoes.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbConfiguracoes.Name = "tsbConfiguracoes"
        Me.tsbConfiguracoes.Size = New System.Drawing.Size(43, 48)
        Me.tsbConfiguracoes.Text = "Configurações"
        Me.tsbConfiguracoes.ToolTipText = "Opções de Configurações do sistema "
        '
        'ConfiguraçãoToolStripMenuItem2
        '
        Me.ConfiguraçãoToolStripMenuItem2.Name = "ConfiguraçãoToolStripMenuItem2"
        Me.ConfiguraçãoToolStripMenuItem2.Size = New System.Drawing.Size(397, 22)
        Me.ConfiguraçãoToolStripMenuItem2.Text = "Configuração de Parametros Banco de Dados & Pastas Padrões"
        '
        'ConfExportarArquivoParaOSToolStripMenuItem1
        '
        Me.ConfExportarArquivoParaOSToolStripMenuItem1.Name = "ConfExportarArquivoParaOSToolStripMenuItem1"
        Me.ConfExportarArquivoParaOSToolStripMenuItem1.Size = New System.Drawing.Size(397, 22)
        Me.ConfExportarArquivoParaOSToolStripMenuItem1.Text = "Configurar Parametros do Sistema"
        '
        'ToolStripSeparator21
        '
        Me.ToolStripSeparator21.Name = "ToolStripSeparator21"
        Me.ToolStripSeparator21.Size = New System.Drawing.Size(394, 6)
        '
        'TSBAssociarMaterial
        '
        Me.TSBAssociarMaterial.CheckOnClick = True
        Me.TSBAssociarMaterial.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBAssociarMaterial.Image = Global.SwLynx_4._1.My.Resources.Resources.sinco_Diversos
        Me.TSBAssociarMaterial.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBAssociarMaterial.Name = "TSBAssociarMaterial"
        Me.TSBAssociarMaterial.Size = New System.Drawing.Size(34, 48)
        Me.TSBAssociarMaterial.Text = "material"
        Me.TSBAssociarMaterial.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TSBAssociarMaterial.ToolTipText = "Abre o formulario para composição da peças corrente/Associação de material"
        '
        'tsbInserirNaOS
        '
        Me.tsbInserirNaOS.CheckOnClick = True
        Me.tsbInserirNaOS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbInserirNaOS.Image = Global.SwLynx_4._1.My.Resources.Resources.sinco_CheckList
        Me.tsbInserirNaOS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbInserirNaOS.Name = "tsbInserirNaOS"
        Me.tsbInserirNaOS.Size = New System.Drawing.Size(34, 48)
        Me.tsbInserirNaOS.Text = "Inserir na OS"
        Me.tsbInserirNaOS.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbInserirNaOS.ToolTipText = "Inseri na Ordem de Serviço Selecionada a peças corrente, a quantidade deve ser in" &
    "formada."
        '
        'ToolStripSeparator23
        '
        Me.ToolStripSeparator23.Name = "ToolStripSeparator23"
        Me.ToolStripSeparator23.Size = New System.Drawing.Size(6, 51)
        '
        'ToolStripSeparator24
        '
        Me.ToolStripSeparator24.Name = "ToolStripSeparator24"
        Me.ToolStripSeparator24.Size = New System.Drawing.Size(6, 51)
        '
        'ToolStripSeparator25
        '
        Me.ToolStripSeparator25.Name = "ToolStripSeparator25"
        Me.ToolStripSeparator25.Size = New System.Drawing.Size(6, 51)
        '
        'TsbInspecaoQualidade
        '
        Me.TsbInspecaoQualidade.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TsbInspecaoQualidade.Image = Global.SwLynx_4._1.My.Resources.Resources.sinco_Medição
        Me.TsbInspecaoQualidade.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsbInspecaoQualidade.Name = "TsbInspecaoQualidade"
        Me.TsbInspecaoQualidade.Size = New System.Drawing.Size(34, 48)
        Me.TsbInspecaoQualidade.Text = "Ficha para Controle Dimencional"
        Me.TsbInspecaoQualidade.ToolTipText = "Ficha para Controle Dimensional: " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Informe as principais medidas para controle di" &
    "mensional da peças."
        '
        'ToolStripSeparator39
        '
        Me.ToolStripSeparator39.Name = "ToolStripSeparator39"
        Me.ToolStripSeparator39.Size = New System.Drawing.Size(6, 51)
        '
        'ToolStripButton9
        '
        Me.ToolStripButton9.CheckOnClick = True
        Me.ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton9.Image = Global.SwLynx_4._1.My.Resources.Resources.Atualizar___Copia
        Me.ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton9.Name = "ToolStripButton9"
        Me.ToolStripButton9.Size = New System.Drawing.Size(34, 48)
        Me.ToolStripButton9.Text = "Atualizar os dados que são recebidos do SINCO"
        Me.ToolStripButton9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton9.ToolTipText = "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Caso um novo cadastro de Acabamento, Tipo Desenho, Projeto e/ou Ta" &
    "g seja inserido e não " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "apareça no ComboBox, basta clicar aqui para atualizar os" &
    " dados."
        '
        'txtNomeArquivo
        '
        Me.txtNomeArquivo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtNomeArquivo.Name = "txtNomeArquivo"
        Me.txtNomeArquivo.Size = New System.Drawing.Size(108, 51)
        Me.txtNomeArquivo.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtNomeArquivo.ToolTipText = "Nome do arquivo corrente"
        '
        'tslVersaoSistema
        '
        Me.tslVersaoSistema.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tslVersaoSistema.Name = "tslVersaoSistema"
        Me.tslVersaoSistema.Size = New System.Drawing.Size(22, 15)
        Me.tslVersaoSistema.Text = "---"
        '
        'ProgresseBarPrincipal
        '
        Me.ProgresseBarPrincipal.Name = "ProgresseBarPrincipal"
        Me.ProgresseBarPrincipal.Size = New System.Drawing.Size(75, 49)
        '
        'lblResumo
        '
        Me.lblResumo.Name = "lblResumo"
        Me.lblResumo.Size = New System.Drawing.Size(87, 15)
        Me.lblResumo.Text = "ToolStripLabel1"
        '
        'tpgBom
        '
        Me.tpgBom.Controls.Add(Me.Panel2)
        Me.tpgBom.Controls.Add(Me.BindingNavigator2)
        Me.tpgBom.Location = New System.Drawing.Point(4, 25)
        Me.tpgBom.Name = "tpgBom"
        Me.tpgBom.Padding = New System.Windows.Forms.Padding(3)
        Me.tpgBom.Size = New System.Drawing.Size(648, 747)
        Me.tpgBom.TabIndex = 1
        Me.tpgBom.Text = "BOM Lista de material"
        Me.tpgBom.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.FlowLayoutPanel1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 47)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(642, 697)
        Me.Panel2.TabIndex = 31
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.dgvDataGridBOM)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 49)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(642, 648)
        Me.Panel5.TabIndex = 2
        '
        'dgvDataGridBOM
        '
        Me.dgvDataGridBOM.AllowUserToAddRows = False
        Me.dgvDataGridBOM.AllowUserToDeleteRows = False
        Me.dgvDataGridBOM.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.dgvDataGridBOM.ColumnHeadersHeight = 21
        Me.dgvDataGridBOM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvDataGridBOM.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DGVIconeLXDS, Me.DGVIconeDXF, Me.dgvIconePDF})
        Me.dgvDataGridBOM.ContextMenuStrip = Me.mnudgvDataGridBOM
        Me.dgvDataGridBOM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDataGridBOM.Location = New System.Drawing.Point(0, 0)
        Me.dgvDataGridBOM.MultiSelect = False
        Me.dgvDataGridBOM.Name = "dgvDataGridBOM"
        Me.dgvDataGridBOM.ReadOnly = True
        Me.dgvDataGridBOM.RowHeadersWidth = 51
        Me.dgvDataGridBOM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDataGridBOM.Size = New System.Drawing.Size(642, 648)
        Me.dgvDataGridBOM.TabIndex = 0
        '
        'DGVIconeLXDS
        '
        Me.DGVIconeLXDS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DGVIconeLXDS.Frozen = True
        Me.DGVIconeLXDS.HeaderText = "LXDS"
        Me.DGVIconeLXDS.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DGVIconeLXDS.MinimumWidth = 6
        Me.DGVIconeLXDS.Name = "DGVIconeLXDS"
        Me.DGVIconeLXDS.ReadOnly = True
        Me.DGVIconeLXDS.Visible = False
        Me.DGVIconeLXDS.Width = 50
        '
        'DGVIconeDXF
        '
        Me.DGVIconeDXF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DGVIconeDXF.Frozen = True
        Me.DGVIconeDXF.HeaderText = "DXF"
        Me.DGVIconeDXF.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DGVIconeDXF.MinimumWidth = 6
        Me.DGVIconeDXF.Name = "DGVIconeDXF"
        Me.DGVIconeDXF.ReadOnly = True
        Me.DGVIconeDXF.Width = 50
        '
        'dgvIconePDF
        '
        Me.dgvIconePDF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.dgvIconePDF.Frozen = True
        Me.dgvIconePDF.HeaderText = "PDF"
        Me.dgvIconePDF.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvIconePDF.MinimumWidth = 6
        Me.dgvIconePDF.Name = "dgvIconePDF"
        Me.dgvIconePDF.ReadOnly = True
        Me.dgvIconePDF.Width = 50
        '
        'mnudgvDataGridBOM
        '
        Me.mnudgvDataGridBOM.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnudgvDataGridBOM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator34, Me.ToolStripMenuItem3, Me.ToolStripSeparator33, Me.ToolStripMenuItem2, Me.ToolStripSeparator35, Me.AbrirLXDSDaLinhaSelecionadaToolStripMenuItem, Me.ToolStripSeparator36, Me.FaçaUmaAnalizeTecnicaToolStripMenuItem, Me.ToolStripSeparator31, Me.BuscarListaDeMaterialNoFAPToolStripMenuItem, Me.LocarCadasttroDeDesenhoPeloNumweroDoDocumentoToolStripMenuItem, Me.BuscarListaDePeçasAvulçasToolStripMenuItem})
        Me.mnudgvDataGridBOM.Name = "mnudgvDataGridBOM"
        Me.mnudgvDataGridBOM.Size = New System.Drawing.Size(397, 216)
        '
        'ToolStripSeparator34
        '
        Me.ToolStripSeparator34.Name = "ToolStripSeparator34"
        Me.ToolStripSeparator34.Size = New System.Drawing.Size(393, 6)
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Image = Global.SwLynx_4._1.My.Resources.Resources.pdf
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(396, 26)
        Me.ToolStripMenuItem3.Text = "Abrir PDF da Linha Selecionada"
        '
        'ToolStripSeparator33
        '
        Me.ToolStripSeparator33.Name = "ToolStripSeparator33"
        Me.ToolStripSeparator33.Size = New System.Drawing.Size(393, 6)
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Image = Global.SwLynx_4._1.My.Resources.Resources.arquivo_dxf
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(396, 26)
        Me.ToolStripMenuItem2.Text = "Abrir DXF da Linha Selecionada"
        '
        'ToolStripSeparator35
        '
        Me.ToolStripSeparator35.Name = "ToolStripSeparator35"
        Me.ToolStripSeparator35.Size = New System.Drawing.Size(393, 6)
        '
        'AbrirLXDSDaLinhaSelecionadaToolStripMenuItem
        '
        Me.AbrirLXDSDaLinhaSelecionadaToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.CYPCUT
        Me.AbrirLXDSDaLinhaSelecionadaToolStripMenuItem.Name = "AbrirLXDSDaLinhaSelecionadaToolStripMenuItem"
        Me.AbrirLXDSDaLinhaSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(396, 26)
        Me.AbrirLXDSDaLinhaSelecionadaToolStripMenuItem.Text = "Abrir LXDS da Linha Selecionada"
        '
        'ToolStripSeparator36
        '
        Me.ToolStripSeparator36.Name = "ToolStripSeparator36"
        Me.ToolStripSeparator36.Size = New System.Drawing.Size(393, 6)
        '
        'FaçaUmaAnalizeTecnicaToolStripMenuItem
        '
        Me.FaçaUmaAnalizeTecnicaToolStripMenuItem.Name = "FaçaUmaAnalizeTecnicaToolStripMenuItem"
        Me.FaçaUmaAnalizeTecnicaToolStripMenuItem.Size = New System.Drawing.Size(396, 26)
        Me.FaçaUmaAnalizeTecnicaToolStripMenuItem.Text = "Faça uma Analize Tecnica"
        '
        'ToolStripSeparator31
        '
        Me.ToolStripSeparator31.Name = "ToolStripSeparator31"
        Me.ToolStripSeparator31.Size = New System.Drawing.Size(393, 6)
        '
        'BuscarListaDeMaterialNoFAPToolStripMenuItem
        '
        Me.BuscarListaDeMaterialNoFAPToolStripMenuItem.Enabled = False
        Me.BuscarListaDeMaterialNoFAPToolStripMenuItem.Name = "BuscarListaDeMaterialNoFAPToolStripMenuItem"
        Me.BuscarListaDeMaterialNoFAPToolStripMenuItem.Size = New System.Drawing.Size(396, 26)
        Me.BuscarListaDeMaterialNoFAPToolStripMenuItem.Text = "Buscar Lista de Material no FAP"
        '
        'LocarCadasttroDeDesenhoPeloNumweroDoDocumentoToolStripMenuItem
        '
        Me.LocarCadasttroDeDesenhoPeloNumweroDoDocumentoToolStripMenuItem.Name = "LocarCadasttroDeDesenhoPeloNumweroDoDocumentoToolStripMenuItem"
        Me.LocarCadasttroDeDesenhoPeloNumweroDoDocumentoToolStripMenuItem.Size = New System.Drawing.Size(396, 26)
        Me.LocarCadasttroDeDesenhoPeloNumweroDoDocumentoToolStripMenuItem.Text = "Identificar cadastro de desenho pelo número do documento"
        '
        'BuscarListaDePeçasAvulçasToolStripMenuItem
        '
        Me.BuscarListaDePeçasAvulçasToolStripMenuItem.Enabled = False
        Me.BuscarListaDePeçasAvulçasToolStripMenuItem.Name = "BuscarListaDePeçasAvulçasToolStripMenuItem"
        Me.BuscarListaDePeçasAvulçasToolStripMenuItem.Size = New System.Drawing.Size(396, 26)
        Me.BuscarListaDePeçasAvulçasToolStripMenuItem.Text = "Buscar Lista de Peças Avulças"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.chkTrocarFormato)
        Me.FlowLayoutPanel1.Controls.Add(Me.choBloqueaArquivoExistente)
        Me.FlowLayoutPanel1.Controls.Add(Me.chkConverterPDF)
        Me.FlowLayoutPanel1.Controls.Add(Me.chkConverterDXF)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(642, 49)
        Me.FlowLayoutPanel1.TabIndex = 1
        '
        'chkTrocarFormato
        '
        Me.chkTrocarFormato.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkTrocarFormato.BackColor = System.Drawing.Color.Transparent
        Me.chkTrocarFormato.Image = Global.SwLynx_4._1.My.Resources.Resources.papel_A3
        Me.chkTrocarFormato.Location = New System.Drawing.Point(2, 2)
        Me.chkTrocarFormato.Margin = New System.Windows.Forms.Padding(2)
        Me.chkTrocarFormato.Name = "chkTrocarFormato"
        Me.chkTrocarFormato.Size = New System.Drawing.Size(146, 35)
        Me.chkTrocarFormato.TabIndex = 33
        Me.chkTrocarFormato.Text = "Trocar Formato do Detalhamento"
        Me.chkTrocarFormato.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTipAjuda.SetToolTip(Me.chkTrocarFormato, "Faz a Troca do Formato Corrente pelo o Formato Padrão atual. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & """Esta opção na ent" &
        "ra na condição de Não atualizar Existentes.""")
        Me.chkTrocarFormato.UseVisualStyleBackColor = False
        '
        'choBloqueaArquivoExistente
        '
        Me.choBloqueaArquivoExistente.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.choBloqueaArquivoExistente.BackColor = System.Drawing.Color.Transparent
        Me.choBloqueaArquivoExistente.Image = Global.SwLynx_4._1.My.Resources.Resources.bloqueado
        Me.choBloqueaArquivoExistente.Location = New System.Drawing.Point(152, 2)
        Me.choBloqueaArquivoExistente.Margin = New System.Windows.Forms.Padding(2)
        Me.choBloqueaArquivoExistente.Name = "choBloqueaArquivoExistente"
        Me.choBloqueaArquivoExistente.Size = New System.Drawing.Size(146, 35)
        Me.choBloqueaArquivoExistente.TabIndex = 32
        Me.choBloqueaArquivoExistente.Text = "Não Atualizar Existentes"
        Me.choBloqueaArquivoExistente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTipAjuda.SetToolTip(Me.choBloqueaArquivoExistente, "Esta opção geras nos arquivo PDF dos desenhos de detalhamento.")
        Me.choBloqueaArquivoExistente.UseVisualStyleBackColor = False
        '
        'chkConverterPDF
        '
        Me.chkConverterPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkConverterPDF.BackColor = System.Drawing.Color.Transparent
        Me.chkConverterPDF.Image = Global.SwLynx_4._1.My.Resources.Resources.pdf_16_161
        Me.chkConverterPDF.Location = New System.Drawing.Point(302, 2)
        Me.chkConverterPDF.Margin = New System.Windows.Forms.Padding(2)
        Me.chkConverterPDF.Name = "chkConverterPDF"
        Me.chkConverterPDF.Size = New System.Drawing.Size(146, 35)
        Me.chkConverterPDF.TabIndex = 9
        Me.chkConverterPDF.Text = "Converter PDF"
        Me.chkConverterPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTipAjuda.SetToolTip(Me.chkConverterPDF, "Esta opção geras nos arquivo PDF dos desenhos de detalhamento.")
        Me.chkConverterPDF.UseVisualStyleBackColor = False
        '
        'chkConverterDXF
        '
        Me.chkConverterDXF.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkConverterDXF.BackColor = System.Drawing.Color.Transparent
        Me.chkConverterDXF.Image = Global.SwLynx_4._1.My.Resources.Resources.dxf_16_16
        Me.chkConverterDXF.Location = New System.Drawing.Point(452, 2)
        Me.chkConverterDXF.Margin = New System.Windows.Forms.Padding(2)
        Me.chkConverterDXF.Name = "chkConverterDXF"
        Me.chkConverterDXF.Size = New System.Drawing.Size(146, 35)
        Me.chkConverterDXF.TabIndex = 8
        Me.chkConverterDXF.Text = "Converter DXF"
        Me.chkConverterDXF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTipAjuda.SetToolTip(Me.chkConverterDXF, "Marque esta opção para gerar arquivos DXF, esta opção paga  os arquivo XLSD e DFT" &
        ".")
        Me.chkConverterDXF.UseVisualStyleBackColor = False
        '
        'BindingNavigator2
        '
        Me.BindingNavigator2.AddNewItem = Nothing
        Me.BindingNavigator2.AutoSize = False
        Me.BindingNavigator2.CountItem = Nothing
        Me.BindingNavigator2.DeleteItem = Nothing
        Me.BindingNavigator2.ImageScalingSize = New System.Drawing.Size(30, 30)
        Me.BindingNavigator2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BindingNavigator2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton11, Me.ToolStripButton4, Me.ToolStripSeparator26, Me.ToolStripButton5, Me.ToolStripSeparator28, Me.tsbProcessaoListaMaterialBOM, Me.ToolStripSeparator27, Me.ToolStripButton7, Me.ToolStripSeparator37, Me.TsbAtualizarBOM, Me.tsbTrocarFormato, Me.ToolStripSeparator38, Me.ProgressBarListaSW, Me.lblOrdemServicoAtiva})
        Me.BindingNavigator2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.BindingNavigator2.Location = New System.Drawing.Point(3, 3)
        Me.BindingNavigator2.MoveFirstItem = Nothing
        Me.BindingNavigator2.MoveLastItem = Nothing
        Me.BindingNavigator2.MoveNextItem = Nothing
        Me.BindingNavigator2.MovePreviousItem = Nothing
        Me.BindingNavigator2.Name = "BindingNavigator2"
        Me.BindingNavigator2.PositionItem = Nothing
        Me.BindingNavigator2.Size = New System.Drawing.Size(642, 44)
        Me.BindingNavigator2.Stretch = True
        Me.BindingNavigator2.TabIndex = 30
        Me.BindingNavigator2.Text = "BindingNavigator2"
        '
        'ToolStripButton11
        '
        Me.ToolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton11.Image = CType(resources.GetObject("ToolStripButton11.Image"), System.Drawing.Image)
        Me.ToolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton11.Name = "ToolStripButton11"
        Me.ToolStripButton11.Size = New System.Drawing.Size(34, 34)
        Me.ToolStripButton11.Text = "ToolStripButton10"
        Me.ToolStripButton11.ToolTipText = "Video de Treinamento da Tela de Dados Principais"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.CheckOnClick = True
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Image = Global.SwLynx_4._1.My.Resources.Resources.Sinco_Limpar_Campos
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(34, 34)
        Me.ToolStripButton4.Text = "Limpar a BOM"
        Me.ToolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton4.ToolTipText = "Limpa o Grid - Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Limpa o Grid da Lista de Materiais antes da leitura" &
    " da próxima lista. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "No entanto, é possível carregar várias listas para a mesma " &
    "Ordem de Serviço (OS)."
        '
        'ToolStripSeparator26
        '
        Me.ToolStripSeparator26.Name = "ToolStripSeparator26"
        Me.ToolStripSeparator26.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.CheckOnClick = True
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Image = Global.SwLynx_4._1.My.Resources.Resources.sinco_Diversos
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(34, 34)
        Me.ToolStripButton5.Text = "Ler BOM"
        Me.ToolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton5.ToolTipText = resources.GetString("ToolStripButton5.ToolTipText")
        '
        'ToolStripSeparator28
        '
        Me.ToolStripSeparator28.Name = "ToolStripSeparator28"
        Me.ToolStripSeparator28.Size = New System.Drawing.Size(6, 23)
        '
        'tsbProcessaoListaMaterialBOM
        '
        Me.tsbProcessaoListaMaterialBOM.CheckOnClick = True
        Me.tsbProcessaoListaMaterialBOM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbProcessaoListaMaterialBOM.Image = Global.SwLynx_4._1.My.Resources.Resources.sinco_Setores
        Me.tsbProcessaoListaMaterialBOM.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbProcessaoListaMaterialBOM.Name = "tsbProcessaoListaMaterialBOM"
        Me.tsbProcessaoListaMaterialBOM.Size = New System.Drawing.Size(34, 34)
        Me.tsbProcessaoListaMaterialBOM.Text = "Processar a Lista de material"
        Me.tsbProcessaoListaMaterialBOM.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbProcessaoListaMaterialBOM.ToolTipText = resources.GetString("tsbProcessaoListaMaterialBOM.ToolTipText")
        '
        'ToolStripSeparator27
        '
        Me.ToolStripSeparator27.Name = "ToolStripSeparator27"
        Me.ToolStripSeparator27.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripButton7
        '
        Me.ToolStripButton7.CheckOnClick = True
        Me.ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton7.Image = Global.SwLynx_4._1.My.Resources.Resources.sinco_CheckList
        Me.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton7.Name = "ToolStripButton7"
        Me.ToolStripButton7.Size = New System.Drawing.Size(34, 34)
        Me.ToolStripButton7.Text = "Inserir Lista de material na Ordem de Serviço"
        Me.ToolStripButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton7.ToolTipText = "Inserir na Ordem de Serviço - Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Insira na Ordem de Serviço (OS) a li" &
    "sta de materiais carregada no Grid. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Neste ponto, é necessário informar o fator" &
    " multiplicador."
        '
        'ToolStripSeparator37
        '
        Me.ToolStripSeparator37.Name = "ToolStripSeparator37"
        Me.ToolStripSeparator37.Size = New System.Drawing.Size(6, 23)
        '
        'TsbAtualizarBOM
        '
        Me.TsbAtualizarBOM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TsbAtualizarBOM.Image = Global.SwLynx_4._1.My.Resources.Resources.Atualizar___Copia
        Me.TsbAtualizarBOM.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsbAtualizarBOM.Name = "TsbAtualizarBOM"
        Me.TsbAtualizarBOM.Size = New System.Drawing.Size(34, 34)
        Me.TsbAtualizarBOM.Text = "Atualizar"
        Me.TsbAtualizarBOM.ToolTipText = "Clique aqui para atualizar a verificação dos tipos de arquivos já existentes"
        '
        'tsbTrocarFormato
        '
        Me.tsbTrocarFormato.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbTrocarFormato.Image = Global.SwLynx_4._1.My.Resources.Resources.lista
        Me.tsbTrocarFormato.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbTrocarFormato.Name = "tsbTrocarFormato"
        Me.tsbTrocarFormato.Size = New System.Drawing.Size(34, 34)
        Me.tsbTrocarFormato.Text = "Tocar Formato pelo Padrão"
        Me.tsbTrocarFormato.Visible = False
        '
        'ToolStripSeparator38
        '
        Me.ToolStripSeparator38.Name = "ToolStripSeparator38"
        Me.ToolStripSeparator38.Size = New System.Drawing.Size(6, 23)
        '
        'ProgressBarListaSW
        '
        Me.ProgressBarListaSW.AutoSize = False
        Me.ProgressBarListaSW.Name = "ProgressBarListaSW"
        Me.ProgressBarListaSW.Size = New System.Drawing.Size(100, 20)
        Me.ProgressBarListaSW.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'lblOrdemServicoAtiva
        '
        Me.lblOrdemServicoAtiva.Name = "lblOrdemServicoAtiva"
        Me.lblOrdemServicoAtiva.Size = New System.Drawing.Size(22, 15)
        Me.lblOrdemServicoAtiva.Text = "---"
        '
        'tpgOrdemServico
        '
        Me.tpgOrdemServico.Controls.Add(Me.Panel4)
        Me.tpgOrdemServico.Controls.Add(Me.Panel3)
        Me.tpgOrdemServico.Controls.Add(Me.BindingNavigator1)
        Me.tpgOrdemServico.Location = New System.Drawing.Point(4, 25)
        Me.tpgOrdemServico.Margin = New System.Windows.Forms.Padding(2)
        Me.tpgOrdemServico.Name = "tpgOrdemServico"
        Me.tpgOrdemServico.Size = New System.Drawing.Size(648, 747)
        Me.tpgOrdemServico.TabIndex = 3
        Me.tpgOrdemServico.Text = "Ordem de Serviço"
        Me.tpgOrdemServico.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.TabControlOS)
        Me.Panel4.Controls.Add(Me.txtPesqNumeroDesenho)
        Me.Panel4.Controls.Add(Me.Label27)
        Me.Panel4.Controls.Add(Me.cboOpcoesAcabamento)
        Me.Panel4.Controls.Add(Me.Label26)
        Me.Panel4.Controls.Add(Me.txtPesqAcabamentoDesenho)
        Me.Panel4.Controls.Add(Me.btnAplicarAcabamento)
        Me.Panel4.Controls.Add(Me.Label28)
        Me.Panel4.Controls.Add(Me.Label29)
        Me.Panel4.Controls.Add(Me.txtPesqTipoDesenho)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 403)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(648, 344)
        Me.Panel4.TabIndex = 38
        '
        'TabControlOS
        '
        Me.TabControlOS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControlOS.Controls.Add(Me.TabPage2)
        Me.TabControlOS.Controls.Add(Me.TabPage3)
        Me.TabControlOS.Location = New System.Drawing.Point(3, 88)
        Me.TabControlOS.Name = "TabControlOS"
        Me.TabControlOS.SelectedIndex = 0
        Me.TabControlOS.Size = New System.Drawing.Size(642, 253)
        Me.TabControlOS.TabIndex = 24
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DGVListaMaterialSW)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(634, 227)
        Me.TabPage2.TabIndex = 0
        Me.TabPage2.Text = "Desenhos"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DGVListaMaterialSW
        '
        Me.DGVListaMaterialSW.AllowUserToAddRows = False
        Me.DGVListaMaterialSW.AllowUserToDeleteRows = False
        Me.DGVListaMaterialSW.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.DGVListaMaterialSW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVListaMaterialSW.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvSelecao, Me.dgvIconeItemOS, Me.dgvDXF, Me.dgvPDF})
        Me.DGVListaMaterialSW.ContextMenuStrip = Me.mnuDGVListaMaterialSW
        Me.DGVListaMaterialSW.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVListaMaterialSW.Location = New System.Drawing.Point(3, 3)
        Me.DGVListaMaterialSW.Margin = New System.Windows.Forms.Padding(2)
        Me.DGVListaMaterialSW.Name = "DGVListaMaterialSW"
        Me.DGVListaMaterialSW.ReadOnly = True
        Me.DGVListaMaterialSW.RowHeadersWidth = 51
        Me.DGVListaMaterialSW.RowTemplate.Height = 24
        Me.DGVListaMaterialSW.Size = New System.Drawing.Size(628, 221)
        Me.DGVListaMaterialSW.TabIndex = 9
        '
        'dgvSelecao
        '
        Me.dgvSelecao.Frozen = True
        Me.dgvSelecao.HeaderText = "dgvSelecao"
        Me.dgvSelecao.MinimumWidth = 6
        Me.dgvSelecao.Name = "dgvSelecao"
        Me.dgvSelecao.ReadOnly = True
        Me.dgvSelecao.Width = 6
        '
        'dgvIconeItemOS
        '
        Me.dgvIconeItemOS.Frozen = True
        Me.dgvIconeItemOS.HeaderText = "dgvIconeItemOS"
        Me.dgvIconeItemOS.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvIconeItemOS.MinimumWidth = 6
        Me.dgvIconeItemOS.Name = "dgvIconeItemOS"
        Me.dgvIconeItemOS.ReadOnly = True
        Me.dgvIconeItemOS.Width = 6
        '
        'dgvDXF
        '
        Me.dgvDXF.HeaderText = "DXF"
        Me.dgvDXF.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvDXF.MinimumWidth = 6
        Me.dgvDXF.Name = "dgvDXF"
        Me.dgvDXF.ReadOnly = True
        Me.dgvDXF.Width = 6
        '
        'dgvPDF
        '
        Me.dgvPDF.HeaderText = "PDF"
        Me.dgvPDF.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvPDF.MinimumWidth = 6
        Me.dgvPDF.Name = "dgvPDF"
        Me.dgvPDF.ReadOnly = True
        Me.dgvPDF.Width = 6
        '
        'mnuDGVListaMaterialSW
        '
        Me.mnuDGVListaMaterialSW.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuDGVListaMaterialSW.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator2, Me.MarcarTodosToolStripMenuItem, Me.DesmarcarTodosToolStripMenuItem, Me.InverterSeleçãoToolStripMenuItem, Me.ToolStripSeparator6, Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem, Me.ToolStripSeparator1, Me.ToolStripSeparator15, Me.AbrirDXFDaLinhaSelecionadaToolStripMenuItem, Me.AbrirDWRDaLinhaSelecionadaToolStripMenuItem, Me.ToolStripSeparator3, Me.ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem, Me.ToolStripSeparator5, Me.MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem, Me.DesmarcarComoConjuntoPrincipalToolStripMenuItem, Me.ToolStripSeparator7, Me.AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem, Me.ToolStripSeparator8, Me.ImprimirDesenhoPDFSelecionadoToolStripMenuItem, Me.ToolStripSeparator18, Me.GerarPDFDasLinhasSelecionadasToolStripMenuItem, Me.ToolStripSeparator19, Me.GerarDXFDasLinhasSelecionadasToolStripMenuItem})
        Me.mnuDGVListaMaterialSW.Name = "ContextMenuStrip1"
        Me.mnuDGVListaMaterialSW.Size = New System.Drawing.Size(407, 402)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(403, 6)
        '
        'MarcarTodosToolStripMenuItem
        '
        Me.MarcarTodosToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.marcado
        Me.MarcarTodosToolStripMenuItem.Name = "MarcarTodosToolStripMenuItem"
        Me.MarcarTodosToolStripMenuItem.Size = New System.Drawing.Size(406, 26)
        Me.MarcarTodosToolStripMenuItem.Text = "Marcar Todos"
        '
        'DesmarcarTodosToolStripMenuItem
        '
        Me.DesmarcarTodosToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.desmarcado
        Me.DesmarcarTodosToolStripMenuItem.Name = "DesmarcarTodosToolStripMenuItem"
        Me.DesmarcarTodosToolStripMenuItem.Size = New System.Drawing.Size(406, 26)
        Me.DesmarcarTodosToolStripMenuItem.Text = "Desmarcar Todos"
        '
        'InverterSeleçãoToolStripMenuItem
        '
        Me.InverterSeleçãoToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.alterar
        Me.InverterSeleçãoToolStripMenuItem.Name = "InverterSeleçãoToolStripMenuItem"
        Me.InverterSeleçãoToolStripMenuItem.Size = New System.Drawing.Size(406, 26)
        Me.InverterSeleçãoToolStripMenuItem.Text = "Inverter Seleção"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(403, 6)
        '
        'AbrirPDFDaLinhaSelecionadaToolStripMenuItem
        '
        Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.pdf
        Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem.Name = "AbrirPDFDaLinhaSelecionadaToolStripMenuItem"
        Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(406, 26)
        Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem.Text = "Abrir PDF da Linha Selecionada"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(403, 6)
        '
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        Me.ToolStripSeparator15.Size = New System.Drawing.Size(403, 6)
        '
        'AbrirDXFDaLinhaSelecionadaToolStripMenuItem
        '
        Me.AbrirDXFDaLinhaSelecionadaToolStripMenuItem.Name = "AbrirDXFDaLinhaSelecionadaToolStripMenuItem"
        Me.AbrirDXFDaLinhaSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(406, 26)
        Me.AbrirDXFDaLinhaSelecionadaToolStripMenuItem.Text = "Abrir DXF da Linha Selecionada"
        '
        'AbrirDWRDaLinhaSelecionadaToolStripMenuItem
        '
        Me.AbrirDWRDaLinhaSelecionadaToolStripMenuItem.Name = "AbrirDWRDaLinhaSelecionadaToolStripMenuItem"
        Me.AbrirDWRDaLinhaSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(406, 26)
        Me.AbrirDWRDaLinhaSelecionadaToolStripMenuItem.Text = "Abrir DRW da Linha Selecionada"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(403, 6)
        '
        'ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem
        '
        Me.ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.sinco_Excluir
        Me.ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem.Name = "ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem"
        Me.ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(406, 26)
        Me.ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem.Text = "Excluir o Documento da Linha Selecionada"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(403, 6)
        '
        'MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem
        '
        Me.MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.IconeswPrincipal
        Me.MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem.Name = "MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem"
        Me.MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem.Size = New System.Drawing.Size(406, 26)
        Me.MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem.Text = "Marcar como Conjunto Principal da Ordem de Serviço"
        '
        'DesmarcarComoConjuntoPrincipalToolStripMenuItem
        '
        Me.DesmarcarComoConjuntoPrincipalToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.IcopneMontagemSW
        Me.DesmarcarComoConjuntoPrincipalToolStripMenuItem.Name = "DesmarcarComoConjuntoPrincipalToolStripMenuItem"
        Me.DesmarcarComoConjuntoPrincipalToolStripMenuItem.Size = New System.Drawing.Size(406, 26)
        Me.DesmarcarComoConjuntoPrincipalToolStripMenuItem.Text = "Desmarcar Como Conjunto Principal da Ordem de Serviço"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(403, 6)
        '
        'AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem
        '
        Me.AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.multiply
        Me.AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem.Name = "AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem"
        Me.AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(406, 26)
        Me.AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem.Text = "Alterar a quantidade de peças/Fabricação da linha selecionada"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(403, 6)
        '
        'ImprimirDesenhoPDFSelecionadoToolStripMenuItem
        '
        Me.ImprimirDesenhoPDFSelecionadoToolStripMenuItem.Name = "ImprimirDesenhoPDFSelecionadoToolStripMenuItem"
        Me.ImprimirDesenhoPDFSelecionadoToolStripMenuItem.Size = New System.Drawing.Size(406, 26)
        Me.ImprimirDesenhoPDFSelecionadoToolStripMenuItem.Text = "Selecione as Linhas para impressão dos arquivos em PDF"
        '
        'ToolStripSeparator18
        '
        Me.ToolStripSeparator18.Name = "ToolStripSeparator18"
        Me.ToolStripSeparator18.Size = New System.Drawing.Size(403, 6)
        '
        'GerarPDFDasLinhasSelecionadasToolStripMenuItem
        '
        Me.GerarPDFDasLinhasSelecionadasToolStripMenuItem.Name = "GerarPDFDasLinhasSelecionadasToolStripMenuItem"
        Me.GerarPDFDasLinhasSelecionadasToolStripMenuItem.Size = New System.Drawing.Size(406, 26)
        Me.GerarPDFDasLinhasSelecionadasToolStripMenuItem.Text = "Gerar PDF das Linhas Selecionadas"
        '
        'ToolStripSeparator19
        '
        Me.ToolStripSeparator19.Name = "ToolStripSeparator19"
        Me.ToolStripSeparator19.Size = New System.Drawing.Size(403, 6)
        '
        'GerarDXFDasLinhasSelecionadasToolStripMenuItem
        '
        Me.GerarDXFDasLinhasSelecionadasToolStripMenuItem.Name = "GerarDXFDasLinhasSelecionadasToolStripMenuItem"
        Me.GerarDXFDasLinhasSelecionadasToolStripMenuItem.Size = New System.Drawing.Size(406, 26)
        Me.GerarDXFDasLinhasSelecionadasToolStripMenuItem.Text = "Gerar DXF das Linhas Selecionadas"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.DGVListaMaterialSWMateriais)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(634, 227)
        Me.TabPage3.TabIndex = 1
        Me.TabPage3.Text = "Materiais"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'DGVListaMaterialSWMateriais
        '
        Me.DGVListaMaterialSWMateriais.AllowUserToAddRows = False
        Me.DGVListaMaterialSWMateriais.AllowUserToDeleteRows = False
        Me.DGVListaMaterialSWMateriais.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.DGVListaMaterialSWMateriais.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVListaMaterialSWMateriais.ContextMenuStrip = Me.mnuDGVListaMaterialSW
        Me.DGVListaMaterialSWMateriais.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVListaMaterialSWMateriais.Location = New System.Drawing.Point(3, 3)
        Me.DGVListaMaterialSWMateriais.Margin = New System.Windows.Forms.Padding(2)
        Me.DGVListaMaterialSWMateriais.Name = "DGVListaMaterialSWMateriais"
        Me.DGVListaMaterialSWMateriais.ReadOnly = True
        Me.DGVListaMaterialSWMateriais.RowHeadersWidth = 51
        Me.DGVListaMaterialSWMateriais.RowTemplate.Height = 24
        Me.DGVListaMaterialSWMateriais.Size = New System.Drawing.Size(628, 221)
        Me.DGVListaMaterialSWMateriais.TabIndex = 10
        '
        'txtPesqNumeroDesenho
        '
        Me.txtPesqNumeroDesenho.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqNumeroDesenho.Location = New System.Drawing.Point(14, 63)
        Me.txtPesqNumeroDesenho.Margin = New System.Windows.Forms.Padding(2)
        Me.txtPesqNumeroDesenho.Name = "txtPesqNumeroDesenho"
        Me.txtPesqNumeroDesenho.Size = New System.Drawing.Size(148, 20)
        Me.txtPesqNumeroDesenho.TabIndex = 19
        Me.ToolTipAjuda.SetToolTip(Me.txtPesqNumeroDesenho, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Selecione a caixa de texto desejada e informe os critérios para a " &
        "execução do filtro. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ao digitar os valores de pesquisa, o Grid será atualizado " &
        "automaticamente.")
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(14, 45)
        Me.Label27.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(93, 13)
        Me.Label27.TabIndex = 18
        Me.Label27.Text = "Numero Desenho:"
        Me.ToolTipAjuda.SetToolTip(Me.Label27, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Selecione a caixa de texto desejada e informe os critérios para a " &
        "execução do filtro. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ao digitar os valores de pesquisa, o Grid será atualizado " &
        "automaticamente.")
        '
        'cboOpcoesAcabamento
        '
        Me.cboOpcoesAcabamento.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboOpcoesAcabamento.FormattingEnabled = True
        Me.cboOpcoesAcabamento.Location = New System.Drawing.Point(81, 14)
        Me.cboOpcoesAcabamento.Margin = New System.Windows.Forms.Padding(2)
        Me.cboOpcoesAcabamento.Name = "cboOpcoesAcabamento"
        Me.cboOpcoesAcabamento.Size = New System.Drawing.Size(247, 21)
        Me.cboOpcoesAcabamento.TabIndex = 18
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(11, 18)
        Me.Label26.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(70, 13)
        Me.Label26.TabIndex = 17
        Me.Label26.Text = "Acabamento:"
        '
        'txtPesqAcabamentoDesenho
        '
        Me.txtPesqAcabamentoDesenho.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqAcabamentoDesenho.Location = New System.Drawing.Point(332, 63)
        Me.txtPesqAcabamentoDesenho.Margin = New System.Windows.Forms.Padding(2)
        Me.txtPesqAcabamentoDesenho.Name = "txtPesqAcabamentoDesenho"
        Me.txtPesqAcabamentoDesenho.Size = New System.Drawing.Size(159, 20)
        Me.txtPesqAcabamentoDesenho.TabIndex = 23
        Me.ToolTipAjuda.SetToolTip(Me.txtPesqAcabamentoDesenho, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Selecione a caixa de texto desejada e informe os critérios para a " &
        "execução do filtro. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ao digitar os valores de pesquisa, o Grid será atualizado " &
        "automaticamente.")
        '
        'btnAplicarAcabamento
        '
        Me.btnAplicarAcabamento.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAplicarAcabamento.Location = New System.Drawing.Point(334, 12)
        Me.btnAplicarAcabamento.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAplicarAcabamento.Name = "btnAplicarAcabamento"
        Me.btnAplicarAcabamento.Size = New System.Drawing.Size(104, 25)
        Me.btnAplicarAcabamento.TabIndex = 19
        Me.btnAplicarAcabamento.Text = "Aplicar"
        Me.btnAplicarAcabamento.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTipAjuda.SetToolTip(Me.btnAplicarAcabamento, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Selecione as peças nas quais deseja aplicar o Acabamento, escolha " &
        "a opção" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "desejada no ComboBox e clique no botão Aplicar.")
        Me.btnAplicarAcabamento.UseVisualStyleBackColor = True
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(167, 45)
        Me.Label28.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(77, 13)
        Me.Label28.TabIndex = 20
        Me.Label28.Text = "Tipo Desenho:"
        Me.ToolTipAjuda.SetToolTip(Me.Label28, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Selecione a caixa de texto desejada e informe os critérios para a " &
        "execução do filtro. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ao digitar os valores de pesquisa, o Grid será atualizado " &
        "automaticamente.")
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(330, 45)
        Me.Label29.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(94, 13)
        Me.Label29.TabIndex = 22
        Me.Label29.Text = "Tipo Acabamento:"
        Me.ToolTipAjuda.SetToolTip(Me.Label29, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Selecione a caixa de texto desejada e informe os critérios para a " &
        "execução do filtro. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ao digitar os valores de pesquisa, o Grid será atualizado " &
        "automaticamente.")
        '
        'txtPesqTipoDesenho
        '
        Me.txtPesqTipoDesenho.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqTipoDesenho.Location = New System.Drawing.Point(168, 63)
        Me.txtPesqTipoDesenho.Margin = New System.Windows.Forms.Padding(2)
        Me.txtPesqTipoDesenho.Name = "txtPesqTipoDesenho"
        Me.txtPesqTipoDesenho.Size = New System.Drawing.Size(159, 20)
        Me.txtPesqTipoDesenho.TabIndex = 21
        Me.ToolTipAjuda.SetToolTip(Me.txtPesqTipoDesenho, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Selecione a caixa de texto desejada e informe os critérios para a " &
        "execução do filtro. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ao digitar os valores de pesquisa, o Grid será atualizado " &
        "automaticamente.")
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lbltxtSaldoTag)
        Me.Panel3.Controls.Add(Me.lbltxtQtdeLiberada)
        Me.Panel3.Controls.Add(Me.lbltxtQtdeTag)
        Me.Panel3.Controls.Add(Me.dgvos)
        Me.Panel3.Controls.Add(Me.lblFator)
        Me.Panel3.Controls.Add(Me.Label37)
        Me.Panel3.Controls.Add(Me.chkMostraLiberadasPelaEngenharia)
        Me.Panel3.Controls.Add(Me.Label38)
        Me.Panel3.Controls.Add(Me.Label36)
        Me.Panel3.Controls.Add(Me.Label30)
        Me.Panel3.Controls.Add(Me.txtPesqCriadoPor)
        Me.Panel3.Controls.Add(Me.Label35)
        Me.Panel3.Controls.Add(Me.txtDescricao)
        Me.Panel3.Controls.Add(Me.Label25)
        Me.Panel3.Controls.Add(Me.txtCliente)
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Controls.Add(Me.cboProjeto)
        Me.Panel3.Controls.Add(Me.Label24)
        Me.Panel3.Controls.Add(Me.cboTag)
        Me.Panel3.Controls.Add(Me.txtDescricaoTag)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 51)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(648, 352)
        Me.Panel3.TabIndex = 37
        '
        'lbltxtSaldoTag
        '
        Me.lbltxtSaldoTag.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbltxtSaldoTag.BackColor = System.Drawing.Color.White
        Me.lbltxtSaldoTag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbltxtSaldoTag.Location = New System.Drawing.Point(585, 63)
        Me.lbltxtSaldoTag.Name = "lbltxtSaldoTag"
        Me.lbltxtSaldoTag.Size = New System.Drawing.Size(60, 19)
        Me.lbltxtSaldoTag.TabIndex = 39
        Me.lbltxtSaldoTag.Text = "0"
        Me.lbltxtSaldoTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbltxtQtdeLiberada
        '
        Me.lbltxtQtdeLiberada.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbltxtQtdeLiberada.BackColor = System.Drawing.Color.White
        Me.lbltxtQtdeLiberada.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbltxtQtdeLiberada.Location = New System.Drawing.Point(585, 36)
        Me.lbltxtQtdeLiberada.Name = "lbltxtQtdeLiberada"
        Me.lbltxtQtdeLiberada.Size = New System.Drawing.Size(60, 19)
        Me.lbltxtQtdeLiberada.TabIndex = 38
        Me.lbltxtQtdeLiberada.Text = "0"
        Me.lbltxtQtdeLiberada.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbltxtQtdeTag
        '
        Me.lbltxtQtdeTag.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbltxtQtdeTag.BackColor = System.Drawing.Color.White
        Me.lbltxtQtdeTag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbltxtQtdeTag.Location = New System.Drawing.Point(585, 7)
        Me.lbltxtQtdeTag.Name = "lbltxtQtdeTag"
        Me.lbltxtQtdeTag.Size = New System.Drawing.Size(60, 19)
        Me.lbltxtQtdeTag.TabIndex = 37
        Me.lbltxtQtdeTag.Text = "0"
        Me.lbltxtQtdeTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvos
        '
        Me.dgvos.AllowUserToAddRows = False
        Me.dgvos.AllowUserToDeleteRows = False
        Me.dgvos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.dgvos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvStatus})
        Me.dgvos.ContextMenuStrip = Me.mnudgvos
        Me.dgvos.Location = New System.Drawing.Point(4, 172)
        Me.dgvos.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvos.Name = "dgvos"
        Me.dgvos.ReadOnly = True
        Me.dgvos.RowHeadersWidth = 51
        Me.dgvos.RowTemplate.Height = 24
        Me.dgvos.Size = New System.Drawing.Size(642, 178)
        Me.dgvos.TabIndex = 7
        '
        'dgvStatus
        '
        Me.dgvStatus.Frozen = True
        Me.dgvStatus.HeaderText = "Status"
        Me.dgvStatus.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvStatus.MinimumWidth = 6
        Me.dgvStatus.Name = "dgvStatus"
        Me.dgvStatus.ReadOnly = True
        Me.dgvStatus.Width = 6
        '
        'mnudgvos
        '
        Me.mnudgvos.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnudgvos.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AbrirPastaDaOrdemDeServiçoToolStripMenuItem, Me.ToolStripSeparator4, Me.LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem, Me.CancelarLiberaçãoDaOSToolStripMenuItem, Me.ToolStripSeparator9, Me.AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem, Me.ToolStripSeparator10, Me.AlterarOFatorMultipçlicadorDaOSToolStripMenuItem, Me.ToolStripSeparator14, Me.GeralExcelDaOSToolStripMenuItem, Me.ToolStripSeparator11, Me.LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem, Me.ToolStripSeparator17, Me.CancelarAFabricaçãoDaOSToolStripMenuItem, Me.ToolStripSeparator12, Me.GerarArquivoEmDXFToolStripMenuItem, Me.ToolStripSeparator13, Me.GerarArquivoEmPDFToolStripMenuItem, Me.ToolStripSeparator16, Me.ToolStripSeparator20, Me.CriarUmCopiaDaOSSelecionadaToolStripMenuItem, Me.TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem, Me.ToolStripSeparator30, Me.InserirMaterialPeloFAPToolStripMenuItem, Me.InserirNumeroOPToolStripMenuItem})
        Me.mnudgvos.Name = "mnudgvos"
        Me.mnudgvos.Size = New System.Drawing.Size(443, 434)
        '
        'AbrirPastaDaOrdemDeServiçoToolStripMenuItem
        '
        Me.AbrirPastaDaOrdemDeServiçoToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.pasta
        Me.AbrirPastaDaOrdemDeServiçoToolStripMenuItem.Name = "AbrirPastaDaOrdemDeServiçoToolStripMenuItem"
        Me.AbrirPastaDaOrdemDeServiçoToolStripMenuItem.Size = New System.Drawing.Size(442, 26)
        Me.AbrirPastaDaOrdemDeServiçoToolStripMenuItem.Text = "Abrir Pasta da Ordem de Serviço"
        Me.AbrirPastaDaOrdemDeServiçoToolStripMenuItem.ToolTipText = "Abre a Pasta da OS Selecionada!"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(439, 6)
        '
        'LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem
        '
        Me.LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.verificado
        Me.LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem.Name = "LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem"
        Me.LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem.Size = New System.Drawing.Size(442, 26)
        Me.LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem.Text = "Liberar Ordem de Serviço para Produção"
        Me.LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem.ToolTipText = "Aqui você libera a OS para o processo de fabricação pelo SINCO e inportas os arqu" &
    "ivo em PDF's e DXF's para as pastas da OS."
        '
        'CancelarLiberaçãoDaOSToolStripMenuItem
        '
        Me.CancelarLiberaçãoDaOSToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.cancelar
        Me.CancelarLiberaçãoDaOSToolStripMenuItem.Name = "CancelarLiberaçãoDaOSToolStripMenuItem"
        Me.CancelarLiberaçãoDaOSToolStripMenuItem.Size = New System.Drawing.Size(442, 26)
        Me.CancelarLiberaçãoDaOSToolStripMenuItem.Text = "Cancelar Liberação da Ordem de Serviço"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(439, 6)
        '
        'AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem
        '
        Me.AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.atualizar
        Me.AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem.Name = "AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem"
        Me.AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem.Size = New System.Drawing.Size(442, 26)
        Me.AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem.Text = "Atualizar PDF's, DXF's/LXDS's e DTF's  na pasta da OS"
        Me.AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem.ToolTipText = "Atenção: Esta função exclui todos os arquivos da OS e os atualizas com os documen" &
    "to da lista de materiais da OS."
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(439, 6)
        '
        'AlterarOFatorMultipçlicadorDaOSToolStripMenuItem
        '
        Me.AlterarOFatorMultipçlicadorDaOSToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.multiply
        Me.AlterarOFatorMultipçlicadorDaOSToolStripMenuItem.Name = "AlterarOFatorMultipçlicadorDaOSToolStripMenuItem"
        Me.AlterarOFatorMultipçlicadorDaOSToolStripMenuItem.Size = New System.Drawing.Size(442, 26)
        Me.AlterarOFatorMultipçlicadorDaOSToolStripMenuItem.Text = "Alterar o Fator Multiplicador da OS"
        Me.AlterarOFatorMultipçlicadorDaOSToolStripMenuItem.ToolTipText = "A alteração do Fator multiplicador irá ajustar todas as quantidades de totas as p" &
    "eças da OS, é com isso irá atualizar todos os arquivos da pasta da OS."
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(439, 6)
        '
        'GeralExcelDaOSToolStripMenuItem
        '
        Me.GeralExcelDaOSToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.icons8_exportação_excel_48
        Me.GeralExcelDaOSToolStripMenuItem.Name = "GeralExcelDaOSToolStripMenuItem"
        Me.GeralExcelDaOSToolStripMenuItem.Size = New System.Drawing.Size(442, 26)
        Me.GeralExcelDaOSToolStripMenuItem.Text = "Gerar Excel da OS sem Liberação para  Produção"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(439, 6)
        '
        'LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem
        '
        Me.LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.Limpar5
        Me.LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem.Name = "LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem"
        Me.LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(442, 26)
        Me.LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem.Text = "Limpar OS"
        Me.LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem.ToolTipText = "Esta função limpar todos os desenhos da OS e excluir todos os arquivos de desenho" &
    "s da pasta da OS."
        '
        'ToolStripSeparator17
        '
        Me.ToolStripSeparator17.Name = "ToolStripSeparator17"
        Me.ToolStripSeparator17.Size = New System.Drawing.Size(439, 6)
        '
        'CancelarAFabricaçãoDaOSToolStripMenuItem
        '
        Me.CancelarAFabricaçãoDaOSToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.sinco_Excluir
        Me.CancelarAFabricaçãoDaOSToolStripMenuItem.Name = "CancelarAFabricaçãoDaOSToolStripMenuItem"
        Me.CancelarAFabricaçãoDaOSToolStripMenuItem.Size = New System.Drawing.Size(442, 26)
        Me.CancelarAFabricaçãoDaOSToolStripMenuItem.Text = "Excluir Ordem de Serviço"
        Me.CancelarAFabricaçãoDaOSToolStripMenuItem.ToolTipText = "Cancela todos o processo de produção da OS."
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(439, 6)
        '
        'GerarArquivoEmDXFToolStripMenuItem
        '
        Me.GerarArquivoEmDXFToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.dxf
        Me.GerarArquivoEmDXFToolStripMenuItem.Name = "GerarArquivoEmDXFToolStripMenuItem"
        Me.GerarArquivoEmDXFToolStripMenuItem.Size = New System.Drawing.Size(442, 26)
        Me.GerarArquivoEmDXFToolStripMenuItem.Text = "Gerar arquivo em DXF"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(439, 6)
        '
        'GerarArquivoEmPDFToolStripMenuItem
        '
        Me.GerarArquivoEmPDFToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.pdf
        Me.GerarArquivoEmPDFToolStripMenuItem.Name = "GerarArquivoEmPDFToolStripMenuItem"
        Me.GerarArquivoEmPDFToolStripMenuItem.Size = New System.Drawing.Size(442, 26)
        Me.GerarArquivoEmPDFToolStripMenuItem.Text = "Gerar arquivo em PDF"
        '
        'ToolStripSeparator16
        '
        Me.ToolStripSeparator16.Name = "ToolStripSeparator16"
        Me.ToolStripSeparator16.Size = New System.Drawing.Size(439, 6)
        '
        'ToolStripSeparator20
        '
        Me.ToolStripSeparator20.Name = "ToolStripSeparator20"
        Me.ToolStripSeparator20.Size = New System.Drawing.Size(439, 6)
        '
        'CriarUmCopiaDaOSSelecionadaToolStripMenuItem
        '
        Me.CriarUmCopiaDaOSSelecionadaToolStripMenuItem.Name = "CriarUmCopiaDaOSSelecionadaToolStripMenuItem"
        Me.CriarUmCopiaDaOSSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(442, 26)
        Me.CriarUmCopiaDaOSSelecionadaToolStripMenuItem.Text = "Criar uma copia da OS Selecionada"
        '
        'TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem
        '
        Me.TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.produtos_32x32
        Me.TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem.Name = "TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem"
        Me.TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem.Size = New System.Drawing.Size(442, 26)
        Me.TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem.Text = "Transformar esta Ordem de Serviço em Referencia de produto Padrão"
        Me.TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem.ToolTipText = resources.GetString("TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem.ToolTip" &
        "Text")
        '
        'ToolStripSeparator30
        '
        Me.ToolStripSeparator30.Name = "ToolStripSeparator30"
        Me.ToolStripSeparator30.Size = New System.Drawing.Size(439, 6)
        '
        'InserirMaterialPeloFAPToolStripMenuItem
        '
        Me.InserirMaterialPeloFAPToolStripMenuItem.Name = "InserirMaterialPeloFAPToolStripMenuItem"
        Me.InserirMaterialPeloFAPToolStripMenuItem.Size = New System.Drawing.Size(442, 26)
        Me.InserirMaterialPeloFAPToolStripMenuItem.Text = "Inserir Material pelo FAP"
        '
        'InserirNumeroOPToolStripMenuItem
        '
        Me.InserirNumeroOPToolStripMenuItem.Name = "InserirNumeroOPToolStripMenuItem"
        Me.InserirNumeroOPToolStripMenuItem.Size = New System.Drawing.Size(442, 26)
        Me.InserirNumeroOPToolStripMenuItem.Text = "Inserir Numero da Ordem de Produção do ERP"
        '
        'lblFator
        '
        Me.lblFator.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFator.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.lblFator.Enabled = False
        Me.lblFator.Location = New System.Drawing.Point(606, 148)
        Me.lblFator.Margin = New System.Windows.Forms.Padding(2)
        Me.lblFator.Name = "lblFator"
        Me.lblFator.Size = New System.Drawing.Size(38, 20)
        Me.lblFator.TabIndex = 36
        '
        'Label37
        '
        Me.Label37.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(548, 66)
        Me.Label37.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(37, 13)
        Me.Label37.TabIndex = 33
        Me.Label37.Text = "Saldo:"
        '
        'chkMostraLiberadasPelaEngenharia
        '
        Me.chkMostraLiberadasPelaEngenharia.AutoSize = True
        Me.chkMostraLiberadasPelaEngenharia.Location = New System.Drawing.Point(6, 149)
        Me.chkMostraLiberadasPelaEngenharia.Margin = New System.Windows.Forms.Padding(2)
        Me.chkMostraLiberadasPelaEngenharia.Name = "chkMostraLiberadasPelaEngenharia"
        Me.chkMostraLiberadasPelaEngenharia.Size = New System.Drawing.Size(200, 17)
        Me.chkMostraLiberadasPelaEngenharia.TabIndex = 8
        Me.chkMostraLiberadasPelaEngenharia.Text = "Mostar OS Liberada pela Engenharia"
        Me.ToolTipAjuda.SetToolTip(Me.chkMostraLiberadasPelaEngenharia, resources.GetString("chkMostraLiberadasPelaEngenharia.ToolTip"))
        Me.chkMostraLiberadasPelaEngenharia.UseVisualStyleBackColor = True
        '
        'Label38
        '
        Me.Label38.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(570, 150)
        Me.Label38.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(34, 13)
        Me.Label38.TabIndex = 35
        Me.Label38.Text = "Fator:"
        '
        'Label36
        '
        Me.Label36.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(535, 39)
        Me.Label36.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(51, 13)
        Me.Label36.TabIndex = 31
        Me.Label36.Text = "Liberada:"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(202, 150)
        Me.Label30.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(59, 13)
        Me.Label30.TabIndex = 24
        Me.Label30.Text = "Criado Por:"
        '
        'txtPesqCriadoPor
        '
        Me.txtPesqCriadoPor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqCriadoPor.Location = New System.Drawing.Point(266, 148)
        Me.txtPesqCriadoPor.Margin = New System.Windows.Forms.Padding(2)
        Me.txtPesqCriadoPor.Name = "txtPesqCriadoPor"
        Me.txtPesqCriadoPor.Size = New System.Drawing.Size(127, 20)
        Me.txtPesqCriadoPor.TabIndex = 25
        '
        'Label35
        '
        Me.Label35.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(553, 10)
        Me.Label35.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(33, 13)
        Me.Label35.TabIndex = 29
        Me.Label35.Text = "Qtde:"
        '
        'txtDescricao
        '
        Me.txtDescricao.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescricao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDescricao.Location = New System.Drawing.Point(70, 86)
        Me.txtDescricao.Margin = New System.Windows.Forms.Padding(2)
        Me.txtDescricao.MaxLength = 200
        Me.txtDescricao.Multiline = True
        Me.txtDescricao.Name = "txtDescricao"
        Me.txtDescricao.Size = New System.Drawing.Size(576, 50)
        Me.txtDescricao.TabIndex = 13
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(14, 89)
        Me.Label25.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(58, 13)
        Me.Label25.TabIndex = 12
        Me.Label25.Text = "Descrição:"
        '
        'txtCliente
        '
        Me.txtCliente.Enabled = False
        Me.txtCliente.Location = New System.Drawing.Point(70, 32)
        Me.txtCliente.Margin = New System.Windows.Forms.Padding(2)
        Me.txtCliente.Multiline = True
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.Size = New System.Drawing.Size(174, 50)
        Me.txtCliente.TabIndex = 5
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(28, 11)
        Me.Label20.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(43, 13)
        Me.Label20.TabIndex = 1
        Me.Label20.Text = "Projeto:"
        '
        'cboProjeto
        '
        Me.cboProjeto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboProjeto.FormattingEnabled = True
        Me.cboProjeto.Location = New System.Drawing.Point(70, 8)
        Me.cboProjeto.Margin = New System.Windows.Forms.Padding(2)
        Me.cboProjeto.Name = "cboProjeto"
        Me.cboProjeto.Size = New System.Drawing.Size(174, 21)
        Me.cboProjeto.TabIndex = 2
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(248, 11)
        Me.Label24.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(29, 13)
        Me.Label24.TabIndex = 3
        Me.Label24.Text = "Tag:"
        '
        'cboTag
        '
        Me.cboTag.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboTag.FormattingEnabled = True
        Me.cboTag.Location = New System.Drawing.Point(278, 8)
        Me.cboTag.Margin = New System.Windows.Forms.Padding(2)
        Me.cboTag.Name = "cboTag"
        Me.cboTag.Size = New System.Drawing.Size(253, 21)
        Me.cboTag.TabIndex = 4
        '
        'txtDescricaoTag
        '
        Me.txtDescricaoTag.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescricaoTag.Enabled = False
        Me.txtDescricaoTag.Location = New System.Drawing.Point(278, 32)
        Me.txtDescricaoTag.Margin = New System.Windows.Forms.Padding(2)
        Me.txtDescricaoTag.Multiline = True
        Me.txtDescricaoTag.Name = "txtDescricaoTag"
        Me.txtDescricaoTag.Size = New System.Drawing.Size(253, 50)
        Me.txtDescricaoTag.TabIndex = 6
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Nothing
        Me.BindingNavigator1.AutoSize = False
        Me.BindingNavigator1.CountItem = Nothing
        Me.BindingNavigator1.DeleteItem = Nothing
        Me.BindingNavigator1.ImageScalingSize = New System.Drawing.Size(30, 30)
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton10, Me.ToolStripButton1, Me.TSBSalvarOrdemServico, Me.ToolStripButton3, Me.ProgressBarProcessoLiberacaoOrdemServico, Me.ToolStripSeparator29, Me.ToolStripTextBox1})
        Me.BindingNavigator1.Location = New System.Drawing.Point(0, 0)
        Me.BindingNavigator1.MoveFirstItem = Nothing
        Me.BindingNavigator1.MoveLastItem = Nothing
        Me.BindingNavigator1.MoveNextItem = Nothing
        Me.BindingNavigator1.MovePreviousItem = Nothing
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Nothing
        Me.BindingNavigator1.Size = New System.Drawing.Size(648, 51)
        Me.BindingNavigator1.TabIndex = 28
        Me.BindingNavigator1.Text = "BindingNavigator1"
        '
        'ToolStripButton10
        '
        Me.ToolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton10.Image = Global.SwLynx_4._1.My.Resources.Resources.Ajuda___Copia
        Me.ToolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton10.Name = "ToolStripButton10"
        Me.ToolStripButton10.Size = New System.Drawing.Size(34, 48)
        Me.ToolStripButton10.Text = "Trabalhando com a BOM"
        Me.ToolStripButton10.ToolTipText = "Video de Treinamento da BOM"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.CheckOnClick = True
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = Global.SwLynx_4._1.My.Resources.Resources.novo_arquivo
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(34, 48)
        Me.ToolStripButton1.Text = "Nova OS"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton1.ToolTipText = "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Clique aqui para criar uma nova Ordem de Serviço (OS)."
        '
        'TSBSalvarOrdemServico
        '
        Me.TSBSalvarOrdemServico.CheckOnClick = True
        Me.TSBSalvarOrdemServico.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBSalvarOrdemServico.Image = Global.SwLynx_4._1.My.Resources.Resources.sinco_salva___Copia
        Me.TSBSalvarOrdemServico.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBSalvarOrdemServico.Name = "TSBSalvarOrdemServico"
        Me.TSBSalvarOrdemServico.Size = New System.Drawing.Size(34, 48)
        Me.TSBSalvarOrdemServico.Text = "Salvar"
        Me.TSBSalvarOrdemServico.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TSBSalvarOrdemServico.ToolTipText = "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Após selecionar o Projeto e a Tag, clique aqui para salvar uma nov" &
    "a Ordem de Serviço (OS)."
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.CheckOnClick = True
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = Global.SwLynx_4._1.My.Resources.Resources.Atualizar___Copia
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(34, 48)
        Me.ToolStripButton3.Text = "Atualizar os dados que são recebidos do SINCO."
        Me.ToolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton3.ToolTipText = "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Caso um novo cadastro de Projeto e/ou Tag seja inserido e não " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ap" &
    "areça no ComboBox, basta clicar aqui para atualizar os dados."
        '
        'ProgressBarProcessoLiberacaoOrdemServico
        '
        Me.ProgressBarProcessoLiberacaoOrdemServico.AutoSize = False
        Me.ProgressBarProcessoLiberacaoOrdemServico.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ProgressBarProcessoLiberacaoOrdemServico.ForeColor = System.Drawing.Color.Green
        Me.ProgressBarProcessoLiberacaoOrdemServico.Name = "ProgressBarProcessoLiberacaoOrdemServico"
        Me.ProgressBarProcessoLiberacaoOrdemServico.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ProgressBarProcessoLiberacaoOrdemServico.RightToLeftLayout = True
        Me.ProgressBarProcessoLiberacaoOrdemServico.Size = New System.Drawing.Size(100, 20)
        Me.ProgressBarProcessoLiberacaoOrdemServico.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'ToolStripSeparator29
        '
        Me.ToolStripSeparator29.Name = "ToolStripSeparator29"
        Me.ToolStripSeparator29.Size = New System.Drawing.Size(6, 51)
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ToolStripTextBox1.AutoSize = False
        Me.ToolStripTextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ToolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ToolStripTextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.ToolStripTextBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ToolStripTextBox1.MaxLength = 10
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(94, 23)
        Me.ToolStripTextBox1.ToolTipText = "Digite aqui o numero do projeto para Facilitar a localização no Combox do Projeto" &
    ""
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgvTimerMedidordgvMedidorProjetistaOSMes)
        Me.TabPage1.Controls.Add(Me.Label16)
        Me.TabPage1.Controls.Add(Me.dgvTimerMedidordgvMedidorPesoProducao)
        Me.TabPage1.Controls.Add(Me.Label15)
        Me.TabPage1.Controls.Add(Me.Panel7)
        Me.TabPage1.Controls.Add(Me.Panel6)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(648, 747)
        Me.TabPage1.TabIndex = 4
        Me.TabPage1.Text = "Meu - Medidor"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'dgvTimerMedidordgvMedidorProjetistaOSMes
        '
        Me.dgvTimerMedidordgvMedidorProjetistaOSMes.AllowUserToAddRows = False
        Me.dgvTimerMedidordgvMedidorProjetistaOSMes.AllowUserToDeleteRows = False
        Me.dgvTimerMedidordgvMedidorProjetistaOSMes.AllowUserToOrderColumns = True
        Me.dgvTimerMedidordgvMedidorProjetistaOSMes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.dgvTimerMedidordgvMedidorProjetistaOSMes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTimerMedidordgvMedidorProjetistaOSMes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTimerMedidordgvMedidorProjetistaOSMes.Location = New System.Drawing.Point(3, 541)
        Me.dgvTimerMedidordgvMedidorProjetistaOSMes.Name = "dgvTimerMedidordgvMedidorProjetistaOSMes"
        Me.dgvTimerMedidordgvMedidorProjetistaOSMes.ReadOnly = True
        Me.dgvTimerMedidordgvMedidorProjetistaOSMes.RowHeadersWidth = 51
        Me.dgvTimerMedidordgvMedidorProjetistaOSMes.Size = New System.Drawing.Size(642, 203)
        Me.dgvTimerMedidordgvMedidorProjetistaOSMes.TabIndex = 7
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(111, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.White
        Me.Label16.Location = New System.Drawing.Point(3, 505)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(642, 36)
        Me.Label16.TabIndex = 6
        Me.Label16.Text = "Medição de Produção por Projetista/Numero OS/Mês"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvTimerMedidordgvMedidorPesoProducao
        '
        Me.dgvTimerMedidordgvMedidorPesoProducao.AllowUserToAddRows = False
        Me.dgvTimerMedidordgvMedidorPesoProducao.AllowUserToDeleteRows = False
        Me.dgvTimerMedidordgvMedidorPesoProducao.AllowUserToOrderColumns = True
        Me.dgvTimerMedidordgvMedidorPesoProducao.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.dgvTimerMedidordgvMedidorPesoProducao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTimerMedidordgvMedidorPesoProducao.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgvTimerMedidordgvMedidorPesoProducao.Location = New System.Drawing.Point(3, 340)
        Me.dgvTimerMedidordgvMedidorPesoProducao.Name = "dgvTimerMedidordgvMedidorPesoProducao"
        Me.dgvTimerMedidordgvMedidorPesoProducao.ReadOnly = True
        Me.dgvTimerMedidordgvMedidorPesoProducao.RowHeadersWidth = 51
        Me.dgvTimerMedidordgvMedidorPesoProducao.Size = New System.Drawing.Size(642, 165)
        Me.dgvTimerMedidordgvMedidorPesoProducao.TabIndex = 5
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(111, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.White
        Me.Label15.Location = New System.Drawing.Point(3, 304)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(642, 36)
        Me.Label15.TabIndex = 4
        Me.Label15.Text = "Medição de Produção por Projetista/Mês/Tipo Desenho"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.dgvMedidorNumeroOS)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(3, 39)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(642, 265)
        Me.Panel7.TabIndex = 3
        '
        'dgvMedidorNumeroOS
        '
        Me.dgvMedidorNumeroOS.AllowUserToAddRows = False
        Me.dgvMedidorNumeroOS.AllowUserToDeleteRows = False
        Me.dgvMedidorNumeroOS.AllowUserToOrderColumns = True
        Me.dgvMedidorNumeroOS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.dgvMedidorNumeroOS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMedidorNumeroOS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMedidorNumeroOS.Location = New System.Drawing.Point(0, 0)
        Me.dgvMedidorNumeroOS.Name = "dgvMedidorNumeroOS"
        Me.dgvMedidorNumeroOS.ReadOnly = True
        Me.dgvMedidorNumeroOS.RowHeadersWidth = 51
        Me.dgvMedidorNumeroOS.Size = New System.Drawing.Size(642, 265)
        Me.dgvMedidorNumeroOS.TabIndex = 0
        '
        'Panel6
        '
        Me.Panel6.AutoSize = True
        Me.Panel6.Controls.Add(Me.btnDadosUsuario)
        Me.Panel6.Controls.Add(Me.Label11)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(3, 3)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(642, 36)
        Me.Panel6.TabIndex = 2
        '
        'btnDadosUsuario
        '
        Me.btnDadosUsuario.Location = New System.Drawing.Point(3, 7)
        Me.btnDadosUsuario.Name = "btnDadosUsuario"
        Me.btnDadosUsuario.Size = New System.Drawing.Size(125, 23)
        Me.btnDadosUsuario.TabIndex = 2
        Me.btnDadosUsuario.Text = "Meus Numeros"
        Me.btnDadosUsuario.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(111, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(642, 36)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Numero de OS Liberadas para Produção/Projetista"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.txtPesqTagProjetista)
        Me.TabPage4.Controls.Add(Me.Label39)
        Me.TabPage4.Controls.Add(Me.txtPesqProjetoProjetista)
        Me.TabPage4.Controls.Add(Me.Label34)
        Me.TabPage4.Controls.Add(Me.chkProjetista)
        Me.TabPage4.Controls.Add(Me.chkMostraProjetoTagsFinalizadas)
        Me.TabPage4.Controls.Add(Me.Button1)
        Me.TabPage4.Controls.Add(Me.Label18)
        Me.TabPage4.Controls.Add(Me.dgvPlanejamentoProjetista)
        Me.TabPage4.Location = New System.Drawing.Point(4, 25)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(648, 747)
        Me.TabPage4.TabIndex = 5
        Me.TabPage4.Text = "Meu Planejamento"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'txtPesqTagProjetista
        '
        Me.txtPesqTagProjetista.Location = New System.Drawing.Point(232, 70)
        Me.txtPesqTagProjetista.Margin = New System.Windows.Forms.Padding(2)
        Me.txtPesqTagProjetista.Name = "txtPesqTagProjetista"
        Me.txtPesqTagProjetista.Size = New System.Drawing.Size(134, 20)
        Me.txtPesqTagProjetista.TabIndex = 10
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(191, 72)
        Me.Label39.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(29, 13)
        Me.Label39.TabIndex = 9
        Me.Label39.Text = "Tag:"
        '
        'txtPesqProjetoProjetista
        '
        Me.txtPesqProjetoProjetista.Location = New System.Drawing.Point(47, 68)
        Me.txtPesqProjetoProjetista.Margin = New System.Windows.Forms.Padding(2)
        Me.txtPesqProjetoProjetista.Name = "txtPesqProjetoProjetista"
        Me.txtPesqProjetoProjetista.Size = New System.Drawing.Size(134, 20)
        Me.txtPesqProjetoProjetista.TabIndex = 8
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(6, 70)
        Me.Label34.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(43, 13)
        Me.Label34.TabIndex = 7
        Me.Label34.Text = "Projeto:"
        '
        'chkProjetista
        '
        Me.chkProjetista.AutoSize = True
        Me.chkProjetista.Location = New System.Drawing.Point(197, 47)
        Me.chkProjetista.Margin = New System.Windows.Forms.Padding(2)
        Me.chkProjetista.Name = "chkProjetista"
        Me.chkProjetista.Size = New System.Drawing.Size(118, 17)
        Me.chkProjetista.TabIndex = 6
        Me.chkProjetista.Text = "Filtra Meus Projetos"
        Me.chkProjetista.UseVisualStyleBackColor = True
        '
        'chkMostraProjetoTagsFinalizadas
        '
        Me.chkMostraProjetoTagsFinalizadas.AutoSize = True
        Me.chkMostraProjetoTagsFinalizadas.Location = New System.Drawing.Point(5, 47)
        Me.chkMostraProjetoTagsFinalizadas.Margin = New System.Windows.Forms.Padding(2)
        Me.chkMostraProjetoTagsFinalizadas.Name = "chkMostraProjetoTagsFinalizadas"
        Me.chkMostraProjetoTagsFinalizadas.Size = New System.Drawing.Size(186, 17)
        Me.chkMostraProjetoTagsFinalizadas.TabIndex = 5
        Me.chkMostraProjetoTagsFinalizadas.Text = "Mostrar Projetos/Tags Realizadas"
        Me.chkMostraProjetoTagsFinalizadas.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(6, 11)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(142, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Atualizar Planejamento"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(111, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.White
        Me.Label18.Location = New System.Drawing.Point(3, 3)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(642, 36)
        Me.Label18.TabIndex = 3
        Me.Label18.Text = "Lista as atividades do projetista Logado"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvPlanejamentoProjetista
        '
        Me.dgvPlanejamentoProjetista.AllowUserToAddRows = False
        Me.dgvPlanejamentoProjetista.AllowUserToDeleteRows = False
        Me.dgvPlanejamentoProjetista.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPlanejamentoProjetista.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.dgvPlanejamentoProjetista.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        Me.dgvPlanejamentoProjetista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPlanejamentoProjetista.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvEngpro, Me.dgvcaminhoPDF})
        Me.dgvPlanejamentoProjetista.ContextMenuStrip = Me.mnuTimerdgvPlanejamentoProjetista
        Me.dgvPlanejamentoProjetista.Location = New System.Drawing.Point(3, 92)
        Me.dgvPlanejamentoProjetista.Name = "dgvPlanejamentoProjetista"
        Me.dgvPlanejamentoProjetista.ReadOnly = True
        Me.dgvPlanejamentoProjetista.RowHeadersWidth = 51
        Me.dgvPlanejamentoProjetista.Size = New System.Drawing.Size(644, 652)
        Me.dgvPlanejamentoProjetista.TabIndex = 0
        '
        'dgvEngpro
        '
        Me.dgvEngpro.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.dgvEngpro.Frozen = True
        Me.dgvEngpro.HeaderText = "dgvEngpro"
        Me.dgvEngpro.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvEngpro.MinimumWidth = 6
        Me.dgvEngpro.Name = "dgvEngpro"
        Me.dgvEngpro.ReadOnly = True
        Me.dgvEngpro.Width = 25
        '
        'dgvcaminhoPDF
        '
        Me.dgvcaminhoPDF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.dgvcaminhoPDF.Frozen = True
        Me.dgvcaminhoPDF.HeaderText = "dgvcaminhoPDF"
        Me.dgvcaminhoPDF.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvcaminhoPDF.MinimumWidth = 6
        Me.dgvcaminhoPDF.Name = "dgvcaminhoPDF"
        Me.dgvcaminhoPDF.ReadOnly = True
        Me.dgvcaminhoPDF.Width = 25
        '
        'mnuTimerdgvPlanejamentoProjetista
        '
        Me.mnuTimerdgvPlanejamentoProjetista.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuTimerdgvPlanejamentoProjetista.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MarcarIncioDaExecuçãoToolStripMenuItem, Me.MarcarFinalizaçãoDoProjetoToolStripMenuItem})
        Me.mnuTimerdgvPlanejamentoProjetista.Name = "mnuTimerdgvPlanejamentoProjetista"
        Me.mnuTimerdgvPlanejamentoProjetista.Size = New System.Drawing.Size(235, 56)
        '
        'MarcarIncioDaExecuçãoToolStripMenuItem
        '
        Me.MarcarIncioDaExecuçãoToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.curti
        Me.MarcarIncioDaExecuçãoToolStripMenuItem.Name = "MarcarIncioDaExecuçãoToolStripMenuItem"
        Me.MarcarIncioDaExecuçãoToolStripMenuItem.Size = New System.Drawing.Size(234, 26)
        Me.MarcarIncioDaExecuçãoToolStripMenuItem.Text = "Marcar Incio da Execução"
        '
        'MarcarFinalizaçãoDoProjetoToolStripMenuItem
        '
        Me.MarcarFinalizaçãoDoProjetoToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.mao
        Me.MarcarFinalizaçãoDoProjetoToolStripMenuItem.Name = "MarcarFinalizaçãoDoProjetoToolStripMenuItem"
        Me.MarcarFinalizaçãoDoProjetoToolStripMenuItem.Size = New System.Drawing.Size(234, 26)
        Me.MarcarFinalizaçãoDoProjetoToolStripMenuItem.Text = "Marcar Finalização do Projeto"
        '
        'tpbOrdemServico
        '
        Me.tpbOrdemServico.Controls.Add(Me.btnAtualizarDadosOrdemServicoItens)
        Me.tpbOrdemServico.Controls.Add(Me.Label33)
        Me.tpbOrdemServico.Controls.Add(Me.dgvOrdemservico)
        Me.tpbOrdemServico.Controls.Add(Me.txtOS)
        Me.tpbOrdemServico.Controls.Add(Me.Label32)
        Me.tpbOrdemServico.Controls.Add(Me.TXTTag)
        Me.tpbOrdemServico.Controls.Add(Me.TXTPROJETO)
        Me.tpbOrdemServico.Controls.Add(Me.Label31)
        Me.tpbOrdemServico.Controls.Add(Me.Label19)
        Me.tpbOrdemServico.Location = New System.Drawing.Point(4, 25)
        Me.tpbOrdemServico.Margin = New System.Windows.Forms.Padding(2)
        Me.tpbOrdemServico.Name = "tpbOrdemServico"
        Me.tpbOrdemServico.Padding = New System.Windows.Forms.Padding(2)
        Me.tpbOrdemServico.Size = New System.Drawing.Size(648, 747)
        Me.tpbOrdemServico.TabIndex = 6
        Me.tpbOrdemServico.Text = "Ordem de Serviço/Peças"
        Me.tpbOrdemServico.UseVisualStyleBackColor = True
        '
        'btnAtualizarDadosOrdemServicoItens
        '
        Me.btnAtualizarDadosOrdemServicoItens.Location = New System.Drawing.Point(354, 57)
        Me.btnAtualizarDadosOrdemServicoItens.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAtualizarDadosOrdemServicoItens.Name = "btnAtualizarDadosOrdemServicoItens"
        Me.btnAtualizarDadosOrdemServicoItens.Size = New System.Drawing.Size(86, 19)
        Me.btnAtualizarDadosOrdemServicoItens.TabIndex = 8
        Me.btnAtualizarDadosOrdemServicoItens.Text = "Atualizar"
        Me.ToolTipAjuda.SetToolTip(Me.btnAtualizarDadosOrdemServicoItens, "Atualiza os dados nas Ordem de Serviço em Aberto")
        Me.btnAtualizarDadosOrdemServicoItens.UseVisualStyleBackColor = True
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(111, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.White
        Me.Label33.Location = New System.Drawing.Point(2, 2)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(644, 36)
        Me.Label33.TabIndex = 7
        Me.Label33.Text = "Lista  as Ordem de Serviço da peça corrente"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvOrdemservico
        '
        Me.dgvOrdemservico.AllowUserToAddRows = False
        Me.dgvOrdemservico.AllowUserToDeleteRows = False
        Me.dgvOrdemservico.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvOrdemservico.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.dgvOrdemservico.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        Me.dgvOrdemservico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOrdemservico.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvEng})
        Me.dgvOrdemservico.Location = New System.Drawing.Point(5, 80)
        Me.dgvOrdemservico.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvOrdemservico.Name = "dgvOrdemservico"
        Me.dgvOrdemservico.ReadOnly = True
        Me.dgvOrdemservico.RowHeadersWidth = 51
        Me.dgvOrdemservico.RowTemplate.Height = 24
        Me.dgvOrdemservico.Size = New System.Drawing.Size(641, 667)
        Me.dgvOrdemservico.TabIndex = 6
        '
        'dgvEng
        '
        Me.dgvEng.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.dgvEng.Frozen = True
        Me.dgvEng.HeaderText = "dgvInicio"
        Me.dgvEng.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvEng.MinimumWidth = 6
        Me.dgvEng.Name = "dgvEng"
        Me.dgvEng.ReadOnly = True
        Me.dgvEng.Width = 25
        '
        'txtOS
        '
        Me.txtOS.Location = New System.Drawing.Point(5, 58)
        Me.txtOS.Margin = New System.Windows.Forms.Padding(2)
        Me.txtOS.Name = "txtOS"
        Me.txtOS.Size = New System.Drawing.Size(113, 20)
        Me.txtOS.TabIndex = 5
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(3, 42)
        Me.Label32.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(25, 13)
        Me.Label32.TabIndex = 4
        Me.Label32.Text = "OS:"
        '
        'TXTTag
        '
        Me.TXTTag.Location = New System.Drawing.Point(238, 58)
        Me.TXTTag.Margin = New System.Windows.Forms.Padding(2)
        Me.TXTTag.Name = "TXTTag"
        Me.TXTTag.Size = New System.Drawing.Size(113, 20)
        Me.TXTTag.TabIndex = 3
        '
        'TXTPROJETO
        '
        Me.TXTPROJETO.Location = New System.Drawing.Point(122, 58)
        Me.TXTPROJETO.Margin = New System.Windows.Forms.Padding(2)
        Me.TXTPROJETO.Name = "TXTPROJETO"
        Me.TXTPROJETO.Size = New System.Drawing.Size(113, 20)
        Me.TXTPROJETO.TabIndex = 2
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(236, 42)
        Me.Label31.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(29, 13)
        Me.Label31.TabIndex = 1
        Me.Label31.Text = "Tag:"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(119, 42)
        Me.Label19.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(43, 13)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "Projeto:"
        '
        'mnuDGVMontaPeca
        '
        Me.mnuDGVMontaPeca.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuDGVMontaPeca.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcluirOMaterialDoDesenhoToolStripMenuItem})
        Me.mnuDGVMontaPeca.Name = "mnuDGVMontaPeca"
        Me.mnuDGVMontaPeca.Size = New System.Drawing.Size(236, 30)
        '
        'ExcluirOMaterialDoDesenhoToolStripMenuItem
        '
        Me.ExcluirOMaterialDoDesenhoToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.sinco_Excluir
        Me.ExcluirOMaterialDoDesenhoToolStripMenuItem.Name = "ExcluirOMaterialDoDesenhoToolStripMenuItem"
        Me.ExcluirOMaterialDoDesenhoToolStripMenuItem.Size = New System.Drawing.Size(235, 26)
        Me.ExcluirOMaterialDoDesenhoToolStripMenuItem.Text = "Excluir o material do Desenho"
        '
        'mnubtnListaMaterial
        '
        Me.mnubtnListaMaterial.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnubtnListaMaterial.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InformeOTituloPadrãoDoProdutoToolStripMenuItem})
        Me.mnubtnListaMaterial.Name = "mnubtnListaMaterial"
        Me.mnubtnListaMaterial.Size = New System.Drawing.Size(263, 26)
        '
        'InformeOTituloPadrãoDoProdutoToolStripMenuItem
        '
        Me.InformeOTituloPadrãoDoProdutoToolStripMenuItem.Name = "InformeOTituloPadrãoDoProdutoToolStripMenuItem"
        Me.InformeOTituloPadrãoDoProdutoToolStripMenuItem.Size = New System.Drawing.Size(262, 22)
        Me.InformeOTituloPadrãoDoProdutoToolStripMenuItem.Text = "Informe o Titulo Padrão do Produto"
        '
        'Timerdgvos
        '
        '
        'TimerDGVListaMaterialSW
        '
        '
        'ToolTipAjuda
        '
        Me.ToolTipAjuda.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTipAjuda.ToolTipTitle = "Dicas de Uso:"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'TimerHoraServidor
        '
        Me.TimerHoraServidor.Interval = 300000
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.Frozen = True
        Me.DataGridViewImageColumn1.HeaderText = "Tipo"
        Me.DataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DataGridViewImageColumn1.MinimumWidth = 6
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.ReadOnly = True
        Me.DataGridViewImageColumn1.Width = 6
        '
        'TimerProdutoItens
        '
        Me.TimerProdutoItens.Interval = 500
        '
        'TimerProdutos
        '
        Me.TimerProdutos.Interval = 500
        '
        'TimerMedidordgvMedidorNumeroOS
        '
        '
        'TimerAviso
        '
        '
        'TimerdgvPlanejamentoProjetista
        '
        '
        'TimerdgvOrdemservico
        '
        '
        'Painel_Leitura_Dados
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Controls.Add(Me.tpgPrincipal)
        Me.Name = "Painel_Leitura_Dados"
        Me.Size = New System.Drawing.Size(662, 779)
        Me.tpgPrincipal.ResumeLayout(False)
        Me.tpgFolhaDados.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        CType(Me.BnPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BnPrincipal.ResumeLayout(False)
        Me.BnPrincipal.PerformLayout()
        Me.tpgBom.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.dgvDataGridBOM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnudgvDataGridBOM.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        CType(Me.BindingNavigator2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator2.ResumeLayout(False)
        Me.BindingNavigator2.PerformLayout()
        Me.tpgOrdemServico.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.TabControlOS.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DGVListaMaterialSW, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuDGVListaMaterialSW.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        CType(Me.DGVListaMaterialSWMateriais, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.dgvos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnudgvos.ResumeLayout(False)
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.dgvTimerMedidordgvMedidorProjetistaOSMes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTimerMedidordgvMedidorPesoProducao, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        CType(Me.dgvMedidorNumeroOS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        CType(Me.dgvPlanejamentoProjetista, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuTimerdgvPlanejamentoProjetista.ResumeLayout(False)
        Me.tpbOrdemServico.ResumeLayout(False)
        Me.tpbOrdemServico.PerformLayout()
        CType(Me.dgvOrdemservico, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuDGVMontaPeca.ResumeLayout(False)
        Me.mnubtnListaMaterial.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tpgPrincipal As Windows.Forms.TabControl
    Friend WithEvents tpgFolhaDados As Windows.Forms.TabPage
    Friend WithEvents tpgBom As Windows.Forms.TabPage
    Friend WithEvents txtComentarios As Windows.Forms.TextBox
    Friend WithEvents txtPalavraChave As Windows.Forms.TextBox
    Friend WithEvents txtAuthor As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents txtAssuntoSubiTitulo As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents Label13 As Windows.Forms.Label
    Friend WithEvents Label14 As Windows.Forms.Label
    Friend WithEvents Label21 As Windows.Forms.Label
    Friend WithEvents Label22 As Windows.Forms.Label
    Friend WithEvents Label23 As Windows.Forms.Label
    Friend WithEvents Label17 As Windows.Forms.Label
    Friend WithEvents dgvDataGridBOM As Windows.Forms.DataGridView
    Friend WithEvents chkVerificarDXF As Windows.Forms.CheckBox
    Friend WithEvents chkVerificarPDF As Windows.Forms.CheckBox
    Friend WithEvents mnuDGVListaMaterialSW As Windows.Forms.ContextMenuStrip
    Friend WithEvents AbrirPDFDaLinhaSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As Windows.Forms.ToolStripSeparator
    Friend WithEvents AbrirDXFDaLinhaSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents tpgOrdemServico As Windows.Forms.TabPage
    Friend WithEvents cboProjeto As Windows.Forms.ComboBox
    Friend WithEvents Label20 As Windows.Forms.Label
    Friend WithEvents txtDescricaoTag As Windows.Forms.TextBox
    Friend WithEvents txtCliente As Windows.Forms.TextBox
    Friend WithEvents cboTag As Windows.Forms.ComboBox
    Friend WithEvents Label24 As Windows.Forms.Label
    Friend WithEvents chkConverterDXF As Windows.Forms.CheckBox
    Friend WithEvents chkConverterPDF As Windows.Forms.CheckBox
    Friend WithEvents ToolStripSeparator2 As Windows.Forms.ToolStripSeparator
    Friend WithEvents DataGridViewImageColumn1 As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents ToolStripSeparator3 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ExcluirODocumentoDaLinhaSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimerMontaPeca As Windows.Forms.Timer
    Friend WithEvents dgvos As Windows.Forms.DataGridView
    Friend WithEvents chkMostraLiberadasPelaEngenharia As Windows.Forms.CheckBox
    Friend WithEvents Timerdgvos As Windows.Forms.Timer
    Friend WithEvents dgvStatus As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DGVListaMaterialSW As Windows.Forms.DataGridView
    Friend WithEvents TimerDGVListaMaterialSW As Windows.Forms.Timer
    Friend WithEvents txtDescricao As Windows.Forms.TextBox
    Friend WithEvents Label25 As Windows.Forms.Label
    Friend WithEvents mnudgvos As Windows.Forms.ContextMenuStrip
    Friend WithEvents AbrirPastaDaOrdemDeServiçoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As Windows.Forms.ToolStripSeparator
    Friend WithEvents LiberarOrdemDeServiçoParaProduçãoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As Windows.Forms.ToolStripSeparator
    Friend WithEvents MarcarComoConjuntoPrincipalDaOrdemDeServiçoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DesmarcarComoConjuntoPrincipalToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents cboOpcoesAcabamento As Windows.Forms.ComboBox
    Friend WithEvents Label26 As Windows.Forms.Label
    Friend WithEvents btnAplicarAcabamento As Windows.Forms.Button
    Friend WithEvents MarcarTodosToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DesmarcarTodosToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents InverterSeleçãoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As Windows.Forms.ToolStripSeparator
    Friend WithEvents txtPesqAcabamentoDesenho As Windows.Forms.TextBox
    Friend WithEvents Label29 As Windows.Forms.Label
    Friend WithEvents txtPesqTipoDesenho As Windows.Forms.TextBox
    Friend WithEvents Label28 As Windows.Forms.Label
    Friend WithEvents txtPesqNumeroDesenho As Windows.Forms.TextBox
    Friend WithEvents Label27 As Windows.Forms.Label
    Friend WithEvents ToolStripSeparator7 As Windows.Forms.ToolStripSeparator
    Friend WithEvents AlterarAQuantidadeDePeçasFabricaçãoDaLinhaSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ImprimirDesenhoPDFSelecionadoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As Windows.Forms.ToolStripSeparator
    Friend WithEvents AtualizarPDFsEDXFsNaPastaDaOSMToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GeralExcelDaOSToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolTipAjuda As Windows.Forms.ToolTip
    Friend WithEvents ToolStripSeparator11 As Windows.Forms.ToolStripSeparator
    Friend WithEvents LimparPastaOrdemDeServiçoSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As Windows.Forms.OpenFileDialog
    Friend WithEvents txtPesqCriadoPor As Windows.Forms.TextBox
    Friend WithEvents Label30 As Windows.Forms.Label
    Friend WithEvents ToolStripSeparator12 As Windows.Forms.ToolStripSeparator
    Friend WithEvents CancelarAFabricaçãoDaOSToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDGVMontaPeca As Windows.Forms.ContextMenuStrip
    Friend WithEvents ExcluirOMaterialDoDesenhoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnubtnListaMaterial As Windows.Forms.ContextMenuStrip
    Friend WithEvents InformeOTituloPadrãoDoProdutoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox8 As Windows.Forms.GroupBox
    Friend WithEvents optProcessoSoldagemSim As Windows.Forms.RadioButton
    Friend WithEvents optProcessoSoldagemNao As Windows.Forms.RadioButton
    Friend WithEvents GroupBox7 As Windows.Forms.GroupBox
    Friend WithEvents OPTEstoqueSim As Windows.Forms.RadioButton
    Friend WithEvents OPTEstoqueNao As Windows.Forms.RadioButton
    Friend WithEvents TimerHoraServidor As Windows.Forms.Timer
    Friend WithEvents chkVerificarDFT As Windows.Forms.CheckBox
    Friend WithEvents GerarArquivoEmDXFToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GerarArquivoEmPDFToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvSelecao As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents dgvIconeItemOS As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents dgvDXF As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents dgvPDF As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents ToolStripSeparator14 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator13 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator15 As Windows.Forms.ToolStripSeparator
    Friend WithEvents AbrirDWRDaLinhaSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents AlterarOFatorMultipçlicadorDaOSToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator16 As Windows.Forms.ToolStripSeparator
    Friend WithEvents CriarUmCopiaDaOSSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator17 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator18 As Windows.Forms.ToolStripSeparator
    Friend WithEvents GerarPDFDasLinhasSelecionadasToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GerarDXFDasLinhasSelecionadasToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator19 As Windows.Forms.ToolStripSeparator
    Friend WithEvents CancelarLiberaçãoDaOSToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator20 As Windows.Forms.ToolStripSeparator
    Friend WithEvents chkVerificarLXDS As Windows.Forms.CheckBox
    Friend WithEvents btnPendencias As Windows.Forms.Button
    Friend WithEvents chkBoxTipoDesenho As Windows.Forms.CheckedListBox
    Friend WithEvents chkBoxAcabamento As Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox10 As Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents lblEspessura As Windows.Forms.Label
    Friend WithEvents lblPeso As Windows.Forms.Label
    Friend WithEvents lblMaterial As Windows.Forms.Label
    Friend WithEvents lblLarguraTotalCaixaDelimitadora As Windows.Forms.Label
    Friend WithEvents lblAreaPintura As Windows.Forms.Label
    Friend WithEvents lblNumeroDobra As Windows.Forms.Label
    Friend WithEvents lblComprimento As Windows.Forms.Label
    Friend WithEvents lblLargura As Windows.Forms.Label
    Friend WithEvents lblProfundidadeTotalCaixaDelimitadora As Windows.Forms.Label
    Friend WithEvents lblAlturaTotalCaixaDelimitadora As Windows.Forms.Label
    Friend WithEvents GroupBox5 As Windows.Forms.GroupBox
    Friend WithEvents TransformarEstaOrdemDeServiçoEmReferenciaDeProdutoPadrãoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimerProdutoItens As Windows.Forms.Timer
    Friend WithEvents mnudgvDataGridBOM As Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripSeparator34 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem3 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator33 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem2 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator35 As Windows.Forms.ToolStripSeparator
    Friend WithEvents AbrirLXDSDaLinhaSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator36 As Windows.Forms.ToolStripSeparator
    Friend WithEvents txtTitulo As Windows.Forms.TextBox
    Friend WithEvents Label35 As Windows.Forms.Label
    Friend WithEvents Label37 As Windows.Forms.Label
    Friend WithEvents Label36 As Windows.Forms.Label
    Friend WithEvents lblFator As Windows.Forms.TextBox
    Friend WithEvents Label38 As Windows.Forms.Label
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents chkBoxProcessos As Windows.Forms.CheckedListBox
    Friend WithEvents BindingNavigator1 As Windows.Forms.BindingNavigator
    Friend WithEvents ToolStripButton10 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton1 As Windows.Forms.ToolStripButton
    Friend WithEvents TSBSalvarOrdemServico As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As Windows.Forms.ToolStripButton
    Friend WithEvents TimerFiltroPecaAtivaOS As Windows.Forms.Timer
    Friend WithEvents TimerProdutos As Windows.Forms.Timer
    Friend WithEvents TimerpcpAgrupamentoProjeto As Windows.Forms.Timer
    Friend WithEvents BnPrincipal As Windows.Forms.BindingNavigator
    Friend WithEvents tsbAjuda As Windows.Forms.ToolStripButton
    Friend WithEvents tsBLerDados As Windows.Forms.ToolStripButton
    Friend WithEvents tsbSalvar As Windows.Forms.ToolStripButton
    Friend WithEvents tsbConverterDXF As Windows.Forms.ToolStripButton
    Friend WithEvents TSBConverterPDF As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton8 As Windows.Forms.ToolStripButton
    Friend WithEvents TSBAssociarMaterial As Windows.Forms.ToolStripButton
    Friend WithEvents tsbInserirNaOS As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator23 As Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbConfiguracoes As Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ConfiguraçãoToolStripMenuItem2 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfExportarArquivoParaOSToolStripMenuItem1 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator21 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator24 As Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbFerramentas As Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripSeparator22 As Windows.Forms.ToolStripSeparator
    Friend WithEvents AtualizarDesenhoPeloDiretorioToolStripMenuItem1 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsarFormatoA3ToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsarFormatoA4ToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsarFornatoA4DToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator25 As Windows.Forms.ToolStripSeparator
    Friend WithEvents TsbInspecaoQualidade As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator39 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton9 As Windows.Forms.ToolStripButton
    Friend WithEvents tslVersaoSistema As Windows.Forms.ToolStripLabel
    Friend WithEvents txtNomeArquivo As Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigator2 As Windows.Forms.BindingNavigator
    Friend WithEvents ToolStripButton4 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator26 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton5 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator28 As Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbProcessaoListaMaterialBOM As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator27 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton7 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator37 As Windows.Forms.ToolStripSeparator
    Friend WithEvents TsbAtualizarBOM As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator38 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ProgressBarListaSW As Windows.Forms.ToolStripProgressBar
    Friend WithEvents lblOrdemServicoAtiva As Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripButton11 As Windows.Forms.ToolStripButton
    Friend WithEvents ProgressBarProcessoLiberacaoOrdemServico As Windows.Forms.ToolStripProgressBar
    Friend WithEvents chkAtualizacao As Windows.Forms.CheckBox
    Friend WithEvents tspOpcaoSalvamentoAUTOMADICO As Windows.Forms.ToolStripButton
    Friend WithEvents txtAprovado As Windows.Forms.TextBox
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents txtVerificado As Windows.Forms.TextBox
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents Panel1 As Windows.Forms.Panel
    Friend WithEvents Panel2 As Windows.Forms.Panel
    Friend WithEvents Panel4 As Windows.Forms.Panel
    Friend WithEvents Panel3 As Windows.Forms.Panel
    Friend WithEvents TrocarFormatoA3PeloDiretorioSelecionadoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbTrocarFormato As Windows.Forms.ToolStripButton
    Friend WithEvents choBloqueaArquivoExistente As Windows.Forms.CheckBox
    Friend WithEvents chkTrocarFormato As Windows.Forms.CheckBox
    Friend WithEvents FlowLayoutPanel1 As Windows.Forms.FlowLayoutPanel
    Friend WithEvents Panel5 As Windows.Forms.Panel
    Friend WithEvents TabPage1 As Windows.Forms.TabPage
    Friend WithEvents Panel7 As Windows.Forms.Panel
    Friend WithEvents dgvMedidorNumeroOS As Windows.Forms.DataGridView
    Friend WithEvents Panel6 As Windows.Forms.Panel
    Friend WithEvents Label11 As Windows.Forms.Label
    Friend WithEvents TimerMedidordgvMedidorNumeroOS As Windows.Forms.Timer
    Friend WithEvents dgvTimerMedidordgvMedidorPesoProducao As Windows.Forms.DataGridView
    Friend WithEvents Label15 As Windows.Forms.Label
    Friend WithEvents dgvTimerMedidordgvMedidorProjetistaOSMes As Windows.Forms.DataGridView
    Friend WithEvents Label16 As Windows.Forms.Label
    Friend WithEvents DGVIconeLXDS As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DGVIconeDXF As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents dgvIconePDF As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents lbltxtQtdeTag As Windows.Forms.Label
    Friend WithEvents lbltxtSaldoTag As Windows.Forms.Label
    Friend WithEvents lbltxtQtdeLiberada As Windows.Forms.Label
    Friend WithEvents TimerAviso As Windows.Forms.Timer
    Friend WithEvents FaçaUmaAnalizeTecnicaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnDadosUsuario As Windows.Forms.Button
    Friend WithEvents TabControlOS As Windows.Forms.TabControl
    Friend WithEvents TabPage2 As Windows.Forms.TabPage
    Friend WithEvents TabPage3 As Windows.Forms.TabPage
    Friend WithEvents DGVListaMaterialSWMateriais As Windows.Forms.DataGridView
    Friend WithEvents TabPage4 As Windows.Forms.TabPage
    Friend WithEvents dgvPlanejamentoProjetista As Windows.Forms.DataGridView
    Friend WithEvents TimerdgvPlanejamentoProjetista As Windows.Forms.Timer
    Friend WithEvents mnuTimerdgvPlanejamentoProjetista As Windows.Forms.ContextMenuStrip
    Friend WithEvents MarcarIncioDaExecuçãoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents MarcarFinalizaçãoDoProjetoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents Label18 As Windows.Forms.Label
    Friend WithEvents ToolStripTextBox1 As Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator29 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton6 As Windows.Forms.ToolStripButton
    Friend WithEvents tpbOrdemServico As Windows.Forms.TabPage
    Friend WithEvents Label19 As Windows.Forms.Label
    Friend WithEvents TXTTag As Windows.Forms.TextBox
    Friend WithEvents TXTPROJETO As Windows.Forms.TextBox
    Friend WithEvents Label31 As Windows.Forms.Label
    Friend WithEvents txtOS As Windows.Forms.TextBox
    Friend WithEvents Label32 As Windows.Forms.Label
    Friend WithEvents dgvOrdemservico As Windows.Forms.DataGridView
    Friend WithEvents TimerOrdemSevico As Windows.Forms.Timer
    Friend WithEvents TimerdgvOrdemservico As Windows.Forms.Timer
    Friend WithEvents chkMostraProjetoTagsFinalizadas As Windows.Forms.CheckBox
    Friend WithEvents Label33 As Windows.Forms.Label
    Friend WithEvents chkProjetista As Windows.Forms.CheckBox
    Friend WithEvents ToolStripSeparator30 As Windows.Forms.ToolStripSeparator
    Friend WithEvents InserirMaterialPeloFAPToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarListaDeMaterialNoFAPToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator31 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ProgresseBarPrincipal As Windows.Forms.ToolStripProgressBar
    Friend WithEvents lblResumo As Windows.Forms.ToolStripLabel
    Friend WithEvents LocarCadasttroDeDesenhoPeloNumweroDoDocumentoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtPesqTagProjetista As Windows.Forms.TextBox
    Friend WithEvents Label39 As Windows.Forms.Label
    Friend WithEvents txtPesqProjetoProjetista As Windows.Forms.TextBox
    Friend WithEvents Label34 As Windows.Forms.Label
    Friend WithEvents btnAtualizarDadosOrdemServicoItens As Windows.Forms.Button
    Friend WithEvents dgvEng As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents dgvEngpro As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents dgvcaminhoPDF As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents BuscarListaDePeçasAvulçasToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents InserirNumeroOPToolStripMenuItem As Windows.Forms.ToolStripMenuItem
End Class
