using System.Collections.Generic;
using explore_.net.Interfaces;
using explore_.net.Models;
using Microsoft.AspNetCore.Mvc;
namespace explore_.net.Controllers
{
    public class PlacesController : BaseApiController
    {
        
        private readonly IPlaceRepository placeCommands;

        public PlacesController(IPlaceRepository placeCommands)
        {
            this.placeCommands = placeCommands;
        }

        [HttpGet]
        public ActionResult<IList<Place>> GetPlaces()
        {
            return placeCommands.GetPlacesList();
        }

        [HttpGet("{id}")]
        public ActionResult<Place> GetPlaceById(int id)
        {
            return placeCommands.GetPlaceById(id);
        }

        [HttpPost("AddOrEditPlace")]
        public ActionResult<Place> AddOrEditPlace(Place place)
        {
            
            return placeCommands.AddNewOrEditPlace(place);
        }
    }
}
