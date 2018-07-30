using System;
using System.Collections.Generic;
using System.Text;
using Interfaces;

namespace AirportWebAPI.DataAccessLayer.Entities
{
    public class AirportLocationDto : IModelDto
    {
        public Guid Id { get; set; }
        public string AirportName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
