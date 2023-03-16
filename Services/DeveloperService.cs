using dotnetdevs.Data;
using dotnetdevs.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Services
{
	public class DeveloperService
	{
		private readonly IDbContextFactory<ApplicationDbContext> _factory;

		public DeveloperService(IDbContextFactory<ApplicationDbContext> factory)
		{
			this._factory = factory;
		}

		public async Task<List<Developer>> GetAll()
		{
			using var context = _factory.CreateDbContext();
			return await context.Developers
							.Include(developer => developer.SearchStatus)
							.Include(developer => developer.ExperienceLevel)
							.OrderByDescending(developer => developer.CreatedDate)
							.ToListAsync();
        }

        public async Task<List<Developer>> GetAvailableDevelopers()
		{
			using var context = _factory.CreateDbContext();
			return await context.Developers
                            .Include(developer => developer.SearchStatus)
                            .Include(developer => developer.ExperienceLevel)
							.Where(developer => developer.SearchStatusID != 4)
                            .OrderByDescending(developer => developer.CreatedDate)
                            .ToListAsync();
		}
		public async Task<List<string>> GetAllDeveloperLocations()
		{
			using var context = _factory.CreateDbContext();
			return await context.Developers
				.Select(m => m.Country)
				.Distinct()
				.OrderBy(country => country)
				.ToListAsync();
		}

		public async Task<List<Developer>> Get10Developers()
		{
			using var context = _factory.CreateDbContext();
			return await context.Developers
							.Include(developer => developer.SearchStatus)
							.Include(developer => developer.ExperienceLevel)
							.Where(developer => developer.SearchStatusID != 4)
							.OrderByDescending(developer => developer.CreatedDate)
							.Take(10)
							.ToListAsync();
		}

		public async Task<Developer?> Get(int id)
		{
			using var context = _factory.CreateDbContext();
			return await context.Developers
							.Where(d => d.ID == id)
							.Include(developer => developer.SearchStatus)
							.Include(developer => developer.ExperienceLevel)
							.FirstOrDefaultAsync();
		}

		public async Task<Developer?> GetWithUser(int id)
		{
			using var context = _factory.CreateDbContext();
			return await context.Developers
							.Where(d => d.ID == id)
							.Include(developer => developer.ApplicationUser)
							.Include(developer => developer.SearchStatus)
							.Include(developer => developer.ExperienceLevel)
							.FirstOrDefaultAsync();
		}

		public async Task<Developer?> GetByUserId(string userId)
		{
			using var context = _factory.CreateDbContext();
			return await context.Developers
							.Where(d => d.UserID == userId)
							.Include(developer => developer.SearchStatus)
							.Include(developer => developer.ExperienceLevel)
							.FirstOrDefaultAsync();
		}

		public async Task<Developer> Store(Developer developer)
		{
			using var context = _factory.CreateDbContext();
			context.Developers.Add(developer);
			context.SaveChanges();
			return developer;
		}

		public async Task<Developer> Update(Developer developer)
		{
			using var context = _factory.CreateDbContext();
			//context.Attach(developer);
			//context.SaveChanges();
			context.Developers.Update(developer);
			context.SaveChanges();
			return developer;
		}
	}
}
