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
    public class SettingsViewModel : INotifyPropertyChanged
    {
        
        private DispatcherTimer updateTimer;
        private APIApi api;

        private const string baseUrl = "http://localhost:5501";

        private ISharedService productionGoal_AP1;
        public int ProductionGoal_AP1
        {
            get { return productionGoal_AP1.ProductionGoal; }
            set
            {
                if (productionGoal_AP1.ProductionGoal != value)
                {
                    productionGoal_AP1.ProductionGoal = value;
                    OnPropertyChanged(nameof(ProductionGoal_AP1));
                }
            }
        }

        private ISharedService productionGoal_AP2;

        public int ProductionGoal_AP2
        {
            get { return productionGoal_AP2.ProductionGoal; }
            set
            {
                if (productionGoal_AP2.ProductionGoal != value)
                {
                    productionGoal_AP2.ProductionGoal = value;
                    OnPropertyChanged(nameof(ProductionGoal_AP2));
                }
            }
        }

        public SettingsViewModel(ISharedService productionService)
        {
            this.productionGoal_AP1 = productionService;
            this.productionGoal_AP2 = productionService;

            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromMilliseconds(2000);
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
            api = new APIApi(baseUrl);
        }

        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            if (api == null) return;
            productionGoal_AP1.ProductionGoal = api.GesamttubenanzZielGet();
            productionGoal_AP2.ProductionGoal = api.GesamttubenanzZielGet();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
