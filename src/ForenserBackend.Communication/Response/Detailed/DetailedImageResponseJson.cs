namespace ForenserBackend.Communication.Response.Detailed
{
    public class DetailedImageResponseJson
    {
        public string Id { get; set; } = string.Empty;
        public  string Name { get; set; } = string.Empty;
        public  string ImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public  string OccurenceId { get; set; } = string.Empty;
        public  double ImageSize { get; set; }
    }
}
