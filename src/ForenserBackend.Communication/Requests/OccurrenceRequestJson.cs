using ForenserBackend.Domain.Enums;

namespace ForenserBackend.Communication.Requests
{
    public class OccurrenceRequestJson
    {
        public  string UserId { get; set; } = string.Empty;

        public  string OccurrenceStreet { get; set; } = string.Empty;
        public  string OccurrenceCity { get; set; } = string.Empty;

        public  Ufs OccurrenceState { get; set; }

        public string? OccurrenceDescription { get; set; }

        public  DateTime OccurrenceDate { get; set; }

        public  OccurrenceType Type { get; set; }

        public List<string> ObjectList { get; set; } = [];

        public List<string> Vitms { get; set; } = [];

        public List<string> WitnessList { get; set; } = [];
        public List<string> ReferencePoints { get; set; } = [];

    }
}
