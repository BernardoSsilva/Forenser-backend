using ForenserBackend.Communication.Response.Short;

namespace ForenserBackend.Communication.Response.Multiple
{
    public class MultipleReportsResponseJson
    {
        public List<ShortReportResponseJson> Reports { get; set; } = [];
    }
}
