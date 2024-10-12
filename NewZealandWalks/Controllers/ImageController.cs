using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.Models.Domain;
using NewZealandWalks.Models.Dto.Image;
using NewZealandWalks.Repository.Interfaces;

namespace NewZealandWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public ImageController(IMapper mapper,IImageRepository imageRepository) 
        {
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            var image=mapper.Map<Image>(imageUploadRequestDto);
            ValidateImage(image);
            if (ModelState.IsValid)
            {
                image = await imageRepository.UploadImageAsync(image);
                return Ok(image);
            }
            return  BadRequest(ModelState);
        }

        private void ValidateImage(Image image)
        {
            var exts = new string[] { ".png", ".jpg", ".jpeg" };
            if (!exts.Contains(image.Extension))
            {
                ModelState.AddModelError("File", "Unsupported file extension");
            }
            if (image.sizeinbytes > 10485760)
            {
                ModelState.AddModelError("File", "Please upload file less than 10 MB");
            }
                

        }
    }
}
