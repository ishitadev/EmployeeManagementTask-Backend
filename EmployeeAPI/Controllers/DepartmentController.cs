using Employee.Data;
using Employee.Data.Core.DTO;
using Employee.Data.Core.Interface;
using EmployeeAPI.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }



        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ResponseDTO<IEnumerable<DepartmentDTO>>>> GetAll()
        {
            try
            {
                var departmentList = await _departmentService.GetAllDepartmentAsync();
                var result = departmentList.Select(d => new DepartmentDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                });

                return Ok(new ResponseDTO<IEnumerable<DepartmentDTO>>
                {
                    StatusCode = 200,
                    Message = "Department records retrieved.",
                    Result = result
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResponseDTO<IEnumerable<DepartmentDTO>>
                {
                    StatusCode = 500,
                    Message = $"Error: {e.Message}",
                    Result = new List<DepartmentDTO>()
                });
            }
        }
    }
}
