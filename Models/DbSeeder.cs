using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalCatalogSqLite.Context;
using Microsoft.AspNetCore.Identity;

namespace AnimalCatalogSqLite.Models
{
    public class DbSeeder
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("alan@bol.com.br").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "Alan",
                    Email = "alan@bol.com.br"
                };

                IdentityResult result = userManager.CreateAsync(user, "Ab123/4").Result;
                
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
