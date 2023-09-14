using API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class JwtServices
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _jwtKey;
        public JwtServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
        }
        public string CreateJwt(User user)
        {
            var Userclaim = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id),
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.GivenName , user.FirstName),
                new Claim(ClaimTypes.Surname , user.LastName)

            };
            var credentials = new SigningCredentials(_jwtKey ,SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Userclaim),
                Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration["jwt:ExprisDays"])),
                SigningCredentials = credentials,
                Issuer = _configuration["jwt:Issuer"]
            };

            var tokenhandelr = new JwtSecurityTokenHandler();
            var jwt = tokenhandelr.CreateToken(tokenDescriptor);

            return tokenhandelr.WriteToken(jwt);
        }
    }
}
