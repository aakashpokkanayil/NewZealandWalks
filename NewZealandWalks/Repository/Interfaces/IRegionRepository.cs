using NewZealandWalks.Models.Domain;

namespace NewZealandWalks.Repository.Interfaces
{
    public interface IRegionRepository : IBaseRepository<Region>
    {
        Task<Region?> UpdateAsync(Guid id, Region region);
    }
}
