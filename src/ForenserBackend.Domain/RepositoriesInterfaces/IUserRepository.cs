using ForenserBackend.Domain.entities;

namespace ForenserBackend.Domain.RepositoriesInterfaces
{
    public interface IUserRepository
    {

        public Task RegisterNewUser(UserEntity userData);
        public void UpdateUser(UserEntity newUserData);
        public Task DeleteUser(string userId);

        public Task<UserEntity> GetUserById(string userId);
        public Task<UserEntity> GetUserByEmail(string userEmail);

        public Task<UserEntity> GetUserByCpf(string userCpf);

        public Task<List<UserEntity>> GetAllUsers();


    }
}
