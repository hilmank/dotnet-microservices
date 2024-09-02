namespace UserApp.Core.Entities
{
	[System.ComponentModel.DataAnnotations.Schema.Table("user_file", Schema = "usr")]
	public class UserFile
	{
		[Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required]
		[System.ComponentModel.DataAnnotations.Schema.Column("user_id")]
		public string Id { get; set; }
		[Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required]
		[System.ComponentModel.DataAnnotations.Schema.Column("type")]
		public string Type { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("category")]
		public string? Category { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("file_name")]
		public string FileName { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("file_thumbnail")]
		public string? FileThumbnail { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("title")]
		public string Title { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("description")]
		public string? Description { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.NotMapped]
		public const string SqlInsert = "INSERT INTO usr.user_file(" +
		"user_id, type, category, file_name, file_thumbnail, title, description) VALUES " +
		"(@id, @type, @category, @fileName, @fileThumbnail, @title, @description)";

		public TbHistory<UserFile> SetHistory(string userid, string endpointName, string note = "-", string attachFile = "-")
		{
			TbHistory<UserFile> tbHistory = new()
			{
				CreatedBy = userid,
				EndpointName = endpointName,
				Note = note,
				AttachFile = attachFile,
				HistoryData = System.Text.Json.JsonSerializer.Serialize(this)
			};
			return tbHistory;
		}
	}
}