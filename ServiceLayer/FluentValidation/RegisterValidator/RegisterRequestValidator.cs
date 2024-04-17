using EntityLayer.DTOs.Register;
using FluentValidation;
using ServiceLayer.Messages;

namespace ServiceLayer.FluentValidation.RegisterValidator
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequestDTO>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Email"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Email"))
                .EmailAddress().WithMessage(ValidationMessages.EmailMessage("Email"));

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Password"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Password"))
                .MinimumLength(7).WithMessage(ValidationMessages.GreaterThanMessage("Password", 7));
        }
    }
}
