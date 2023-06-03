using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public string AvatarBase64 { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public bool IsBanned { get; set; }
    }
}