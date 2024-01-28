using IDP.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace IDP.Application.Context
{
    public interface IIDPContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserToken> UserTokens { get; set; }




        int SaveChanges(bool AcceptAllChangeSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool AcceptAllChangeSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }

    
}
