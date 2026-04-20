using ForenserBackend.Communication.Response.Detailed;

namespace ForenserBackend.Application.UseCases.User.Get.Interfaces
{
    public interface IFindUserByIdUseCase
    {
        Task<DetailedUserResponseJson> GetUserDetailsById(string id);
    }
}
