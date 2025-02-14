using ForenserBackend.Domain.entities;

namespace ForenserBackend.Domain.RepositoriesInterfaces
{
    public interface IOccurrenceRepository
    {
        public Task CreateNewOccurrence(OccurrenceEntity occurrence);
        public void UpdateOccurrence(OccurrenceEntity occurrenceNewData);
        public Task DeleteOccurrence(string occurrenceId);

        public Task<OccurrenceEntity> GetOccurrenceDetailsById(string occurrenceId);

        public Task<List<OccurrenceEntity>> GetAllOccurrences();
    }
}
