using ForenserBackend.Application.UseCases.Occurrence.Delete.interfaces;
using ForenserBackend.Authentication.services;
using ForenserBackend.Domain;
using ForenserBackend.Domain.RepositoriesInterfaces;

namespace ForenserBackend.Application.UseCases.Occurrence.Delete
{
    public class DeleteOccurrenceUseCase : IDeleteOccurrenceUseCase
    {
        private readonly IOccurrenceRepository _occurrenceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOccurrenceUseCase(IOccurrenceRepository occurrenceRepository, IUnitOfWork unitOfWork)
        {
            _occurrenceRepository = occurrenceRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task DeleteOccurrence(string userToken, string occurrenceId)
        {
            TokenAdmin tokenAdmin = new TokenAdmin();
            tokenAdmin.ValidateToken(userToken);

            await _occurrenceRepository.DeleteOccurrence(occurrenceId);
            await _unitOfWork.Commit();
        }
    }
}
