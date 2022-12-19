using dotnetdevs.Data;
using dotnetdevs.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Services
{
	public class ConversationService
	{
		private readonly ApplicationDbContext _dbContext;

		public ConversationService(ApplicationDbContext dbContext)
		{
			this._dbContext = dbContext;
		}

		public async Task<Conversation?> Get(int id)
		{
			return await _dbContext.Conversations
							.Include(c => c.Company)
							.Include(c => c.Developer)
							.Where(d => d.ID == id)
							.FirstOrDefaultAsync();
		}

		public async Task<List<Conversation>> GetAllByDeveloper(int developerId)
		{
			return await _dbContext.Conversations
							.Include(c => c.Company)
							.Include(c => c.Developer)
							.Where(d => d.DeveloperID == developerId)
							.ToListAsync();
		}

		public async Task<List<Conversation>> GetAllByCompany(int companyId)
		{
			return await _dbContext.Conversations
							.Include(c => c.Company)
							.Include(c => c.Developer)
							.Where(d => d.CompanyID == companyId)
							.ToListAsync();
		}


		public async Task<Conversation?> Get(int developerId, int companyId)
		{
			return await _dbContext.Conversations
							.Where(d => d.DeveloperID == developerId)
							.Where(d => d.CompanyID == companyId)
							.FirstOrDefaultAsync();
		}

		public async Task<Conversation> Store(Conversation conversation)
		{
			_dbContext.Conversations.Add(conversation);
			_dbContext.SaveChanges();
			return conversation;
		}

		//public async Task<Company?> GetByUserId(string userId)
		//{
		//	return await _dbContext.Companies
		//					.Where(x => x.UserID == userId)
		//					.FirstOrDefaultAsync();
		//}



		//public async Task<Company> Update(Company company)
		//{
		//	_dbContext.Attach(company);
		//	_dbContext.SaveChanges();
		//	return company;
		//}
	}
}
