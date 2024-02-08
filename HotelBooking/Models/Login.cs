using System.Text.Json.Serialization;

namespace HotelBooking.Models
{
    public class Login
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        [JsonIgnore]
        public string? Key { get; set; }
    }
}
