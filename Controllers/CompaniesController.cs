using AutoMapper;
using dotnetdevs.Models;
using dotnetdevs.Services;
using dotnetdevs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetdevs.Controllers
{
	public class CompaniesController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly CompanyService _companyService;
		private readonly UserService _userService;
		private readonly ExperienceLevelService _experienceLevelService;
		private readonly SearchStatusService _searchStatusService;
		private readonly IMapper _mapper;

		public CompaniesController(ILogger<HomeController> logger, CompanyService companyService, UserService userService, ExperienceLevelService experienceLevelService, SearchStatusService searchStatusService, IMapper mapper)
		{
			_logger = logger;
			_companyService = companyService;
			_userService = userService;
			_experienceLevelService = experienceLevelService;
			_searchStatusService = searchStatusService;
			_mapper = mapper;
		}

		public async Task<IActionResult> Show(int id)
		{
			var company = await _companyService.Get(id);
			if (company == null)
			{
				return NotFound();
			}

			CompanyShow model = new CompanyShow();
			model.Company = company;
			return View(model);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Create()
		{
			var user = await _userService.GetAuthenticatedUser(this.User);
			var existingCompany = await _companyService.GetByUserId(user.Id);
			if (existingCompany != null)
			{
				return RedirectToAction("Edit", "Companies", new { id = existingCompany.ID });
			}
			CompanyCreate model = new CompanyCreate();
			return View(model);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Store(CompanyCreate company)
		{
			if (company.AvatarFile != null)
			{
				var isImageTypeValid = ImageKitService.ValidateImageType(company.AvatarFile);
				var isImageSizeValid = ImageKitService.ValidateImageSize(company.AvatarFile);
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
				Company newCompany = _mapper.Map<Company>(company);
				var user = await _userService.GetAuthenticatedUser(this.User);
				newCompany.Avatar = await ImageKitService.UploadImage(company.AvatarFile);
				newCompany.UserID = user.Id;
				newCompany.IsSubscribed = false;
				newCompany.CreatedDate = DateTime.Now;
				newCompany.UpdatedDate = DateTime.Now;

				newCompany = await _companyService.Store(newCompany);
				return RedirectToAction("Show", "Companies", new { id = newCompany.ID });
			}

			return View("Create", company);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Edit(int id)
		{
			var user = await _userService.GetAuthenticatedUser(this.User);
			var existingCompany = await _companyService.GetByUserId(user.Id);
			if (existingCompany == null)
			{
				return RedirectToAction("Create", "Companies");
			}
			CompanyEdit model = _mapper.Map<CompanyEdit>(existingCompany);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Update(CompanyEdit company)
		{
			var existingCompany = await _companyService.Get(company.ID);
			if (existingCompany == null)
			{
				ModelState.AddModelError("Company", "Company does not exist.");
			}
			var user = await _userService.GetAuthenticatedUser(this.User);
			if (user.Id != existingCompany.UserID)
			{
				ModelState.AddModelError("User", "Your not allowed to edit this company profile.");
			}
			if (company.AvatarFile != null)
			{
				var isImageTypeValid = ImageKitService.ValidateImageType(company.AvatarFile);
				var isImageSizeValid = ImageKitService.ValidateImageSize(company.AvatarFile);
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
				if (company.AvatarFile != null)
				{
					existingCompany.Avatar = await ImageKitService.UploadImage(company.AvatarFile);
				}
				existingCompany.UserID = user.Id;
				existingCompany.CompanyName = company.CompanyName;
				existingCompany.website = company.Website;
				existingCompany.PersonalName = company.PersonalName;
				existingCompany.JobTitle = company.JobTitle;
				existingCompany.Bio = company.Bio;
				existingCompany.UpdatedDate = DateTime.Now;
				await _companyService.Update(existingCompany);
				return RedirectToAction("Show", "Companies", new { id = existingCompany.ID });
			}

			return View("Edit", company);
		}
	}
}
