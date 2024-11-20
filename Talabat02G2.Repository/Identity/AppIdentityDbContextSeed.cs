using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG02.Core.Entities.Identity;

namespace TalabatG02.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Donia Eltarawt",
                    Email = "donia772002@gmail.com",
                    UserName = "donia.eltarawy",
                    PhoneNumber = "01066794128"
                };

                await userManager.CreateAsync(User, "P@ssw0rd");
            }
        }
    }
}
