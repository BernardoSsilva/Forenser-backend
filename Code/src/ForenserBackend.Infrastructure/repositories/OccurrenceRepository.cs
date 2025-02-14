using ForenserBackend.Domain.entities;
using ForenserBackend.Domain.RepositoriesInterfaces;
using ForenserBackend.Exception.HttpErrors;
using Microsoft.EntityFrameworkCore;

namespace ForenserBackend.Infrastructure.repositories
{
    public class OccurrenceRepository : IOccurrenceRepository
    {
        private readonly ForenserDbContext _context;

        public OccurrenceRepository(ForenserDbContext context)
        {
            _context = context;
        }
        public async Task CreateNewOccurrence(OccurrenceEntity occurrence)
        {
            await _context.AddAsync(occurrence);
        }

        public async Task DeleteOccurrence(string occurrenceId)
        {
            var occurrenceToDelete = await _context.Occurrences.FirstOrDefaultAsync(occurrence => occurrence.Id == occurrenceId);
            if (occurrenceToDelete is null)
            {
                throw new NotFoundException("Occurrence not found");
            }
            _context.Remove(occurrenceToDelete);
        }

        public async Task<List<OccurrenceEntity>> GetAllOccurrences()
        {
            return await _context.Occurrences.AsNoTracking().ToListAsync();
        }

        public async Task<OccurrenceEntity> GetOccurrenceDetailsById(string occurrenceId)
        {
            var occurrence = await _context.Occurrences.AsNoTracking().FirstOrDefaultAsync(occurrence => occurrence.Id == occurrenceId);
            if (occurrence is null)
            {
                throw new NotFoundException("Occurrence not found");
            }
            return occurrence;
        }

        public void UpdateOccurrence(OccurrenceEntity occurrenceNewData)
        {
            _context.Update(occurrenceNewData);
        }

     
    }
}
