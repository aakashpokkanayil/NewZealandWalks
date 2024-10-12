namespace NewZealandWalks.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? ImageUrl { get; set; }

        //foriegn key columns
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        // Navigation Properties : define relationship between entities
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }

    }
}
