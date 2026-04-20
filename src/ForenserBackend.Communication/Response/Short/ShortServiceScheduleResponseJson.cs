using ForenserBackend.Domain.Enums;

namespace ForenserBackend.Communication.Response.Short
{
    public class ShortServiceScheduleResponseJson
    {
        public string Id { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public ServiceType Type { get; set; } 
        public string PoliceUnity { get; set; } = string.Empty;

    }
}
