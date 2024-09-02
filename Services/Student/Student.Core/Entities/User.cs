

namespace Student.Core.Entities;

[System.ComponentModel.DataAnnotations.Schema.Table("user", Schema = "usr")]
public abstract class User
{
    [Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.Schema.Column("id")]
    public string Id { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("username")]
    public string Username { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("password")]
    public string Password { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("email")]
    public string Email { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("first_name")]
    public string FirstName { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("middle_name")]
    public string? MiddleName { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("last_name")]
    public string? LastName { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("address")]
    public string Address { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("phone_number")]
    public string PhoneNumber { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("mobile_number")]
    public string MobileNumber { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("orgid")]
    public string Orgid { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("status")]
    public int Status { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("last_login")]
    public DateTime? LastLogin { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("created_by")]
    public string CreatedBy { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("created_date")]
    public DateTime CreatedDate { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("updated_by")]
    public string UpdatedBy { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("updated_date")]
    public DateTime? UpdatedDate { get; set; }

    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public string FullName
    {
        get
        {
            string fullName = FirstName;
            if (!string.IsNullOrEmpty(MiddleName))
                fullName += " " + MiddleName;
            if (!string.IsNullOrEmpty(LastName))
                fullName += " " + LastName;
            return fullName.Trim();
        }
    }
}
