using dotnetdevs.Data;
using dotnetdevs.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Services
{
	public class SearchStatusService
	{
		private readonly ApplicationDbContext _dbContext;

		public SearchStatusService(ApplicationDbContext dbContext)
		{
			this._dbContext = dbContext;
		}
		public async Task<List<SearchStatus>> GetAll()
		{
			return await _dbContext.SearchStatuses.ToListAsync();
		}
	}
}
