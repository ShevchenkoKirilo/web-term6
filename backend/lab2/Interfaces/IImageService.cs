using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab2.Dtos;
using lab2.Entities;

namespace lab2.Interfaces
{
    public interface IImageService
    {
        Task<List<ImageDto>> GetImagesAsync();
        Task<ImageDto?> PostImageAsync(PostImageDto image);
        Task<bool> DeleteImageAsync(int id);
    }
}