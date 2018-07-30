using System;
using Interfaces;

namespace AirportUWP.Models
{
    public class StewardessDto : IModelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
