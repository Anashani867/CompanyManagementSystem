using Application.DTOs;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeDto?>> GetAllAsync()
        {
            var employees = await _repository.GetAllAsync();
            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name
            });
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null) return null;

            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name
            };
        }

        public async Task<EmployeeDto?> AddAsync(CreateEmployeeDto dto)
        {
            var employee = new Employee { Name = dto.Name };
            await _repository.AddAsync(employee);

            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name
            };
        }

        public async Task<EmployeeDto?> UpdateAsync(UpdateEmployeeDto dto)
        {
            var employee = await _repository.GetByIdAsync(dto.Id);
            if (employee == null) return null;

            employee.Name = dto.Name;
            await _repository.UpdateAsync(employee);



            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null || employee.IsDeleted)
                return false;

            employee.IsDeleted = true;
            await _repository.UpdateAsync(employee);
            return true;
        }
    }
}
