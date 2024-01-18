using FrontEnd_2.SharedServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd_2.ViewModels
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        private ISharedService requiredAmount;

        public int RequiredAmount
        {
            get { return requiredAmount.RequiredAmount; }
            set
            {
                if (requiredAmount.RequiredAmount != value)
                {
                    requiredAmount.RequiredAmount = value;
                    OnPropertyChanged(nameof(RequiredAmount));
                }
            }
        }

        public SettingsViewModel(ISharedService requiredAmount)
        {
            this.requiredAmount = requiredAmount;
        }

        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
