using Microsoft.AspNetCore.Http;
using NewZealandWalks.Data;
using NewZealandWalks.Models.Domain;
using NewZealandWalks.Repository.Interfaces;

namespace NewZealandWalks.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext nZWalksDbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor, NZWalksDbContext nZWalksDbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.nZWalksDbContext = nZWalksDbContext;
        }

        async Task<Image> IImageRepository.UploadImageAsync(Image image)
        {
            var localfilepath=Path.Combine(webHostEnvironment.ContentRootPath,"Uploads",$"{image.Name}{image.Extension}");

            using var stream=new FileStream(localfilepath,FileMode.Create);
            await image.File.CopyToAsync(stream);

            var request = httpContextAccessor.HttpContext.Request;
            var urlFilePath = $"{request.Scheme}://{request.Host}{request.PathBase}/Uploads/{image.Name}{image.Extension}";
            image.path = urlFilePath;

            await nZWalksDbContext.Images.AddAsync(image);
            await nZWalksDbContext.SaveChangesAsync();

            return image;
        }
    }
}
