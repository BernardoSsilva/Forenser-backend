using AutoMapper;
using ForenserBackend.Application.UseCases.Occurrence.Put.interfaces;
using ForenserBackend.Application.Validators;
using ForenserBackend.Authentication.services;
using ForenserBackend.Communication.Requests;
using ForenserBackend.Domain;
using ForenserBackend.Domain.entities;
using ForenserBackend.Domain.RepositoriesInterfaces;
using ForenserBackend.Exception;

namespace ForenserBackend.Application.UseCases.Occurrence.Put
{
    public class UpdateOccurrenceUseCase : IUpdateOccurrenceUseCase
    {
        private readonly IOccurrenceRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOccurrenceUseCase(IOccurrenceRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task UpdateOccurrence(string userToken, string occurrenceId, OccurrenceRequestJson newOccurrenceData)
        {
            TokenAdmin tokenAdmin = new TokenAdmin();
            tokenAdmin.ValidateToken(userToken);
            Validate(newOccurrenceData);

            OccurrenceEntity newOccurrenceEntity = _mapper.Map<OccurrenceEntity>(newOccurrenceData);

            newOccurrenceEntity.Id = occurrenceId;

            _repository.UpdateOccurrence(newOccurrenceEntity);
            await _unitOfWork.Commit();
        }

        private void Validate(OccurrenceRequestJson occurrenceToValidate)
        {
            var validator = new OccurrenceValidator();
            var result = validator.Validate(occurrenceToValidate);

            if (!result.IsValid)
            {
                List<string> errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new MultipleErrorsException(errorMessages);
            }
        }

    }
}