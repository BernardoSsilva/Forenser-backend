using ForenserBackend.Domain.entities;
using ForenserBackend.Exception.HttpErrors;
using ForenserBackend.Infrastructure;
using ForenserBackend.Infrastructure.repositories;
using Microsoft.EntityFrameworkCore;

namespace ForenserBackend.Tests.RepositoriesTest
{
    public class VehiclesRepositoryTests
    {
        private readonly VehiclesRepository _vehiclesRepository;
        private readonly ForenserDbContext _dbContext;
        public VehiclesRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ForenserDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ForenserDbContext(options);
            _vehiclesRepository = new VehiclesRepository(_dbContext);

        }

        [Fact]
        public async Task VehiclesRepository_ShouldCreateANewVehicleData()
        {
            var newVehicle = new VehicleEntity
            {
                Model = "testModel",
                OcurrenceId = "testId",
                VehicleMark = "testMark"
            };

            await _vehiclesRepository.RegisterVehicle(newVehicle);
            await _dbContext.SaveChangesAsync();

            var registeredVehicles = await _vehiclesRepository.GetAllVehicles();

            Assert.Equal(1, registeredVehicles.Count());
            Assert.Equal("testModel", registeredVehicles[0].Model);
        }

        [Fact]
        public async Task VehiclesRepository_ShouldFindVehicleData()
        {
            var newVehicle = new VehicleEntity
            {
                Model = "testModel",
                OcurrenceId = "testId",
                VehicleMark = "testMark"
            };

            await _vehiclesRepository.RegisterVehicle(newVehicle);
            await _dbContext.SaveChangesAsync();

            var registeredVehicles = await _vehiclesRepository.GetVehicleById(newVehicle.Id);

            Assert.Equal("testModel", registeredVehicles.Model);
        }

        [Fact]
        public async Task VehiclesRepository_ShouldThrowErrorIfVehicleDontExists()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _vehiclesRepository.GetVehicleById("testId"));
        }

        [Fact]
        public async Task VehiclesRepository_ShouldFindAllVehicles()
        {
            var newVehicle = new VehicleEntity
            {
                Model = "testModel",
                OcurrenceId = "testId",
                VehicleMark = "testMark"
            };
            var newVehicle2 = new VehicleEntity
            {
                Model = "testModel",
                OcurrenceId = "testId",
                VehicleMark = "testMark"
            };

            await _vehiclesRepository.RegisterVehicle(newVehicle);
            await _vehiclesRepository.RegisterVehicle(newVehicle2);

            await _dbContext.SaveChangesAsync();

            var registeredVehicles = await _vehiclesRepository.GetAllVehicles();

            Assert.Equal(2, registeredVehicles.Count());
        }

        [Fact]
        public async Task VehiclesRepository_ShouldBeAbleToDeleteAVehicle()
        {
            var newVehicle = new VehicleEntity
            {
                Model = "testModel",
                OcurrenceId = "testId",
                VehicleMark = "testMark"
            };
            await _vehiclesRepository.RegisterVehicle(newVehicle);

            await _dbContext.SaveChangesAsync();
            var registeredVehicles = await _vehiclesRepository.GetAllVehicles();

            Assert.Equal(1, registeredVehicles.Count());

            await _vehiclesRepository.UnregisterVehicle(newVehicle.Id);
            await _dbContext.SaveChangesAsync();
            registeredVehicles = await _vehiclesRepository.GetAllVehicles();
            Assert.Equal(0, registeredVehicles.Count());


        }

        [Fact]
        public async Task VehiclesRepository_ShouldThrowErrorIfTryDeleteANonExistentVehicle()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _vehiclesRepository.UnregisterVehicle("randomId"));
        }


        [Fact]
        public async Task VehiclesRepository_ShouldBeAbleToEditAVehicle()
        {
            var newVehicle = new VehicleEntity
            {
                Model = "testModel",
                OcurrenceId = "testId",
                VehicleMark = "testMark"
            };
            await _vehiclesRepository.RegisterVehicle(newVehicle);

            await _dbContext.SaveChangesAsync();


            var registeredVehicleOnDatabase = await _vehiclesRepository.GetVehicleById(newVehicle.Id);
            Assert.Equal(newVehicle.Model, registeredVehicleOnDatabase.Model);

            newVehicle.VehicleMark = "OtherMark";

            _vehiclesRepository.UpdateVehicleData(newVehicle);
            await _dbContext.SaveChangesAsync();
            registeredVehicleOnDatabase = await _vehiclesRepository.GetVehicleById(newVehicle.Id);
            Assert.Equal("OtherMark", registeredVehicleOnDatabase.VehicleMark);
        }
    }
}
