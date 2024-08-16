using Domain.Entities.Base;
using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Task : BaseEntity<int>, ICreateEntity, IModifiedEntity, ISoftDeleteEntity
    {
        public required string Name { get; set; }
        public string Description { get; set; }
        public required Status Status { get; set; } = Status.Todo;
        public DateTime? DueDate { get; set; } = null;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; } = null;
        public string? ModifiedBy { get; set; } = null;
        public DateTime? DeletedAt { get; set; } = null;
        public string? DeletedBy { get; set; } = null;

        public required int ProjectId { get; set; }
        public Project Project { get; set; }    
    }
}
