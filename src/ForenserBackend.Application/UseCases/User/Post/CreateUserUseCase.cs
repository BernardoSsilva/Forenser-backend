using AutoMapper;
using ForenserBackend.Application.UseCases.User.Post.Interfaces;
using ForenserBackend.Application.Validators;
using ForenserBackend.Communication.Requests;
using ForenserBackend.Domain;
using ForenserBackend.Domain.entities;
using ForenserBackend.Domain.RepositoriesInterfaces;
using ForenserBackend.Exception;
using MD5Hash;
using System.ComponentModel.DataAnnotations;

namespace ForenserBackend.Application.UseCases.User.Post
{
    public class CreateUserUseCase:ICreateUserUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repository;

        public CreateUserUseCase(IMapper mapper, IUnitOfWork unitOfWork, IUserRepository repository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task RegisterUser(UserRequestJson userRequest)
        {
            Validate(userRequest);

            var newUserEntity = _mapper.Map<UserEntity>(userRequest);

            newUserEntity.Password = userRequest.Password.GetMD5();
            await _repository.RegisterNewUser(newUserEntity);
            await _unitOfWork.Commit();
        }

        public void Validate(UserRequestJson userData) {

            UserValidator validator = new UserValidator();

            var result = validator.Validate(userData);

            if (!result.IsValid) {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new MultipleErrorsException(errorMessages);
            }

        }
    }
}
