using dotnetdevs.Models;
using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.ViewModels
{
	public class ConversationShow
	{
		public List<Message> Messages { get; set; }
	}
}
