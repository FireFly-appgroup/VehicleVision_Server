using System;
using VehicleVision.DAL.Models;
using VehicleVision.DAL.Repository;

namespace VehicleVision.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VehicleVisionDBContext _context;
        private IGenericRepository<VehiclesInformation> _vehiclesInformationRepository;

        public UnitOfWork()
        {
            this._context = new VehicleVisionDBContext();
        }

        public IGenericRepository<VehiclesInformation> VehicleInformationRepository
        {
            get { return this._vehiclesInformationRepository ?? (this._vehiclesInformationRepository = new GenericRepository<VehiclesInformation>(_context)); }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}