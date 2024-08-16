using Domain.Entities;

namespace Application.Models
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; } = null;
        public string? ModifiedBy { get; set; } = null;

        public List<TaskDto> Tasks { get; set; } = new();

        public static ProjectDto Create(Project project)
        {
            return new()
            {
                Id = project.Id,
                CreatedAt = project.CreatedAt,
                CreatedBy = project.CreatedBy,
                ModifiedAt = project.ModifiedAt,
                ModifiedBy = project.ModifiedBy,
                Description = project.Description,
                Name = project.Name,
                Tasks = project.Tasks is null ? new() : project.Tasks.Select(s => TaskDto.Create(s)).ToList(),
            };
        }
    }
}
