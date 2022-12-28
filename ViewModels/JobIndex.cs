using dotnetdevs.Models;

namespace dotnetdevs.ViewModels
{
	public class JobIndex : BaseViewModel
	{
		public List<Job> Jobs { get; set; } = new List<Job>();
	}
}
