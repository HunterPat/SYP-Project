using Org.OpenAPITools.Api;
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

namespace ProdVisAdminFrontend.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        private const string baseUrl = "http://localhost:5501";
        private SettingsViewModel viewModel;
        
        public SettingsView()
        {
            InitializeComponent();
            

        }



        private void ConfirmGoal_Clicked(object sender, RoutedEventArgs e)
        {
            var api = new APIApi(baseUrl);
            var productionGoal = Int32.Parse(txtProductionGoal.Text);
            var response = api.GesamttubenanzZielPost(productionGoal);
            viewModel.ProductionGoal_AP1 = productionGoal;
            viewModel.ProductionGoal_AP2 = productionGoal;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel = DataContext as SettingsViewModel;
        }
    }
}
