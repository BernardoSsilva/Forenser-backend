using ForenserBackend.Domain.Enums;

namespace ForenserBackend.Domain.entities
{
    public class ServiceScheduleEntity
    {
        public string  Id { get; set; } = Guid.NewGuid().ToString();
        public required string UserId { get; set; }
        public required string City { get; set; }

        public Ufs State { get; set; }

        public ServiceType Type { get; set; }
        public string PostalCode { get; set; } = string.Empty;

        public required string PoliceUnity { get; set; } 

        public required string ContactPhone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public  UserEntity User { get; set; }= null!;
    }
}
