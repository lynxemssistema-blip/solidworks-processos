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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOpcaoLiberacaoOrdemServico))
        Me.btnLiberarOrdemServico = New System.Windows.Forms.Button()
        Me.btnCANCELAR = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optNaoBaixaSaldoTag = New System.Windows.Forms.RadioButton()
        Me.optBaixaSaldoTag = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAjuda = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnLiberarOrdemServico
        '
        Me.btnLiberarOrdemServico.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLiberarOrdemServico.Location = New System.Drawing.Point(352, 96)
        Me.btnLiberarOrdemServico.Name = "btnLiberarOrdemServico"
        Me.btnLiberarOrdemServico.Size = New System.Drawing.Size(116, 32)
        Me.btnLiberarOrdemServico.TabIndex = 10063
        Me.btnLiberarOrdemServico.Text = "Salvar"
        Me.btnLiberarOrdemServico.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLiberarOrdemServico.UseVisualStyleBackColor = True
        '
        'btnCANCELAR
        '
        Me.btnCANCELAR.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCANCELAR.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCANCELAR.Location = New System.Drawing.Point(474, 96)
        Me.btnCANCELAR.Name = "btnCANCELAR"
        Me.btnCANCELAR.Size = New System.Drawing.Size(116, 32)
        Me.btnCANCELAR.TabIndex = 10064
        Me.btnCANCELAR.Text = "Cancelar"
        Me.btnCANCELAR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCANCELAR.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.optNaoBaixaSaldoTag)
        Me.GroupBox1.Controls.Add(Me.optBaixaSaldoTag)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 10)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(587, 74)
        Me.GroupBox1.TabIndex = 10065
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Tipo Liberação da Ordem de Serviço"
        '
        'optNaoBaixaSaldoTag
        '
        Me.optNaoBaixaSaldoTag.AutoSize = True
        Me.optNaoBaixaSaldoTag.Location = New System.Drawing.Point(13, 50)
        Me.optNaoBaixaSaldoTag.Margin = New System.Windows.Forms.Padding(2)
        Me.optNaoBaixaSaldoTag.Name = "optNaoBaixaSaldoTag"
        Me.optNaoBaixaSaldoTag.Size = New System.Drawing.Size(530, 17)
        Me.optNaoBaixaSaldoTag.TabIndex = 1
        Me.optNaoBaixaSaldoTag.Text = "Opção 02 - Libera a Ordem de Serviço não da Baixa no saldo da Tag, Opção para OS " &
    "de liberação parcial!"
        Me.optNaoBaixaSaldoTag.UseVisualStyleBackColor = True
        '
        'optBaixaSaldoTag
        '
        Me.optBaixaSaldoTag.AutoSize = True
        Me.optBaixaSaldoTag.Checked = True
        Me.optBaixaSaldoTag.Location = New System.Drawing.Point(13, 29)
        Me.optBaixaSaldoTag.Margin = New System.Windows.Forms.Padding(2)
        Me.optBaixaSaldoTag.Name = "optBaixaSaldoTag"
        Me.optBaixaSaldoTag.Size = New System.Drawing.Size(420, 17)
        Me.optBaixaSaldoTag.TabIndex = 0
        Me.optBaixaSaldoTag.TabStop = True
        Me.optBaixaSaldoTag.Text = "Opção 01 - Libera a Ordem de Serviço e da Baixa na quantidade informada na Tag."
        Me.optBaixaSaldoTag.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.Linen
        Me.Label1.Location = New System.Drawing.Point(4, 89)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(587, 0)
        Me.Label1.TabIndex = 10066
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'btnAjuda
        '
        Me.btnAjuda.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAjuda.Location = New System.Drawing.Point(230, 96)
        Me.btnAjuda.Name = "btnAjuda"
        Me.btnAjuda.Size = New System.Drawing.Size(116, 32)
        Me.btnAjuda.TabIndex = 10067
        Me.btnAjuda.Text = "Ajuda"
        Me.btnAjuda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAjuda.UseVisualStyleBackColor = True
        '
        'frmOpcaoLiberacaoOrdemServico
        '
        Me.AcceptButton = Me.btnLiberarOrdemServico
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCANCELAR
        Me.ClientSize = New System.Drawing.Size(600, 139)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnAjuda)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCANCELAR)
        Me.Controls.Add(Me.btnLiberarOrdemServico)
        Me.Margin = New System.Windows.Forms.Padding(2)
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
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents btnAjuda As Windows.Forms.Button
End Class
