namespace UserApp.Core.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("ref_tables", Schema = "sdi")]
    public class RefTablesTr
    {
        [Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Schema.Column("id")]
        public string Id { get; set; }
        [Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Schema.Column("language_id")]
        public string LanguageId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("info", TypeName = "jsonb")]
        public List<RefTablesJsonData> Info { get; set; }

        public TbLanguage Language { get; set; }

    }
}