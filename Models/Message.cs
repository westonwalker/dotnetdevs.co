using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetdevs.Models
{
	public class Message : EntityBase
	{
		public int ID { get; set; }

		[Required]
		public int ConversationID { get; set; }

		public virtual Conversation Conversation { get; set; }

		[Required]
		public string Sender { get; set; } // Hacky but whatever. Values are DEVELOPER or COMPANY.

		public bool HasBeenRead { get; set; }

		[Required]
		public string Text { get; set; }
	}
}
