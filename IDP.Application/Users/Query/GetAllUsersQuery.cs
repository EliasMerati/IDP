using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace IDP.Application.Users.Query
{
    public class GetAllUsersQuery : IRequest<IEnumerable<GetAllUsersResponse>>
    {

    }

    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<GetAllUsersResponse>>
    {
        private readonly IConfiguration _configuration;
        public GetAllUsersHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<GetAllUsersResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var connectionstring = _configuration.GetConnectionString("IDPConnectionString");
            var sql = "SELECT * FROM USERS";
            var con = new SqlConnection(connectionstring);
            var result = con.Query<GetAllUsersResponse>(sql);
            return await Task.FromResult(
                result.Select(p => new GetAllUsersResponse
                {
                    FullName = p.FullName,
                    Password = p.Password,
                    UserAge = p.UserAge,
                    UserName = p.UserName,
                }).ToList());
        }
    }

    public class GetAllUsersResponse
    {
        public string FullName { get; set; }
        public int UserAge { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
