using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd_2.DataSimulation
{
    internal class DataService
    {
        private int _currentAmount;

        public async Task<int> GetDataFromApi()
        {
            // Simulate a delay between 0.5 and 2 seconds
            int delayMilliseconds = new Random().Next(500, 2001);
            await Task.Delay(delayMilliseconds);

            // Increase _currentAmount by 1
            _currentAmount += 1;

            return _currentAmount;
        }
    }
}
