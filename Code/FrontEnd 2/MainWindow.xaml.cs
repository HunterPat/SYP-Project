using FrontEnd_2.SharedServices;
using FrontEnd_2.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrontEnd_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ISharedService requiredAmount;
        private OverviewViewModel overviewViewModel;
        private SettingsViewModel settingsViewModel;

        public MainWindow()
        {
            InitializeComponent();
            requiredAmount = new SharedRequiredAmount(500);
            overviewViewModel = new OverviewViewModel(requiredAmount, 0);
            settingsViewModel = new SettingsViewModel(requiredAmount);

            DataContext = overviewViewModel;
            
        }

        private void SettingsButton_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = settingsViewModel;
        }

        private void OverviewButtonClicked(object sender, RoutedEventArgs e)
        {
            DataContext = overviewViewModel;
        }
    }
}