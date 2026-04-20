using ForenserBackend.Domain.Enums;

namespace ForenserBackend.Communication.Requests
{
    public class ServiceScheduleRequestJson
    {
        public  string UserId { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public Ufs State { get; set; }

        public ServiceType Type { get; set; }
        public string PostalCode { get; set; } = string.Empty;

        public  string PoliceUnity { get; set; } = string.Empty;

        public  string ContactPhone { get; set; } = string.Empty;
    }
}
