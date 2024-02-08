using System.Linq;
using System.Threading.Tasks;
using HotelBooking.Interfaces;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace HotelBooking.Repository
{
	public class ElasticSearchComands : IElasticSearchRepository
    {
        private readonly IElasticClient elasticClient;

        public ElasticSearchComands(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        #region funtions
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

