namespace UserApp.Core.Entities
{
	[System.ComponentModel.DataAnnotations.Schema.Table("user_role", Schema = "usr")]
	public class UserRole
	{
		[Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required]
		[System.ComponentModel.DataAnnotations.Schema.Column("user_id")]
		public string Id { get; set; }
		[Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required]
		[System.ComponentModel.DataAnnotations.Schema.Column("role_id")]
		public string RoleId { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.NotMapped]
		public const string SqlInsert = "INSERT INTO usr.user_role (user_id, role_id) VALUES (@UserId, @RoleId)";
		public TbHistory<UserRole> SetHistory(string userid, string endpointName, string note = "-", string attachFile = "-")
		{
			TbHistory<UserRole> tbHistory = new()
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
