using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab2.Dtos;

namespace lab2.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetUserByNicknameAsync(string nickname);
        Task<UserDto?> CreateUserAsync(PostUserDto user);
        Task<UserDto?> BanUserAsync(int id);
        Task<UserDto?> PardonUserAsync(int id);
    }
}