using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.ViewModels
{
	public class Register
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
