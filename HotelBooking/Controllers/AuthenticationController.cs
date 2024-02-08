using HotelBooking.Models;
using HotelBooking.Service.JwtServices;
using HotelBooking.Service.UserService;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    public class AuthenticationController: MyController
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;

        public AuthenticationController(IUserService userService, JwtService jwtService)
        {
             _userService = userService;
             _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public ActionResult Login(Login login)
        {
            if (login.Email == "" || login.Password == "")
            {
                return Ok(new { success = false, message = "Invalid Credentials" });
            }
            var userInfo = _userService.GetUser(login);
            var validateEmailAndPassword = userInfo.Email != login.Email || !BCrypt.Net.BCrypt.Verify(login.Password, userInfo.Password);
            if (validateEmailAndPassword)
            {
                return Ok(new { success = false, message = "Invalid Credentials" });
            }
            var token = _jwtService.GenerateToken(userInfo.UserId);

            return Ok(new { success = true, token });
        }

        [HttpPost("AddUser")]
        public ActionResult<CreateUser> AddUser(CreateUser user)
        {
            try
            {
                _userService.AddNewUser(user);
                return Ok(new { success = true, message = "Account created successfully" });
            }
            catch
            {
                return Ok(new {success = false, message = "Failed to create new user" });
            }
        }

        [HttpPost("CheckUser")]
        public ActionResult CheckUser(Key key)
        {

            if(key.SecretKey == "")
            {
                return Ok(new { success = false, message = "No valid key" });
            }

            try
            {      
                var userId = _jwtService.GetUserIdFromToken(key.SecretKey);

                if (userId == "" || userId == null)
                {
                    return Ok(new { success = false, message = "No valid key" });
                }

                var user = _userService.GetUserById(int.Parse(userId));
                if (user == null)
                {
                    return Ok(new { success = false, message = "No valid user" });
                }

                var isActiveKey = _userService.CheckIfKeyValid(new Key { SecretKey = key.SecretKey, UserId = int.Parse(userId) });

                if (!isActiveKey)
                {
                    return Ok(new { success = false, message = "Key is not active" });
                }

                return Ok(new { success = true, user });
            } catch
            {
                return Ok(new { success = false, message= "Wrong key" });
            }
        
        }


        [HttpPost("SetNewPasswordLink")]
        public ActionResult SetNewPasswordLink(Login login)
        {
            if (login.Email == "")
            {
                return Ok(new { success = false, message = "No email" });
            }

            var result = _userService.GeneratePasswordChangLink(login.Email);

            return Ok(new { success = result });
        }

        [HttpPost("ChangePassword")]
        public ActionResult ChangePassword(Login login)
        {
            if (login.Password == "")
            {
                return Ok(new { success = false, message = "Plese add valid password" });
            }

            var result = _userService.ChangePassword(login);

            return Ok(new { success = result });
        }
    }
}
