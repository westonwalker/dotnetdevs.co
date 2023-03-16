using dotnetdevs.Models;
using dotnetdevs.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Asn1.Ocsp;
using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.Areas.Identity.Pages.Account
{
	public class RegisterModel : PageModel
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly EmailService _email;
		private readonly ConvertKit _convertKit;
		public RegisterModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ConvertKit convertKit, EmailService email)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_convertKit = convertKit;
			_email = email;
		}

		[BindProperty]
		public InputModel Input { get; set; }
		public string ReturnUrl { get; set; }

		public void OnGet()
		{
			ReturnUrl = Url.Content("~/");
		}

		public async Task<IActionResult> OnPost()
		{
			ReturnUrl = Url.Content("~/choose-profile");
			if (ModelState.IsValid)
			{
				var userCheck = await _userManager.FindByEmailAsync(Input.Email);
				if (userCheck == null)
				{
					var user = new ApplicationUser
					{
						UserName = Input.Email,
						NormalizedUserName = Input.Email,
						Email = Input.Email,
						EmailConfirmed = true,
					};
					var result = await _userManager.CreateAsync(user, Input.Password);
					if (result.Succeeded)
					{
						await _signInManager.SignInAsync(user, isPersistent: true);
						await _convertKit.Subscribe(user.Email);
						_email.SendAdminAlert(user);
						return LocalRedirect(ReturnUrl);
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
						return Page();
					}
				}
				else
				{
					ModelState.AddModelError("message", "Email already exists.");
					return Page();
				}
			}
			return Page();
		}

		public class InputModel
		{
			[EmailAddress(ErrorMessage = "Invalid email address")]
			[Required(ErrorMessage = "Email is required")]
			public string Email { get; set; }

			[Required(ErrorMessage = "Password is required")]
			public string Password { get; set; }

			[Required(ErrorMessage = "Confirm password is required")]
			[Compare("Password", ErrorMessage = "The Password and Confirm Password do not match.")]
			public string ConfirmPassword { get; set; }
		}
	}
}