using System;
using System.Collections.Generic;
using System.Text;
using Interfaces;

namespace AirportWebAPI.DataAccessLayer.Entities
{
    public class TicketDto : IModelDto
    {
        public Guid Id { get; set; }
        public uint Number { get; set; }
        public float Price { get; set; }
        public Guid FlightId { get; set; }
    }
}
