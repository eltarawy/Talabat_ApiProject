using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TalabatG02.APIs.Dtos;
using TalabatG02.APIs.Errors;
using TalabatG02.APIs.Extentions;
using TalabatG02.Core.Entities.Identity;
using TalabatG02.Core.Services;

namespace TalabatG02.APIs.Controllers
{
    public class AccountsController : ApiBaseController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
            ITokenService tokenService, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var User = await userManager.FindByEmailAsync(model.Email);
            if (User is null) return Unauthorized(new ApiErrorResponse(401));
            var result = await signInManager.CheckPasswordSignInAsync(User, model.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiErrorResponse(401));

            return Ok(new UserDto()
            {
                DisplayName =User.DisplayName,
                Email =User.Email,
                Token = await tokenService.CreateTokenAsync(User, userManager)      
            });
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if(checkEmailExsist(model.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors=new string[] {"this Email is Already Exsist"} });
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber
            };
            var Result = await userManager.CreateAsync(user,model.Password);
            if (!Result.Succeeded) return BadRequest(new ApiErrorResponse(400));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenService.CreateTokenAsync(user, userManager)
            });
        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenService.CreateTokenAsync(user, userManager)
            });                      
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await userManager.FindUserWithAddressByEmailAsync (User);
            var address = mapper.Map<Address, AddressDto>(user.Address);

            return Ok(address);                    
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto updateAddress)
        {
            var address = mapper.Map<AddressDto, Address>(updateAddress);
            var user = await userManager.FindUserWithAddressByEmailAsync(User);

            address.Id = user.Address.Id;
            user.Address = address;

            var Result = await userManager.UpdateAsync(user); 
            if(!Result.Succeeded) return BadRequest(new ApiErrorResponse(400));

            return Ok(updateAddress);
        }


        [HttpGet("checkemail")]
        public async Task<ActionResult<bool>> checkEmailExsist(string email)
        {
            return await userManager.FindByEmailAsync(email) is not null;
        }
    }
}
