namespace Employee_Skills.Models.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<int> SkillIds { get; set; } 
    }
}
