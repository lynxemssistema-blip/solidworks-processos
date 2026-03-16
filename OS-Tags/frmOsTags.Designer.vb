<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOsTags
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
        Me.dgvTags = New System.Windows.Forms.DataGridView()
        Me.TimerOsTags = New System.Windows.Forms.Timer(Me.components)
        CType(Me.dgvTags, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvTags
        '
        Me.dgvTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTags.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTags.Location = New System.Drawing.Point(0, 0)
        Me.dgvTags.Name = "dgvTags"
        Me.dgvTags.RowHeadersWidth = 51
        Me.dgvTags.RowTemplate.Height = 24
        Me.dgvTags.Size = New System.Drawing.Size(754, 619)
        Me.dgvTags.TabIndex = 0
        '
        'frmOsTags
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(754, 619)
        Me.Controls.Add(Me.dgvTags)
        Me.Name = "frmOsTags"
        Me.Text = "Lista de OS por Tag"
        CType(Me.dgvTags, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvTags As Windows.Forms.DataGridView
    Friend WithEvents TimerOsTags As Windows.Forms.Timer
End Class
