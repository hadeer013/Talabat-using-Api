using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Identity;

namespace Talabat.DAL.IdentityContext
{
    public class IdentityContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "Hadeer Ramzy",
                    UserName = "hadeerramzy01",
                    Email = "hadeerramzy01@gmail.com",
                    PhoneNumber = "010645979",
                    Address = new Address()
                    {
                        FirstName = "Hadeer",
                        LastName = "Ramzy",
                        Country = "Egypt",
                        City = "Giza",
                        Street = "10 Tahrir St."
                    }
                };
                await userManager.CreateAsync(user,"P@ssw0rd");
            }
        }
    }
}
