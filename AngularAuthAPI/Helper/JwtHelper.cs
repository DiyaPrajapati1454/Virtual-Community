using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AngularAuthAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace AngularAuthAPI.Helper
{
    public class JwtHelper
    {
        private IConfiguration _iconfig;
        public JwtHelper(IConfiguration iconfig)
        {
            _iconfig = iconfig;
        }
        public string GetJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfig["Jwt:Key"]));
            var creds=new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim("Id",user.Id.ToString()),
                new Claim("Name",user.First_Name),
                new Claim("Email",user.Email),
                new Claim(ClaimTypes.Role,user.type)
            };
            var token = new JwtSecurityToken(
               _iconfig["Jwt:Issuer"],
               _iconfig["Jwt:Audience"],
               claims,
               expires: DateTime.Now.AddHours(2),
               signingCredentials: creds
           );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
