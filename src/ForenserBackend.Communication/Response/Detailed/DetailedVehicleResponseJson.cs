using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForenserBackend.Communication.Response.Detailed
{
    public class DetailedVehicleResponseJson
    {
        public string Id { get; set; } = string.Empty;
        public string OcurrenceId { get; set; } = string.Empty;


        public string Model { get; set; } = string.Empty;

        public string VehicleYear { get; set; } = string.Empty;

        public string VehicleMark { get; set; } = string.Empty;
    }
}
