using ForenserBackend.Domain.entities;
using ForenserBackend.Exception.HttpErrors;
using ForenserBackend.Infrastructure;
using ForenserBackend.Infrastructure.repositories;
using Microsoft.EntityFrameworkCore;

namespace ForenserBackend.Tests.RepositoriesTest
{
    public class ServiceScheduleRepositoryTests
    {
        private readonly ServiceScheduleRepository _serviceScheduleRepository;
        private readonly ForenserDbContext _dbContext;

        public ServiceScheduleRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ForenserDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ForenserDbContext(options);
            _serviceScheduleRepository = new ServiceScheduleRepository(_dbContext);
        }

        [Fact]
        public async Task ServiceSchedule_ShouldBeAbleToScheduleANewObject()
        {
            var newEntity = new ServiceScheduleEntity
            {
                City = "test city",
                ContactPhone = "test phone",
                PoliceUnity = "test unity",
                UserId = Guid.NewGuid().ToString(),
            };

            await _serviceScheduleRepository.RegisterServiceSchedule(newEntity);
            await _dbContext.SaveChangesAsync();

            var registeredRepository = await _serviceScheduleRepository.GetServiceScheduleById(newEntity.Id);

            Assert.NotNull(registeredRepository);
            Assert.Equal(newEntity.Id, registeredRepository.Id);
        }

        [Fact]
        public async Task ServiceSchedule_ShouldFindAScheduleById()
        {
            var newEntity = new ServiceScheduleEntity
            {
                City = "test city",
                ContactPhone = "test phone",
                PoliceUnity = "test unity",
                UserId = Guid.NewGuid().ToString(),
            };

            await _serviceScheduleRepository.RegisterServiceSchedule(newEntity);
            await _dbContext.SaveChangesAsync();

            var registeredRepository = await _serviceScheduleRepository.GetServiceScheduleById(newEntity.Id);

            Assert.NotNull(registeredRepository);
        }

        [Fact]
        public async Task ServiceSchedule_ShouldThrowErrorIfScheduleIsNotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _serviceScheduleRepository.GetServiceScheduleById("random id"));
        }

        [Fact]
        public async Task ServiceSchedule_ShouldBeAbleToDeleteASchedule()
        {
            var newEntity = new ServiceScheduleEntity
            {
                City = "test city",
                ContactPhone = "test phone",
                PoliceUnity = "test unity",
                UserId = Guid.NewGuid().ToString(),
            };

            await _serviceScheduleRepository.RegisterServiceSchedule(newEntity);
            await _dbContext.SaveChangesAsync();

            var registeredRepositories = await _serviceScheduleRepository.GetAllScheduledServices();

            Assert.Equal(1, registeredRepositories.Count());

            await _serviceScheduleRepository.UnscheduleService(newEntity.Id);
            await _dbContext.SaveChangesAsync();
            registeredRepositories = await _serviceScheduleRepository.GetAllScheduledServices();
            Assert.Equal(0, registeredRepositories.Count());
        }

        [Fact]
        public async Task ServiceSchedule_ShouldThrowErrorIfScheduleTryDeleteANonExistentShcedule()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _serviceScheduleRepository.UnscheduleService("random id"));
        }

        [Fact]
        public async Task ServiceSchedule_ShouldFindAllSchedules()
        {

            var registeredRepositories = await _serviceScheduleRepository.GetAllScheduledServices();
            Assert.Equal(0, registeredRepositories.Count());

            var newEntity = new ServiceScheduleEntity
            {
                City = "test city",
                ContactPhone = "test phone",
                PoliceUnity = "test unity",
                UserId = Guid.NewGuid().ToString(),
            };

            await _serviceScheduleRepository.RegisterServiceSchedule(newEntity);
            await _dbContext.SaveChangesAsync();

            registeredRepositories = await _serviceScheduleRepository.GetAllScheduledServices();

            Assert.Equal(1, registeredRepositories.Count());
        }

        [Fact]
        public async Task ServiceSchedule_ShouldBeAbleToUpdateASchedule()
        {

            var newEntity = new ServiceScheduleEntity
            {
                City = "test city",
                ContactPhone = "test phone",
                PoliceUnity = "test unity",
                UserId = Guid.NewGuid().ToString(),
                
            };

            await _serviceScheduleRepository.RegisterServiceSchedule(newEntity);
            await _dbContext.SaveChangesAsync();

            var registeredRepository = await _serviceScheduleRepository.GetServiceScheduleById(newEntity.Id);

            Assert.Empty(registeredRepository.PostalCode);

            newEntity.PostalCode = "new code";

            _serviceScheduleRepository.UpdateSchedule(newEntity);
            await _dbContext.SaveChangesAsync();

            registeredRepository = await _serviceScheduleRepository.GetServiceScheduleById(newEntity.Id);
            Assert.Equal("new code",registeredRepository.PostalCode);

        }

    }
}
