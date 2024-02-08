using System;
using System.Collections.Generic;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Service.HotelService
{
    public interface IHotelService
    {
        public ActionResult<List<Hotel>> GetHotelsList();
        public ActionResult<Hotel> GetHotelById(int placeId);
        public ActionResult<Hotel> AddNewOrEditHotel(Hotel place);
        
    }
}
