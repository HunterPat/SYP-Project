using Org.OpenAPITools.Api;
using ProdVisAdminFrontend.SharedServices;
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
        private DispatcherTimer updateTimer;
        private APIApi api;
        private const string baseUrl = "http://localhost:5501";

        // Event to be triggered by the button click
        public event EventHandler SwitchUserControlRequested;

        public void OnSwitchUserControlRequested()
        {
            // Raise the event
            SwitchUserControlRequested?.Invoke(this, EventArgs.Empty);
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

        private ISharedService currentAmount_AP1;

        public int CurrentAmount_AP1
        {
            get { return currentAmount_AP1.Value; }
            set
            {
                if (currentAmount_AP1.Value != value)
                {
                    currentAmount_AP1.Value = value;
                    OnPropertyChanged(nameof(CurrentAmount_AP1));
                }
            }
        }

        private ISharedService currentAmount_AP2;

        public int CurrentAmount_AP2
        {
            get { return currentAmount_AP2.Value; }
            set
            {
                if (currentAmount_AP2.Value != value)
                {
                    currentAmount_AP2.Value = value;
                    OnPropertyChanged(nameof(CurrentAmount_AP2));
                }
            }
        }

        private ISharedService productionGoal_AP1;

        public int ProductionGoal_AP1
        {
            get { return productionGoal_AP1.Value; }
            set
            {
                if (productionGoal_AP1.Value != value)
                {
                    productionGoal_AP1.Value = value;
                    OnPropertyChanged(nameof(ProductionGoal_AP1));
                }
            }
        }

        private ISharedService productionGoal_AP2;

        public int ProductionGoal_AP2
        {
            get { return productionGoal_AP2.Value; }
            set
            {
                if (productionGoal_AP2.Value != value)
                {
                    productionGoal_AP2.Value = value;
                    OnPropertyChanged(nameof(ProductionGoal_AP2));
                }
            }
        }


        public OverviewViewModel(ISharedService productionGoal_AP1, ISharedService productionGoal_AP2, ISharedService currentAmount_AP1, ISharedService currentAmount_AP2)
        {

            this.productionGoal_AP1 = productionGoal_AP1;
            this.productionGoal_AP2 = productionGoal_AP2;
            this.currentAmount_AP1 = currentAmount_AP1;
            this.currentAmount_AP2 = currentAmount_AP2;
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
