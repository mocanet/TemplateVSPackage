
''' <summary>
''' クッキー項目たち
''' </summary>
''' <remarks>
''' クッキーを使用するときは、<see cref="HttpCookie"></see>の読取り専用プロパティを定義してください。<br/>
''' <example>
''' ReadOnly Property Company() As HttpCookie
''' </example>
''' インタフェースを使うときは、リクエスト、レスポンス用として分けてメンバとして定義してください。<br/>
''' <example>
''' &gt;Cookie(CookieType.Request)&lt; _
''' Protected cookieReq As ICookie
''' <br/>
''' &gt;Cookie(CookieType.Response)&lt; _
''' Protected cookieRes As ICookie
''' </example>
''' </remarks>
Public Interface $safeitemname$

    ReadOnly Property Company() As HttpCookie

End Interface
