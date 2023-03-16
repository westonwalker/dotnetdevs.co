using dotnetdevs.Data;
using dotnetdevs.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Services
{
	public class ExperienceLevelService
	{
		private readonly IDbContextFactory<ApplicationDbContext> _factory;

		public ExperienceLevelService(IDbContextFactory<ApplicationDbContext> factory)
		{
			this._factory = factory;
		}
		public async Task<List<ExperienceLevel>> GetAll()
		{
			using var context = _factory.CreateDbContext();
			return await context.ExperienceLevels.ToListAsync();
		}
		public async Task<ExperienceLevel?> Get(int id)
		{
			using var context = _factory.CreateDbContext();
			return await context.ExperienceLevels
							.Where(d => d.ID == id)
							.FirstOrDefaultAsync();
		}
	}
}
