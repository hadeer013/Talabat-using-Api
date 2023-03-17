using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Identity;

namespace Talabat.BLL.Interfaces
{
    public interface ITokenServices
    {
        Task<string> CreateToken(ApplicationUser applicationUser, UserManager<ApplicationUser> userManager);
    }
}
