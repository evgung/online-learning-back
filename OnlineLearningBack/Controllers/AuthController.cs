using Microsoft.AspNetCore.Mvc;
using OnlineLearningBack.Models;
using OnlineLearningBack.Models.Dto;
using OnlineLearningBack.Services;
using System;

namespace OnlineLearningBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly AuthService _authService;

        public AuthController(AppDbContext db, AuthService authService)
        {
            _db = db;
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized();

            var token = _authService.GenerateJwtToken(user);
            Response.Cookies.Append("jwt", token);
            Response.Cookies.Append("isAdmin", user.IsAdmin.ToString());
            return Ok(new { token });
        }
    }
}
