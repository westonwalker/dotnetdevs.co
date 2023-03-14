using dotnetdevs.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.ViewModels
{
	public class CompanySignup
	{
		[Required]
		[StringLength(50, ErrorMessage = "Name is to long (50 character limit)")]
		public string CompanyName { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "Name is to long (50 character limit)")]
		public string PersonalName { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "Email is to long (50 character limit)")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "Name is to long (100 character limit)")]
		public string Website { get; set; }

		[Required]
		public string Bio { get; set; }
	}
}
