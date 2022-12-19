using dotnetdevs.Data;
using dotnetdevs.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Services
{
	public class MessageService
	{
		private readonly ApplicationDbContext _dbContext;

		public MessageService(ApplicationDbContext dbContext)
		{
			this._dbContext = dbContext;
		}

		public async Task<Message> Store(Message message)
		{
			_dbContext.Messages.Add(message);
			_dbContext.SaveChanges();
			return message;
		}

		//public async Task<Conversation?> Get(int id)
		//{
		//	return await _dbContext.Conversations
		//					.Where(d => d.ID == id)
		//					.FirstOrDefaultAsync();
		//}

		public async Task<List<Message>> GetConversationMessages(int conversationid)
		{
			return await _dbContext.Messages
							.Include(c => c.Conversation)
							.Where(m => m.ConversationID == conversationid)
							.ToListAsync();
		}

		//public async Task<Company?> GetByUserId(string userId)
		//{
		//	return await _dbContext.Companies
		//					.Where(x => x.UserID == userId)
		//					.FirstOrDefaultAsync();
		//}



		public async Task UpdateMessagesToRead(int conversationId, string sender)
		{
			_dbContext.Messages.Where(x => x.ConversationID == conversationId && x.Sender == sender).ToList().ForEach(x =>
			{
				x.HasBeenRead = true; x.UpdatedDate = DateTime.Now;
			});
			_dbContext.SaveChanges();
		}
	}
}
