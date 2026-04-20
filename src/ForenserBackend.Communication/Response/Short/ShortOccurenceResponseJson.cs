using ForenserBackend.Domain.Enums;

namespace ForenserBackend.Communication.Response.Short
{
    public class ShortOccurenceResponseJson
    {
        public string Id { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public string OccurrenceStreet { get; set; } = string.Empty;
        public string OccurrenceCity { get; set; } = string.Empty;
        public Ufs OccurrenceState { get; set; }

        public OccurrenceType Type { get; set; }
        public DateTime OccurrenceDate { get; set; }
    }
}
