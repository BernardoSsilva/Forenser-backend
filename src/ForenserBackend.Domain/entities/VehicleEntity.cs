namespace ForenserBackend.Domain.entities
{
    public class VehicleEntity
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string OcurrenceId { get; set; }
    

        public required string Model { get; set; }

        public string VehicleYear { get; set; } = string.Empty;

        public required string VehicleMark { get; set; }

        public OccurrenceEntity Occurrence { get; set; } = null!;
    }
}
