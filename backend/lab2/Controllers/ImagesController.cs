using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using lab2.Dtos;
using lab2.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace lab2.Controllers
{
    [Route("[controller]")]
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService, Interfaces.IAuthorizationService authorizationService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetImagesAsync()
        {
            var images = await _imageService.GetImagesAsync();
            return Ok(images);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostImageAsync([FromBody] PostImageDto image)
        {
            var createdImage = await _imageService.PostImageAsync(image);
            if (createdImage == null)
            {
                return BadRequest();
            }
            return Ok(createdImage);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteImageAsync(int id)
        {
            var deletedImage = await _imageService.DeleteImageAsync(id);
            if (!deletedImage)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}