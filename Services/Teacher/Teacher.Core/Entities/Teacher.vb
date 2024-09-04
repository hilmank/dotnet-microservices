<System.ComponentModel.DataAnnotations.Schema.Table("student", Schema := "std")>
Public Class Teacher
    <Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required>
    <System.ComponentModel.DataAnnotations.Schema.Column("id")>
    Public Property Id As String

    <System.ComponentModel.DataAnnotations.Schema.Column("nick_name")>
    Public Property NickName As String

    <System.ComponentModel.DataAnnotations.Schema.Column("place_of_birth")>
    Public Property PlaceOfBirth As String

    <System.ComponentModel.DataAnnotations.Schema.Column("date_of_birth")>
    Public Property DateOfBirth As DateTime

    <System.ComponentModel.DataAnnotations.Schema.Column("gender")>
    Public Property Gender As String
End Class
