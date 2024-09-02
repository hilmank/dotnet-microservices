namespace UserApp.Core.Entities
{
	[System.ComponentModel.DataAnnotations.Schema.Table("appl_task_delegation", Schema = "usr")]
	public class ApplTaskDelegation : BaseEntity
	{
		[System.ComponentModel.DataAnnotations.Schema.Column("appl_task_id")]
		public string ApplTaskId { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("delegate_for")]
		public string DelegateFor { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("delegate_by")]
		public string DelegateBy { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("approved_by")]
		public string ApprovedBy { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("start_date")]
		public DateTime StartDate { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("end_date")]
		public DateTime EndDate { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("status")]
		public int Status { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("created_by")]
		public string CreatedBy { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("created_date")]
		public DateTime CreatedDate { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("updated_by")]
		public string UpdatedBy { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.Column("updated_date")]
		public DateTime? UpdatedDate { get; set; }
	}
}