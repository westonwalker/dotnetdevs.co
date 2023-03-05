using dotnetdevs.Models;
using dotnetdevs.Services;
using dotnetdevs.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace dotnetdevs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DeveloperService _developerService;

        public HomeController(ILogger<HomeController> logger, DeveloperService developerService)
        {
            _logger = logger;
            _developerService = developerService;
		}

        public async Task<IActionResult> Index()
		{
			Home model = new Home();
			model.Developers = await _developerService.Get10Developers();
			return View(model);
		}
		
		[Route("hire/sign-up")]
		public async Task<IActionResult> SignUp()
		{
			return View();
		}

		[Route("open-startup")]
		public IActionResult OpenStartup()
		{
			return View();
		}

		[Route("choose-profile")]
		public IActionResult ChooseProfile()
        {
            return View();
        }

		[Route("hire")]
		public IActionResult Hire()
		{
			return View();
        }

        [Route("about")]
		public IActionResult About()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}