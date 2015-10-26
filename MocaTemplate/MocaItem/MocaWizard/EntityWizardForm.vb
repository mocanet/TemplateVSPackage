
Imports System.Windows.Forms
Imports Moca.Db

Public Class EntityWizardForm

    Private _dbmsWk As Dbms

    Private _dao As Db.Impl.DaoGetter

    Private _dt As DataTable

    Private _gen As EntityCodeGenerator

    Public Property ClassName As String

    Public Property Properties As EntityWizardProperties

    Public Property [Namespace] As String

    Public Property VsMgr As VsManager

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        _dao.Dispose()
    End Sub

    Public ReadOnly Property gen As EntityCodeGenerator
        Get
            If _gen Is Nothing Then
                _gen = New EntityCodeGenerator
            End If
            Return _gen
        End Get
    End Property

    Private Sub EntityWizardForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.txtNamespace.Text = Me.Namespace
        Me.txtClassName.Text = Me.ClassName
        Me.txtTableName.Text = Me.ClassName
        Me.chkAutoImplementedProperties.Checked = True
        Me.txtTableName.Enabled = False

        _setCbo()
        If Not String.IsNullOrEmpty(Me.Properties.SelectedConnection) Then
            Me.cboConnectionStrings.Text = Me.Properties.SelectedConnection
        End If
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        ' Ctrl + Enter をボタンのショートカットキーとして処理する
        If keyData = Keys.Control + Keys.Enter Then
            Me.WizardControl1.NextPage()
            Return True
        End If
        ' Ctrl + S をボタンのショートカットキーとして処理する
        If keyData = Keys.Control + Keys.S Then
            Me.WizardControl1.NextPage()
            Return True
        End If
        ' Ctrl + O をボタンのショートカットキーとして処理する
        If keyData = Keys.Control + Keys.O Then
            Me.WizardControl1.NextPage()
            Return True
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub btnAddDataSource_Click(sender As Object, e As EventArgs) Handles btnAddDataSource.Click
        If VsMgr.AddDataConnection() Is Nothing Then
            Return
        End If
        _setCbo()
    End Sub

    Private Sub wzpExecSQL_Commit(sender As Object, e As AeroWizard.WizardPageConfirmEventArgs) Handles wzpExecSQL.Commit
        If Me.cboConnectionStrings.SelectedIndex < 0 Then
            e.Cancel = True
            System.Windows.Forms.MessageBox.Show("Not selected data source." & vbCrLf & "Please set it with a data source again.")
            Return
        End If

        If Not _canConnectDb() Then
            e.Cancel = True
            System.Windows.Forms.MessageBox.Show("Not connected to the database." & vbCrLf & "Please set it with a data source again.")
            Return
        End If

        If Me.RichTextBox1.Text.Length = 0 Then
            System.Windows.Forms.MessageBox.Show("Input SQL")
            Me.RichTextBox1.Focus()
            e.Cancel = True
            Return
        End If

        Dim sql As String
        sql = Me.RichTextBox1.Text

        Try
            _dt = _dao.Go(sql)
            _setGenerate()
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.ToString)
            e.Cancel = True
        End Try
    End Sub

    Private Sub wzpGenerateSettings_Commit(sender As Object, e As AeroWizard.WizardPageConfirmEventArgs) Handles wzpGenerateSettings.Commit
        If Me.txtClassName.Text.Length = 0 Then
            System.Windows.Forms.MessageBox.Show("Input Class Name")
            Me.txtClassName.Focus()
            e.Cancel = True
            Return
        End If
        If Me.chkDifinition.Checked Then
            If Me.txtTableName.Text.Length = 0 Then
                System.Windows.Forms.MessageBox.Show("Input TableName Name")
                Me.txtTableName.Focus()
                e.Cancel = True
                Return
            End If
        End If

        Try
            Dim dt As DataTable

            dt = Me.DataGridView2.DataSource

            gen.Namespace = Me.txtNamespace.Text
            gen.ClassName = Me.txtClassName.Text
            gen.AutoImplementedProperties = Me.chkAutoImplementedProperties.Checked
            gen.INotifyPropertyChangedBase = Me.chkINotifyPropertyChanged.Checked
            gen.TableProperty = Me.chkTable.Checked
            If Me.chkDifinition.Checked Then
                gen.TableName = Me.txtTableName.Text
                gen.Generate(_dt.Columns, dt.Rows)
            Else
                gen.Generate(_dt.Columns)
            End If
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.ToString)
            e.Cancel = True
        End Try
    End Sub

    Private Sub chkDifinition_CheckedChanged(sender As Object, e As EventArgs) Handles chkDifinition.CheckedChanged
        Me.txtTableName.Enabled = Me.chkDifinition.Checked
    End Sub

    Private Sub _setCbo()
        Me.cboConnectionStrings.BeginUpdate()
        Me.cboConnectionStrings.DataSource = _vsMgr.DbContextList
        Me.cboConnectionStrings.DisplayMember = "Name"
        Me.cboConnectionStrings.ValueMember = "ConnectionString"
        Me.cboConnectionStrings.EndUpdate()
    End Sub

    Private Sub _setGenerate()
        Me.DataGridView2.AllowUserToAddRows = False

        Dim dt As DataTable

        dt = New DataTable("")
        dt.Columns.Add("ColumnName")

        For Each col As DataColumn In _dt.Columns
            Dim row As DataRow = dt.NewRow()
            dt.Rows.Add(row)
            row.Item(0) = col.ColumnName
        Next

        Me.DataGridView2.DataSource = dt
    End Sub

    ''' <summary>
    ''' DB接続可能かチェックする
    ''' </summary>
    ''' <remarks></remarks>
    Private Function _canConnectDb() As Boolean
        Try
            Dim dbContext As DbContextProperty

            dbContext = Me.cboConnectionStrings.SelectedItem
            _dbmsWk = New Dbms(dbContext.Name, dbContext.ProviderName, dbContext.ConnectionString)

            _dao = New Db.Impl.DaoGetter(_dbmsWk)
            _dao.Connection.Open()
            _dao.Connection.Close()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

End Class
