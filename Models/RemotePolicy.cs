using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.Models
{
	public class RemotePolicy : EntityBase
	{
		// Remote, Hybrid, In Office
		public int ID { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }
	}
}
