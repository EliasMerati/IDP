using IDP.Core.Entities;
using System.Data.Entity;

namespace IDP.Persistence.Context
{
    public class IDPContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
    }
}
