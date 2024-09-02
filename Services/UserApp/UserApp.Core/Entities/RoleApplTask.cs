namespace UserApp.Core.Entities
{
	[System.ComponentModel.DataAnnotations.Schema.Table("role_appl_task", Schema = "usr")]
	public class RoleApplTask {
        [Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required]
		[System.ComponentModel.DataAnnotations.Schema.Column("role_id")]
		public string Id { get; set; }
        [Dapper.Contrib.Extensions.ExplicitKey, System.ComponentModel.DataAnnotations.Required]
		[System.ComponentModel.DataAnnotations.Schema.Column("appl_task_id")]
		public string ApplTaskId { get; set; }
	}
}