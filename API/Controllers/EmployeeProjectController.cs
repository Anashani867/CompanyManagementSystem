using Application.Services;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeProjectController : ControllerBase
    {
        private readonly EmployeeProjectService _service;

        public EmployeeProjectController(EmployeeProjectService service)
        {
            _service = service;
        }



        [HttpGet("project/{projectId}/employees")]
        public async Task<IActionResult> GetEmployeesByProjectId(int projectId)
        {
            var employees = await _service.GetEmployeesByProjectIdAsync(projectId);
            return Ok(employees);
        }


        [HttpGet("employee/{employeeid}/projects")]
        public async Task<IActionResult> GetProjectsByEmployeeId(int id)
        {
            var projects = await _service.GetProjectsByEmployeeIdAsync(id);
            return Ok(projects);
        }


        [HttpGet("all-projects-with-employees")]
        public async Task<IActionResult> GetAllProjectsWithEmployees()
        {
            var result = await _service.GetAllProjectsWithEmployeesAsync();
            return Ok(result);
        }

        [HttpGet("all-employees-with-projects")]
        public async Task<IActionResult> GetAllEmployeesWithProjects()
        {
            var result = await _service.GetAllEmployeesWithProjectsAsync();
            return Ok(result);
        }

        [HttpGet("project-total-hours")]
        public async Task<IActionResult> GetTotalHoursPerProject()
        {
            var result = await _service.GetTotalHoursPerProjectAsync();
            return Ok(result);
        }

        [HttpGet("employee-total-hours")]
        public async Task<IActionResult> GetTotalHoursPerEmployee()
        {
            var result = await _service.GetTotalHoursPerEmployeeAsync();
            return Ok(result);
        }




        [HttpPost("assign")]
        public async Task<IActionResult> AssignToProject([FromBody] EmployeeProjectDto dto)
        {
            var result = await _service.AssignToProjectAsync(dto);

            if (!result)
                return BadRequest("Assignment failed. Please verify the employee and project.");

            return Ok("Assignment completed successfully.");
        }
    }
}
