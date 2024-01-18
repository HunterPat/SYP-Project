using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace FrontEnd_2.SharedServices
{
    class SharedRequiredAmount : ISharedService
    {
        private int requiredAmount;
        public int RequiredAmount
        {
            get { return requiredAmount; }
            set
            {
                if (requiredAmount != value)
                {
                    requiredAmount = value;
                    OnPropertyChanged(nameof(RequiredAmount));
                }
            }
        }

        public SharedRequiredAmount(int requiredAmount)
        {
            RequiredAmount = requiredAmount;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
