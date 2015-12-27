
Public Class XElementRow

    Private _element As XElement

    Public Sub New(ByVal element As XElement)
        _element = element
        Selected = True
    End Sub

    Public Property Selected As Boolean

    Public ReadOnly Property ID As String
        Get
            Return _element.@id
        End Get
    End Property

    Public ReadOnly Property Tag As String
        Get
            Dim typ As String = String.Empty
            If _element.Name.LocalName.Equals("input") AndAlso _element.Attribute("type") IsNot Nothing Then
                typ = String.Format("[@type={0}]", _element.Attribute("type").Value)
            End If
            Return _element.Name.LocalName & typ
        End Get
    End Property

End Class
