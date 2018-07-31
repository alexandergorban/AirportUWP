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
    public sealed partial class DepartureView : Page
    {
        private readonly DepartureService _departureService;
        public ObservableCollection<DepartureDto> DepartureDtos { get; set; }

        public DepartureView()
        {
            this.InitializeComponent();

            _departureService = new DepartureService();
            DepartureDtos = new ObservableCollection<DepartureDto>();

            AirplaneTypesList.ItemsSource = DepartureDtos;
        }

        public async void GetEntities(object sender, RoutedEventArgs e)
        {
            await LoadDepartureAsync();
        }

        private async Task LoadDepartureAsync()
        {
            foreach (var airplaneType in await _departureService.GetEntitiesAsync())
            {
                DepartureDtos.Add(airplaneType);
            }
        }
    }
}
