using AutoMapper;
using dotnetdevs.Models;
using dotnetdevs.Services;
using dotnetdevs.ViewModels;
using Imagekit;
using Imagekit.Sdk;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Mime;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace dotnetdevs.Controllers
{
	public class DevelopersController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly DeveloperService _developerService;
		private readonly UserService _userService;
		private readonly ExperienceLevelService _experienceLevelService;
		private readonly SearchStatusService _searchStatusService;
		private readonly IMapper _mapper;

		public DevelopersController(ILogger<HomeController> logger, DeveloperService developerService, UserService userService, ExperienceLevelService experienceLevelService, SearchStatusService searchStatusService, IMapper mapper)
		{
			_logger = logger;
			_developerService = developerService;
			_userService = userService;
			_experienceLevelService = experienceLevelService;
			_searchStatusService = searchStatusService;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			DeveloperIndex model = new DeveloperIndex();
			model.Developers = await _developerService.GetAll();
			return View(model);
		}

		public async Task<IActionResult> Show(int id)
		{
			var developer = await _developerService.Get(id);
			if (developer == null)
			{
				return NotFound();
			}

			DeveloperShow model = new DeveloperShow();
			model.Developer = developer;
			model.User = await _userService.GetAuthenticatedUser(this.User);
			return View(model);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Create()
		{
			var user = await _userService.GetAuthenticatedUser(this.User);
			var existingDeveloper = await _developerService.GetByUserId(user.Id);
			if (existingDeveloper != null)
			{
				return RedirectToAction("Edit", "Developers", new { id = existingDeveloper.ID });
			}
			DeveloperCreate model = new DeveloperCreate();
			model.StartDate = DateTime.Today;
			model.SearchStatuses = await _searchStatusService.GetAll();
			model.ExperienceLevels = await _experienceLevelService.GetAll();
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Store(DeveloperCreate developer)
		{
			if (developer.AvatarFile != null)
			{
				var isImageTypeValid = ImageKitService.ValidateImageType(developer.AvatarFile);
				var isImageSizeValid = ImageKitService.ValidateImageSize(developer.AvatarFile);
				if (!isImageTypeValid)
				{
					ModelState.AddModelError("AvatarFile", "Only png and jpg images are allowed.");
				}
				if (!isImageSizeValid)
				{
					ModelState.AddModelError("AvatarFile", "Images must be less than 1 MB.");
				}
			}
			else
			{
				ModelState.AddModelError("AvatarFile", "Avatar image is required.");
			}

			if (ModelState.IsValid)
			{
				Developer newDeveloper = _mapper.Map<Developer>(developer);
				var user = await _userService.GetAuthenticatedUser(this.User);
                newDeveloper.Avatar = await ImageKitService.UploadImage(developer.AvatarFile);
                newDeveloper.UserID = user.Id;
                newDeveloper.CreatedDate = DateTime.Now;
				newDeveloper.UpdatedDate = DateTime.Now;

				newDeveloper = await _developerService.Store(newDeveloper);
				return RedirectToAction("Show", "Developers", new { id = newDeveloper.ID });
			}

			developer.SearchStatuses = await _searchStatusService.GetAll();
			developer.ExperienceLevels = await _experienceLevelService.GetAll();
			return View("Create", developer);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Edit(int id)
		{
			var user = await _userService.GetAuthenticatedUser(this.User);
			var existingDeveloper = await _developerService.GetByUserId(user.Id);
			if (existingDeveloper == null)
			{
				return RedirectToAction("Create", "Developers");
			}
			DeveloperEdit model = _mapper.Map<DeveloperEdit>(existingDeveloper);
			model.SearchStatuses = await _searchStatusService.GetAll();
			model.ExperienceLevels = await _experienceLevelService.GetAll();
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Update(DeveloperEdit developer)
		{
			var existingDeveloper = await _developerService.Get(developer.ID);
			if (existingDeveloper == null)
			{
				ModelState.AddModelError("Developer", "Developer does not exist.");
			}
			var user = await _userService.GetAuthenticatedUser(this.User);
			if (user.Id != existingDeveloper.UserID)
			{
				ModelState.AddModelError("User", "Your not allowed to edit this developer profile.");
			}
			if (developer.AvatarFile != null)
			{
				var isImageTypeValid = ImageKitService.ValidateImageType(developer.AvatarFile);
				var isImageSizeValid = ImageKitService.ValidateImageSize(developer.AvatarFile);
				if (!isImageTypeValid)
				{
					ModelState.AddModelError("Avatar", "Only png and jpg images are allowed.");
				}
				if (!isImageSizeValid)
				{
					ModelState.AddModelError("Avatar", "Images must be less than 1 MB.");
				}
			}

			if (ModelState.IsValid)
			{
				if (developer.AvatarFile != null)
				{
					existingDeveloper.Avatar = await ImageKitService.UploadImage(developer.AvatarFile);
				}
				existingDeveloper.UserID = user.Id;
				existingDeveloper.FullName = developer.FullName;
				existingDeveloper.Hero = developer.Hero;
				existingDeveloper.City = developer.City;
				existingDeveloper.State = developer.State;
				existingDeveloper.Country = developer.Country;
				existingDeveloper.Bio = developer.Bio;
				existingDeveloper.SearchStatusID= developer.SearchStatusID;
				existingDeveloper.ExperienceLevelID = developer.ExperienceLevelID;
				existingDeveloper.StartDate = developer.StartDate;
				existingDeveloper.Website = developer.Website;
				existingDeveloper.Github = developer.Github;
				existingDeveloper.Twitter = developer.Twitter;
				existingDeveloper.Linkedin = developer.Linkedin;
				existingDeveloper.StackOverflow = developer.StackOverflow;
				existingDeveloper.CreatedDate = existingDeveloper.CreatedDate;
				existingDeveloper.UpdatedDate = DateTime.Now;
				await _developerService.Update(existingDeveloper);
				return RedirectToAction("Show", "Developers", new { id = existingDeveloper.ID });
			}

			developer.SearchStatuses = await _searchStatusService.GetAll();
			developer.ExperienceLevels = await _experienceLevelService.GetAll();
			return View("Edit", developer);
		}
	}
}
