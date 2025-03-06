using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MusicNotification.Common.Database;

namespace MusicNotification.Catalogs;

public class CatalogsDbContext(DbContextOptions<CatalogsDbContext> options) : BaseDbContextUnitOfWork<CatalogsDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
