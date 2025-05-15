namespace OnlineLearningBack.Models
{
    public class TestQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<string> Answers { get; set; } = new();
        public int CorrectAnswerIndex { get; set; }

        public Course Course { get; set; }
        public int CourseId { get; set; }
    }
}