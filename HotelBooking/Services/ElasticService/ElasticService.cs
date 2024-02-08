using System.Linq;
using System.Threading.Tasks;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace HotelBooking.Service.ElasticService
{
	public class ElasticService : IElasticService
    {
        private readonly IElasticClient elasticClient;

        public ElasticService(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        #region functions
        public async Task<IActionResult> SearchHotels(string keyWord)
        {
             var result = await elasticClient.SearchAsync<Hotel>(
             s => s.Query(
                 q => q.QueryString(d => d.Query($"*{keyWord}*"))
                 ).Size(100));

            var hotels = result.Documents.ToList();

            return new OkObjectResult(hotels);
        }

        #endregion

    }
}

