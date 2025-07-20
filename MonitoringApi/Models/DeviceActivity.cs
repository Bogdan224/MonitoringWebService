using System.ComponentModel.DataAnnotations;

namespace MonitoringApi.Models
{
    

    public class DeviceActivity
    {
        private const string _requiredErrorMessage = "Обязательное поле!";
        private const int _idLength = 36;
        private const int _versionLength = 9;

        [Required(ErrorMessage = _requiredErrorMessage)]
        [StringLength(_idLength, MinimumLength = _idLength, ErrorMessage = "Длина должна быть ровно 36 символов!")]
        public string Id { get; set; }

        [Required(ErrorMessage = _requiredErrorMessage)]
        public string UserName { get; set; }

        [Required(ErrorMessage = _requiredErrorMessage)]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = _requiredErrorMessage)]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = _requiredErrorMessage)]
        [StringLength(_versionLength, MinimumLength = _versionLength, ErrorMessage = $"Длина должна быть ровно 9 символов!")]
        public string Version { get; set; }
    }
}
