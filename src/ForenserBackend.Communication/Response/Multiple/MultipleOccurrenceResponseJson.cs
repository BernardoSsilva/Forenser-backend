using ForenserBackend.Communication.Response.Short;

namespace ForenserBackend.Communication.Response.Multple
{
    public class MultipleOccurrenceResponseJson
    {
        public List<ShortOccurenceResponseJson> Occurrences { get; set; } = [];
    }
}
