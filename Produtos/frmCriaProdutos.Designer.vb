<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCriaProdutos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCriaProdutos))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnBuscarIsometrico = New System.Windows.Forms.Button()
        Me.btnBuscarFichaTecnica = New System.Windows.Forms.Button()
        Me.lblEnderecoIsometrico = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblEnderecoFichaTecnica = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtDescricaoProduto = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCodOmie = New System.Windows.Forms.TextBox()
        Me.txCodDesenhoProduto = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.mnuimagens = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BuscarImagensNoGoogleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalvarAImagemComoReferenciaDoMaterialToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarImagemNaBibliotecaDoSistemaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ColarImagemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.mnudgvProdutos = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AbrirPDFFichaTecnicaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AbrirPDFIsometricoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer1dgvProdutos = New System.Windows.Forms.Timer(Me.components)
        Me.btnExcluir = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuimagens.SuspendLayout()
        Me.mnudgvProdutos.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.btnBuscarIsometrico)
        Me.GroupBox1.Controls.Add(Me.btnBuscarFichaTecnica)
        Me.GroupBox1.Controls.Add(Me.lblEnderecoIsometrico)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.lblEnderecoFichaTecnica)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtDescricaoProduto)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtCodOmie)
        Me.GroupBox1.Controls.Add(Me.txCodDesenhoProduto)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 71)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1302, 447)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Dados do Produto"
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Location = New System.Drawing.Point(906, 18)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(389, 413)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 10066
        Me.PictureBox1.TabStop = False
        '
        'btnBuscarIsometrico
        '
        Me.btnBuscarIsometrico.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBuscarIsometrico.Location = New System.Drawing.Point(847, 244)
        Me.btnBuscarIsometrico.Name = "btnBuscarIsometrico"
        Me.btnBuscarIsometrico.Size = New System.Drawing.Size(40, 23)
        Me.btnBuscarIsometrico.TabIndex = 17
        Me.btnBuscarIsometrico.Text = "..."
        Me.btnBuscarIsometrico.UseVisualStyleBackColor = True
        '
        'btnBuscarFichaTecnica
        '
        Me.btnBuscarFichaTecnica.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBuscarFichaTecnica.Location = New System.Drawing.Point(847, 172)
        Me.btnBuscarFichaTecnica.Name = "btnBuscarFichaTecnica"
        Me.btnBuscarFichaTecnica.Size = New System.Drawing.Size(40, 23)
        Me.btnBuscarFichaTecnica.TabIndex = 16
        Me.btnBuscarFichaTecnica.Text = "..."
        Me.btnBuscarFichaTecnica.UseVisualStyleBackColor = True
        '
        'lblEnderecoIsometrico
        '
        Me.lblEnderecoIsometrico.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEnderecoIsometrico.BackColor = System.Drawing.SystemColors.Window
        Me.lblEnderecoIsometrico.Location = New System.Drawing.Point(6, 244)
        Me.lblEnderecoIsometrico.Name = "lblEnderecoIsometrico"
        Me.lblEnderecoIsometrico.Size = New System.Drawing.Size(797, 23)
        Me.lblEnderecoIsometrico.TabIndex = 15
        Me.lblEnderecoIsometrico.Text = "Buscar Ficha Tecnica:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 224)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(117, 16)
        Me.Label15.TabIndex = 14
        Me.Label15.Text = "Buscar Isometrico:"
        '
        'lblEnderecoFichaTecnica
        '
        Me.lblEnderecoFichaTecnica.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEnderecoFichaTecnica.BackColor = System.Drawing.SystemColors.Window
        Me.lblEnderecoFichaTecnica.Location = New System.Drawing.Point(6, 172)
        Me.lblEnderecoFichaTecnica.Name = "lblEnderecoFichaTecnica"
        Me.lblEnderecoFichaTecnica.Size = New System.Drawing.Size(809, 23)
        Me.lblEnderecoFichaTecnica.TabIndex = 13
        Me.lblEnderecoFichaTecnica.Text = "Buscar Ficha Tecnica:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 151)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(140, 16)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "Buscar Ficha Tecnica:"
        '
        'txtDescricaoProduto
        '
        Me.txtDescricaoProduto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescricaoProduto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDescricaoProduto.Location = New System.Drawing.Point(6, 307)
        Me.txtDescricaoProduto.Multiline = True
        Me.txtDescricaoProduto.Name = "txtDescricaoProduto"
        Me.txtDescricaoProduto.Size = New System.Drawing.Size(881, 124)
        Me.txtDescricaoProduto.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 288)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(140, 16)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Descrição do produto:"
        '
        'txtCodOmie
        '
        Me.txtCodOmie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCodOmie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCodOmie.Location = New System.Drawing.Point(6, 113)
        Me.txtCodOmie.Name = "txtCodOmie"
        Me.txtCodOmie.Size = New System.Drawing.Size(881, 22)
        Me.txtCodOmie.TabIndex = 7
        '
        'txCodDesenhoProduto
        '
        Me.txCodDesenhoProduto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txCodDesenhoProduto.Location = New System.Drawing.Point(6, 52)
        Me.txCodDesenhoProduto.Name = "txCodDesenhoProduto"
        Me.txCodDesenhoProduto.Size = New System.Drawing.Size(881, 22)
        Me.txCodDesenhoProduto.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 92)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(321, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Codigo do Produto no Sistema Gestão (Produto Pai):"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(180, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Codigo Desenho do produto:"
        '
        'mnuimagens
        '
        Me.mnuimagens.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuimagens.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BuscarImagensNoGoogleToolStripMenuItem, Me.SalvarAImagemComoReferenciaDoMaterialToolStripMenuItem, Me.BuscarImagemNaBibliotecaDoSistemaToolStripMenuItem, Me.ColarImagemToolStripMenuItem})
        Me.mnuimagens.Name = "mnuimagens"
        Me.mnuimagens.Size = New System.Drawing.Size(387, 100)
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
        'ColarImagemToolStripMenuItem
        '
        Me.ColarImagemToolStripMenuItem.Name = "ColarImagemToolStripMenuItem"
        Me.ColarImagemToolStripMenuItem.Size = New System.Drawing.Size(386, 24)
        Me.ColarImagemToolStripMenuItem.Text = "Colar Imagem"
        '
        'btnSalvar
        '
        Me.btnSalvar.Image = Global.SwLynx_4._1.My.Resources.Resources.salvar
        Me.btnSalvar.Location = New System.Drawing.Point(12, 11)
        Me.btnSalvar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(164, 51)
        Me.btnSalvar.TabIndex = 4
        Me.btnSalvar.Text = "Salvar"
        Me.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSalvar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Image = Global.SwLynx_4._1.My.Resources.Resources.cancelar
        Me.btnCancelar.Location = New System.Drawing.Point(1154, 11)
        Me.btnCancelar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(164, 51)
        Me.btnCancelar.TabIndex = 5
        Me.btnCancelar.Text = "Sair"
        Me.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'mnudgvProdutos
        '
        Me.mnudgvProdutos.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnudgvProdutos.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AbrirPDFFichaTecnicaToolStripMenuItem, Me.AbrirPDFIsometricoToolStripMenuItem})
        Me.mnudgvProdutos.Name = "mnudgvProdutos"
        Me.mnudgvProdutos.Size = New System.Drawing.Size(233, 52)
        '
        'AbrirPDFFichaTecnicaToolStripMenuItem
        '
        Me.AbrirPDFFichaTecnicaToolStripMenuItem.Name = "AbrirPDFFichaTecnicaToolStripMenuItem"
        Me.AbrirPDFFichaTecnicaToolStripMenuItem.Size = New System.Drawing.Size(232, 24)
        Me.AbrirPDFFichaTecnicaToolStripMenuItem.Text = "Abrir PDF Ficha Tecnica"
        '
        'AbrirPDFIsometricoToolStripMenuItem
        '
        Me.AbrirPDFIsometricoToolStripMenuItem.Name = "AbrirPDFIsometricoToolStripMenuItem"
        Me.AbrirPDFIsometricoToolStripMenuItem.Size = New System.Drawing.Size(232, 24)
        Me.AbrirPDFIsometricoToolStripMenuItem.Text = "Abrir PDF Isometrico"
        '
        'Timer1dgvProdutos
        '
        '
        'btnExcluir
        '
        Me.btnExcluir.Image = Global.SwLynx_4._1.My.Resources.Resources.excluir
        Me.btnExcluir.Location = New System.Drawing.Point(182, 11)
        Me.btnExcluir.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnExcluir.Name = "btnExcluir"
        Me.btnExcluir.Size = New System.Drawing.Size(164, 51)
        Me.btnExcluir.TabIndex = 57
        Me.btnExcluir.Text = "Excluir"
        Me.btnExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExcluir.UseVisualStyleBackColor = True
        '
        'frmCriaProdutos
        '
        Me.AcceptButton = Me.btnSalvar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(1326, 526)
        Me.Controls.Add(Me.btnExcluir)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnSalvar)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCriaProdutos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Criar Produtos com Base na OS  selecionada"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuimagens.ResumeLayout(False)
        Me.mnudgvProdutos.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents txtDescricaoProduto As Windows.Forms.TextBox
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents txtCodOmie As Windows.Forms.TextBox
    Friend WithEvents txCodDesenhoProduto As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents lblEnderecoFichaTecnica As Windows.Forms.Label
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents lblEnderecoIsometrico As Windows.Forms.Label
    Friend WithEvents Label15 As Windows.Forms.Label
    Friend WithEvents btnSalvar As Windows.Forms.Button
    Friend WithEvents btnCancelar As Windows.Forms.Button
    Friend WithEvents btnBuscarIsometrico As Windows.Forms.Button
    Friend WithEvents btnBuscarFichaTecnica As Windows.Forms.Button
    Friend WithEvents PictureBox1 As Windows.Forms.PictureBox
    Friend WithEvents mnuimagens As Windows.Forms.ContextMenuStrip
    Friend WithEvents BuscarImagensNoGoogleToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalvarAImagemComoReferenciaDoMaterialToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarImagemNaBibliotecaDoSistemaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ColarImagemToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer1dgvProdutos As Windows.Forms.Timer
    Friend WithEvents btnExcluir As Windows.Forms.Button
    Friend WithEvents mnudgvProdutos As Windows.Forms.ContextMenuStrip
    Friend WithEvents AbrirPDFFichaTecnicaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents AbrirPDFIsometricoToolStripMenuItem As Windows.Forms.ToolStripMenuItem
End Class
