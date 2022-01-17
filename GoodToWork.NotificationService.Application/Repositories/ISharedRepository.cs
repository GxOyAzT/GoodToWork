namespace GoodToWork.NotificationService.Application.Repositories;

public interface ISharedRepository<TEntity>
{
    Task<List<TEntity>> Get();
    Task<TEntity> Find(Guid id);
    Task Insert(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(TEntity entity);
}
