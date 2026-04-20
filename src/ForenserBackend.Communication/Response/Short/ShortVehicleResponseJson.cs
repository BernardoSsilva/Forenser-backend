namespace ForenserBackend.Communication.Response.Short
{
    public class ShortVehicleResponseJson
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Model { get; set; } = string.Empty;
        public string VehicleMark { get; set; } = string.Empty;
    }
}
