using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Restaurants.Commands.UploadMenuItemImage
{
    public record UploadMenuItemImageCommand(Guid RestaurantId, Guid MenuItemId, byte[] ImageData, string FileName, string ContentType)
        : IRequest<Result<string>>;

    public class UploadMenuItemImageCommandValidator : AbstractValidator<UploadMenuItemImageCommand>
    {
        public UploadMenuItemImageCommandValidator()
        {
            RuleFor(x => x.RestaurantId)
                .NotEmpty();

            RuleFor(x => x.MenuItemId)
                .NotEmpty();

            RuleFor(x => x.ImageData)
                .NotEmpty()
                .WithMessage("Image is required");

            RuleFor(x => x.FileName)
                .NotEmpty()
                .Must(BeValidExtension)
                .WithMessage("Invalid file extension. Allowed: jpg, jpeg, png, webp");

            RuleFor(x => x.ContentType)
                .Must(BeValidContentType)
                .WithMessage("Invalid content type. Allowed: image/jpeg, image/png, image/webp");
        }

        private static bool BeValidExtension(string fileName)
        {
            var ext = Path.GetExtension(fileName)?.ToLowerInvariant();
            return ext is ".jpg" or ".jpeg" or ".png" or ".webp";
        }

        private static bool BeValidContentType(string contentType)
        {
            return contentType is "image/jpeg" or "image/png" or "image/webp";
        }
    }
}