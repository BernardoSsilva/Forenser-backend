namespace ForenserBackend.Communication.Response.Short
{
    public class ShortReportResponseJson
    {
        public string Id { get; set; } = string.Empty;
        public  string Title { get; set; } = string.Empty;
        public  string City { get; set; } = string.Empty;

        public  DateTime ReportingDate { get; set; } 
    }
}
