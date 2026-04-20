using ForenserBackend.Domain.entities;
using ForenserBackend.Domain.RepositoriesInterfaces;
using ForenserBackend.Exception.HttpErrors;
using Microsoft.EntityFrameworkCore;

namespace ForenserBackend.Infrastructure.repositories
{
    public class VehiclesRepository : IVehiclesRepository
    {
        private readonly ForenserDbContext _dbContext;

        public VehiclesRepository(ForenserDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<VehicleEntity>> GetAllVehicles()
        {
            return await _dbContext.Vehicles.AsNoTracking().ToListAsync();
        }

        public async Task<VehicleEntity> GetVehicleById(string vehicleId)
        {
            var vehicle = await _dbContext.Vehicles.AsNoTracking().FirstOrDefaultAsync(vehicle => vehicle.Id == vehicleId);

            if (vehicle is null)
            {
                throw new NotFoundException("Vehicle not found");
            }
            return vehicle;
        }

        public async Task<List<VehicleEntity>> GetVehiclesByOccurence(string occurenceId)
        {
            var AllVehicles = await _dbContext.Vehicles.ToListAsync();
            return AllVehicles.Where(vehicle => vehicle.OcurrenceId == occurenceId).ToList();
        }

        public async Task RegisterVehicle(VehicleEntity vehicle)
        {
            await _dbContext.AddAsync(vehicle);
        }

        public async Task UnregisterVehicle(string vehicleId)
        {
            var vehicleToDelete = await _dbContext.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == vehicleId);

            if (vehicleToDelete is null) {
                throw new NotFoundException("Vehicle not found"); 
            }

            _dbContext.Remove(vehicleToDelete);
        }

        public void UpdateVehicleData(VehicleEntity vehicle)
        {
           _dbContext.Update(vehicle);
        }
    }
}
