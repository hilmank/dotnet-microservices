namespace UserApp.Core.Entities
{
	[System.ComponentModel.DataAnnotations.Schema.Table("appl_infographic", Schema = "usr")]
	public class ApplInfographic : BaseEntity
	{
		[System.ComponentModel.DataAnnotations.Schema.Column("title")]
		public string Title { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("description")]
		public string Description { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("file_thumbnail")]
		public string FileThumbnail { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("file_infographic")]
		public string FileInfographic { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("is_approve")]
		public bool IsApprove { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("created_date")]
		public DateTime CreatedDate { get; set; }
	}
}