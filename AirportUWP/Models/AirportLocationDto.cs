using System;
using Interfaces;

namespace AirportUWP.Models
{
    public class AirportLocationDto : IModelDto
    {
        public Guid Id { get; set; }
        public string AirportName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
