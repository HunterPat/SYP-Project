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

        public SettingsViewModel(ISharedService productionGoal_AP1, ISharedService productionGoal_AP2, ISharedService currentAmount_AP1, ISharedService currentAmount_AP2)
        {
            this.productionGoal_AP1 = productionGoal_AP1;
            this.productionGoal_AP2 = productionGoal_AP2;
            this.currentAmount_AP1 = currentAmount_AP1;
            this.currentAmount_AP2 = currentAmount_AP2;

            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
