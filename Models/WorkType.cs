using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.Models
{
	public class WorkType : EntityBase
	{
		// Full time, part time, internship, freelance, contract
		public int ID { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }
	}
}
