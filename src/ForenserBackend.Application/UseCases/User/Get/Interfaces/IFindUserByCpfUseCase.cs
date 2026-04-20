using ForenserBackend.Communication.Response.Detailed;

namespace ForenserBackend.Application.UseCases.User.Get.Interfaces
{
    public interface IFindUserByCpfUseCase
    {
        Task<DetailedUserResponseJson> GetUserDetailsByCpf(string cpf);

    }
}
