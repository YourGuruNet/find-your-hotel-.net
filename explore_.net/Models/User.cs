using System;
using System.Text.Json.Serialization;

namespace explore_.net.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }
    }
}
