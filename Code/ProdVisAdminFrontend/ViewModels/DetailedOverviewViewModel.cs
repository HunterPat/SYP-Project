using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdVisAdminFrontend.ViewModels
{
    class DetailedOverviewViewModel
    {
        // Event to be triggered by the button click
        public event EventHandler SwitchUserControlRequested;


        public void OnSwitchUserControlRequested()
        {
            // Raise the event
            SwitchUserControlRequested?.Invoke(this, EventArgs.Empty);
        }

        public DetailedOverviewViewModel() { }

    }
}
