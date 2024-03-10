using ProdVisOverviewFrontendV2.ViewModels;
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
using System.Windows.Threading;


namespace ProdVisOverviewFrontendV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OverviewViewModel overviewViewModel;
        private DetailedOverviewViewModel detailedOverviewViewModel;
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            overviewViewModel = new OverviewViewModel();
            detailedOverviewViewModel = new DetailedOverviewViewModel();
            DataContext = overviewViewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += Timer_Tick;
            timer.Start();
            
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (DataContext == null) return;
            if (DataContext == overviewViewModel)
            {
                DataContext = detailedOverviewViewModel;
            }
            else
            {
                DataContext = overviewViewModel;
            }

        }
    }
}