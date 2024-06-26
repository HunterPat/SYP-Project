﻿using Org.OpenAPITools.Api;
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

        public void UpdateAllValues()
        {
            if (api == null) return;
            viewModel.Progress_A1 =  api.GesamttubenAnzVisualTAA1PercentGet();
            viewModel.Progress_A2 =  api.GesamttubenAnzVisualTAA2PercentGet();
            viewModel.Progress_A3 =  api.GesamttubenAnzVisualTAA3PercentGet();
            viewModel.Progress_A4 =  api.GesamttubenAnzVisualTAA4PercentGet();
            viewModel.ProductionGoal_A1 =  api.GesamttubenanzZiel4MachinesGet();
            viewModel.ProductionGoal_A2 =  api.GesamttubenanzZiel4MachinesGet();
            viewModel.ProductionGoal_A3 =  api.GesamttubenanzZiel4MachinesGet();
            viewModel.ProductionGoal_A4 =  api.GesamttubenanzZiel4MachinesGet();
            viewModel.CurrentAmount_A1 =  api.GesamttubenAnzVisualMachine1ServerIDGet(1);
            viewModel.CurrentAmount_A2 =  api.GesamttubenAnzVisualMachine2ServerIDGet(1);
            viewModel.CurrentAmount_A3 =  api.GesamttubenAnzVisualMachine1ServerIDGet(2);
            viewModel.CurrentAmount_A4 =  api.GesamttubenAnzVisualMachine2ServerIDGet(2);
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            api = new APIApi(baseUrl);
            viewModel = DataContext as DetailedOverviewViewModel;
            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromMilliseconds(StaticValues.ThreadCallInterval);
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
            UpdateAllValues();
        }
    }
}
