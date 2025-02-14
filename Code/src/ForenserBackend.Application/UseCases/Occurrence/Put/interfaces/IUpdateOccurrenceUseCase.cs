using ForenserBackend.Communication.Requests;

namespace ForenserBackend.Application.UseCases.Occurrence.Put.interfaces
{
    public interface IUpdateOccurrenceUseCase
    {
        Task UpdateOccurrence(string token, string occurrenceId, OccurrenceRequestJson newOccurrenceData);
    }
}