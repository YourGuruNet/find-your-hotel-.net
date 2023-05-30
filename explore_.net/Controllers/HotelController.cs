using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using explore_.net.Interfaces;
using explore_.net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace explore_.net.Controllers
{
    public class HotelsController : BaseApiController
    {
        private readonly IHotelRepository hotelCommands;
        private readonly HttpClient _httpClient;


        public HotelsController(IHotelRepository hotelCommands, IHttpClientFactory httpClientFactory)
        {
            this.hotelCommands = hotelCommands;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", "ff98d447d7msh67a868efb5bdce7p182461jsn8323cd5cf915");
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "skyscanner-skyscanner-hotel-prices-v1.p.rapidapi.com");
        }



            [HttpGet]
        public ActionResult<IList<Hotel>> GetHotelsList()
        {
            var a = hotelCommands.GetHotelsList();
            return a;
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


  
}


