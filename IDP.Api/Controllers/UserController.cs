using IDP.Application.Users.Comand;
using IDP.Application.Users.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IDP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            GetAllUsersQuery send = new GetAllUsersQuery();
            var result = _mediator.Send(send);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(UserDto user)
        {
            AddUserComand send = new AddUserComand(user);
            var result = _mediator.Send(send);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put(UserDto user)
        {
            EditUserCommand send = new EditUserCommand(user);
            var result = _mediator.Send(send);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            DeleteUserComand send = new DeleteUserComand(id);
            var result = _mediator.Send(send);
            return Ok(result);
        }
    }
}
