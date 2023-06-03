using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab2.Dtos;
using lab2.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lab2.Services
{
    public class UserService : IUserService
    {
        private readonly Context _context;
        private readonly Interfaces.IAuthorizationService _authorizationService;

        public UserService(Context context, Interfaces.IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        public async Task<UserDto> BanUserAsync(int id)
        {
            if (_authorizationService.GetCurrentUser().IsAdmin)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user == null)
                {
                    return null;
                }
                user.IsBanned = true;
                await _context.SaveChangesAsync();

                return new UserDto
                {

                    Id = user.Id,
                    Nickname = user.Nickname,
                    AvatarBase64 = user.AvatarBase64,
                    IsAdmin = user.IsAdmin,
                    IsBanned = user.IsBanned
                };
            }
            return null;
        }

        public async Task<UserDto?> CreateUserAsync(PostUserDto user)
        {
            if (await _context.Users.AnyAsync(u => u.Nickname == user.Nickname))
            {
                return null;
            }
            _context.Users.Add(new Entities.User
            {
                Nickname = user.Nickname,
                Password = user.Password,
                AvatarBase64 = user.AvatarBase64,
            });
            await _context.SaveChangesAsync();
            return await GetUserByNicknameAsync(user.Nickname);
        }

        public async Task<UserDto?> GetUserByNicknameAsync(string nickname)
        {
            return await _context.Users
                .Where(user => user.Nickname == nickname)
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    Nickname = user.Nickname,
                    AvatarBase64 = user.AvatarBase64,
                    IsAdmin = user.IsAdmin,
                    IsBanned = user.IsBanned
                })
                .FirstOrDefaultAsync();
        }

        public async Task<UserDto> PardonUserAsync(int id)
        {
            if (_authorizationService.GetCurrentUser().IsAdmin)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user == null)
                {
                    return null;
                }
                user.IsBanned = false;
                await _context.SaveChangesAsync();

                return new UserDto
                {
                    Id = user.Id,
                    Nickname = user.Nickname,
                    AvatarBase64 = user.AvatarBase64,
                    IsAdmin = user.IsAdmin,
                    IsBanned = user.IsBanned
                };
            }
            return null;
        }
    }
}