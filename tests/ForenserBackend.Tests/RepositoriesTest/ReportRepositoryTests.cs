using ForenserBackend.Domain.entities;
using ForenserBackend.Exception.HttpErrors;
using ForenserBackend.Infrastructure;
using ForenserBackend.Infrastructure.repositories;
using Microsoft.EntityFrameworkCore;

namespace ForenserBackend.Tests.RepositoriesTest
{
    public class ReportRepositoryTests
    {
        private readonly ReportRepository _reportRepository;
        private readonly ForenserDbContext _dbContext;

        public ReportRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ForenserDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ForenserDbContext(options);
            _reportRepository = new ReportRepository(_dbContext);
        }

        [Fact]
        public async Task ReportRepository_ShouldBeAbleToCreateANewReport()
        {
            var newReport = new ReportEntity
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Initial Title",
                City = "Initial city",
                CreatedAt = DateTime.Now,
                ContactPhone = "testPhone",
                Description = "test description",
                ReportedPeopleName = "test name",
                ReportingDate = DateTime.Now,
                State = Domain.Enums.Ufs.RO,
                Street = "test speed"
            };

            await _reportRepository.CreateNewReport(newReport);
            await _dbContext.SaveChangesAsync();

            var registeredReport = await _reportRepository.GetReportById(newReport.Id);
            Assert.NotNull(registeredReport);
            Assert.Equal(newReport.Title, registeredReport.Title);
        }

        [Fact]
        public async Task ReportRepository_ShouldThrowErrorWhenReportNotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _reportRepository.GetReportById("InvalidId"));
        }

        [Fact]
        public async Task ReportRepository_ShouldUpdateReport()
        {
            var newReport = new ReportEntity
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Initial Title",
                City = "Initial city",
                CreatedAt = DateTime.Now,
                ContactPhone = "testPhone",
                Description="test description",
                ReportedPeopleName="test name",
                ReportingDate= DateTime.Now,
                State = Domain.Enums.Ufs.RO,
                Street = "test speed"
            };

            await _reportRepository.CreateNewReport(newReport);
            await _dbContext.SaveChangesAsync();

            newReport.Title = "Updated Title";
            _reportRepository.UpdateReport(newReport);
            await _dbContext.SaveChangesAsync();

            var updatedReport = await _reportRepository.GetReportById(newReport.Id);
            Assert.Equal("Updated Title", updatedReport.Title);
        }

        [Fact]
        public async Task ReportRepository_ShouldDeleteReport()
        {
            var newReport = new ReportEntity
            {
              
                Id = Guid.NewGuid().ToString(),
                Title = "Initial Title",
                City = "Initial city",
                CreatedAt = DateTime.Now,
                ContactPhone = "testPhone",
                Description="test description",
                ReportedPeopleName="test name",
                ReportingDate= DateTime.Now,
                State = Domain.Enums.Ufs.RO,
                Street = "test speed"
            
            };

            await _reportRepository.CreateNewReport(newReport);
            await _dbContext.SaveChangesAsync();

            await _reportRepository.DeleteReport(newReport.Id);
            await _dbContext.SaveChangesAsync();

            await Assert.ThrowsAsync<NotFoundException>(async () => await _reportRepository.GetReportById(newReport.Id));
        }

        [Fact]
        public async Task ReportRepository_ShouldThrowErrorIfDeleteNonExistentReport()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _reportRepository.DeleteReport("InvalidId"));
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
