using System.ComponentModel.DataAnnotations;

namespace NewZealandWalks.Models.Dto.Region
{
    public class CreateRegionDto
    {
        [Required]
        [MaxLength(3,ErrorMessage = "Max lenth is 3 char")]
        [MinLength(3,ErrorMessage = "Min lenth is 3 char")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100,ErrorMessage ="Max lenth is 100 char")]
        public string Name { get; set; }

        public string? ImageUrl { get; set; }
    }
}
