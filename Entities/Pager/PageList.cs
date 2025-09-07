using TestAPI.Entities.Base;

namespace TestAPI.Entities.Pager
{
    public class PageList<T> where T : BaseEntity
    {
        public int Skip { get; set; }
        public int Count { get; set; }
        public List<T>? Items { get; set; }
    }
}