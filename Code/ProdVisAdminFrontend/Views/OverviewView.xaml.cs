﻿using Org.OpenAPITools.Api;
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
        private const string baseUrl = "http://localhost:5501";
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

        private int CalculatePercentage(int numerator, int denominator)
        {
            // Check if the denominator is not zero to avoid division by zero
            if (denominator != 0)
            {
                // Calculate the percentage and return the result
                double result = ((double)numerator / denominator) * 100;
                return (int)result;
            }
            return 0;
        }

        public void UpdateAllValues()
        {
            if (api == null) return;
            viewModel.ProductionGoal_AP1 = api.GesamttubenanzZielGet();
            viewModel.ProductionGoal_AP2 = api.GesamttubenanzZielGet();
            viewModel.CurrentAmount_AP1 = (int)api.GesamttubenanzServer1Get();
            viewModel.CurrentAmount_AP2 = (int)api.GesamttubenanzServer2Get();
            viewModel.Progress_AP1 = CalculatePercentage(viewModel.CurrentAmount_AP1, viewModel.ProductionGoal_AP1);
            viewModel.Progress_AP2 = CalculatePercentage(viewModel.CurrentAmount_AP2, viewModel.ProductionGoal_AP2);
        }
    }
}