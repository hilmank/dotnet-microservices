<System.ComponentModel.DataAnnotations.Schema.Table("course", Schema := "std")>
Public Class Course
    <Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required>
    <System.ComponentModel.DataAnnotations.Schema.Column("id")>
    Public Property Id As String

    <System.ComponentModel.DataAnnotations.Schema.Column("name")>
    Public Property Name As String

    <System.ComponentModel.DataAnnotations.Schema.Column("description")>
    Public Property Description As String
End Class