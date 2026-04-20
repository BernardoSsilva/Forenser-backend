using ForenserBackend.Domain.Enums;

namespace ForenserBackend.Communication.Response.Detailed
{
    public class DetailedServiceScheduleResponseJson
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public Ufs State { get; set; }

        public ServiceType Type { get; set; }
        public string PostalCode { get; set; } = string.Empty;

        public string PoliceUnity { get; set; } = string.Empty;

        public string ContactPhone { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } 
    }
}
