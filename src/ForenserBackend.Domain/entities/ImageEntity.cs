namespace ForenserBackend.Domain.entities
{
    public class ImageEntity
    {

        public string Id { get; set; } = Guid.NewGuid().ToString(); 
        public  required string Name { get; set; }
        public required string ImageUrl { get; set; }
        public string Description { get; set; } = string.Empty;
        public  required string OccurenceId { get; set; }

        public required double ImageSize { get; set; }
        public OccurrenceEntity Occurrence { get; set; } = null!;

    }
}
