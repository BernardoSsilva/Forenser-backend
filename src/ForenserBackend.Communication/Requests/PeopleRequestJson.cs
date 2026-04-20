using ForenserBackend.Domain.Enums;

namespace ForenserBackend.Communication.Requests
{
    public class PeopleRequestJson
    {
        public  string PersonName { get; set; } = string.Empty;

        public  int PersonAge { get; set; }

        public  EnvolveType Type { get; set; }

        public string OccurrenceId { get; set; } = string.Empty;

    }
}
