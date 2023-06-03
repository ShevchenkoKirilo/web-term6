using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using lab2.Entities;
using lab2.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace lab2.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private User CurrentUser { get; set; }

        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public AuthorizationService(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

            CurrentUser = new User();
        }

        public async Task<string> Login(string nickname, string password)
        {
            CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.Nickname == nickname && u.Password == password);
            if (CurrentUser == null || CurrentUser.Nickname == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var KEY = _configuration["JWT:Key"];
            var ISSUER = _configuration["JWT:Issuer"];
            var AUDIENCE = _configuration["JWT:Audience"];

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                    issuer: ISSUER,
                    audience: AUDIENCE,
                    notBefore: now,
                    claims: GetIdentity(nickname, password).Claims,
                    expires: now.Add(TimeSpan.FromMinutes(30)),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])), SecurityAlgorithms.HmacSha256)
                    );

            using (StreamWriter sw = new StreamWriter("temp.txt", false))
            {
                sw.WriteLine(CurrentUser.Id);
            }

            return tokenHandler.WriteToken(token);
        }

        private ClaimsIdentity GetIdentity(string nickname, string password)
        {
            var currentUser = _context.Users.FirstOrDefault(x => x.Nickname == nickname && x.Password == password);
            if (currentUser != null && currentUser.Nickname != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, currentUser.Nickname),
                };
                ClaimsIdentity claimsIdentity =
                new(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }

        public User GetCurrentUser()
        {
            using (StreamReader sr = new StreamReader("temp.txt"))
            {
                CurrentUser = _context.Users.Find(int.Parse(sr.ReadLine()));
                return CurrentUser;
            }
        }
    }
}