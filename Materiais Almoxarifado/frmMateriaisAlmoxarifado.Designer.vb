<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMateriaisAlmoxarifado
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMateriaisAlmoxarifado))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPesoMaterial = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFatorUtilizacao = New System.Windows.Forms.TextBox()
        Me.txtLarguram2 = New System.Windows.Forms.TextBox()
        Me.txtComprimentom2 = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.TxtPesqJuridico = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TxtPesqDesc3 = New System.Windows.Forms.TextBox()
        Me.TxtPesqDesc2 = New System.Windows.Forms.TextBox()
        Me.TxtPesqDesc1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtPesqCod = New System.Windows.Forms.TextBox()
        Me.dgvMaterial = New System.Windows.Forms.DataGridView()
        Me.mnudgvMaterial = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimerDgvMaterial = New System.Windows.Forms.Timer(Me.components)
        Me.gpbChapas = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtEspessura = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnCalcular = New System.Windows.Forms.Button()
        Me.btnCalculadora = New System.Windows.Forms.Button()
        Me.btnCalculaPeso = New System.Windows.Forms.Button()
        Me.txtValorCalculado = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPesoCalculado = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.mnuimagens = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BuscarImagensNoGoogleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalvarAImagemComoReferenciaDoMaterialToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarImagemNaBibliotecaDoSistemaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DGVMontaPeca = New System.Windows.Forms.DataGridView()
        Me.mnuDGVMontaPeca = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExcluirALinhaSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimerMontaPeca = New System.Windows.Forms.Timer(Me.components)
        Me.mnuPrincipal = New System.Windows.Forms.MenuStrip()
        Me.BuscarMateriaisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProtheusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OmieToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MegaSeniorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblvICMS = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblvIPI = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblPercICMS = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblPercIPI = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblVALOR = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblPeso = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblUnidade = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblDescDetal = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblCodigoJuridicoMat = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblCodMatFabricante = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblNumeroRP = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtNumeroEPR = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnatualizar = New System.Windows.Forms.Button()
        Me.lblvICMSCalculadoTotal = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.lblvIPICalculadoTotal = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblVALORCalculadoTotal = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.lblPesoCalculadoTotal = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblQtde = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.lblvICMSCalculado = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.lblvIPICalculado = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lblPercICMSCalculado = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.lblPercIPICalculado = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.lblVALORCalculado = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblPesoCalculado = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.cboUnidadeUtilizacao = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.btnAssociarMaterialM2 = New System.Windows.Forms.Button()
        CType(Me.dgvMaterial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnudgvMaterial.SuspendLayout()
        Me.gpbChapas.SuspendLayout()
        Me.mnuimagens.SuspendLayout()
        CType(Me.DGVMontaPeca, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuDGVMontaPeca.SuspendLayout()
        Me.mnuPrincipal.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(53, 127)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(118, 16)
        Me.Label2.TabIndex = 91
        Me.Label2.Text = "Peso Solid Works:"
        '
        'txtPesoMaterial
        '
        Me.txtPesoMaterial.Enabled = False
        Me.txtPesoMaterial.Location = New System.Drawing.Point(183, 122)
        Me.txtPesoMaterial.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtPesoMaterial.Name = "txtPesoMaterial"
        Me.txtPesoMaterial.Size = New System.Drawing.Size(152, 22)
        Me.txtPesoMaterial.TabIndex = 90
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(373, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 16)
        Me.Label1.TabIndex = 89
        Me.Label1.Text = "Taxa de Utilização"
        '
        'txtFatorUtilizacao
        '
        Me.txtFatorUtilizacao.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFatorUtilizacao.Location = New System.Drawing.Point(374, 118)
        Me.txtFatorUtilizacao.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtFatorUtilizacao.Name = "txtFatorUtilizacao"
        Me.txtFatorUtilizacao.Size = New System.Drawing.Size(138, 22)
        Me.txtFatorUtilizacao.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtFatorUtilizacao, resources.GetString("txtFatorUtilizacao.ToolTip"))
        '
        'txtLarguram2
        '
        Me.txtLarguram2.Enabled = False
        Me.txtLarguram2.Location = New System.Drawing.Point(183, 25)
        Me.txtLarguram2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtLarguram2.Name = "txtLarguram2"
        Me.txtLarguram2.Size = New System.Drawing.Size(152, 22)
        Me.txtLarguram2.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtLarguram2, "Informe o Novo Valor da Largura do Blank:")
        '
        'txtComprimentom2
        '
        Me.txtComprimentom2.Enabled = False
        Me.txtComprimentom2.Location = New System.Drawing.Point(183, 55)
        Me.txtComprimentom2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtComprimentom2.Name = "txtComprimentom2"
        Me.txtComprimentom2.Size = New System.Drawing.Size(152, 22)
        Me.txtComprimentom2.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.txtComprimentom2, "Informe o Novo Valor da Comprimento do Blank:")
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(57, 30)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(112, 16)
        Me.Label20.TabIndex = 1
        Me.Label20.Text = "Largura do Blank:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(24, 60)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(146, 16)
        Me.Label21.TabIndex = 2
        Me.Label21.Text = "Comprimento do Blank:"
        '
        'TxtPesqJuridico
        '
        Me.TxtPesqJuridico.BackColor = System.Drawing.Color.White
        Me.TxtPesqJuridico.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPesqJuridico.Location = New System.Drawing.Point(962, 309)
        Me.TxtPesqJuridico.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesqJuridico.Name = "TxtPesqJuridico"
        Me.TxtPesqJuridico.Size = New System.Drawing.Size(156, 22)
        Me.TxtPesqJuridico.TabIndex = 15
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(962, 288)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(77, 16)
        Me.Label17.TabIndex = 97
        Me.Label17.Text = "Fornecedor"
        '
        'TxtPesqDesc3
        '
        Me.TxtPesqDesc3.BackColor = System.Drawing.Color.White
        Me.TxtPesqDesc3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPesqDesc3.Location = New System.Drawing.Point(797, 309)
        Me.TxtPesqDesc3.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesqDesc3.Name = "TxtPesqDesc3"
        Me.TxtPesqDesc3.Size = New System.Drawing.Size(156, 22)
        Me.TxtPesqDesc3.TabIndex = 14
        '
        'TxtPesqDesc2
        '
        Me.TxtPesqDesc2.BackColor = System.Drawing.Color.White
        Me.TxtPesqDesc2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPesqDesc2.Location = New System.Drawing.Point(632, 309)
        Me.TxtPesqDesc2.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesqDesc2.Name = "TxtPesqDesc2"
        Me.TxtPesqDesc2.Size = New System.Drawing.Size(156, 22)
        Me.TxtPesqDesc2.TabIndex = 13
        '
        'TxtPesqDesc1
        '
        Me.TxtPesqDesc1.BackColor = System.Drawing.Color.White
        Me.TxtPesqDesc1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPesqDesc1.Location = New System.Drawing.Point(466, 309)
        Me.TxtPesqDesc1.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesqDesc1.Name = "TxtPesqDesc1"
        Me.TxtPesqDesc1.Size = New System.Drawing.Size(156, 22)
        Me.TxtPesqDesc1.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(465, 288)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 16)
        Me.Label3.TabIndex = 91
        Me.Label3.Text = "Descrição"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(5, 288)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 16)
        Me.Label6.TabIndex = 90
        Me.Label6.Text = "Código"
        '
        'TxtPesqCod
        '
        Me.TxtPesqCod.BackColor = System.Drawing.Color.White
        Me.TxtPesqCod.Location = New System.Drawing.Point(4, 309)
        Me.TxtPesqCod.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesqCod.Name = "TxtPesqCod"
        Me.TxtPesqCod.Size = New System.Drawing.Size(284, 22)
        Me.TxtPesqCod.TabIndex = 10
        '
        'dgvMaterial
        '
        Me.dgvMaterial.AllowUserToAddRows = False
        Me.dgvMaterial.AllowUserToDeleteRows = False
        Me.dgvMaterial.AllowUserToOrderColumns = True
        Me.dgvMaterial.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMaterial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.dgvMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMaterial.Location = New System.Drawing.Point(4, 341)
        Me.dgvMaterial.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvMaterial.Name = "dgvMaterial"
        Me.dgvMaterial.ReadOnly = True
        Me.dgvMaterial.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.dgvMaterial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMaterial.Size = New System.Drawing.Size(1504, 167)
        Me.dgvMaterial.TabIndex = 88
        '
        'mnudgvMaterial
        '
        Me.mnudgvMaterial.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnudgvMaterial.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.mnudgvMaterial.Name = "mnuDGVMontaPeca"
        Me.mnudgvMaterial.Size = New System.Drawing.Size(262, 30)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Image = Global.SwLynx_4._1.My.Resources.Resources.excluir
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(261, 26)
        Me.ToolStripMenuItem1.Text = "Excluir a Linha Selecionada"
        '
        'TimerDgvMaterial
        '
        Me.TimerDgvMaterial.Interval = 500
        '
        'gpbChapas
        '
        Me.gpbChapas.BackColor = System.Drawing.SystemColors.Info
        Me.gpbChapas.Controls.Add(Me.Label4)
        Me.gpbChapas.Controls.Add(Me.txtEspessura)
        Me.gpbChapas.Controls.Add(Me.txtLarguram2)
        Me.gpbChapas.Controls.Add(Me.Label21)
        Me.gpbChapas.Controls.Add(Me.Label20)
        Me.gpbChapas.Controls.Add(Me.txtComprimentom2)
        Me.gpbChapas.Controls.Add(Me.Label2)
        Me.gpbChapas.Controls.Add(Me.txtPesoMaterial)
        Me.gpbChapas.Location = New System.Drawing.Point(11, 87)
        Me.gpbChapas.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gpbChapas.Name = "gpbChapas"
        Me.gpbChapas.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gpbChapas.Size = New System.Drawing.Size(357, 153)
        Me.gpbChapas.TabIndex = 99
        Me.gpbChapas.TabStop = False
        Me.gpbChapas.Text = "Dados Coletados do SolidWorks"
        Me.ToolTip1.SetToolTip(Me.gpbChapas, resources.GetString("gpbChapas.ToolTip"))
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 91)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 16)
        Me.Label4.TabIndex = 92
        Me.Label4.Text = "Espessura Blank:"
        '
        'txtEspessura
        '
        Me.txtEspessura.Enabled = False
        Me.txtEspessura.Location = New System.Drawing.Point(183, 86)
        Me.txtEspessura.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtEspessura.Name = "txtEspessura"
        Me.txtEspessura.Size = New System.Drawing.Size(152, 22)
        Me.txtEspessura.TabIndex = 93
        Me.ToolTip1.SetToolTip(Me.txtEspessura, "Informe o Novo Valor da Comprimento do Blank:")
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip1.ToolTipTitle = "Dicas de Uso:"
        '
        'btnCalcular
        '
        Me.btnCalcular.Location = New System.Drawing.Point(676, 85)
        Me.btnCalcular.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnCalcular.Name = "btnCalcular"
        Me.btnCalcular.Size = New System.Drawing.Size(49, 153)
        Me.btnCalcular.TabIndex = 117
        Me.btnCalcular.Text = ">>"
        Me.btnCalcular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnCalcular, "Clique aqui para recalcular a taxa de utilização do material.")
        Me.btnCalcular.UseVisualStyleBackColor = True
        '
        'btnCalculadora
        '
        Me.btnCalculadora.Image = Global.SwLynx_4._1.My.Resources.Resources.calculadora
        Me.btnCalculadora.Location = New System.Drawing.Point(528, 148)
        Me.btnCalculadora.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnCalculadora.Name = "btnCalculadora"
        Me.btnCalculadora.Size = New System.Drawing.Size(137, 53)
        Me.btnCalculadora.TabIndex = 105
        Me.btnCalculadora.Text = "Calculadora"
        Me.btnCalculadora.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnCalculadora, "Clique aqui para recalcular a taxa de utilização do material.")
        Me.btnCalculadora.UseVisualStyleBackColor = True
        '
        'btnCalculaPeso
        '
        Me.btnCalculaPeso.Image = Global.SwLynx_4._1.My.Resources.Resources.calculadora
        Me.btnCalculaPeso.Location = New System.Drawing.Point(374, 147)
        Me.btnCalculaPeso.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnCalculaPeso.Name = "btnCalculaPeso"
        Me.btnCalculaPeso.Size = New System.Drawing.Size(137, 53)
        Me.btnCalculaPeso.TabIndex = 121
        Me.btnCalculaPeso.Text = "Calcula Peso"
        Me.btnCalculaPeso.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnCalculaPeso, "Clique aqui para recalcular a taxa de utilização do material.")
        Me.btnCalculaPeso.UseVisualStyleBackColor = True
        '
        'txtValorCalculado
        '
        Me.txtValorCalculado.Location = New System.Drawing.Point(1126, 309)
        Me.txtValorCalculado.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtValorCalculado.Name = "txtValorCalculado"
        Me.txtValorCalculado.Size = New System.Drawing.Size(89, 22)
        Me.txtValorCalculado.TabIndex = 16
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(1122, 288)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 16)
        Me.Label5.TabIndex = 101
        Me.Label5.Text = "Valor Material:"
        '
        'txtPesoCalculado
        '
        Me.txtPesoCalculado.Location = New System.Drawing.Point(1230, 309)
        Me.txtPesoCalculado.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtPesoCalculado.Name = "txtPesoCalculado"
        Me.txtPesoCalculado.Size = New System.Drawing.Size(89, 22)
        Me.txtPesoCalculado.TabIndex = 102
        Me.txtPesoCalculado.Text = "17"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(1227, 287)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(93, 16)
        Me.Label7.TabIndex = 103
        Me.Label7.Text = "Peso Material:"
        '
        'mnuimagens
        '
        Me.mnuimagens.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuimagens.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BuscarImagensNoGoogleToolStripMenuItem, Me.SalvarAImagemComoReferenciaDoMaterialToolStripMenuItem, Me.BuscarImagemNaBibliotecaDoSistemaToolStripMenuItem})
        Me.mnuimagens.Name = "mnuimagens"
        Me.mnuimagens.Size = New System.Drawing.Size(387, 76)
        '
        'BuscarImagensNoGoogleToolStripMenuItem
        '
        Me.BuscarImagensNoGoogleToolStripMenuItem.Name = "BuscarImagensNoGoogleToolStripMenuItem"
        Me.BuscarImagensNoGoogleToolStripMenuItem.Size = New System.Drawing.Size(386, 24)
        Me.BuscarImagensNoGoogleToolStripMenuItem.Text = "Buscar Imagens no Google"
        '
        'SalvarAImagemComoReferenciaDoMaterialToolStripMenuItem
        '
        Me.SalvarAImagemComoReferenciaDoMaterialToolStripMenuItem.Name = "SalvarAImagemComoReferenciaDoMaterialToolStripMenuItem"
        Me.SalvarAImagemComoReferenciaDoMaterialToolStripMenuItem.Size = New System.Drawing.Size(386, 24)
        Me.SalvarAImagemComoReferenciaDoMaterialToolStripMenuItem.Text = "Salvar a Imagem como Referencia do Material"
        '
        'BuscarImagemNaBibliotecaDoSistemaToolStripMenuItem
        '
        Me.BuscarImagemNaBibliotecaDoSistemaToolStripMenuItem.Name = "BuscarImagemNaBibliotecaDoSistemaToolStripMenuItem"
        Me.BuscarImagemNaBibliotecaDoSistemaToolStripMenuItem.Size = New System.Drawing.Size(386, 24)
        Me.BuscarImagemNaBibliotecaDoSistemaToolStripMenuItem.Text = "Buscar Imagem na Biblioteca do Sistema"
        '
        'DGVMontaPeca
        '
        Me.DGVMontaPeca.AllowUserToAddRows = False
        Me.DGVMontaPeca.AllowUserToDeleteRows = False
        Me.DGVMontaPeca.AllowUserToOrderColumns = True
        Me.DGVMontaPeca.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGVMontaPeca.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.DGVMontaPeca.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        Me.DGVMontaPeca.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVMontaPeca.ContextMenuStrip = Me.mnuDGVMontaPeca
        Me.DGVMontaPeca.Location = New System.Drawing.Point(9, 100)
        Me.DGVMontaPeca.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGVMontaPeca.Name = "DGVMontaPeca"
        Me.DGVMontaPeca.ReadOnly = True
        Me.DGVMontaPeca.RowHeadersWidth = 51
        Me.DGVMontaPeca.RowTemplate.Height = 24
        Me.DGVMontaPeca.Size = New System.Drawing.Size(1499, 403)
        Me.DGVMontaPeca.TabIndex = 106
        '
        'mnuDGVMontaPeca
        '
        Me.mnuDGVMontaPeca.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuDGVMontaPeca.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcluirALinhaSelecionadaToolStripMenuItem})
        Me.mnuDGVMontaPeca.Name = "mnuDGVMontaPeca"
        Me.mnuDGVMontaPeca.Size = New System.Drawing.Size(262, 30)
        '
        'ExcluirALinhaSelecionadaToolStripMenuItem
        '
        Me.ExcluirALinhaSelecionadaToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.excluir
        Me.ExcluirALinhaSelecionadaToolStripMenuItem.Name = "ExcluirALinhaSelecionadaToolStripMenuItem"
        Me.ExcluirALinhaSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(261, 26)
        Me.ExcluirALinhaSelecionadaToolStripMenuItem.Text = "Excluir a Linha Selecionada"
        '
        'TimerMontaPeca
        '
        '
        'mnuPrincipal
        '
        Me.mnuPrincipal.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuPrincipal.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BuscarMateriaisToolStripMenuItem})
        Me.mnuPrincipal.Location = New System.Drawing.Point(0, 0)
        Me.mnuPrincipal.Name = "mnuPrincipal"
        Me.mnuPrincipal.Size = New System.Drawing.Size(1523, 28)
        Me.mnuPrincipal.TabIndex = 107
        Me.mnuPrincipal.Text = "MenuStrip1"
        '
        'BuscarMateriaisToolStripMenuItem
        '
        Me.BuscarMateriaisToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProtheusToolStripMenuItem, Me.OmieToolStripMenuItem, Me.MegaSeniorToolStripMenuItem})
        Me.BuscarMateriaisToolStripMenuItem.Name = "BuscarMateriaisToolStripMenuItem"
        Me.BuscarMateriaisToolStripMenuItem.Size = New System.Drawing.Size(131, 24)
        Me.BuscarMateriaisToolStripMenuItem.Text = "Buscar Materiais"
        '
        'ProtheusToolStripMenuItem
        '
        Me.ProtheusToolStripMenuItem.Enabled = False
        Me.ProtheusToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.downloadprotheus
        Me.ProtheusToolStripMenuItem.Name = "ProtheusToolStripMenuItem"
        Me.ProtheusToolStripMenuItem.Size = New System.Drawing.Size(186, 26)
        Me.ProtheusToolStripMenuItem.Text = "Protheus"
        '
        'OmieToolStripMenuItem
        '
        Me.OmieToolStripMenuItem.Enabled = False
        Me.OmieToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.download_omie
        Me.OmieToolStripMenuItem.Name = "OmieToolStripMenuItem"
        Me.OmieToolStripMenuItem.Size = New System.Drawing.Size(186, 26)
        Me.OmieToolStripMenuItem.Text = "Omie"
        '
        'MegaSeniorToolStripMenuItem
        '
        Me.MegaSeniorToolStripMenuItem.Enabled = False
        Me.MegaSeniorToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.download_mega_senior
        Me.MegaSeniorToolStripMenuItem.Name = "MegaSeniorToolStripMenuItem"
        Me.MegaSeniorToolStripMenuItem.Size = New System.Drawing.Size(186, 26)
        Me.MegaSeniorToolStripMenuItem.Text = "Mega - Senior"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.lblvICMS)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.lblvIPI)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.lblPercICMS)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.lblPercIPI)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.lblVALOR)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.lblPeso)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.lblUnidade)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.lblDescDetal)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.lblCodigoJuridicoMat)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.lblCodMatFabricante)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.lblNumeroRP)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 4)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(1508, 266)
        Me.GroupBox1.TabIndex = 114
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Dados Produto Selecionado"
        '
        'lblvICMS
        '
        Me.lblvICMS.BackColor = System.Drawing.Color.White
        Me.lblvICMS.Location = New System.Drawing.Point(693, 143)
        Me.lblvICMS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblvICMS.Name = "lblvICMS"
        Me.lblvICMS.Size = New System.Drawing.Size(127, 22)
        Me.lblvICMS.TabIndex = 128
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(693, 124)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(66, 16)
        Me.Label18.TabIndex = 127
        Me.Label18.Text = "R$_ICMS:"
        '
        'lblvIPI
        '
        Me.lblvIPI.BackColor = System.Drawing.Color.White
        Me.lblvIPI.Location = New System.Drawing.Point(828, 143)
        Me.lblvIPI.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblvIPI.Name = "lblvIPI"
        Me.lblvIPI.Size = New System.Drawing.Size(127, 22)
        Me.lblvIPI.TabIndex = 126
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(828, 124)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 16)
        Me.Label8.TabIndex = 125
        Me.Label8.Text = "R$_IPI:"
        '
        'lblPercICMS
        '
        Me.lblPercICMS.BackColor = System.Drawing.Color.White
        Me.lblPercICMS.Location = New System.Drawing.Point(693, 94)
        Me.lblPercICMS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPercICMS.Name = "lblPercICMS"
        Me.lblPercICMS.Size = New System.Drawing.Size(127, 22)
        Me.lblPercICMS.TabIndex = 124
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(693, 75)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(54, 16)
        Me.Label10.TabIndex = 123
        Me.Label10.Text = "%ICMS:"
        '
        'lblPercIPI
        '
        Me.lblPercIPI.BackColor = System.Drawing.Color.White
        Me.lblPercIPI.Location = New System.Drawing.Point(828, 94)
        Me.lblPercIPI.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPercIPI.Name = "lblPercIPI"
        Me.lblPercIPI.Size = New System.Drawing.Size(127, 22)
        Me.lblPercIPI.TabIndex = 122
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(828, 75)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(37, 16)
        Me.Label16.TabIndex = 121
        Me.Label16.Text = "%IPI:"
        '
        'lblVALOR
        '
        Me.lblVALOR.BackColor = System.Drawing.Color.White
        Me.lblVALOR.Location = New System.Drawing.Point(559, 94)
        Me.lblVALOR.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVALOR.Name = "lblVALOR"
        Me.lblVALOR.Size = New System.Drawing.Size(127, 22)
        Me.lblVALOR.TabIndex = 120
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(559, 75)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(26, 16)
        Me.Label15.TabIndex = 119
        Me.Label15.Text = "Vlr:"
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.ContextMenuStrip = Me.mnuimagens
        Me.PictureBox1.Location = New System.Drawing.Point(1223, 12)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(273, 170)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 104
        Me.PictureBox1.TabStop = False
        '
        'lblPeso
        '
        Me.lblPeso.BackColor = System.Drawing.Color.White
        Me.lblPeso.Location = New System.Drawing.Point(480, 94)
        Me.lblPeso.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPeso.Name = "lblPeso"
        Me.lblPeso.Size = New System.Drawing.Size(71, 22)
        Me.lblPeso.TabIndex = 118
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(480, 75)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(42, 16)
        Me.Label13.TabIndex = 117
        Me.Label13.Text = "Peso:"
        '
        'lblUnidade
        '
        Me.lblUnidade.BackColor = System.Drawing.Color.White
        Me.lblUnidade.Location = New System.Drawing.Point(401, 94)
        Me.lblUnidade.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUnidade.Name = "lblUnidade"
        Me.lblUnidade.Size = New System.Drawing.Size(71, 22)
        Me.lblUnidade.TabIndex = 116
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(401, 75)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(33, 16)
        Me.Label11.TabIndex = 115
        Me.Label11.Text = "Unit."
        '
        'lblDescDetal
        '
        Me.lblDescDetal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDescDetal.BackColor = System.Drawing.Color.White
        Me.lblDescDetal.Location = New System.Drawing.Point(12, 193)
        Me.lblDescDetal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDescDetal.Name = "lblDescDetal"
        Me.lblDescDetal.Size = New System.Drawing.Size(1484, 63)
        Me.lblDescDetal.TabIndex = 112
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(12, 175)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 16)
        Me.Label12.TabIndex = 111
        Me.Label12.Text = "Descrição:"
        '
        'lblCodigoJuridicoMat
        '
        Me.lblCodigoJuridicoMat.BackColor = System.Drawing.Color.White
        Me.lblCodigoJuridicoMat.Location = New System.Drawing.Point(8, 142)
        Me.lblCodigoJuridicoMat.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCodigoJuridicoMat.Name = "lblCodigoJuridicoMat"
        Me.lblCodigoJuridicoMat.Size = New System.Drawing.Size(447, 22)
        Me.lblCodigoJuridicoMat.TabIndex = 110
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(8, 123)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(74, 16)
        Me.Label19.TabIndex = 109
        Me.Label19.Text = "Fabricante:"
        '
        'lblCodMatFabricante
        '
        Me.lblCodMatFabricante.BackColor = System.Drawing.Color.White
        Me.lblCodMatFabricante.Location = New System.Drawing.Point(11, 94)
        Me.lblCodMatFabricante.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCodMatFabricante.Name = "lblCodMatFabricante"
        Me.lblCodMatFabricante.Size = New System.Drawing.Size(379, 22)
        Me.lblCodMatFabricante.TabIndex = 108
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(9, 75)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(105, 16)
        Me.Label22.TabIndex = 107
        Me.Label22.Text = "Cod. Fabricante:"
        '
        'lblNumeroRP
        '
        Me.lblNumeroRP.BackColor = System.Drawing.Color.White
        Me.lblNumeroRP.Location = New System.Drawing.Point(12, 44)
        Me.lblNumeroRP.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNumeroRP.Name = "lblNumeroRP"
        Me.lblNumeroRP.Size = New System.Drawing.Size(377, 22)
        Me.lblNumeroRP.TabIndex = 106
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(11, 26)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(104, 16)
        Me.Label23.TabIndex = 105
        Me.Label23.Text = "Cod. Interno/RP:"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(-1, 244)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1523, 547)
        Me.TabControl1.TabIndex = 115
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label14)
        Me.TabPage1.Controls.Add(Me.txtNumeroEPR)
        Me.TabPage1.Controls.Add(Me.dgvMaterial)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.TxtPesqCod)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TxtPesqDesc1)
        Me.TabPage1.Controls.Add(Me.TxtPesqDesc2)
        Me.TabPage1.Controls.Add(Me.TxtPesqDesc3)
        Me.TabPage1.Controls.Add(Me.Label17)
        Me.TabPage1.Controls.Add(Me.TxtPesqJuridico)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.txtValorCalculado)
        Me.TabPage1.Controls.Add(Me.txtPesoCalculado)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Size = New System.Drawing.Size(1515, 518)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Lista de Materiais"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(295, 288)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 16)
        Me.Label14.TabIndex = 115
        Me.Label14.Text = "Numero RP:"
        '
        'txtNumeroEPR
        '
        Me.txtNumeroEPR.BackColor = System.Drawing.Color.White
        Me.txtNumeroEPR.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNumeroEPR.Location = New System.Drawing.Point(296, 309)
        Me.txtNumeroEPR.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNumeroEPR.Name = "txtNumeroEPR"
        Me.txtNumeroEPR.Size = New System.Drawing.Size(156, 22)
        Me.txtNumeroEPR.TabIndex = 11
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Controls.Add(Me.DGVMontaPeca)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage2.Size = New System.Drawing.Size(1515, 518)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Produto Montado"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GroupBox3.Controls.Add(Me.btnatualizar)
        Me.GroupBox3.Controls.Add(Me.lblvICMSCalculadoTotal)
        Me.GroupBox3.Controls.Add(Me.Label25)
        Me.GroupBox3.Controls.Add(Me.lblvIPICalculadoTotal)
        Me.GroupBox3.Controls.Add(Me.Label29)
        Me.GroupBox3.Controls.Add(Me.lblVALORCalculadoTotal)
        Me.GroupBox3.Controls.Add(Me.Label38)
        Me.GroupBox3.Controls.Add(Me.lblPesoCalculadoTotal)
        Me.GroupBox3.Controls.Add(Me.Label40)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 7)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(1500, 87)
        Me.GroupBox3.TabIndex = 117
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Dados Calculados:"
        '
        'btnatualizar
        '
        Me.btnatualizar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnatualizar.Image = Global.SwLynx_4._1.My.Resources.Resources.atualizar
        Me.btnatualizar.Location = New System.Drawing.Point(1384, 20)
        Me.btnatualizar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnatualizar.Name = "btnatualizar"
        Me.btnatualizar.Size = New System.Drawing.Size(99, 48)
        Me.btnatualizar.TabIndex = 137
        Me.btnatualizar.UseVisualStyleBackColor = True
        '
        'lblvICMSCalculadoTotal
        '
        Me.lblvICMSCalculadoTotal.BackColor = System.Drawing.Color.White
        Me.lblvICMSCalculadoTotal.Location = New System.Drawing.Point(232, 46)
        Me.lblvICMSCalculadoTotal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblvICMSCalculadoTotal.Name = "lblvICMSCalculadoTotal"
        Me.lblvICMSCalculadoTotal.Size = New System.Drawing.Size(127, 22)
        Me.lblvICMSCalculadoTotal.TabIndex = 136
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(232, 27)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(66, 16)
        Me.Label25.TabIndex = 135
        Me.Label25.Text = "R$_ICMS:"
        '
        'lblvIPICalculadoTotal
        '
        Me.lblvIPICalculadoTotal.BackColor = System.Drawing.Color.White
        Me.lblvIPICalculadoTotal.Location = New System.Drawing.Point(367, 46)
        Me.lblvIPICalculadoTotal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblvIPICalculadoTotal.Name = "lblvIPICalculadoTotal"
        Me.lblvIPICalculadoTotal.Size = New System.Drawing.Size(127, 22)
        Me.lblvIPICalculadoTotal.TabIndex = 134
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(367, 27)
        Me.Label29.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(49, 16)
        Me.Label29.TabIndex = 133
        Me.Label29.Text = "R$_IPI:"
        '
        'lblVALORCalculadoTotal
        '
        Me.lblVALORCalculadoTotal.BackColor = System.Drawing.Color.White
        Me.lblVALORCalculadoTotal.Location = New System.Drawing.Point(97, 46)
        Me.lblVALORCalculadoTotal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVALORCalculadoTotal.Name = "lblVALORCalculadoTotal"
        Me.lblVALORCalculadoTotal.Size = New System.Drawing.Size(127, 22)
        Me.lblVALORCalculadoTotal.TabIndex = 124
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(97, 27)
        Me.Label38.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(26, 16)
        Me.Label38.TabIndex = 123
        Me.Label38.Text = "Vlr:"
        '
        'lblPesoCalculadoTotal
        '
        Me.lblPesoCalculadoTotal.BackColor = System.Drawing.Color.White
        Me.lblPesoCalculadoTotal.Location = New System.Drawing.Point(19, 46)
        Me.lblPesoCalculadoTotal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPesoCalculadoTotal.Name = "lblPesoCalculadoTotal"
        Me.lblPesoCalculadoTotal.Size = New System.Drawing.Size(71, 22)
        Me.lblPesoCalculadoTotal.TabIndex = 122
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(19, 27)
        Me.Label40.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(42, 16)
        Me.Label40.TabIndex = 121
        Me.Label40.Text = "Peso:"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GroupBox2.Controls.Add(Me.lblQtde)
        Me.GroupBox2.Controls.Add(Me.Label27)
        Me.GroupBox2.Controls.Add(Me.lblvICMSCalculado)
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Controls.Add(Me.lblvIPICalculado)
        Me.GroupBox2.Controls.Add(Me.Label30)
        Me.GroupBox2.Controls.Add(Me.lblPercICMSCalculado)
        Me.GroupBox2.Controls.Add(Me.Label32)
        Me.GroupBox2.Controls.Add(Me.lblPercIPICalculado)
        Me.GroupBox2.Controls.Add(Me.Label34)
        Me.GroupBox2.Controls.Add(Me.lblVALORCalculado)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.lblPesoCalculado)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Location = New System.Drawing.Point(732, 87)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(570, 153)
        Me.GroupBox2.TabIndex = 116
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Dados Calculados:"
        '
        'lblQtde
        '
        Me.lblQtde.BackColor = System.Drawing.Color.White
        Me.lblQtde.Location = New System.Drawing.Point(28, 66)
        Me.lblQtde.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblQtde.Name = "lblQtde"
        Me.lblQtde.Size = New System.Drawing.Size(71, 22)
        Me.lblQtde.TabIndex = 138
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(28, 47)
        Me.Label27.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(39, 16)
        Me.Label27.TabIndex = 137
        Me.Label27.Text = "Qtde:"
        '
        'lblvICMSCalculado
        '
        Me.lblvICMSCalculado.BackColor = System.Drawing.Color.White
        Me.lblvICMSCalculado.Location = New System.Drawing.Point(295, 104)
        Me.lblvICMSCalculado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblvICMSCalculado.Name = "lblvICMSCalculado"
        Me.lblvICMSCalculado.Size = New System.Drawing.Size(127, 22)
        Me.lblvICMSCalculado.TabIndex = 136
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(295, 85)
        Me.Label28.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(66, 16)
        Me.Label28.TabIndex = 135
        Me.Label28.Text = "R$_ICMS:"
        '
        'lblvIPICalculado
        '
        Me.lblvIPICalculado.BackColor = System.Drawing.Color.White
        Me.lblvIPICalculado.Location = New System.Drawing.Point(430, 104)
        Me.lblvIPICalculado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblvIPICalculado.Name = "lblvIPICalculado"
        Me.lblvIPICalculado.Size = New System.Drawing.Size(127, 22)
        Me.lblvIPICalculado.TabIndex = 134
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(430, 85)
        Me.Label30.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(49, 16)
        Me.Label30.TabIndex = 133
        Me.Label30.Text = "R$_IPI:"
        '
        'lblPercICMSCalculado
        '
        Me.lblPercICMSCalculado.BackColor = System.Drawing.Color.White
        Me.lblPercICMSCalculado.Location = New System.Drawing.Point(295, 55)
        Me.lblPercICMSCalculado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPercICMSCalculado.Name = "lblPercICMSCalculado"
        Me.lblPercICMSCalculado.Size = New System.Drawing.Size(127, 22)
        Me.lblPercICMSCalculado.TabIndex = 132
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(295, 36)
        Me.Label32.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(54, 16)
        Me.Label32.TabIndex = 131
        Me.Label32.Text = "%ICMS:"
        '
        'lblPercIPICalculado
        '
        Me.lblPercIPICalculado.BackColor = System.Drawing.Color.White
        Me.lblPercIPICalculado.Location = New System.Drawing.Point(430, 55)
        Me.lblPercIPICalculado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPercIPICalculado.Name = "lblPercIPICalculado"
        Me.lblPercIPICalculado.Size = New System.Drawing.Size(127, 22)
        Me.lblPercIPICalculado.TabIndex = 130
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(430, 36)
        Me.Label34.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(37, 16)
        Me.Label34.TabIndex = 129
        Me.Label34.Text = "%IPI:"
        '
        'lblVALORCalculado
        '
        Me.lblVALORCalculado.BackColor = System.Drawing.Color.White
        Me.lblVALORCalculado.Location = New System.Drawing.Point(160, 104)
        Me.lblVALORCalculado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVALORCalculado.Name = "lblVALORCalculado"
        Me.lblVALORCalculado.Size = New System.Drawing.Size(127, 22)
        Me.lblVALORCalculado.TabIndex = 124
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(160, 85)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(26, 16)
        Me.Label24.TabIndex = 123
        Me.Label24.Text = "Vlr:"
        '
        'lblPesoCalculado
        '
        Me.lblPesoCalculado.BackColor = System.Drawing.Color.White
        Me.lblPesoCalculado.Location = New System.Drawing.Point(160, 54)
        Me.lblPesoCalculado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPesoCalculado.Name = "lblPesoCalculado"
        Me.lblPesoCalculado.Size = New System.Drawing.Size(71, 22)
        Me.lblPesoCalculado.TabIndex = 122
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(160, 35)
        Me.Label26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(42, 16)
        Me.Label26.TabIndex = 121
        Me.Label26.Text = "Peso:"
        '
        'cboUnidadeUtilizacao
        '
        Me.cboUnidadeUtilizacao.FormattingEnabled = True
        Me.cboUnidadeUtilizacao.Location = New System.Drawing.Point(528, 118)
        Me.cboUnidadeUtilizacao.Margin = New System.Windows.Forms.Padding(4)
        Me.cboUnidadeUtilizacao.Name = "cboUnidadeUtilizacao"
        Me.cboUnidadeUtilizacao.Size = New System.Drawing.Size(137, 24)
        Me.cboUnidadeUtilizacao.TabIndex = 118
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(525, 96)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(140, 16)
        Me.Label9.TabIndex = 119
        Me.Label9.Text = "Unidade de Utilização"
        '
        'lblTitulo
        '
        Me.lblTitulo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTitulo.BackColor = System.Drawing.Color.Green
        Me.lblTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitulo.ForeColor = System.Drawing.Color.White
        Me.lblTitulo.Location = New System.Drawing.Point(0, 30)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(1521, 34)
        Me.lblTitulo.TabIndex = 120
        Me.lblTitulo.Text = "Taxa de Utilização"
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAssociarMaterialM2
        '
        Me.btnAssociarMaterialM2.Image = Global.SwLynx_4._1.My.Resources.Resources.material_escolar_32
        Me.btnAssociarMaterialM2.Location = New System.Drawing.Point(1333, 87)
        Me.btnAssociarMaterialM2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnAssociarMaterialM2.Name = "btnAssociarMaterialM2"
        Me.btnAssociarMaterialM2.Size = New System.Drawing.Size(177, 77)
        Me.btnAssociarMaterialM2.TabIndex = 88
        Me.btnAssociarMaterialM2.Text = "Associar Material"
        Me.btnAssociarMaterialM2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAssociarMaterialM2.UseVisualStyleBackColor = True
        '
        'frmMateriaisAlmoxarifado
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1523, 805)
        Me.Controls.Add(Me.btnCalculaPeso)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cboUnidadeUtilizacao)
        Me.Controls.Add(Me.btnCalcular)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnCalculadora)
        Me.Controls.Add(Me.gpbChapas)
        Me.Controls.Add(Me.btnAssociarMaterialM2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtFatorUtilizacao)
        Me.Controls.Add(Me.mnuPrincipal)
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.mnuPrincipal
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "frmMateriaisAlmoxarifado"
        Me.Text = "Materiais do Almoxarifado"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvMaterial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnudgvMaterial.ResumeLayout(False)
        Me.gpbChapas.ResumeLayout(False)
        Me.gpbChapas.PerformLayout()
        Me.mnuimagens.ResumeLayout(False)
        CType(Me.DGVMontaPeca, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuDGVMontaPeca.ResumeLayout(False)
        Me.mnuPrincipal.ResumeLayout(False)
        Me.mnuPrincipal.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnAssociarMaterialM2 As Windows.Forms.Button
    Friend WithEvents txtFatorUtilizacao As Windows.Forms.TextBox
    Friend WithEvents txtLarguram2 As Windows.Forms.TextBox
    Friend WithEvents txtComprimentom2 As Windows.Forms.TextBox
    Friend WithEvents Label20 As Windows.Forms.Label
    Friend WithEvents Label21 As Windows.Forms.Label
    Friend WithEvents TxtPesqJuridico As Windows.Forms.TextBox
    Friend WithEvents Label17 As Windows.Forms.Label
    Friend WithEvents TxtPesqDesc3 As Windows.Forms.TextBox
    Friend WithEvents TxtPesqDesc2 As Windows.Forms.TextBox
    Friend WithEvents TxtPesqDesc1 As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents TxtPesqCod As Windows.Forms.TextBox
    Friend WithEvents dgvMaterial As Windows.Forms.DataGridView
    Friend WithEvents TimerDgvMaterial As Windows.Forms.Timer
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents txtPesoMaterial As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents gpbChapas As Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents txtValorCalculado As Windows.Forms.TextBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents txtPesoCalculado As Windows.Forms.TextBox
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents PictureBox1 As Windows.Forms.PictureBox
    Friend WithEvents btnCalculadora As Windows.Forms.Button
    Friend WithEvents DGVMontaPeca As Windows.Forms.DataGridView
    Friend WithEvents TimerMontaPeca As Windows.Forms.Timer
    Friend WithEvents mnuPrincipal As Windows.Forms.MenuStrip
    Friend WithEvents BuscarMateriaisToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProtheusToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents OmieToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents MegaSeniorToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuimagens As Windows.Forms.ContextMenuStrip
    Friend WithEvents BuscarImagensNoGoogleToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalvarAImagemComoReferenciaDoMaterialToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarImagemNaBibliotecaDoSistemaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDGVMontaPeca As Windows.Forms.ContextMenuStrip
    Friend WithEvents ExcluirALinhaSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnudgvMaterial As Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents lblvICMS As Windows.Forms.Label
    Friend WithEvents Label18 As Windows.Forms.Label
    Friend WithEvents lblvIPI As Windows.Forms.Label
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents lblPercICMS As Windows.Forms.Label
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents lblPercIPI As Windows.Forms.Label
    Friend WithEvents Label16 As Windows.Forms.Label
    Friend WithEvents lblVALOR As Windows.Forms.Label
    Friend WithEvents Label15 As Windows.Forms.Label
    Friend WithEvents lblPeso As Windows.Forms.Label
    Friend WithEvents Label13 As Windows.Forms.Label
    Friend WithEvents lblUnidade As Windows.Forms.Label
    Friend WithEvents Label11 As Windows.Forms.Label
    Friend WithEvents lblDescDetal As Windows.Forms.Label
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents lblCodigoJuridicoMat As Windows.Forms.Label
    Friend WithEvents Label19 As Windows.Forms.Label
    Friend WithEvents lblCodMatFabricante As Windows.Forms.Label
    Friend WithEvents Label22 As Windows.Forms.Label
    Friend WithEvents lblNumeroRP As Windows.Forms.Label
    Friend WithEvents Label23 As Windows.Forms.Label
    Friend WithEvents TabControl1 As Windows.Forms.TabControl
    Friend WithEvents TabPage1 As Windows.Forms.TabPage
    Friend WithEvents TabPage2 As Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents lblvICMSCalculado As Windows.Forms.Label
    Friend WithEvents Label28 As Windows.Forms.Label
    Friend WithEvents lblvIPICalculado As Windows.Forms.Label
    Friend WithEvents Label30 As Windows.Forms.Label
    Friend WithEvents lblPercICMSCalculado As Windows.Forms.Label
    Friend WithEvents Label32 As Windows.Forms.Label
    Friend WithEvents lblPercIPICalculado As Windows.Forms.Label
    Friend WithEvents Label34 As Windows.Forms.Label
    Friend WithEvents lblVALORCalculado As Windows.Forms.Label
    Friend WithEvents Label24 As Windows.Forms.Label
    Friend WithEvents lblPesoCalculado As Windows.Forms.Label
    Friend WithEvents Label26 As Windows.Forms.Label
    Friend WithEvents btnCalcular As Windows.Forms.Button
    Friend WithEvents cboUnidadeUtilizacao As Windows.Forms.ComboBox
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents lblvICMSCalculadoTotal As Windows.Forms.Label
    Friend WithEvents Label25 As Windows.Forms.Label
    Friend WithEvents lblvIPICalculadoTotal As Windows.Forms.Label
    Friend WithEvents Label29 As Windows.Forms.Label
    Friend WithEvents lblVALORCalculadoTotal As Windows.Forms.Label
    Friend WithEvents Label38 As Windows.Forms.Label
    Friend WithEvents lblPesoCalculadoTotal As Windows.Forms.Label
    Friend WithEvents Label40 As Windows.Forms.Label
    Friend WithEvents lblTitulo As Windows.Forms.Label
    Friend WithEvents btnCalculaPeso As Windows.Forms.Button
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents txtEspessura As Windows.Forms.TextBox
    Friend WithEvents btnatualizar As Windows.Forms.Button
    Friend WithEvents lblQtde As Windows.Forms.Label
    Friend WithEvents Label27 As Windows.Forms.Label
    Friend WithEvents Label14 As Windows.Forms.Label
    Friend WithEvents txtNumeroEPR As Windows.Forms.TextBox
End Class
