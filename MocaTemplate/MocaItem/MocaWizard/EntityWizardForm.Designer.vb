<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EntityWizardForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EntityWizardForm))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.WizardControl1 = New AeroWizard.WizardControl()
        Me.wzpExecSQL = New AeroWizard.WizardPage()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboConnectionStrings = New System.Windows.Forms.ComboBox()
        Me.btnAddDataSource = New System.Windows.Forms.Button()
        Me.wzpGenerateSettings = New AeroWizard.WizardPage()
        Me.chkINotifyPropertyChanged = New System.Windows.Forms.CheckBox()
        Me.chkAutoImplementedProperties = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtClassName = New System.Windows.Forms.TextBox()
        Me.chkTable = New System.Windows.Forms.CheckBox()
        Me.txtTableName = New System.Windows.Forms.TextBox()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.chkDifinition = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNamespace = New System.Windows.Forms.TextBox()
        CType(Me.WizardControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.wzpExecSQL.SuspendLayout()
        Me.wzpGenerateSettings.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'WizardControl1
        '
        resources.ApplyResources(Me.WizardControl1, "WizardControl1")
        Me.WizardControl1.BackColor = System.Drawing.Color.White
        Me.WizardControl1.Name = "WizardControl1"
        Me.WizardControl1.Pages.Add(Me.wzpExecSQL)
        Me.WizardControl1.Pages.Add(Me.wzpGenerateSettings)
        '
        'wzpExecSQL
        '
        resources.ApplyResources(Me.wzpExecSQL, "wzpExecSQL")
        Me.wzpExecSQL.Controls.Add(Me.RichTextBox1)
        Me.wzpExecSQL.Controls.Add(Me.Label1)
        Me.wzpExecSQL.Controls.Add(Me.cboConnectionStrings)
        Me.wzpExecSQL.Controls.Add(Me.btnAddDataSource)
        Me.wzpExecSQL.Name = "wzpExecSQL"
        Me.wzpExecSQL.NextPage = Me.wzpGenerateSettings
        '
        'RichTextBox1
        '
        Me.RichTextBox1.AcceptsTab = True
        resources.ApplyResources(Me.RichTextBox1, "RichTextBox1")
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RichTextBox1.Name = "RichTextBox1"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'cboConnectionStrings
        '
        resources.ApplyResources(Me.cboConnectionStrings, "cboConnectionStrings")
        Me.cboConnectionStrings.FormattingEnabled = True
        Me.cboConnectionStrings.Name = "cboConnectionStrings"
        '
        'btnAddDataSource
        '
        resources.ApplyResources(Me.btnAddDataSource, "btnAddDataSource")
        Me.btnAddDataSource.Name = "btnAddDataSource"
        Me.btnAddDataSource.UseVisualStyleBackColor = True
        '
        'wzpGenerateSettings
        '
        resources.ApplyResources(Me.wzpGenerateSettings, "wzpGenerateSettings")
        Me.wzpGenerateSettings.Controls.Add(Me.chkINotifyPropertyChanged)
        Me.wzpGenerateSettings.Controls.Add(Me.chkAutoImplementedProperties)
        Me.wzpGenerateSettings.Controls.Add(Me.Label3)
        Me.wzpGenerateSettings.Controls.Add(Me.txtClassName)
        Me.wzpGenerateSettings.Controls.Add(Me.chkTable)
        Me.wzpGenerateSettings.Controls.Add(Me.txtTableName)
        Me.wzpGenerateSettings.Controls.Add(Me.DataGridView2)
        Me.wzpGenerateSettings.Controls.Add(Me.chkDifinition)
        Me.wzpGenerateSettings.Controls.Add(Me.Label2)
        Me.wzpGenerateSettings.Controls.Add(Me.txtNamespace)
        Me.wzpGenerateSettings.IsFinishPage = True
        Me.wzpGenerateSettings.Name = "wzpGenerateSettings"
        '
        'chkINotifyPropertyChanged
        '
        resources.ApplyResources(Me.chkINotifyPropertyChanged, "chkINotifyPropertyChanged")
        Me.chkINotifyPropertyChanged.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.chkINotifyPropertyChanged.Name = "chkINotifyPropertyChanged"
        Me.chkINotifyPropertyChanged.UseVisualStyleBackColor = True
        '
        'chkAutoImplementedProperties
        '
        resources.ApplyResources(Me.chkAutoImplementedProperties, "chkAutoImplementedProperties")
        Me.chkAutoImplementedProperties.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.chkAutoImplementedProperties.Name = "chkAutoImplementedProperties"
        Me.chkAutoImplementedProperties.UseVisualStyleBackColor = True
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Label3.Name = "Label3"
        '
        'txtClassName
        '
        resources.ApplyResources(Me.txtClassName, "txtClassName")
        Me.txtClassName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.txtClassName.Name = "txtClassName"
        '
        'chkTable
        '
        resources.ApplyResources(Me.chkTable, "chkTable")
        Me.chkTable.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.chkTable.Name = "chkTable"
        Me.chkTable.UseVisualStyleBackColor = True
        '
        'txtTableName
        '
        resources.ApplyResources(Me.txtTableName, "txtTableName")
        Me.txtTableName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.txtTableName.Name = "txtTableName"
        '
        'DataGridView2
        '
        resources.ApplyResources(Me.DataGridView2, "DataGridView2")
        Me.DataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Yu Gothic UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Yu Gothic UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView2.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView2.Name = "DataGridView2"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Yu Gothic UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView2.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView2.RowTemplate.Height = 21
        '
        'chkDifinition
        '
        resources.ApplyResources(Me.chkDifinition, "chkDifinition")
        Me.chkDifinition.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.chkDifinition.Name = "chkDifinition"
        Me.chkDifinition.UseVisualStyleBackColor = True
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Label2.Name = "Label2"
        '
        'txtNamespace
        '
        resources.ApplyResources(Me.txtNamespace, "txtNamespace")
        Me.txtNamespace.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.txtNamespace.Name = "txtNamespace"
        '
        'EntityWizardForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ControlBox = False
        Me.Controls.Add(Me.WizardControl1)
        Me.KeyPreview = True
        Me.Name = "EntityWizardForm"
        CType(Me.WizardControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.wzpExecSQL.ResumeLayout(False)
        Me.wzpExecSQL.PerformLayout()
        Me.wzpGenerateSettings.ResumeLayout(False)
        Me.wzpGenerateSettings.PerformLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WizardControl1 As AeroWizard.WizardControl
    Friend WithEvents wzpExecSQL As AeroWizard.WizardPage
    Friend WithEvents btnAddDataSource As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboConnectionStrings As System.Windows.Forms.ComboBox
    Friend WithEvents wzpGenerateSettings As AeroWizard.WizardPage
    Friend WithEvents chkTable As System.Windows.Forms.CheckBox
    Friend WithEvents txtTableName As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents chkDifinition As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNamespace As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtClassName As System.Windows.Forms.TextBox
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents chkAutoImplementedProperties As System.Windows.Forms.CheckBox
    Friend WithEvents chkINotifyPropertyChanged As System.Windows.Forms.CheckBox
End Class
