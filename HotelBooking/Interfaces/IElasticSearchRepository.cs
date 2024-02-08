using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelBooking.Interfaces;

public interface IElasticSearchRepository
{
    Task<IActionResult> SearchHotels(string keyWord);
}


