using ForenserBackend.Domain.entities;
using ForenserBackend.Domain.RepositoriesInterfaces;
using ForenserBackend.Exception.HttpErrors;
using Microsoft.EntityFrameworkCore;

namespace ForenserBackend.Infrastructure.repositories
{
    public class PeoplesRepository : IPeopleRepository
    {
        private readonly ForenserDbContext _context;

        public PeoplesRepository(ForenserDbContext context)
        {
            _context = context;
        }

        public async Task DeletePeopleRegister(string peopleId)
        {
            var peopleRegisterToDelete = await _context.Peoples.FirstOrDefaultAsync(people => people.Id == peopleId);
            if (peopleRegisterToDelete is null)
            {
                throw new NotFoundException("People not found");
            }

            _context.Remove(peopleRegisterToDelete);
        }

        public async Task<List<PeopleEntity>> FindAllPeople()
        {
            return await _context.Peoples.ToListAsync(); ;
        }

        public async Task<List<PeopleEntity>> FindAllPeopleOnOcurrence(string occurenceId)
        {
            var peoplesList = await _context.Peoples.ToListAsync();
            return peoplesList.Where(e => e.OccurrenceId == occurenceId).ToList();
        }

        public async Task<PeopleEntity> FindPeopleById(string peopleId)
        {
            var people = await _context.Peoples.AsNoTracking().FirstOrDefaultAsync(e => e.Id == peopleId);
            if (people is null)
            {
                throw new NotFoundException("People not found");
            }
            return people;
        }

        public async Task RegisterNewPeople(PeopleEntity peopleData)
        {
           await _context.AddAsync(peopleData);
        }

        public void UpdatePeopleData(PeopleEntity newPeopleData)
        {
            _context.Update(newPeopleData);

        }
    }
}
