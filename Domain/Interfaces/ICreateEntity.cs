namespace Domain.Interfaces
{
    public interface ICreateEntity
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
