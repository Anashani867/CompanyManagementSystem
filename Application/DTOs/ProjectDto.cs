namespace Application.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class CreateProjectDto

    {
        public string Title { get; set; }
    }

    public class UpdateProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
