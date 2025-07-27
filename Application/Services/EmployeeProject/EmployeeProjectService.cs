using Application.DTOs;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public class EmployeeProjectService
    {
        private readonly IEmployeeProjectRepository _repository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectRepository _projectRepository;

        public EmployeeProjectService(
            IEmployeeProjectRepository repository,
            IEmployeeRepository employeeRepository,
            IProjectRepository projectRepository)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
        }

        public async Task<bool> AssignToProjectAsync(EmployeeProjectDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);
            var project = await _projectRepository.GetByIdAsync(dto.ProjectId);

            if (employee == null || project == null || employee.IsDeleted || project.IsDeleted)
                return false;

            // 🔍 تحقق قبل الإضافة
            var exists = await _repository.ExistsAsync(dto.EmployeeId, dto.ProjectId);
            if (exists)
                return false; // أو حدّث الساعات لو حاب

            var assignment = new EmployeeProject
            {
                EmployeeId = dto.EmployeeId,
                ProjectId = dto.ProjectId,
                AssignedHours = dto.AssignedHours
            };

            await _repository.AddAsync(assignment);
            await _repository.SaveChangesAsync();

            return true;
        }


        //public async Task<bool> AssignToProjectAsync(EmployeeProjectDto dto)
        //{
        //    var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);
        //    var project = await _projectRepository.GetByIdAsync(dto.ProjectId);

        //    if (employee == null || project == null || employee.IsDeleted || project.IsDeleted)
        //        return false;

        //    var assignment = new EmployeeProject
        //    {
        //        EmployeeId = dto.EmployeeId,
        //        ProjectId = dto.ProjectId,
        //        AssignedHours = dto.AssignedHours
        //    };

        //    await _repository.AddAsync(assignment);
        //    await _repository.SaveChangesAsync();

        //    return true;
        //}


        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByProjectIdAsync(int projectId)
        {
            var employees = await _repository.GetEmployeesByProjectIdAsync(projectId);
            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name
            });
        }


        public async Task<IEnumerable<ProjectDto>> GetProjectsByEmployeeIdAsync(int employeeId)
        {
            var assignments = await _repository.GetProjectsByEmployeeIdAsync(employeeId);

            return assignments.Select(ep => new ProjectDto
            {
                Id = ep.ProjectId,
                Title = ep.Project.Title
            });
        }


        public async Task<PagedResultDto<ProjectWithEmployeesDto>> GetAllProjectsWithEmployeesAsync(PagedRequestDto request)
        {
            var assignments = await _repository.GetAllWithEmployeesAndProjectsAsync();

            var grouped = assignments
                .GroupBy(ep => ep.Project)
                .Select(g => new ProjectWithEmployeesDto
                {
                    ProjectId = g.Key.Id,
                    Title = g.Key.Title,
                    Employees = g.Select(ep => new EmployeeAssignmentDto
                    {
                        Id = ep.Employee.Id,
                        Name = ep.Employee.Name,
                        AssignedHours = ep.AssignedHours
                    }).ToList()
                });

            var totalCount = grouped.Count();
            var pagedData = grouped
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            return new PagedResultDto<ProjectWithEmployeesDto>
            {
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                Data = pagedData
            };
        }
        

        //public async Task<IEnumerable<EmployeeWithProjectsDto>> GetAllEmployeesWithProjectsAsync()
        //{
        //    var assignments = await _repository.GetAllWithEmployeesAndProjectsAsync();

        //    var grouped = assignments
        //        .GroupBy(ep => ep.Employee)
        //        .Select(g => new EmployeeWithProjectsDto
        //        {
        //            EmployeeId = g.Key.Id,
        //            Name = g.Key.Name,
        //            Projects = g.Select(ep => new ProjectAssignmentDto
        //            {
        //                Id = ep.Project.Id,
        //                Title = ep.Project.Title,
        //                AssignedHours = ep.AssignedHours
        //            }).ToList()
        //        });

        //    return grouped;
        //}


        public async Task<PagedResultDto<EmployeeWithProjectsDto>> GetAllEmployeesWithProjectsAsync(PagedRequestDto request)
        {
            var assignments = await _repository.GetAllWithEmployeesAndProjectsAsync();

            var grouped = assignments
                .GroupBy(ep => ep.Employee)
                .Select(g => new EmployeeWithProjectsDto
                {
                    EmployeeId = g.Key.Id,
                    Name = g.Key.Name,
                    Projects = g.Select(ep => new ProjectAssignmentDto
                    {
                        Id = ep.Project.Id,
                        Title = ep.Project.Title,
                        AssignedHours = ep.AssignedHours
                    }).ToList()
                });

            var totalCount = grouped.Count();
            var pagedData = grouped
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            return new PagedResultDto<EmployeeWithProjectsDto>
            {
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                Data = pagedData
            };
        }


        public async Task<IEnumerable<ProjectTotalHoursDto>> GetTotalHoursPerProjectAsync()
        {
            var data = await _repository.GetAllWithProjectsAsync();

            return data
                .GroupBy(ep => ep.Project)
                .Select(g => new ProjectTotalHoursDto
                {
                    ProjectId = g.Key.Id,
                    Title = g.Key.Title,
                    TotalHours = g.Sum(ep => ep.AssignedHours)
                });
        }

        public async Task<IEnumerable<EmployeeTotalHoursDto>> GetTotalHoursPerEmployeeAsync()
        {
            var data = await _repository.GetAllWithEmployeesAsync();

            return data
                .GroupBy(ep => ep.Employee)
                .Select(g => new EmployeeTotalHoursDto
                {
                    EmployeeId = g.Key.Id,
                    Name = g.Key.Name,
                    TotalHours = g.Sum(ep => ep.AssignedHours)
                });
        }


    }
}
