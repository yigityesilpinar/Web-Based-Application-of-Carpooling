using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSharing.Domain.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool? Gender { get; set; }
        public int? Age { get; set; }
        [JsonIgnore]
        public virtual ICollection<Trip> Trips { get; set; }
        [JsonIgnore]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        [JsonIgnore]
        public virtual ICollection<Seat> Seats { get; set; }

        public User()
        {
            Seats = new HashSet<Seat>();
            Trips = new HashSet<Trip>();
            Feedbacks = new HashSet<Feedback>();
        }
    }
}
