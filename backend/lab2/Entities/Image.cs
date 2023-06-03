using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageBase64 { get; set; } = string.Empty;

        public User User { get; set; } = new User();
        public List<Like> Likes { get; set; } = new List<Like>();
    }
}