namespace OnlineLearningBack.Models.Dto
{
    public class TestQuestionDto
    {
        public string Question { get; set; }
        public List<string> Answers { get; set; } = new();
        public int CorrectAnswerIndex { get; set; }
    }
}
