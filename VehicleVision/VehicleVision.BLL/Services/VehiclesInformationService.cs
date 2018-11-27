using System.Collections.Generic;
using VehicleVision.DAL.Models;
using VehicleVision.DAL.UnitOfWork;

namespace VehicleVision.BLL.Services
{
    public class VehiclesInformationService : IVehiclesInformationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehiclesInformationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<VehiclesInformation> GetVehicleInformation()
        {
            return _unitOfWork.VehicleInformationRepository.GetAll();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}