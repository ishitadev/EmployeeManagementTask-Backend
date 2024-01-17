using Employee.Data.Core.Interface;
using Employee.Data.Interface;

namespace Employee.Data.Core
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentAsync()
        {
            return await _unitOfWork.repository<Department>().GetAllAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _unitOfWork.repository<Department>().GetByIdAsync(id);
        }

        public async Task AddDepartmentAsync(Department department)
        {
            await _unitOfWork.repository<Department>().Add(department);
            await _unitOfWork.Complete();
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            var isExist = await _unitOfWork.repository<Department>().GetByIdAsync(department.Id);

            if (isExist != null)
            {
                await _unitOfWork.repository<Department>().Update(department);
                await _unitOfWork.Complete();
            }
        }

        public async Task DeleteDepartmentAsync(Department department)
        {
            await _unitOfWork.repository<Department>().Delete(department);
            await _unitOfWork.Complete();
        }
    }
}
