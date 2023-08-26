﻿using System.Linq.Expressions;
using Core.Packages.Paging;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Packages.Repositories;

public interface IAsyncRepository<TEntity, TEntityId> : IQuery<TEntity> where TEntity : Entity<TEntityId>
{
    TEntity? GetAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);

    Paginate<TEntity> GetListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);

    Paginate<TEntity> GetListByDynamic(
        DynamicQuey dynamic,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);

    bool AnyAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    TEntity AddAsync(TEntity entity);

    ICollection<TEntity> AddRangeAsync(ICollection<TEntity> entity);

    TEntity UpdateAsync(TEntity entity);

    ICollection<TEntity> UpdateRangeAsync(ICollection<TEntity> entity);

    TEntity DeleteAsync(TEntity entity, bool permanent = false);

    ICollection<TEntity> DeleteRangeAsync(ICollection<TEntity> entity, bool permanent = false);
}