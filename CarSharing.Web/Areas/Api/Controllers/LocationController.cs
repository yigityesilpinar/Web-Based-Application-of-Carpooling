using CarSharing.Domain.DataAccess;
using CarSharing.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CarSharing.Web.Areas.Api.Controllers
{
    public class LocationController : ApiController
    {
        [HttpGet]
        public List<Location> GetProvinces()
        {
            GenericRepository<Location> locationRepo = new GenericRepository<Location>();
            return locationRepo.Get(l => l.ParentId == -1).ToList();
        }

        [HttpGet]
        public List<Location> GetDistrictsByProvince(int provinceId)
        {
            GenericRepository<Location> locationRepo = new GenericRepository<Location>();
            return locationRepo.Get(l => l.ParentId == provinceId).ToList();
        }
    }
}
