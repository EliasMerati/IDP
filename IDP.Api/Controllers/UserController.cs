using IDP.Application.Users.Comand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IDP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public IActionResult Post(UserDto user)
        {
            AddUserComand send = new AddUserComand(user);
            var result = _mediator.Send(send);
            return Ok(result);
        }
    }
}
