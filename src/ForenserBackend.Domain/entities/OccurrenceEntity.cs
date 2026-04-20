using ForenserBackend.Domain.Enums;

namespace ForenserBackend.Domain.entities
{
    public class OccurrenceEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public required string UserId { get; set; }

        public required string OccurrenceStreet { get; set; }
        public required string OccurrenceCity { get; set; }

        public required Ufs OccurrenceState { get; set; }

        public string? OccurrenceDescription { get; set; } 

        public required DateTime OccurrenceDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime LastUpdatedAt { get; set; } 

        public required OccurrenceType Type { get; set; }

        public List<string> ObjectList { get; set; } = [];

        public ICollection<PeopleEntity> EnvolvedPeople { get;} = new List<PeopleEntity>();
        public List<string> ReferencePoints { get; set; } = [];

        public UserEntity User { get; set; } = null!;
        public ICollection<ImageEntity> Images { get; } = new List<ImageEntity>();
        public ICollection<VehicleEntity> Vehicles { get;  } = new List<VehicleEntity>();

    }
}
