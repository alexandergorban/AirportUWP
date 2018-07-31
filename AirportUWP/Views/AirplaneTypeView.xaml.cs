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
    public sealed partial class AirplaneTypeView : Page
    {
        private readonly AirplaneTypeService _airplaneTypeService;
        public ObservableCollection<AirplaneTypeDto> AirplaneTypeDtos { get; set; }
        public AirplaneTypeDto SelectedItem { get; set; }

        public AirplaneTypeView()
        {
            this.InitializeComponent();

            _airplaneTypeService = new AirplaneTypeService();
            AirplaneTypeDtos = new ObservableCollection<AirplaneTypeDto>();
            SelectedItem = new AirplaneTypeDto();

            AirplaneTypesList.ItemsSource = AirplaneTypeDtos;

            HideDetailFields();
            HideAddAndSaveButtons();
        }

        public async void GetEntities(object sender, RoutedEventArgs e)
        {
            HideAddAndSaveButtons();

            await ReloadEntitiesAsync();
        }

        public void OnSelectedItem(object sender, RoutedEventArgs e)
        {
            SelectedItem = GetSelected(sender, e);

            if (SelectedItem != null)
            {
                TextId.Text = SelectedItem.Id.ToString();
                TextAirplaneModel.Text = SelectedItem.Model;
                TextNumberOfSeats.Text = SelectedItem.NumberOfSeats.ToString();
                TextLoadCapacity.Text = SelectedItem.LoadCapacity.ToString();
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
            TextAirplaneModel.Text = "";
            TextNumberOfSeats.Text = "";
            TextLoadCapacity.Text = "";

            ShowDetailFields();
            ShowAddAndHideSaveButtons();
        }

        public async void AddEntity(object sender, RoutedEventArgs e)
        {
            SelectedItem.Id = Guid.NewGuid();
            SelectedItem.Model = TextAirplaneModel.Text;
            SelectedItem.NumberOfSeats = int.Parse(TextNumberOfSeats.Text);
            SelectedItem.LoadCapacity = int.Parse(TextLoadCapacity.Text);

            await _airplaneTypeService.CreateEntityAsync(SelectedItem);

            AddButton.Visibility = Visibility.Collapsed;
            TextId.IsReadOnly = false;

            await ReloadEntitiesAsync();
        }

        public async void DeleteEntity(object sender, RoutedEventArgs e)
        {
            HideAddAndSaveButtons();

            if (TextId.IsReadOnly)
            {
                TextId.IsReadOnly = false;
            }

            await _airplaneTypeService.DeleteEntityAsync(SelectedItem.Id.ToString());

            await ReloadEntitiesAsync();
        }

        public void UpdateEntity(object sender, RoutedEventArgs e)
        {
            TextId.IsReadOnly = true;

            ShowDetailFields();
            HideAddAndShowSaveButtons();
        }

        public async void SaveEntity(object sender, RoutedEventArgs e)
        {
            SelectedItem.Model = TextAirplaneModel.Text;
            SelectedItem.NumberOfSeats = int.Parse(TextNumberOfSeats.Text);
            SelectedItem.LoadCapacity = int.Parse(TextLoadCapacity.Text);

            var id = SelectedItem.Id;
            SelectedItem.Id = Guid.NewGuid();

            await _airplaneTypeService.UpdateEntityAsync(id.ToString(), SelectedItem);

            TextId.IsReadOnly = false;
            SaveButton.Visibility = Visibility.Collapsed;

            await ReloadEntitiesAsync();
        }

        private AirplaneTypeDto GetSelected(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;
            return (AirplaneTypeDto)listView.SelectedItem;
        }

        private async Task ReloadEntitiesAsync()
        {
            AirplaneTypeDtos.Clear();
            foreach (var airplaneType in await _airplaneTypeService.GetEntitiesAsync())
            {
                AirplaneTypeDtos.Add(airplaneType);
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
            LabelAirplaneModel.Visibility = Visibility.Visible;
            TextAirplaneModel.Visibility = Visibility.Visible;
            LabelNumberOfSeats.Visibility = Visibility.Visible;
            TextNumberOfSeats.Visibility = Visibility.Visible;
            LabelLoadCapacity.Visibility = Visibility.Visible;
            TextLoadCapacity.Visibility = Visibility.Visible;
        }

        private void HideDetailFields()
        {
            LabelId.Visibility = Visibility.Collapsed;
            TextId.Visibility = Visibility.Collapsed;
            LabelAirplaneModel.Visibility = Visibility.Collapsed;
            TextAirplaneModel.Visibility = Visibility.Collapsed;
            LabelNumberOfSeats.Visibility = Visibility.Collapsed;
            TextNumberOfSeats.Visibility = Visibility.Collapsed;
            LabelLoadCapacity.Visibility = Visibility.Collapsed;
            TextLoadCapacity.Visibility = Visibility.Collapsed;
        }
    }
}
