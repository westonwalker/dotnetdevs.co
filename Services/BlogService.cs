using dotnetdevs.Data;
using dotnetdevs.Models;
using dotnetdevs.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Services
{
    public class BlogService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public BlogService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }
        public async Task<List<BlogPost>> GetAll()
        {
            using var context = _factory.CreateDbContext();
            return context.Posts
                .Select(p => new BlogPost()
                {
                    Title = p.Title,
                    Slug = p.Slug,
                    Description = p.Description,
                    CreatedDate = p.CreatedDate
                }).OrderByDescending(p => p.CreatedDate).ToList();
        }

        public async Task<Post> Get(string slug)
        {
            using var context = _factory.CreateDbContext();
            return await context.Posts
                        .Where(x => x.Slug == slug)
                        .FirstOrDefaultAsync();
        }
    }
}
