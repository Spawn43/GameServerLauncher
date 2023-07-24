using System.Diagnostics;


namespace GameServerLauncher.Services
{
    public interface ICPUService
    {
        float GetCpuUsage();
    }
    public class CPUService : ICPUService
    {
        PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        
        public float GetCpuUsage()
        {
            float cpuUsage = cpuCounter.NextValue();
            return cpuUsage;
        }
    }
}
