using System.ComponentModel.DataAnnotations;

namespace NewZealandWalks.Models.Dto.Region
{
    public class UpdateRegionDto
    {
        [Required]
        [MaxLength(3,ErrorMessage ="Max length 3 char")]
        [MinLength(3,ErrorMessage ="Min length 3 char")]
        public string Code { get; set; }

        [MaxLength(100,ErrorMessage ="Max length 100 char")]
        public string Name { get; set; }

        public string? ImageUrl { get; set; }
    }
}
