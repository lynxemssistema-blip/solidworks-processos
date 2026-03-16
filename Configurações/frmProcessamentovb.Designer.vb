<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProcessamentovb
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
        Me.ProgressBarProcessamento = New System.Windows.Forms.ProgressBar()
        Me.lblTituloProcessamento = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ProgressBarProcessamento
        '
        Me.ProgressBarProcessamento.ForeColor = System.Drawing.Color.Green
        Me.ProgressBarProcessamento.Location = New System.Drawing.Point(12, 48)
        Me.ProgressBarProcessamento.Name = "ProgressBarProcessamento"
        Me.ProgressBarProcessamento.Size = New System.Drawing.Size(764, 47)
        Me.ProgressBarProcessamento.TabIndex = 0
        '
        'lblTituloProcessamento
        '
        Me.lblTituloProcessamento.AutoSize = True
        Me.lblTituloProcessamento.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTituloProcessamento.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTituloProcessamento.Location = New System.Drawing.Point(12, 19)
        Me.lblTituloProcessamento.Name = "lblTituloProcessamento"
        Me.lblTituloProcessamento.Size = New System.Drawing.Size(77, 25)
        Me.lblTituloProcessamento.TabIndex = 1
        Me.lblTituloProcessamento.Text = "Label1"
        '
        'frmProcessamentovb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 107)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblTituloProcessamento)
        Me.Controls.Add(Me.ProgressBarProcessamento)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmProcessamentovb"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Processamento"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ProgressBarProcessamento As Windows.Forms.ProgressBar
    Friend WithEvents lblTituloProcessamento As Windows.Forms.Label
End Class
