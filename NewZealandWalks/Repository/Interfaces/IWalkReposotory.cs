using NewZealandWalks.Models.Domain;

namespace NewZealandWalks.Repository.Interfaces
{
    public interface IWalkReposotory : IBaseRepository<Walk>
    {
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
    }
}
