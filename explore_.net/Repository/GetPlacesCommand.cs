using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using explore_.net.Models;
using System.Linq;

namespace explore_.net.Repository
{
    public class GetPlacesCommand
    {
        private string _connectionString;

        public GetPlacesCommand(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public IList<Place> GetList()
        {
            List<Place> items = new List<Place>();

            var sql = "Places_GetList";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                items = connection.Query<Place>(sql).ToList();
            }

            return items;
        }
    }
}
