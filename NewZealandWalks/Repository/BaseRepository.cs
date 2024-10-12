
using Microsoft.EntityFrameworkCore;
using NewZealandWalks.Data;
using NewZealandWalks.Repository.Interfaces;

namespace NewZealandWalks.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly NZWalksDbContext _nZWalksDbContext;

        public BaseRepository(NZWalksDbContext nZWalksDbContext) 
        {
            this._nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _nZWalksDbContext.Set<T>().AddAsync(entity);
            await _nZWalksDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> DeleteAsync(Guid id)
        {
            var existingEntity= await _nZWalksDbContext.Set<T>().FindAsync(id);
            if (existingEntity == null) return null;
            _nZWalksDbContext.Set<T>().Remove(existingEntity);
            await _nZWalksDbContext.SaveChangesAsync();
            return existingEntity;
        }

        public async virtual Task<List<T>> GetAllAsync()
        {
            return await _nZWalksDbContext.Set<T>().ToListAsync();
        }

        public async virtual Task<T?> GetByIdAsync(Guid id)
        {
            return await _nZWalksDbContext.Set<T>().FindAsync(id);
        }

        public async virtual Task<List<T>> GetAllAsync(string? filterOn, string? filterQuery, string? sortOn, bool isAscending, int pageNumber = 1, int pageSize = 1000)
        {
            throw new NotImplementedException();
        }
    }
}
