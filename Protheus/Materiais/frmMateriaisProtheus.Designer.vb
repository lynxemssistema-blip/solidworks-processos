<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMateriaisProtheus
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.dgvMaterialProtheus = New System.Windows.Forms.DataGridView()
        Me.dgvSelecao = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TimerdgvMaterialProtheus = New System.Windows.Forms.Timer(Me.components)
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtStringExcluir = New System.Windows.Forms.TextBox()
        Me.chkTpoFiltro = New System.Windows.Forms.CheckBox()
        Me.txtPesqFabricante = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPesqCodProtheus = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnLimpar = New System.Windows.Forms.Button()
        Me.txtPesqTipo = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtPesqDescricao03 = New System.Windows.Forms.TextBox()
        Me.txtPesqDescricao02 = New System.Windows.Forms.TextBox()
        Me.txtPesqDescricao01 = New System.Windows.Forms.TextBox()
        Me.txtPesqCodFabricante = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblB1_PICM_Valor = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblB1_IPI_Valor = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblB1_PICM = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblB1_IPI = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblB1_UPRC = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblB1_PESO = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblB1_UM = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblB1_TIPO = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblB1_DESC = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblB1_ZFABRIC = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblB1_ZREF = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblCodProtheus = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.dgvMaterialProtheus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvMaterialProtheus
        '
        Me.dgvMaterialProtheus.AllowUserToAddRows = False
        Me.dgvMaterialProtheus.AllowUserToDeleteRows = False
        Me.dgvMaterialProtheus.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMaterialProtheus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.dgvMaterialProtheus.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.dgvMaterialProtheus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMaterialProtheus.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvSelecao})
        Me.dgvMaterialProtheus.Location = New System.Drawing.Point(10, 370)
        Me.dgvMaterialProtheus.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvMaterialProtheus.Name = "dgvMaterialProtheus"
        Me.dgvMaterialProtheus.ReadOnly = True
        Me.dgvMaterialProtheus.RowHeadersWidth = 51
        Me.dgvMaterialProtheus.RowTemplate.Height = 24
        Me.dgvMaterialProtheus.Size = New System.Drawing.Size(952, 372)
        Me.dgvMaterialProtheus.TabIndex = 11
        '
        'dgvSelecao
        '
        Me.dgvSelecao.Frozen = True
        Me.dgvSelecao.HeaderText = "dgvSelecao"
        Me.dgvSelecao.Name = "dgvSelecao"
        Me.dgvSelecao.ReadOnly = True
        Me.dgvSelecao.Width = 5
        '
        'TimerdgvMaterialProtheus
        '
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Enabled = False
        Me.Label9.Location = New System.Drawing.Point(424, 122)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(77, 13)
        Me.Label9.TabIndex = 110
        Me.Label9.Text = "String a Excluir"
        '
        'txtStringExcluir
        '
        Me.txtStringExcluir.BackColor = System.Drawing.Color.RosyBrown
        Me.txtStringExcluir.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtStringExcluir.Enabled = False
        Me.txtStringExcluir.Location = New System.Drawing.Point(507, 117)
        Me.txtStringExcluir.Name = "txtStringExcluir"
        Me.txtStringExcluir.Size = New System.Drawing.Size(80, 20)
        Me.txtStringExcluir.TabIndex = 109
        '
        'chkTpoFiltro
        '
        Me.chkTpoFiltro.AutoSize = True
        Me.chkTpoFiltro.Checked = True
        Me.chkTpoFiltro.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTpoFiltro.Enabled = False
        Me.chkTpoFiltro.Location = New System.Drawing.Point(222, 120)
        Me.chkTpoFiltro.Name = "chkTpoFiltro"
        Me.chkTpoFiltro.Size = New System.Drawing.Size(193, 17)
        Me.chkTpoFiltro.TabIndex = 108
        Me.chkTpoFiltro.Text = "Limitar Numero de Registro no Filtro"
        Me.chkTpoFiltro.UseVisualStyleBackColor = True
        '
        'txtPesqFabricante
        '
        Me.txtPesqFabricante.BackColor = System.Drawing.Color.White
        Me.txtPesqFabricante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqFabricante.Location = New System.Drawing.Point(12, 117)
        Me.txtPesqFabricante.Name = "txtPesqFabricante"
        Me.txtPesqFabricante.Size = New System.Drawing.Size(198, 20)
        Me.txtPesqFabricante.TabIndex = 107
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 102)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 13)
        Me.Label5.TabIndex = 106
        Me.Label5.Text = "Fabricante."
        '
        'txtPesqCodProtheus
        '
        Me.txtPesqCodProtheus.BackColor = System.Drawing.Color.White
        Me.txtPesqCodProtheus.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqCodProtheus.Location = New System.Drawing.Point(11, 79)
        Me.txtPesqCodProtheus.Name = "txtPesqCodProtheus"
        Me.txtPesqCodProtheus.Size = New System.Drawing.Size(94, 20)
        Me.txtPesqCodProtheus.TabIndex = 105
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 13)
        Me.Label8.TabIndex = 104
        Me.Label8.Text = "Cod. Protheus:"
        '
        'btnLimpar
        '
        Me.btnLimpar.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnLimpar.Location = New System.Drawing.Point(690, 77)
        Me.btnLimpar.Name = "btnLimpar"
        Me.btnLimpar.Size = New System.Drawing.Size(75, 38)
        Me.btnLimpar.TabIndex = 103
        Me.btnLimpar.Text = "Limpar"
        Me.btnLimpar.UseVisualStyleBackColor = False
        '
        'txtPesqTipo
        '
        Me.txtPesqTipo.BackColor = System.Drawing.Color.White
        Me.txtPesqTipo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqTipo.Location = New System.Drawing.Point(612, 79)
        Me.txtPesqTipo.Name = "txtPesqTipo"
        Me.txtPesqTipo.Size = New System.Drawing.Size(72, 20)
        Me.txtPesqTipo.TabIndex = 100
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(609, 64)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(28, 13)
        Me.Label37.TabIndex = 99
        Me.Label37.Text = "Tipo"
        '
        'txtPesqDescricao03
        '
        Me.txtPesqDescricao03.BackColor = System.Drawing.Color.White
        Me.txtPesqDescricao03.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqDescricao03.Location = New System.Drawing.Point(478, 79)
        Me.txtPesqDescricao03.Name = "txtPesqDescricao03"
        Me.txtPesqDescricao03.Size = New System.Drawing.Size(128, 20)
        Me.txtPesqDescricao03.TabIndex = 98
        '
        'txtPesqDescricao02
        '
        Me.txtPesqDescricao02.BackColor = System.Drawing.Color.White
        Me.txtPesqDescricao02.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqDescricao02.Location = New System.Drawing.Point(344, 79)
        Me.txtPesqDescricao02.Name = "txtPesqDescricao02"
        Me.txtPesqDescricao02.Size = New System.Drawing.Size(128, 20)
        Me.txtPesqDescricao02.TabIndex = 97
        '
        'txtPesqDescricao01
        '
        Me.txtPesqDescricao01.BackColor = System.Drawing.Color.White
        Me.txtPesqDescricao01.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqDescricao01.Location = New System.Drawing.Point(210, 79)
        Me.txtPesqDescricao01.Name = "txtPesqDescricao01"
        Me.txtPesqDescricao01.Size = New System.Drawing.Size(128, 20)
        Me.txtPesqDescricao01.TabIndex = 96
        '
        'txtPesqCodFabricante
        '
        Me.txtPesqCodFabricante.BackColor = System.Drawing.Color.White
        Me.txtPesqCodFabricante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqCodFabricante.Location = New System.Drawing.Point(111, 79)
        Me.txtPesqCodFabricante.Name = "txtPesqCodFabricante"
        Me.txtPesqCodFabricante.Size = New System.Drawing.Size(94, 20)
        Me.txtPesqCodFabricante.TabIndex = 95
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(210, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 94
        Me.Label3.Text = "Descrição"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(109, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 13)
        Me.Label2.TabIndex = 93
        Me.Label2.Text = "Codigo Fabricante:"
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Image = Global.SwLynx_4._1.My.Resources.Resources.cancelar
        Me.btnCancelar.Location = New System.Drawing.Point(839, 11)
        Me.btnCancelar.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(123, 41)
        Me.btnCancelar.TabIndex = 112
        Me.btnCancelar.Text = "Sair"
        Me.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnSalvar
        '
        Me.btnSalvar.Image = Global.SwLynx_4._1.My.Resources.Resources.salvar
        Me.btnSalvar.Location = New System.Drawing.Point(10, 11)
        Me.btnSalvar.Margin = New System.Windows.Forms.Padding(2)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(123, 41)
        Me.btnSalvar.TabIndex = 111
        Me.btnSalvar.Text = "Salvar"
        Me.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSalvar.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.lblB1_PICM_Valor)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.lblB1_IPI_Valor)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.lblB1_PICM)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.lblB1_IPI)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.lblB1_UPRC)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.lblB1_PESO)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.lblB1_UM)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.lblB1_TIPO)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.lblB1_DESC)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.lblB1_ZFABRIC)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lblB1_ZREF)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblCodProtheus)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 149)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(956, 216)
        Me.GroupBox1.TabIndex = 113
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Dados Produto Selecionado"
        '
        'lblB1_PICM_Valor
        '
        Me.lblB1_PICM_Valor.BackColor = System.Drawing.Color.White
        Me.lblB1_PICM_Valor.Location = New System.Drawing.Point(583, 116)
        Me.lblB1_PICM_Valor.Name = "lblB1_PICM_Valor"
        Me.lblB1_PICM_Valor.Size = New System.Drawing.Size(95, 18)
        Me.lblB1_PICM_Valor.TabIndex = 128
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(583, 101)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(56, 13)
        Me.Label18.TabIndex = 127
        Me.Label18.Text = "R$_ICMS:"
        '
        'lblB1_IPI_Valor
        '
        Me.lblB1_IPI_Valor.BackColor = System.Drawing.Color.White
        Me.lblB1_IPI_Valor.Location = New System.Drawing.Point(684, 116)
        Me.lblB1_IPI_Valor.Name = "lblB1_IPI_Valor"
        Me.lblB1_IPI_Valor.Size = New System.Drawing.Size(95, 18)
        Me.lblB1_IPI_Valor.TabIndex = 126
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(684, 101)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(43, 13)
        Me.Label20.TabIndex = 125
        Me.Label20.Text = "R$_IPI:"
        '
        'lblB1_PICM
        '
        Me.lblB1_PICM.BackColor = System.Drawing.Color.White
        Me.lblB1_PICM.Location = New System.Drawing.Point(583, 76)
        Me.lblB1_PICM.Name = "lblB1_PICM"
        Me.lblB1_PICM.Size = New System.Drawing.Size(95, 18)
        Me.lblB1_PICM.TabIndex = 124
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(583, 61)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(44, 13)
        Me.Label17.TabIndex = 123
        Me.Label17.Text = "%ICMS:"
        '
        'lblB1_IPI
        '
        Me.lblB1_IPI.BackColor = System.Drawing.Color.White
        Me.lblB1_IPI.Location = New System.Drawing.Point(684, 76)
        Me.lblB1_IPI.Name = "lblB1_IPI"
        Me.lblB1_IPI.Size = New System.Drawing.Size(95, 18)
        Me.lblB1_IPI.TabIndex = 122
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(684, 61)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(31, 13)
        Me.Label16.TabIndex = 121
        Me.Label16.Text = "%IPI:"
        '
        'lblB1_UPRC
        '
        Me.lblB1_UPRC.BackColor = System.Drawing.Color.White
        Me.lblB1_UPRC.Location = New System.Drawing.Point(482, 76)
        Me.lblB1_UPRC.Name = "lblB1_UPRC"
        Me.lblB1_UPRC.Size = New System.Drawing.Size(95, 18)
        Me.lblB1_UPRC.TabIndex = 120
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(482, 61)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(22, 13)
        Me.Label15.TabIndex = 119
        Me.Label15.Text = "Vlr:"
        '
        'lblB1_PESO
        '
        Me.lblB1_PESO.BackColor = System.Drawing.Color.White
        Me.lblB1_PESO.Location = New System.Drawing.Point(423, 76)
        Me.lblB1_PESO.Name = "lblB1_PESO"
        Me.lblB1_PESO.Size = New System.Drawing.Size(53, 18)
        Me.lblB1_PESO.TabIndex = 118
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(423, 61)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(34, 13)
        Me.Label13.TabIndex = 117
        Me.Label13.Text = "Peso:"
        '
        'lblB1_UM
        '
        Me.lblB1_UM.BackColor = System.Drawing.Color.White
        Me.lblB1_UM.Location = New System.Drawing.Point(364, 76)
        Me.lblB1_UM.Name = "lblB1_UM"
        Me.lblB1_UM.Size = New System.Drawing.Size(53, 18)
        Me.lblB1_UM.TabIndex = 116
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(364, 61)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(29, 13)
        Me.Label10.TabIndex = 115
        Me.Label10.Text = "Unit."
        '
        'lblB1_TIPO
        '
        Me.lblB1_TIPO.BackColor = System.Drawing.Color.White
        Me.lblB1_TIPO.Location = New System.Drawing.Point(305, 76)
        Me.lblB1_TIPO.Name = "lblB1_TIPO"
        Me.lblB1_TIPO.Size = New System.Drawing.Size(53, 18)
        Me.lblB1_TIPO.TabIndex = 114
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(305, 61)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(31, 13)
        Me.Label14.TabIndex = 113
        Me.Label14.Text = "Tipo:"
        '
        'lblB1_DESC
        '
        Me.lblB1_DESC.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblB1_DESC.BackColor = System.Drawing.Color.White
        Me.lblB1_DESC.Location = New System.Drawing.Point(9, 162)
        Me.lblB1_DESC.Name = "lblB1_DESC"
        Me.lblB1_DESC.Size = New System.Drawing.Size(938, 51)
        Me.lblB1_DESC.TabIndex = 112
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(9, 147)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(58, 13)
        Me.Label12.TabIndex = 111
        Me.Label12.Text = "Descrição:"
        '
        'lblB1_ZFABRIC
        '
        Me.lblB1_ZFABRIC.BackColor = System.Drawing.Color.White
        Me.lblB1_ZFABRIC.Location = New System.Drawing.Point(6, 115)
        Me.lblB1_ZFABRIC.Name = "lblB1_ZFABRIC"
        Me.lblB1_ZFABRIC.Size = New System.Drawing.Size(335, 18)
        Me.lblB1_ZFABRIC.TabIndex = 110
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 100)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 13)
        Me.Label7.TabIndex = 109
        Me.Label7.Text = "Fabricante:"
        '
        'lblB1_ZREF
        '
        Me.lblB1_ZREF.BackColor = System.Drawing.Color.White
        Me.lblB1_ZREF.Location = New System.Drawing.Point(8, 76)
        Me.lblB1_ZREF.Name = "lblB1_ZREF"
        Me.lblB1_ZREF.Size = New System.Drawing.Size(284, 18)
        Me.lblB1_ZREF.TabIndex = 108
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 61)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 13)
        Me.Label6.TabIndex = 107
        Me.Label6.Text = "Cod. Fabricante:"
        '
        'lblCodProtheus
        '
        Me.lblCodProtheus.BackColor = System.Drawing.Color.White
        Me.lblCodProtheus.Location = New System.Drawing.Point(9, 36)
        Me.lblCodProtheus.Name = "lblCodProtheus"
        Me.lblCodProtheus.Size = New System.Drawing.Size(97, 18)
        Me.lblCodProtheus.TabIndex = 106
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 13)
        Me.Label1.TabIndex = 105
        Me.Label1.Text = "Cod. Protheus:"
        '
        'frmMateriaisProtheus
        '
        Me.AcceptButton = Me.btnSalvar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(971, 752)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnSalvar)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtStringExcluir)
        Me.Controls.Add(Me.chkTpoFiltro)
        Me.Controls.Add(Me.txtPesqFabricante)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtPesqCodProtheus)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnLimpar)
        Me.Controls.Add(Me.txtPesqTipo)
        Me.Controls.Add(Me.Label37)
        Me.Controls.Add(Me.txtPesqDescricao03)
        Me.Controls.Add(Me.txtPesqDescricao02)
        Me.Controls.Add(Me.txtPesqDescricao01)
        Me.Controls.Add(Me.txtPesqCodFabricante)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dgvMaterialProtheus)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "frmMateriaisProtheus"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lista de Material do Protheus"
        CType(Me.dgvMaterialProtheus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvMaterialProtheus As Windows.Forms.DataGridView
    Friend WithEvents TimerdgvMaterialProtheus As Windows.Forms.Timer
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents txtStringExcluir As Windows.Forms.TextBox
    Friend WithEvents chkTpoFiltro As Windows.Forms.CheckBox
    Friend WithEvents txtPesqFabricante As Windows.Forms.TextBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents txtPesqCodProtheus As Windows.Forms.TextBox
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents btnLimpar As Windows.Forms.Button
    Friend WithEvents txtPesqTipo As Windows.Forms.TextBox
    Friend WithEvents Label37 As Windows.Forms.Label
    Friend WithEvents txtPesqDescricao03 As Windows.Forms.TextBox
    Friend WithEvents txtPesqDescricao02 As Windows.Forms.TextBox
    Friend WithEvents txtPesqDescricao01 As Windows.Forms.TextBox
    Friend WithEvents txtPesqCodFabricante As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents btnCancelar As Windows.Forms.Button
    Friend WithEvents btnSalvar As Windows.Forms.Button
    Friend WithEvents dgvSelecao As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents lblB1_ZFABRIC As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents lblB1_ZREF As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents lblCodProtheus As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents lblB1_TIPO As Windows.Forms.Label
    Friend WithEvents Label14 As Windows.Forms.Label
    Friend WithEvents lblB1_DESC As Windows.Forms.Label
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents lblB1_UPRC As Windows.Forms.Label
    Friend WithEvents Label15 As Windows.Forms.Label
    Friend WithEvents lblB1_PESO As Windows.Forms.Label
    Friend WithEvents Label13 As Windows.Forms.Label
    Friend WithEvents lblB1_UM As Windows.Forms.Label
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents lblB1_PICM_Valor As Windows.Forms.Label
    Friend WithEvents Label18 As Windows.Forms.Label
    Friend WithEvents lblB1_IPI_Valor As Windows.Forms.Label
    Friend WithEvents Label20 As Windows.Forms.Label
    Friend WithEvents lblB1_PICM As Windows.Forms.Label
    Friend WithEvents Label17 As Windows.Forms.Label
    Friend WithEvents lblB1_IPI As Windows.Forms.Label
    Friend WithEvents Label16 As Windows.Forms.Label
End Class
