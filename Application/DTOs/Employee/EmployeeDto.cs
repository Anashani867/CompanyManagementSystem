namespace Application.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class CreateEmployeeDto

    {
        public string? Name { get; set; }
    }

    public class UpdateEmployeeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
