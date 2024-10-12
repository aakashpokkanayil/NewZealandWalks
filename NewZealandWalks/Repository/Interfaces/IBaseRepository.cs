using NewZealandWalks.Models.Domain;

namespace NewZealandWalks.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortOn = null, bool isAscending = false, int pageNumber = 1, int pageSize = 1000);
        Task<T?> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task<T?> DeleteAsync(Guid id);

    }
}
