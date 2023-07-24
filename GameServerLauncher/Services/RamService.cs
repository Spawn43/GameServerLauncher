using System.Management;

namespace GameServerLauncher.Services
{
    public interface IRamService
    {
        float CalculateRamTotal();
        float CalculateRamUsage();
    }
    public class RamService : IRamService
    {
        public float CalculateRamTotal()
        {

            ManagementObjectCollection ramInfo = GetRamInfo();

            float ramTotal = 0f;
            foreach (ManagementObject mo in ramInfo)
            {
                ramTotal = (float)Convert.ToDouble(mo["TotalVisibleMemorySize"]) / (1024 * 1024);
            }

            return ramTotal;
        }

        

        public float CalculateRamUsage()
        {
            ManagementObjectCollection ramInfo = GetRamInfo();

            float ramTotal = CalculateRamTotal(ramInfo);
            float ramFree = 0f;
            foreach (ManagementObject mo in ramInfo)
            {
                ramFree = (float)Convert.ToDouble(mo["FreePhysicalMemory"]) / (1024 * 1024);

            }

            return ramTotal - ramFree;
        }


        private float CalculateRamTotal(ManagementObjectCollection ramInfo)
        {

            float ramTotal = 0f;
            foreach (ManagementObject mo in ramInfo)
            {
                ramTotal = (float)Convert.ToDouble(mo["TotalVisibleMemorySize"]) / (1024 * 1024);
            }

            return ramTotal;
        }

        private ManagementObjectCollection GetRamInfo()
        {
            ObjectQuery wmi_obj = new("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher findObj = new(wmi_obj);
            return findObj.Get();
           
        }
        
    }
}
