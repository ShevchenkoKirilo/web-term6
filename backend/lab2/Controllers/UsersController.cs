using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using lab2.Dtos;
using lab2.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace lab2.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly Interfaces.IAuthorizationService _authorizationService;
        public UsersController(IUserService userService, Interfaces.IAuthorizationService authorizationService)
        {
            _userService = userService;
            _authorizationService = authorizationService;
        }

        [HttpGet("{nickname}")]
        public async Task<IActionResult> GetUserByNicknameAsync(string nickname)
        {
            var user = await _userService.GetUserByNicknameAsync(nickname);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] PostUserDto user)
        {
            var token = await _authorizationService.Login(user.Nickname, user.Password);
            if (token == null)
            {
                return BadRequest();
            }
            return Ok(new TokenDto() { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUserAsync([FromBody] PostUserDto user)
        {
            var createdUser = await _userService.CreateUserAsync(user);
            if (createdUser == null)
            {
                return BadRequest();
            }
            var token = await _authorizationService.Login(user.Nickname, user.Password);
            return Ok(new TokenDto() { Token = token });
        }

        [HttpPut("{id}/ban")]
        [Authorize]
        public async Task<IActionResult> BanUserAsync(int id)
        {
            var bannedUser = await _userService.BanUserAsync(id);
            if (bannedUser == null)
            {
                return NotFound();
            }
            return Ok(bannedUser);
        }

        [HttpPut("{id}/pardon")]
        [Authorize]
        public async Task<IActionResult> PardonUserAsync(int id)
        {
            var pardonedUser = await _userService.PardonUserAsync(id);
            if (pardonedUser == null)
            {
                return NotFound();
            }
            return Ok(pardonedUser);
        }
    }
}