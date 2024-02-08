using Dapper;
using System.Collections.Generic;
using HotelBooking.Models;
using System;
using System.Threading.Tasks;
using HotelBooking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace HotelBooking.Service.HotelService
{
    public class HotelService : IHotelService
    {    
        private readonly DataContext _dataContext;

        public HotelService( DataContext dataContext, IHttpContextAccessor httpContextAccessor)
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
                var hotel =   await _dataContext.Hotels.FirstOrDefaultAsync(hotel => hotel.HotelId == hotelId)  ?? throw new Exception($"Hotels not found");   
                serviceResponse.Data = hotel;
                return serviceResponse;
            
            } catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<Hotel>> Add(Hotel hotel)
        {
             var serviceResponse = new ServiceResponse<Hotel>();
            try
            {
                var newHotel = hotel;
                _dataContext.Hotels.Add(hotel);
                await _dataContext.SaveChangesAsync(); 
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
