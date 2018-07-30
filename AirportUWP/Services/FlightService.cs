﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportUWP.Abstractions;
using AirportUWP.Models;

namespace AirportUWP.Services
{
    public class FlightService : BaseAirportDataService<FlightDto>
    {
        public FlightService() : base("http://localhost:32157/api/v1/flights")
        {
            
        }
    }
}
