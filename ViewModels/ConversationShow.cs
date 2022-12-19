using dotnetdevs.Models;
using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.ViewModels
{
	public class ConversationShow : BaseViewModel
	{
		public Conversation Conversation { get; set; }

		public List<Message> Messages { get; set; }

		public bool UserIsCompany { get; set; }

		[Required]
		public string Text { get; set; }

		[Required]
		public int DeveloperId { get; set; }

		[Required]
		public int CompanyId { get; set; }

		[Required]
		public string Sender { get; set; }
	}
}
