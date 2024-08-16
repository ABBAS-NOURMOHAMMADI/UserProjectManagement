using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Project.DeleteProject
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, bool>
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IApplicationDbContext context;

        public DeleteProjectHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            this.context = context;
            this.currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await context.Project.FindAsync(request.ProjectId);

            if (project is null || project.DeletedAt.HasValue)
                throw new Exception("project not found");

            project.DeletedAt = DateTime.UtcNow;
            project.DeletedBy = currentUserService.UserId;

            context.Project.Update(project);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
