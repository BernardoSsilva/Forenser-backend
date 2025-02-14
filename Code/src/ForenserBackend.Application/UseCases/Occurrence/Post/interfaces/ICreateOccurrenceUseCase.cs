
using ForenserBackend.Communication.Requests;

namespace ForenserBackend.Application.UseCases.Occurrence.Post.interfaces
{
    public interface ICreateOccurrenceUseCase
    {
        Task CreateOccurrence(string userToken, OccurrenceRequestJson occurrence);
    }
}
