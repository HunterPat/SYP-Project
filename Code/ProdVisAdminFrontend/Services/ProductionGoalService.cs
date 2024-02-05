using Org.OpenAPITools.Api;
using ProdVisAdminFrontend.SharedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdVisAdminFrontend.Services
{
    class ProductionGoalService
    {
        private bool isRunning = true;
        private readonly ISharedService productionGoal_1;
        private readonly ISharedService productionGoal_2;
        private APIApi api;
        private const string baseUrl = "http://localhost:5501";

        public ProductionGoalService(ISharedService productionGoal_1, ISharedService productionGoal_2)
        {
            api = new APIApi(baseUrl);
            this.productionGoal_1 = productionGoal_1;
            this.productionGoal_2 = productionGoal_2;
        }

        public void Start()
        {
            Task.Run(() =>
            {
                while (isRunning)
                {
                    // Your background code
                    Thread.Sleep(1000); // Example: Sleep for 1 second
                    if (api == null) return;
                    productionGoal_1.ProductionGoal = api.GesamttubenanzZielGet();
                    productionGoal_2.ProductionGoal = api.GesamttubenanzZielGet();


                }
            });
        }

        public void Stop()
        {
            isRunning = false;
        }
    }
}
