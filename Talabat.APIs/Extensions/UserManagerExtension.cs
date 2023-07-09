using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Identity;

namespace Talabat.APIs.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser?> FindUserByEmailAsync(this UserManager<AppUser> userManager,ClaimsPrincipal User)
        {
            var email=User.FindFirstValue(ClaimTypes.Email);
            var user=userManager.Users.Include(U=>U.Address).FirstOrDefault(U=>U.Email== email);

            return user;
        }
    }
}
