
Imports System.CodeDom
Imports System.CodeDom.Compiler
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports Moca.Db

''' <summary>
''' エンティティコード作成
''' </summary>
''' <remarks></remarks>
Public Class EntityCodeGenerator

#Region " Declare "

    Public Enum LanguageType As Integer
        CSharp
        VisualBasic
    End Enum

    Private C_COMMENT_FORMAT As String = " {0} "

    Private _namespace As String
    Private _className As String
    Private _tableProperty As Boolean
    Private _defName As String
    Private _compileUnit As CodeCompileUnit

    Private _dump As CodeDomProvider

#End Region

#Region " コンストラクタ "

    Public Sub New()
        _compileUnit = New CodeCompileUnit
    End Sub

#End Region

#Region " Property "

    Public Property Language As LanguageType

    Public Property TargetFrameworkMoniker As String

    Public Property [Namespace]() As String
        Get
            Return Me._namespace
        End Get
        Set(ByVal value As String)
            Me._namespace = value
        End Set
    End Property

    Public Property ClassName() As System.String
        Get
            Return Me._className
        End Get
        Set(ByVal value As System.String)
            Me._className = value
        End Set
    End Property

    Public Property AutoImplementedProperties As Boolean

    Public Property PropertyOrderAttribute As Boolean

    Public Property TableProperty() As Boolean
        Get
            Return _tableProperty
        End Get
        Set(ByVal value As Boolean)
            _tableProperty = value
        End Set
    End Property

    Public Property DefName() As System.String
        Get
            Return Me._defName
        End Get
        Set(ByVal value As System.String)
            Me._defName = value
        End Set
    End Property

    Public Property INotifyPropertyChangedBase As Boolean

    Public Property DefField As Boolean

    Private ReadOnly Property DefinitionName As String
        Get
            Return String.Format("I{0}Definition", _defName)
        End Get
    End Property

    Public Property TableName As String

    Public Property ConnectionSettingsName As String

    Public ReadOnly Property CompilerVersion As String
        Get
            Dim value = TargetFrameworkMoniker.Split("=")
            Return value.Last
        End Get
    End Property

    Public ReadOnly Property ToUseCallerMemberName As Boolean
        Get
            If CInt(CompilerVersion.
                Replace("v", String.Empty).
                Replace(".", String.Empty)) < 45D Then
                Return False
            End If
            Return True
        End Get
    End Property

