using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab2.Dtos;
using lab2.Entities;
using lab2.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lab2.Services
{
    public class ImageService : IImageService
    {
        private readonly Context _context;
        private readonly Interfaces.IAuthorizationService _authorizationService;

        public ImageService(Context context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        public async Task<List<ImageDto>> GetImagesAsync()
        {
            return await _context.Images
                .Select(image => new ImageDto
                {
                    Id = image.Id,
                    ImageBase64 = image.ImageBase64,
                    UserLikedIds = image.Likes.Select(l => l.User.Id).ToList(),
                    UserId = image.User.Id,
                    UserIsBanned = image.User.IsBanned,
                })
                .ToListAsync();
        }

        public async Task<ImageDto> PostImageAsync(PostImageDto imageDto)
        {
            var user = _authorizationService.GetCurrentUser();
            Image image = new()
            {
                User = user,
                ImageBase64 = imageDto.ImageBase64
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return new ImageDto
            {
                ImageBase64 = image.ImageBase64,
                UserLikedIds = image.Likes.Select(l => l.User.Id).ToList()
            };
        }

        public async Task<bool> DeleteImageAsync(int id)
        {
            var user = _authorizationService.GetCurrentUser();
            if (user.IsAdmin || user.Images.Any(image => image.Id == id))
            {
                var image = await _context.Images.FindAsync(id);
                if (image == null)
                {
                    return false;
                }
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}