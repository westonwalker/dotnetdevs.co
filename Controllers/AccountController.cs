using dotnetdevs.Models;
using dotnetdevs.Services;
using dotnetdevs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Principal;

namespace dotnetdevs.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly EmailService _email;
		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, EmailService email)
		{
			_email = email;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet, AllowAnonymous]
		[Route("register")]
		public IActionResult Register()
		{
			Register model = new Register();
			return View(model);
		}

		[HttpPost, AllowAnonymous]
		[Route("register")]
		public async Task<IActionResult> Register(Register request)
		{
			if (ModelState.IsValid)
			{
				var userCheck = await _userManager.FindByEmailAsync(request.Email);
				if (userCheck == null)
				{
					var user = new ApplicationUser
					{
						UserName = request.Email,
						NormalizedUserName = request.Email,
						Email = request.Email,
						EmailConfirmed = true,
					};
					var result = await _userManager.CreateAsync(user, request.Password);
					if (result.Succeeded)
					{
						await _signInManager.SignInAsync(user, isPersistent: true);
						_email.SendAdminAlert(user);
                        return RedirectToAction("ChooseProfile", "Home");
					}
					else
					{
						if (result.Errors.Count() > 0)
						{
							foreach (var error in result.Errors)
							{
								ModelState.AddModelError("message", error.Description);
							}
						}
						return View(request);
					}
				}
				else
				{
					ModelState.AddModelError("message", "Email already exists.");
					return View(request);
				}
			}
			return View(request);

		}

		[HttpGet]
		[AllowAnonymous]
		[Route("login")]
		public IActionResult Login()
		{
			Login model = new Login();
			return View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("login")]
		public async Task<IActionResult> Login(Login model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null && !user.EmailConfirmed)
				{
					ModelState.AddModelError("message", "Email not confirmed yet");
					return View(model);

				}
				if (await _userManager.CheckPasswordAsync(user, model.Password) == false)
				{
					ModelState.AddModelError("message", "Invalid email or password.");
					return View(model);

				}

				var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

				if (result.Succeeded)
				{
					// await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
					return RedirectToAction("Index", "Home");
				}
				else if (result.IsLockedOut)
				{
					return View("AccountLocked");
				}
				else
				{
					ModelState.AddModelError("message", "Invalid email or password.");
					return View(model);
				}
			}
			return View(model);
		}

		[Route("logout")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
