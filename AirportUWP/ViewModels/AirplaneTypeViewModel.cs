using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportUWP.Models;
using AirportUWP.Services;

namespace AirportUWP.ViewModels
{
    public class AirplaneTypeViewModel : BaseAirportViewModel
    {
        public ObservableCollection<AirplaneTypeDto> AirplaneTypeDtos { get; set; }
        private readonly AirplaneTypeService _airplaneTypeService;

        public AirplaneTypeViewModel()
        {
            _airplaneTypeService = new AirplaneTypeService();
            AirplaneTypeDtos = new ObservableCollection<AirplaneTypeDto>();

            Title = "AirplaneType";

            GetEntities();
        }

        public void GetEntities()
        {
            foreach (var airplaneTypeDto in _airplaneTypeService.GetEntitiesAsync().Result)
            {
                AirplaneTypeDtos.Add(airplaneTypeDto);
            }
        }

    }
}
