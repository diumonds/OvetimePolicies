using MainProject.CommonHelper;
using MainProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MainProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public SalaryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("AddSalary")]
        public async Task<IActionResult> AddSalary([FromBody] AddRequestModel model)
        {
            Employee employee = await ProcessInput.GetEmployee(model.data);
            if (employee != null)
            {
                switch (model.OverTimeCalculator)
                {
                    case "CalcurlatorA":
                        employee.Amount = employee.BasicSalery + employee.Allowance + employee.Transportation + OvertimePolicies.OvertimePolicies.CalcurlatorA(employee.BasicSalery, employee.Allowance);
                        break;
                    case "CalcurlatorB":
                        employee.Amount = employee.BasicSalery + employee.Allowance + employee.Transportation + OvertimePolicies.OvertimePolicies.CalcurlatorB(employee.BasicSalery, employee.Allowance);
                        break;
                    case "CalcurlatorC":
                        employee.Amount = employee.BasicSalery + employee.Allowance + employee.Transportation + OvertimePolicies.OvertimePolicies.CalcurlatorC(employee.BasicSalery, employee.Allowance);
                        break;
                    default:
                        break;
                }
            }
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AddRequestModel model)
        {
            Employee employee = await ProcessInput.GetEmployee(model.data);
            _dbContext.Entry(employee).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var salary = await _dbContext.Employees.FindAsync(id);
            if (salary == null)
                return NotFound();

            _dbContext.Employees.Remove(salary);
            await _dbContext.SaveChangesAsync();
            return Ok(salary);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var salary = await _dbContext.Employees.FindAsync(id);
            if (salary == null)
                return NotFound();

            return Ok(salary);
        }

        [HttpGet]
        public async Task<IActionResult> GetRange()
        {
            var salaries = await _dbContext.Employees.ToListAsync();

            return Ok(salaries);
        }
    }
}
