namespace ForenserBackend.Communication.Requests
{
    public class ImageRequestJson
    {
        public  string Name { get; set; } = string.Empty;
        public  string ImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public  string OccurenceId { get; set; } = string.Empty;
        public  string UserId { get; set; } = string.Empty;
        public  double ImageSize { get; set; }
    }
}
