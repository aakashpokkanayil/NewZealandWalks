using System.ComponentModel.DataAnnotations;

namespace NewZealandWalks.Models.Dto.Image
{
    public class ImageUploadRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile File { get; set; }
        public string? Description { get; set; }
    }
}
