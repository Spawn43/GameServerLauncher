using System.Net.NetworkInformation;

namespace GameServerLauncher.Services
{

    public interface INetworkService
    {
        float GetNetworkUsage();
    }
    public class NetworkService : INetworkService
    {

        public long previousbytessend = 0;
        public long previousbytesreceived = 0;


        IPv4InterfaceStatistics interfaceStats;     

        public float GetNetworkUsage()
        {

            //Must Initialize it each second to update values;
            interfaceStats = NetworkInterface.GetAllNetworkInterfaces()[2].GetIPv4Statistics();

            //SPEED = MAGNITUDE / TIME ; HERE, TIME = 1 second Hence :
            float downloadspeed;
            float uploadspeed;

            long recievedBytes = interfaceStats.BytesReceived;            
            downloadspeed = Convert.ToSingle(recievedBytes - previousbytesreceived) / 1024 / 1024;
            previousbytesreceived = recievedBytes;

            long sentBytes = interfaceStats.BytesSent;
            uploadspeed = Convert.ToSingle(sentBytes - previousbytessend) / 1024 / 1024;
            previousbytessend = sentBytes;

            return (float)Math.Round((downloadspeed + uploadspeed), 2);
        }

    }
}
