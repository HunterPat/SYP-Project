using API.Services;
using Microsoft.AspNetCore.Mvc;
using OPC_UA_Client;
using System.Timers;

namespace API.Maps
{
    public static class MachineMaps
    {
        public static long timeToNextResetInSeconds = 0;
        public static int gesamtTubenAnzZiel = 8000;
        private static MachineServices service = null;

        public static IEndpointRouteBuilder MapMachineData(this IEndpointRouteBuilder routes)
        {
            var gesamtTubenAnzDataGroup = routes.MapGroup("/gesamttubenAnz");
            var resetBitGroup = routes.MapGroup("/resetBit");
            var gesamtTubenAnzZielGroup = routes.MapGroup("/gesamttubenanzZiel");
            var secondsInADay = 86400;
            timeToNextResetInSeconds = secondsInADay - (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second);
            //   MachineServices.CreateDatabase(); If not created
            System.Timers.Timer timer = new System.Timers.Timer(3000); // timeToNextResetInSeconds*1000
            timer.Elapsed += Timer_Elapsed!;
            timer.Start();
            gesamtTubenAnzZielGroup.MapGet("", () => gesamtTubenAnzZiel);
            gesamtTubenAnzZielGroup.MapPost("", ([FromBody] int value) =>
            {
                gesamtTubenAnzZiel = value;
                return gesamtTubenAnzZiel;
            });
            service = new MachineServices();

            resetBitGroup.MapPost("/Machine1/resetBit", (int serverID, MachineServices service) => service.PostResetbitServer1());
            resetBitGroup.MapPost("/Machine2/resetBit", (int serverID, MachineServices service) => service.PostResetbitServer2());
            gesamtTubenAnzDataGroup.MapGet("/Server1", (MachineServices service) => service.GetGesamttubenanzahlServer1());
            gesamtTubenAnzDataGroup.MapGet("/Server2", (MachineServices service) => service.GetGesamttubenanzahlServer2());

            gesamtTubenAnzDataGroup.MapGet("/Machine1/{serverID}", (int serverID, MachineServices service) => service.GetGesamttubenanzahlMachine1(serverID));
            gesamtTubenAnzDataGroup.MapGet("/Machine2/{serverID}", (int serverID, MachineServices service) => service.GetGesamttubenanzahlMachine2(serverID));
            //in Percent
            gesamtTubenAnzDataGroup.MapGet("/Server1/Percent", () => service.GetGesamttubenanzahlPercentServer1(gesamtTubenAnzZiel));
            gesamtTubenAnzDataGroup.MapGet("/Server2/Percent", () => service.GetGesamttubenanzahlPercentServer2(gesamtTubenAnzZiel));
            gesamtTubenAnzDataGroup.MapGet("/TAA1/Percent", () => service.GetGesamttubenanzahlPercentTAA1(gesamtTubenAnzZiel));
            gesamtTubenAnzDataGroup.MapGet("/TAA2/Percent", () => service.GetGesamttubenanzahlPercentTAA2(gesamtTubenAnzZiel));
            gesamtTubenAnzDataGroup.MapGet("/TAA3/Percent", () => service.GetGesamttubenanzahlPercentTAA3(gesamtTubenAnzZiel));
            gesamtTubenAnzDataGroup.MapGet("/TAA4/Percent", () => service.GetGesamttubenanzahlPercentTAA4(gesamtTubenAnzZiel));


            gesamtTubenAnzZielGroup.MapGet("/MachinePairs", () => service.GetGesamttubenanzahlZielMachinePairs(gesamtTubenAnzZiel));
            gesamtTubenAnzZielGroup.MapGet("/4Machines", () => service.GetGesamttubenanzahlZiel4Machines(gesamtTubenAnzZiel));

            return routes;
        }
        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //   if (DateTime.Now.TimeOfDay.Hours == 12 && DateTime.Now.TimeOfDay.Minutes == 0)
            service.SaveValueIntoDB(1, service.GetGesamttubenanzahlServer1(), service.GetGesamttubenanzahlServer1(), DateTime.Now.ToString("dd.MMMM.yyyy hh:mm "));
            service.SaveValueIntoDB(2, service.GetGesamttubenanzahlServer2(), service.GetGesamttubenanzahlServer2(), DateTime.Now.ToString("dd.MMMM.yyyy hh:mm "));
        }
    }
}
