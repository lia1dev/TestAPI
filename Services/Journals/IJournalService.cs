using TestAPI.Entities.Journals;
using TestAPI.Entities.Pager;

namespace TestAPI.Services.Journals
{
    public interface IJournalService
    {
        Task<Journal> GetJournalByIdAsync(long id);
        Task<PageList<Journal>> SearchByFilterAsync(int skip, int take, JournalFilter? filter = null);
    }
}