Imports System
Imports System.Runtime.InteropServices
Imports System.Threading
Imports Microsoft.VisualBasic
Imports Microsoft.VisualStudio.Shell
Imports Task = System.Threading.Tasks.Task


''' <summary>
''' This is the class that implements the package exposed by this assembly.
''' </summary>
''' <remarks>
''' <para>
''' The minimum requirement for a class to be considered a valid package for Visual Studio
''' Is to implement the IVsPackage interface And register itself with the shell.
''' This package uses the helper classes defined inside the Managed Package Framework (MPF)
''' to do it: it derives from the Package Class that provides the implementation Of the 
''' IVsPackage interface And uses the registration attributes defined in the framework to 
''' register itself And its components with the shell. These attributes tell the pkgdef creation
''' utility what data to put into .pkgdef file.
''' </para>
''' <para>
''' To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
''' </para>
''' </remarks>
<PackageRegistration(UseManagedResourcesOnly:=True, AllowsBackgroundLoading:=True)>
<InstalledProductRegistration("#110", "#112", MocaTemplateVSPackage.Version)>
<Guid(MocaTemplateVSPackage.PackageGuidString)>
Public NotInheritable Class MocaTemplateVSPackage
    Inherits AsyncPackage

    ''' <summary>
    ''' Package guid
    ''' </summary>
    Public Const PackageGuidString As String = "8f23ff00-7d14-4c97-9366-3f0d92fae82b"

    'Public Const Id As String = "6c799bc4-0d4c-4172-98bc-5d464b612dca"
    Public Const Name As String = "Moca.NET Template 3.0"
    Public Const Description As String = "Moca.NET framework Templates."
    Public Const Language As String = "en-US"
    Public Const Version As String = "4.0.0"
    Public Const Author As String = "MiYABiS"
    Public Const Tags As String = "Moca.NET, Templates"

#Region "Package Members"

    ''' <summary>
    ''' Initialization of the package; this method is called right after the package is sited, so this is the place
    ''' where you can put all the initialization code that rely on services provided by VisualStudio.
    ''' </summary>
    ''' <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
    ''' <param name="progress">A provider for progress updates.</param>
    ''' <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
    Protected Overrides Async Function InitializeAsync(cancellationToken As CancellationToken, progress As IProgress(Of ServiceProgressData)) As Task
        ' When initialized asynchronously, the current thread may be a background thread at this point.
        ' Do any initialization that requires the UI thread after switching to the UI thread.
        Await Me.JoinableTaskFactory.SwitchToMainThreadAsync()
    End Function

#End Region

End Class
