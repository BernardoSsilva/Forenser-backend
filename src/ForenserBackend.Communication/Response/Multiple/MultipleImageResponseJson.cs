using ForenserBackend.Communication.Response.Short;

namespace ForenserBackend.Communication.Response.Multple
{
    public class MultipleImageResponseJson
    {
        public List<ShortImageResponseJson> Images { get; set; } = [];
    }
}
