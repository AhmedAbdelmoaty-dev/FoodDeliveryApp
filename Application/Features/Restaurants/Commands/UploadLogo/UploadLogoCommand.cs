using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Restaurants.Commands.UploadLogo
{
    public record UploadLogoCommand(Guid RestaurantId, byte[] ImageData, string FileName, string ContentType)
        : IRequest<Result<string>>;

    public class UploadLogoCommandValidator : AbstractValidator<UploadLogoCommand>
    {
        private const long MaxFileSize = 5 * 1024 * 1024; // 5MB

        public UploadLogoCommandValidator()
        {
            RuleFor(x => x.RestaurantId)
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