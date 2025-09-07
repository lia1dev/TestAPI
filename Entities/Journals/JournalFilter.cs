namespace TestAPI.Entities.Journals
{
    public class JournalFilter
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? Search { get; set; }
    }
}