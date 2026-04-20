using ForenserBackend.Communication.Requests;

namespace ForenserBackend.Application.UseCases.User.Put.Interfaces
{
    public interface IUpdateUserUseCase
    {
        Task UpdateUser(string userToken, UserRequestJson newUserData);
    }
}
