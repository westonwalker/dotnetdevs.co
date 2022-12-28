using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetdevs.Models
{
	public class Job : EntityBase
	{
		public int ID { get; set; }

		[Required]
		public string Slug { get; set; }

		public int? CompanyID { get; set; }
		public virtual Company Company { get; set; }

		public int? UnverifiedCompanyID { get; set; }
		public virtual UnverifiedCompany UnverifiedCompany { get; set; }

		[Required]
		[StringLength(100)]
		public string Title { get; set; }

		[Column(TypeName = "text")]
		[Required]
		public string Description { get; set; }

		[Required]
		public int WorkTypeID { get; set; }
		public virtual WorkType WorkType { get; set; }

		[Required]
		public int RemotePolicyID { get; set; }
		public virtual RemotePolicy RemotePolicy { get; set; }

		[Required]
		public int ExperienceLevelID { get; set; }
		public virtual ExperienceLevel ExperienceLevel { get; set; }

		public int? SalaryStart { get; set; }

		public int? SalaryEnd { get; set; }

		[Required]
		public string ApplyLink { get; set; }
	}
}
