using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdVisAdminFrontend.SharedServices
{
    internal class SharedProductionGoal : ISharedService, INotifyPropertyChanged
    {
        private int productionGoal;

        public int ProductionGoal
        {
            get { return productionGoal; }
            set
            {
                if (productionGoal != value)
                {
                    productionGoal = value;
                    OnPropertyChanged(nameof(productionGoal));
                }
            }
        }

        public SharedProductionGoal(int productionGoal)
        {
            ProductionGoal = productionGoal;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
