
Public Class NuGetPackage

	Public Sub Install(ByVal project As EnvDTE.Project, ByVal packages As IDictionary(Of String, String))
		Const C_MSG As String = "Installing NuGet Packages"
		Const C_MSG_DETAIL As String = C_MSG & " {0} {1} ..."
		Dim source As String
		Dim componentModel As Microsoft.VisualStudio.ComponentModelHost.IComponentModel
		Dim installer As NuGet.VisualStudio.IVsPackageInstaller

		Dim typeNuGetConstants As Type = Type.GetType(My.Settings.NuGetConstantsType, True)
		source = CType(typeNuGetConstants.GetField(My.Settings.NuGetDefaultFeedUrl).GetValue(Nothing), String)

		componentModel = Microsoft.VisualStudio.Shell.Package.GetGlobalService(GetType(Microsoft.VisualStudio.ComponentModelHost.SComponentModel))
		installer = componentModel.GetService(Of NuGet.VisualStudio.IVsPackageInstaller)()
		If installer Is Nothing Then
			Return
		End If

		Dim cnt As Integer = 0
		For Each packageName As String In packages.Keys
			Dim packageVersion As String = packages(packageName)
			Dim msg As String = String.Format(C_MSG_DETAIL, packageName, packageVersion)
			'project.DTE.StatusBar.Text = msg
			project.DTE.StatusBar.Progress(True, msg, cnt, packages.Count)
			installer.InstallPackage(source, project, packageName, packageVersion, False)
			cnt += 1
			project.DTE.StatusBar.Progress(True, msg, cnt, packages.Count)
		Next

		project.DTE.StatusBar.Progress(False)
	End Sub

End Class
