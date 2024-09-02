namespace UserApp.Core.Entities;

[System.ComponentModel.DataAnnotations.Schema.Table("tb_language", Schema = "public")]
public class TbLanguage
{
    [System.ComponentModel.DataAnnotations.Key, System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.Schema.Column("id")]
    public string Id { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("name")]
    public string Name { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.Column("description")]
    public string Description { get; set; }
}

