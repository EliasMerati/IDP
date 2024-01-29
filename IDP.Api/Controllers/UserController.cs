using IDP.Application.LogRepository;
using IDP.Application.Users.Comand;
using IDP.Application.Users.Query;
using IDP.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IDP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogRepository _log;
        public UserController(IMediator mediator, ILogger<UserController> logger, ILogRepository log)
        {
            _mediator = mediator;
            _log = log;
        }

        [HttpGet]
        public IActionResult Get()
        {
            GetAllUsersQuery send = new GetAllUsersQuery();
            var result = _mediator.Send(send);
            _log.execute(new LogInfo { Message = "Get All Users" });
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(UserDto user)
        {
            AddUserComand send = new AddUserComand(user);
            var result = _mediator.Send(send);
            _log.execute(new LogInfo { Message = "Add User" });
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put(UserDto user)
        {
            EditUserCommand send = new EditUserCommand(user);
            var result = _mediator.Send(send);
            _log.execute(new LogInfo { Message = "Update User" });
            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int id)
        {
            DeleteUserComand send = new DeleteUserComand(id);
            var result = _mediator.Send(send);
            _log.execute(new LogInfo { Message = "delete User" });
            return Ok(result);
        }
    }
}
