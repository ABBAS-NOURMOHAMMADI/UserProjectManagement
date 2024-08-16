using Application.Commands.Task.CreateTask;
using Application.Commands.Task.DeleteTask;
using Application.Commands.Task.UpdateTask;
using Application.Queries.Task.GetListTask;
using Microsoft.AspNetCore.Mvc;

namespace UserProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TaskController : BaseController
    {
        private readonly ILogger<TaskController> _logger;

        public TaskController(ILogger<TaskController> logger)
            : base(logger)

        {
            _logger = logger;
        }

        [HttpPost("{ProjectId}")]
        public async Task<IActionResult> Create([FromRoute] int ProjectId, [FromBody] CreateTaskCommand command)
        {
            command.ProjectId = ProjectId;
            return await HandleRequest(command);
        }

        [HttpPatch("{ProjectId}/{TaskId}")]
        public async Task<IActionResult> Update([FromRoute] int ProjectId, [FromRoute] int TaskId, [FromBody] UpdateTaskCommand command)
        {
            command.ProjectId = ProjectId;
            command.TaskId = TaskId;
            return await HandleRequest(command);
        }

        [HttpDelete("{ProjectId}/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int ProjectId, [FromRoute] int Id)
        {
            return await HandleRequest(new DeleteTaskCommand { ProjectId = ProjectId, TaskId = Id });
        }

        [HttpGet("{ProjectId}")]
        public async Task<IActionResult> Tasks([FromRoute] int ProjectId)
        {
            return await HandleRequest(new GetListTaskQuery { ProjectId = ProjectId });
        }
    }
}
