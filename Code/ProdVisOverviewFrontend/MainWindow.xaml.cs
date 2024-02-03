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
        private DispatcherTimer timeDisplayTimer;
        private DispatcherTimer updateTimer;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;

            // Set an initial value for DateText
            viewModel.DateText = "Initial Date";

            // Set up the timer to update DateText every second

            timeDisplayTimer = new DispatcherTimer();
            timeDisplayTimer.Interval = TimeSpan.FromMilliseconds(500);
            timeDisplayTimer.Tick += TimeTimer_Tick;
            timeDisplayTimer.Start();

            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromMilliseconds(2000);
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();

            viewModel.ProductionGoal_1 = 1000;
            viewModel.ProductionGoal_2 = 1000;
            viewModel.CurrentAmount_1 = 345;
            viewModel.CurrentAmount_2 = 234;
        }

        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            viewModel.Progress_1 = CalculatePercentage(viewModel.CurrentAmount_1, viewModel.ProductionGoal_1);
            viewModel.Progress_2 = CalculatePercentage(viewModel.CurrentAmount_2, viewModel.ProductionGoal_2);
            //int result = (int)CalculatePercentage(10, 100); = 10
            //viewModel.ProductionGoal_1 = result;
        }

        private int CalculatePercentage(int numerator, int denominator)
        {
            // Check if the denominator is not zero to avoid division by zero
            if (denominator != 0)
            {
                // Calculate the percentage and return the result
                double result = ((double)numerator / denominator) * 100;
                return (int) result;
            }
            return 0;
        }

        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            // Update DateText with the current DateTime.Now value
            viewModel.DateText = DateTime.Now.ToString("dddd, dd.MM.yyyy - HH:mm:ss");
        }
    }
}