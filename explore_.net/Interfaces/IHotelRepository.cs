using System;
using System.Collections.Generic;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Interfaces
{
    public interface IHotelRepository
    {
        public ActionResult<IList<Hotel>> GetHotelsList();
        public ActionResult<Hotel> GetHotelById(int placeId);
        public ActionResult<Hotel> AddNewOrEditHotel(Hotel place);
        
    }
}
