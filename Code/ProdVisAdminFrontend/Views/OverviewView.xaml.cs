using Org.OpenAPITools.Api;
using ProdVisAdminFrontend.Values;
using ProdVisAdminFrontend.ViewModels;
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

namespace ProdVisAdminFrontend.Views
{
    /// <summary>
    /// Interaction logic for OverviewView.xaml
    /// </summary>
    public partial class OverviewView : UserControl
    {
        
        private string baseUrl = StaticValues.BaseUrl;
        private OverviewViewModel viewModel;
        private DispatcherTimer updateTimer;
        private APIApi api;
        public OverviewView()
        {
            InitializeComponent();
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel = DataContext as OverviewViewModel;
            
            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromMilliseconds(StaticValues.ThreadTimeInterval);
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
            api = new APIApi(baseUrl);
            UpdateAllValues();
        }

        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            UpdateAllValues();
        }

        public void UpdateAllValues()
        {
            if (api == null) return;
            viewModel.ProductionGoal_AP1 = api.GesamttubenanzZielMachinePairsGet();
            viewModel.ProductionGoal_AP2 =  api.GesamttubenanzZielMachinePairsGet();
            viewModel.CurrentAmount_AP1 =  api.GesamttubenAnzVisualServer1Get(); 
            viewModel.CurrentAmount_AP2 =  api.GesamttubenAnzVisualServer2Get(); 
            viewModel.Progress_AP1 = api.GesamttubenAnzVisualServer1PercentGet(); 
            viewModel.Progress_AP2 =  api.GesamttubenAnzVisualServer2PercentGet(); 
        }

        //public event EventHandler SwitchUserControlRequested;
        private void Switch_Clicked(object sender, RoutedEventArgs e)
        {
            viewModel.OnSwitchUserControlRequested();
        }


    }
}
