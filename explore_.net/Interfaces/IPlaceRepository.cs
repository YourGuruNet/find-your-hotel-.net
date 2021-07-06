using System;
using System.Collections.Generic;
using explore_.net.Models;
using Microsoft.AspNetCore.Mvc;

namespace explore_.net.Interfaces
{
    public interface IPlaceRepository
    {
        public ActionResult<IList<Place>> GetPlacesList();
        public ActionResult<Place> GetPlaceById(int placeId);
        public ActionResult<Place> AddNewOrEditPlace(Place place);
        
    }
}
