using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarSharing.Web.Areas.Api.Models
{
    public class LoginModel
    {
      
        public string Username { get; set; }
        public string Password { get; set; }
    }
}