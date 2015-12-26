
Imports System.IO
Imports EnvDTE
Imports Microsoft.VisualStudio.TemplateWizard

Public Class SeleniumPageWizard
    Implements IWizard

#Region " Declare "

    Private _vsMgr As VsManager

#End Region

#Region " Implements "

    Public Sub BeforeOpeningFile(projectItem As ProjectItem) Implements IWizard.BeforeOpeningFile
    End Sub

    Public Sub ProjectFinishedGenerating(project As Project) Implements IWizard.ProjectFinishedGenerating
    End Sub

    Public Sub ProjectItemFinishedGenerating(projectItem As ProjectItem) Implements IWizard.ProjectItemFinishedGenerating
    End Sub

    Public Sub RunFinished() Implements IWizard.RunFinished
    End Sub

    Public Sub RunStarted(automationObject As Object, replacementsDictionary As Dictionary(Of String, String), runKind As WizardRunKind, customParams() As Object) Implements IWizard.RunStarted
        Try
            _vsMgr = New VsManager(automationObject)

            Dim classNamespace As String = String.Empty
            Dim dir As String = String.Empty
            Dim project As EnvDTE.Project = Nothing
            Dim projectItem As EnvDTE.ProjectItem = Nothing
            For Each item As EnvDTE.SelectedItem In _vsMgr.DTE.SelectedItems
                If item.Project IsNot Nothing Then
                    project = item.Project
                    dir = Path.GetDirectoryName(project.FullName)
                End If
                If item.ProjectItem IsNot Nothing Then
                    projectItem = item.ProjectItem
                    project = projectItem.ContainingProject
                    dir = projectItem.Properties.Item("FullPath").Value
                    dir = dir.Substring(0, dir.Length - 1)
                    classNamespace = dir.Replace(Path.GetDirectoryName(project.FullName) & Path.DirectorySeparatorChar, String.Empty)
                    classNamespace = classNamespace.Replace("\", ".")
                End If
            Next

            Dim language As EntityCodeGenerator.LanguageType
            Select Case project.CodeModel.Language
                Case "{B5E9BD34-6D3E-4B5D-925E-8A43B79820B4}"
                    language = EntityCodeGenerator.LanguageType.CSharp
                Case "{B5E9BD33-6D3E-4B5D-925E-8A43B79820B4}"
                    language = EntityCodeGenerator.LanguageType.VisualBasic
                Case Else
                    _vsMgr.DTE.StatusBar.Text = "Not applicable to this language."
                    _vsMgr.DTE.StatusBar.Highlight(True)
                    Throw New Exception("Not applicable to this language.")
            End Select

            Dim className As String = replacementsDictionary("$rootname$")
            If replacementsDictionary.ContainsKey("$safeitemname$") Then
                className = replacementsDictionary("$safeitemname$")
            End If
            If Not String.IsNullOrEmpty(className) Then
                className = Path.GetFileNameWithoutExtension(className)
            End If

            Using frm As New SeleniumPageWizardForm
                frm.VsMgr = _vsMgr
                frm.ClassName = className
                frm.Namespace = classNamespace
                frm.gen.Language = language
                If frm.ShowDialog() = System.Windows.Forms.DialogResult.Cancel Then
                    Return
                End If

                Dim filename As String
                filename = frm.gen.Write(dir, language)

                Dim targetProjectItem As EnvDTE.ProjectItem
                If projectItem IsNot Nothing Then
                    targetProjectItem = projectItem.ProjectItems.AddFromFile(filename)
                Else
                    targetProjectItem = project.ProjectItems.AddFromFile(filename)
                End If

                Dim fileWindow As EnvDTE.Window
                fileWindow = targetProjectItem.Open(EnvDTE.Constants.vsViewKindCode)
                fileWindow.Activate()
            End Using
        Catch ex As Exception
            If _vsMgr IsNot Nothing Then
                _vsMgr.BuildOutputString(ex.ToString)
            End If
        End Try
    End Sub

    Public Function ShouldAddProjectItem(filePath As String) As Boolean Implements IWizard.ShouldAddProjectItem
        Return True
    End Function

#End Region

End Class
