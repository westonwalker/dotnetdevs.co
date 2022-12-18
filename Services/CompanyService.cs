using dotnetdevs.Data;
using dotnetdevs.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Services
{
	public class CompanyService
	{
		private readonly ApplicationDbContext _dbContext;

		public CompanyService(ApplicationDbContext dbContext)
		{
			this._dbContext = dbContext;
		}
		public async Task<Company?> Get(int id)
		{
			return await _dbContext.Companies
							.Where(d => d.ID == id)
							.FirstOrDefaultAsync();
		}

		public async Task<Company?> GetByUserId(string userId)
		{
			return await _dbContext.Companies
							.Where(x => x.UserID == userId)
							.FirstOrDefaultAsync();
		}

		public async Task<Company> Store(Company company)
		{
			_dbContext.Companies.Add(company);
			_dbContext.SaveChanges();
			return company;
		}

		public async Task<Company> Update(Company company)
		{
			_dbContext.Attach(company);
			_dbContext.SaveChanges();
			return company;
		}
	}
}
