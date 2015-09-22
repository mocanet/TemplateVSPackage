Imports System.Web.SessionState
Imports System.Web.Routing

Public Class Global_asax
	Inherits System.Web.HttpApplication

	''' <summary>Logging For Log4net</summary>
	Private Shared ReadOnly _mylog As log4net.ILog = log4net.LogManager.GetLogger(String.Empty)

	''' <summary>
	''' アプリケーションの起動時に呼び出されます
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
		_mylog.Info("Application Start !!")
		RouteConfig.RegisterRoutes(RouteTable.Routes)
	End Sub

	''' <summary>
	''' セッションの開始時に呼び出されます
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
	End Sub

	''' <summary>
	''' 各要求の開始時に呼び出されます
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
	End Sub

	''' <summary>
	''' 使用の認証時に呼び出されます
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
	End Sub

	''' <summary>
	''' エラーの発生時に呼び出されます
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
	End Sub

	''' <summary>
	''' セッションの終了時に呼び出されます
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
	End Sub

	''' <summary>
	''' アプリケーションの終了時に呼び出されます
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
	End Sub

End Class
