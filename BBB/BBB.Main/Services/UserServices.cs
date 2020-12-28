using BBB.Data;
using BBB.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BBB.Main.Services
{
    public class UserServices : IUserServices
    {
        private ApplicationDbContext _context;
        private IConfiguration _configuration;
        public UserServices(ApplicationDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string AddUser(User User)
        {
            try 
            {
                _context.Users.Add(User);
                var response = _context.SaveChanges();
                if (response < 1)
                {
                    return "Cannot execute. Plz contact Admin";
                }
                return "OK";
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string DeleteUser(User User)
        {
            try
            {
                _context.Users.Attach(User);
                _context.Users.Remove(User);
                var response = _context.SaveChanges();
                if (response < 1)
                {
                    return "Cannot execute. Plz contact Admin";
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string UpdateUser(User User)
        {
            try
            {
                _context.Users.Update(User);
                var response = _context.SaveChanges();
                if (response < 1)
                {
                    return "Cannot execute. Plz contact Admin";
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string GenerateJSONWebToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role,  user.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
