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
    public class HotelCommands : IHotelRepository
    {
        public ActionResult<IList<Hotel>> GetHotelsList()
        {
            try
            {
                List<Hotel> items = new();
                using SqlConnection connection = new SqlConnection(Constants.Constants.connectionString);
                return connection.Query<Hotel>("sp_getHotelsList").ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error {0}", ex);
                return null;
            }
        }

        public ActionResult<Hotel> GetHotelById(int hotelId)
        {
            try
            {
                using SqlConnection connection = new(Constants.Constants.connectionString);
                return connection.QueryFirstOrDefault<Hotel>("sp_cafe_getList_by_id", new { hotelId }, commandType: CommandType.StoredProcedure);
            } catch (Exception ex)
            {
                Console.WriteLine("Error {0}", ex);
                return null;
            }
        }

        public ActionResult<Hotel> AddNewOrEditHotel(Hotel place)
        {
            try
            {
                using SqlConnection connection = new(Constants.Constants.connectionString);
                return connection.QueryFirstOrDefault<Hotel>("sp_cafe_upsert", new {
                    HotelId = place.HotelId,
                    Title = place.Title,
                    City = place.City,
                    Adress = place.Adress,
                    Country = place.Country,
                    Description = place.HotelDescription,
                    Latitude = place.Latitude,
                    Longitude = place.Longitude,
                    CreatorId = place.CreatorId,
                    Picture = place.PictureUrl,
                    Logo = place.Logo
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
