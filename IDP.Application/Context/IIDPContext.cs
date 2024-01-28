using IDP.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace IDP.Application.Context
{
    public interface IIDPContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserToken> UserTokens { get; set; }
    }
}
