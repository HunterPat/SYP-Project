using ProdVisAdminFrontend.SharedServices;
using ProdVisAdminFrontend.ViewModels;
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

namespace ProdVisAdminFrontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OverviewViewModel overviewViewModel;
        private SettingsViewModel settingsViewModel;
        private ISharedService productionGoal_AP1;
        private ISharedService productionGoal_AP2;
        private ISharedService currentAmount_AP1;
        private ISharedService currentAmount_AP2;

        public MainWindow()
        {
            InitializeComponent();
            productionGoal_AP1 = new IntegerSharedService(0);
            productionGoal_AP2 = new IntegerSharedService(0);
            currentAmount_AP1 = new IntegerSharedService(0);
            currentAmount_AP2 = new IntegerSharedService(0);
            overviewViewModel = new OverviewViewModel(productionGoal_AP1, productionGoal_AP2, currentAmount_AP1, currentAmount_AP2);
            settingsViewModel = new SettingsViewModel(productionGoal_AP1, productionGoal_AP2, currentAmount_AP1, currentAmount_AP2);

            DataContext = overviewViewModel;
        }

        private void Overview_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = overviewViewModel;
        }

        private void Settings_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = settingsViewModel;
        }
    }
}