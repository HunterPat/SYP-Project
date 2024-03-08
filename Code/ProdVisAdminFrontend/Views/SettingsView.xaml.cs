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
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        private string baseUrl = StaticValues.BaseUrl;
        private SettingsViewModel viewModel;
        private DispatcherTimer updateTimer;
        private APIApi api;

        public SettingsView()
        {
            InitializeComponent();


        }



        private void ConfirmGoal_Clicked(object sender, RoutedEventArgs e)
        {
            OpenPasswordPopup();
            //var api = new APIApi(baseUrl);
            //var productionGoal = Int32.Parse(txtProductionGoal.Text);
            //var response = api.GesamttubenanzZielPost(productionGoal);
            //UpdateAllValues();
        }

        private void OpenPasswordPopup()
        {
            var alert = passwordAlert.Child as PasswordAlert;
            alert.CloseButtonClicked += PasswordAlert_CloseButtonClicked;
            alert.ConfirmButtonClicked += PasswordAlert_ConfirmButtonClicked;
            alert.WrongPasswordVisibility = Visibility.Hidden;
            alert.Message = "Passwort benötigt!";

            passwordAlert.IsOpen = true;
            alert.Visibility = Visibility.Visible;
        }

        

        private void PasswordAlert_ConfirmButtonClicked(object? sender, EventArgs e)
        {
            //TODO: check password with api and do the post request if correct
            var alert = passwordAlert.Child as PasswordAlert;
            
            //set passowrd correct
            var passwordCorrect = api.PasswordCheckGet(alert.Password);
            if (passwordCorrect)
            {
                alert.Visibility = Visibility.Hidden;
                var productionGoal = int.Parse(txtProductionGoal.Text);
                api.GesamttubenanzZielPut(productionGoal);
                UpdateAllValues();
             //   txtProductionGoal.Text = "";
                return;
            }
            else
            {
                alert.WrongPasswordVisibility = Visibility.Visible;
            }
        }

        private void PasswordAlert_CloseButtonClicked(object? sender, EventArgs e)
        {
            HidePasswordAlert();
        }

        private void HidePasswordAlert()
        {
            var alert = passwordAlert.Child as PasswordAlert;
            passwordAlert.IsOpen = false;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel = DataContext as SettingsViewModel;
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
            viewModel.ProductionGoal_AP1 = await api.GesamttubenanzZielMachinePairsGetAsync();
            viewModel.ProductionGoal_AP2 = await api.GesamttubenanzZielMachinePairsGetAsync();
            viewModel.CurrentAmount_AP1 = await api.GesamttubenAnzVisualServer1GetAsync();
            viewModel.CurrentAmount_AP2 = await api.GesamttubenAnzVisualServer2GetAsync();

        }

        private void Resest_Clicked(object sender, RoutedEventArgs e)
        {
            var alert = passwordAlert.Child as PasswordAlert;
            alert.CloseButtonClicked += PasswordAlert_CloseButtonClicked;
            alert.ConfirmButtonClicked += PasswordAlert_ConfirmButtonClicked;

            resetAlert.IsOpen = true;
        }

        private void CustomAlert_ConfirmButtonClicked(object? sender, EventArgs e)
        {
            HideResetAlert(true);
        }

        private void CustomAlert_CloseButtonClicked(object sender, EventArgs e)
        {
            // This method will be called when the OK button is clicked in the CustomAlert
            // Hide the custom alert
            HideResetAlert(false);
        }



        private async void HideResetAlert(bool contiueReset)
        {
            var customAlert = resetAlert.Child as CustomAlert;
            if (!contiueReset)
            {


                // Unsubscribe from the CloseButtonClicked event

            }
            else
            {



                customAlert.Message = "Bitte warten!";
                customAlert.Details = "Die Tubenanzahl wird zurückgesetzt!";
                customAlert.CancelButtonVisibility = Visibility.Hidden;
                customAlert.ConfirmButtonVisibility = Visibility.Hidden;
                await api.ResetBitServer1PostAsync();
                await api.ResetBitServer2PostAsync();

                UpdateAllValues();
                resetAlert.IsOpen = false;
            }
            resetAlert.IsOpen = false;
            customAlert.CancelButtonVisibility = Visibility.Visible;
            customAlert.ConfirmButtonVisibility = Visibility.Visible;
            customAlert.Message = "Tubenanzahl wird zurückgesetzt!";
            customAlert.Details = "Bestätigen um fortzufahren";
        }

        private void HideIntervalInfoAlert()
        {
            popupInfo.IsOpen = false;

        }

        private void IntervalInfo_Entered(object sender, MouseEventArgs e)
        {
            var alert = popupInfo.Child as CustomAlert;
            alert.Message = "Information";
            alert.Details = "Interval für den Ansichtswechsel am großen Bildschirm";
            alert.CancelButtonVisibility = Visibility.Hidden;
            alert.ConfirmButtonVisibility = Visibility.Hidden;
            popupInfo.IsOpen = true;
        }

        private void IntervalInfo_Leave(object sender, MouseEventArgs e)
        {
            HideIntervalInfoAlert();
        }
    }
}
