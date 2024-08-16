using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Task.DeleteTask
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IApplicationDbContext context;

        public DeleteTaskHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            this.context = context;
            this.currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var project = await context.Project.FindAsync(request.ProjectId);
            if (project is null || project.DeletedAt.HasValue)
                throw new Exception("project not found");

            var task = await context.Tasks.FindAsync(request.TaskId);
            if (task is null || task.DeletedAt.HasValue)
                throw new Exception("task not found");

            if (task.ProjectId != project.Id)
                throw new Exception();

            task.DeletedAt = DateTime.UtcNow;
            task.DeletedBy = currentUserService.UserId;

            context.Tasks.Update(task);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
