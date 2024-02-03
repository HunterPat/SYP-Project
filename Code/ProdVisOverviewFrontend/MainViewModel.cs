using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdVisOverviewFrontend
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private string _dateText;

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

        private int productionGoal_1;

        public int ProductionGoal_1
        {
            get { return productionGoal_1; }
            set
            {
                if (productionGoal_1 != value)
                {
                    productionGoal_1 = value;
                    OnPropertyChanged(nameof(ProductionGoal_1));
                }
            }
        }

        private int currentAmount_1;

        public int CurrentAmount_1
        {
            get { return currentAmount_1; }
            set
            {
                if (currentAmount_1 != value)
                {
                    currentAmount_1 = value;
                    OnPropertyChanged(nameof(CurrentAmount_1));
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
