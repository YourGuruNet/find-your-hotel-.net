using System.Threading.Tasks;
using HotelBooking.Interfaces;
using HotelBooking.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class HotelsController : BaseApiController
{
    #region Base set up
    private readonly IHotelRepository hotelCommands;
    private readonly IElasticSearchRepository elasticComands;

    public HotelsController(IHotelRepository hotelCommands, IElasticSearchRepository elasticComands)
    {
        this.hotelCommands = hotelCommands;
        this.elasticComands = elasticComands;
    }

    #endregion

    [HttpGet]
    public async Task<IActionResult> GetHotelsList()
    {
        var result = await hotelCommands.GetHotelsList();

        return Ok(result);
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


