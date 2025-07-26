using Core.Entities;

namespace Core.Interfaces
{
    public interface IEmployeeProjectRepository
    {
        Task AddAsync(EmployeeProject entity);
        Task SaveChangesAsync();
        Task<IEnumerable<Employee>> GetEmployeesByProjectIdAsync(int projectId);
        Task<IEnumerable<EmployeeProject>> GetProjectsByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<EmployeeProject>> GetAllWithEmployeesAndProjectsAsync();
        Task<IEnumerable<EmployeeProject>> GetAllWithProjectsAsync();
        Task<IEnumerable<EmployeeProject>> GetAllWithEmployeesAsync();





    }
}
