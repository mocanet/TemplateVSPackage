
Imports System.Windows.Forms

Public Class SeleniumPageWizardForm

    Public Property ClassName As String

    Public Property [Namespace] As String

    Public Property VsMgr As VsManager

    Private _gen As SeleniumPageCodeGenerator

    Public ReadOnly Property gen As SeleniumPageCodeGenerator
        Get
            If _gen Is Nothing Then
                _gen = New SeleniumPageCodeGenerator
            End If
            Return _gen
        End Get
    End Property

    Private Sub SeleniumPageWizardForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.txtNamespace.Text = Me.Namespace
        Me.txtClassName.Text = Me.ClassName
        CancelButton = btnCancel
        Me.txtURL.Focus()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If Me.txtClassName.Text.Length = 0 Then
            System.Windows.Forms.MessageBox.Show("Input Class Name")
            Me.txtClassName.Focus()
            Return
        End If

        If grdvElements.DataSource Is Nothing Then
            System.Windows.Forms.MessageBox.Show("Get Page")
            Return
        End If

        Try
            gen.Namespace = Me.txtNamespace.Text
            gen.ClassName = Me.txtClassName.Text
            gen.Generate(grdvElements.DataSource)
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.ToString)
        End Try

        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub btnGet_Click(sender As Object, e As EventArgs) Handles btnGet.Click
        WebBrowser1.Url = New Uri(Me.txtURL.Text)
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        Try
            Cursor = Windows.Forms.Cursors.WaitCursor
            grdvElements.SuspendLayout()

            Dim target As New HtmlAnalyzer()
            Dim lst As IList(Of XElementRow)
            lst = target.GetPage(WebBrowser1.DocumentStream)
            grdvElements.DataSource = lst
            grdvElements.AutoSizeColumnsMode = Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.ToString)
        Finally
            grdvElements.ResumeLayout()
            Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub

End Class
