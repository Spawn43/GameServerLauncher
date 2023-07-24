namespace GameServerLauncher.Models
{
    public class ServerStatistic
    {
        public DateTime Time { get; set; }
        public float CPU { get; set; }
        public float RAMAmount { get; set; }
        public float RAMUsage { get; set; }
        public float Bandwidth { get; set; }
        public float StorageAmount { get; set; }
        public float StorageUsage { get; set; }

    }
}
