namespace UserApp.Core.Entities
{
	[System.ComponentModel.DataAnnotations.Schema.Table("appl", Schema = "usr")]
	public class Appl : BaseEntity
	{
		[System.ComponentModel.DataAnnotations.Schema.Column("code")]
		public string Code { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("name")]
		public string Name { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("description")]
		public string Description { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("bgcolor")]
		public string Bgcolor { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("iconfile")]
		public string Iconfile { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("imagefile")]
		public string Imagefile { get; set; }
		public void SetTr(ApplTr Tr)
		{
			if (Tr != null)
			{
				this.Name = Tr.Name;
				this.Description = Tr.Description;
			}
		}
	}
}