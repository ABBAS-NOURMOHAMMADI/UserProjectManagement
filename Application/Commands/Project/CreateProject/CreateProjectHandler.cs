using Application.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Project.CreateProject
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IApplicationDbContext context;

        public CreateProjectHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            this.context = context;
            this.currentUserService = currentUserService;
        }

        public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Domain.Entities.Project
            {
                Name = request.Name,
                Description = request.Description
            };

            context.Project.Add(project);
            await context.SaveChangesAsync();

            return ProjectDto.Create(project);
        }
    }
}
