<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmOpcaoLiberacaoOrdemServico
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
        Me.btnLiberarOrdemServico = New System.Windows.Forms.Button()
        Me.btnCANCELAR = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optNaoBaixaSaldoTag = New System.Windows.Forms.RadioButton()
        Me.optBaixaSaldoTag = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnLiberarOrdemServico
        '
        Me.btnLiberarOrdemServico.Location = New System.Drawing.Point(469, 121)
        Me.btnLiberarOrdemServico.Margin = New System.Windows.Forms.Padding(4)
        Me.btnLiberarOrdemServico.Name = "btnLiberarOrdemServico"
        Me.btnLiberarOrdemServico.Size = New System.Drawing.Size(155, 39)
        Me.btnLiberarOrdemServico.TabIndex = 10063
        Me.btnLiberarOrdemServico.Text = "Salvar"
        Me.btnLiberarOrdemServico.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLiberarOrdemServico.UseVisualStyleBackColor = True
        '
        'btnCANCELAR
        '
        Me.btnCANCELAR.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCANCELAR.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCANCELAR.Location = New System.Drawing.Point(632, 121)
        Me.btnCANCELAR.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCANCELAR.Name = "btnCANCELAR"
        Me.btnCANCELAR.Size = New System.Drawing.Size(155, 39)
        Me.btnCANCELAR.TabIndex = 10064
        Me.btnCANCELAR.Text = "Cancelar"
        Me.btnCANCELAR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCANCELAR.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.optNaoBaixaSaldoTag)
        Me.GroupBox1.Controls.Add(Me.optBaixaSaldoTag)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(783, 102)
        Me.GroupBox1.TabIndex = 10065
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Tipo Liberação da Ordem de Serviço"
        '
        'optNaoBaixaSaldoTag
        '
        Me.optNaoBaixaSaldoTag.AutoSize = True
        Me.optNaoBaixaSaldoTag.Location = New System.Drawing.Point(17, 76)
        Me.optNaoBaixaSaldoTag.Name = "optNaoBaixaSaldoTag"
        Me.optNaoBaixaSaldoTag.Size = New System.Drawing.Size(599, 20)
        Me.optNaoBaixaSaldoTag.TabIndex = 1
        Me.optNaoBaixaSaldoTag.Text = "Libera a Ordem de Serviço não da Baixa no saldo da Tag, Opção para OS de liberaçã" &
    "o parcial!"
        Me.optNaoBaixaSaldoTag.UseVisualStyleBackColor = True
        '
        'optBaixaSaldoTag
        '
        Me.optBaixaSaldoTag.AutoSize = True
        Me.optBaixaSaldoTag.Checked = True
        Me.optBaixaSaldoTag.Location = New System.Drawing.Point(17, 36)
        Me.optBaixaSaldoTag.Name = "optBaixaSaldoTag"
        Me.optBaixaSaldoTag.Size = New System.Drawing.Size(457, 20)
        Me.optBaixaSaldoTag.TabIndex = 0
        Me.optBaixaSaldoTag.TabStop = True
        Me.optBaixaSaldoTag.Text = "Libera a Ordem de Serviço e da Baixa na quantidade informada na Tag."
        Me.optBaixaSaldoTag.UseVisualStyleBackColor = True
        '
        'frmOpcaoLiberacaoOrdemServico
        '
        Me.AcceptButton = Me.btnLiberarOrdemServico
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCANCELAR
        Me.ClientSize = New System.Drawing.Size(800, 165)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCANCELAR)
        Me.Controls.Add(Me.btnLiberarOrdemServico)
        Me.Name = "frmOpcaoLiberacaoOrdemServico"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Opçao Liberação da Ordem Serviço e Controle de Tag"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnLiberarOrdemServico As Windows.Forms.Button
    Friend WithEvents btnCANCELAR As Windows.Forms.Button
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents optNaoBaixaSaldoTag As Windows.Forms.RadioButton
    Friend WithEvents optBaixaSaldoTag As Windows.Forms.RadioButton
End Class
