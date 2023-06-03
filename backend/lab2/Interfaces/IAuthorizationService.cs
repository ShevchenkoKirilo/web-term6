using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab2.Entities;

namespace lab2.Interfaces
{
    public interface IAuthorizationService
    {
        Task<string> Login(string nickname, string password);
        User GetCurrentUser();
    }
}