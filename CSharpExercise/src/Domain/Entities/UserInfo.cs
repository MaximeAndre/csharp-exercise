using System.Text.Json.Serialization;

namespace CSharpExercise.src.Domain.Entities
{
    public class UserInfo
    {
        public int Id { get; set; }
        [JsonIgnore]
        public string? Login { get; set; }
        [JsonIgnore]
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
