using dotnetdevs.Data;
using dotnetdevs.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Services
{
	public class JobService
	{
		private readonly IDbContextFactory<ApplicationDbContext> _factory;

		public JobService(IDbContextFactory<ApplicationDbContext> factory)
		{
			this._factory = factory;
		}

		public async Task<List<Job>> GetAll()
		{
			using var context = _factory.CreateDbContext();
			return await context.Jobs
							.Include(job => job.RemotePolicy)
							.Include(job => job.Company)
							.Include(job => job.UnverifiedCompany)
							.Include(job => job.WorkType)
							.Include(job => job.ExperienceLevel)
							.OrderByDescending(job => job.CreatedDate)
							.ToListAsync();
		}

		public async Task<Job?> GetBySlug(string slug)
		{
			using var context = _factory.CreateDbContext();
			return await context.Jobs
							.Where(d => d.Slug == slug)
							.Include(job => job.RemotePolicy)
							.Include(job => job.Company)
							.Include(job => job.UnverifiedCompany)
							.Include(job => job.WorkType)
							.Include(job => job.ExperienceLevel)
							.FirstOrDefaultAsync();
		}

		public async Task<List<WorkType>> GetWorkTypes()
		{
			using var context = _factory.CreateDbContext();
			return await context.WorkTypes.ToListAsync();
		}

		public async Task<List<RemotePolicy>> GetRemotePolicies()
		{
			using var context = _factory.CreateDbContext();
			return await context.RemotePolicies.ToListAsync();
		}

		public async Task<List<ExperienceLevel>> GetExperienceLevels()
		{
			using var context = _factory.CreateDbContext();
			return await context.ExperienceLevels.ToListAsync();
		}


	}
}
