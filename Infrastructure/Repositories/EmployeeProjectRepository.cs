using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployeeProjectRepository : IEmployeeProjectRepository
    {
        private readonly AppDbContext _context;

        public EmployeeProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EmployeeProject entity)
        {
            await _context.EmployeeProjects.AddAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Employee>> GetEmployeesByProjectIdAsync(int projectId)
        {
            return await _context.EmployeeProjects
                .Where(ep => ep.ProjectId == projectId) // فلترة على المشروع المحدد
                .Include(ep => ep.Employee) // تحميل بيانات الموظف المرتبط
                .Select(ep => ep.Employee) // أخذ الموظف فقط (وليس العلاقة)
                .Where(e => !e.IsDeleted) // حذف المحذوفين (Soft Delete)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeProject>> GetProjectsByEmployeeIdAsync(int employeeId)
        {
            return await _context.EmployeeProjects
                .Include(ep => ep.Project)
                .Where(ep => ep.EmployeeId == employeeId && !ep.Project.IsDeleted)
                .ToListAsync();
        }


        public async Task<IEnumerable<EmployeeProject>> GetAllWithEmployeesAndProjectsAsync()
        {
            return await _context.EmployeeProjects
                .Include(ep => ep.Employee)
                .Include(ep => ep.Project)
                .Where(ep => !ep.Employee.IsDeleted && !ep.Project.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeProject>> GetAllWithProjectsAsync()
        {
            return await _context.EmployeeProjects
                .Include(ep => ep.Project)
                .Where(ep => !ep.Project.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeProject>> GetAllWithEmployeesAsync()
        {
            return await _context.EmployeeProjects
                .Include(ep => ep.Employee)
                .Where(ep => !ep.Employee.IsDeleted)
                .ToListAsync();
        }


    }
}
