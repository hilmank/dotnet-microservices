namespace Student.Core.Entities;

[System.ComponentModel.DataAnnotations.Schema.Table("enrollment", Schema = "std")]
public abstract class Enrollment
{
    [Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.Schema.Column("id")]
    public string Id { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("enrollment_date")]
    public DateTime EnrollmentDate { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("grade")]
    public string Grade { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("course_id")]
    public string CourseId { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("student_id")]
    public string StudentId { get; set; }
    public Student Student{ get; set; }
    public Course Course{ get; set; }
}
