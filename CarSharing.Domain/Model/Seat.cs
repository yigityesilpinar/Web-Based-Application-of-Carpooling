using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSharing.Domain.Model
{
    public class Seat
    {
        public int SeatId { get; set; }
        public int TripId { get; set; }
        public int UserId { get; set; }
        public DateTime RegTime { get; set; }
        public int DepartLocationId { get; set; }
        public int ArrivalLocationId { get; set; }

        public virtual Trip Trip { get; set; }
        public virtual User User { get; set; }
        public virtual Location DepartLocation { get; set; }
        public virtual Location ArrivalLocation { get; set; }
    }
}
