using Employee.Data.Contaxt;
using Employee.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Employee.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await _context.Set<T>().AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                await _context.AddAsync<T>(entity);

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<T>> Add(List<T> entities)
        {
            try
            {
                await _context.Set<T>().AddRangeAsync(entities);
                return entities;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> Update(T entity)
        {
            try
            {
                _context.Attach<T>(entity);
                _context.Entry(entity).State = EntityState.Modified;

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> Delete(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<T>> Delete(List<T> entities)
        {
            try
            {
                _context.Set<T>().RemoveRange(entities);

                return entities;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> queryable = _context.Set<T>().Where(expression).AsNoTracking();

                foreach (Expression<Func<T, object>> include in includes)
                {
                    queryable = queryable.Include<T, object>(include);
                }

                return await queryable.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
