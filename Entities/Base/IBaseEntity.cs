namespace TestAPI.Entities.Base
{
    public interface IBaseEntity
    {
        long Id { get; set; }
        string? CreatedBy { get; set; }
        string? ModifiedBy { get; set; }
        DateTime? CreatedOnUtc { get; set; }
        DateTime? ModifiedOnUtc { get; set; }
    }
}