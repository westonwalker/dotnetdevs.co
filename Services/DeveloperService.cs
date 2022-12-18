using dotnetdevs.Data;
using dotnetdevs.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Services
{
	public class DeveloperService
	{
		private readonly ApplicationDbContext _dbContext;

		public DeveloperService(ApplicationDbContext dbContext)
		{
			this._dbContext = dbContext;
		}

		public async Task<List<Developer>> GetAll()
		{
			return await _dbContext.Developers
							.Include(developer => developer.SearchStatus)
							.Include(developer => developer.ExperienceLevel)
							.ToListAsync();
		}

		public async Task<Developer?> Get(int id)
		{
			return await _dbContext.Developers
							.Where(d => d.ID == id)
							.Include(developer => developer.SearchStatus)
							.Include(developer => developer.ExperienceLevel)
							.FirstOrDefaultAsync();
		}

		public async Task<Developer?> GetByUserId(string userId)
		{
			return await _dbContext.Developers
							.Where(d => d.UserID == userId)
							.Include(developer => developer.SearchStatus)
							.Include(developer => developer.ExperienceLevel)
							.FirstOrDefaultAsync();
		}

		public async Task<Developer> Store(Developer developer)
		{
			_dbContext.Developers.Add(developer);
			_dbContext.SaveChanges();
			return developer;
		}

		public async Task<Developer> Update(Developer developer)
		{
			_dbContext.Attach(developer);
			_dbContext.SaveChanges();
			return developer;
		}
	}
}
