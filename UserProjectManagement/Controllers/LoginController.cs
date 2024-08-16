using Application.Queries.Login;
using Microsoft.AspNetCore.Mvc;

namespace UserProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly ILogger<ProjectController> _logger;

        public LoginController(ILogger<ProjectController> logger)
            : base(logger)

        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginQuery command)
        {
            return await HandleRequest(command);
        }
    }
}
