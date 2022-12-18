using dotnetdevs.Models;

namespace dotnetdevs.ViewModels
{
	public class Home : BaseViewModel
	{
		public List<Developer> Developers { get; set; } = new List<Developer>();
	}
}
