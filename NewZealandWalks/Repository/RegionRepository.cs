using Microsoft.EntityFrameworkCore;
using NewZealandWalks.Data;
using NewZealandWalks.Models.Domain;
using NewZealandWalks.Repository.Interfaces;

namespace NewZealandWalks.Repository
{
    public class RegionRepository: BaseRepository<Region>, IRegionRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext):base(nZWalksDbContext)
        {
            this._nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingEntity= await _nZWalksDbContext.Regions.FirstOrDefaultAsync(r=>r.Id==id);
            if (existingEntity==null) return null;
            existingEntity.Code = region.Code;
            existingEntity.Name = region.Name;
            existingEntity.ImageUrl = region.ImageUrl;
            _nZWalksDbContext.Update(existingEntity);
            await _nZWalksDbContext.SaveChangesAsync();
            return existingEntity;
        }
    }
}
