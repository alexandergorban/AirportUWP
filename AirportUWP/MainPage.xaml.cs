using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AirportUWP.Views;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AirportUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            routerFrame.Navigate(typeof(HomeView));
            TitleTextBlock.Text = "AirportUWP";
        }

        private void Main_Button_Click(object sender, RoutedEventArgs e)
        {
            mySplitView.IsPaneOpen = !mySplitView.IsPaneOpen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HomeView.IsSelected)
            {
                routerFrame.Navigate(typeof(HomeView));
                TitleTextBlock.Text = "AirportUWP";
            }
            else if (AirplaneView.IsSelected)
            {
                routerFrame.Navigate(typeof(AirplaneView));
                TitleTextBlock.Text = "Airplanes";
            }
            else if (AirplaneTypeView.IsSelected)
            {
                routerFrame.Navigate(typeof(AirplaneTypeView));
                TitleTextBlock.Text = "AirplaneTypes";
            }
            else if (CrewView.IsSelected)
            {
                routerFrame.Navigate(typeof(CrewView));
                TitleTextBlock.Text = "Crews";
            }
            else if (DepartureView.IsSelected)
            {
                routerFrame.Navigate(typeof(DepartureView));
                TitleTextBlock.Text = "Departures";
            }
            else if (FlightView.IsSelected)
            {
                routerFrame.Navigate(typeof(FlightView));
                TitleTextBlock.Text = "Flights";
            }
            else if (PilotView.IsSelected)
            {
                routerFrame.Navigate(typeof(PilotView));
                TitleTextBlock.Text = "Pilots";
            }
            else if (StewardesseView.IsSelected)
            {
                routerFrame.Navigate(typeof(StewardessView));
                TitleTextBlock.Text = "Stewardesses";
            }
            else if (TicketView.IsSelected)
            {
                routerFrame.Navigate(typeof(TicketView));
                TitleTextBlock.Text = "Tickets";
            }
        }
    }
}
