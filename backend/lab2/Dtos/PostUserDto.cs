using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Dtos
{
    public class PostUserDto
    {
        public string Nickname { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string AvatarBase64 { get; set; } = string.Empty;
    }
}