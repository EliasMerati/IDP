using Dapper;
using IDP.Core.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace IDP.Application.Users.Comand
{

    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int UserAge { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }

    public class AddUserComand : IRequest<AddUserResponseDto>
    {
        public AddUserComand(UserDto user)
        {
            User = user;
        }
        public UserDto User { get; set; }

    }

    public class AddUserHandler : IRequestHandler<AddUserComand, AddUserResponseDto>
    {
        private readonly IConfiguration _configuration;
        public AddUserHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<AddUserResponseDto> Handle(AddUserComand request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                UserId = request.User.Id,
                FullName = request.User.FullName,
                Password = request.User.Password,
                UserAge = request.User.UserAge,
                UserName = request.User.UserName,
                CreateDate = DateTime.Now,
                IsActive = true,
            };

            var connectionstring = _configuration.GetConnectionString("IDPConnectionString");
            var sql = "INSERT INTO USERS(FullName,UserAge,UserName,Password,CreateDate) VALUES (@fullname,@userage,@username,@password , @createdate)";
            var con = new SqlConnection(connectionstring);
            con.Execute(sql, new { user.FullName, user.UserAge, user.UserName, user.Password , user.CreateDate });
            return await Task.FromResult(new AddUserResponseDto
            {
                Id = user.UserId,
            });
        }
    }
    public class AddUserResponseDto
    {
        public int Id { get; set; }
    }
}
