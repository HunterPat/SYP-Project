﻿using FrontEnd_2.DataSimulation;
using FrontEnd_2.SharedServices;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace FrontEnd_2.ViewModels
{
    class OverviewViewModel : INotifyPropertyChanged
    {
        private readonly DispatcherTimer _timer;
        private DataService _dataService = new DataService();

        private ISharedService requiredAmount;

        public int RequiredAmount
        {
            get { return requiredAmount.RequiredAmount; }
            set
            {
                if (requiredAmount.RequiredAmount != value)
                {
                    requiredAmount.RequiredAmount = value;
                    OnPropertyChanged(nameof(RequiredAmount));
                    
                }
            }
        }


        private int currentAmount;

        public int CurrentAmount
        {
            get { return currentAmount; }
            set
            {
                if (currentAmount != value)
                {
                    currentAmount = value;
                    OnPropertyChanged(nameof(CurrentAmount));
                    OnPropertyChanged(nameof(Progress));
                }
            }
        }

        private int _progress;
        public int Progress
        {
            get { return CalculateProgress(CurrentAmount, RequiredAmount); }
            set
            {
                if (_progress != value)
                {
                    _progress = value;
                    OnPropertyChanged(nameof(Progress));
                }
            }
        }

        public OverviewViewModel(ISharedService requiredAmount, int currentAmount)
        {
            this.requiredAmount = requiredAmount;
            CurrentAmount = currentAmount;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(2); // Set the interval to 5 seconds
            _timer.Tick += Timer_Tick;

            // Start the timer
            _timer.Start();
        }

        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private int CalculateProgress(int currentAmount, int maxRequiredAmount)
        {
            if (maxRequiredAmount == 0)
            {
                throw new ArgumentException("maxRequiredAmount darf nicht 0 sein.", nameof(maxRequiredAmount));
            }

            double progressPercentage = (double)currentAmount / maxRequiredAmount * 100;

            // Du kannst das Ergebnis runden, wenn du nur ganze Prozent möchtest.
            // int roundedPercentage = (int)Math.Round(progressPercentage);

            return (int)progressPercentage;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            UpdateData();
        }

        private async void UpdateData()
        {
            int newData = await GetCurrentAmountFromApi();
            CurrentAmount = newData;
        }

        private async Task<int> GetCurrentAmountFromApi()
        {
            string baseUrl = "http://192.168.139.120";
            string endPoint = "/1/TAA1";

            var client = new RestClient(baseUrl);
            var request = new RestRequest(endPoint, Method.Get);

            try
            {
                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    // Parse the response content and return the result
                    int currentAmount = int.Parse(response.Content);
                    return currentAmount;
                }
                else
                {
                    // Handle the error
                    Console.WriteLine($"Error: {response.ErrorMessage}");
                    return -1; // or throw an exception or handle the error accordingly
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., network issues
                Console.WriteLine($"Exception: {ex.Message}");
                return -1; // or throw an exception or handle the error accordingly
            }
        }
    }
}
