public class EmployeeWithProjectsDto
{
	public int EmployeeId { get; set; }
	public string Name { get; set; }
	public List<ProjectAssignmentDto> Projects { get; set; }
}

public class ProjectAssignmentDto
{
	public int Id { get; set; }
	public string Title { get; set; }
	public int AssignedHours { get; set; }
}
