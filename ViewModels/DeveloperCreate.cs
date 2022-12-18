using dotnetdevs.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.ViewModels
{
	public class DeveloperCreate : BaseViewModel
	{
		[Required]
		[StringLength(50, ErrorMessage = "Name is to long (50 character limit)")]
		public string FullName { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "Hero is to long (100 character limit)")]
		public string Hero { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "City is to long (50 character limit)")]
		public string City { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "State is to long (50 character limit)")]
		public string State { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "Country is to long (50 character limit)")]
		public string Country { get; set; }

		[Required(ErrorMessage = "An avatar image is required. Only jpg and png images are allowed.")]
		public IFormFile AvatarFile { get; set; }

		[Required]
		public string Bio { get; set; }

		[Required]
		public int SearchStatusID { get; set; }
		public List<SearchStatus> SearchStatuses { get; set; } = new List<SearchStatus>();

		[Required]
		public int ExperienceLevelID { get; set; }
		public List<ExperienceLevel> ExperienceLevels { get; set; } = new List<ExperienceLevel>();

		public DateTime StartDate { get; set; }

		public string? Website { get; set; }

		public string? Github { get; set; }

		public string? Twitter { get; set; }

		public string? Linkedin { get; set; }

		public string? StackOverflow { get; set; }
	}
}
