<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMateriaisProtheusa
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtCodProtheus = New System.Windows.Forms.TextBox()
        Me.txtZref = New System.Windows.Forms.TextBox()
        Me.txtDescricao1 = New System.Windows.Forms.TextBox()
        Me.txtDescricao2 = New System.Windows.Forms.TextBox()
        Me.txtDescricao3 = New System.Windows.Forms.TextBox()
        Me.txtFabricante = New System.Windows.Forms.TextBox()
        Me.dgvMaterialProtheus = New System.Windows.Forms.DataGridView()
        Me.TimerdgvMaterialProtheus = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtUnidade = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtLarguram2 = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtValorMaterial = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtComprimentom2 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPesoMaterial = New System.Windows.Forms.TextBox()
        Me.btnAssociarMaterialM2 = New System.Windows.Forms.Button()
        CType(Me.dgvMaterialProtheus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Cod. Protheus:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(118, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Cod. Ref."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(223, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Descrição:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(754, 79)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 16)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Fabricante:"
        '
        'txtCodProtheus
        '
        Me.txtCodProtheus.Location = New System.Drawing.Point(11, 98)
        Me.txtCodProtheus.Name = "txtCodProtheus"
        Me.txtCodProtheus.Size = New System.Drawing.Size(100, 22)
        Me.txtCodProtheus.TabIndex = 5
        '
        'txtZref
        '
        Me.txtZref.Location = New System.Drawing.Point(117, 98)
        Me.txtZref.Name = "txtZref"
        Me.txtZref.Size = New System.Drawing.Size(100, 22)
        Me.txtZref.TabIndex = 6
        '
        'txtDescricao1
        '
        Me.txtDescricao1.Location = New System.Drawing.Point(223, 98)
        Me.txtDescricao1.Name = "txtDescricao1"
        Me.txtDescricao1.Size = New System.Drawing.Size(176, 22)
        Me.txtDescricao1.TabIndex = 7
        '
        'txtDescricao2
        '
        Me.txtDescricao2.Location = New System.Drawing.Point(399, 98)
        Me.txtDescricao2.Name = "txtDescricao2"
        Me.txtDescricao2.Size = New System.Drawing.Size(176, 22)
        Me.txtDescricao2.TabIndex = 8
        '
        'txtDescricao3
        '
        Me.txtDescricao3.Location = New System.Drawing.Point(575, 98)
        Me.txtDescricao3.Name = "txtDescricao3"
        Me.txtDescricao3.Size = New System.Drawing.Size(176, 22)
        Me.txtDescricao3.TabIndex = 9
        '
        'txtFabricante
        '
        Me.txtFabricante.Location = New System.Drawing.Point(757, 98)
        Me.txtFabricante.Name = "txtFabricante"
        Me.txtFabricante.Size = New System.Drawing.Size(176, 22)
        Me.txtFabricante.TabIndex = 10
        '
        'dgvMaterialProtheus
        '
        Me.dgvMaterialProtheus.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMaterialProtheus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.dgvMaterialProtheus.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.dgvMaterialProtheus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMaterialProtheus.Location = New System.Drawing.Point(14, 126)
        Me.dgvMaterialProtheus.Name = "dgvMaterialProtheus"
        Me.dgvMaterialProtheus.RowHeadersWidth = 51
        Me.dgvMaterialProtheus.RowTemplate.Height = 24
        Me.dgvMaterialProtheus.Size = New System.Drawing.Size(1149, 557)
        Me.dgvMaterialProtheus.TabIndex = 11
        '
        'TimerdgvMaterialProtheus
        '
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Info
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtUnidade)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtLarguram2)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtValorMaterial)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtComprimentom2)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtPesoMaterial)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(880, 61)
        Me.GroupBox1.TabIndex = 101
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Material Tipo Chapa"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(706, 32)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(62, 16)
        Me.Label9.TabIndex = 96
        Me.Label9.Text = "Unidade:"
        '
        'txtUnidade
        '
        Me.txtUnidade.Location = New System.Drawing.Point(774, 29)
        Me.txtUnidade.Name = "txtUnidade"
        Me.txtUnidade.Size = New System.Drawing.Size(72, 22)
        Me.txtUnidade.TabIndex = 94
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(1085, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(62, 16)
        Me.Label8.TabIndex = 95
        Me.Label8.Text = "Unidade:"
        '
        'txtLarguram2
        '
        Me.txtLarguram2.Location = New System.Drawing.Point(81, 29)
        Me.txtLarguram2.Name = "txtLarguram2"
        Me.txtLarguram2.Size = New System.Drawing.Size(72, 22)
        Me.txtLarguram2.TabIndex = 3
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(159, 32)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(90, 16)
        Me.Label21.TabIndex = 2
        Me.Label21.Text = "Comprimento:"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(19, 32)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(56, 16)
        Me.Label20.TabIndex = 1
        Me.Label20.Text = "Largura:"
        '
        'txtValorMaterial
        '
        Me.txtValorMaterial.Location = New System.Drawing.Point(607, 29)
        Me.txtValorMaterial.Name = "txtValorMaterial"
        Me.txtValorMaterial.Size = New System.Drawing.Size(72, 22)
        Me.txtValorMaterial.TabIndex = 92
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(508, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 16)
        Me.Label4.TabIndex = 93
        Me.Label4.Text = "Valor material:"
        '
        'txtComprimentom2
        '
        Me.txtComprimentom2.Location = New System.Drawing.Point(255, 29)
        Me.txtComprimentom2.Name = "txtComprimentom2"
        Me.txtComprimentom2.Size = New System.Drawing.Size(72, 22)
        Me.txtComprimentom2.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(331, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(93, 16)
        Me.Label6.TabIndex = 91
        Me.Label6.Text = "Peso material:"
        '
        'txtPesoMaterial
        '
        Me.txtPesoMaterial.Location = New System.Drawing.Point(430, 29)
        Me.txtPesoMaterial.Name = "txtPesoMaterial"
        Me.txtPesoMaterial.Size = New System.Drawing.Size(72, 22)
        Me.txtPesoMaterial.TabIndex = 90
        '
        'btnAssociarMaterialM2
        '
        Me.btnAssociarMaterialM2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAssociarMaterialM2.Image = Global.SwLynx_4._1.My.Resources.Resources.salvar
        Me.btnAssociarMaterialM2.Location = New System.Drawing.Point(910, 18)
        Me.btnAssociarMaterialM2.Name = "btnAssociarMaterialM2"
        Me.btnAssociarMaterialM2.Size = New System.Drawing.Size(147, 46)
        Me.btnAssociarMaterialM2.TabIndex = 100
        Me.btnAssociarMaterialM2.Text = "Salvar material"
        Me.btnAssociarMaterialM2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAssociarMaterialM2.UseVisualStyleBackColor = True
        '
        'frmMateriaisProtheusa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1175, 695)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnAssociarMaterialM2)
        Me.Controls.Add(Me.dgvMaterialProtheus)
        Me.Controls.Add(Me.txtFabricante)
        Me.Controls.Add(Me.txtDescricao3)
        Me.Controls.Add(Me.txtDescricao2)
        Me.Controls.Add(Me.txtDescricao1)
        Me.Controls.Add(Me.txtZref)
        Me.Controls.Add(Me.txtCodProtheus)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmMateriaisProtheusa"
        Me.Text = "Lista de Material do Protheus"
        CType(Me.dgvMaterialProtheus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents txtCodProtheus As Windows.Forms.TextBox
    Friend WithEvents txtZref As Windows.Forms.TextBox
    Friend WithEvents txtDescricao1 As Windows.Forms.TextBox
    Friend WithEvents txtDescricao2 As Windows.Forms.TextBox
    Friend WithEvents txtDescricao3 As Windows.Forms.TextBox
    Friend WithEvents txtFabricante As Windows.Forms.TextBox
    Friend WithEvents dgvMaterialProtheus As Windows.Forms.DataGridView
    Friend WithEvents TimerdgvMaterialProtheus As Windows.Forms.Timer
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents txtUnidade As Windows.Forms.TextBox
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents txtLarguram2 As Windows.Forms.TextBox
    Friend WithEvents Label21 As Windows.Forms.Label
    Friend WithEvents Label20 As Windows.Forms.Label
    Friend WithEvents txtValorMaterial As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents txtComprimentom2 As Windows.Forms.TextBox
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents txtPesoMaterial As Windows.Forms.TextBox
    Friend WithEvents btnAssociarMaterialM2 As Windows.Forms.Button
End Class
