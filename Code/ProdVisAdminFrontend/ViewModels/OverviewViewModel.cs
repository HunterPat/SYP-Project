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

        private ISharedService productionGoal_1;

        public int ProductionGoal_1
        {
            get { return productionGoal_1.ProductionGoal; }
            set
            {
                if (productionGoal_1.ProductionGoal != value)
                {
                    productionGoal_1.ProductionGoal = value;
                    OnPropertyChanged(nameof(ProductionGoal_1));
                }
            }
        }

        private ISharedService productionGoal_2;

        public int ProductionGoal_2
        {
            get { return productionGoal_2.ProductionGoal; }
            set
            {
                if (productionGoal_2.ProductionGoal != value)
                {
                    productionGoal_2.ProductionGoal = value;
                    OnPropertyChanged(nameof(ProductionGoal_2));
                }
            }
        }


        public OverviewViewModel(ISharedService productionGoal_1, ISharedService productionGoal_2)
        {

            this.productionGoal_1 = productionGoal_1;
            this.productionGoal_2 = productionGoal_2;
            timeTimer = new DispatcherTimer();
            _dateText = "Loading...";
            timeTimer.Interval = TimeSpan.FromMilliseconds(500);
            timeTimer.Tick += TimeTimer_Tick;
            timeTimer.Start();

            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromMilliseconds(2000);
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
            api = new APIApi(baseUrl);
        }

        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            if (api == null) return;
            productionGoal_1.ProductionGoal = api.GesamttubenanzZielGet();
            productionGoal_2.ProductionGoal = api.GesamttubenanzZielGet();
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
