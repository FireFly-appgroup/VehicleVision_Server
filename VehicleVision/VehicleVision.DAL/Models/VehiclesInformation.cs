using System;
using System.Collections.Generic;

namespace VehicleVision.DAL.Models
{
    public partial class VehiclesInformation
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string VehiclePhoto { get; set; }
        public string VehicleNumberPhoto { get; set; }
    }
}
