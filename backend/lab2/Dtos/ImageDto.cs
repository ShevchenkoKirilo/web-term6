using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Dtos
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string ImageBase64 { get; set; } = string.Empty;
        public List<int> UserLikedIds { get; set; } = new List<int>();
        public int UserId { get; set; }
        public bool UserIsBanned { get; set; }
    }
}