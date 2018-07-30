using System;
using Interfaces;

namespace AirportUWP.Models
{
    public class AirplaneTypeDto : IModelDto
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public int NumberOfSeats { get; set; }
        public int LoadCapacity { get; set; }
    }
}
