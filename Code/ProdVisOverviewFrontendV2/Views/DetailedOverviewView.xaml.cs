using Org.OpenAPITools.Api;
using ProdVisOverviewFrontend.Values;
using ProdVisOverviewFrontendV2.ViewModels;
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
using System.Windows.Threading;

namespace ProdVisOverviewFrontendV2.Views
{
    /// <summary>
    /// Interaction logic for DetailedOverviewView.xaml
    /// </summary>
    public partial class DetailedOverviewView : UserControl
    {
        private string baseUrl = StaticValues.BaseUrl;

        private DispatcherTimer updateTimer;
        private APIApi api;
        private DetailedOverviewViewModel viewModel;
        public DetailedOverviewView()
        {
            InitializeComponent();
        }

        

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            UpdateAllValues();
        }

        public async void UpdateAllValues()
        {
            if (api == null) return;
            viewModel.Progress_A1 = await api.GesamttubenAnzVisualTAA1PercentGetAsync();
            viewModel.Progress_A2 = await api.GesamttubenAnzVisualTAA2PercentGetAsync();
            viewModel.Progress_A3 = await api.GesamttubenAnzVisualTAA3PercentGetAsync();
            viewModel.Progress_A4 = await api.GesamttubenAnzVisualTAA4PercentGetAsync();
            viewModel.ProductionGoal_A1 = await api.GesamttubenanzZiel4MachinesGetAsync();
            viewModel.ProductionGoal_A2 = await api.GesamttubenanzZiel4MachinesGetAsync();
            viewModel.ProductionGoal_A3 = await api.GesamttubenanzZiel4MachinesGetAsync();
            viewModel.ProductionGoal_A4 = await api.GesamttubenanzZiel4MachinesGetAsync();
            viewModel.CurrentAmount_A1 = await api.GesamttubenAnzVisualMachine1ServerIDGetAsync(1);
            viewModel.CurrentAmount_A2 = await api.GesamttubenAnzVisualMachine2ServerIDGetAsync(1);
            viewModel.CurrentAmount_A3 = await api.GesamttubenAnzVisualMachine1ServerIDGetAsync(2);
            viewModel.CurrentAmount_A4 = await api.GesamttubenAnzVisualMachine2ServerIDGetAsync(2);
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            viewModel = DataContext as DetailedOverviewViewModel;
            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromMilliseconds(1000);
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
            api = new APIApi(baseUrl);
            UpdateAllValues();
        }
    }
}
