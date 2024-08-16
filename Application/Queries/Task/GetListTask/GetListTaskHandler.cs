using Application.Models;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Task.GetListTask
{
    public class GetListTaskHandler :
                 IRequestHandler<GetListTaskQuery, GetListTaskQueryResult>
    {
        private readonly IApplicationDbContext context;

        public GetListTaskHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<GetListTaskQueryResult> Handle(GetListTaskQuery request,
                                           CancellationToken cancellationToken)
        {
            var project = await context.Project.FindAsync(request.ProjectId);

            if (project is null || project.DeletedAt.HasValue)
                throw new Exception("project not found");

            var tasks = await context.Tasks
                          .Where(t => t.DeletedAt == null && 
                                      t.ProjectId == project.Id).ToListAsync();

            if (tasks is null)
                return new(new());

            return new(tasks.Select(t => TaskDto.Create(t)).ToList());
        }
    }
}
