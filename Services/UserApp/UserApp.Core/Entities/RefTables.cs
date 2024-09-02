namespace UserApp.Core.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("ref_tables", Schema = "usr")]
    public class RefTables : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Schema.Column("name")]
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("description")]
        public string Description { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("info", TypeName = "jsonb")]
        public List<RefTablesJsonData> Info { get; set; }
    }
}