using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Shared;
using GoodToWork.TasksOrganizer.Domain.Entities.Shared;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.Shared;

internal abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly AppDbContext _appDbContext;

    public BaseRepository(
        AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<TEntity> Add(TEntity entity)
    {
        await _appDbContext.Set<TEntity>().AddAsync(entity);

        return entity;
    }

    public Task Delete(TEntity entity)
    {
        _appDbContext.Set<TEntity>().Remove(entity);

        return Task.CompletedTask;
    }

    public Task<TEntity> Find(Func<TEntity, bool> filter)
    {
        return Task.FromResult(_appDbContext.Set<TEntity>().FirstOrDefault(filter));
    }

    public Task<List<TEntity>> Get(Func<TEntity, bool> filter)
    {
        return Task.FromResult(_appDbContext.Set<TEntity>().Where(filter).ToList());
    }

    public Task<List<TEntity>> Get() =>
        _appDbContext.Set<TEntity>().ToListAsync();

    public Task Update(TEntity entity)
    {
        _appDbContext.Set<TEntity>().Update(entity);

        return Task.FromResult(entity);
    }
}
