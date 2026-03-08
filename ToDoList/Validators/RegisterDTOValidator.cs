using FluentValidation;
using ToDoList.DTO.Register;

namespace ToDoList.Validators
{
    public class RegisterDTOValidator :AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(x => x.accountDTO.Email)
                .NotEmpty().WithMessage("email feild is empty")
                .EmailAddress().WithMessage("the format is invalid");

            RuleFor(x => x.accountDTO.Password)
                .NotEmpty().WithMessage("password feild is empty");

            RuleFor(x => x.accountDTO.username)
                .NotEmpty().WithMessage("username feild is empty");

            RuleFor(x => x.profileDTO.DisplayName)
                .NotEmpty().WithMessage("display name feild is empty");

        }
    }
}
