using ForenserBackend.Communication.Requests;
using ForenserBackend.Communication.Response;


namespace ForenserBackend.Application.UseCases.User.Post.Interfaces
{
    public interface IAuthenticateUserUseCase
    {
        Task<AuthenticationResponse> AuthenticateUser(AuthenticationRequestJson request);
    }
}
