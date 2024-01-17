using System;

namespace Employee.Data.Core.DTO
{
    public class EmployeeDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Department? Department { get; set; }
    }
}
