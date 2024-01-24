using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace IDP.Application.Users.Comand
{

    public class EditUserCommand : IRequest<EditUserResponseDto>
    {
        public EditUserCommand(UserDto user)
        {
            User = user;
        }
        public UserDto User { get; set; }
    }

    public class EditUserHandler : IRequestHandler<EditUserCommand, EditUserResponseDto>
    {
        private readonly IConfiguration _configuration;
        public EditUserHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<EditUserResponseDto> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var connectionstring = _configuration.GetConnectionString("IDPConnectionString");
            var sql = "UPDATE USERS SET FullName = @fullname,UserAge = @userage,UserName = @username,Password = @password WHERE UserId = @Id";
            var connection = new SqlConnection(connectionstring);
            connection.Execute(sql, new { request.User.FullName, request.User.UserAge, request.User.UserName, request.User.Password, request.User.Id });
            return await Task.FromResult(new EditUserResponseDto
            {
                Id = request.User.Id,
            });
        }
    }
    public class EditUserResponseDto
    {
        public int Id { get; set; }
    }
}
