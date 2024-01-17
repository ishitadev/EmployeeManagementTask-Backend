
namespace Employee.Data.Core.Interface
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllDepartmentAsync();
        Task<Department> GetDepartmentByIdAsync(int id);
        Task AddDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(Department department);
    }
}
