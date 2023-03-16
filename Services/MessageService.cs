using dotnetdevs.Data;
using dotnetdevs.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Services
{
	public class MessageService
	{
		private readonly IDbContextFactory<ApplicationDbContext> _factory;

		public MessageService(IDbContextFactory<ApplicationDbContext> factory)
		{
			this._factory = factory;
		}

		public async Task<Message> Store(Message message)
		{
			using var context = _factory.CreateDbContext();
			context.Messages.Add(message);
			context.SaveChanges();
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
			using var context = _factory.CreateDbContext();
			return await context.Messages
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
			using var context = _factory.CreateDbContext();
			context.Messages.Where(x => x.ConversationID == conversationId && x.Sender == sender).ToList().ForEach(x =>
			{
				x.HasBeenRead = true; x.UpdatedDate = DateTime.Now;
			});
			context.SaveChanges();
		}
	}
}
