﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarSharing.Web.Areas.Api.Models
{
    public class AddTripModel
    {

        public string Depdate { get; set; }
        public int DepLocId { get; set; }
        public int ArrLocId { get; set; }
        public double Price { get; set; }
        public int Seatnum { get; set; }
        public int EstHour { get; set; }
        public int EstMin { get; set; }
        public int Hour { get; set; }
        public int Min { get; set; }
    }
}