namespace Domain.Interfaces
{
    public interface ISoftDeleteEntity
    {
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }
}
