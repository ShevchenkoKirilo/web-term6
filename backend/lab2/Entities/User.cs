using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string AvatarBase64 { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public bool IsBanned { get; set; }

        public List<Image> Images { get; set; } = new List<Image>();
        public List<Like> Likes { get; set; } = new List<Like>();
    }
}