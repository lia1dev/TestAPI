using TestAPI.Entities.Base;

namespace TestAPI.Repositories
{
    public interface IBaseRepository<T> where T : IBaseEntity
    {
        Task<IList<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task<T> CreateAsync(T model);
        Task<T> UpdateAsync(T model);
        Task DeleteAsync(long id);
        Task DeleteRangeAsync(long[] ids);
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
    }
}