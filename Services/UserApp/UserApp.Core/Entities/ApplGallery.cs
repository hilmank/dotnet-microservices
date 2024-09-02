namespace UserApp.Core.Entities
{
	[System.ComponentModel.DataAnnotations.Schema.Table("appl_gallery", Schema = "usr")]
	public class ApplGallery : BaseEntity
	{
		[System.ComponentModel.DataAnnotations.Schema.Column("title")]
		public string Title { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("description")]
		public string Description { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("type")]
		public string Type { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("file_thumbnail")]
		public string FileThumbnail { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("file_gallery")]
		public string FileGallery { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("is_banner")]
		public bool IsBanner { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("is_slider")]
		public bool IsSlider { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("is_approve")]
		public bool IsApprove { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("created_date")]
		public DateTime CreatedDate { get; set; }
	}
}