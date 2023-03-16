using dotnetdevs.Data;
using dotnetdevs.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Services
{
	public class ConversationService
	{
		private readonly IDbContextFactory<ApplicationDbContext> _factory;

		public ConversationService(IDbContextFactory<ApplicationDbContext> factory)
		{
			this._factory = factory;
		}

		public async Task<Conversation?> Get(int id)
		{
			using var context = _factory.CreateDbContext();
			return await context.Conversations
							.Include(c => c.Company)
							.Include(c => c.Developer)
							.Where(d => d.ID == id)
							.FirstOrDefaultAsync();
		}

		public async Task<List<Conversation>> GetAllByDeveloper(int developerId)
		{
			using var context = _factory.CreateDbContext();
			return await context.Conversations
							.Include(c => c.Company)
							.Include(c => c.Developer)
							.Where(d => d.DeveloperID == developerId)
							.ToListAsync();
		}

		public async Task<List<Conversation>> GetAllByCompany(int companyId)
		{
			using var context = _factory.CreateDbContext();
			return await context.Conversations
							.Include(c => c.Company)
							.Include(c => c.Developer)
							.Where(d => d.CompanyID == companyId)
							.ToListAsync();
		}


		public async Task<Conversation?> Get(int developerId, int companyId)
		{
			using var context = _factory.CreateDbContext();
			return await context.Conversations
							.Where(d => d.DeveloperID == developerId)
							.Where(d => d.CompanyID == companyId)
							.FirstOrDefaultAsync();
		}

		public async Task<Conversation> Store(Conversation conversation)
		{
			using var context = _factory.CreateDbContext();
			context.Conversations.Add(conversation);
			context.SaveChanges();
			return conversation;
		}
	}
}
