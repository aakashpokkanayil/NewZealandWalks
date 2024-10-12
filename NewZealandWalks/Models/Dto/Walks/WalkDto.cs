
using NewZealandWalks.Models.Dto.Difficulty;
using NewZealandWalks.Models.Dto.Region;

namespace NewZealandWalks.Models.Dto.Walks
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? ImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        public DifficultyDto Difficulty { get; set; }
        public RegionDto Region { get; set; }
    }
}
