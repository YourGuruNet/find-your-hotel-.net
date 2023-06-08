using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using explore_.net.Helpers;
using explore_.net.Interfaces;
using explore_.net.Models;
using Microsoft.AspNetCore.Mvc;

namespace explore_.net.Repository
{
    public class UserCommands : IUserRepository
    {
        private readonly JwtService jwtService;

        public UserCommands(JwtService jwtService)
        { 
            this.jwtService = jwtService;
        }

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

        public User? GetUser(Login login)
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
                return null;
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


        public ActionResult<Key>? UpdateKeyBackList(Key key)
        {
            try
            {
                using SqlConnection connection = new(Settings.BaseConnection);
                return connection.QueryFirstOrDefault<Key>("sp_update_blackList", new
                {
                   key = key.SecretKey,
                   userId= key.UserId
                }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error {0}", ex);
                return null;
            }

        }


        public bool GeneratePasswordChangLink(string email)
        {
            var user = GetUser(new Login { Email = email });
            if (user == null)
            {
                return false;
            }

            var key = jwtService.GenerateResetToken(user.UserId.ToString());

            UpdateKeyBackList(new Key { SecretKey = key, UserId = user.UserId });

            var link = $"{Settings.WebClient}change-password?key={key}";

            EmailSender.SendEmail("arvisiljins@gmail.com", "Password reset link", link);

            return true;

        }

        public bool ChekIfKeyValid(Key key)
        {
            try
            {
                using SqlConnection connection = new(Settings.BaseConnection);
                var result = connection.QueryFirstOrDefault<Blacklist>("sp_check_blackList", new
                {
                    key = key.SecretKey,
                    userId = key.UserId
                }, commandType: CommandType.StoredProcedure);
                if (result != null && result.IsBlacklisted)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error {0}", ex);
                return false;
            }

        }
    }
}
