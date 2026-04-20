using ForenserBackend.Domain.entities;

namespace ForenserBackend.Domain.RepositoriesInterfaces
{
    public interface IPeopleRepository
    {
        Task RegisterNewPeople(PeopleEntity peopleData);
        Task DeletePeopleRegister (string peopleId);

        void UpdatePeopleData(PeopleEntity newPeopleData);

        Task<PeopleEntity> FindPeopleById(string peopleId);

        Task<List<PeopleEntity>> FindAllPeople();

        Task<List<PeopleEntity>> FindAllPeopleOnOcurrence(string occurenceId);


    }
}
