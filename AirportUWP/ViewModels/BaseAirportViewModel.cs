using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace AirportUWP.ViewModels
{
    public class BaseAirportViewModel : ViewModelBase
    {
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }
    }
}
