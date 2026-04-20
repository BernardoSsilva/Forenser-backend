using ForenserBackend.Domain.entities;

namespace ForenserBackend.Tests.EntitiesTests
{
    public class ImageEntityTests
    {
        [Fact]
        public void ImageEntity_ShouldInitializeWithDefaultValues()
        {
            
            var image = new ImageEntity
            {
                Name = "Sample Image",
                ImageUrl = "https://example.com/image.jpg",
                OccurenceId = "occurence123",
                ImageSize = 2.5 
            }; ;

            Assert.NotNull(image.Id);
            Assert.Equal(string.Empty, image.Description);
        }

        [Fact]
        public void ImageEntity_ShouldRequireMandatoryFields()
        {
            var image = new ImageEntity
            {
                Name = "Sample Image",
                ImageUrl = "https://example.com/image.jpg",
                OccurenceId = "occurence123",
                ImageSize = 2.5 

            };

            Assert.Equal("Sample Image", image.Name);
            Assert.Equal("https://example.com/image.jpg", image.ImageUrl);
            Assert.Equal("occurence123", image.OccurenceId);
            Assert.Equal(2.5, image.ImageSize);
        }

        [Fact]
        public void ImageEntity_ShouldAllowSettingOptionalDescription()
        {
            
            var image = new ImageEntity
            {
                Name = "Sample Image",
                ImageUrl = "https://example.com/image.jpg",
                OccurenceId = "occurence123",
                ImageSize = 2.5,
                Description = "This is a sample image description."
            };

            Assert.Equal("This is a sample image description.", image.Description);
        }
    }
}

