using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Project.DeleteProject
{
    public class DeleteProjectCommand : IRequest<bool>, IDeleteCommand
    {
        public int ProjectId { get; set; }
    }
}
