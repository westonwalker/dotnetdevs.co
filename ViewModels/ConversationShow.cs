using dotnetdevs.Models;
using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.ViewModels
{
	public class ConversationShow : BaseViewModel
	{
		public Conversation Conversation { get; set; }

		public List<Message> Messages { get; set; }

		[Required]
		public string Text { get; set; }

		[Required]
		public string Sender { get; set; }
	}
}
