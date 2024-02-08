using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBooking.Models;
using HotelBooking.Service.HotelService;
using HotelBooking.Service.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class HotelsController : MyController
{
    #region Base set up
    private readonly IHotelService _hotelService;

    public HotelsController(IHttpContextAccessor httpContextAccessor, IUserService userService, IHotelService hotelService)  : base(httpContextAccessor, userService)
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

    [HttpPost("Add")]
    public async Task<ActionResult<ServiceResponse<Hotel>>> Add(Hotel place)
    {
        place.CreatorId = User.UserId;
        return Ok(await _hotelService.Add(place));
    }
}


