using EntityLayer.DTOs.API.User;
using FluentValidation;
using ServiceLayer.Messages;

namespace ServiceLayer.FluentValidation.API.UserValidator
{
    public class UserUpdateValidation : AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Name"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Name"))
                .MaximumLength(50).WithMessage(ValidationMessages.MaximumCharacterAllowence("Name", 50));

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Email"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Email"))
                .MaximumLength(100).WithMessage(ValidationMessages.MaximumCharacterAllowence("Email", 100))
                .EmailAddress().WithMessage(ValidationMessages.EmailMessage("Email"));
        }
    }
}
