using Blog.API.Commons;
using Blog.API.Data;
using Blog.API.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog.API.Repositories.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly BlogDbContext _dbContext;
        public BaseRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddManyAsync(IEnumerable<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> UpdateManyAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Find(predicate);
            _dbContext.Set<TEntity>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(LinqFindOptions? findOptions = null)
        {
            return await Get(findOptions).ToListAsync();
        }

        public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate, LinqFindOptions? findOptions = null)
        {
            return await Get(findOptions).FirstOrDefaultAsync(predicate);
        }

        public async Task<List<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> predicate, LinqFindOptions? findOptions = null)
        {
            return await Get(findOptions).Where(predicate).ToListAsync();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, LinqFindOptions? findOptions = null)
        {
            return Get(findOptions).Where(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().CountAsync(predicate);
        }

        protected DbSet<TEntity> Get(LinqFindOptions? findOptions = null)
        {
            findOptions ??= new LinqFindOptions();
            var entity = _dbContext.Set<TEntity>();
            if (findOptions.IsAsNoTracking && findOptions.IsIgnoreAutoIncludes)
            {
                entity.IgnoreAutoIncludes().AsNoTracking();
            }
            else if (findOptions.IsIgnoreAutoIncludes)
            {
                entity.IgnoreAutoIncludes();
            }
            else if (findOptions.IsAsNoTracking)
            {
                entity.AsNoTracking();
            }
            return entity;
        }
    }
}
