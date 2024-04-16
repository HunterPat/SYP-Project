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
            string text = txtBadTubes_A1.Text;
            if (int.TryParse(text, out int result1))
            {
                // The text represents a valid integer, and the parsed value is in 'result'
                api.KaputteTubenAnzTAA1Put(result1);
            }
            text = txtBadTubes_A2.Text;
            if (int.TryParse(text, out int result2))
            {
                // The text represents a valid integer, and the parsed value is in 'result'
                api.KaputteTubenAnzTAA2Put(result2);
            }
            text = txtBadTubes_A3.Text;
            if (int.TryParse(text, out int result3))
            {
                // The text represents a valid integer, and the parsed value is in 'result'
                api.KaputteTubenAnzTAA3Put(result3);
            }
            text = txtBadTubes_A4.Text;
            if (int.TryParse(text, out int result4))
            {
                // The text represents a valid integer, and the parsed value is in 'result'
                api.KaputteTubenAnzTAA4Put(result4);
            }
            UpdateAllValues();
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
