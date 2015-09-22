using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace $rootnamespace$
{

    /// <summary>
    /// クッキー項目たち
    /// </summary>
    /// <remarks>
    /// クッキーを使用するときは、<see cref="HttpCookie"></see>の読取り専用プロパティを定義してください。<br/>
    /// <example>
    /// HttpCookie Company { get; }
    /// </example>
    /// インタフェースを使うときは、リクエスト、レスポンス用として分けてメンバとして定義してください。<br/>
    /// <example>
    /// [Moca.Web.Attr.Cookie(Moca.Web.Attr.CookieType.Request)]
    /// protected ICookie cookieReq;
    /// <br/>
    /// [Moca.Web.Attr.Cookie(Moca.Web.Attr.CookieType.Response)]
    /// protected ICookie cookieRes;
    /// </example>
    /// </remarks>
    public interface $safeitemname$
    {
        HttpCookie Company { get; }
    }

}
