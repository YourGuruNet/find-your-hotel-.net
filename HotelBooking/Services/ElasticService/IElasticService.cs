using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelBooking.Service.ElasticService;

public interface IElasticService
{
    Task<IActionResult> SearchHotels(string keyWord);
}
