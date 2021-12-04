using System.ComponentModel.DataAnnotations;

namespace CSharpExercise.src.Domain.Entities
{
    /// <summary>
    /// Model for the Basic Auth
    /// </summary>
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
