using Application.DTOs;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _repository;

        public ProjectService(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProjectDto?>> GetAllAsync()
        {
            var projects = await _repository.GetAllAsync();
            return projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                Title = p.Title
            });
        }

        public async Task<ProjectDto?> GetByIdAsync(int id)
        {
            var project = await _repository.GetByIdAsync(id);
            if (project == null) return null;

            return new ProjectDto
            {
                Id = project.Id,
                Title = project.Title
            };
        }

        public async Task<ProjectDto?> AddAsync(CreateProjectDto dto)
        {
            var project = new Project { Title = dto.Title };
            await _repository.AddAsync(project);

            return new ProjectDto
            {
                Id = project.Id,
                Title = project.Title
            };
        }

        public async Task<ProjectDto?> UpdateAsync(UpdateProjectDto dto)
        {
            var project = await _repository.GetByIdAsync(dto.Id);
            if (project == null) return null;

            project.Title = dto.Title;
            await _repository.UpdateAsync(project);



            return new ProjectDto
            {
                Id = project.Id,
                Title = project.Title
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _repository.GetByIdAsync(id);
            if (project == null || project.IsDeleted)
                return false;

            project.IsDeleted = true;
            await _repository.UpdateAsync(project);
            return true;
        }
    }
}
