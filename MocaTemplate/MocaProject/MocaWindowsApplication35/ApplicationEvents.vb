Imports Moca.Di

Namespace My

	''' <summary>
	''' MyApplication
	''' </summary>
	''' <remarks>
	''' 次のイベントは MyApplication に対して利用できます:<br/>
	''' <br/>
	''' Startup: アプリケーションが開始されたとき、スタートアップ フォームが作成される前に発生します。<br/>
	''' Shutdown: アプリケーション フォームがすべて閉じられた後に発生します。このイベントは、通常の終了以外の方法でアプリケーションが終了されたときには発生しません。<br/>
	''' UnhandledException: ハンドルされていない例外がアプリケーションで発生したときに発生するイベントです。<br/>
	''' StartupNextInstance: 単一インスタンス アプリケーションが起動され、それが既にアクティブであるときに発生します。 <br/>
	''' NetworkAvailabilityChanged: ネットワーク接続が接続されたとき、または切断されたときに発生します。<br/>
	''' </remarks>
	Partial Friend Class MyApplication

		Private Sub MyApplication_Startup(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
			' Moca初期化
			MocaContainerFactory.Init()
		End Sub

		Private Sub MyApplication_Shutdown(sender As Object, e As System.EventArgs) Handles Me.Shutdown
			' Moca初期化
			MocaContainerFactory.Destroy()
		End Sub

	End Class


End Namespace

