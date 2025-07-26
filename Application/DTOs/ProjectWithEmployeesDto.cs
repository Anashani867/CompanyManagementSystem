namespace Application.DTOs
{
    public class ProjectWithEmployeesDto
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public List<EmployeeAssignmentDto> Employees { get; set; }
    }

    public class EmployeeAssignmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AssignedHours { get; set; }
    }
}