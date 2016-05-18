using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSharing.Domain.Model
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public int TripId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }

        public virtual Trip Trip { get; set; }
        public virtual User User { get; set; }
    }
}
