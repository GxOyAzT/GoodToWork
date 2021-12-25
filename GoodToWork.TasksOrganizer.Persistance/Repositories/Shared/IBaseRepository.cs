namespace GoodToWork.TasksOrganizer.Persistance.Repositories.Shared;

internal interface IBaseRepository<TEntity>
{
    Task<TEntity> Find(Func<TEntity, bool> filter);
    Task<List<TEntity>> Get(Func<TEntity, bool> filter);
    Task<List<TEntity>> Get();
    Task<TEntity> Add(TEntity entity);
    Task Update(TEntity entity);
    Task Delete (TEntity entity);
}
