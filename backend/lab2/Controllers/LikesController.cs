using lab2.Dtos;
using lab2.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lab2.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class LikesController : Controller
    {
        private readonly ILikeService _likeService;

        public LikesController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPut("{imageId}/like")]
        public async Task<IActionResult> LikeImageAsync(int imageId)
        {
            var likedImage = await _likeService.LikeImageAsync(imageId);
            if (likedImage == null)
            {
                return NotFound();
            }
            return Ok(likedImage);
        }

        [HttpPut("{imageId}/dislike")]
        public async Task<IActionResult> DislikeImageAsync(int imageId)
        {
            var dislikedImage = await _likeService.DislikeImageAsync(imageId);
            if (dislikedImage == null)
            {
                return NotFound();
            }
            return Ok(dislikedImage);
        }
    }
}
