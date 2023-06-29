using System.Collections.Generic;
using HotelBooking.Interfaces;
using HotelBooking.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpPost("AddOrEditHotel")]
        public ActionResult<Hotel> AddOrEditHotel(Hotel place)
        {
            
            return hotelCommands.AddNewOrEditHotel(place);
        }
    }
  
}


