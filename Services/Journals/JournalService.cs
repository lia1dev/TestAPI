using Microsoft.EntityFrameworkCore;
using TestAPI.Entities.Journals;
using TestAPI.Entities.Pager;
using TestAPI.Repositories;

namespace TestAPI.Services.Journals
{
    public class JournalService : IJournalService
    {
        private IBaseRepository<Journal> _journals;

        public JournalService(IBaseRepository<Journal> journals)
        {
            _journals = journals;
        }

        public async Task<Journal> GetJournalByIdAsync(long id)
        {
            var item = await _journals.GetByIdAsync(id);

            return item;
        }

        public async Task<PageList<Journal>> SearchByFilterAsync(int skip, int take, JournalFilter? filter = null)
        {
            var query = _journals.TableNoTracking;

            if (filter != null)
            {
                if (filter.From.HasValue)
                    query = query.Where(x => x.CreatedOnUtc >= filter.From.Value);
                if (filter.To.HasValue)
                    query = query.Where(x => x.CreatedOnUtc <= filter.To.Value);
                if (!string.IsNullOrEmpty(filter.Search))
                    query = query.Where(x => x.Text.Contains(filter.Search, StringComparison.OrdinalIgnoreCase));
            }

            var totalItems = await query.CountAsync();

            var items =  await query.Skip(skip).Take(take)
                .Select(j => new Journal
                {
                    Id = j.Id,
                    EventId = j.EventId,
                    Text = j.Text,
                    CreatedOnUtc = j.CreatedOnUtc,
                    ModifiedOnUtc = j.ModifiedOnUtc,
                    CreatedBy = j.CreatedBy,
                    ModifiedBy = j.ModifiedBy
                }).ToListAsync();


            var list = new PageList<Journal>()
            {
                Skip = skip,
                Count = items.Count,
                Items = items
            };

            return list;
        }       
    }
}