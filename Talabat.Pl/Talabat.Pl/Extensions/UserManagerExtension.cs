using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Identity;

namespace Talabat.Pl.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<ApplicationUser> FindUserWithAddressByEmailAsync(this UserManager<ApplicationUser> userManager,ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            var user=await userManager.Users.Include(u=>u.Address).SingleOrDefaultAsync(u=>u.Email == email);
            return user;
        }
    }
}
