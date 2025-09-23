using Employee_Skills.Data;
using Employee_Skills.Models.DTOs;
using Employee_Skills.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Skills.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : Controller
    {
        private readonly AppDbContext _context;

        public SkillsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillDto>>> GetSkills()
        {
            return await _context.Skills.Select(s => new SkillDto { Id = s.Id, Name = s.Name }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkillDto>> GetSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) return NotFound();
            return new SkillDto { Id = skill.Id, Name = skill.Name };
        }

        [HttpPost]
        public async Task<ActionResult<SkillDto>> PostSkill(SkillDto skillDto)
        {
            var skill = new Skill { Name = skillDto.Name };
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
            skillDto.Id = skill.Id;
            return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skillDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill(int id, SkillDto skillDto)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) return NotFound();
            skill.Name = skillDto.Name;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) return NotFound();
            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
