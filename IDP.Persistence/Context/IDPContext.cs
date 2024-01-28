using IDP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using IDP.Application.Context;

namespace IDP.Persistence.Context
{
    public class IDPContext : DbContext , IIDPContext
    {
        public IDPContext(DbContextOptions<IDPContext> options) : base(options)
        {
                
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
    }
}
