using Org.OpenAPITools.Api;
using ProdVisOverviewFrontend.Values;
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
        private DispatcherTimer updateTimer;
        private string baseUrl = StaticValues.BaseUrl;
        private APIApi api;
        public MainWindow()
        {
            InitializeComponent();
            overviewViewModel = new OverviewViewModel();
            detailedOverviewViewModel = new DetailedOverviewViewModel();
            DataContext = overviewViewModel;
            api = new APIApi(baseUrl);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var interval = api.TimeIntervalGet();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(interval);
            timer.Tick += Timer_Tick;
            timer.Start();
            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromMilliseconds(200);
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }

        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            var interval = api.TimeIntervalGet();
            if (timer.Interval.TotalSeconds == interval) return;
            timer.Interval = TimeSpan.FromSeconds(interval);
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