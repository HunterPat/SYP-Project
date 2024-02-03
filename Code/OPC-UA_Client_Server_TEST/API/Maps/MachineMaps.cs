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
            var gesamtTubenAnzDataGroup = routes.MapGroup("/{serverID}");
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
            resetBitGroup.MapGet("/", () =>  resetBit);
            resetBitGroup.MapPost("/", ([FromBody] int value) =>
            {
                if(value == 1 || value == 0)
                {
                    resetBit = value;
                    return resetBit;
                }
                return -1;
            });
            gesamtTubenAnzDataGroup.MapGet("/TAA1", (int serverID, MachineServices service) => service.GetGesamttubenanzahlTAA1(serverID));
            gesamtTubenAnzDataGroup.MapGet("/TAA2", (int serverID, MachineServices service) => service.GetGesamttubenanzahlTAA2(serverID));
            return routes;
        }
    }
}
