using Application.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Project.CreateProject
{
    public class CreateProjectCommand : IRequest<ProjectDto>, ICreateCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
