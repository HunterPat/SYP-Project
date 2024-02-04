using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ProdVisAdminFrontend.ViewModels
{
    class OverviewViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _dateText;
        private DispatcherTimer timeTimer;

        public string DateText
        {
            get { return _dateText; }
            set
            {
                if (_dateText != value)
                {
                    _dateText = value;
                    OnPropertyChanged(nameof(DateText));
                }
            }
        }


        public OverviewViewModel()
        {
            timeTimer = new DispatcherTimer();
            _dateText = "Loading...";
            timeTimer.Interval = TimeSpan.FromMilliseconds(500);
            timeTimer.Tick += TimeTimer_Tick;
            timeTimer.Start();
        }

        private void TimeTimer_Tick(object? sender, EventArgs e)
        {
            DateText = DateTime.Now.ToString("dddd, dd.MM.yyyy - HH:mm:ss");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
