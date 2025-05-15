using System.ComponentModel.DataAnnotations;

namespace OnlineLearningBack.Models.Dto
{
    public class LoginDto
    {
        [Required]
        public string? Username { get; init; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; init; }
    }
}
