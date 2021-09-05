using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarMaintenanceApplication.Models
{
    public class ServiceClass
    {
        public enum Services
         { 
          TireChange =1 ,
          OilChange,
          Alignment,
          EngineTune,
          AirFilterChange
        }
    }
}