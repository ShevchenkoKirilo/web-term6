
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab2.Dtos;
using lab2.Entities;
using lab2.Interfaces;

namespace lab2.Services
{
    public class LikeService : ILikeService
    {
        private readonly Context _context;
        private readonly Interfaces.IAuthorizationService _authorizationService;
        public LikeService(Context context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }
        public async Task<bool> DislikeImageAsync(int imageId)
        {
            var like = _context.Likes
                .FirstOrDefault(l => l.Image.Id == imageId && 
                                     l.User == _authorizationService.GetCurrentUser());
            if (like == null)
            {
                return false;
            }
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> LikeImageAsync(int imageId)
        {
            var hasLike = _context.Likes
                        .Any(l => l.Image.Id == imageId &&
                                  l.User == _authorizationService.GetCurrentUser());
            if (hasLike)
            {
                return false;
            }

            var image = _context.Images.Find(imageId);

            if(image == null)
            {
                return false;
            }

            var like = new Like()
            {
                Image = image,
                User = _authorizationService.GetCurrentUser()
            };

            _context.Likes.Add(like);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}