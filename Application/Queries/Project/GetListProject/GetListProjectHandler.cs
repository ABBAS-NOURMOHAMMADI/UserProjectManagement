using Application.Models;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Project.GetListProject
{
    public class GetListProjectHandler :
                 IRequestHandler<GetListProjectQuery, GetListProjectQueryResult>
    {
        private readonly IApplicationDbContext context;

        public GetListProjectHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<GetListProjectQueryResult> Handle(GetListProjectQuery request, CancellationToken cancellationToken)
        {
            var projects = await context.Projects
                           .Include(t => t.Tasks.Where(t => t.DeletedAt == null))
                           .Where(p => p.DeletedAt == null).ToListAsync();

            if (projects is null)
                return new(new());

            return new(projects.Select(p => ProjectDto.Create(p)).ToList());
        }
    }
}
