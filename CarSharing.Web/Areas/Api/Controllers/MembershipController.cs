using CarSharing.Domain.DataAccess;
using CarSharing.Domain.Helper;
using CarSharing.Domain.Model;
using CarSharing.Web.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CarSharing.Web.Areas.Api.Controllers
{
    public class MembershipController : ApiController
    { 
        [HttpPost]
        public object Login(LoginModel model)
        {
            GenericRepository<User> userRepo = new GenericRepository<User>();
            User user = userRepo.GetSingle(u => u.Username == model.Username);
            if (user == null)
            {
                return false;
            }
            if (!CryptoHelper.VerifyHashedPassword(user.Password, model.Password))
            {
                return false;
            }
            System.Web.Security.FormsAuthentication.SetAuthCookie(model.Username, true);
            return true;
        
        }
         [HttpPost]
        public object CheckUserName(RegisterModel model)
        {
            GenericRepository<User> userRepo = new GenericRepository<User>();
            User user = userRepo.First(u => u.Username == model.Username);
            if (user == null)
            {
                return true;
            }
            return false;
        }
        public object CheckMail(RegisterModel model)
        {
            GenericRepository<User> userRepo = new GenericRepository<User>();
            User user = userRepo.First(u => u.Email == model.Email);
            if (user == null)
            {
                return true;
            }

            return false;

        }
    
        [HttpPost]
        public object Register(RegisterModel model)
        {
            GenericRepository<User> userRepo = new GenericRepository<User>();
            User user = new User()
            {
                Name = model.Name,
                Surname = model.Lastname,
                Email = model.Email,
                Username = model.Username,
                Password = CryptoHelper.HashPassword(model.Password),
                Gender = (model.Gender == "Female") ? true : false,
                Age = model.Age
            };
            userRepo.Add(user);
            return true;
        }

        [HttpPost]
        public object Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return true;
        }
        [HttpGet]
        public object IsLoggedIn()
        {
         if( User.Identity.Name=="")
            return false;
         return true;
        }
    }
}