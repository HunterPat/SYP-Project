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

        private int productionGoal_2;

        public int ProductionGoal_2
        {
            get { return productionGoal_2; }
            set
            {
                if (productionGoal_2 != value)
                {
                    productionGoal_2 = value;
                    OnPropertyChanged(nameof(ProductionGoal_2));
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

        private int currentAmount_2;

        public int CurrentAmount_2
        {
            get { return currentAmount_2; }
            set
            {
                if (currentAmount_2 != value)
                {
                    currentAmount_2 = value;
                    OnPropertyChanged(nameof(CurrentAmount_2));
                }
            }
        }

        private int progress_1;

        public int Progress_1
        {
            get { return progress_1; }
            set
            {
                if (progress_1 != value)
                {
                    progress_1 = value;
                    OnPropertyChanged(nameof(Progress_1));
                }
            }
        }

        private int progress_2;

        public int Progress_2
        {
            get { return progress_2; }
            set
            {
                if (progress_2 != value)
                {
                    progress_2 = value;
                    OnPropertyChanged(nameof(Progress_2));
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
