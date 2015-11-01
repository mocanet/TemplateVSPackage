
Imports OpenQA.Selenium
Imports MiYABiS.SeleniumTestAssist

Public Class $safeitemname$
    Inherits SeleniumAction

    Public Overrides ReadOnly Property MyPageName As String
        Get
            Return "Default.aspx"
        End Get
    End Property

    Public Sub New(ByVal driver As IWebDriver, ByVal baseUrl As String)
        MyBase.New(driver, baseUrl)
    End Sub

End Class
