
Imports System.Data

Imports Moca.Db

Namespace Db.Impl

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DaoGetter
        Inherits AbstractDao

        Public Function Go(ByVal sql As String) As System.Data.DataTable
            Using cmd As IDbCommandSelect = CreateCommandSelect(sql)
                cmd.Execute()
                Return cmd.Result1stTable
            End Using
        End Function

        Public Sub New(ByVal value As String)
            MyBase.New(New Dbms(value))
        End Sub

        Public Sub New(ByVal value As Dbms)
            MyBase.New(value)
        End Sub

    End Class

End Namespace
