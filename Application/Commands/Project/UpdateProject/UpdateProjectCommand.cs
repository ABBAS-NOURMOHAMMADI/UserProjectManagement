using Application.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Project.UpdateProject
{
    public class UpdateProjectCommand : IRequest<ProjectDto>, IUpdateCommand
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
