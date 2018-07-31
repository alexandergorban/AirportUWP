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
    public sealed partial class AirplaneView : Page
    {
        private readonly AirplaneService _airplaneService;
        public ObservableCollection<AirplaneDto> AirplaneDtos { get; set; }
        public AirplaneDto SelectedItem { get; set; }

        public AirplaneView()
        {
            this.InitializeComponent();

            _airplaneService = new AirplaneService();
            AirplaneDtos = new ObservableCollection<AirplaneDto>();
            SelectedItem = new AirplaneDto();

            AirplaneTypesList.ItemsSource = AirplaneDtos;

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
                TextAirplaneName.Text = SelectedItem.Name;
                TextDateOfIssue.Text = SelectedItem.DateOfIssue.ToString();
                TextLifeTime.Text = SelectedItem.LifeTime.ToString();
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
            TextAirplaneName.Text = "";
            TextDateOfIssue.Text = "";
            TextLifeTime.Text = "";

            ShowDetailFields();
            ShowAddAndHideSaveButtons();
        }

        public async void AddEntity(object sender, RoutedEventArgs e)
        {
            SelectedItem.Id = Guid.NewGuid();
            SelectedItem.Name = TextAirplaneName.Text;
            SelectedItem.DateOfIssue = DateTime.Parse(TextDateOfIssue.Text);
            SelectedItem.LifeTime = TimeSpan.Parse(TextLifeTime.Text);

            await _airplaneService.CreateEntityAsync(SelectedItem);

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

            await _airplaneService.DeleteEntityAsync(SelectedItem.Id.ToString());

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
            SelectedItem.Name = TextAirplaneName.Text;
            SelectedItem.DateOfIssue = DateTime.Parse(TextDateOfIssue.Text);
            SelectedItem.LifeTime = TimeSpan.Parse(TextLifeTime.Text);

            var id = SelectedItem.Id;
            SelectedItem.Id = Guid.NewGuid();

            await _airplaneService.UpdateEntityAsync(id.ToString(), SelectedItem);

            TextId.IsReadOnly = false;
            SaveButton.Visibility = Visibility.Collapsed;

            await ReloadEntitiesAsync();
        }

        private AirplaneDto GetSelected(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;
            return (AirplaneDto)listView.SelectedItem;
        }

        private async Task ReloadEntitiesAsync()
        {
            AirplaneDtos.Clear();
            foreach (var airplaneType in await _airplaneService.GetEntitiesAsync())
            {
                AirplaneDtos.Add(airplaneType);
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
            LabelAirplaneName.Visibility = Visibility.Visible;
            TextAirplaneName.Visibility = Visibility.Visible;
            LabelDateOfIssue.Visibility = Visibility.Visible;
            TextDateOfIssue.Visibility = Visibility.Visible;
            LabelLifeTime.Visibility = Visibility.Visible;
            TextLifeTime.Visibility = Visibility.Visible;
        }

        private void HideDetailFields()
        {
            LabelId.Visibility = Visibility.Collapsed;
            TextId.Visibility = Visibility.Collapsed;
            LabelAirplaneName.Visibility = Visibility.Collapsed;
            TextAirplaneName.Visibility = Visibility.Collapsed;
            LabelDateOfIssue.Visibility = Visibility.Collapsed;
            TextDateOfIssue.Visibility = Visibility.Collapsed;
            LabelLifeTime.Visibility = Visibility.Collapsed;
            TextLifeTime.Visibility = Visibility.Collapsed;
        }
    }
}
