namespace TestAPI.Common
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        string? CreatedBy { get; set; }
        string? ModifiedBy { get; set; }
        DateTime? CreatedOnUtc { get; set; }
        DateTime? ModifiedOnUtc { get; set; }
    }
}