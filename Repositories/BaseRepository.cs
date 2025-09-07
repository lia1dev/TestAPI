using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Entities.Base;
using TestAPI.Helpers;

namespace TestAPI.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private TreeDbContext _context { get; set; }

        public BaseRepository(TreeDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> TableNoTracking => _context.Set<T>().AsNoTracking();
        public IQueryable<T> Table => _context.Set<T>();

        public async Task<IList<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(long id) => await _context.Set<T>().FirstOrDefaultAsync(m => m.Id == id);

        public async Task<T> CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var item = await _context.Set<T>().FirstOrDefaultAsync(m => m.Id == entity.Id);

            if (item != null)
            {
                item = entity;
                _context.Update(item);
                await _context.SaveChangesAsync();
            }
            
            return item;
        }

        public async Task DeleteAsync(long id)
        {
            var item = await _context.Set<T>().FirstOrDefaultAsync(m => m.Id == id);

            if (item != null)
            {
                _context.Set<T>().Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRangeAsync(long[] ids)
        {
            var items = await _context.Set<T>().Where(m => ids.Contains(m.Id)).ToListAsync();

            if (items.IsAny())
            {
                _context.Set<T>().RemoveRange(items);
                await _context.SaveChangesAsync();
            }
        }
    }
}