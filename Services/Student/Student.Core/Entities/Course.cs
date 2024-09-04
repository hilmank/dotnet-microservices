namespace Student.Core.Entities;

[System.ComponentModel.DataAnnotations.Schema.Table("course", Schema = "std")]
public class Course
{
    [Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.Schema.Column("id")]
    public string Id { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("name")]
    public string Name { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("description")]
    public string Description { get; set; }

}
