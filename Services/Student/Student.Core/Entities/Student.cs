namespace Student.Core.Entities;

[System.ComponentModel.DataAnnotations.Schema.Table("student", Schema = "std")]
public abstract class Student
{
    [Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.Schema.Column("id")]
    public string Id { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("nick_name")]
    public string NickName { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("place_of_birth")]
    public string PlaceOfBirth { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("date_of_birth")]
    public DateTime DateOfBirth { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("gender")]
    public string Gender { get; set; }
    public User User { get; set; }
}

