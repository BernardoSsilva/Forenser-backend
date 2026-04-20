using ForenserBackend.Domain.Enums;

namespace ForenserBackend.Communication.Response.Detailed
{
    public class DetailedPeopleResponseJson
    {
        public string Id { get; set; } = string.Empty;
        public  string PersonName { get; set; } = string.Empty;

        public  int PersonAge { get; set; }

        public  EnvolveType Type { get; set; }

        public  string OccurrenceId { get; set; } = string.Empty;
    }
}
