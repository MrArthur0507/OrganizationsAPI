using Microsoft.IdentityModel.Tokens;
using Organizations.Models.Models;
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

        private readonly string secretKey = "eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmFtZSI6IkphdmFJblVzZSIsImV4cCI6MTY5Nzg2OTYyMCwiaWF0IjoxNjk3ODY5NjIwfQ.yE7D6Lj12wX7qUYNTXVYqJhMdPsU7TA9C8WVG4mCCY4"; 

        public string GenerateToken(Account account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            string role = "User";
            if (CheckIfAdmin(account))
            {
                role = "Admin";
            } 
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, account.Username),
            new Claim(ClaimTypes.Role, role),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

        private bool CheckIfAdmin(Account account)
        {
            if (account.IsAdmin)
            {
                return true;
            }
            return false;
        }
    }
}
