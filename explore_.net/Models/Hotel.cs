using System;
namespace explore_.net.Models
{
    public class Hotel
    {
        public int HotelId { get; set;}
        public string Title { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string Country { get; set; }
        public string HotelDescription { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int CreatorId { get; set; }
        public string PictureUrl { get; set; }
        public string Logo { get; set; }
    }
}
