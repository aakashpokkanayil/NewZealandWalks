using Microsoft.EntityFrameworkCore;
using NewZealandWalks.Data;
using NewZealandWalks.Models.Domain;
using NewZealandWalks.Repository.Interfaces;

namespace NewZealandWalks.Repository
{
    public class WalkRepository : BaseRepository<Walk>, IWalkReposotory
    {
        private readonly NZWalksDbContext _nZWalksDbContext;

        public WalkRepository(NZWalksDbContext nZWalksDbContext):base(nZWalksDbContext)
        {
            this._nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<Walk?> UpdateAsync(Guid id,Walk walk)
        {
            var walksDomainModel= await _nZWalksDbContext.Walks.FirstOrDefaultAsync(r => r.Id == id);
            if (walksDomainModel == null) return null; 
            
            walksDomainModel.Name = walk.Name;
            walksDomainModel.Description = walk.Description;    
            walksDomainModel.LengthInKm = walk.LengthInKm;
            walksDomainModel.ImageUrl = walk.ImageUrl;
            walksDomainModel.DifficultyId = walk.DifficultyId;
            walksDomainModel.RegionId = walk.RegionId;  

            _nZWalksDbContext.Walks.Update(walksDomainModel);
            await _nZWalksDbContext.SaveChangesAsync();

            return walksDomainModel;

        }

        public override async Task<List<Walk>> GetAllAsync(string? filterOn,string? filterQuery, string? sortOn, bool isAscending, int pageNumber = 1, int pageSize = 1000)
        {
            var walksDomainModel= _nZWalksDbContext.Walks.Include("Region").Include("Difficulty").AsQueryable();

            if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery) && !string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery)) {

                if (filterOn.Contains("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walksDomainModel=walksDomainModel.Where(x => x.Name.Contains(filterQuery));
                }
            }

            if (!string.IsNullOrEmpty(sortOn)  && !string.IsNullOrWhiteSpace(sortOn))
            {

                if (sortOn.Contains("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walksDomainModel = isAscending!=false?walksDomainModel.OrderBy(x=>x.Name):walksDomainModel.OrderByDescending(x=>x.Name);
                }
                else if (sortOn.Contains("distance", StringComparison.OrdinalIgnoreCase))
                {
                    walksDomainModel = isAscending != false ? walksDomainModel.OrderBy(x => x.LengthInKm) : walksDomainModel.OrderByDescending(x => x.LengthInKm);
                }
            }

            int skipnumber = (pageNumber - 1) * pageSize;
            return await walksDomainModel.Skip(skipnumber).Take(pageSize).ToListAsync(); ;
        }

        public override async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _nZWalksDbContext.Walks.Include("Region").Include("Difficulty").FirstOrDefaultAsync(x=>x.Id==id);
        }

    }
}
