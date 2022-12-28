using AutoMapper;
using dotnetdevs.Services;
using dotnetdevs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Controllers
{
	public class JobsController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly JobService _jobService;
		private readonly ConvertKit _convertKit;
		private readonly UserService _userService;
		private readonly IMapper _mapper;

		public JobsController(ILogger<HomeController> logger, ConvertKit convertKit, JobService jobService, UserService userService, IMapper mapper)
		{
			_logger = logger;
			_jobService = jobService;
			_userService = userService;
			_convertKit = convertKit;
			_mapper = mapper;
		}

		[Route("jobs")]
		public async Task<IActionResult> Index()
		{
			JobIndex model = new JobIndex();
			model.Jobs = await _jobService.GetAll();
			return View(model);
		}

		[Route("jobs/{id}")]
		public async Task<IActionResult> Show(string id)
		{
			var job = await _jobService.GetBySlug(id);
			if (job == null) { return NotFound(); }

			JobShow model = new JobShow();
			model.Job = job;
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("jobs/subscribe")]
		public async Task<IActionResult> Subscribe(SubscribeCreate subscriber)
		{
			if (!String.IsNullOrEmpty(subscriber.Email))
			{
				await _convertKit.Subscribe(subscriber.Email);
			}
			return RedirectToAction("Subscribed", "Jobs");
		}

		[Route("subscribed")]
		public IActionResult Subscribed()
		{
			return View();
		}
	}
}
