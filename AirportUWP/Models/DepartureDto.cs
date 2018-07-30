using System;
using Interfaces;

namespace AirportUWP.Models
{
    public class DepartureDto : IModelDto
    {
        public Guid Id { get; set; }
        public DateTime DepartureTime { get; set; }
        public bool IsFlightDelay { get; set; }
        public DateTime DepartureTimeChanged { get; set; }
        public FlightDto Flight { get; set; }
        public CrewDto Crew { get; set; }
        public AirplaneDto Airplane { get; set; }
    }
}
