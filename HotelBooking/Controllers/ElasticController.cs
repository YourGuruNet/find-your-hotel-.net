using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBooking.Models;
using HotelBooking.Service.HotelService;
using HotelBooking.Service.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace HotelBooking.Controllers;

public class ElasticController : MyController
{
    private readonly IElasticClient elasticClient;
    private readonly IHotelService hotelCommands;
    public ElasticController(IHttpContextAccessor httpContextAccessor, IUserService userService, IElasticClient elasticClient, IHotelService hotelCommands) : base(httpContextAccessor, userService)
	{
        this.elasticClient = elasticClient;
        this.hotelCommands = hotelCommands;
    }

    [HttpPost("UpdateProductList")]
    public async Task<IActionResult> UpdateProductList()
    {
        var hotelList =  hotelCommands.GetHotelsList();

        await elasticClient.IndexDocumentAsync(hotelList);

        return Ok();
    }
}
