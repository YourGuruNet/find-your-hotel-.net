using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using explore_.net.Models;
using System.Linq;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using explore_.net.Interfaces;

namespace explore_.net.Repository
{
    public class PlacesCommands : IPlaceRepository
    {
        public ActionResult<IList<Place>> GetPlacesList()
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

        public ActionResult<Place> AddNewOrEditPlace(Place place)
        {
            try
            {
                using SqlConnection connection = new(Constants.Constants.connectionString);
                return connection.QueryFirstOrDefault<Place>("sp_places_Upsert", new {
                    PlaceId = place.PlaceId,
                    Title = place.Title,
                    City = place.City,
                    Adress = place.Adress,
                    Country = place.Country,
                    Description = place.Description,
                    Latitude = place.Latitude,
                    Longitude = place.Longitude,
                    CreatorId = place.CreatorId,
                    Picture = place.Picture,
                }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error {0}", ex);
                return null;
            }

        }
    }
}
