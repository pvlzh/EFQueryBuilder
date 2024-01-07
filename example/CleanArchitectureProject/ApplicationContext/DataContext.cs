using Domain.Entities;
using Domain.Entities.Base;
using Domain.Interfaces;
using Domain.Interfaces.Entities;
using EFQueryBuilder;
using EFQueryBuilder.Abstractions;
using EFQueryBuilder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationContext;

/// <summary>
/// Контекст базы.
/// </summary>
internal class DataContext : DbContext, IDataContext
{
    /// <summary>
    /// Пользователи.
    /// </summary>
    public DbSet<Participant> Users { get; set; } = null!;
    
    /// <summary>
    /// Договора.
    /// </summary>
    public DbSet<Contract> Contracts { get; set; } = null!;
    
    /// <inheritdoc cref="DataContext"/>
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }


    /// <inheritdoc />
    public IQuery<TEntity> Get<TEntity>(Specification<TEntity> specification) 
        where TEntity : Entity
    {
        return QueryEvaluator.Evaluate(Set<TEntity>(), specification);
    }

    /// <inheritdoc />
    public IProjectionQuery<TObject> Get<TEntity, TObject>(Projection<TEntity, TObject> projection) 
        where TEntity : Entity
    {
        return QueryEvaluator.Evaluate(Set<TEntity>(), projection);
    }
    
    /// <inheritdoc cref="IDataContext.SaveChanges"/>
    public void Add<TEntity>(TEntity entity) 
        where TEntity : IAdditionalEntity
    {
        base.Add(entity);
    }
    
    /// <inheritdoc cref="IDataContext.SaveChanges"/>
    public void Remove<TEntity>(TEntity entity) 
        where TEntity : IRemovableEntity
    {
        base.Remove(entity);
    }
    
    /// <inheritdoc cref="IDataContext.SaveChanges"/>
    public void SaveChanges()
    {
        base.SaveChanges();
    }
    
    /// <inheritdoc cref="IDataContext.SaveChangesAsync"/>
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}