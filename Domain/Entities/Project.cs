using Domain.Entities.Base;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Project : BaseEntity<int>, ICreateEntity, IModifiedEntity, ISoftDeleteEntity
    {
        public required string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; } = null;
        public string? ModifiedBy { get; set; } = null;
        public DateTime? DeletedAt { get; set; } = null;
        public string? DeletedBy { get; set; } = null;

        public ICollection<Task> Tasks { get; set; }
    }
}
