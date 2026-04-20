using ForenserBackend.Domain.entities;
using ForenserBackend.Exception.HttpErrors;
using ForenserBackend.Infrastructure;
using ForenserBackend.Infrastructure.repositories;
using Microsoft.EntityFrameworkCore;

namespace ForenserBackend.Tests.RepositoriesTest
{
    public class PeoplesRepositoryTests
    {
        private readonly PeoplesRepository _peoplesRepository;
        private readonly ForenserDbContext _dbContext;

        public PeoplesRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ForenserDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ForenserDbContext(options);
            _peoplesRepository = new PeoplesRepository(_dbContext);
        }

        [Fact]
        public async Task PeoplesRepository_ShouldRegisterNewPeople()
        {
            var newEntity = new PeopleEntity
            {
                PersonName = "John Doe",
                OccurrenceId = Guid.NewGuid().ToString(),
                PersonAge = 20,
                Type = Domain.Enums.EnvolveType.vitim
            };

            await _peoplesRepository.RegisterNewPeople(newEntity);
            await _dbContext.SaveChangesAsync();

            var registeredPeople = await _peoplesRepository.FindPeopleById(newEntity.Id);

            Assert.NotNull(registeredPeople);
            Assert.Equal(newEntity.Id, registeredPeople.Id);
        }

        [Fact]
        public async Task PeoplesRepository_ShouldFindPeopleById()
        {
            var newEntity = new PeopleEntity
            {
                PersonName = "John Doe",
                OccurrenceId = Guid.NewGuid().ToString(),
                PersonAge = 20,
                Type = Domain.Enums.EnvolveType.vitim
            };

            await _peoplesRepository.RegisterNewPeople(newEntity);
            await _dbContext.SaveChangesAsync();

            var foundPeople = await _peoplesRepository.FindPeopleById(newEntity.Id);

            Assert.NotNull(foundPeople);
        }

        [Fact]
        public async Task PeoplesRepository_ShouldThrowErrorIfPeopleNotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _peoplesRepository.FindPeopleById("random id"));
        }

        [Fact]
        public async Task PeoplesRepository_ShouldDeletePeopleRegister()
        {
            var newEntity = new PeopleEntity
            {
                PersonName = "John Doe",
                OccurrenceId = Guid.NewGuid().ToString(),
                PersonAge = 20,
                Type = Domain.Enums.EnvolveType.vitim
            };

            await _peoplesRepository.RegisterNewPeople(newEntity);
            await _dbContext.SaveChangesAsync();

            var registeredPeoples = await _peoplesRepository.FindAllPeople();

            Assert.Equal(1, registeredPeoples.Count());

            await _peoplesRepository.DeletePeopleRegister(newEntity.Id);
            await _dbContext.SaveChangesAsync();

            registeredPeoples = await _peoplesRepository.FindAllPeople();
            Assert.Equal(0, registeredPeoples.Count());
        }

        [Fact]
        public async Task PeoplesRepository_ShouldThrowErrorIfDeletingNonExistentPeople()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _peoplesRepository.DeletePeopleRegister("random id"));
        }

        [Fact]
        public async Task PeoplesRepository_ShouldFindAllPeople()
        {
            var peoplesList = await _peoplesRepository.FindAllPeople();
            Assert.Equal(0, peoplesList.Count());

            var newEntity = new PeopleEntity
            {
                PersonName = "John Doe",
                OccurrenceId = Guid.NewGuid().ToString(),
                PersonAge = 20,
                Type = Domain.Enums.EnvolveType.vitim
            };

            await _peoplesRepository.RegisterNewPeople(newEntity);
            await _dbContext.SaveChangesAsync();

            peoplesList = await _peoplesRepository.FindAllPeople();
            Assert.Equal(1, peoplesList.Count());
        }

        [Fact]
        public async Task PeoplesRepository_ShouldUpdatePeopleData()
        {
            var newEntity = new PeopleEntity
            {
                PersonName = "John Doe",
                OccurrenceId = Guid.NewGuid().ToString(),
                PersonAge = 20,
                Type = Domain.Enums.EnvolveType.vitim
            };

            await _peoplesRepository.RegisterNewPeople(newEntity);
            await _dbContext.SaveChangesAsync();

            var foundPeople = await _peoplesRepository.FindPeopleById(newEntity.Id);
            Assert.Equal("John Doe", foundPeople.PersonName);

            newEntity.PersonName = "Jane Doe";
            _peoplesRepository.UpdatePeopleData(newEntity);
            await _dbContext.SaveChangesAsync();

            foundPeople = await _peoplesRepository.FindPeopleById(newEntity.Id);
            Assert.Equal("Jane Doe", foundPeople.PersonName);
        }

        [Fact]
        public async Task PeoplesRepository_ShouldFindAllPeopleOnOccurrence()
        {
            var occurrenceId = Guid.NewGuid().ToString();

            var people1 = new PeopleEntity
            {
                PersonName = "Person 1",
                OccurrenceId = occurrenceId,
                PersonAge = 20,
                Type = Domain.Enums.EnvolveType.vitim
            };

            var people2 = new PeopleEntity
            {
                PersonName = "Person 2",
                OccurrenceId = occurrenceId,
                PersonAge = 20,
                Type = Domain.Enums.EnvolveType.vitim
            };

            var people3 = new PeopleEntity
            {
                PersonName = "Person 3",
                OccurrenceId = Guid.NewGuid().ToString(), // Different occurrence
                  PersonAge = 20,
                Type = Domain.Enums.EnvolveType.vitim
            };

            await _peoplesRepository.RegisterNewPeople(people1);
            await _peoplesRepository.RegisterNewPeople(people2);
            await _peoplesRepository.RegisterNewPeople(people3);
            await _dbContext.SaveChangesAsync();

            var peoplesList = await _peoplesRepository.FindAllPeopleOnOcurrence(occurrenceId);

            Assert.Equal(2, peoplesList.Count());
            Assert.Contains(peoplesList, p => p.PersonName == "Person 1");
            Assert.Contains(peoplesList, p => p.PersonName == "Person 2");
            Assert.DoesNotContain(peoplesList, p => p.PersonName == "Person 3");
        }
    }
}
