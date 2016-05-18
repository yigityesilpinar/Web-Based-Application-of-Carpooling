using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSharing.Domain.Model
{
    public class Car
    {
        public int CarId { get; set; }
        public int SeatNumber { get; set; }
        public double FuelPerformance { get; set; }

        [JsonIgnore]
        public virtual ICollection<Trip> Trips { get; set; }

        public Car()
        {
            Trips = new HashSet<Trip>();
        }
    }
}
