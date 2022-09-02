using System.Collections.Generic;
using explore_.net.Interfaces;
using explore_.net.Models;
using Microsoft.AspNetCore.Mvc;
namespace explore_.net.Controllers
{
    public class HotelsController : BaseApiController
    {
        
        private readonly IHotelRepository hotelCommands;

        public HotelsController(IHotelRepository hotelCommands)
        {
            this.hotelCommands = hotelCommands;
        }

        [HttpGet]
        public ActionResult<IList<Hotel>> GetHotelsList()
        {
            return hotelCommands.GetHotelsList();
        }

        [HttpGet("{id}")]
        public ActionResult<Hotel> GetHotelById(int id)
        {
            return hotelCommands.GetHotelById(id);
        }

        [HttpPost("AddOrEditPlace")]
        public ActionResult<Hotel> AddOrEditHotel(Hotel place)
        {
            
            return hotelCommands.AddNewOrEditHotel(place);
        }
    }
}
