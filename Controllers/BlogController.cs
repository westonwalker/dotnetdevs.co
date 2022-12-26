using AutoMapper;
using dotnetdevs.Data;
using dotnetdevs.Models;
using dotnetdevs.Services;
using dotnetdevs.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public BlogController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [Route("blog")]
        public IActionResult Index()
        {
            var posts = _dbContext.Posts
                           .Select(p => new BlogPost()
                           {
                               Title = p.Title,
                               Slug = p.Slug,
                               Description = p.Description,
                               CreatedDate = p.CreatedDate
                           }).OrderByDescending(p => p.CreatedDate).ToList();
            var model = new BlogIndex() { Posts= posts };
            return View(model);
        }

        [Route("blog/{id}")]
        public async Task<IActionResult> Show(string id)
        {
            var post = await _dbContext.Posts
                        .Where(x => x.Slug == id)
                        .FirstOrDefaultAsync();

            if (post == null) { return NotFound(); }

            var model = post;
            return View(model);
        }
    }
}
