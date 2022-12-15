using System;
using explore_.net.Models;
using Microsoft.AspNetCore.Mvc;

namespace explore_.net.Interfaces
{
    public interface IUserRepository
    {
        public ActionResult<CreateUser> AddNewUser(CreateUser user);
        public User Login(Login login);
        public User GetUserById(int userId);
    }
}
