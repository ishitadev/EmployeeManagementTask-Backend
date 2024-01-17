namespace Employee.Data.Interface
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> repository<TEntity>() where TEntity : class;
        Task<int> Complete();
    }
}
