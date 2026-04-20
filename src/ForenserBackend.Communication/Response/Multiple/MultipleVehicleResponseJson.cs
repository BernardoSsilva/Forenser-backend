using ForenserBackend.Communication.Response.Short;

namespace ForenserBackend.Communication.Response.Multiple
{
    public class MultipleVehicleResponseJson
    {
        public List<ShortVehicleResponseJson> Vehicles { get; set; } = [];
    }
}
