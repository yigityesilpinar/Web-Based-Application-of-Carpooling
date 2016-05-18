using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSharing.Domain.Model
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }

        [JsonIgnore]
        public virtual ICollection<Trip> DepartTrips { get; set; }
        [JsonIgnore]
        public virtual ICollection<Trip> ArrivalTrips { get; set; }

        public Location()
        {
            DepartTrips = new HashSet<Trip>();
            ArrivalTrips = new HashSet<Trip>();
        }
    }
}
