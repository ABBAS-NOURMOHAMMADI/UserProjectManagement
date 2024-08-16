using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Task.DeleteTask
{
    public class DeleteTaskCommand : IRequest<bool>, IDeleteCommand
    {
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
    }
}
