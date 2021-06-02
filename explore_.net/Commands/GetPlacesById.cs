using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using explore_.net.Models;
using System.Linq;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace explore_.net.Repository
{
    public class GetPlacesById
    {
        public ActionResult<Place> GetById(int placeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Constants.Constants.connectionString))
                {
                    return connection.QueryFirstOrDefault<Place>("Places_GetList_By_Id", new { placeId }, commandType: CommandType.StoredProcedure);
                }
            } catch (Exception ex)
            {
                return null;
            }
             
        }
    }
}
