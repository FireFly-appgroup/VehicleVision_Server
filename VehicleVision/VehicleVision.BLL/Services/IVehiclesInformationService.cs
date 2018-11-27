using System;
using System.Collections.Generic;
using VehicleVision.DAL.Models;

namespace VehicleVision.BLL.Services
{
    public interface IVehiclesInformationService : IDisposable
    {
        IEnumerable<VehiclesInformation> GetVehicleInformation();
    }
}
