
Namespace UI

	''' <summary>
	''' ユーザーコントロール共通機能
	''' </summary>
	''' <remarks></remarks>
	Public Class AjaxMinUserControl
		Inherits Moca.Web.UI.MocaUserControl

		''' <summary>
		''' 圧縮版と無圧縮版のURLを条件で振り分ける
		''' </summary>
		''' <param name="value"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Shadows Function ResolveUrl(ByVal value As String) As String
#If JSNOMIN Then
			value = value.Replace(".min.", ".")
#End If
			Return MyBase.ResolveUrl(value)
		End Function

	End Class

End Namespace
