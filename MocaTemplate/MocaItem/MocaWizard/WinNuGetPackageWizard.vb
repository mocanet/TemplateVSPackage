Imports Microsoft.VisualStudio.TemplateWizard

Public Class WinNuGetPackageWizard
	Implements IWizard

	Private _project As EnvDTE.Project

	Public Sub BeforeOpeningFile(projectItem As EnvDTE.ProjectItem) Implements Microsoft.VisualStudio.TemplateWizard.IWizard.BeforeOpeningFile

	End Sub

	Public Sub ProjectFinishedGenerating(project As EnvDTE.Project) Implements Microsoft.VisualStudio.TemplateWizard.IWizard.ProjectFinishedGenerating
		_project = project
	End Sub

	Public Sub ProjectItemFinishedGenerating(projectItem As EnvDTE.ProjectItem) Implements Microsoft.VisualStudio.TemplateWizard.IWizard.ProjectItemFinishedGenerating

	End Sub

	Public Sub RunFinished() Implements Microsoft.VisualStudio.TemplateWizard.IWizard.RunFinished
		Dim dic As New Dictionary(Of String, String)
		Dim nugetPkg As NuGetPackage

		nugetPkg = New NuGetPackage()

        dic.Add(My.Settings.Log4net, My.Settings.Log4netVer)
        dic.Add(My.Settings.MocaNETWindowsFormProject, Nothing)

        nugetPkg.Install(_project, dic)
	End Sub

	Public Sub RunStarted(automationObject As Object, replacementsDictionary As System.Collections.Generic.Dictionary(Of String, String), runKind As Microsoft.VisualStudio.TemplateWizard.WizardRunKind, customParams() As Object) Implements Microsoft.VisualStudio.TemplateWizard.IWizard.RunStarted
	End Sub

	Public Function ShouldAddProjectItem(filePath As String) As Boolean Implements Microsoft.VisualStudio.TemplateWizard.IWizard.ShouldAddProjectItem
		Return True
	End Function

End Class
