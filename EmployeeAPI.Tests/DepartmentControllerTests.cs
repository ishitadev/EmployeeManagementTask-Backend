using Employee.API.Controllers;
using Employee.Data;
using Employee.Data.Contaxt;
using Employee.Data.Core;
using Employee.Data.Core.DTO;
using Employee.Data.Repository;
using EmployeeAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee.Tests
{
    [TestFixture]
    public class DepartmentControllerTests
    {
        private DataContext _context;

        [SetUp]
        public void Setup()
        {
            // Initialize the in-memory database
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "EmployeeDb")
                .Options;

            _context = new DataContext(options);
            
            _context.Department.AddRange(new List<Department>
            {
                new Department { Id = 3, Name = "Department 3" },
                new Department { Id = 4, Name = "Department 4" }
            });

            _context.SaveChanges();
        }

        [Test]
        public async Task GetAll()
        {
            // Arrange
            var departmentService = new DepartmentService(new UnitOfWork(_context));
            var controller = new DepartmentController(departmentService);
            
            // Act
            var result = await controller.GetAll();
            Assert.IsNotNull(result);

            var department = ((ResponseDTO<IEnumerable<DepartmentDTO>>)((ObjectResult)result.Result).Value).Result;
            Assert.IsNotNull(department);
            Assert.IsTrue(department.Count() == 2);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up the in-memory database after each test
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
