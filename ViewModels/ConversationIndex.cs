using dotnetdevs.Models;

namespace dotnetdevs.ViewModels
{
	public class ConversationIndex : BaseViewModel
	{
        public bool HasDeveloperProfile {get;set;}
        public bool HasCompanyProfile {get;set;}
		public Developer? Developer { get; set; }
        public Company? Company { get; set; }
        public List<Conversation> CompanyConversations {get;set;} = new List<Conversation>();
        public List<Conversation> DeveloperConversations {get;set;} = new List<Conversation>();
	}
}
