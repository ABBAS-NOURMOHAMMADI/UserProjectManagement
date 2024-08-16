using Application.Models;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Project.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ProjectDto>
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IApplicationDbContext context;

        public UpdateProjectHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            this.context = context;
            this.currentUserService = currentUserService;
        }

        public async Task<ProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await context.Project.FindAsync(request.ProjectId);

            if (project is null || project.DeletedAt.HasValue)
                throw new Exception("project not found");

            if (ProjectInfoUpdate(request, project))
            {
                project.ModifiedAt = DateTime.UtcNow;
                project.ModifiedBy = currentUserService.UserId;
                context.Project.Update(project);
                await context.SaveChangesAsync();
            }

            return ProjectDto.Create(project);
        }

        private bool ProjectInfoUpdate(UpdateProjectCommand request, Domain.Entities.Project project)
        {
            if (request.Name != project.Name)
            {
                project.Name = request.Name;
            }

            if (request.Description != project.Description)
            {
                project.Description = request.Description;
            }

            var entry = context.GetChangeTracker().Entries<Domain.Entities.Project>();

            foreach (var item in entry)
                if (item.State == EntityState.Modified)
                    return true;

            return false;
        }
    }
}
