using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using HotelBooking.Models;
using System.Linq;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using HotelBooking.Interfaces;

namespace HotelBooking.Repository
{
    public class HotelCommands : IHotelRepository
    { 
        public ActionResult<List<Hotel>>? GetHotelsList()
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
                    place.HotelId,
                    place.Title,
                    place.City,
                    place.Address,
                    place.Country,
                    place.HotelDescription,
                    place.Latitude,
                    place.Longitude,
                    place.PictureUrl,
                    place.Logo,
                    place.CreatorId,
                    place.FiltersList,
                    place.LabelsList

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
