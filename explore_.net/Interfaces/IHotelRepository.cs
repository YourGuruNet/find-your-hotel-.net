using System;
using System.Collections.Generic;
using explore_.net.Models;
using Microsoft.AspNetCore.Mvc;

namespace explore_.net.Interfaces
{
    public interface IHotelRepository
    {
        public ActionResult<IList<Hotel>> GetHotelsList();
        public ActionResult<Hotel> GetHotelById(int placeId);
        public ActionResult<Hotel> AddNewOrEditHotel(Hotel place);
        
    }
}
