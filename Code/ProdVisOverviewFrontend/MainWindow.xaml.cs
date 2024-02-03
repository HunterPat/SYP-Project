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

namespace ProdVisOverviewFrontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;

            // Set an initial value for DateText
            viewModel.DateText = "Initial Date";

            // Set up the timer to update DateText every second

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();

            viewModel.ProductionGoal_1 = 1000;
            viewModel.CurrentAmount_1 = 345;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update DateText with the current DateTime.Now value
            viewModel.DateText = DateTime.Now.ToString("dddd, dd.MM.yyyy - HH:mm:ss");
        }
    }
}