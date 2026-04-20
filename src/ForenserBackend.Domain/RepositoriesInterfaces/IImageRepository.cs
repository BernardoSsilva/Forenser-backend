
using ForenserBackend.Domain.entities;

namespace ForenserBackend.Domain.RepositoriesInterfaces
{
    public interface IImageRepository
    {
        public Task RegisterImage(ImageEntity image);
        public Task UnregisterImage(string imageId);

        public Task<ImageEntity> GetImageById(string imageId);
        public Task<List<ImageEntity>> GetAllImages();
    }
}
