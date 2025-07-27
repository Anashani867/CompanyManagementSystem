using Application.Services;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EmployeeController : ControllerBase
	{
		private readonly EmployeeService _service;

		public EmployeeController(EmployeeService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var employees = await _service.GetAllAsync();
			return Ok(employees);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var employee = await _service.GetByIdAsync(id);
			if (employee == null)
				return NotFound();

			return Ok(employee);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateEmployeeDto dto)
		{
			var createdEmployee = await _service.AddAsync(dto);
			return Ok(createdEmployee);
		}

		[HttpPut]
		public async Task<IActionResult> Update(UpdateEmployeeDto dto)
		{
			var UpdateEmployee = await _service.UpdateAsync(dto);

			if (UpdateEmployee == null)
				return NotFound("Employee IS NOT FOUND");

			return Ok(UpdateEmployee);
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
