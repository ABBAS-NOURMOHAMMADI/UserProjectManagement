using Domain.Enums;

namespace Application.Models
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; } = Status.Todo;
        public DateTime? DueDate { get; set; } = null;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; } = null;
        public string? ModifiedBy { get; set; } = null;
        public int ProjectId { get; set; }

        public static TaskDto Create(Domain.Entities.Task task)
        {
            return new()
            {
                Id = task.Id,
                CreatedAt = task.CreatedAt,
                CreatedBy = task.CreatedBy,
                ModifiedAt = task.ModifiedAt,
                ProjectId = task.ProjectId,
                Description = task.Description,
                Status = task.Status,
                DueDate = task.DueDate,
                ModifiedBy = task.ModifiedBy,
                Name = task.Name
            };
        }
    }
}
