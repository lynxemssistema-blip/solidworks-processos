<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLogin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkSalvarDadosEntrada = New System.Windows.Forms.CheckBox()
        Me.chkMostrarSenha = New System.Windows.Forms.CheckBox()
        Me.btnEntrar = New System.Windows.Forms.Button()
        Me.mskSenha = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtLogin = New System.Windows.Forms.TextBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.btnFechar = New System.Windows.Forms.Button()
        Me.btnRecuperarSenha = New System.Windows.Forms.Button()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Image = CType(resources.GetObject("Label2.Image"), System.Drawing.Image)
        Me.Label2.Location = New System.Drawing.Point(27, 220)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 81)
        Me.Label2.TabIndex = 10083
        Me.Label2.Tag = "rotulo"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(119, 222)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(117, 16)
        Me.Label4.TabIndex = 10081
        Me.Label4.Text = "Senha de Acesso:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(119, 150)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 16)
        Me.Label3.TabIndex = 10080
        Me.Label3.Text = "Login do Usuário:"
        '
        'chkSalvarDadosEntrada
        '
        Me.chkSalvarDadosEntrada.AutoSize = True
        Me.chkSalvarDadosEntrada.BackColor = System.Drawing.Color.Transparent
        Me.chkSalvarDadosEntrada.Location = New System.Drawing.Point(117, 288)
        Me.chkSalvarDadosEntrada.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkSalvarDadosEntrada.Name = "chkSalvarDadosEntrada"
        Me.chkSalvarDadosEntrada.Size = New System.Drawing.Size(112, 20)
        Me.chkSalvarDadosEntrada.TabIndex = 10079
        Me.chkSalvarDadosEntrada.Text = "Salvar Dados"
        Me.chkSalvarDadosEntrada.UseVisualStyleBackColor = False
        '
        'chkMostrarSenha
        '
        Me.chkMostrarSenha.AutoSize = True
        Me.chkMostrarSenha.BackColor = System.Drawing.Color.Transparent
        Me.chkMostrarSenha.Location = New System.Drawing.Point(273, 288)
        Me.chkMostrarSenha.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkMostrarSenha.Name = "chkMostrarSenha"
        Me.chkMostrarSenha.Size = New System.Drawing.Size(112, 20)
        Me.chkMostrarSenha.TabIndex = 10078
        Me.chkMostrarSenha.Text = "Mostra Senha"
        Me.chkMostrarSenha.UseVisualStyleBackColor = False
        '
        'btnEntrar
        '
        Me.btnEntrar.Location = New System.Drawing.Point(24, 329)
        Me.btnEntrar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnEntrar.Name = "btnEntrar"
        Me.btnEntrar.Size = New System.Drawing.Size(155, 39)
        Me.btnEntrar.TabIndex = 10077
        Me.btnEntrar.Text = "Entrar"
        Me.btnEntrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnEntrar.UseVisualStyleBackColor = True
        '
        'mskSenha
        '
        Me.mskSenha.Location = New System.Drawing.Point(117, 247)
        Me.mskSenha.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.mskSenha.Name = "mskSenha"
        Me.mskSenha.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.mskSenha.Size = New System.Drawing.Size(277, 22)
        Me.mskSenha.TabIndex = 10075
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Image = CType(resources.GetObject("Label1.Image"), System.Drawing.Image)
        Me.Label1.Location = New System.Drawing.Point(27, 134)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 81)
        Me.Label1.TabIndex = 10073
        Me.Label1.Tag = "rotulo"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtLogin
        '
        Me.txtLogin.Location = New System.Drawing.Point(117, 171)
        Me.txtLogin.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtLogin.Name = "txtLogin"
        Me.txtLogin.Size = New System.Drawing.Size(277, 22)
        Me.txtLogin.TabIndex = 10088
        '
        'PictureBox2
        '
        Me.PictureBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(15, 14)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(403, 106)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 10089
        Me.PictureBox2.TabStop = False
        '
        'btnFechar
        '
        Me.btnFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnFechar.Location = New System.Drawing.Point(241, 329)
        Me.btnFechar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnFechar.Name = "btnFechar"
        Me.btnFechar.Size = New System.Drawing.Size(155, 39)
        Me.btnFechar.TabIndex = 10076
        Me.btnFechar.Text = "Fechar"
        Me.btnFechar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnFechar.UseVisualStyleBackColor = True
        '
        'btnRecuperarSenha
        '
        Me.btnRecuperarSenha.Location = New System.Drawing.Point(24, 382)
        Me.btnRecuperarSenha.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnRecuperarSenha.Name = "btnRecuperarSenha"
        Me.btnRecuperarSenha.Size = New System.Drawing.Size(372, 39)
        Me.btnRecuperarSenha.TabIndex = 10090
        Me.btnRecuperarSenha.Text = "Recuperar senha de Acesso!"
        Me.btnRecuperarSenha.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnRecuperarSenha.UseVisualStyleBackColor = True
        '
        'frmLogin
        '
        Me.AcceptButton = Me.btnEntrar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnFechar
        Me.ClientSize = New System.Drawing.Size(432, 437)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnRecuperarSenha)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.txtLogin)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.chkSalvarDadosEntrada)
        Me.Controls.Add(Me.chkMostrarSenha)
        Me.Controls.Add(Me.btnEntrar)
        Me.Controls.Add(Me.btnFechar)
        Me.Controls.Add(Me.mskSenha)
        Me.Controls.Add(Me.Label1)
        Me.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents chkSalvarDadosEntrada As Windows.Forms.CheckBox
    Friend WithEvents chkMostrarSenha As Windows.Forms.CheckBox
    Friend WithEvents btnEntrar As Windows.Forms.Button
    Friend WithEvents mskSenha As Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents txtLogin As Windows.Forms.TextBox
    Friend WithEvents PictureBox2 As Windows.Forms.PictureBox
    Friend WithEvents btnFechar As Windows.Forms.Button
    Friend WithEvents btnRecuperarSenha As Windows.Forms.Button
End Class
