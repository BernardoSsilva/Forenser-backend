using AutoMapper;
using ForenserBackend.Application.UseCases.User.Put.Interfaces;
using ForenserBackend.Application.Validators;
using ForenserBackend.Authentication.services;
using ForenserBackend.Communication.Requests;
using ForenserBackend.Domain;
using ForenserBackend.Domain.RepositoriesInterfaces;
using ForenserBackend.Exception;

namespace ForenserBackend.Application.UseCases.User.Put
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UpdateUserUseCase(IUnitOfWork unitOfWork, IUserRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task UpdateUser(string userToken, UserRequestJson newUserData)
        {
            TokenAdmin tokenAdmin = new TokenAdmin();

            tokenAdmin.ValidateToken(userToken);

            Validate(newUserData);

            var decodedToken = tokenAdmin.DecodeToken(userToken);
            var userToEdit = await _repository.GetUserById(decodedToken.UserId);

            var newData = _mapper.Map(newUserData, userToEdit);
            _repository.UpdateUser(newData);
            await _unitOfWork.Commit();
        }

        private void Validate(UserRequestJson data) {
            var validator = new UserValidator();
            var result = validator.Validate(data);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new MultipleErrorsException(errorMessages);
            }
        }
    }
}
