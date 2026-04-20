using ForenserBackend.Domain.entities;
using ForenserBackend.Domain.Enums;

namespace ForenserBackend.Communication.Response.Detailed
{
    public class DetailedOccurenceResponseJson
    {
        public string Id { get; set; } = string.Empty;

        public  string UserId { get; set; } = string.Empty;

        public  string OccurrenceStreet { get; set; } = string.Empty;
        public  string OccurrenceCity { get; set; } = string.Empty;

        public  Ufs OccurrenceState { get; set; }

        public string OccurrenceDescription { get; set; } = string.Empty;

        public  DateTime OccurrenceDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public  OccurrenceType Type { get; set; }

        public List<string> ObjectList { get; set; } = [];

        public List<string> ReferencePoints { get; set; } = [];

    }
}
