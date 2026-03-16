<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIAAnaliseTecnica
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
        Me.txtConversa = New CADImport.CADImportForms.RichTextBoxEx()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtConversa
        '
        Me.txtConversa.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtConversa.Location = New System.Drawing.Point(12, 25)
        Me.txtConversa.Name = "txtConversa"
        Me.txtConversa.Size = New System.Drawing.Size(776, 772)
        Me.txtConversa.TabIndex = 11
        Me.txtConversa.Text = ""
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(9, 9)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(55, 13)
        Me.Label19.TabIndex = 8
        Me.Label19.Text = "Resposta:"
        '
        'frmIAAnaliseTecnica
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 809)
        Me.Controls.Add(Me.txtConversa)
        Me.Controls.Add(Me.Label19)
        Me.Name = "frmIAAnaliseTecnica"
        Me.Text = "Asistente de analise da Engenharia Mecanica"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtConversa As CADImport.CADImportForms.RichTextBoxEx
    Friend WithEvents Label19 As Windows.Forms.Label
End Class
