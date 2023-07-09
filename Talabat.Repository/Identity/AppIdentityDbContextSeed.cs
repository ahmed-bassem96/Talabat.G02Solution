using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {

        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
             if(!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Ahmed Bassem",
                    Email="ahmedbassem@gmail.com",
                    UserName="ahmedbassem.itworx",
                    PhoneNumber="0123456789"
                };
             await userManager.CreateAsync(user,"Pa$$w0rd");

            }
        }
    }
}
