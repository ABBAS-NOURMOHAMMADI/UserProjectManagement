using Application.Models;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Task.UpdateTask
{
    public class UpdateTaskCommand : IRequest<TaskDto>, IUpdateCommand
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; } = Status.Todo;
        public DateTime? DueDate { get; set; } = null;
        public int ProjectId { get; set; }
    }
}
