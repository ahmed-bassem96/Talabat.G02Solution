using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.Core.Identity;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
    
    public class AccountController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser>userManager,
            SignInManager<AppUser>signInManager,ITokenService tokenService,IMapper mapper) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }
            var Result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!Result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401));
            }
            return Ok(new UserDto()
            {
                DisplayName=user.DisplayName,
                Email=user.Email,
                Token= await _tokenService.CreateTokenAsync(user),
            }
                );
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (CheckEmailExists(model.Email).Result.Value){
                return BadRequest(new ApiValidationErrorResponse()
                {
                    Errors = new string[] {"This Email is Already Exists!"}
                });

            }
            var user = new AppUser()
            {
                Email= model.Email,
                DisplayName=model.DisplayName,
                PhoneNumber=model.PhoneNumber,
                UserName = model.Email.Split('@')[0], /////ahmedbassem

            };
        var Result= await _userManager.CreateAsync(user,model.Password);

            if(!Result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(new UserDto()
            {
                DisplayName=user.DisplayName,
                Email=user.Email,
                Token=await _tokenService.CreateTokenAsync(user),
            });
        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user= await _userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                Email=user.Email,
                DisplayName=user.DisplayName,
                Token=await _tokenService.CreateTokenAsync(user),
            });
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            
            var user = await _userManager.FindUserByEmailAsync(User);
            var MappedAddress=_mapper.Map<Address,AddressDto>(user.Address);


            return Ok(MappedAddress);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto UpdatedAddress)
        {
            
            var user =await _userManager.FindUserByEmailAsync(User);
            var MappedAddress = _mapper.Map<AddressDto, Address>(UpdatedAddress);
            MappedAddress.Id = user.Address.Id;
            UpdatedAddress.Id= user.Address.Id;
            user.Address= MappedAddress;

         var Result=await  _userManager.UpdateAsync(user);

            if (!Result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(UpdatedAddress);
             
        }


        [HttpGet("emailExists")] //GET: api/account/emailExists?email=ahmedFathy@gmail.com
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
}
