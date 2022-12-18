using dotnetdevs.Models;

namespace dotnetdevs.ViewModels
{
	public class DeveloperIndex : BaseViewModel
	{
		public List<Developer> Developers { get; set; } = new List<Developer>();
	}
}
