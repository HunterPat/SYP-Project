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
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            string text = txtProductionGoal.Text;
            if (int.TryParse(text, out int result))
            {
                // The text represents a valid integer, and the parsed value is in 'result'

                if (result % 4 == 0)
                {
                    //valid
                    var alert = passwordAlert.Child as PasswordAlert;
                    alert.CloseButtonClicked -= PasswordAlertForReset_CloseButtonClicked;
                    alert.ConfirmButtonClicked -= PasswordAlertForReset_ConfirmButtonClicked;
                    alert.CloseButtonClicked -= PasswordAlert_CloseButtonClicked;
                    alert.ConfirmButtonClicked -= PasswordAlert_ConfirmButtonClicked;
                    OpenPasswordPopup();
                }
                else
                {
                    OpenInvalidNumberPopup();
                }
                
            }
            else
            {
                // The text does not represent a valid integer
                OpenNotANumberPopup();
            }


            
            //var api = new APIApi(baseUrl);
            //var productionGoal = Int32.Parse(txtProductionGoal.Text);
            //var response = api.GesamttubenanzZielPost(productionGoal);
            //UpdateAllValues();
        }

        private void OpenInvalidNumberPopup()
        {
            var customAlert = resetAlert.Child as CustomAlert;
            customAlert.Message = "Falsche Eingabe!";
            customAlert.Details = "Hier kann nur eine durch 4 teilbare Zahl eingegeben werden!";

            customAlert.CancelButtonVisibility = Visibility.Hidden;
            customAlert.ConfirmButtonVisibility = Visibility.Visible;
            customAlert.ConfirmButtonClicked += AlertPopupClose_Clicked;
            resetAlert.IsOpen = true;
        }

        private void OpenNotANumberPopup()
        {
            var customAlert = resetAlert.Child as CustomAlert;
            customAlert.Message = "Falsche Eingabe!";
            customAlert.Details = "Hier kann nur eine ganze Zahl eingegeben werden!";

            customAlert.CancelButtonVisibility = Visibility.Hidden;
            customAlert.ConfirmButtonVisibility = Visibility.Visible;
            customAlert.ConfirmButtonClicked += AlertPopupClose_Clicked;
            resetAlert.IsOpen = true;
        }

        private void AlertPopupClose_Clicked(object? sender, EventArgs e)
        {
            
            var customAlert = resetAlert.Child as CustomAlert;
            customAlert.ConfirmButtonClicked -= AlertPopupClose_Clicked;
            resetAlert.IsOpen = false;

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

        private void OpenPasswordPopupForReset()
        {
            var alert = passwordAlert.Child as PasswordAlert;
            alert.CloseButtonClicked += PasswordAlertForReset_CloseButtonClicked;
            alert.ConfirmButtonClicked += PasswordAlertForReset_ConfirmButtonClicked;
            alert.WrongPasswordVisibility = Visibility.Hidden;
            alert.Message = "Passwort benötigt!";

            passwordAlert.IsOpen = true;
            alert.Visibility = Visibility.Visible;
        }

        private void PasswordAlertForReset_ConfirmButtonClicked(object? sender, EventArgs e)
        {
            var alert = passwordAlert.Child as PasswordAlert;

            //set passowrd correct
            var passwordCorrect = api.PasswordCheckGet(alert.Password);
            if (passwordCorrect)
            {
                alert.Visibility = Visibility.Hidden;
                //var productionGoal = int.Parse(txtProductionGoal.Text);
                ShowResetAlert();
                UpdateAllValues();
                //   txtProductionGoal.Text = "";
                return;
            }
            else
            {
                alert.WrongPasswordVisibility = Visibility.Visible;
            }
        }

        private void PasswordAlertForReset_CloseButtonClicked(object? sender, EventArgs e)
        {
            var alert = passwordAlert.Child as PasswordAlert;
            passwordAlert.IsOpen = false;
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
                var response = api.GesamttubenanzZielPut(productionGoal);
                if (response)
                {
                    viewModel.ProductionGoal_AP1 = productionGoal / 2;
                    viewModel.ProductionGoal_AP2 = productionGoal / 2;
                }
                //UpdateAllValues();
             //   txtProductionGoal.Text = "";
                //return;
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
            alert.CloseButtonClicked -= PasswordAlertForReset_CloseButtonClicked;
            alert.ConfirmButtonClicked -= PasswordAlertForReset_ConfirmButtonClicked;
            alert.CloseButtonClicked -= PasswordAlert_CloseButtonClicked;
            alert.ConfirmButtonClicked -= PasswordAlert_ConfirmButtonClicked;
            //UpdateAllValues();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {


            viewModel = DataContext as SettingsViewModel;
            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromMilliseconds(1000);
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
           
            viewModel.ProductionGoal_AP1 = api.GesamttubenanzZielMachinePairsGet();
            viewModel.ProductionGoal_AP2 = api.GesamttubenanzZielMachinePairsGet();
            viewModel.CurrentAmount_AP1 =  api.GesamttubenAnzVisualServer1Get();
            viewModel.CurrentAmount_AP2 =  api.GesamttubenAnzVisualServer2Get();
            var interval = api.TimeIntervalGet().ToString();
            foreach (ComboBoxItem item in cboInterval.Items)
            {
                if (item.Tag != null && item.Tag.ToString() == interval)
                {
                    cboInterval.SelectedItem = item;
                }
            }
        }

        private void Resest_Clicked(object sender, RoutedEventArgs e)
        {
            var alert = passwordAlert.Child as PasswordAlert;
            //alert.CloseButtonClicked += PasswordAlert_CloseButtonClicked;
            //alert.ConfirmButtonClicked += PasswordAlert_ConfirmButtonClicked;

            OpenPasswordPopupForReset();
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

        private void ShowResetAlert()
        {
            var customAlert = resetAlert.Child as CustomAlert;
            customAlert.Message = "Bitte warten!";
            customAlert.Details = "Die Tubenanzahl wird zurückgesetzt!";

            customAlert.CancelButtonVisibility = Visibility.Visible;
            customAlert.ConfirmButtonVisibility = Visibility.Visible;
            resetAlert.IsOpen = true;
            HideResetAlert(true);
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
                api.ResetBitServer1Post();
                api.ResetBitServer2Post();

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

        private void Substract_Clicked(object sender, RoutedEventArgs e)
        {
            var window = new Window();
            var substractTubes = new SubstractTubesView();
            window.Content = substractTubes;
            window.Width = 1000;
            window.Height = 600;
            window.Show();
            window.Title = "Tuben entfernen";
        }

        private void Interval_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (api == null) return;
            if (cboInterval.SelectedItem == null) return;
            var selectedItem = cboInterval.SelectedItem as ComboBoxItem;
            var interval = int.Parse(selectedItem.Tag.ToString());
            api.TimeIntervalPut(interval);
        }
    }
}
