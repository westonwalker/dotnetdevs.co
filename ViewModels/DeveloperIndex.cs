using dotnetdevs.Models;

namespace dotnetdevs.ViewModels
{
	public class DeveloperIndex : BaseViewModel
	{
		public List<Developer> Developers { get; set; } = new List<Developer>();
		public List<string> Locations { get; set; } = new List<string>();
		public List<ExperienceLevel> ExperienceLevels { get; set; } = new List<ExperienceLevel>();
		public string locationSearch = "";
		public string experienceLevelSearch = "";
	}
}
