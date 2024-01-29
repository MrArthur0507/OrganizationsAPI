using AutoMapper;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        public JwtGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        

        public string GenerateToken(Account account)
        {
            string secretKey = _configuration.GetSection("Secret-Key").Value;
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
