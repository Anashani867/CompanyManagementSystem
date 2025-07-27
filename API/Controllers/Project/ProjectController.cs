using Application.Services;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProjectController : ControllerBase
	{
		private readonly ProjectService _service;

		public ProjectController(ProjectService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var projects = await _service.GetAllAsync();
			return Ok(projects);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var project = await _service.GetByIdAsync(id);
			if (project == null)
				return NotFound();

			return Ok(project);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateProjectDto dto)
		{
			var createdproject = await _service.AddAsync(dto);
			return Ok(createdproject);
		}

		[HttpPut]
		public async Task<IActionResult> Update(UpdateProjectDto dto)
		{
			var UpdateProject = await _service.UpdateAsync(dto);

			if (UpdateProject == null)
				return NotFound("Employee IS NOT FOUND");

			return Ok(UpdateProject);
		}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
		{
			var deleted = await _service.DeleteAsync(id);
			if (!deleted)
				return NotFound("الموظف غير موجود أو تم حذفه مسبقًا");

			return Ok("تم الحذف (Soft Delete) بنجاح");
		}
	}
}
