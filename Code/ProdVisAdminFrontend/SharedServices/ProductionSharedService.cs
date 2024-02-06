using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdVisAdminFrontend.SharedServices
{
    class ProductionSharedService : ISharedService, INotifyPropertyChanged
    {
        private int currentAmount_AP1;

        public int CurrentAmount_AP1
        {
            get { return currentAmount_AP1; }
            set
            {
                if (currentAmount_AP1 != value)
                {
                    currentAmount_AP1 = value;
                    OnPropertyChanged(nameof(currentAmount_AP1));
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
                    OnPropertyChanged(nameof(currentAmount_AP2));
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
                    OnPropertyChanged(nameof(productionGoal_AP1));
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
                    OnPropertyChanged(nameof(productionGoal_AP2));
                }
            }
        }

        public ProductionSharedService(int currentAmount, int productionGoal)
        {
            CurrentAmount_AP1 = currentAmount;
            ProductionGoal_AP1 = productionGoal;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

    }
}
