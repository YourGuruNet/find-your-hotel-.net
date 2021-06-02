using System.Collections.Generic;
using explore_.net.Models;
using explore_.net.Repository;
using Microsoft.AspNetCore.Mvc;
namespace explore_.net.Controllers
{
    public class PlacesController : BaseApiController
    {
        private static readonly GetPlacesCommand _placesActions = new GetPlacesCommand();

        [HttpGet]
        public IList<Place> GetPlaces()
        {
            return _placesActions.GetList();
        }
    }
}
