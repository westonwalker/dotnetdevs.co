using dotnetdevs.Models;
using Imagekit.Constant;
using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.ViewModels
{
	public class ConversationCreate
	{
		[Required]
		public int DeveloperId { get; set; }

		public Developer? Developer { get; set; }

		[Required]
		public string Text { get; set; }

		public bool Terms { get; set; }
	}
}
