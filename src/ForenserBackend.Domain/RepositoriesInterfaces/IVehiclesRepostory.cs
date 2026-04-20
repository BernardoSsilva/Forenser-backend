using ForenserBackend.Domain.entities;

namespace ForenserBackend.Domain.RepositoriesInterfaces
{
    public interface IVehiclesRepository
    {
        public Task RegisterVehicle(VehicleEntity vehicle);
        public void UpdateVehicleData(VehicleEntity vehicle);
        public Task UnregisterVehicle(string vehicleId);

        public Task<VehicleEntity> GetVehicleById(string vehicleId);
        public Task<List<VehicleEntity>> GetVehiclesByOccurence(string occurenceId);
        public Task<List<VehicleEntity>> GetAllVehicles();
    }
}
