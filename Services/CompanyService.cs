using dotnetdevs.Data;
using dotnetdevs.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Services
{
	public class CompanyService
	{
		private readonly IDbContextFactory<ApplicationDbContext> _factory;

		public CompanyService(IDbContextFactory<ApplicationDbContext> factory)
		{
			this._factory = factory;
		}
		public async Task<Company?> Get(int id)
		{
			using var context = _factory.CreateDbContext();
			return await context.Companies
							.Where(d => d.ID == id)
							.FirstOrDefaultAsync();
		}

		public async Task<Company?> GetByUserId(string userId)
		{
			using var context = _factory.CreateDbContext();
			return await context.Companies
							.Where(x => x.UserID == userId)
							.FirstOrDefaultAsync();
		}

		public async Task<Company> Store(Company company)
		{
			using var context = _factory.CreateDbContext();
			context.Companies.Add(company);
			context.SaveChanges();
			return company;
		}

		public async Task<Company> Update(Company company)
		{
			using var context = _factory.CreateDbContext();
			context.Attach(company);
			context.SaveChanges();
			return company;
		}
	}
}
