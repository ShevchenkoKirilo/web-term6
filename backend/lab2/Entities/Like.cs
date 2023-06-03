using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Entities
{
    public class Like
    {
        public int Id { get; set; }

        public Image Image { get; set; } = new Image();
        public User User { get; set; } = new User();
    }
}