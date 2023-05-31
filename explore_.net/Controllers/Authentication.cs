using explore_.net.Helpers;
using explore_.net.Interfaces;
using explore_.net.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace explore_.net.Controllers
{
    public class Authentication: BaseApiController
    {
        private readonly IUserRepository userCommands;
        private readonly JwtService jwtService;

        public Authentication(IUserRepository userCommands, JwtService jwtService)
        {
             this.userCommands = userCommands;
             this.jwtService = jwtService;
        }

        [HttpPost("Login")]
        public ActionResult Login(Login login)
        {
            var userInfo = userCommands.Login(login);
            var validateEmailAndPassword = userInfo.Email != login.Email || !BCrypt.Net.BCrypt.Verify(login.Password, userInfo.Password);
            if (validateEmailAndPassword)
            {
                return Ok(new { success = false, message = "Invalid Creditials" });
            }
            var token = jwtService.GenerateToken(userInfo.UserId);

            return Ok(new { success = true, token });
        }

        [HttpPost("AddUser")]
        public ActionResult<CreateUser> AddUser(CreateUser user)
        {

            try
            {
                userCommands.AddNewUser(user);
                return Ok(new { succes = true });
            }
            catch
            {
                return Ok(new {success = true, message = "Failed to create new user" });
            }
        }

        [HttpGet("User")]
        public ActionResult User()
        {
            try
            {
                var token = Request.Cookies["token"];
                var vertifyToken = jwtService.Vertify(token);
                int userId = int.Parse(vertifyToken.Issuer);
                var user = userCommands.GetUserById(userId);
                return Ok(user);
            } catch
            {
                return Unauthorized(); 
            }
        
        }
    }
}
