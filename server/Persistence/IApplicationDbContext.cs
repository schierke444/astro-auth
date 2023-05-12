using Microsoft.EntityFrameworkCore;
using server.Entities;

namespace server.Persistence;

public interface IApplicationDbContext
{
    DbSet<User> User {get;}
    DbSet<Items> Items {get;}

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}