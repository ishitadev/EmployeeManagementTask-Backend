namespace Employee.Data.Core.DTO
{
    public class EmployeeCreateDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public int? DepartmentId { get; set; }
    }
}
