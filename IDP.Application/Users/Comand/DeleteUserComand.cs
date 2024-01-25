using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace IDP.Application.Users.Comand
{
    public class DeleteUserComand : IRequest<DeleteUserResponseDto>
    {
        public DeleteUserComand(int id)
        {
            Id = id;
        }
        //public UserDto User { get; set; }
        public int Id { get; set; }
    }

    public class DeleteUserHandler : IRequestHandler<DeleteUserComand, DeleteUserResponseDto>
    {
        private readonly IConfiguration _configuration;
        public DeleteUserHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<DeleteUserResponseDto> Handle(DeleteUserComand request, CancellationToken cancellationToken)
        {
            var connectionstring = _configuration.GetConnectionString("IDPConnectionString");
            var sql = "DELETE FROM USERS WHERE UserId = @Id";
            var connection = new SqlConnection(connectionstring);
            connection.Execute(sql, new { request.Id });
            return await Task.FromResult(new DeleteUserResponseDto
            {
                Id = request.Id,
            });
        }
    }

    public class DeleteUserResponseDto
    {
        public int Id { get; set; }
    }
}
