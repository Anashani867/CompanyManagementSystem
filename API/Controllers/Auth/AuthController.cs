using Microsoft.AspNetCore.Mvc;
using Core.Interfaces; // إذا IAuthRepository في Core
using Application.DTOs; // مكان LoginDto

    
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // ✅ تأكد أنك تمرر كل المطلوب
            var token = await _authRepository.LoginAsync(dto.Username, dto.Password);

            if (token == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { token });
        }
    }
}
