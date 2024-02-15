using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProdVisAdminFrontend.Views
{
    /// <summary>
    /// Interaction logic for PasswordAlert.xaml
    /// </summary>
    public partial class PasswordAlert : UserControl, INotifyPropertyChanged
    {
        public PasswordAlert()
        {
            InitializeComponent();
            DataContext = this;
        }

        private Visibility _cancelButtonVisibility;

        public Visibility CancelButtonVisibility
        {
            get { return _cancelButtonVisibility; }
            set
            {
                if (_cancelButtonVisibility != value)
                {
                    _cancelButtonVisibility = value;
                    OnPropertyChanged(nameof(CancelButtonVisibility));
                }
            }
        }

        private Visibility _wrongPasswordVisibility;

        public Visibility WrongPasswordVisibility
        {
            get { return _wrongPasswordVisibility; }
            set
            {
                if (_wrongPasswordVisibility != value)
                {
                    _wrongPasswordVisibility = value;
                    OnPropertyChanged(nameof(WrongPasswordVisibility));
                }
            }
        }


        private Visibility _confirmButtonVisibility;


        public Visibility ConfirmButtonVisibility
        {
            get { return _confirmButtonVisibility; }
            set
            {
                if (_confirmButtonVisibility != value)
                {
                    _confirmButtonVisibility = value;
                    OnPropertyChanged(nameof(ConfirmButtonVisibility));
                }
            }
        }



        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }

        private string _details;

        public string Details
        {
            get { return _details; }
            set
            {
                if (_details != value)
                {
                    _details = value;
                    OnPropertyChanged(nameof(Details));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public event EventHandler CloseButtonClicked;
        public event EventHandler ConfirmButtonClicked;


        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            CloseButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void Confirm_Clicked(object sender, RoutedEventArgs e)
        {
            ConfirmButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
