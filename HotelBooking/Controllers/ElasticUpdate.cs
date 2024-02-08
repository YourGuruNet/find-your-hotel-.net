using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBooking.Interfaces;
using HotelBooking.Models;
using HotelBooking.Repository;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace HotelBooking.Controllers;

public class ElasticUpdate : BaseApiController
{
    private readonly IElasticClient elasticClient;
    private readonly IHotelRepository hotelCommands;
    public ElasticUpdate(IElasticClient elasticClient, IHotelRepository hotelCommands)
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
