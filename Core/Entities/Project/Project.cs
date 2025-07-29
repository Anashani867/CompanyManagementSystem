namespace Core.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        // Navigation property for many-to-many
        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    }
}
