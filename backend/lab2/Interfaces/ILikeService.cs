using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab2.Dtos;

namespace lab2.Interfaces
{
    public interface ILikeService
    {
        Task<bool> LikeImageAsync(int imageId);
        Task<bool> DislikeImageAsync(int imageId);
    }
}