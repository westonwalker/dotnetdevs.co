using dotnetdevs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public RegisterModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

			[Required]
			[DataType(DataType.Password)]
			public string ConfirmPassword { get; set; }

		}
    }
}
