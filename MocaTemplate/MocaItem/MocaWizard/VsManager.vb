
Imports Microsoft.VisualStudio.Data
Imports Microsoft.VisualStudio.Data.Interop
Imports Microsoft.VisualStudio.Shell

''' <summary>
''' VSの各種管理
''' </summary>
''' <remarks></remarks>
Public Class VsManager

#Region " Declare "

    Const BUILD_OUTPUT_PANE_GUID As String = "{1BD8A850-02D1-11D1-BEE7-00A0C913D1F8}"

    Private _dte As EnvDTE.DTE

    Private _buildOutputWindowPane As EnvDTE.OutputWindowPane

    Private _lstDbProp As IList(Of DbContextProperty)

    Private _IVsDataConnectionsService As IVsDataConnectionsService
    Private _IVsDataConnectionDialogFactory As IVsDataConnectionDialogFactory
    Private _IVsDataConnectionDialog As IVsDataConnectionDialog
    Private _IVsDataExplorerConnectionManager As Microsoft.VisualStudio.Data.Services.IVsDataExplorerConnectionManager
    Private _IVsDataProviderManager As IVsDataProviderManager
    Private _IVsDataProvider As IVsDataProvider
    Private _IVsDataSourceManager As IVsDataSourceManager
    Private _IVsDataSource As IVsDataSource
    Private _IVsDataConnectionManager As IVsDataConnectionManager
    Private _IVsDataConnection As IVsDataConnection

#End Region

#Region " コンストラクタ "

    Public Sub New(ByVal automationObject As Object)
        _dte = DirectCast(automationObject, EnvDTE.DTE)

        _getBuildOutputWindowPane()

        _getDataInformation()
        _getConnectionsService()
        _getDataDialog()
    End Sub

#End Region

#Region " Property "

    Public ReadOnly Property DTE As EnvDTE.DTE
        Get
            Return _dte
        End Get
    End Property

    Public ReadOnly Property DbContextList As IList(Of DbContextProperty)
        Get
            Return _lstDbProp
        End Get
    End Property

    Public ReadOnly Property ConnectionsService As IVsDataConnectionsService
        Get
            Return _IVsDataConnectionsService
        End Get
    End Property

#End Region

