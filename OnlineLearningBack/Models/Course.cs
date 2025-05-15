namespace OnlineLearningBack.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public int ReadingTime { get; set; }
        public bool HasTest { get; set; }
        public List<TextBlock> TextBlocks { get; set; } = new();
        public List<TestQuestion> TestQuestions { get; set; } = new();

        public string AuthorId { get; set; }
        public User Author { get; set; }
    }
}
