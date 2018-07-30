using System;
using System.Collections.Generic;
using Interfaces;

namespace AirportUWP.Models
{
    public class CrewDto : IModelDto
    {
        public Guid Id { get; set; }
        public PilotDto Pilot { get; set; }
        public List<StewardessDto> Stewardesses { get; set; }
    }
}
