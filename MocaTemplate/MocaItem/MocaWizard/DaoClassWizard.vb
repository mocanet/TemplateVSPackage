
Imports System.IO
Imports EnvDTE
Imports Microsoft.VisualStudio.TemplateWizard

Public Class DaoClassWizard
    Implements IWizard

#Region " Declare "

    Private _vsMgr As VsManager

#End Region

    Public Sub RunStarted(automationObject As Object, replacementsDictionary As Dictionary(Of String, String), runKind As WizardRunKind, customParams() As Object) Implements IWizard.RunStarted
        Dim project As EnvDTE.Project = Nothing
        Dim projectItem As EnvDTE.ProjectItem = Nothing

        _vsMgr = New VsManager(automationObject)

        For Each item As EnvDTE.SelectedItem In _vsMgr.DTE.SelectedItems
            If item.ProjectItem IsNot Nothing Then
                projectItem = item.ProjectItem
                project = projectItem.ContainingProject
            End If
        Next

        Const C_FILENAME As String = "Moca.config.user"
        Dim configName As String = Path.Combine(Path.GetDirectoryName(project.FullName), C_FILENAME)
        Dim prop As EntityWizardProperties

        If File.Exists(configName) Then
            Using sr As New System.IO.StreamReader(configName, New System.Text.UTF8Encoding(False))
                Dim serializer As New System.Xml.Serialization.XmlSerializer(GetType(EntityWizardProperties))
                prop = DirectCast(serializer.Deserialize(sr), EntityWizardProperties)
            End Using
        Else
            prop = New EntityWizardProperties
        End If

        replacementsDictionary.Add("$ConnectionSettingsName$",
                                   IIf(String.IsNullOrEmpty(prop.ConnectionSettingsName),
                                       My.Resources.TableAttributeArg1,
                                       prop.ConnectionSettingsName)
                                   )
    End Sub

    Public Sub ProjectFinishedGenerating(project As Project) Implements IWizard.ProjectFinishedGenerating
    End Sub

    Public Sub ProjectItemFinishedGenerating(projectItem As ProjectItem) Implements IWizard.ProjectItemFinishedGenerating
    End Sub

    Public Sub BeforeOpeningFile(projectItem As ProjectItem) Implements IWizard.BeforeOpeningFile
    End Sub

    Public Sub RunFinished() Implements IWizard.RunFinished
    End Sub

    Public Function ShouldAddProjectItem(filePath As String) As Boolean Implements IWizard.ShouldAddProjectItem
        Return True
    End Function

End Class
