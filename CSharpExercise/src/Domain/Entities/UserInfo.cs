using System.Text.Json.Serialization;

namespace CSharpExercise.src.Domain.Entities
{
    /// <summary>
    /// UserInfo Entity
    /// </summary>
    public class UserInfo
    {
        public int Id { get; set; }
        //Ignoring Login when displaying in JSON
        [JsonIgnore]
        public string Login { get; set; }
        //Ignoring Password when displaying in JSON
        [JsonIgnore]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
