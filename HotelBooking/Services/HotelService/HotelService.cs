using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using HotelBooking.Models;
using System.Linq;
using System.Data;
using System;
using System.Threading.Tasks;
using HotelBooking.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Service.HotelService
{
    public class HotelService : IHotelService
    {    
        private readonly DataContext _dataContext;

        public HotelService( DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<Hotel>>> GetHotelsList()
        {
            var serviceResponse = new ServiceResponse<List<Hotel>>();
            try
            {
                var hotels = await _dataContext.Hotels.ToListAsync();
                serviceResponse.Data = hotels;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<Hotel>> GetHotelById(int hotelId)
        {
            var serviceResponse = new ServiceResponse<Hotel>();
            try
            {
                using SqlConnection connection = new(Settings.BaseConnection);
                var hotel = (await connection.QueryFirstOrDefaultAsync<Hotel>("sp_cafe_getList_by_id",
                 new { hotelId }, commandType: CommandType.StoredProcedure)) ?? throw new Exception($"Hotels not found");
            
                serviceResponse.Data = hotel;
                return serviceResponse;
            
            } catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<Hotel>> AddNewOrEditHotel(Hotel place)
        {
             var serviceResponse = new ServiceResponse<Hotel>();
            try
            {
                using SqlConnection connection = new(Settings.BaseConnection);
                var hotel =  await connection.QueryFirstOrDefaultAsync<Hotel>("sp_hotel_upsert", new {
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

                serviceResponse.Data = hotel;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }

        }
    }
}
