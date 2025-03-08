using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MusicNotification.Common.Database;

namespace MusicNotification.Feeder;

public class FeederDbContext(DbContextOptions<FeederDbContext> options) : BaseDbContextUnitOfWork<FeederDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
