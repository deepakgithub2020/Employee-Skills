using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Employee_Skills.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]        
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
