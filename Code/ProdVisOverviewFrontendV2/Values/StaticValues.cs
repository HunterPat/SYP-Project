using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace ProdVisOverviewFrontend.Values
{
    internal class StaticValues
    {
        public static string BaseUrl { get; set; } = "http://localhost:5000";
        public static int ThreadCallInterval { get; set; } = 3000;

    }
}
