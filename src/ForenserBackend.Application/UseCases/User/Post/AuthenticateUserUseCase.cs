using ForenserBackend.Application.UseCases.User.Post.Interfaces;
using ForenserBackend.Authentication;
using ForenserBackend.Authentication.services;
using ForenserBackend.Communication.Requests;
using ForenserBackend.Communication.Response;
using ForenserBackend.Domain.RepositoriesInterfaces;
using ForenserBackend.Exception.HttpErrors;
using MD5Hash;

namespace ForenserBackend.Application.UseCases.User.Post
{
    public class AuthenticateUserUseCase:IAuthenticateUserUseCase
    {
        private readonly IUserRepository _repository;

        public AuthenticateUserUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<AuthenticationResponse> AuthenticateUser(AuthenticationRequestJson request)
        {
            var userToAuthenticate = await _repository.GetUserByCpf(request.UserCpf);

            if (userToAuthenticate is null) {
                throw new NotFoundException("User not found");
            }
            if(request.UserPassword.GetMD5()!= userToAuthenticate.Password)
            {
                throw new UnauthorizedAccessException("Access Denied");
            }


            TokenPayload newPayload = new TokenPayload
            {
                UserEmail = userToAuthenticate.UserEmail,
                UserId = userToAuthenticate.Id,
                UserName = userToAuthenticate.UserName,
            };

            TokenAdmin tokenAdmin = new TokenAdmin();

            var newToken = tokenAdmin.Generate(newPayload);

            return new AuthenticationResponse
            {
                Token = newToken
            };
        }
    }
}
