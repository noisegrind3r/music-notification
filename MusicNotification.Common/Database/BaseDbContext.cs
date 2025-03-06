using MusicNotification.Common.Entities;
using MusicNotification.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Reflection;

namespace MusicNotification.Common.Database
{
    public abstract class BaseDbContextUnitOfWork<TDbContext> : DbContext, IUnitOfWork
        where TDbContext : DbContext
    {
        private IDbContextTransaction? _dbContextTransaction;

        public BaseDbContextUnitOfWork(DbContextOptions<TDbContext> options)
            : base(options)
        {
        }

        public async Task<IDisposable> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
        {
            _dbContextTransaction = await Database.BeginTransactionAsync(isolationLevel, cancellationToken);
            return _dbContextTransaction;
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_dbContextTransaction != null)
                await _dbContextTransaction.CommitAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        public Task<int> SaveChangesAsync(string userName, CancellationToken cancellationToken = default)
        {
            AddTimestamps(userName);
            UpdateSoftDeleteStatuses();
            var saveChangesResult = base.SaveChangesAsync(cancellationToken);
            return saveChangesResult;
        }
        

        private void AddTimestamps(string? userName = default)
        {
            var entities = ChangeTracker?.Entries().Where(x => x.State is EntityState.Added or EntityState.Modified or EntityState.Deleted);

            if (entities is null)
                return;

            foreach (var entry in entities)
            {
                var entity = entry.Entity;

                if (entity is null) continue;

                if (entity.GetType().BaseType == typeof(BaseAuditableEntity) || (entity.GetType().BaseType?.IsSubclassOf(typeof(BaseAuditableEntity)) ?? false))
                {
                    var currentDateTime = DateTimeOffset.Now.ToUniversalTime();

                    var BaseEntity = entity as BaseAuditableEntity;

                    if (BaseEntity is not null && entry.State == EntityState.Added)
                    {
                        BaseEntity.CreatedAt = currentDateTime;
                    }
                    ((BaseAuditableEntity)entity).UpdatedAt = currentDateTime;
                }
            }
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var entity = entry.Entity;

                if (entity is null) continue;

                if (entity.GetType().BaseType == typeof(BaseAuditableEntity) || (entity.GetType().BaseType?.IsSubclassOf(typeof(BaseAuditableEntity)) ?? false))
                {

                    switch (entry.State)
                    {
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["DeletedAt"] = DateTimeOffset.Now.ToUniversalTime();
                            break;
                    }
                }
            }
        }
    }
}
