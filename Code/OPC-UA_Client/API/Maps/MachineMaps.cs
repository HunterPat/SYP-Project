using API.Services;
using Microsoft.AspNetCore.Mvc;
using OPC_UA_Client;

namespace API.Maps
{
    public static class MachineMaps
    {
        public static IEndpointRouteBuilder MapMachineData(this IEndpointRouteBuilder routes)
        {
            var gesamtTubenAnzDataGroup = routes.MapGroup("/{serverID}");
            var resetBitGroup = routes.MapGroup("/resetBit");
            var gesamtTubenAnzZielGroup = routes.MapGroup("/gesamttubenanzZiel");
            int gesamtTubenAnzZiel = 0;
            int resetBit = 0;
            gesamtTubenAnzZielGroup.MapGet("", () => gesamtTubenAnzZiel);
            gesamtTubenAnzZielGroup.MapPost("", ([FromBody] int value) =>
            {
                gesamtTubenAnzZiel = value;
                return gesamtTubenAnzZiel;
            });
            resetBitGroup.MapPost("/Machine1/resetBit", (int serverID, MachineServices service) => service.PostResetBitMachine1(serverID));
            resetBitGroup.MapPost("/Machine2/resetBit", (int serverID, MachineServices service) => service.PostResetBitMachine2(serverID));
            gesamtTubenAnzDataGroup.MapGet("/Machine1", (int serverID, MachineServices service) => service.GetGesamttubenanzahlMachine1(serverID));
            gesamtTubenAnzDataGroup.MapGet("/Machine2", (int serverID, MachineServices service) => service.GetGesamttubenanzahlMachine2(serverID));
            return routes;
        }
    }
}
