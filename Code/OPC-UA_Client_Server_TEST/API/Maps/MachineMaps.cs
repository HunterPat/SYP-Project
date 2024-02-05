using API.Services;
using Microsoft.AspNetCore.Mvc;
using OPC_UA_Client;

namespace API.Maps
{
    public static class MachineMaps
    {
        public static int gesamtTubenAnzZiel = 0;
        public static int resetBit = 1;

        public static IEndpointRouteBuilder MapMachineData(this IEndpointRouteBuilder routes)
        {
            var gesamtTubenAnzDataGroup = routes.MapGroup("/gesamttubenanz");
            var gesamtTubenAnzZielGroup = routes.MapGroup("/gesamttubenanzZiel");
            var resetBitGroup = routes.MapGroup("/resetBit");


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
            resetBitGroup.MapPost("/Server1", (MachineServices service) =>  service.PostResetbitServer1());
            resetBitGroup.MapPost("/Server2", ( MachineServices service) =>  service.PostResetbitMachine2());
            //gesamttubenAnzahl GET-Requests
            gesamtTubenAnzDataGroup.MapGet("/Server1", (MachineServices service) => service.GetGesamttubenanzahlServer1());
            gesamtTubenAnzDataGroup.MapGet("/Server2", (MachineServices service) => service.GetGesamttubenanzahlServer2());
            gesamtTubenAnzDataGroup.MapGet("/Machine1/{serverID}", (int serverID, MachineServices service) => service.GetGesamttubenanzahlMachine1(serverID));
            gesamtTubenAnzDataGroup.MapGet("/Machine2/{serverID}", (int serverID, MachineServices service) => service.GetGesamttubenanzahlMachine2(serverID));

            return routes;
        }
    }
}
