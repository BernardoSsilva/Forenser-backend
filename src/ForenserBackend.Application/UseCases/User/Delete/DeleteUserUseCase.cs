using ForenserBackend.Application.UseCases.User.Delete.interfaces;
using ForenserBackend.Domain.RepositoriesInterfaces;
using ForenserBackend.Domain;
using ForenserBackend.Authentication.services;

namespace ForenserBackend.Application.UseCases.User.Delete
{
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repository;

        public DeleteUserUseCase(IUnitOfWork unitOfWork, IUserRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task DeleteUser(string userToken)
        {
            TokenAdmin tokenAdmin = new TokenAdmin();

            tokenAdmin.ValidateToken(userToken);

            var decodedToken = tokenAdmin.DecodeToken(userToken);
            await _repository.DeleteUser(decodedToken.UserId);
            await _unitOfWork.Commit();
        }
    }
}
