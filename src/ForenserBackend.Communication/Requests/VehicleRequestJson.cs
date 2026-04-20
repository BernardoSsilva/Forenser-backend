namespace ForenserBackend.Communication.Requests
{
    public class VehicleRequestJson
    {
        public  string OcurrenceId { get; set; } = string.Empty;

        public  string UserRegisterId { get; set; } = string.Empty;

        public  string Model { get; set; } = string.Empty;

        public string VehicleYear { get; set; } = string.Empty;

        public  string VehicleMark { get; set; } = string.Empty;
    }
}
