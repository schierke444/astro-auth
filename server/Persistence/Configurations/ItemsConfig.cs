using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Entities;

namespace server.Persistence.Configurationsl;

public class ItemsConfig : IEntityTypeConfiguration<Items>
{
    public void Configure(EntityTypeBuilder<Items> builder)
    {
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.UserId);
    }
}