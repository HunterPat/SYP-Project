using API.Services;
using Microsoft.AspNetCore.Mvc;
using OPC_UA_Client;
using System.Timers;
namespace API.Maps
{
    public static class MachineMaps
    {
        public static int gesamtTubenAnzZiel = 1;
        public static long timeToNextResetInSeconds = 0;
        public static IEndpointRouteBuilder MapMachineData(this IEndpointRouteBuilder routes)
        {
            var gesamtTubenAnzDataGroup = routes.MapGroup("/gesamttubenanz");
            var gesamtTubenAnzZielGroup = routes.MapGroup("/gesamttubenanzZiel");
            var resetBitGroup = routes.MapGroup("/resetBit");
            //check DateTime = 12:00 to resetAll
            var secondsInADay = 86400;
            timeToNextResetInSeconds = secondsInADay - (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second);
            //   MachineServices.CreateDatabase(); If not created
            System.Timers.Timer timer = new System.Timers.Timer(3000); // timeToNextResetInSeconds*1000
            timer.Elapsed += Timer_Elapsed!;
            timer.Start();

            MyOPCServer1 server1 = new MyOPCServer1(MachineServices.server1URL);
            MyOPCServer2 server2 = new MyOPCServer2(MachineServices.server2URL);
            server1.StartServer();
            server2.StartServer();

            gesamtTubenAnzZielGroup.MapGet("", () => gesamtTubenAnzZiel);
            gesamtTubenAnzZielGroup.MapPost("", ([FromBody] int value) =>
            {
                gesamtTubenAnzZiel = value;
                return gesamtTubenAnzZiel;
            });

            resetBitGroup.MapPost("/Server1", (MachineServices service) => service.PostResetbitServer1());
            resetBitGroup.MapPost("/Server2", (MachineServices service) => service.PostResetbitMachine2());
            //gesamttubenAnzahl GET-Requests
            gesamtTubenAnzDataGroup.MapGet("/Server1", (MachineServices service) => service.GetGesamttubenanzahlServer1());
            gesamtTubenAnzDataGroup.MapGet("/Server2", (MachineServices service) => service.GetGesamttubenanzahlServer2());
            gesamtTubenAnzDataGroup.MapGet("/Machine1/{serverID}", (int serverID, MachineServices service) => service.GetGesamttubenanzahlMachine1(serverID));
            gesamtTubenAnzDataGroup.MapGet("/Machine2/{serverID}", (int serverID, MachineServices service) => service.GetGesamttubenanzahlMachine2(serverID));

            return routes;
        }
        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //   if (DateTime.Now.TimeOfDay.Hours == 12 && DateTime.Now.TimeOfDay.Minutes == 0)
            MachineServices services = new MachineServices();
            services.SaveValueIntoDB(1, services.GetGesamttubenanzahlServer1(), gesamtTubenAnzZiel, DateTime.Now.ToString("dd.MMMM.yyyy hh:mm "));
            services.SaveValueIntoDB(2, services.GetGesamttubenanzahlServer2(), gesamtTubenAnzZiel, DateTime.Now.ToString("dd.MMMM.yyyy hh:mm "));
        }
    }
}
