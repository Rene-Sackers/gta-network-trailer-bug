using GTANetworkServer;
using GTANetworkShared;
using System.Linq;

namespace FlatbedLoader.resources.trailer_bug
{
    public class Main : Script
    {
        Vehicle trailer;

        public Main()
        {
            API.onPlayerConnected += PlayerSpawnEvent;
            API.onPlayerRespawn += PlayerSpawnEvent;

            API.getAllPlayers().Take(1).ToList().ForEach(PlayerSpawnEvent);

            API.onVehicleTrailerChange += API_onVehicleTrailerChange;
        }

        private void API_onVehicleTrailerChange(NetHandle tower, NetHandle trailer)
        {
            API.consoleOutput("Trailer change. Tower: " + tower + ", trailer: " + trailer);
        }

        private void PlayerSpawnEvent(Client player)
        {
            API.getAllVehicles().ForEach(v => API.deleteEntity(v));

            var truck = API.createVehicle(VehicleHash.Phantom, new Vector3(433, 3579, 34), new Vector3(0, 0, -100), 0, 0);
            trailer = API.createVehicle(VehicleHash.TR4, new Vector3(413, 3582, 34), new Vector3(0, 0, -100), 0, 0);

            player.setIntoVehicle(truck, -1);
        }

        [Command(Alias = "del")]
        public void DeleteTrailerCommand(Client client)
        {
            if (trailer == null) return;

            trailer.delete();
        }
    }
}
