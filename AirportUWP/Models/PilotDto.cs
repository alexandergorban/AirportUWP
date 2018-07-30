using System;
using System.Collections.Generic;
using System.Text;
using Interfaces;

namespace AirportWebAPI.DataAccessLayer.Entities
{
    public class PilotDto : IModelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public TimeSpan Experience { get; set; }
    }
}
