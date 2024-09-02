namespace UserApp.Core.Entities
{
	[System.ComponentModel.DataAnnotations.Schema.Table("role", Schema = "usr")]
	public class Role : BaseEntity
	{
		[System.ComponentModel.DataAnnotations.Schema.Column("code")]
		public string Code { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("name")]
		public string Name { get; set; }
	}
}