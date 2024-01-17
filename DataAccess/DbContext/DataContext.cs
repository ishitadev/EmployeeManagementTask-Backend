using Microsoft.EntityFrameworkCore;

namespace Employee.Data.Contaxt
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Employees> Employee { get; set; }
        public DbSet<Department> Department { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
