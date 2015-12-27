
Imports System.CodeDom
Imports System.CodeDom.Compiler
Imports System.IO

''' <summary>
''' Selenium PageObjects Class Generator
''' </summary>
Public Class SeleniumPageCodeGenerator

#Region " Declare "

    Public Enum LanguageType As Integer
        CSharp
        VisualBasic
    End Enum

    Private C_COMMENT_FORMAT As String = " {0} "

    Private _namespace As String
    Private _className As String
    Private _compileUnit As CodeCompileUnit

    Private _dump As CodeDomProvider

#End Region

#Region " コンストラクタ "

    ''' <summary>
    ''' デフォルトコンストラクタ
    ''' </summary>
    Public Sub New()
        _compileUnit = New CodeCompileUnit
    End Sub

#End Region

#Region " Property "

    Public Property Language As LanguageType

    Public Property [Namespace]() As String
        Get
            Return Me._namespace
        End Get
        Set(ByVal value As String)
            Me._namespace = value
        End Set
    End Property

    Public Property ClassName() As String
        Get
            Return Me._className
        End Get
        Set(ByVal value As String)
            Me._className = value
        End Set
    End Property

#End Region

#Region " Methods "

    ''' <summary>
    ''' ファイル出力
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Write(ByVal path As String, Optional ByVal language As LanguageType = LanguageType.VisualBasic) As String
        Dim opt As New CodeGeneratorOptions

        opt.BlankLinesBetweenMembers = True

        _dump = CodeDomProvider.CreateProvider(language.ToString)

        If Not Directory.Exists(path) Then
            Directory.CreateDirectory(path)
        End If

        Dim filename As String = IO.Path.Combine(path, Me._className & "." & _dump.FileExtension)
        Using writer As StreamWriter = New StreamWriter(filename, False)
            Using tw As New IndentedTextWriter(writer)
                _dump.GenerateCodeFromCompileUnit(_compileUnit, tw, opt)
            End Using
        End Using

        Return filename
    End Function

    ''' <summary>
    ''' コード作成
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Generate(ByVal els As IList(Of XElementRow))
        Dim cn As CodeNamespace = New CodeNamespace(_namespace)

        _dump = CodeDomProvider.CreateProvider(Language.ToString)

        ' Imports 定義
        cn.Imports.Add(New CodeNamespaceImport("OpenQA.Selenium"))
        cn.Imports.Add(New CodeNamespaceImport("OpenQA.Selenium.Support.PageObjects"))
        cn.Imports.Add(New CodeNamespaceImport("MiYABiS.SeleniumTestAssist"))

        ' 準備
        _compileUnit.Namespaces.Add(cn)
        _compileUnit.UserData.Clear()

        ' Class 定義
        Dim class1 As CodeTypeDeclaration
        class1 = _createType(_className)
        cn.Types.Add(class1)

        class1.BaseTypes.Add("SeleniumAction")

        Dim constructor As CodeConstructor = New CodeConstructor
        constructor.Attributes = MemberAttributes.Public Or MemberAttributes.Final
        constructor.Parameters.Add(New CodeParameterDeclarationExpression("IWebDriver", "driver"))
        constructor.BaseConstructorArgs.Add(New CodeVariableReferenceExpression("driver"))
        class1.Members.Add(constructor)

        Dim lst = (From el As XElementRow In els
                   Where el.Selected
                   Select el).ToList

        _createFields(class1, lst)
    End Sub

    ''' <summary>
    ''' クラス作成
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function _createType(ByVal name As String) As CodeTypeDeclaration
        Dim class1 As CodeTypeDeclaration = New CodeTypeDeclaration(name)
        _addComment(class1, name & " PageObjects ")
        Return class1
    End Function

    ''' <summary>
    ''' エンティティコード作成
    ''' </summary>
    ''' <param name="typ"></param>
    ''' <param name="els"></param>
    ''' <remarks></remarks>
    Private Sub _createFields(ByVal typ As CodeTypeDeclaration, ByVal els As IList(Of XElementRow))
        Dim aryField As New List(Of CodeMemberField)

        For Each el As XElementRow In els
            Dim field As CodeMemberField
            field = _createField(el)
            aryField.Add(field)
            If Language = LanguageType.VisualBasic Then
                field.CustomAttributes.Add(New CodeAttributeDeclaration("FindsBy", New CodeAttributeArgument(New CodeSnippetExpression("[Using]:=""" & el.ID & """"))))
            Else
                field.CustomAttributes.Add(New CodeAttributeDeclaration("FindsBy", New CodeAttributeArgument(New CodeSnippetExpression("Using =""" & el.ID & """"))))
            End If
            typ.Members.Add(field)
        Next

        aryField(0).StartDirectives.Add(_createRegionStart("Declare"))
        aryField(els.Count - 1).EndDirectives.Add(_createRegionEnd())
    End Sub

    ''' <summary>
    ''' フィールド作成
    ''' </summary>
    ''' <param name="el"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function _createField(ByVal el As XElementRow) As CodeMemberField
        Dim fieldName As String
        Dim vals() As String
        Dim name As String

        name = el.ID
        If name.StartsWith("_") Then
            name = name.Substring(1, name.Length - 1)
        End If
        vals = name.Split("_"c, "."c, "-"c)

        If vals.Length.Equals(1) Then
            If vals(0).ToUpper.Equals(vals(0)) Then
                vals(0) = vals(0).Substring(0, 1).ToLower() & vals(0).Substring(1, vals(0).Length - 1).ToLower
            Else
                vals(0) = vals(0).Substring(0, 1).ToLower() & vals(0).Substring(1, vals(0).Length - 1)
            End If
        Else
            vals(0) = vals(0).Substring(0, 1).ToLower() & vals(0).Substring(1, vals(0).Length - 1).ToLower
            For ii As Integer = 1 To vals.Length - 1
                vals(ii) = vals(ii).Substring(0, 1).ToUpper() & vals(ii).Substring(1, vals(ii).Length - 1).ToLower
            Next
        End If

        fieldName = "_" & String.Join(String.Empty, vals)

        Return New CodeMemberField("IWebElement", fieldName)
    End Function

    Private Sub _addComment(ByVal cls As CodeTypeDeclaration, ByVal summary As String)
        cls.Comments.Add(New CodeCommentStatement(_getComment("<summary>"), True))
        cls.Comments.Add(New CodeCommentStatement(_getComment(summary), True))
        cls.Comments.Add(New CodeCommentStatement(_getComment("</summary>"), True))
        cls.Comments.Add(New CodeCommentStatement(_getComment("<remarks></remarks>"), True))
        cls.Comments.Add(New CodeCommentStatement(_getComment("<history>"), True))
        cls.Comments.Add(New CodeCommentStatement(_getComment("</history>"), True))
    End Sub

    Private Function _getComment(ByVal msg As String) As String
        Return String.Format(C_COMMENT_FORMAT, msg)
    End Function

    Private Function _createRegionStart(ByVal value As String) As CodeRegionDirective
        Return New CodeRegionDirective(CodeRegionMode.Start, _getComment(value))
    End Function

    Private Function _createRegionEnd() As CodeRegionDirective
        Return New CodeRegionDirective(CodeRegionMode.End, String.Empty)
    End Function

#End Region

End Class
