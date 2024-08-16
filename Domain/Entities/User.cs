using Domain.Interfaces;

namespace Domain.Entities
{
    public class User : ICreateEntity, IModifiedEntity, ISoftDeleteEntity
    {
        public required string UserId { get; set; }
        public required string UserName { get; set; }
        public required string NormalizedUserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; } // didnt hash just for test
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; } = null;
        public string? ModifiedBy { get; set; } = null;
        public DateTime? DeletedAt { get; set; } = null;
        public string? DeletedBy { get; set; } = null;
    }
}
