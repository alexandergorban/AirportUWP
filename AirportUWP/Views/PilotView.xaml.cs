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
    public sealed partial class PilotView : Page
    {
        private readonly PilotService _pilotService;
        public ObservableCollection<PilotDto> PilotDtos { get; set; }
        public PilotDto SelectedItem { get; set; }

        public PilotView()
        {
            this.InitializeComponent();

            _pilotService = new PilotService();
            PilotDtos = new ObservableCollection<PilotDto>();
            SelectedItem = new PilotDto();

            AirplaneTypesList.ItemsSource = PilotDtos;

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
                TextName.Text = SelectedItem.Name;
                TextSurname.Text = SelectedItem.Surname.ToString();
                TextDeteOfBirth.Text = SelectedItem.DateOfBirth.ToString();
                TextExperience.Text = SelectedItem.Experience.ToString();
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
            TextSurname.Text = "";
            TextDeteOfBirth.Text = "";
            TextExperience.Text = "";

            ShowDetailFields();
            ShowAddAndHideSaveButtons();
        }

        public async void AddEntity(object sender, RoutedEventArgs e)
        {
            SelectedItem.Id = Guid.NewGuid();
            SelectedItem.Name = TextName.Text;
            SelectedItem.Surname = TextSurname.Text;
            SelectedItem.DateOfBirth = DateTime.Parse(TextDeteOfBirth.Text);
            SelectedItem.Experience = TimeSpan.Parse(TextExperience.Text);

            await _pilotService.CreateEntityAsync(SelectedItem);

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

            await _pilotService.DeleteEntityAsync(SelectedItem.Id.ToString());

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
            SelectedItem.Name = TextName.Text;
            SelectedItem.Surname = TextSurname.Text;
            SelectedItem.DateOfBirth = DateTime.Parse(TextDeteOfBirth.Text);
            SelectedItem.Experience = TimeSpan.Parse(TextExperience.Text);

            var id = SelectedItem.Id;
            SelectedItem.Id = Guid.NewGuid();

            await _pilotService.UpdateEntityAsync(id.ToString(), SelectedItem);

            TextId.IsReadOnly = false;
            SaveButton.Visibility = Visibility.Collapsed;

            await ReloadEntitiesAsync();
        }

        private PilotDto GetSelected(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;
            return (PilotDto)listView.SelectedItem;
        }

        private async Task ReloadEntitiesAsync()
        {
            PilotDtos.Clear();
            foreach (var airplaneType in await _pilotService.GetEntitiesAsync())
            {
                PilotDtos.Add(airplaneType);
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
            LabelSurname.Visibility = Visibility.Visible;
            TextSurname.Visibility = Visibility.Visible;
            LabelDateOfBirth.Visibility = Visibility.Visible;
            TextDeteOfBirth.Visibility = Visibility.Visible;
            LabelExperience.Visibility = Visibility.Visible;
            TextExperience.Visibility = Visibility.Visible;
        }

        private void HideDetailFields()
        {
            LabelId.Visibility = Visibility.Collapsed;
            TextId.Visibility = Visibility.Collapsed;
            LabelName.Visibility = Visibility.Collapsed;
            TextName.Visibility = Visibility.Collapsed;
            LabelSurname.Visibility = Visibility.Collapsed;
            TextSurname.Visibility = Visibility.Collapsed;
            LabelDateOfBirth.Visibility = Visibility.Collapsed;
            TextDeteOfBirth.Visibility = Visibility.Collapsed;
            LabelExperience.Visibility = Visibility.Collapsed;
            TextExperience.Visibility = Visibility.Collapsed;
        }
    }
}
