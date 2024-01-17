using Employee.Data.Core.DTO;

namespace Employee.Data.Interface
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employees>> GetAllEmployeeAsync();
        Task<Employees> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(Employees employee);
        Task UpdateEmployeeAsync(int id, EmployeeDTO employee);
        Task DeleteEmployeeAsync(Employees employee);
    }
}
