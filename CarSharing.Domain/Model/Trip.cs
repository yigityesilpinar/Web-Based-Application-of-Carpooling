using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSharing.Domain.Model
{
    public class Trip
    {
        public int TripId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime DepartDate { get; set; }
        public int EstimatedMin { get; set; }
        public int EstimatedHour { get; set; }
        public int DepHour { get; set; }
        public int DepMin { get; set; }
        public double Price { get; set; }
        public int DepartLocationId { get; set; }
        public int ArrivalLocationId { get; set; }
        public int AvailableSeatNumber { get; set; }

        public virtual User User { get; set; }
        public virtual Car Car { get; set; }
        public virtual Location DepartLocation { get; set; }
        public virtual Location ArrivalLocation { get; set; }
        [JsonIgnore]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        [JsonIgnore]
        public virtual ICollection<Seat> Seats { get; set; }

        public Trip()
        {
            Seats = new HashSet<Seat>();
            Feedbacks = new HashSet<Feedback>();
        }
    }
}
