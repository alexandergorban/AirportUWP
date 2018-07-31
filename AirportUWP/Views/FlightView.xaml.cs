using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AirportUWP.Models;
using AirportUWP.Services;

namespace AirportUWP.Views
{
    public sealed partial class FlightView : Page
    {
        private readonly FlightService _flightService;
        public ObservableCollection<FlightDto> FlightDtos { get; set; }

        public FlightView()
        {
            this.InitializeComponent();

            _flightService = new FlightService();
            FlightDtos = new ObservableCollection<FlightDto>();

            AirplaneTypesList.ItemsSource = FlightDtos;
        }

        public async void GetEntities(object sender, RoutedEventArgs e)
        {
            await LoadFlightAsync();
        }

        private async Task LoadFlightAsync()
        {
            foreach (var airplaneType in await _flightService.GetEntitiesAsync())
            {
                FlightDtos.Add(airplaneType);
            }
        }
    }
}
