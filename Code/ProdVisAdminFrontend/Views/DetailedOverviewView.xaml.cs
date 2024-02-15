﻿using Org.OpenAPITools.Api;
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

        private void Switch_Clicked(object sender, RoutedEventArgs e)
        {
            viewModel.OnSwitchUserControlRequested();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel = DataContext as DetailedOverviewViewModel;
            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromMilliseconds(5000);
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
            api = new APIApi(baseUrl);
            UpdateAllValues();
        }

        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            UpdateAllValues();
        }

        public async void UpdateAllValues()
        {
            if (api == null) return;
            viewModel.Progress_A1 = await api.GesamttubenAnzTAA1PercentGetAsync();
            viewModel.Progress_A2 = await api.GesamttubenAnzTAA2PercentGetAsync();
            viewModel.Progress_A3 = await api.GesamttubenAnzTAA3PercentGetAsync();
            viewModel.Progress_A4 = await api.GesamttubenAnzTAA4PercentGetAsync();
            viewModel.ProductionGoal_A1 = await api.GesamttubenanzZiel4MachinesGetAsync();
            viewModel.ProductionGoal_A2 = await api.GesamttubenanzZiel4MachinesGetAsync();
            viewModel.ProductionGoal_A3 = await api.GesamttubenanzZiel4MachinesGetAsync();
            viewModel.ProductionGoal_A4 = await api.GesamttubenanzZiel4MachinesGetAsync();
            viewModel.CurrentAmount_A1 = await api.GesamttubenAnzMachine1ServerIDGetAsync(1);
            viewModel.CurrentAmount_A2 = await api.GesamttubenAnzMachine2ServerIDGetAsync(1);
            viewModel.CurrentAmount_A3 = await api.GesamttubenAnzMachine1ServerIDGetAsync(2);
            viewModel.CurrentAmount_A4 = await api.GesamttubenAnzMachine2ServerIDGetAsync(2);
        }
    }
}
