
namespace Application.DTOs
{
    public class ProjectTotalHoursDto
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public int TotalHours { get; set; }
    }

    public class EmployeeTotalHoursDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int TotalHours { get; set; }
    }
}