using System.IdentityModel.Tokens.Jwt;//لإنشاء وتوليد توكن JWT
using System.Security.Claims;//لإضافة Claims (مثل اسم المستخدم والدور) داخل التوكن
using System.Security.Cryptography;//لتشفير/تحقق من كلمة المرور
using System.Text;//لتحويل النصوص إلى بايتات عند التشفير
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;//للوصول إلى الإعدادات من appsettings.json
using Microsoft.IdentityModel.Tokens;//لبناء التوكن وتوقيعه
using Application.DTOs;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository userRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _config = config;
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);

        if (user == null || !VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            return null;

        return GenerateJwtToken(user);
    }

    private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
    {
        using var hmac = new HMACSHA512(storedSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(storedHash);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        foreach (var role in user.UserRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
        }


        //var roleName = user.UserRoles.FirstOrDefault()?.Role?.Name;

        //if (!string.IsNullOrEmpty(roleName))
        //{
        //    claims.Add(new Claim(ClaimTypes.Role, roleName));
        //}

        var jwtKey = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT key not found in configuration.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(3),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
