using Employee.API.Controllers;
using Employee.Data;
using Employee.Data.Contaxt;
using Employee.Data.Core;
using Employee.Data.Core.DTO;
using Employee.Data.Repository;
using EmployeeAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Tests
{
    [TestFixture]
    public class EmployeeControllerTests
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

            _context.Employee.AddRange(new List<Employees>
            {
                new Employees { Name = "John Doe", Email = "john@example.com", DOB = DateTime.Parse("1990-01-01"), CreatedOn = DateTime.Now.AddDays(-5) },
                new Employees { Name = "Jane Smith", Email = "jane@example.com", DOB = DateTime.Parse("1985-02-15"), CreatedOn = DateTime.Now.AddDays(-10) }
            });
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAll()
        {
            // Arrange
            var employeeService = new EmployeeService(new UnitOfWork(_context));
            var controller = new EmployeeController(employeeService);

            // Act
            var result = await controller.GetAll();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Details()
        {
            // Arrange
            var controller = new EmployeeController(new EmployeeService(new UnitOfWork(_context)));

            // Act
            var result = await controller.Details(1); // Assuming you want to get details for Employee with Id = 1
            EmployeeRequestDTO? employee = ((ResponseDTO<EmployeeRequestDTO>)((ObjectResult)result.Result).Value).Result;
            Assert.IsNotNull(employee);
            Assert.AreEqual(1, employee.Id);
        }

        [Test]
        public async Task Create()
        {
            // Arrange
            var controller = new EmployeeController(new EmployeeService(new UnitOfWork(_context)));
            var newEmployee = new EmployeeCreateDTO { Name = "Mark Zuck", Email = "mark@example.com", DOB = DateTime.Parse("1990-01-01")};

            // Act
            var result = await controller.Create(newEmployee);
            var isInserted = ((ResponseDTO<bool>)((ObjectResult)result.Result).Value).Result;
            Assert.IsNotNull(result);
            Assert.IsTrue(isInserted);
        }

        [Test]
        public async Task Edit()
        {
            // Arrange
            var controller = new EmployeeController(new EmployeeService(new UnitOfWork(_context)));
            var newEmployee = new EmployeeDTO { Name = "Mark Zuck", Email = "mark@example.com", DOB = DateTime.Parse("1990-01-01"), CreatedOn = DateTime.Now.AddDays(-5) };

            // Act
            var result = await controller.Edit(1,newEmployee);
            var isUpdated = ((ResponseDTO<bool>)((ObjectResult)result.Result).Value).Result;
            Assert.IsNotNull(result);
            Assert.IsTrue(isUpdated);
        }

        [Test]
        public async Task Delete()
        {
            // Arrange
            var controller = new EmployeeController(new EmployeeService(new UnitOfWork(_context)));
            
            // Act
            var result = await controller.Delete(1);
            var isDeleted = ((ResponseDTO<bool>)((ObjectResult)result.Result).Value).Result;
            Assert.IsNotNull(result);
            Assert.IsTrue(isDeleted);
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
