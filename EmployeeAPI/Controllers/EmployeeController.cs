using Employee.Data;
using Employee.Data.Core.DTO;
using Employee.Data.Interface;
using EmployeeAPI.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ResponseDTO<IEnumerable<EmployeeRequestDTO>>>> GetAll()
        {
            try
            {
                var employeeList = await _employeeService.GetAllEmployeeAsync();
                var result = employeeList.Select(e => new EmployeeRequestDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    DOB = e.DOB,
                    DepartmentId = e.DepartmentId,
                    CreatedOn = e.CreatedOn,
                    UpdatedOn = e.UpdatedOn,
                    Department = e.Department
                });


                return Ok(new ResponseDTO<IEnumerable<EmployeeRequestDTO>>
                {
                    StatusCode = 200,
                    Message = "Employee records retrieved.",
                    Result = result
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResponseDTO<IEnumerable<EmployeeRequestDTO>>
                {
                    StatusCode = 500,
                    Message = $"Error: {e.Message}",
                    Result = new List<EmployeeRequestDTO>()
                });
            }
        }


        [HttpGet]
        [Route("Details/{id}")]
        public async Task<ActionResult<ResponseDTO<EmployeeRequestDTO>>> Details(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);

                if (employee == null)
                {
                    return NotFound(new ResponseDTO<EmployeeRequestDTO>
                    {
                        StatusCode = 404,
                        Message = "Employee not found.",
                        Result = null
                    });
                }

                var result = new EmployeeRequestDTO
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    DOB = employee.DOB,
                    DepartmentId = employee.DepartmentId,
                    CreatedOn = employee.CreatedOn,
                    UpdatedOn = employee.UpdatedOn,
                    Department = employee.Department
                };

                return Ok(new ResponseDTO<EmployeeRequestDTO>
                {
                    StatusCode = 200,
                    Message = "Employee retrieved.",
                    Result = result
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResponseDTO<EmployeeRequestDTO>
                {
                    StatusCode = 500,
                    Message = $"Error: {e.Message}",
                    Result = null
                });
            }
        }


        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<ResponseDTO<bool>>> Create(EmployeeCreateDTO reqModel)
        {
            try
            {
                var employee = new Employees
                {
                    Name = reqModel.Name,
                    Email = reqModel.Email,
                    DOB = reqModel.DOB,
                    DepartmentId = reqModel.DepartmentId,
                };

                await _employeeService.AddEmployeeAsync(employee);

                return Ok(new ResponseDTO<bool>
                {
                    StatusCode = 200,
                    Message = "Employee Added.",
                    Result = true
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResponseDTO<bool>
                {
                    StatusCode = 500,
                    Message = $"Error: {e.Message}",
                    Result = false
                });
            }
        }



        [HttpPatch]
        [Route("Update/{id}")]
        public async Task<ActionResult<ResponseDTO<bool>>> Edit(int id, EmployeeDTO reqModal)
        {
            try
            {
                await _employeeService.UpdateEmployeeAsync(id, reqModal);

                return Ok(new ResponseDTO<bool>
                {
                    StatusCode = 200,
                    Message = "Employee Edited.",
                    Result = true
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResponseDTO<bool>
                {
                    StatusCode = 500,
                    Message = $"Error: {e.Message}",
                    Result = false
                });
            }
        }




        [HttpDelete]
        public async Task<ActionResult<ResponseDTO<bool>>> Delete(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return BadRequest(new ResponseDTO<bool>
                    {
                        StatusCode = 400,
                        Message = "Employee Not Found.",
                        Result = false
                    });
                }

                await _employeeService.DeleteEmployeeAsync(employee);

                return Ok(new ResponseDTO<bool>
                {
                    StatusCode = 200,
                    Message = "Employee Deleted.",
                    Result = true
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResponseDTO<bool>
                {
                    StatusCode = 500,
                    Message = $"Error: {e.Message}",
                    Result = false
                });
            }
        }



        [HttpGet("search")]
        public async Task<ActionResult<ResponseDTO<IEnumerable<EmployeeRequestDTO>>>> Search(
            [FromQuery] string name = null,
            [FromQuery] string email = null)
        {
            try
            {
                var employeeList = await _employeeService.GetAllEmployeeAsync();

                var result = employeeList
                    .Where(e =>
                        (string.IsNullOrEmpty(name) || e?.Name?.Contains(name, StringComparison.OrdinalIgnoreCase) == true) ||
                        (string.IsNullOrEmpty(email) || e?.Email?.Contains(email, StringComparison.OrdinalIgnoreCase) == true)
                        )
                    .Select(e => new EmployeeRequestDTO
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Email = e.Email,
                        DOB = e.DOB,
                        DepartmentId = e.DepartmentId,
                        CreatedOn = e.CreatedOn,
                        UpdatedOn = e.UpdatedOn,
                        Department = e.Department
                    }).ToList();

                var response = new ResponseDTO<IEnumerable<EmployeeRequestDTO>>
                {
                    StatusCode = 200,
                    Message = "Employee records retrieved.",
                    Result = result
                };

                Console.WriteLine($"Total employees: {response.Result.Count()}");

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResponseDTO<IEnumerable<EmployeeRequestDTO>>
                {
                    StatusCode = 500,
                    Message = $"Error: {e.Message}",
                    Result = new List<EmployeeRequestDTO>()
                });
            }
        }




    }
}
