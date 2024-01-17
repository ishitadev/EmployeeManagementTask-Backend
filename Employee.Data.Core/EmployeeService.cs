using Employee.Data.Core.DTO;
using Employee.Data.Interface;

namespace Employee.Data.Core
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Employees>> GetAllEmployeeAsync()
        {
            return await _unitOfWork.repository<Employees>().GetAllAsync();
        }

        public async Task<Employees> GetEmployeeByIdAsync(int id)
        {
            return await _unitOfWork.repository<Employees>().GetByIdAsync(id);
        }

        public async Task AddEmployeeAsync(Employees employee)
        {
            employee.CreatedOn = DateTime.Now;
            await _unitOfWork.repository<Employees>().Add(employee);
            await _unitOfWork.Complete();
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeDTO employee)
        {
            var isExist = await _unitOfWork.repository<Employees>().GetByIdAsync(id);

            if (isExist != null)
            {
                isExist.Name = employee.Name;
                isExist.Email = employee.Email;
                isExist.DOB = employee.DOB;
                isExist.DepartmentId = employee.DepartmentId;
                isExist.Department = employee.Department;
                isExist.UpdatedOn = DateTime.Now;
                await _unitOfWork.repository<Employees>().Update(isExist);
                await _unitOfWork.Complete();
            }
        }

        public async Task DeleteEmployeeAsync(Employees employee)
        {
            await _unitOfWork.repository<Employees>().Delete(employee);
            await _unitOfWork.Complete();
        }
    }
}
