<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMateriaisOmie
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
        Me.lblcodigo_produto = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblcodigo = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.dgvSelecao = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TimerdgvMaterialProtheus = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtStringExcluir = New System.Windows.Forms.TextBox()
        Me.chkTpoFiltro = New System.Windows.Forms.CheckBox()
        Me.txtPesqFabricante = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnLimpar = New System.Windows.Forms.Button()
        Me.txtPesqDescricao03 = New System.Windows.Forms.TextBox()
        Me.txtPesqDescricao02 = New System.Windows.Forms.TextBox()
        Me.txtPesqDescricao01 = New System.Windows.Forms.TextBox()
        Me.txtPesqcodigo_produto = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dgvMaterialOmie = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvMaterialOmie, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblB1_PICM_Valor
        '
        Me.lblB1_PICM_Valor.BackColor = System.Drawing.Color.White
        Me.lblB1_PICM_Valor.Enabled = False
        Me.lblB1_PICM_Valor.Location = New System.Drawing.Point(777, 143)
        Me.lblB1_PICM_Valor.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblB1_PICM_Valor.Name = "lblB1_PICM_Valor"
        Me.lblB1_PICM_Valor.Size = New System.Drawing.Size(127, 22)
        Me.lblB1_PICM_Valor.TabIndex = 128
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Enabled = False
        Me.Label18.Location = New System.Drawing.Point(777, 124)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(66, 16)
        Me.Label18.TabIndex = 127
        Me.Label18.Text = "R$_ICMS:"
        '
        'lblB1_IPI_Valor
        '
        Me.lblB1_IPI_Valor.BackColor = System.Drawing.Color.White
        Me.lblB1_IPI_Valor.Enabled = False
        Me.lblB1_IPI_Valor.Location = New System.Drawing.Point(912, 143)
        Me.lblB1_IPI_Valor.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblB1_IPI_Valor.Name = "lblB1_IPI_Valor"
        Me.lblB1_IPI_Valor.Size = New System.Drawing.Size(127, 22)
        Me.lblB1_IPI_Valor.TabIndex = 126
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Enabled = False
        Me.Label20.Location = New System.Drawing.Point(912, 124)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(49, 16)
        Me.Label20.TabIndex = 125
        Me.Label20.Text = "R$_IPI:"
        '
        'lblB1_PICM
        '
        Me.lblB1_PICM.BackColor = System.Drawing.Color.White
        Me.lblB1_PICM.Enabled = False
        Me.lblB1_PICM.Location = New System.Drawing.Point(777, 94)
        Me.lblB1_PICM.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblB1_PICM.Name = "lblB1_PICM"
        Me.lblB1_PICM.Size = New System.Drawing.Size(127, 22)
        Me.lblB1_PICM.TabIndex = 124
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Enabled = False
        Me.Label17.Location = New System.Drawing.Point(777, 75)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(54, 16)
        Me.Label17.TabIndex = 123
        Me.Label17.Text = "%ICMS:"
        '
        'lblB1_IPI
        '
        Me.lblB1_IPI.BackColor = System.Drawing.Color.White
        Me.lblB1_IPI.Enabled = False
        Me.lblB1_IPI.Location = New System.Drawing.Point(912, 94)
        Me.lblB1_IPI.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblB1_IPI.Name = "lblB1_IPI"
        Me.lblB1_IPI.Size = New System.Drawing.Size(127, 22)
        Me.lblB1_IPI.TabIndex = 122
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Enabled = False
        Me.Label16.Location = New System.Drawing.Point(912, 75)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(37, 16)
        Me.Label16.TabIndex = 121
        Me.Label16.Text = "%IPI:"
        '
        'lblB1_UPRC
        '
        Me.lblB1_UPRC.BackColor = System.Drawing.Color.White
        Me.lblB1_UPRC.Location = New System.Drawing.Point(643, 94)
        Me.lblB1_UPRC.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblB1_UPRC.Name = "lblB1_UPRC"
        Me.lblB1_UPRC.Size = New System.Drawing.Size(127, 22)
        Me.lblB1_UPRC.TabIndex = 120
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(643, 75)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(26, 16)
        Me.Label15.TabIndex = 119
        Me.Label15.Text = "Vlr:"
        '
        'lblB1_PESO
        '
        Me.lblB1_PESO.BackColor = System.Drawing.Color.White
        Me.lblB1_PESO.Location = New System.Drawing.Point(564, 94)
        Me.lblB1_PESO.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblB1_PESO.Name = "lblB1_PESO"
        Me.lblB1_PESO.Size = New System.Drawing.Size(71, 22)
        Me.lblB1_PESO.TabIndex = 118
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(564, 75)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(42, 16)
        Me.Label13.TabIndex = 117
        Me.Label13.Text = "Peso:"
        '
        'lblB1_UM
        '
        Me.lblB1_UM.BackColor = System.Drawing.Color.White
        Me.lblB1_UM.Location = New System.Drawing.Point(485, 94)
        Me.lblB1_UM.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblB1_UM.Name = "lblB1_UM"
        Me.lblB1_UM.Size = New System.Drawing.Size(71, 22)
        Me.lblB1_UM.TabIndex = 116
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(485, 75)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(33, 16)
        Me.Label10.TabIndex = 115
        Me.Label10.Text = "Unit."
        '
        'lblB1_TIPO
        '
        Me.lblB1_TIPO.BackColor = System.Drawing.Color.White
        Me.lblB1_TIPO.Enabled = False
        Me.lblB1_TIPO.Location = New System.Drawing.Point(407, 94)
        Me.lblB1_TIPO.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblB1_TIPO.Name = "lblB1_TIPO"
        Me.lblB1_TIPO.Size = New System.Drawing.Size(71, 22)
        Me.lblB1_TIPO.TabIndex = 114
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Enabled = False
        Me.Label14.Location = New System.Drawing.Point(407, 75)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(38, 16)
        Me.Label14.TabIndex = 113
        Me.Label14.Text = "Tipo:"
        '
        'lblB1_DESC
        '
        Me.lblB1_DESC.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblB1_DESC.BackColor = System.Drawing.Color.White
        Me.lblB1_DESC.Location = New System.Drawing.Point(12, 199)
        Me.lblB1_DESC.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblB1_DESC.Name = "lblB1_DESC"
        Me.lblB1_DESC.Size = New System.Drawing.Size(1092, 63)
        Me.lblB1_DESC.TabIndex = 112
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(12, 181)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 16)
        Me.Label12.TabIndex = 111
        Me.Label12.Text = "Descrição:"
        '
        'lblB1_ZFABRIC
        '
        Me.lblB1_ZFABRIC.BackColor = System.Drawing.Color.White
        Me.lblB1_ZFABRIC.Enabled = False
        Me.lblB1_ZFABRIC.Location = New System.Drawing.Point(8, 142)
        Me.lblB1_ZFABRIC.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblB1_ZFABRIC.Name = "lblB1_ZFABRIC"
        Me.lblB1_ZFABRIC.Size = New System.Drawing.Size(447, 22)
        Me.lblB1_ZFABRIC.TabIndex = 110
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Enabled = False
        Me.Label7.Location = New System.Drawing.Point(8, 123)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 16)
        Me.Label7.TabIndex = 109
        Me.Label7.Text = "Fabricante:"
        '
        'lblcodigo_produto
        '
        Me.lblcodigo_produto.BackColor = System.Drawing.Color.White
        Me.lblcodigo_produto.Location = New System.Drawing.Point(11, 94)
        Me.lblcodigo_produto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblcodigo_produto.Name = "lblcodigo_produto"
        Me.lblcodigo_produto.Size = New System.Drawing.Size(379, 22)
        Me.lblcodigo_produto.TabIndex = 108
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 75)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 16)
        Me.Label6.TabIndex = 107
        Me.Label6.Text = "Cod. Produto:"
        '
        'lblcodigo
        '
        Me.lblcodigo.BackColor = System.Drawing.Color.White
        Me.lblcodigo.Location = New System.Drawing.Point(12, 44)
        Me.lblcodigo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblcodigo.Name = "lblcodigo"
        Me.lblcodigo.Size = New System.Drawing.Size(129, 22)
        Me.lblcodigo.TabIndex = 106
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 26)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 16)
        Me.Label1.TabIndex = 105
        Me.Label1.Text = "Codigo:"
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Image = Global.SwLynx_4._1.My.Resources.Resources.cancelar
        Me.btnCancelar.Location = New System.Drawing.Point(955, 14)
        Me.btnCancelar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(164, 50)
        Me.btnCancelar.TabIndex = 132
        Me.btnCancelar.Text = "Sair"
        Me.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnSalvar
        '
        Me.btnSalvar.Image = Global.SwLynx_4._1.My.Resources.Resources.salvar
        Me.btnSalvar.Location = New System.Drawing.Point(15, 14)
        Me.btnSalvar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(164, 50)
        Me.btnSalvar.TabIndex = 131
        Me.btnSalvar.Text = "Salvar"
        Me.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSalvar.UseVisualStyleBackColor = True
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
        'TimerdgvMaterialProtheus
        '
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
        Me.GroupBox1.Controls.Add(Me.lblcodigo_produto)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblcodigo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 183)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(1116, 266)
        Me.GroupBox1.TabIndex = 133
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Dados Produto Selecionado"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Enabled = False
        Me.Label9.Location = New System.Drawing.Point(567, 150)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(94, 16)
        Me.Label9.TabIndex = 130
        Me.Label9.Text = "String a Excluir"
        '
        'txtStringExcluir
        '
        Me.txtStringExcluir.BackColor = System.Drawing.Color.RosyBrown
        Me.txtStringExcluir.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtStringExcluir.Enabled = False
        Me.txtStringExcluir.Location = New System.Drawing.Point(677, 144)
        Me.txtStringExcluir.Margin = New System.Windows.Forms.Padding(4)
        Me.txtStringExcluir.Name = "txtStringExcluir"
        Me.txtStringExcluir.Size = New System.Drawing.Size(105, 22)
        Me.txtStringExcluir.TabIndex = 129
        '
        'chkTpoFiltro
        '
        Me.chkTpoFiltro.AutoSize = True
        Me.chkTpoFiltro.Checked = True
        Me.chkTpoFiltro.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTpoFiltro.Enabled = False
        Me.chkTpoFiltro.Location = New System.Drawing.Point(297, 148)
        Me.chkTpoFiltro.Margin = New System.Windows.Forms.Padding(4)
        Me.chkTpoFiltro.Name = "chkTpoFiltro"
        Me.chkTpoFiltro.Size = New System.Drawing.Size(242, 20)
        Me.chkTpoFiltro.TabIndex = 128
        Me.chkTpoFiltro.Text = "Limitar Numero de Registro no Filtro"
        Me.chkTpoFiltro.UseVisualStyleBackColor = True
        '
        'txtPesqFabricante
        '
        Me.txtPesqFabricante.BackColor = System.Drawing.Color.White
        Me.txtPesqFabricante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqFabricante.Enabled = False
        Me.txtPesqFabricante.Location = New System.Drawing.Point(17, 144)
        Me.txtPesqFabricante.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPesqFabricante.Name = "txtPesqFabricante"
        Me.txtPesqFabricante.Size = New System.Drawing.Size(263, 22)
        Me.txtPesqFabricante.TabIndex = 127
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Enabled = False
        Me.Label5.Location = New System.Drawing.Point(15, 126)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 16)
        Me.Label5.TabIndex = 126
        Me.Label5.Text = "Fabricante."
        '
        'btnLimpar
        '
        Me.btnLimpar.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnLimpar.Location = New System.Drawing.Point(921, 95)
        Me.btnLimpar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnLimpar.Name = "btnLimpar"
        Me.btnLimpar.Size = New System.Drawing.Size(100, 47)
        Me.btnLimpar.TabIndex = 123
        Me.btnLimpar.Text = "Limpar"
        Me.btnLimpar.UseVisualStyleBackColor = False
        '
        'txtPesqDescricao03
        '
        Me.txtPesqDescricao03.BackColor = System.Drawing.Color.White
        Me.txtPesqDescricao03.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqDescricao03.Location = New System.Drawing.Point(507, 95)
        Me.txtPesqDescricao03.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPesqDescricao03.Name = "txtPesqDescricao03"
        Me.txtPesqDescricao03.Size = New System.Drawing.Size(169, 22)
        Me.txtPesqDescricao03.TabIndex = 120
        '
        'txtPesqDescricao02
        '
        Me.txtPesqDescricao02.BackColor = System.Drawing.Color.White
        Me.txtPesqDescricao02.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqDescricao02.Location = New System.Drawing.Point(328, 95)
        Me.txtPesqDescricao02.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPesqDescricao02.Name = "txtPesqDescricao02"
        Me.txtPesqDescricao02.Size = New System.Drawing.Size(169, 22)
        Me.txtPesqDescricao02.TabIndex = 119
        '
        'txtPesqDescricao01
        '
        Me.txtPesqDescricao01.BackColor = System.Drawing.Color.White
        Me.txtPesqDescricao01.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqDescricao01.Location = New System.Drawing.Point(149, 95)
        Me.txtPesqDescricao01.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPesqDescricao01.Name = "txtPesqDescricao01"
        Me.txtPesqDescricao01.Size = New System.Drawing.Size(169, 22)
        Me.txtPesqDescricao01.TabIndex = 118
        '
        'txtPesqcodigo_produto
        '
        Me.txtPesqcodigo_produto.BackColor = System.Drawing.Color.White
        Me.txtPesqcodigo_produto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPesqcodigo_produto.Location = New System.Drawing.Point(17, 95)
        Me.txtPesqcodigo_produto.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPesqcodigo_produto.Name = "txtPesqcodigo_produto"
        Me.txtPesqcodigo_produto.Size = New System.Drawing.Size(124, 22)
        Me.txtPesqcodigo_produto.TabIndex = 117
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(149, 77)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 16)
        Me.Label3.TabIndex = 116
        Me.Label3.Text = "Descrição"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 77)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 16)
        Me.Label2.TabIndex = 115
        Me.Label2.Text = "Cod. Produto:"
        '
        'dgvMaterialOmie
        '
        Me.dgvMaterialOmie.AllowUserToAddRows = False
        Me.dgvMaterialOmie.AllowUserToDeleteRows = False
        Me.dgvMaterialOmie.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMaterialOmie.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.dgvMaterialOmie.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.dgvMaterialOmie.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMaterialOmie.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvSelecao})
        Me.dgvMaterialOmie.Location = New System.Drawing.Point(15, 455)
        Me.dgvMaterialOmie.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvMaterialOmie.Name = "dgvMaterialOmie"
        Me.dgvMaterialOmie.ReadOnly = True
        Me.dgvMaterialOmie.RowHeadersWidth = 51
        Me.dgvMaterialOmie.RowTemplate.Height = 24
        Me.dgvMaterialOmie.Size = New System.Drawing.Size(1116, 415)
        Me.dgvMaterialOmie.TabIndex = 114
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(1029, 95)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(100, 47)
        Me.Button1.TabIndex = 134
        Me.Button1.Text = "Buscar"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'frmMateriaisOmie
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1147, 884)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnSalvar)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtStringExcluir)
        Me.Controls.Add(Me.chkTpoFiltro)
        Me.Controls.Add(Me.txtPesqFabricante)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnLimpar)
        Me.Controls.Add(Me.txtPesqDescricao03)
        Me.Controls.Add(Me.txtPesqDescricao02)
        Me.Controls.Add(Me.txtPesqDescricao01)
        Me.Controls.Add(Me.txtPesqcodigo_produto)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dgvMaterialOmie)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmMateriaisOmie"
        Me.Text = "Materiais do Omie"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvMaterialOmie, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblB1_PICM_Valor As Windows.Forms.Label
    Friend WithEvents Label18 As Windows.Forms.Label
    Friend WithEvents lblB1_IPI_Valor As Windows.Forms.Label
    Friend WithEvents Label20 As Windows.Forms.Label
    Friend WithEvents lblB1_PICM As Windows.Forms.Label
    Friend WithEvents Label17 As Windows.Forms.Label
    Friend WithEvents lblB1_IPI As Windows.Forms.Label
    Friend WithEvents Label16 As Windows.Forms.Label
    Friend WithEvents lblB1_UPRC As Windows.Forms.Label
    Friend WithEvents Label15 As Windows.Forms.Label
    Friend WithEvents lblB1_PESO As Windows.Forms.Label
    Friend WithEvents Label13 As Windows.Forms.Label
    Friend WithEvents lblB1_UM As Windows.Forms.Label
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents lblB1_TIPO As Windows.Forms.Label
    Friend WithEvents Label14 As Windows.Forms.Label
    Friend WithEvents lblB1_DESC As Windows.Forms.Label
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents lblB1_ZFABRIC As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents lblcodigo_produto As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents lblcodigo As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents btnCancelar As Windows.Forms.Button
    Friend WithEvents btnSalvar As Windows.Forms.Button
    Friend WithEvents dgvSelecao As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents TimerdgvMaterialProtheus As Windows.Forms.Timer
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents txtStringExcluir As Windows.Forms.TextBox
    Friend WithEvents chkTpoFiltro As Windows.Forms.CheckBox
    Friend WithEvents txtPesqFabricante As Windows.Forms.TextBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents btnLimpar As Windows.Forms.Button
    Friend WithEvents txtPesqDescricao03 As Windows.Forms.TextBox
    Friend WithEvents txtPesqDescricao02 As Windows.Forms.TextBox
    Friend WithEvents txtPesqDescricao01 As Windows.Forms.TextBox
    Friend WithEvents txtPesqcodigo_produto As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents dgvMaterialOmie As Windows.Forms.DataGridView
    Friend WithEvents Button1 As Windows.Forms.Button
End Class
