using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.Models
{
	public class Conversation : EntityBase
	{
		public int ID { get; set; }

		[Required]
		public int CompanyID { get; set; }

		public virtual Company Company { get; set; }

		[Required]
		public int DeveloperID { get; set; }

		public virtual Developer Developer { get; set; }
	}
}
