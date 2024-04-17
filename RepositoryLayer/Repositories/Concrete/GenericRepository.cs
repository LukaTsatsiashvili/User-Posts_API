using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context.API;
using RepositoryLayer.Repositories.Abstract;
using System.Linq.Expressions;

namespace RepositoryLayer.Repositories.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }


        public IQueryable<T> GetAllEntity()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }
        public async Task<T> GetEntityByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task AddEntityAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public void UpdateEntity(T entity)
        {
            _dbSet.Update(entity);
        }
        public void DeleteEntity(T entity)
        {
            _dbSet.Remove(entity);
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}
