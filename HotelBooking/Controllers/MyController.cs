using System.Security.Claims;
using HotelBooking.Models;
using HotelBooking.Service.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyController : ControllerBase
    {
        private User? _user;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public MyController( IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }


        protected new User User
        {
             get
                {
                    if (_user != null)
                        return _user;

                    var idClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("id");
                    var id = int.Parse(idClaim.Value);
                    _user = _userService.GetUserById(id);
                    return _user;
                }
        }
    }
}