#Region " Method "

    Public Function GetService(ByVal type As System.Type) As Object
        Return GetService(type.GUID)
    End Function

    Public Function GetService(ByVal guid As System.Guid) As Object
        Return _getService(_dte, guid)
    End Function

    Private Function GetService(ByVal serviceProvider As Object, ByVal type As System.Type) As Object
        Return _getService(serviceProvider, type.GUID)
    End Function

    ''' <summary>
    ''' 出力のビルドへメッセージ追加
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <remarks></remarks>
    Public Sub BuildOutputString(ByVal msg As String)
        _buildOutputWindowPane.OutputString(msg)
        _buildOutputWindowPane.OutputString(vbCrLf)
    End Sub

    ''' <summary>
    ''' サーバーエクスプローラーのデータ接続を追加する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddDataConnection() As IVsDataConnection
        Try
            _IVsDataConnectionDialog.AddAllSources()
            Dim objIVsDataConnection As IVsDataConnection
            objIVsDataConnection = _IVsDataConnectionDialog.ShowDialogAndConnect()
            If objIVsDataConnection Is Nothing Then
                Return Nothing
            End If
            If _IVsDataConnectionDialog.SaveSelection Then
                _IVsDataConnectionDialog.SaveProviderSelections()
                _IVsDataConnectionDialog.SaveSourceSelection()
            End If

            _IVsDataExplorerConnectionManager.AddConnection(Nothing, objIVsDataConnection.Provider, objIVsDataConnection.EncryptedConnectionString, True)

            ' 再読み込み
            _getConnectionsService()

            Return objIVsDataConnection
        Catch ex As Exception
            BuildOutputString(ex.ToString)
            Return Nothing
        End Try
    End Function

    Private Function _getService(ByVal serviceProvider As Object, ByVal guid As System.Guid) As Object
        Dim objService As Object = Nothing
        Dim objIServiceProvider As Microsoft.VisualStudio.OLE.Interop.IServiceProvider
        Dim objIntPtr As IntPtr
        Dim hr As Integer
        Dim objSIDGuid As Guid
        Dim objIIDGuid As Guid
        objSIDGuid = guid
        objIIDGuid = objSIDGuid
        objIServiceProvider = CType(serviceProvider, Microsoft.VisualStudio.OLE.Interop.IServiceProvider)
        hr = objIServiceProvider.QueryService(objSIDGuid, objIIDGuid, objIntPtr)
        If hr <> 0 Then
            System.Runtime.InteropServices.Marshal.ThrowExceptionForHR(hr)
        ElseIf Not objIntPtr.Equals(IntPtr.Zero) Then
            objService = System.Runtime.InteropServices.Marshal.GetObjectForIUnknown(objIntPtr)
            System.Runtime.InteropServices.Marshal.Release(objIntPtr)
        End If
        Return objService
    End Function

    Private Sub _getBuildOutputWindowPane()
        Dim win As EnvDTE.Window
        Dim outputWindow As EnvDTE.OutputWindow
        win = _dte.Windows.Item(EnvDTE.Constants.vsWindowKindOutput)
        outputWindow = DirectCast(win.Object, EnvDTE.OutputWindow)
        For Each objOutputWindowPane In outputWindow.OutputWindowPanes
            If objOutputWindowPane.Guid.ToUpper = BUILD_OUTPUT_PANE_GUID Then
                _buildOutputWindowPane = objOutputWindowPane
                Exit For
            End If
        Next
        _buildOutputWindowPane.Activate()
    End Sub

    Private Sub _getDataDialog()
        _IVsDataConnectionDialogFactory = CType(GetService(GetType(IVsDataConnectionDialogFactory)), IVsDataConnectionDialogFactory)
        _IVsDataConnectionDialog = _IVsDataConnectionDialogFactory.CreateConnectionDialog()

        Try
            _IVsDataExplorerConnectionManager = CType(Microsoft.VisualStudio.Shell.Package.GetGlobalService(GetType(Microsoft.VisualStudio.Data.Services.IVsDataExplorerConnectionManager)), Microsoft.VisualStudio.Data.Services.IVsDataExplorerConnectionManager)

            If _IVsDataExplorerConnectionManager Is Nothing Then
                Throw New InvalidOperationException("Microsoft.VisualStudio.Data.Services.IVsDataExplorerConnectionManager is Null")
            End If
        Catch ex As Exception
            BuildOutputString(ex.ToString)
        End Try
    End Sub

    Private Sub _getDataInformation()
        'Dim sMsg As String
        _IVsDataProviderManager = CType(GetService(_dte, GetType(IVsDataProviderManager)), IVsDataProviderManager)
        _IVsDataSourceManager = CType(GetService(_dte, GetType(IVsDataSourceManager)), IVsDataSourceManager)
        _IVsDataConnectionsService = CType(GetService(_dte, GetType(IVsDataConnectionsService)), IVsDataConnectionsService)
        _IVsDataConnectionManager = CType(GetService(_dte, GetType(IVsDataConnectionManager)), IVsDataConnectionManager)
        '' Data providers
        'For Each objIVsDataProvider In objIVsDataProviderManager.GetDataProviders
        '    sMsg = "Data Provider: " & objIVsDataProvider.DisplayName & Microsoft.VisualBasic.ControlChars.CrLf
        '    sMsg &= Microsoft.VisualBasic.ControlChars.CrLf
        '    sMsg &= "Description: " & objIVsDataProvider.Description & Microsoft.VisualBasic.ControlChars.CrLf
        '    sMsg &= Microsoft.VisualBasic.ControlChars.CrLf
        '    sMsg &= "Guid: " & objIVsDataProvider.Guid.ToString & Microsoft.VisualBasic.ControlChars.CrLf
        '    _buildOutputString(sMsg)
        'Next
        '' Data sources
        'For Each objIVsDataSource In objIVsDataSourceManager.GetDataSources()
        '    sMsg = "Data Source: " & objIVsDataSource.DisplayName & Microsoft.VisualBasic.ControlChars.CrLf
        '    sMsg &= "Guid: " & objIVsDataSource.Guid.ToString & Microsoft.VisualBasic.ControlChars.CrLf
        '    sMsg &= "Providers: " & Microsoft.VisualBasic.ControlChars.CrLf
        '    For Each objProviderGuid In objIVsDataSource.GetProviders()
        '        objIVsDataProvider = objIVsDataProviderManager.GetDataProvider(objProviderGuid)
        '        sMsg &= objIVsDataProvider.DisplayName & Microsoft.VisualBasic.ControlChars.CrLf
        '    Next
        '    sMsg &= Microsoft.VisualBasic.ControlChars.CrLf
        '    _buildOutputString(sMsg)
        'Next
    End Sub

    Private Sub _getConnectionsService()
        Dim objProviderGuid As Guid
        Dim iConnectionIndex As Integer

        ' Data connections
        _lstDbProp = New List(Of DbContextProperty)
        Dim dbProp As DbContextProperty
        For iConnectionIndex = 0 To _IVsDataConnectionsService.Count - 1
            dbProp = New DbContextProperty
            _lstDbProp.Add(dbProp)

            dbProp.Name = _IVsDataConnectionsService.GetConnectionName(iConnectionIndex)
            dbProp.ConnectionString = _IVsDataConnectionsService.GetConnectionString(iConnectionIndex)

            objProviderGuid = _IVsDataConnectionsService.GetProvider(iConnectionIndex)
            'sMsg = "Data Connection: " & dbProp.Name & Microsoft.VisualBasic.ControlChars.CrLf
            _IVsDataProvider = _IVsDataProviderManager.GetDataProvider(objProviderGuid)
            dbProp.ProviderName = _IVsDataProvider.GetProperty("InvariantName")
            'sMsg &= "Provider: " & objIVsDataProvider.DisplayName & Microsoft.VisualBasic.ControlChars.CrLf
            'sMsg &= "        : " & dbProp.ProviderName & Microsoft.VisualBasic.ControlChars.CrLf
            'sMsg &= "        : " & objIVsDataProvider.Description & Microsoft.VisualBasic.ControlChars.CrLf
            'sMsg &= "        : " & objIVsDataProvider.Guid.ToString & Microsoft.VisualBasic.ControlChars.CrLf
            'sMsg &= "        : " & objIVsDataProvider.Technology.ToString & Microsoft.VisualBasic.ControlChars.CrLf
            _IVsDataConnection = _IVsDataConnectionManager.GetDataConnection(objProviderGuid, dbProp.ConnectionString, True)
            dbProp.ConnectionString = DataProtection.DecryptString(_IVsDataConnection.EncryptedConnectionString)
            'sMsg &= "Connection String: " & objIVsDataConnection.DisplayConnectionString & Microsoft.VisualBasic.ControlChars.CrLf
            'sMsg &= "                 : " & objIVsDataConnection.Provider.ToString() & Microsoft.VisualBasic.ControlChars.CrLf
            'sMsg &= Microsoft.VisualBasic.ControlChars.CrLf
            '_buildOutputString(sMsg)
        Next
    End Sub

#End Region

End Class
