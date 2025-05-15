using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningBack.Models;
using OnlineLearningBack.Models.Dto;
using OnlineLearningBack.Services;
using System;
using System.Security.Claims;

namespace OnlineLearningBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CoursesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetCourses(
            [FromQuery] string? category,
            [FromQuery] int? readingTime,
            [FromQuery] bool? hasTest)
        {
            var query = _db.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(category))
                query = query.Where(c => c.Category == category);

            if (readingTime.HasValue)
                query = query.Where(c => c.ReadingTime <= readingTime);

            if (hasTest.HasValue)
                query = query.Where(c => c.HasTest == hasTest);

            var result = query
                .Include(c => c.TextBlocks)
                .Include(c => c.TestQuestions)
                .Select(GetCourseDto)
                .ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCourseById(int id)
        {
            var query = _db.Courses
                .AsQueryable()
                .Include(c => c.TextBlocks)
                .Include(c => c.TestQuestions);

            var course = query.FirstOrDefault(course => course.Id == id);

            return Ok(GetCourseDto(course));
        }

        [HttpPost]
        public IActionResult CreateCourse(
            [FromBody] CourseCreateDto courseDto)
        {
            var course = new Course
            {
                Title = courseDto.Title,
                Category = courseDto.Category,
                ReadingTime = courseDto.ReadingTime,
                HasTest = courseDto.HasTest,
                TextBlocks = courseDto.TextBlocks.Select(tb => new TextBlock
                {
                    Title = tb.Title,
                    Content = tb.Content
                }).ToList(),
                TestQuestions = courseDto.TestQuestions.Select(tq => new TestQuestion
                {
                    Question = tq.Question,
                    Answers = tq.Answers,
                    CorrectAnswerIndex = tq.CorrectAnswerIndex
                }).ToList(),
                AuthorId = courseDto.AuthorId,
            };

            _db.Courses.Add(course);
            _db.SaveChanges();

            return Ok();
        }


        private CourseDto GetCourseDto(Course course)
        {
            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Category = course.Category,
                ReadingTime = course.ReadingTime,
                HasTest = course.HasTest,
                TextBlocks = course.TextBlocks.Select(tb => new TextBlockDto
                {
                    Title = tb.Title,
                    Content = tb.Content
                }).ToList(),
                TestQuestions = course.TestQuestions.Select(tq => new TestQuestionDto
                {
                    Question = tq.Question,
                    Answers = tq.Answers,
                    CorrectAnswerIndex = tq.CorrectAnswerIndex
                }).ToList()
            };
        }
    }
}
