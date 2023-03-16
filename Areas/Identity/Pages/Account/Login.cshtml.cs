
using dotnetdevs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.Areas.Identity.Pages.Account
{
	public class LoginModel : PageModel
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		[FromQuery(Name = "returnUrl")]
		public string? ReturnUrl { get; set; }

		public void OnGet()
		{
			if (ReturnUrl == null)
			{
				ReturnUrl = Url.Content("~/");
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ReturnUrl == null)
			{
				ReturnUrl = Url.Content("~/");
			}
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(Input.Email);
				if (user != null && !user.EmailConfirmed)
				{
					ModelState.AddModelError("message", "Email not confirmed yet");
					return Page();

				}
				if (await _userManager.CheckPasswordAsync(user, Input.Password) == false)
				{
					ModelState.AddModelError("message", "Invalid email or password.");
					return Page();
				}

				var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, true, false);

				if (result.Succeeded)
				{
					// await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
					// return RedirectToAction("Index", "Home");
					return LocalRedirect(ReturnUrl);
				}
				else
				{
					ModelState.AddModelError("message", "Invalid email or password.");
					return Page();
				}
			}
			return Page();
		}

		public class InputModel
		{
			[Required(ErrorMessage = "Email is required")]
			[EmailAddress(ErrorMessage = "Invalid email address")]
			public string Email { get; set; }

			[Required(ErrorMessage = "Password is required")]
			[DataType(DataType.Password)]
			public string Password { get; set; }
		}
	}
}