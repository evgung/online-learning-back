namespace OnlineLearningBack.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }
        public List<Course> CreatedCourses { get; set; } = new();
    }
}