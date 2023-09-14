using API.Dtos;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtServices _services;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(JwtServices services ,SignInManager<User> signInManager , UserManager<User> userManager)
        {
            _services = services;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>>Login(LoginDto login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if(user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            if(user.EmailConfirmed == false)
            {
                return Unauthorized("please confirm your email address");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("invalid username or password");
            }

            var userDto = Helper.HelperExstions.CreateApplicationUserDto(user, _services);

            return userDto;
        }


        [HttpPost("register")]

        public async Task<IActionResult> Register(RegisterDto register){

            if(await Helper.HelperExstions.CheckIfEmailAddressExsit(register.Email, _userManager))
            {
                return BadRequest($"Account with this emaill address {register.Email}");
            }

            var userToAdd = new User
            {
                UserName=register.UserName,
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(userToAdd , register.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Your account been create , you can login");
        }

    }
}