#End Region

    ''' <summary>
    ''' ファイル出力
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Write(ByVal path As String, Optional ByVal language As LanguageType = LanguageType.VisualBasic) As String
        Dim opt As New Compiler.CodeGeneratorOptions
        Dim providerOptions = New Dictionary(Of String, String)()
        providerOptions.Add("CompilerVersion", CompilerVersion)

        opt.BlankLinesBetweenMembers = True
        opt.BracingStyle = "C"

        _dump = CodeDomProvider.CreateProvider(language.ToString, providerOptions)

        If Not System.IO.Directory.Exists(path) Then
            System.IO.Directory.CreateDirectory(path)
        End If

        Dim filename As String = System.IO.Path.Combine(path, Me._className & "." & _dump.FileExtension)
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
    ''' <param name="columns"></param>
    ''' <param name="difinition"></param>
    ''' <remarks></remarks>
    Public Sub Generate(ByVal columns As DataColumnCollection, Optional ByVal difinition As DataRowCollection = Nothing)
        Dim cn As CodeNamespace = New CodeNamespace(_namespace)

        ' Imports 定義
        cn.Imports.Add(New CodeNamespaceImport("Moca.Db"))
        cn.Imports.Add(New CodeNamespaceImport("Moca.Db.Attr"))
        If Me.INotifyPropertyChangedBase Then
            cn.Imports.Add(New CodeNamespaceImport("System.ComponentModel"))
        End If
        If ToUseCallerMemberName Then
            cn.Imports.Add(New CodeNamespaceImport("System.Runtime.CompilerServices"))
        End If
        If PropertyOrderAttribute Then
            cn.Imports.Add(New CodeNamespaceImport("Moca.Win"))
        End If

        ' 準備
        _compileUnit.Namespaces.Add(cn)
        _compileUnit.UserData.Clear()

        ' Class 定義
        Dim class1 As CodeTypeDeclaration
        class1 = _createType(_className)
        If Me.INotifyPropertyChangedBase Then
            class1.BaseTypes.Add(GetType(System.ComponentModel.INotifyPropertyChanged))
        End If
        cn.Types.Add(class1)
        _createEntity(class1, columns, (difinition IsNot Nothing))

        If Me.INotifyPropertyChangedBase Then
            Dim ev As CodeMemberEvent = New CodeMemberEvent()
            ev.StartDirectives.Add(_createRegionStart("PropertyChanged"))
            ev.Name = "PropertyChanged"
            ev.Attributes = MemberAttributes.Public
            ev.ImplementationTypes.Add(GetType(System.ComponentModel.INotifyPropertyChanged))
            ev.Type = New CodeTypeReference(GetType(System.ComponentModel.PropertyChangedEventHandler))
            class1.Members.Add(ev)

            Dim mtd As CodeMemberMethod = New CodeMemberMethod()
            mtd.EndDirectives.Add(_createRegionEnd())
            mtd.Name = "OnPropertyChanged"
            mtd.Attributes = MemberAttributes.Family
            class1.Members.Add(mtd)
            Dim methodParameter As CodeParameterDeclarationExpression = New CodeParameterDeclarationExpression(GetType(String), "name")
            If ToUseCallerMemberName Then
                If Me.Language = LanguageType.VisualBasic Then
                    methodParameter = New CodeParameterDeclarationExpression("String = Nothing", "optional name")
                Else
                    methodParameter = New CodeParameterDeclarationExpression(GetType(String), "name = null")
                End If
                methodParameter.CustomAttributes.Add(New CodeAttributeDeclaration("CallerMemberName"))
            End If
            mtd.Parameters.Add(methodParameter)

            If Me.Language = LanguageType.VisualBasic Then
                ' VB
                Dim snip2 As CodeSnippetExpression = New CodeSnippetExpression("RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(name))")
                mtd.Statements.Add(snip2)
            Else
                ' C#
                Dim condition As CodeExpression = New CodeBinaryOperatorExpression(
                        New CodeVariableReferenceExpression("PropertyChanged"),
                        CodeBinaryOperatorType.IdentityInequality,
                        New CodePrimitiveExpression(Nothing)
                    )

                Dim createArgs As CodeObjectCreateExpression = New CodeObjectCreateExpression(GetType(System.ComponentModel.PropertyChangedEventArgs), New CodeArgumentReferenceExpression("name"))
                Dim raiseEventExp As CodeDelegateInvokeExpression = New CodeDelegateInvokeExpression(New CodeVariableReferenceExpression("PropertyChanged"), New CodeThisReferenceExpression(), createArgs)
                Dim trueStatements As CodeExpressionStatement = New CodeExpressionStatement(raiseEventExp)
                Dim ifStatement As CodeConditionStatement = New CodeConditionStatement(condition, trueStatements)

                mtd.Statements.Add(ifStatement)
            End If
        End If

        If difinition Is Nothing Then
            Exit Sub
        End If

        ' 以降はテーブル定義作成
        class1 = _createInterface(_defName)
        cn.Types.Add(class1)

        class1.StartDirectives.Add(_createRegionStart("Definition"))
        class1.EndDirectives.Add(_createRegionEnd())
        _createDifinition(class1, difinition)
    End Sub

    ''' <summary>
    ''' エンティティコード作成
    ''' </summary>
    ''' <param name="typ"></param>
    ''' <param name="columns"></param>
    ''' <remarks></remarks>
    Private Sub _createEntity(ByVal typ As CodeTypeDeclaration, ByVal columns As DataColumnCollection, ByVal isDef As Boolean)
        Dim aryField As New List(Of CodeMemberField)
        Dim aryProperty As New List(Of CodeMemberProperty)
        Dim arySnippet As New List(Of CodeSnippetTypeMember)
        Dim fieldCount As Integer = columns.Count - 1
        Dim columnCount As Integer = columns.Count - 1


        If isDef Then
            If Me.DefField Then
                Dim field As CodeMemberField
                field = _createField("_def")
                field.Attributes = MemberAttributes.Static
                field.Type = New CodeTypeReference(DefinitionName)
                aryField.Add(field)
                typ.Members.Add(field)
                fieldCount += 1
            End If
        End If

        Dim index As Integer = 1

        For Each col As DataColumn In columns
            Dim field As CodeMemberField
            Dim prop As CodeMemberProperty

            field = _createField(col)
            prop = _createColumnProperty(field, col.ColumnName)
            aryField.Add(field)
            If Me.AutoImplementedProperties AndAlso Not Me.INotifyPropertyChangedBase Then
                If Me.Language = LanguageType.VisualBasic Then
                    'snippet.Text = String.Format("<Column(""{0}"")>{1}Public Property {2} AS {3}", col.ColumnName, Environment.NewLine, prop.Name, _dump.GetTypeOutput(dType))
                    field.Name = "Property " & prop.Name
                    field.Attributes = MemberAttributes.Public Or MemberAttributes.Final
                    field.CustomAttributes.Add(New CodeAttributeDeclaration("Column", New CodeAttributeArgument(New CodeSnippetExpression("""" & col.ColumnName & """"))))
                    If PropertyOrderAttribute Then
                        field.CustomAttributes.Add(New CodeAttributeDeclaration("PropertyOrder", New CodeAttributeArgument(New CodePrimitiveExpression(index))))
                    End If
                    typ.Members.Add(field)
                    _addComment(field, prop.Name & " (" & col.ColumnName & ") Property.")
                Else
                    Dim attrString As String
                    Dim dType As New CodeTypeReference(col.DataType)
                    Dim snippet As CodeSnippetTypeMember = New CodeSnippetTypeMember()
                    snippet.Comments.Add(New CodeCommentStatement(_getComment("<summary>"), True))
                    snippet.Comments.Add(New CodeCommentStatement(_getComment(prop.Name & " (" & col.ColumnName & ") Property."), True))
                    snippet.Comments.Add(New CodeCommentStatement(_getComment("</summary>"), True))
                    If PropertyOrderAttribute Then
                        attrString = String.Format("[Column(""{0}"")]{1}[PropertyOrder(""{2}"")]", col.ColumnName, Environment.NewLine, index)
                    Else
                        attrString = String.Format("[Column(""{0}"")]", col.ColumnName)
                    End If
                    snippet.Text = String.Format("{0}{1}public {2} {3} {{ get; set; }}", attrString, Environment.NewLine, _dump.GetTypeOutput(dType), prop.Name) & Environment.NewLine
                    typ.Members.Add(snippet)
                    arySnippet.Add(snippet)
                End If
            Else
                If PropertyOrderAttribute Then
                    prop.CustomAttributes.Add(New CodeAttributeDeclaration("PropertyOrder", New CodeAttributeArgument(New CodePrimitiveExpression(index))))
                End If
                typ.Members.Add(field)
                aryProperty.Add(prop)
                typ.Members.Add(prop)
                If Me.INotifyPropertyChangedBase Then
                End If
            End If
            index += 1
        Next
        If Me.AutoImplementedProperties AndAlso Not Me.INotifyPropertyChangedBase Then
            If Me.Language = LanguageType.VisualBasic Then
                aryField(0).StartDirectives.Add(_createRegionStart("Property"))
                aryField(fieldCount).EndDirectives.Add(_createRegionEnd())
            Else
                arySnippet(0).StartDirectives.Add(_createRegionStart("Property"))
                arySnippet(columnCount).EndDirectives.Add(_createRegionEnd())
            End If
        Else
            aryField(0).StartDirectives.Add(_createRegionStart("Declare"))
            aryField(fieldCount).EndDirectives.Add(_createRegionEnd())
            aryProperty(0).StartDirectives.Add(_createRegionStart("Property"))
            aryProperty(columnCount).EndDirectives.Add(_createRegionEnd())
        End If
    End Sub

    ''' <summary>
    ''' テーブル定義コード作成
    ''' </summary>
    ''' <param name="typ"></param>
    ''' <param name="difinition"></param>
    ''' <remarks></remarks>
    Private Sub _createDifinition(ByVal typ As CodeTypeDeclaration, ByVal difinition As DataRowCollection)
        Dim field As CodeMemberField
        Dim prop As CodeMemberProperty

        If Me.TableProperty Then
            prop = _createTableProperty()
            typ.Members.Add(prop)
        End If

        For Each row As DataRow In difinition
            field = _createField(row.Item(0).ToString)
            prop = _createColumnProperty(field, row.Item(0).ToString)
            typ.Members.Add(prop)
        Next
    End Sub

    Private Function _createTableProperty() As CodeMemberProperty
        Dim prop As CodeMemberProperty
        Dim name As String

        name = "Table"

        prop = New CodeMemberProperty()

        prop.Name = name
        prop.Attributes = MemberAttributes.Public Or MemberAttributes.Final
        prop.Type = New CodeTypeReference(GetType(DbInfoTable))
        prop.GetStatements.Add(New CodeMethodReturnStatement(New CodeFieldReferenceExpression(New CodeThisReferenceExpression(), name)))
        prop.SetStatements.Add(New CodeAssignStatement(New CodeFieldReferenceExpression(New CodeThisReferenceExpression(), name), New CodePropertySetValueReferenceExpression()))
        _addComment(prop, prop.Name & " (" & name & ") Property.")
        Return prop
    End Function

    ''' <summary>
    ''' プロパティ作成
    ''' </summary>
    ''' <param name="field"></param>
    ''' <param name="columnName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function _createColumnProperty(ByVal field As CodeMemberField, ByVal columnName As String) As CodeMemberProperty
        Dim prop As CodeMemberProperty
        Dim name As String

        name = field.Name.Substring(1)
        name = name.Substring(0, 1).ToUpper & name.Substring(1, name.Length - 1)

        prop = New CodeMemberProperty()

        prop.Name = name
        prop.Attributes = MemberAttributes.Public Or MemberAttributes.Final
        prop.Type = field.Type
        If Me.AutoImplementedProperties AndAlso Not Me.INotifyPropertyChangedBase Then
            prop.HasGet = False
            prop.HasSet = False
            prop.EndDirectives.Clear()
        Else
            prop.GetStatements.Add(New CodeMethodReturnStatement(New CodeFieldReferenceExpression(New CodeThisReferenceExpression(), field.Name)))
            prop.SetStatements.Add(New CodeAssignStatement(New CodeFieldReferenceExpression(New CodeThisReferenceExpression(), field.Name), New CodePropertySetValueReferenceExpression()))
            If Me.INotifyPropertyChangedBase Then
                Dim cmre As CodeMethodReferenceExpression = New CodeMethodReferenceExpression()
                cmre.MethodName = "OnPropertyChanged"
                If ToUseCallerMemberName Then
                    prop.SetStatements.Add(New CodeMethodInvokeExpression(cmre, New CodeSnippetExpression()))
                Else
                    prop.SetStatements.Add(New CodeMethodInvokeExpression(cmre, New CodeSnippetExpression(String.Format("""{0}""", prop.Name))))
                End If
            End If
        End If
        _addComment(prop, prop.Name & " (" & columnName & ") Property.")
        prop.CustomAttributes.Add(New CodeAttributeDeclaration("Column", New CodeAttributeArgument(New CodeSnippetExpression("""" & columnName & """"))))
        Return prop
    End Function

    ''' <summary>
    ''' クラス作成
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function _createType(ByVal name As String) As CodeTypeDeclaration
        Dim class1 As CodeTypeDeclaration = New CodeTypeDeclaration(name)
        _addComment(class1, name & " Entity")
        Return class1
    End Function

    ''' <summary>
    ''' インタフェース作成
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function _createInterface(ByVal name As String) As CodeTypeDeclaration
        Dim table As String = DefinitionName
        Dim class1 As CodeTypeDeclaration = New CodeTypeDeclaration(table)
        Dim arg1 As String = IIf(String.IsNullOrEmpty(ConnectionSettingsName), My.Resources.TableAttributeArg1, ConnectionSettingsName)
        Dim arg2 As String = """" & IIf(String.IsNullOrEmpty(TableName), name, TableName) & """"
        _addComment(class1, name & " Entity definition")
        class1.CustomAttributes.Add(New CodeAttributeDeclaration("Table", New CodeAttributeArgument(New CodeSnippetExpression(arg1)), New CodeAttributeArgument(New CodeSnippetExpression(arg2))))
        class1.IsInterface = True
        Return class1
    End Function

    ''' <summary>
    ''' フィールド作成
    ''' </summary>
    ''' <param name="col"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function _createField(ByVal col As String) As CodeMemberField
        Dim fieldName As String
        Dim vals() As String
        Dim name As String

        name = col
        If name.StartsWith("_") Then
            name = name.Substring(1, name.Length - 1)
        End If
        vals = name.Split("_")

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

        Return New CodeMemberField(GetType(DbInfoColumn), fieldName)
    End Function

    ''' <summary>
    ''' フィールド作成
    ''' </summary>
    ''' <param name="col"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function _createField(ByVal col As DataColumn) As CodeMemberField
        Dim fieldName As String
        Dim vals() As String
        Dim name As String

        name = col.ColumnName
        If name.StartsWith("_") Then
            name = name.Substring(1, name.Length - 1)
        End If
        vals = name.Split("_")

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

        Return New CodeMemberField(col.DataType, fieldName)
    End Function

    Private Function _createRegionStart(ByVal value As String) As CodeRegionDirective
        Return New CodeRegionDirective(CodeRegionMode.Start, _getComment(value))
    End Function

    Private Function _createRegionEnd() As CodeRegionDirective
        Return New CodeRegionDirective(CodeRegionMode.End, String.Empty)
    End Function

    Private Sub _addComment(ByVal cls As CodeTypeDeclaration, ByVal summary As String)
        cls.Comments.Add(New CodeCommentStatement(_getComment("<summary>"), True))
        cls.Comments.Add(New CodeCommentStatement(_getComment(summary), True))
        cls.Comments.Add(New CodeCommentStatement(_getComment("</summary>"), True))
        cls.Comments.Add(New CodeCommentStatement(_getComment("<remarks></remarks>"), True))
        cls.Comments.Add(New CodeCommentStatement(_getComment("<history>"), True))
        cls.Comments.Add(New CodeCommentStatement(_getComment("</history>"), True))
    End Sub

    Private Sub _addComment(ByVal prop As CodeMemberProperty, ByVal summary As String)
        prop.Comments.Add(New CodeCommentStatement(_getComment("<summary>"), True))
        prop.Comments.Add(New CodeCommentStatement(_getComment(summary), True))
        prop.Comments.Add(New CodeCommentStatement(_getComment("</summary>"), True))
    End Sub

    Private Sub _addComment(ByVal field As CodeMemberField, ByVal summary As String)
        field.Comments.Add(New CodeCommentStatement(_getComment("<summary>"), True))
        field.Comments.Add(New CodeCommentStatement(_getComment(summary), True))
        field.Comments.Add(New CodeCommentStatement(_getComment("</summary>"), True))
    End Sub

    Private Function _getComment(ByVal msg As String) As String
        Return String.Format(C_COMMENT_FORMAT, msg)
    End Function

End Class
