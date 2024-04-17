using EntityLayer.DTOs.API.Image;
using FluentValidation;
using ServiceLayer.Messages;

namespace ServiceLayer.FluentValidation.API.ImageValidator
{
    public class ImageUploadValidator : AbstractValidator<ImageUploadDTO>
    {
        public ImageUploadValidator()
        {
            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("File Name"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("File Name"))
                .MaximumLength(20).WithMessage(ValidationMessages.MaximumCharacterAllowence("File Name", 20));

            RuleFor(x => x.File)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("File Name"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("File Name"));
        }
    }
}
