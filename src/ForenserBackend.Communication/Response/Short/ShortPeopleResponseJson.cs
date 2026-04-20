using ForenserBackend.Domain.Enums;

namespace ForenserBackend.Communication.Response.Short
{
    public class ShortPeopleResponseJson
    {
        public string Id { get; set; } = string.Empty;
        public string PersonName { get; set; } = string.Empty;

        public int PersonAge { get; set; } 

    }
}
