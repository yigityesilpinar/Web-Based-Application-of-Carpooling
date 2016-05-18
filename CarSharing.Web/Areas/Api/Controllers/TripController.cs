using CarSharing.Domain.DataAccess;
using CarSharing.Domain.Model;
using CarSharing.Web.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Http;
using System.Web.Security;


namespace CarSharing.Web.Areas.Api.Controllers
{
    public class TripController : ApiController
    {
        [HttpPost]
        public object Add(AddTripModel model)
        {
            GenericRepository<Trip> TripRepo = new GenericRepository<Trip>();
            GenericRepository<User> userRepo = new GenericRepository<User>();
             User user = userRepo.GetSingle(u => u.Username == User.Identity.Name);
            DateTime departDate;
            if (!DateTime.TryParse(model.Depdate, out departDate))
            {
                return false;
            }
            
            Trip Trip = new Trip()
            {
                UserId =user.UserId,
                DepartDate = departDate,
                AvailableSeatNumber = model.Seatnum,
                Price = model.Price,
                DepMin=model.Min,
                DepHour=model.Hour,
                EstimatedHour=model.EstHour,
                EstimatedMin=model.EstMin,
                DepartLocationId=model.DepLocId,
                ArrivalLocationId=model.ArrLocId,
                CarId = 1,
               
            };
            TripRepo.Add(Trip);
            return true;
        }

        [HttpGet]
        public List<Trip> Search(string username, string depDate, int? depLocId, int? arrLocId, double? price, int? seatNum, int? estHour, int? hour)
        {
            GenericRepository<Trip> tripRepo = new GenericRepository<Trip>();
            GenericRepository<User> userRepo = new GenericRepository<User>();
            DateTime departDate;
            List<Expression<Func<Trip, bool>>> filters = new List<Expression<Func<Trip, bool>>>();
            if (!string.IsNullOrEmpty(depDate))
            {
                if (DateTime.TryParse(depDate, out departDate))
                {
                    filters.Add(t => t.DepartDate >= departDate);
                }
            }
            if (!string.IsNullOrEmpty(username))
            {
                User user = userRepo.First(u => u.Username.Contains(username));
                filters.Add(t => t.UserId == user.UserId);
            }
            if (seatNum != -1)
            {
                filters.Add(t => t.AvailableSeatNumber >= seatNum);
            }
            if (depLocId != -1)
            {
                filters.Add(t => t.DepartLocationId == depLocId);
            }

            if (arrLocId != -1)
            {
                filters.Add(t => t.ArrivalLocationId == arrLocId);
            }
            if (hour != 0)
            {
                filters.Add(t => t.DepHour >= hour);
            }
            if (estHour != 0)
            {
                filters.Add(t => t.EstimatedHour < estHour);
            }
            if (price!= null)
            {
                filters.Add(t => t.Price <= price);
            }
            

            return tripRepo.GetByMultipleConditions(filters).ToList();
        }

      
    }
}
