using dotnetdevs.Models;
using dotnetdevs.Services;
using dotnetdevs.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace dotnetdevs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DeveloperService _developerService;
        private readonly EmailService _email;

        public HomeController(ILogger<HomeController> logger, EmailService email, DeveloperService developerService)
        {
            _logger = logger;
            _developerService = developerService;
			_email = email;
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
			CompanySignup model = new CompanySignup();
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Store(CompanySignup company)
		{

			if (ModelState.IsValid)
			{
                _email.SendSignupAdminAlert(company);
                return RedirectToAction("Success", "Home");
			}

			return View("SignUp", company);
		}

		[Route("success")]
		public IActionResult Success()
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