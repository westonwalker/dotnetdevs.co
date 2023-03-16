using dotnetdevs.Data;
using dotnetdevs.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Services
{
	public class SearchStatusService
	{
		private readonly IDbContextFactory<ApplicationDbContext> _factory;

		public SearchStatusService(IDbContextFactory<ApplicationDbContext> factory)
		{
			this._factory = factory;
		}
		public async Task<List<SearchStatus>> GetAll()
		{
			using var context = _factory.CreateDbContext();
			return await context.SearchStatuses.ToListAsync();
		}
		public async Task<SearchStatus?> Get(int id)
		{
			using var context = _factory.CreateDbContext();
			return await context.SearchStatuses
							.Where(d => d.ID == id)
							.FirstOrDefaultAsync();
		}
	}
}
