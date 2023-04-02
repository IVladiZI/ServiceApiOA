using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <BaseApiController>
    /// We are in the process of versioning and shipping the controller.
    /// This class was created for any Api controller to inherit the IMediator.
    /// </BaseApiController>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
