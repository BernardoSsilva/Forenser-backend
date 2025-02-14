

using AutoMapper;
using ForenserBackend.Application.UseCases.Occurrence.Post.interfaces;
using ForenserBackend.Application.Validators;
using ForenserBackend.Authentication.services;
using ForenserBackend.Communication.Requests;
using ForenserBackend.Domain;
using ForenserBackend.Domain.entities;
using ForenserBackend.Domain.RepositoriesInterfaces;
using ForenserBackend.Exception;
namespace ForenserBackend.Application.UseCases.Occurrence.Post
{
    public class CreateOccurrenceUseCase : ICreateOccurrenceUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOccurrenceRepository _occurrenceRepository;
        private readonly IMapper _mapper;
        public CreateOccurrenceUseCase(IUnitOfWork unitOfWork, IOccurrenceRepository occurrenceRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _occurrenceRepository = occurrenceRepository;
            _mapper = mapper;
        }

        public async Task CreateOccurrence(string userToken, OccurrenceRequestJson occurrence)
        {
            TokenAdmin tokenAdmin = new TokenAdmin();
            tokenAdmin.ValidateToken(userToken);

            Validate(occurrence);

            var newOccurrence = _mapper.Map<OccurrenceEntity>(occurrence);

            await _occurrenceRepository.CreateNewOccurrence(newOccurrence);
            await _unitOfWork.Commit();

        }

        public void Validate(OccurrenceRequestJson occurrenceToValidate)
        {
            OccurrenceValidator validator = new OccurrenceValidator();
            var result = validator.Validate(occurrenceToValidate);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new MultipleErrorsException(errorMessages);
            }
        }
    }

}
