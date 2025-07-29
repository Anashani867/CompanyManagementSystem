namespace Core.Entities
{
    public class EmployeeProject
    {
        //public int Id { get; set; }
        public Employee Employee { get; set; } = null!;
        public int EmployeeId { get; set; }
        public Project Project { get; set; } = null!;
        public int ProjectId { get; set; }

        public int AssignedHours { get; set; }
    }
