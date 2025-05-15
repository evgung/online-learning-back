using System.Text.Json.Serialization;

namespace OnlineLearningBack.Models
{
    public class TextBlock
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        
        public Course Course { get; set; }
        public int CourseId { get; set; }
    }
}