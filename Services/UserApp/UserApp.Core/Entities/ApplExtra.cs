namespace UserApp.Core.Entities
{
	[System.ComponentModel.DataAnnotations.Schema.Table("appl_extra", Schema = "usr")]
	public class ApplExtra {
        [Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required]
		[System.ComponentModel.DataAnnotations.Schema.Column("appl_id")]
		public string Id { get; set; }
        [Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required]
		[System.ComponentModel.DataAnnotations.Schema.Column("key")]
		public string Key { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("type")]
		public string Type { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("value")]
		public string Value { get; set; }
	}
}