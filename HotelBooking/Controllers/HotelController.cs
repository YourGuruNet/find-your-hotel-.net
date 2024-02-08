using HotelBooking.Models;
using HotelBooking.Service.HotelService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class HotelsController : MyController
{
    #region Base set up
    private readonly IHotelService _hotelService;

    public HotelsController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    #endregion

    [HttpGet]
    public ActionResult GetHotelsList()
    {
        return Ok(_hotelService.GetHotelsList());
    }

    [HttpGet("{id}")]
    public ActionResult<Hotel> GetHotelById(int id)
    {
        return _hotelService.GetHotelById(id);
    }

    [HttpPost("AddOrEdit")]
    public ActionResult<Hotel> AddOrEditHotel(Hotel place)
    {
        return _hotelService.AddNewOrEditHotel(place);
    }
}


