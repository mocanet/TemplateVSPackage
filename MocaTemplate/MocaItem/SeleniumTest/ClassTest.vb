
Imports MiYABiS.SeleniumTestAssist

<TestClass()>
Public Class $safeitemname$
    Inherits AbstractSeleniumTest

#Region " Declare "

    Private Const _PORT As Integer = 80

    Private Shared _baseUrl As String = String.Format("http://localhost:{0}/", _PORT)

#Region " Logging For Log4net "
    ''' <summary>Logging For Log4net</summary>
    Private Shared ReadOnly _mylog As log4net.ILog = log4net.LogManager.GetLogger(String.Empty)
#End Region
#End Region

#Region "追加のテスト属性"

    ''' <summary>
    ''' クラスの最初のテストを実行する前にコードを実行するには、ClassInitialize を使用
    ''' </summary>
    ''' <param name="testContext"></param>
    ''' <remarks></remarks>
    <ClassInitialize()>
    Public Shared Sub ClassInitialize(ByVal testContext As TestContext)
        SeleniumInitialize(_baseUrl)
    End Sub

    ''' <summary>
    ''' クラスのすべてのテストを実行した後にコードを実行するには、ClassCleanup を使用
    ''' </summary>
    ''' <remarks></remarks>
    <ClassCleanup()>
    Public Shared Sub ClassCleanup()
        SeleniumCleanup()
    End Sub

    ''' <summary>
    ''' 各テストを実行する前にコードを実行するには、TestInitialize を使用
    ''' </summary>
    ''' <remarks></remarks>
    <TestInitialize()>
    Public Overrides Sub TestInitialize()
    End Sub

    ''' <summary>
    ''' 各テストを実行した後にコードを実行するには、TestCleanup を使用
    ''' </summary>
    ''' <remarks></remarks>
    <TestCleanup()>
    Public Overrides Sub TestCleanup()
        MyBase.TestCleanup()
    End Sub

#End Region

#Region " TestMethod "

    <TestMethod(),
     Description("テスト内容"),
     TestCategory("カテゴリ")>
    Public Sub TestMethod1()
    End Sub

#End Region

End Class
