using ForenserBackend.Domain.Enums;

namespace ForenserBackend.Communication.Response.Detailed
{
    public class DetailedReportResponseJson
    {
        public string Id { get; set; } = string.Empty;
        public  string Title { get; set; } = string.Empty;
        public  string Description { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        public  Ufs State { get; set; }

        public  string Street { get; set; } = string.Empty;

        public  string ReportedPeopleName { get; set; } = string.Empty;
        public  string City { get; set; } = string.Empty;

        public  DateTime ReportingDate { get; set; }
        public DateTime CreatedAt { get; set; } 

        public string ContactPhone { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

    }
}
