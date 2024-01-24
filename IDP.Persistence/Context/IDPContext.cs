using IDP.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace IDP.Persistence.Context
{
    public class IDPContext : DbContext
    {
        public IDPContext(DbContextOptions<IDPContext> options) : base()
        {
                
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
    }
}
