namespace UserApp.Core.Entities
{
	[System.ComponentModel.DataAnnotations.Schema.Table("appl_news", Schema = "usr_tr")]
	public class ApplNewsTr
	{
		[Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required]
		[System.ComponentModel.DataAnnotations.Schema.Column("id")]
		public string Id { get; set; }
		[Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required]
		[System.ComponentModel.DataAnnotations.Schema.Column("language_id")]
		public string LanguageId { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("title")]
		public string Title { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("description")]
		public string Description { get; set; }
		public TbLanguage Language { get; set; }

	}
}