using Application.Models;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Task.CreateTask
{
    public class CreateTaskCommand : IRequest<TaskDto>, ICreateCommand
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; } = Status.Todo;
        public DateTime? DueDate { get; set; } = null;
    }
}
