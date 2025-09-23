using Employee_Skills.Data;
using Employee_Skills.Models.DTOs;
using Employee_Skills.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Skills.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            return await _context.Employees
                .Include(e => e.EmployeeSkills)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    SkillIds = e.EmployeeSkills.Select(es => es.SkillId).ToList()
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.EmployeeSkills)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return NotFound();
            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                SkillIds = employee.EmployeeSkills.Select(es => es.SkillId).ToList()
            };
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeDto employeeDto)
        {
            var employee = new Employee { Name = employeeDto.Name, Email = employeeDto.Email };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(); 

            foreach (var skillId in employeeDto.SkillIds ?? new List<int>())
            {
                _context.EmployeeSkills.Add(new EmployeeSkill { EmployeeId = employee.Id, SkillId = skillId });
            }
            await _context.SaveChangesAsync();

            employeeDto.Id = employee.Id;
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employeeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDto employeeDto)
        {
            var employee = await _context.Employees
                .Include(e => e.EmployeeSkills)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return NotFound();

            employee.Name = employeeDto.Name;
            employee.Email = employeeDto.Email;

            // Update skills: Remove old, add new
            _context.EmployeeSkills.RemoveRange(employee.EmployeeSkills);
            foreach (var skillId in employeeDto.SkillIds ?? new List<int>())
            {
                _context.EmployeeSkills.Add(new EmployeeSkill { EmployeeId = id, SkillId = skillId });
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
