namespace UserApp.Core.Entities
{
	[System.ComponentModel.DataAnnotations.Schema.Table("appl_news", Schema = "usr")]
	public class ApplNews : BaseEntity
	{
		[System.ComponentModel.DataAnnotations.Schema.Column("appl_news_category_id")]
		public string ApplNewsCategoryId { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("title")]
		public string Title { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("description")]
		public string Description { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("type")]
		public string Type { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("header")]
		public string Header { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("file_thumbnail")]
		public string FileThumbnail { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("file_news")]
		public string FileNews { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("is_approve")]
		public bool IsApprove { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("created_date")]
		public DateTime CreatedDate { get; set; }
	}
}