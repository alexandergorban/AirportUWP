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
    public sealed partial class CrewView : Page
    {
        private readonly CrewService _crewService;
        private readonly PilotService _pilotService;
        private readonly StewardessService _stewardessService;

        public ObservableCollection<CrewDto> CrewDtos { get; set; }
        public ObservableCollection<PilotDto> PilotDtos { get; set; }
        public ObservableCollection<StewardessDto> StewardessDtos { get; set; }

        public CrewDto SelectedItem { get; set; }

        public CrewView()
        {
            this.InitializeComponent();

            _crewService = new CrewService();
            _pilotService = new PilotService();
            _stewardessService = new StewardessService();

            CrewDtos = new ObservableCollection<CrewDto>();
            PilotDtos = new ObservableCollection<PilotDto>();
            StewardessDtos = new ObservableCollection<StewardessDto>();

            SelectedItem = new CrewDto();

            AirplaneTypesList.ItemsSource = CrewDtos;

            HideDetailFields();
            HideAddAndSaveButtons();
        }

        public async void GetEntities(object sender, RoutedEventArgs e)
        {
            HideAddAndSaveButtons();

            await LoadCrewsAsync();
            await LoadPilotsAsync();
            await LoadStewardessesAsync();
        }

        public void OnSelectedItem(object sender, RoutedEventArgs e)
        {
            SelectedItem = GetSelected(sender, e);

            if (SelectedItem != null)
            {
                TextId.Text = SelectedItem.Id.ToString();
                TextName.Text = SelectedItem.Pilot.Name;
                TextStewardess1.Text = SelectedItem.Stewardesses.First().Id.ToString();
                TextStewardess2.Text = SelectedItem.Stewardesses.Last().Id.ToString();
            }
        }

        public void CreateEntity(object sender, RoutedEventArgs e)
        {
            if (TextId.IsReadOnly)
            {
                TextId.IsReadOnly = false;
            }

            TextId.Text = "";
            TextId.IsReadOnly = true;
            TextName.Text = "";
            TextStewardess1.Text = "";
            TextStewardess2.Text = "";

            ShowDetailFields();
            ShowAddAndHideSaveButtons();
        }

        public async void AddEntity(object sender, RoutedEventArgs e)
        {
            SelectedItem.Id = Guid.NewGuid();
            SelectedItem.Pilot.Name = TextName.Text;

            SelectedItem.Stewardesses.Add(StewardessDtos
                .First(stewardess => 
                    stewardess.Id.ToString() == TextStewardess1.Text));
            SelectedItem.Stewardesses.Add(StewardessDtos
                .First(stewardess => 
                    stewardess.Id.ToString() == TextStewardess2.Text));

            await _crewService.CreateEntityAsync(SelectedItem);

            AddButton.Visibility = Visibility.Collapsed;
            TextId.IsReadOnly = false;

            await LoadCrewsAsync();
        }

        public async void DeleteEntity(object sender, RoutedEventArgs e)
        {
            HideAddAndSaveButtons();

            if (TextId.IsReadOnly)
            {
                TextId.IsReadOnly = false;
            }

            await _crewService.DeleteEntityAsync(SelectedItem.Id.ToString());

            await LoadCrewsAsync();
        }

        public void UpdateEntity(object sender, RoutedEventArgs e)
        {
            TextId.IsReadOnly = true;

            ShowDetailFields();
            HideAddAndShowSaveButtons();
        }

        public async void SaveEntity(object sender, RoutedEventArgs e)
        {
            SelectedItem.Pilot.Name = TextName.Text;

            SelectedItem.Stewardesses.Add(StewardessDtos
                .First(stewardess =>
                    stewardess.Id.ToString() == TextStewardess1.Text));
            SelectedItem.Stewardesses.Add(StewardessDtos
                .First(stewardess =>
                    stewardess.Id.ToString() == TextStewardess2.Text));

            var id = SelectedItem.Id;
            SelectedItem.Id = Guid.NewGuid();

            await _crewService.UpdateEntityAsync(id.ToString(), SelectedItem);

            TextId.IsReadOnly = false;
            SaveButton.Visibility = Visibility.Collapsed;

            await LoadCrewsAsync();
        }

        private CrewDto GetSelected(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;
            return (CrewDto)listView.SelectedItem;
        }

        private async Task LoadCrewsAsync()
        {
            CrewDtos.Clear();
            foreach (var airplaneType in await _crewService.GetEntitiesAsync())
            {
                CrewDtos.Add(airplaneType);
            }
        }

        private async Task LoadPilotsAsync()
        {
            PilotDtos.Clear();
            foreach (var airplaneType in await _pilotService.GetEntitiesAsync())
            {
                PilotDtos.Add(airplaneType);
            }

            
        }

        private async Task LoadStewardessesAsync()
        {
            StewardessDtos.Clear();
            foreach (var airplaneType in await _stewardessService.GetEntitiesAsync())
            {
                StewardessDtos.Add(airplaneType);
            }
        }

        private void ShowAddAndHideSaveButtons()
        {
            AddButton.Visibility = Visibility.Visible;
            SaveButton.Visibility = Visibility.Collapsed;
        }

        private void HideAddAndShowSaveButtons()
        {
            AddButton.Visibility = Visibility.Collapsed;
            SaveButton.Visibility = Visibility.Visible;
        }

        private void HideAddAndSaveButtons()
        {
            AddButton.Visibility = Visibility.Collapsed;
            SaveButton.Visibility = Visibility.Collapsed;
        }

        private void ShowDetailFields()
        {
            LabelId.Visibility = Visibility.Visible;
            TextId.Visibility = Visibility.Visible;
            LabelName.Visibility = Visibility.Visible;
            TextName.Visibility = Visibility.Visible;
            LabelStewardess1.Visibility = Visibility.Visible;
            TextStewardess1.Visibility = Visibility.Visible;
            LabelStewardess2.Visibility = Visibility.Visible;
            TextStewardess2.Visibility = Visibility.Visible;
        }

        private void HideDetailFields()
        {
            LabelId.Visibility = Visibility.Collapsed;
            TextId.Visibility = Visibility.Collapsed;
            LabelName.Visibility = Visibility.Collapsed;
            TextName.Visibility = Visibility.Collapsed;
            LabelStewardess1.Visibility = Visibility.Collapsed;
            TextStewardess1.Visibility = Visibility.Collapsed;
            LabelStewardess2.Visibility = Visibility.Collapsed;
            TextStewardess2.Visibility = Visibility.Collapsed;
        }
    }
}
