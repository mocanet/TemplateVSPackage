<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.MocaDi1 = New Moca.Win.MocaDi()
        Me.SuspendLayout()
        '
        'MocaDi1
        '
        Me.MocaDi1.AutoSize = True
        Me.MocaDi1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.MocaDi1.BackgroundImage = CType(resources.GetObject("MocaDi1.BackgroundImage"), System.Drawing.Image)
        Me.MocaDi1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.MocaDi1.Location = New System.Drawing.Point(0, 0)
        Me.MocaDi1.Margin = New System.Windows.Forms.Padding(0)
        Me.MocaDi1.MaximumSize = New System.Drawing.Size(16, 16)
        Me.MocaDi1.MinimumSize = New System.Drawing.Size(16, 16)
        Me.MocaDi1.Name = "MocaDi1"
        Me.MocaDi1.Size = New System.Drawing.Size(16, 16)
        Me.MocaDi1.TabIndex = 0
        Me.MocaDi1.TabStop = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.MocaDi1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MocaDi1 As Moca.Win.MocaDi
End Class
