namespace ForenserBackend.Application.UseCases.User.Delete.interfaces
{
    public interface IDeleteUserUseCase
    {
        Task DeleteUser(string userToken);
    }
}
