using ForenserBackend.Communication.Response.Short;

namespace ForenserBackend.Communication.Response.Multiple
{
    public class MultipleServiceScheduleResponseJson
    {
        public List<ShortServiceScheduleResponseJson> ServiceSchedules { get; set; } = [];
    }
}
