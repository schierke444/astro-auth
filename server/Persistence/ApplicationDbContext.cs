using System.Reflection;
using Microsoft.EntityFrameworkCore;
using server.Entities;
using server.Persistence;
using server.Services;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<User> User => Set<User>();
    public DbSet<Items> Items => Set<Items>();
    private readonly IPasswordService _passwordService;

    private readonly IConfiguration _config;
    public ApplicationDbContext(IConfiguration config, IPasswordService passwordService)
    {
        _config = config;   
        _passwordService = passwordService;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_config["ConnectionStrings:DB"]);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.AddSeedBuilder(_config, _passwordService);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var item in ChangeTracker.Entries<BaseEntity>())
        {
           if(item.State == EntityState.Added) 
           {
                item.Entity.CreatedAt = DateTime.UtcNow;
                item.Entity.UpdatedAt = DateTime.UtcNow;
                break;
           }

           if(item.State == EntityState.Modified)
           {
                item.Entity.UpdatedAt = DateTime.UtcNow;
                break;
           }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
