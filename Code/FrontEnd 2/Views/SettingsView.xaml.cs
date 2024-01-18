using FrontEnd_2.ViewModels;
using System;
using System.Collections.Generic;
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

namespace FrontEnd_2.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private void ApplyButton_Clicked(object sender, RoutedEventArgs e)
        {
            string requiredAmount = requiredAmountTextBox.Text;
            var viewModel = DataContext as SettingsViewModel;
            if (int.TryParse(requiredAmount, out int newValue))
            {
                // Parsing succeeded, newValue contains the parsed integer value
                // Now you can use newValue as needed
                viewModel.RequiredAmount = newValue;
            }
            else
            {
                // Parsing failed, handle the case where the input is not a valid integer
                MessageBox.Show("Die Eingabe muss eine ganze Zahl sein!", "Achtung!", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
    }
}
