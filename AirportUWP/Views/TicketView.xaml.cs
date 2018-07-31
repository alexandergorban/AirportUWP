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
    public sealed partial class TicketView : Page
    {
        private readonly FlightService _flightService;
        private readonly TicketService _ticketService;
        public ObservableCollection<FlightDto> FlightDtos { get; set; }
        public ObservableCollection<TicketDto> TicketDtos { get; set; }

        public FlightDto SelectedItem { get; set; }

        public TicketView()
        {
            this.InitializeComponent();

            _flightService = new FlightService();
            _ticketService = new TicketService();

            FlightDtos = new ObservableCollection<FlightDto>();
            TicketDtos = new ObservableCollection<TicketDto>();
            SelectedItem = new FlightDto();

            AirplaneTypesList.ItemsSource = FlightDtos;
        }

        public async void OnSelectedItem(object sender, RoutedEventArgs e)
        {
            SelectedItem = GetSelected(sender, e);

            if (SelectedItem != null)
            {
                TicketDtos.Clear();
                foreach (var ticketDto in await _ticketService.GetTicketsAsync(SelectedItem.Id))
                {
                    TicketDtos.Add(ticketDto);
                }

                TicketsList.ItemsSource = TicketDtos;
            }
        }

        private FlightDto GetSelected(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;
            return (FlightDto)listView.SelectedItem;
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
