using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdVisOverviewFrontendV2.ViewModels
{
    class DetailedOverviewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int currentAmount_A1;

        public int CurrentAmount_A1
        {
            get { return currentAmount_A1; }
            set
            {
                if (currentAmount_A1 != value)
                {
                    currentAmount_A1 = value;
                    OnPropertyChanged(nameof(CurrentAmount_A1));
                }
            }
        }

        private int currentAmount_A2;

        public int CurrentAmount_A2
        {
            get { return currentAmount_A2; }
            set
            {
                if (currentAmount_A2 != value)
                {
                    currentAmount_A2 = value;
                    OnPropertyChanged(nameof(CurrentAmount_A2));
                }
            }
        }

        private int currentAmount_A3;

        public int CurrentAmount_A3
        {
            get { return currentAmount_A3; }
            set
            {
                if (currentAmount_A3 != value)
                {
                    currentAmount_A3 = value;
                    OnPropertyChanged(nameof(CurrentAmount_A3));
                }
            }
        }

        private int currentAmount_A4;

        public int CurrentAmount_A4
        {
            get { return currentAmount_A4; }
            set
            {
                if (currentAmount_A4 != value)
                {
                    currentAmount_A4 = value;
                    OnPropertyChanged(nameof(CurrentAmount_A4));
                }
            }
        }

        private int productionGoal_A1;

        public int ProductionGoal_A1
        {
            get { return productionGoal_A1; }
            set
            {
                if (productionGoal_A1 != value)
                {
                    productionGoal_A1 = value;
                    OnPropertyChanged(nameof(ProductionGoal_A1));
                }
            }
        }

        private int productionGoal_A2;

        public int ProductionGoal_A2
        {
            get { return productionGoal_A2; }
            set
            {
                if (productionGoal_A2 != value)
                {
                    productionGoal_A2 = value;
                    OnPropertyChanged(nameof(ProductionGoal_A2));
                }
            }
        }

        private int productionGoal_A3;

        public int ProductionGoal_A3
        {
            get { return productionGoal_A3; }
            set
            {
                if (productionGoal_A3 != value)
                {
                    productionGoal_A3 = value;
                    OnPropertyChanged(nameof(ProductionGoal_A3));
                }
            }
        }

        private int productionGoal_A4;

        public int ProductionGoal_A4
        {
            get { return productionGoal_A4; }
            set
            {
                if (productionGoal_A4 != value)
                {
                    productionGoal_A4 = value;
                    OnPropertyChanged(nameof(ProductionGoal_A4));
                }
            }
        }

        private int progress_A1;

        public int Progress_A1
        {
            get { return progress_A1; }
            set
            {
                if (progress_A1 != value)
                {
                    progress_A1 = value;
                    OnPropertyChanged(nameof(Progress_A1));
                }
            }
        }

        private int progress_A2;

        public int Progress_A2
        {
            get { return progress_A2; }
            set
            {
                if (progress_A2 != value)
                {
                    progress_A2 = value;
                    OnPropertyChanged(nameof(Progress_A2));
                }
            }
        }

        private int progress_A3;

        public int Progress_A3
        {
            get { return progress_A3; }
            set
            {
                if (progress_A3 != value)
                {
                    progress_A3 = value;
                    OnPropertyChanged(nameof(Progress_A3));
                }
            }
        }

        private int progress_A4;

        public int Progress_A4
        {
            get { return progress_A4; }
            set
            {
                if (progress_A4 != value)
                {
                    progress_A4 = value;
                    OnPropertyChanged(nameof(Progress_A4));
                }
            }
        }
    }
}
