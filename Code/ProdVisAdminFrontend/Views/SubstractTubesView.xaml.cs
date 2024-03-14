using Org.OpenAPITools.Api;
using ProdVisAdminFrontend.Values;
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

namespace ProdVisAdminFrontend.Views
{
    /// <summary>
    /// Interaction logic for SubstractTubesView.xaml
    /// </summary>
    public partial class SubstractTubesView : UserControl
    {
        private string baseUrl = StaticValues.BaseUrl;
        private APIApi api;
        public SubstractTubesView()
        {
            InitializeComponent();
        }

        private void Send_Clicked(object sender, RoutedEventArgs e)
        {
            var pwAlert = passwordAlert.Child as PasswordAlert;

            passwordAlert.IsOpen = true;
            pwAlert.CloseButtonClicked += PasswordAlert_CloseButtonClicked;
            pwAlert.ConfirmButtonClicked += PasswordAlert_ConfirmButtonClicked;

            pwAlert.WrongPasswordVisibility = Visibility.Hidden;
            pwAlert.Message = "Passwort benötigt!";



        }

        private void PasswordAlert_ConfirmButtonClicked(object? sender, EventArgs e)
        {
            var alert = passwordAlert.Child as PasswordAlert;
            var passwordCorrect = api.PasswordCheckGet(alert.Password);
            if (passwordCorrect)
            {
                // Assuming txtBadTubes_A1, txtBadTubes_A2, txtBadTubes_A3, and txtBadTubes_A4 are TextBox controls

                // Define an array to hold the TextBoxes
                TextBox[] textBoxes = { txtBadTubes_A1, txtBadTubes_A2, txtBadTubes_A3, txtBadTubes_A4 };

                // Define a list to hold valid values
                List<int> validValues = new List<int>();
                // Define a list to hold invalid values
                List<int> invalidValues = new List<int>();

                // Loop through each TextBox and check its content
                foreach (TextBox textBox in textBoxes)
                {
                    int value;
                    // Try parsing the text as an integer
                    if (int.TryParse(textBox.Text, out value) && value > 0)
                    {
                        // Valid integer and positive value
                        validValues.Add(value); // Add valid value to the list
                    }
                    else
                    {
                        // Invalid integer or non-positive value
                        invalidValues.Add(value); // Add invalid value to the list
                    }
                }

                // Check if any invalid values were found
                if (invalidValues.Count == 0)
                {
                    api.KaputteTubenAnzTAA1Put(validValues[0]);
                    api.KaputteTubenAnzTAA2Put(validValues[1]);
                    api.KaputteTubenAnzTAA3Put(validValues[2]);
                    api.KaputteTubenAnzTAA4Put(validValues[3]);
                    UpdateAllValues();
                    
                }
                
                else
                {
                    // At least one value is invalid
                    // Handle the invalid values here
                    foreach (int invalidValue in invalidValues)
                    {
                        Console.WriteLine("Invalid value: " + invalidValue);
                    }
                }
                HidePasswordAlert();
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

            api = new APIApi(baseUrl);
            UpdateAllValues();
        }

        private void UpdateAllValues()
        {
            lastAmount_A1.Content = api.KaputteTubenAnzTAA1Get();
            lastAmount_A2.Content = api.KaputteTubenAnzTAA2Get();
            lastAmount_A3.Content = api.KaputteTubenAnzTAA3Get();
            lastAmount_A4.Content = api.KaputteTubenAnzTAA4Get();
        }
    }
}
