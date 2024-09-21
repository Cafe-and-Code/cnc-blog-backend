using Blog.API.Commons;
using System.Linq.Expressions;

namespace Blog.API.Repositories.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {

        Task<TEntity> AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> AddManyAsync(IEnumerable<TEntity> entities);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> UpdateManyAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllAsync(LinqFindOptions? findOptions = null);
        Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate, LinqFindOptions? findOptions = null);
        Task<List<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> predicate, LinqFindOptions? findOptions = null);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, LinqFindOptions? findOptions = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
