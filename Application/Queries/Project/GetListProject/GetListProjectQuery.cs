using Application.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries.Project.GetListProject
{
    public class GetListProjectQuery : IRequest<GetListProjectQueryResult>, IQuery
    {
    }

    public record GetListProjectQueryResult(List<ProjectDto> Projects);
}
