using System.ComponentModel.DataAnnotations;

namespace Employee.Data
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Employees>? Employees { get; set; }
    }
}
