
Imports Microsoft.VisualStudio.TemplateWizard


Public Class JsAjaxMin
	Implements IWizard

	Private _file As EnvDTE.ProjectItem
	Private _minFile As EnvDTE.ProjectItem

	Public Sub BeforeOpeningFile(projectItem As EnvDTE.ProjectItem) Implements Microsoft.VisualStudio.TemplateWizard.IWizard.BeforeOpeningFile

	End Sub

	Public Sub ProjectFinishedGenerating(project As EnvDTE.Project) Implements Microsoft.VisualStudio.TemplateWizard.IWizard.ProjectFinishedGenerating

	End Sub

	Public Sub ProjectItemFinishedGenerating(projectItem As EnvDTE.ProjectItem) Implements Microsoft.VisualStudio.TemplateWizard.IWizard.ProjectItemFinishedGenerating
		If projectItem.Name.EndsWith(".min.js") Then
			_minFile = projectItem
		ElseIf projectItem.Name.EndsWith(".min.css") Then
			_minFile = projectItem
		Else
			_file = projectItem
		End If
	End Sub

	Public Sub RunFinished() Implements Microsoft.VisualStudio.TemplateWizard.IWizard.RunFinished
		If _file Is Nothing And _minFile Is Nothing Then
			Return
		End If

		_file.ProjectItems.AddFromFile(_minFile.FileNames(0))
		_file.Properties.Item("ItemType").Value = "AjaxMin"
	End Sub

	Public Sub RunStarted(automationObject As Object, replacementsDictionary As System.Collections.Generic.Dictionary(Of String, String), runKind As Microsoft.VisualStudio.TemplateWizard.WizardRunKind, customParams() As Object) Implements Microsoft.VisualStudio.TemplateWizard.IWizard.RunStarted

	End Sub

	Public Function ShouldAddProjectItem(filePath As String) As Boolean Implements Microsoft.VisualStudio.TemplateWizard.IWizard.ShouldAddProjectItem
		Return True
	End Function

End Class
