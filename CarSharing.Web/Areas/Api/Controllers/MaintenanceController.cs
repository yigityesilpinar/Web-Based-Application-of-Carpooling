using CarSharing.Domain.DataAccess;
using CarSharing.Domain.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;

namespace CarSharing.Web.Areas.Api.Controllers
{
    public class MaintenanceController : ApiController
    {
        [HttpGet]
        public void TransferLocations()
        {
            GenericRepository<Location> locationRepo = new GenericRepository<Location>();
            string file = System.Web.HttpContext.Current.Server.MapPath("~/files/provinces.csv");
            using (StreamReader reader = new StreamReader(file, Encoding.UTF8))
            {
                string line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] components = line.Split(',');
                    Location location = new Location()
                    {
                        Name = components[2],
                        Lat = Convert.ToDouble(components[3]),
                        Lon = Convert.ToDouble(components[4]),
                        ParentId = -1
                    };
                    locationRepo.Add(location);
                }
            }

            file = System.Web.HttpContext.Current.Server.MapPath("~/files/districts.csv");
            using (StreamReader reader = new StreamReader(file, Encoding.UTF8))
            {
                string line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] components = line.Split(',');
                    Location location = new Location()
                    {
                        Name = components[2],
                        Lat = Convert.ToDouble(components[3]),
                        Lon = Convert.ToDouble(components[4]),
                        ParentId = Convert.ToInt32(components[1])
                    };
                    locationRepo.Add(location);
                }
            }
        }
    }
}
