using ForenserBackend.Domain.entities;
using ForenserBackend.Exception.HttpErrors;
using ForenserBackend.Infrastructure;
using ForenserBackend.Infrastructure.repositories;
using Microsoft.EntityFrameworkCore;

namespace ForenserBackend.Tests.RepositoriesTest
{
    public class ImageRepositoryTests
    {
        private readonly ImageRepository _imageRepository;
        private readonly ForenserDbContext _dbContext;
        public ImageRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ForenserDbContext>()
                .UseInMemoryDatabase(databaseName: "ForenserTestDb")
                .Options;

            _dbContext = new ForenserDbContext(options);
            _imageRepository = new ImageRepository(_dbContext);

        }

        [Fact]
        public async Task GetAllImages_ShouldReturnAllImages()
        {
            // Arrange
            _dbContext.Images.Add(new ImageEntity { ImageSize=2, ImageUrl="testurl", Name="testname", OccurenceId="testId"});
            _dbContext.Images.Add(new ImageEntity {ImageSize=2, ImageUrl="testurl", Name="testname", OccurenceId="testId"});
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _imageRepository.GetAllImages();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetImageById_ShouldReturnImage_WhenImageExists()
        {
            // Arrange
            var image = new ImageEntity { ImageSize = 2, ImageUrl = "url1", Name = "testname", OccurenceId = "testId" };
            _dbContext.Images.Add(image);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _imageRepository.GetImageById(image.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("url1", result.ImageUrl);
        }

        [Fact]
        public async Task GetImageById_ShouldThrowNotFoundException_WhenImageDoesNotExist()
        {
            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _imageRepository.GetImageById("999"));
        }

        [Fact]
        public async Task RegisterImage_ShouldAddImageToDatabase()
        {
            // Arrange
            var image = new ImageEntity { ImageSize = 2, ImageUrl = "url3", Name = "testname", OccurenceId = "testId" };

            // Act
            await _imageRepository.RegisterImage(image);
            await _dbContext.SaveChangesAsync();

            // Assert
            var result = await _dbContext.Images.FindAsync(image.Id);
            Assert.NotNull(result);
            Assert.Equal("url3", result.ImageUrl);
        }

        [Fact]
        public async Task UnregisterImage_ShouldRemoveImageFromDatabase()
        {
            // Arrange
            var image = new ImageEntity { ImageSize = 2, ImageUrl = "url3", Name = "testname", OccurenceId = "testId" };
            _dbContext.Images.Add(image);
            await _dbContext.SaveChangesAsync();

            // Act
            await _imageRepository.UnregisterImage(image.Id);
            await _dbContext.SaveChangesAsync();

            // Assert
            var result = await _dbContext.Images.FindAsync(image.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task UnregisterImage_ShouldThrowNotFoundException_WhenImageDoesNotExist()
        {
            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _imageRepository.UnregisterImage("999"));
        }
    }
}

