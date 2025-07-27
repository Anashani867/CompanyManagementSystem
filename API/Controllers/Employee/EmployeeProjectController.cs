using Application.Services;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


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
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetProjectsByEmployeeId(int employeeid)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return BadRequest("Invalid user ID in token.");

            var projects = await _service.GetProjectsByEmployeeIdAsync(userId);
            return Ok(projects);
        }


        [HttpPost("all-projects-with-employees")]
        public async Task<IActionResult> GetAllProjectsWithEmployees([FromBody] PagedRequestDto request)
        {
            var result = await _service.GetAllProjectsWithEmployeesAsync(request);
            return Ok(result);
        }

        [HttpPost("all-employees-with-projects")]
        public async Task<IActionResult> GetAllEmployeesWithProjectsPaged([FromBody] PagedRequestDto request)
        {
            var result = await _service.GetAllEmployeesWithProjectsAsync(request);
            return Ok(result);
        }

        [HttpGet("project-total-hours")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTotalHoursPerProject()
        {
            var result = await _service.GetTotalHoursPerProjectAsync();
            return Ok(result);
        }

        [HttpGet("employee-total-hours")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTotalHoursPerEmployee()
        {
            var result = await _service.GetTotalHoursPerEmployeeAsync();
            return Ok(result);
        }




        [HttpPost("assign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignToProject([FromBody] EmployeeProjectDto dto)
        {
            var result = await _service.AssignToProjectAsync(dto);

            if (!result)
                return BadRequest("Assignment failed. Please verify the employee and project.");

            return Ok("Assignment completed successfully.");
        }
    }
}
