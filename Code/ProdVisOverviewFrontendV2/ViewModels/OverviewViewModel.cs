using Org.OpenAPITools.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ProdVisOverviewFrontendV2.ViewModels
{
    class OverviewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _dateText;
        private DispatcherTimer timeTimer;
        private DispatcherTimer updateTimer;
        private APIApi api;
        private const string baseUrl = "http://localhost:5501";

        public OverviewViewModel()
        {

            
            timeTimer = new DispatcherTimer();
            _dateText = "Loading...";
            timeTimer.Interval = TimeSpan.FromMilliseconds(500);
            timeTimer.Tick += TimeTimer_Tick;
            timeTimer.Start();

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TimeTimer_Tick(object? sender, EventArgs e)
        {
            DateText = DateTime.Now.ToString("dddd, dd.MM.yyyy - HH:mm:ss");
        }
        

        private int currentAmount_AP1;

        public int CurrentAmount_AP1
        {
            get { return currentAmount_AP1; }
            set
            {
                if (currentAmount_AP1 != value)
                {
                    currentAmount_AP1 = value;
                    OnPropertyChanged(nameof(CurrentAmount_AP1));
                }
            }
        }

        private int currentAmount_AP2;

        public int CurrentAmount_AP2
        {
            get { return currentAmount_AP2; }
            set
            {
                if (currentAmount_AP2 != value)
                {
                    currentAmount_AP2 = value;
                    OnPropertyChanged(nameof(CurrentAmount_AP2));
                }
            }
        }

        private int productionGoal_AP1;

        public int ProductionGoal_AP1
        {
            get { return productionGoal_AP1; }
            set
            {
                if (productionGoal_AP1 != value)
                {
                    productionGoal_AP1 = value;
                    OnPropertyChanged(nameof(ProductionGoal_AP1));
                }
            }
        }

        private int productionGoal_AP2;

        public int ProductionGoal_AP2
        {
            get { return productionGoal_AP2; }
            set
            {
                if (productionGoal_AP2 != value)
                {
                    productionGoal_AP2 = value;
                    OnPropertyChanged(nameof(ProductionGoal_AP2));
                }
            }
        }

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

        private int progress_AP1;

        public int Progress_AP1
        {
            get { return progress_AP1; }
            set
            {
                if (progress_AP1 != value)
                {
                    progress_AP1 = value;
                    OnPropertyChanged(nameof(Progress_AP1));
                }
            }
        }

        private int progress_AP2;

        public int Progress_AP2
        {
            get { return progress_AP2; }
            set
            {
                if (progress_AP2 != value)
                {
                    progress_AP2 = value;
                    OnPropertyChanged(nameof(Progress_AP2));
                }
            }
        }
    }
}
