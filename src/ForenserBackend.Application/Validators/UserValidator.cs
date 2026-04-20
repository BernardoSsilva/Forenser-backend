using FluentValidation;
using ForenserBackend.Communication.Requests;

namespace ForenserBackend.Application.Validators
{
    public class UserValidator: AbstractValidator<UserRequestJson>
    {
        public UserValidator()
        {
            {
                RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(8).WithMessage("The password must have 8 or more characters");
                RuleFor(user => user.UserName).NotEmpty().WithMessage("The user name is required");
                RuleFor(user => user.UserEmail).NotEmpty().WithMessage("The user email is required");
                RuleFor(user => user.CPF).NotEmpty().WithMessage("The user CPF is required");
                RuleFor(user => user.CPF.Length).NotEqual(14).WithMessage("The cpf must have 14 characters");
            }
        }

    }
}
