namespace Employee_Skills.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
