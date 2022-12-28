using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.Models
{
	public class UnverifiedCompany : EntityBase
	{
		public int ID { get; set; }

		[Required]
		[StringLength(50)]
		public string CompanyName { get; set; }

		[Required]
		[StringLength(100)]
		public string website { get; set; }

		public string Avatar { get; set; }

		[Required]
		public string Bio { get; set; }
	}
}
