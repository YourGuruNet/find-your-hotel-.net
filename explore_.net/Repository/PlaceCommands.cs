using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using explore_.net.Models;
using System.Linq;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using explore_.net.Interfaces;

namespace explore_.net.Repository
{
    public class PlacesCommands : IPlaceRepository
    {

        public IList<Place> GetPlacesList()
        {
            try
            {
                List<Place> items = new();

                using SqlConnection connection = new SqlConnection(Constants.Constants.connectionString);
                return connection.Query<Place>("Places_GetList").ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error {0}", ex);
                return null;
            }
        }

        public ActionResult<Place> GetPlaceById(int placeId)
        {
            try
            {
                using SqlConnection connection = new(Constants.Constants.connectionString);
                return connection.QueryFirstOrDefault<Place>("Places_GetList_By_Id", new { placeId }, commandType: CommandType.StoredProcedure);
            } catch (Exception ex)
            {
                Console.WriteLine("Error {0}", ex);
                return null;
            }
             
        }
    }
}
