using Microsoft.EntityFrameworkCore;
using server.Entities;
using server.Services;

public static class SeedDbContext
{
    public static ModelBuilder AddSeedBuilder(this ModelBuilder modelBuilder, IConfiguration _config, IPasswordService _passwordService)
    {
        var newUser = new User{
            Id = Guid.NewGuid(),
            Username = _config["Seed:Admin_Username"]!,
            Password = _passwordService.HashPassword(_config["Seed:Admin_Password"]!),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt  = DateTime.UtcNow
        };
        modelBuilder.Entity<User>().HasData(newUser);

        modelBuilder.Entity<Items>()
            .HasData(new List<Items>()
            {
                new Items
                {
                    Id = Guid.NewGuid(),
                    Name = "Item1",
                    UserId = newUser.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Items
                {
                    Id = Guid.NewGuid(),
                    Name = "Item2",
                    UserId = newUser.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow

                }
            });

        return modelBuilder;   
    }
}