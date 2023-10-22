using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UPA.BLL.Interfaces;
using UPA.DAL.Models;
using UPA.Errors;

namespace UPA.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("login23")]
        public async Task<ActionResult<UserDto>> Login2(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, loginDto.isRemembered);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            var userDto = new UserDto()
            {
                Email = user.Email,
                UserName = $"{user.UserName}",
                Token = await _tokenService.CreateToken(user, _userManager),
                Message = "success"
            };
            return Ok(userDto);
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, loginDto.isRemembered);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            var userDto = new UserDto()
            {
                Email = user.Email,
                UserName = $"{user.UserName}",
                Token = await _tokenService.CreateToken(user, _userManager),
                Message="success"
            };
            return Ok(userDto);
        }
    }
}
