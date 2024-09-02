namespace UserApp.Core.Entities;

[System.ComponentModel.DataAnnotations.Schema.Table(name: "user", Schema = "usr")]
public class User : BaseEntity
{
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
    public string? PhoneNumber { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("mobile_number")]
    public string? MobileNumber { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("org_id")]
    public string? OrgId { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("status")]
    public int Status { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("last_login")]
    public DateTime? LastLogin { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("created_by")]
    public string CreatedBy { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("created_date")]
    public DateTime CreatedDate { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("updated_by")]
    public string? UpdatedBy { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("updated_date")]
    public DateTime? UpdatedDate { get; set; }

    public IEnumerable<Role>? Roles { get; set; }
    public IEnumerable<UserFile>? Files { get; set; }

    public TbHistory<User> SetHistory(string userid, string endpointName, string note = "-", string attachFile = "-")
    {
        TbHistory<User> tbHistory = new()
        {
            CreatedBy = userid,
            EndpointName = endpointName,
            Note = note,
            AttachFile = attachFile,
            HistoryData = System.Text.Json.JsonSerializer.Serialize(this)
        };
        return tbHistory;
    }
}
