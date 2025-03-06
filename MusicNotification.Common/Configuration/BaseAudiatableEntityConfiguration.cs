using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicNotification.Common.Entities;

namespace MusicNotification.Common.Configuration;

public abstract class BaseAudiatableEntityConfiguration<TEntity> : BaseEntityConfiguration<TEntity>
    where TEntity : BaseAuditableEntity
{
    public string schema = "public";

    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {

        builder
            .HasIndex(b => b.DeletedAt);

        builder.HasQueryFilter(x => EF.Property<DateTime?>(x, "DeletedAt") == DateTime.MinValue || EF.Property<DateTime?>(x, "DeletedAt") == null);
        base.Configure(builder);
    }
}
