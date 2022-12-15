using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using explore_.net.Interfaces;
using explore_.net.Models;
using Microsoft.AspNetCore.Mvc;

namespace explore_.net.Repository
{
    public class UserCommands : IUserRepository
    {
        public ActionResult<CreateUser>? AddNewUser(CreateUser user)
        {
            try
            {
                using SqlConnection connection = new(Settings.BaseConnection);
                return connection.QueryFirstOrDefault<CreateUser>("sp_user_create", new
                {
                    user.LastName,
                    user.FirstName,
                    user.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error {0}", ex);
                return null;
            }

        }

        public User Login(Login login)
        {
            try
            {
                using SqlConnection connection = new(Settings.BaseConnection);
                var user = connection.QueryFirstOrDefault<User>("sp_user_getByMail", new
                {
                    login.Email,
                }, commandType: CommandType.StoredProcedure);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error {0}", ex);
                return new User();
            }

        }

        public User GetUserById(int userId)
        {
            try
            {
                using SqlConnection connection = new(Settings.BaseConnection);
                var user = connection.QueryFirstOrDefault<User>("sp_user_getById", new
                {
                    userId,
                }, commandType: CommandType.StoredProcedure);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error {0}", ex);
                return new User();
            }

        }
    }
}
