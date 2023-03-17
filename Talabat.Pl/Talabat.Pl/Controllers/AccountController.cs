using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.BLL.Interfaces;
using Talabat.DAL.Entities.Identity;
using Talabat.Pl.Dtos;
using Talabat.Pl.Error;
using Talabat.Pl.Extensions;

namespace Talabat.Pl.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenServices tokenServices;
        private readonly IMapper mapper;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,ITokenServices tokenServices,IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenServices = tokenServices;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>>Login(LogInDto Loginuser)
        {
            var user=await userManager.FindByEmailAsync(Loginuser.Email);
            if(user != null)
            {
                var result = await signInManager.CheckPasswordSignInAsync(user, Loginuser.Password, false);
                if (result.Succeeded)
                {
                    var uSerDto = new UserDto()
                    {
                        DisplayName = user.DisplayName,
                        Email = user.Email,
                        Token = await tokenServices.CreateToken(user, userManager)
                    };
                    return Ok(uSerDto);
                }
            }
            return Unauthorized(new ApiErrorResponse(401));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>>Register(RegisterDto registerDto)
        {
            if (CheckEmailExistance(registerDto.Email).Result.Value) 
                return BadRequest(new ApiValidationErrorResponse() { Error=new string[] {"Email already exists"} });
            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split("@")[0],
                Address = new Address()
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Country = registerDto.Country,
                    City = registerDto.City,
                    Street = registerDto.Street
                }
            };
            var result= await userManager.CreateAsync(user,registerDto.Password);
            if(result.Succeeded)
            {
                return Ok(new UserDto() { DisplayName=user.DisplayName, Email=registerDto.Email , Token= await tokenServices.CreateToken(user,userManager)});
            }
            return BadRequest(new ApiErrorResponse(400));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);
            return Ok(new UserDto()
            {
                Email = email,
                DisplayName = user.DisplayName,
                Token = await tokenServices.CreateToken(user, userManager)
            });
        }

        [Authorize]
        [HttpGet("userAddress")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await userManager.FindUserWithAddressByEmailAsync(User);
            var mapped = mapper.Map<Address, AddressDto>(user.Address);
            return Ok(mapped);
        }

        [Authorize]
        [HttpPut("updateAddress")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var user = await userManager.FindUserWithAddressByEmailAsync(User);
            user.Address = mapper.Map<AddressDto, Address>(addressDto);
            var result=await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(new ApiValidationErrorResponse() { Error = new string[] { "An error occured while updating the address" } });
            return Ok(addressDto);
        }

        [HttpGet("emailExist")]
        public async Task<ActionResult<bool>> CheckEmailExistance(string email)
        {
            return await userManager.FindByEmailAsync(email) !=null;
        }
    }
}
