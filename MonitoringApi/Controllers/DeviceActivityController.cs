using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MonitoringApi.Models;
using MonitoringApi.Services;

namespace MonitoringApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceActivityController : ControllerBase
    {
        private readonly StorageService _storage;
        private readonly ILogger<DeviceActivityController> _logger;

        public DeviceActivityController(StorageService storage, ILogger<DeviceActivityController> logger)
        {
            _storage = storage;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] DeviceActivity activity)
        {
            _logger.LogInformation($"Получено действие от {activity.UserName} (ID: {activity.Id})");

            if (_storage.Activities.FirstOrDefault(x => x.Id == activity.Id) != null) 
            {
                _logger.LogError($"Активность с ID: {activity.Id} уже существует!");
                return BadRequest($"Активность с ID: {activity.Id} уже существует!"); 
            }

            try
            {
                _storage.Activities.Add(activity);
                _logger.LogDebug($"Активность пользователя {activity.UserName} добавлена успешно!");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, $"Не удалось сохранить активность для {activity.UserName}");
                return StatusCode(500);
            }
        }

        //Список всех устройств с уникальными именами
        [HttpGet("devices")]
        public IActionResult GetDevices()
        {
            _logger.LogInformation("Выборка списка устройств");

            var devices = _storage.Activities
            .Select(a => a.UserName)
            .Distinct();

            _logger.LogDebug($"Найдено {devices.Count()} девайсов");

            return Ok(devices);
        }

        //Информация о устройстве
        [HttpGet("devices/{userName}")]
        public IActionResult GetDeviceActivities(string userName)
        {
            try
            {
                var device = _storage.Activities.Where(x => x.UserName == userName);

                _logger.LogInformation($"Выбор устройства {userName}");
                return Ok(device);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, $"Устройство {userName} не найдено!");
                return BadRequest($"Устройство {userName} не найдено!");
            }

        }

        [HttpDelete]
        public IActionResult DeleteDeviceActivity([FromBody] string[] ids)
        {
            if (ids.Length == 0 || ids == null) return BadRequest();

            foreach (var id in ids)
            {
                var activity = _storage.Activities.FirstOrDefault(x => x.Id == id);
                if (activity != null)
                {
                    _storage.Activities.Remove(activity);
                    _logger.LogDebug($"Удалена активность: {id}");
                }
            }
            return Ok();
        }
    }
}
