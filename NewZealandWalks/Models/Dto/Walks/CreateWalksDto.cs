using NewZealandWalks.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace NewZealandWalks.Models.Dto.Walks
{
    public class CreateWalksDto
    {
        [Required]
        [MaxLength(100,ErrorMessage ="Max length is 100 char")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000,ErrorMessage ="Max length is 1000 char")]
        public string Description { get; set; }

        [Required]
        [Range(0,100,ErrorMessage = "LengthInKm must be between 0 to 100")]
        public double LengthInKm { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
