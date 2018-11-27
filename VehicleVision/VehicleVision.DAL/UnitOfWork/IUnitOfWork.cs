using System;
using VehicleVision.DAL.Models;
using VehicleVision.DAL.Repository;

namespace VehicleVision.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<VehiclesInformation> VehicleInformationRepository { get; }
        void Save();
    }
}
