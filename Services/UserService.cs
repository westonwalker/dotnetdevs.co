using dotnetdevs.Data;
using dotnetdevs.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace dotnetdevs.Services
{

    public class UserService
	{
		private readonly UserManager<ApplicationUser> _userManager;

		private readonly AuthenticationStateProvider _stateProvider;

		public UserService(AuthenticationStateProvider stateProvider, UserManager<ApplicationUser> userManager)
		{
			_stateProvider = stateProvider;
			_userManager = userManager;
		}

        public async Task<ApplicationUser?> GetAuthenticatedUser(ClaimsPrincipal temp = null)
        {
			var authState = await _stateProvider.GetAuthenticationStateAsync();
			var user = authState.User;
            if (user.Identity != null && user.Identity.IsAuthenticated)
			{
				return _userManager.Users
                    .Include(x => x.Developer)
					.Include(x => x.Company)
					.SingleOrDefault(x => x.Id == _userManager.GetUserId(user));
			}
            return null;
        }
    }
}
