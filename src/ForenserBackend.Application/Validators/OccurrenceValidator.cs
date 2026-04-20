

using FluentValidation;
using ForenserBackend.Communication.Requests;
using ForenserBackend.Domain.Enums;

namespace ForenserBackend.Application.Validators
{
    public class OccurrenceValidator : AbstractValidator<OccurrenceRequestJson>
    {
        public OccurrenceValidator()
        {
            {
                RuleFor(occurrence => occurrence.UserId).NotEmpty().WithMessage("User Id required");
                RuleFor(occurrence => occurrence.OccurrenceStreet).NotEmpty().WithMessage("Occurrence street is required");
                RuleFor(occurrence => occurrence.OccurrenceCity).NotEmpty().WithMessage("Occurrence city is required");
                RuleFor(occurrence => occurrence.OccurrenceState).NotEmpty().WithMessage("Occurrence state is required");
                RuleFor(occurrence => occurrence.OccurrenceState.GetType()).NotEqual(typeof(Ufs)).WithMessage("Occurrence state type must be of type 'UF'");
                RuleFor(occurrence => occurrence.Type).NotEmpty().WithMessage("Occurrence type cannot be empty");
                RuleFor(occurrence => occurrence.Type.GetType()).NotEqual(typeof(OccurrenceType)).WithMessage("Occurrence type must be of type 'OccurrenceType'");
            }
        }
    }
}
