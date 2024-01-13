using Microsoft.IdentityModel.Tokens;
using Organizations.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Implementations
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly string secretKey = "+drMBlyqcyBSNsa3+5k1l6ABUhZDMHruCLJvDroZbPwvdr89pyPBphW35uvM5Zu5KHt2IXqXDHRvW+rSY8hk/Q=="; 

        public string GenerateToken(string username)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(5) 
               
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
