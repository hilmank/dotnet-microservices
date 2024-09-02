namespace UserApp.Core.Entities
{
	[System.ComponentModel.DataAnnotations.Schema.Table("appl_news_category", Schema = "usr")]
	public class ApplNewsCategory : BaseEntity
	{
		[System.ComponentModel.DataAnnotations.Schema.Column("name")]
		public string Name { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("description")]
		public string Description { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("file_logo")]
		public string FileLogo { get; set; }
	}
}