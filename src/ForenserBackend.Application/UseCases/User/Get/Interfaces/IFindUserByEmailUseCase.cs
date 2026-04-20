using ForenserBackend.Communication.Response.Detailed;

namespace ForenserBackend.Application.UseCases.User.Get.Interfaces
{
    public interface IFindUserByEmailUseCase
    {
        Task<DetailedUserResponseJson> GetUserDetailsByEmail(string email);

    }
}
