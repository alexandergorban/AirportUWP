using System;
using Interfaces;

namespace AirportUWP.Models
{
    public class AirplaneDto : IModelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AirplaneTypeDto AirplaneType { get; set; }
        public DateTime DateOfIssue { get; set; }
        public TimeSpan LifeTime { get; set; }
        public bool IsOwnAirplane { get; set; }
    }
}
