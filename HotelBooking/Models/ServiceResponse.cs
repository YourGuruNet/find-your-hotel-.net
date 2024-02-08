namespace HotelBooking.Models
{
   public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "Request was successful!";
    }
}