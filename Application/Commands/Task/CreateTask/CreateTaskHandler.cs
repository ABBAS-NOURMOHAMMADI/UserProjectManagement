using Application.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Task.CreateTask
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskDto>
    {
        private readonly IApplicationDbContext context;

        public CreateTaskHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var project = await context.Project.FindAsync(request.ProjectId);

            if (project is null || project.DeletedAt.HasValue)
                throw new Exception("project not found");

            var task = new Domain.Entities.Task
            {
                Name = request.Name,
                ProjectId = project.Id,
                Status = request.Status,
                Description = request.Description,
                DueDate = request.DueDate
            };

            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            return TaskDto.Create(task);
        }
    }
}
