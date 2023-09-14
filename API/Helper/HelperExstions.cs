using API.Dtos;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Helper
{
    public static class HelperExstions
    {

        public static UserDto CreateApplicationUserDto(User user , JwtServices jwtServices)
        {
            return new UserDto
            {
                
                FirstName = user.FirstName,
                LastName = user.LastName,
                JWT= jwtServices.CreateJwt(user)
            };
        }

        public static async Task<bool> CheckIfEmailAddressExsit(string email, UserManager<User> userManager)
        {
            return await userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }
    }
}
