using MonitoringApi.Models;

namespace MonitoringApi.Services
{
    public class StorageService
    {
        public List<DeviceActivity> Activities { get; set; } = new List<DeviceActivity>();
    }
}
