using System.Linq.Expressions;

namespace Employee.Data.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> Add(T entity);
        Task<List<T>> Add(List<T> entities);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
        Task<List<T>> Delete(List<T> entities);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
    }
}
