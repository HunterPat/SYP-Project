using API.Services;
using Microsoft.AspNetCore.Mvc;
using OPC_UA_Client;
using System.Runtime.InteropServices;
using System.Timers;
namespace API.Maps
{
    public static class MachineMaps
    {
        private static MachineServices service = null;


        public static IEndpointRouteBuilder MapMachineData(this IEndpointRouteBuilder routes)
        {
            var gesamtTubenAnzDataGroup = routes.MapGroup("/gesamttubenAnz");
            var gesamtTubenAnzVisualGroup = routes.MapGroup("/gesamttubenAnzVisual");
            var kaputteTubenAnzGroup = routes.MapGroup("/kaputteTubenAnz");
            var passwordGroup = routes.MapGroup("/password");
            var resetBitGroup = routes.MapGroup("/resetBit");
            var gesamtTubenAnzZielGroup = routes.MapGroup("/gesamttubenanzZiel");
            var timeIntervalGroup = routes.MapGroup("/timeInterval");
            MyOPCServer1 server1 = new MyOPCServer1(MachineServices.server1URL);
            MyOPCServer2 server2 = new MyOPCServer2(MachineServices.server2URL);
            server1.StartServer();
            server2.StartServer();
            service = new MachineServices();
            //   MachineServices.CreateDatabase(); If not created
            System.Timers.Timer timer = new System.Timers.Timer((3600 *1000) * 3); // every 3 hours check => 3600 seconds in a hour
            timer.Elapsed += Timer_Elapsed!;
            timer.Start();

            gesamtTubenAnzZielGroup.MapGet("", () => service.GetGesamttubenanzahlZiel());
            gesamtTubenAnzZielGroup.MapPut("", ([FromBody] int value) => service.PutGesamttubenAnzZiel(value));

            passwordGroup.MapGet("/check", (string passwordValue) => service.CheckPassword(passwordValue));
            passwordGroup.MapGet("/", () => service.GetPassword());
            passwordGroup.MapPut("/newPassword", (string newPasswordValue) => service.PutNewPassword(newPasswordValue));

            resetBitGroup.MapPost("/Server1", () => service.PostResetbitServer1());
            resetBitGroup.MapPost("/Server2", () => service.PostResetbitServer2());

            timeIntervalGroup.MapPut("/", (int intervalValue) => service.PutTimeInterval(intervalValue));
            timeIntervalGroup.MapGet("/", () => service.GetTimeInterval());

            gesamtTubenAnzVisualGroup.MapGet("/Server1", () => service.GetGesamttubenanzahlServer1());
            gesamtTubenAnzVisualGroup.MapGet("/Server2", () => service.GetGesamttubenanzahlServer2());

            gesamtTubenAnzDataGroup.MapGet("/Machine1/{serverID}", (int serverID) => service.GetGesamttubenanzahlMachine1(serverID));
            gesamtTubenAnzDataGroup.MapGet("/Machine2/{serverID}", (int serverID) => service.GetGesamttubenanzahlMachine2(serverID));
            //in Percent
            gesamtTubenAnzVisualGroup.MapGet("/Server1/Percent", () => service.GetGesamttubenanzahlPercentServer1());
            gesamtTubenAnzVisualGroup.MapGet("/Server2/Percent", () => service.GetGesamttubenanzahlPercentServer2());
            gesamtTubenAnzVisualGroup.MapGet("/TAA1/Percent", () => service.GetGesamttubenanzahlPercentTAA1());
            gesamtTubenAnzVisualGroup.MapGet("/TAA2/Percent", () => service.GetGesamttubenanzahlPercentTAA2());
            gesamtTubenAnzVisualGroup.MapGet("/TAA3/Percent", () => service.GetGesamttubenanzahlPercentTAA3());
            gesamtTubenAnzVisualGroup.MapGet("/TAA4/Percent", () => service.GetGesamttubenanzahlPercentTAA4());

            gesamtTubenAnzVisualGroup.MapGet("/Machine1/{serverID}", (int serverID) => service.GetGesamttubenanzahlMachine1Visual(serverID));
            gesamtTubenAnzVisualGroup.MapGet("/Machine2/{serverID}", (int serverID) => service.GetGesamttubenanzahlMachine2Visual(serverID));


            kaputteTubenAnzGroup.MapGet("/TAA1/", () => service.GetKaputtGesamtTubenAnzTAA1());
            kaputteTubenAnzGroup.MapGet("/TAA2/", () => service.GetKaputtGesamtTubenAnzTAA2());
            kaputteTubenAnzGroup.MapGet("/TAA3/", () => service.GetKaputtGesamtTubenAnzTAA3());
            kaputteTubenAnzGroup.MapGet("/TAA4/", () => service.GetKaputtGesamtTubenAnzTAA4());

            kaputteTubenAnzGroup.MapPut("/TAA1/", (int value) => service.PutKaputtGesamtTubenAnzTAA1(value));
            kaputteTubenAnzGroup.MapPut("/TAA2/", (int value) => service.PutKaputtGesamtTubenAnzTAA2(value));
            kaputteTubenAnzGroup.MapPut("/TAA3/", (int value) => service.PutKaputtGesamtTubenAnzTAA3(value));
            kaputteTubenAnzGroup.MapPut("/TAA4/", (int value) => service.PutKaputtGesamtTubenAnzTAA4(value));

            gesamtTubenAnzZielGroup.MapGet("/MachinePairs", () => service.GetGesamttubenanzahlZielMachinePairs());
            gesamtTubenAnzZielGroup.MapGet("/4Machines", () => service.GetGesamttubenanzahlZiel4Machines());

            return routes;
        }
        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now.TimeOfDay.Hours >= 0 && DateTime.Now.TimeOfDay.Hours <= 3 && DateTime.Now.TimeOfDay.Minutes >= 0 && DateTime.Now.TimeOfDay.Minutes <= 60)// check if time is 12:00 or 0:00
            {
                service.SaveValueIntoDB(1, service.GetGesamttubenanzahlServer1(), service.GetGesamttubenanzahlServer1(), DateTime.Now.ToString("dd.MMMM.yyyy hh:mm "));
                service.SaveValueIntoDB(2, service.GetGesamttubenanzahlServer2(), service.GetGesamttubenanzahlServer2(), DateTime.Now.ToString("dd.MMMM.yyyy hh:mm "));
                service = new MachineServices();
            }
        }
    }
}
