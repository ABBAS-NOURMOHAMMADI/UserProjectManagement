using Application.Models;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Task.UpdateTask
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, TaskDto>
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IApplicationDbContext context;

        public UpdateTaskHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            this.context = context;
            this.currentUserService = currentUserService;
        }

        public async Task<TaskDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var project = await context.Project.FindAsync(request.ProjectId);
            if (project is null || project.DeletedAt.HasValue)
                throw new Exception("project not found");

            var task = await context.Tasks.FindAsync(request.TaskId);
            if (task is null || task.DeletedAt.HasValue)
                throw new Exception("task not found");

            if (task.ProjectId != project.Id)
                throw new Exception();

            if (TaskInfoUpdate(request, task))
            {
                task.ModifiedAt = DateTime.UtcNow;
                task.ModifiedBy = currentUserService.UserId;
                context.Project.Update(project);
                await context.SaveChangesAsync();
            }

            return TaskDto.Create(task);
        }

        private bool TaskInfoUpdate(UpdateTaskCommand request, Domain.Entities.Task task)
        {
            if (request.Name != task.Name)
            {
                task.Name = request.Name;
            }

            if (request.Description != task.Description)
            {
                task.Description = request.Description;
            }

            if (request.Status != task.Status)
            {
                task.Description = request.Description;
            }

            if (request.DueDate != task.DueDate)
            {
                task.Description = request.Description;
            }

            var entry = context.GetChangeTracker().Entries<Domain.Entities.Task>();

            foreach (var item in entry)
                if (item.State == EntityState.Modified)
                    return true;

            return false;
        }
    }
}
