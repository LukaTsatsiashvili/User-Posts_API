using EntityLayer.DTOs.Post;
using FluentValidation;
using ServiceLayer.Messages;

namespace ServiceLayer.FluentValidation.PostValidator
{
    public class PostCreateValidation : AbstractValidator<PostCreateDTO>
    {
        public PostCreateValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Title"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Title"))
                .MaximumLength(50).WithMessage(ValidationMessages.MaximumCharacterAllowence("Title", 50));

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Content"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Content"))
                .MaximumLength(5000).WithMessage(ValidationMessages.MaximumCharacterAllowence("Content", 5000));

            RuleFor(x => x.AuthorId)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("AuthorId"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("AuthorId"));
        }
    }
}
