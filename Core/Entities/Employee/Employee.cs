namespace Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Navigation property for many-to-many
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
