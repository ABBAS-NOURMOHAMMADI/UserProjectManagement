using Application.Commands.Project.CreateProject;
using Application.Commands.Project.DeleteProject;
using Application.Commands.Project.UpdateProject;
using Application.Queries.Project.GetListProject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(ILogger<ProjectController> logger)
            : base(logger)

        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectCommand command)
        {
            return await HandleRequest(command);
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] UpdateProjectCommand command)
        {
            command.ProjectId = Id;
            return await HandleRequest(command);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            return await HandleRequest(new DeleteProjectCommand { ProjectId = Id });
        }

        [HttpGet]
        public async Task<IActionResult> Projects()
        {
            return await HandleRequest(new GetListProjectQuery());
        }
    }
}
