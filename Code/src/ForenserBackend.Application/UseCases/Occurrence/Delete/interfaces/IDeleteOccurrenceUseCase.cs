namespace ForenserBackend.Application.UseCases.Occurrence.Delete.interfaces
{
    public interface IDeleteOccurrenceUseCase
    {
        Task DeleteOccurrence(string userToken, string occurrenceId);
    }
}