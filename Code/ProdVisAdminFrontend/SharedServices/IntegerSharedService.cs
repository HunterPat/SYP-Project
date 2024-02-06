using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdVisAdminFrontend.SharedServices
{
    class IntegerSharedService : ISharedService, INotifyPropertyChanged
    {
        private int _value;

        public int Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        public IntegerSharedService(int value)
        {
            Value = value;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

    }
}
