using Infrastructure.Exceptions;
using Infrastructure.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        private readonly ILogger logger;

        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        public BaseController(ILogger logger)
        {
            this.logger = logger;
        }

        async protected Task<IActionResult> HandleRequest<T>(IRequest<T> command, IServiceScope serviceScope = null, CancellationToken cancellationToken = default)
        {
            try
            {
                IMediator mediator = serviceScope != null ? serviceScope.ServiceProvider.GetService<IMediator>() : Mediator;
                var result = await mediator.Send(command, cancellationToken);
                if (result is Unit)
                    return NoContent();

                return Ok(result);
            }
            catch (TimeoutException tex)
            {
                logger.LogError(tex, "HandleRequest failed");
                return Problem(title: tex.Message, statusCode: 504, detail: tex.FullMessage());
            }
            catch (UnauthorizedAccessException accessEx)
            {
                logger.LogError(accessEx, "HandleRequest failed");
                return Unauthorized(accessEx.Message);
            }
            catch (ForbiddenException accessEx)
            {
                logger.LogError(accessEx, "HandleRequest failed");
                return Forbid();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "HandleRequest failed");
                return BadRequest(new BadRequest(ex));
            }
        }
    }
}
