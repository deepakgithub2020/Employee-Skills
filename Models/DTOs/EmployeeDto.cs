using System.ComponentModel.DataAnnotations;

namespace Employee_Skills.Models.DTOs
{
    public class EmployeeDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public List<int> SkillIds { get; set; } 
    }
}
