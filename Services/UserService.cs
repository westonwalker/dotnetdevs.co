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
		private readonly ApplicationDbContext _dbContext;
		private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
			_dbContext = dbContext;
		}

        public async Task<ApplicationUser?> GetAuthenticatedUser(ClaimsPrincipal user)
        {
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
