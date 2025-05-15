using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningBack.Models;
using OnlineLearningBack.Models.Dto;

namespace OnlineLearningBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AdminController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("courses")]
        public IActionResult GetAllCourses()
        {
            var courses = _db.Courses
                .Include(c => c.TextBlocks)
                .Include(c => c.TestQuestions)
                .Include(c => c.Author)
                .Select(GetCourseDto);

            return Ok(courses);
        }

        private CourseAdminDto GetCourseDto(Course course)
        {
            return new CourseAdminDto
            {
                Id = course.Id,
                Title = course.Title,
                Category = course.Category,
                ReadingTime = course.ReadingTime,
                HasTest = course.HasTest,
                Author = new UserDto
                {
                    Id = course.Author.Id,
                    Username = course.Author.Username,
                    IsAdmin = course.Author.IsAdmin
                },
            };
        }
    }
}
