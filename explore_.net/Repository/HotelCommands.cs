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
        public ActionResult<IList<Hotel>>? GetHotelsList()
        {
            try
            {
                using SqlConnection connection = new SqlConnection(Settings.BaseConnection);
                return connection.Query<Hotel>("sp_getHotelsList").ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error {0}", ex);
                return null;
            }
        }

        public ActionResult<Hotel>? GetHotelById(int hotelId)
        {
            try
            {
                using SqlConnection connection = new(Settings.BaseConnection);
                return connection.QueryFirstOrDefault<Hotel>("sp_cafe_getList_by_id", new { hotelId }, commandType: CommandType.StoredProcedure);
            } catch (Exception ex)
            {
                Console.WriteLine("Error {0}", ex);
                return null;
            }
        }

        public ActionResult<Hotel>? AddNewOrEditHotel(Hotel place)
        {
            try
            {
                using SqlConnection connection = new(Settings.BaseConnection);
                return connection.QueryFirstOrDefault<Hotel>("sp_hotel_upsert", new {
                    HotelId = place.HotelId,
                    Title = place.Title,
                    City = place.City,
                    Address = place.Address,
                    Country = place.Country,
                    HotelDescription = place.HotelDescription,
                    Latitude = place.Latitude,
                    Longitude = place.Longitude,
                    PictureUrl = place.PictureUrl,
                    Logo = place.Logo,
                    CreatorId = place.CreatorId,
                    FiltersList = place.FiltersList,
                    LabelsList = place.LabelsList

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
