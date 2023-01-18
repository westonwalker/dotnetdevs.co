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
							.OrderByDescending(developer => developer.CreatedDate)
							.ToListAsync();
        }

        public async Task<List<Developer>> GetAvailableDevelopers()
        {
            return await _dbContext.Developers
                            .Include(developer => developer.SearchStatus)
                            .Include(developer => developer.ExperienceLevel)
							.Where(developer => developer.SearchStatusID != 4)
                            .OrderByDescending(developer => developer.CreatedDate)
                            .ToListAsync();
		}
		public async Task<List<string>> GetAllDeveloperLocations()
		{
			return await _dbContext.Developers.Select(m => m.Country).Distinct().ToListAsync();
		}

		public async Task<List<Developer>> Get10Developers()
		{
			return await _dbContext.Developers
							.Include(developer => developer.SearchStatus)
							.Include(developer => developer.ExperienceLevel)
							.Where(developer => developer.SearchStatusID != 4)
							.OrderByDescending(developer => developer.CreatedDate)
							.Take(10)
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

		public async Task<Developer?> GetWithUser(int id)
		{
			return await _dbContext.Developers
							.Where(d => d.ID == id)
							.Include(developer => developer.ApplicationUser)
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
