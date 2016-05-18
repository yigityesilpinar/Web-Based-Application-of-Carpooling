using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarSharing.Web.Areas.Api.Models
{ 
    public class RegisterModel
    {
        
        public string Username { get; set; }        
        public string Password { get; set; }
        public string Repassword { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
    }
}