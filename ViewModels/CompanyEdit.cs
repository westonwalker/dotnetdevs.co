using Imagekit.Constant;
using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.ViewModels
{
	public class CompanyEdit
	{
		[Required]
		public int ID { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "Name is to long (50 character limit)")]
		public string CompanyName { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "Name is to long (100 character limit)")]
		public string Website { get; set; }

		public IFormFile? AvatarFile { get; set; }
		public string? Avatar { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "Name is to long (50 character limit)")]
		public string PersonalName { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "Name is to long (50 character limit)")]
		public string JobTitle { get; set; }

		[Required]
		public string Bio { get; set; }
		public bool IsSubscribed { get; set; }
	}
}
