using EntityLayer.DTOs.Comment;
using FluentValidation;
using ServiceLayer.Messages;

namespace ServiceLayer.FluentValidation.CommentValidator
{
    public class CommentCreateValidation : AbstractValidator<CommentCreateDTO>
    {
        public CommentCreateValidation()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Content"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Content"))
                .MaximumLength(250).WithMessage(ValidationMessages.MaximumCharacterAllowence("Content", 250));

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("UserId"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("UserId"));

            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("PostId"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("PostId"));
        }
    }
}
