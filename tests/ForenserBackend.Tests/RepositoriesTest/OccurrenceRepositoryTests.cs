using ForenserBackend.Domain.entities;
using ForenserBackend.Exception.HttpErrors;
using ForenserBackend.Infrastructure;
using ForenserBackend.Infrastructure.repositories;
using Microsoft.EntityFrameworkCore;

namespace ForenserBackend.Tests.RepositoriesTest
{
    public class OccurrenceRepositoryTests
    {
        private readonly OccurrenceRepository _occurrenceRepository;
        private readonly ForenserDbContext _dbContext;

        public OccurrenceRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ForenserDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ForenserDbContext(options);
            _occurrenceRepository = new OccurrenceRepository(_dbContext);
        }

        [Fact]
        public async Task OccurrenceRepository_ShouldBeAbleToCreateANewOccurrence()
        {
            var newOccurrence = new OccurrenceEntity
            {
                Id = Guid.NewGuid().ToString(),

                OccurrenceCity = "Initial city",
                OccurrenceDescription = "Initial Description",
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now,
                OccurrenceDate = DateTime.Now,
                OccurrenceState = Domain.Enums.Ufs.AC,
                OccurrenceStreet = " test street",
                Type = Domain.Enums.OccurrenceType.theft,
                UserId = Guid.NewGuid().ToString(),
            };

            await _occurrenceRepository.CreateNewOccurence(newOccurrence);
            await _dbContext.SaveChangesAsync();

            var registeredOccurrence = await _occurrenceRepository.GetOccurenceDetailsById(newOccurrence.Id);
            Assert.NotNull(registeredOccurrence);
            Assert.Equal(newOccurrence.OccurrenceCity, registeredOccurrence.OccurrenceCity);
        }

        [Fact]
        public async Task OccurrenceRepository_ShouldThrowErrorWhenOccurrenceNotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _occurrenceRepository.GetOccurenceDetailsById("InvalidId"));
        }

        [Fact]
        public async Task OccurrenceRepository_ShouldUpdateOccurrence()
        {
            var newOccurrence = new OccurrenceEntity
            {
                Id = Guid.NewGuid().ToString(),
                
                OccurrenceCity= "Initial city",
                OccurrenceDescription = "Initial Description",
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now,
                OccurrenceDate = DateTime.Now,
                OccurrenceState = Domain.Enums.Ufs.AC,
                OccurrenceStreet =" test street",
                Type = Domain.Enums.OccurrenceType.theft,
                UserId = Guid.NewGuid().ToString(),
            };

            await _occurrenceRepository.CreateNewOccurence(newOccurrence);
            await _dbContext.SaveChangesAsync();

            newOccurrence.OccurrenceCity = "Updated city";
            _occurrenceRepository.UpdateOccurence(newOccurrence);
            await _dbContext.SaveChangesAsync();

            var updatedOccurrence = await _occurrenceRepository.GetOccurenceDetailsById(newOccurrence.Id);
            Assert.Equal("Updated city", updatedOccurrence.OccurrenceCity);
        }

        [Fact]
        public async Task OccurrenceRepository_ShouldDeleteOccurrence()
        {
            var newOccurrence = new OccurrenceEntity
            {
                Id = Guid.NewGuid().ToString(),

                OccurrenceCity = "Initial city",
                OccurrenceDescription = "Initial Description",
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now,
                OccurrenceDate = DateTime.Now,
                OccurrenceState = Domain.Enums.Ufs.AC,
                OccurrenceStreet = " test street",
                Type = Domain.Enums.OccurrenceType.theft,
                UserId = Guid.NewGuid().ToString(),
            };

            await _occurrenceRepository.CreateNewOccurence(newOccurrence);
            await _dbContext.SaveChangesAsync();

            await _occurrenceRepository.DeleteOccurence(newOccurrence.Id);
            await _dbContext.SaveChangesAsync();

            await Assert.ThrowsAsync<NotFoundException>(async () => await _occurrenceRepository.GetOccurenceDetailsById(newOccurrence.Id));
        }

        [Fact]
        public async Task OccurrenceRepository_ShouldThrowErrorIfDeleteNonExistentOccurrence()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _occurrenceRepository.DeleteOccurence("InvalidId"));
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}

