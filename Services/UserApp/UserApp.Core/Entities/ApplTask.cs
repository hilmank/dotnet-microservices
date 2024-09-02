namespace UserApp.Core.Entities
{
	[System.ComponentModel.DataAnnotations.Schema.Table("appl_task", Schema = "usr")]
	public class ApplTask : BaseEntity
	{
		[System.ComponentModel.DataAnnotations.Schema.Column("appl_task_parent_id")]
		public string ApplTaskParentId { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("appl_id")]
		public string ApplId { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("index_no")]
		public int IndexNo { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("task_name")]
		public string TaskName { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("controller_name")]
		public string ControllerName { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("action_name")]
		public string ActionName { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("description")]
		public string Description { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("icon_name")]
		public string IconName { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("custom_id")]
		public string CustomId { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("status")]
		public int Status { get; set; }

		public List<ApplTask> Children { get; set; }
		public Appl Appl { get; set; }
		public void SetTr(ApplTaskTr Tr)
		{
			if (Tr != null)
			{
				this.TaskName = Tr.TaskName;
				this.Description = Tr.Description;
			}
		}
	}
}