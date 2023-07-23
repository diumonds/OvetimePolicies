using MainProject.Controllers;
using MainProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestProject
{
    public class Tests
    {
        private SalaryController _controller;
        private ApplicationDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OvertimePoliciesDb;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;
            _dbContext = new ApplicationDbContext(options);
            _controller = new SalaryController(_dbContext);
        }

        [Test]
        public async Task AddSalary_ShouldAddEmployeeToDatabase()
        {
            // Arrange
            AddRequestModel model = new AddRequestModel
            {
                data = "alireza,mahmoudi, 5000, 200, 100",
                OverTimeCalculator = "CalcurlatorA"
            };
            IActionResult result = await _controller.AddSalary(model);
            Employee employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.FirstName == "Alireza");
            Assert.IsNotNull(employee);
            Assert.IsInstanceOf<Employee>(result);
            Assert.AreEqual("alireza", employee.FirstName);
            Assert.AreEqual(5300, employee.Amount);
        }
    }
}