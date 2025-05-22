namespace OnlineLearningBack.Models.Dto
{
    public class CourseCreateDto
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public int ReadingTime { get; set; }
        public bool HasTest { get; set; }
        public List<TextBlockDto> TextBlocks { get; set; } = new();
        public List<TestQuestionDto> TestQuestions { get; set; } = new();
        public string AuthorToken { get; set; }
    }
}
