Imports System.IO
Imports System.Net
Imports System.Text

Public Class HtmlAnalyzer

    Public Sub New()
    End Sub

    Public Function GetPage(ByVal url As String) As IList(Of XElementRow)
        Dim xml As XDocument
        Dim rc As New List(Of XElementRow)

        xml = _getPage(url)

        'Dim ns = xml.Root.Name.Namespace
        For Each el As XElement In xml.Elements
            Dim lst As IList(Of XElementRow)
            lst = _getElements(el)
            rc.AddRange(lst)
        Next

        Return rc
    End Function

    Private Function _getElements(ByVal target As XElement) As IList(Of XElementRow)
        Dim rc As New List(Of XElementRow)

        Dim els = From hoge In target.Elements
                  Where Not String.IsNullOrEmpty(hoge.@id)
                  Select hoge

        For Each el As XElement In els
            rc.Add(New XElementRow(el))
        Next
        For Each el As XElement In target.Elements
            Dim lst As IList(Of XElementRow)
            lst = _getElements(el)
            rc.AddRange(lst)
        Next

        Return rc
    End Function

    Private Function _getPage(ByVal url As String) As XDocument
        'Dim webreq As System.Net.HttpWebRequest = DirectCast(System.Net.WebRequest.Create(url), System.Net.HttpWebRequest)
        'webreq.Proxy = New WebProxy(_proxy)
        'webreq.Credentials = CredentialCache.DefaultNetworkCredentials

        ''サーバーからの応答を受信するためのHttpWebResponseを取得
        'Using webres As System.Net.HttpWebResponse = DirectCast(webreq.GetResponse(), System.Net.HttpWebResponse)
        '	'応答データを受信するためのStreamを取得
        '	Using st As System.IO.Stream = webres.GetResponseStream()
        '		'文字コードを指定して、StreamReaderを作成
        '		Using sr As New System.IO.StreamReader(st, System.Text.Encoding.UTF8)
        '			Using sgml As Sgml.SgmlReader = New Sgml.SgmlReader()
        '				sgml.IgnoreDtd = True
        '				sgml.DocType = "HTML"
        '				sgml.CaseFolding = Global.Sgml.CaseFolding.ToLower
        '				sgml.InputStream = sr
        '				Return XDocument.Load(sgml)
        '			End Using
        '		End Using
        '	End Using
        'End Using
        Using web = New WebClient()
            web.Proxy = WebRequest.GetSystemWebProxy()
            web.Credentials = CredentialCache.DefaultNetworkCredentials
            'web.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko")
            Dim st = web.OpenRead(url)
            Using sr As TextReader = New StreamReader(st, encoding:=Encoding.UTF8)
                Using sgml As Sgml.SgmlReader = New Sgml.SgmlReader()
                    sgml.IgnoreDtd = True
                    sgml.DocType = "HTML"
                    sgml.CaseFolding = Global.Sgml.CaseFolding.ToLower
                    sgml.InputStream = sr
                    Return XDocument.Load(sgml)
                End Using
            End Using
        End Using
    End Function

End Class
