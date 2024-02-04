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

        public MainWindow()
        {
            InitializeComponent();
            overviewViewModel = new OverviewViewModel();
            settingsViewModel = new SettingsViewModel();

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