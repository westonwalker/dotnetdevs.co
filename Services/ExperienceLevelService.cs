using dotnetdevs.Data;
using dotnetdevs.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Services
{
	public class ExperienceLevelService
	{
		private readonly ApplicationDbContext _dbContext;

		public ExperienceLevelService(ApplicationDbContext dbContext)
		{
			this._dbContext = dbContext;
		}
		public async Task<List<ExperienceLevel>> GetAll()
		{
			return await _dbContext.ExperienceLevels.ToListAsync();
		}
	}
}
