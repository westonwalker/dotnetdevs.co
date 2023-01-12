using dotnetdevs.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.ViewModels
{
	public class JobCreate
	{

		[Required]
		[StringLength(100)]
		public string Title { get; set; }

		[Column(TypeName = "text")]
		[Required]
		public string Description { get; set; }

		[Required]
		public int WorkTypeID { get; set; }
		public List<WorkType> WorkTypes { get; set; } = new List<WorkType>();

		[Required]
		public int RemotePolicyID { get; set; }
		public List<RemotePolicy> RemotePolicies { get; set; } = new List<RemotePolicy>();

		[Required]
		public int ExperienceLevelID { get; set; }
		public List<ExperienceLevel> ExperienceLevels { get; set; } = new List<ExperienceLevel>();

		public int? SalaryStart { get; set; }
		public int? SalaryEnd { get; set; }

		[Required]
		public string ApplyLink { get; set; }
	}
}
