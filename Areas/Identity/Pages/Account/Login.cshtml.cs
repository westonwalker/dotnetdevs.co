using dotnetdevs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
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
   //         if (ReturnUrl == null)
   //         {
   //             ReturnUrl = Url.Content("~/");
   //         }
   //         if (ModelState.IsValid)
			//{
			//	var user = await _userManager.FindByEmailAsync(model.Email);
			//	var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, false);
   //             if (result.Succeeded)
   //             {
   //                 return LocalRedirect(ReturnUrl);
   //             }
   //         }
   //         return Page();

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
					return LocalRedirect(ReturnUrl);
				}
				else if (result.IsLockedOut)
				{
					ModelState.AddModelError("message", "Your account is locked.");
				}
				else
				{
					ModelState.AddModelError("message", "Invalid email or password.");
				}
			}
			return Page();
		}

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
