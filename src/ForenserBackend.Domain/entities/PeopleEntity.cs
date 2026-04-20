using ForenserBackend.Domain.Enums;

namespace ForenserBackend.Domain.entities
{
    public class PeopleEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string PersonName { get; set; }

        public required int PersonAge { get; set; }

        public required EnvolveType Type { get; set;}

        public required string OccurrenceId { get; set; } 

        public OccurrenceEntity Occurrence { get; set; } = null!;
    }
}
