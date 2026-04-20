using ForenserBackend.Communication.Requests;

namespace ForenserBackend.Application.UseCases.User.Post.Interfaces
{
    public interface ICreateUserUseCase
    {
        Task RegisterUser(UserRequestJson userRequest);
    }
}
