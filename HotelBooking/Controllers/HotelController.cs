using System.Collections.Generic;
using System.Threading.Tasks;
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
    public async Task<ActionResult<ServiceResponse<List<Hotel>>>> GetHotelsList()
    {
        return Ok(await _hotelService.GetHotelsList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Hotel>>> GetHotelById(int id)
    {
        return Ok(await _hotelService.GetHotelById(id));
    }

    [HttpPost("AddOrEdit")]
    public async Task<ActionResult<ServiceResponse<Hotel>>> AddOrEditHotel(Hotel place)
    {
        return Ok(await _hotelService.AddNewOrEditHotel(place));
    }
}


