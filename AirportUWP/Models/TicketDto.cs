using System;
using Interfaces;

namespace AirportUWP.Models
{
    public class TicketDto : IModelDto
    {
        public Guid Id { get; set; }
        public uint Number { get; set; }
        public float Price { get; set; }
        public Guid FlightId { get; set; }
    }
}
