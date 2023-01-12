using dotnetdevs.Data;
using dotnetdevs.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Services
{
	public class JobService
	{
		private readonly ApplicationDbContext _dbContext;

		public JobService(ApplicationDbContext dbContext)
		{
			this._dbContext = dbContext;
		}

		public async Task<List<Job>> GetAll()
		{
			return await _dbContext.Jobs
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
			return await _dbContext.Jobs
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
			return await _dbContext.WorkTypes.ToListAsync();
		}

		public async Task<List<RemotePolicy>> GetRemotePolicies()
		{
			return await _dbContext.RemotePolicies.ToListAsync();
		}

		public async Task<List<ExperienceLevel>> GetExperienceLevels()
		{
			return await _dbContext.ExperienceLevels.ToListAsync();
		}


	}
}
