using ProdVisAdminFrontend.SharedServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdVisAdminFrontend.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
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

        public SettingsViewModel(ISharedService productionGoal_1, ISharedService productionGoal_2)
        {
            this.productionGoal_1 = productionGoal_1;
            this.productionGoal_2 = productionGoal_2;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
