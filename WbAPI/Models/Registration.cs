using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WbAPI.Models
{
    public class Registration
    {
        public int RegistrationNo { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string OwnerName { get; set; }
        public List<Service> Services { get; set; }
        public string Service { get; set; }
        public int ServiceId { get; set; }
    }
}