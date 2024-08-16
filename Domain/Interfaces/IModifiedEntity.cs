namespace Domain.Interfaces
{
    public interface IModifiedEntity
    {
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
