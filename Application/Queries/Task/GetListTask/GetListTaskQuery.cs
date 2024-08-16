using Application.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries.Task.GetListTask
{
    public class GetListTaskQuery : IRequest<GetListTaskQueryResult>, IQuery
    {
        public required int ProjectId { get; set; }
    }

    public record GetListTaskQueryResult(List<TaskDto> Tasks);
}
