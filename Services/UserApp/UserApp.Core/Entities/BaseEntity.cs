namespace UserApp.Core.Entities;

public class BaseEntity
{
    [Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.Schema.Column("id")]
    public string Id { get; set; }
}
