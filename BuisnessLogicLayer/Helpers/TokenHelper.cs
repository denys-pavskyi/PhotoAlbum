using BuisnessLogicLayer.Models;
using DataAccessLayer.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Helpers
{
    public class TokenHelper
    {
        public const string Issuer = "http://localhost:47392";
        public const string Audience = "http://localhost:47392";
        public const string Secret = "OFRC1j9aaR2BvADxNWlG2pmuD392UfQBZZLM1fuzDEzDlEpSsn+btrpJKd3FfY855OMA9oK4Mc8y48eYUrVUSw==";



        public static string GenerateSecureSecret()
        {
            var hmac = new HMACSHA256();
            return Convert.ToBase64String(hmac.Key);
        }




        public static string GenerateToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(Secret);
            var claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("Role", user.Role.ToString()),
                new Claim("Status", user.UserStatus.ToString())
            });


            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Issuer = Issuer,
                Audience = Audience,
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = signingCredentials,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



    }
}
