using Application.Abstractions.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Infrastructure.Storage
{
    public class ImageProcessor : IImageProcessor
    {
        private const long DefaultMaxFileSize = 5 * 1024 * 1024; // 5MB

        public long MaxFileSizeBytes => DefaultMaxFileSize;

        public string[] AllowedContentTypes => new[] { "image/jpeg", "image/png", "image/webp" };

        public string[] AllowedExtensions => new[] { ".jpg", ".jpeg", ".png", ".webp" };

        public bool IsValidImage(Stream imageStream, out string? error)
        {
            error = null;

            if (imageStream.Length > MaxFileSizeBytes)
            {
                error = $"File size exceeds maximum allowed size of {MaxFileSizeBytes / 1024 / 1024}MB";
                return false;
            }

            imageStream.Position = 0;

            try
            {
                var decoder = Image.DetectFormat(imageStream);
                
                if (decoder == null)
                {
                    error = "Unable to detect image format";
                    return false;
                }

                var mimeType = decoder.DefaultMimeType;
                
                if (!AllowedContentTypes.Contains(mimeType))
                {
                    error = $"Image format {mimeType} is not allowed. Allowed: {string.Join(", ", AllowedContentTypes)}";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                error = $"Invalid image: {ex.Message}";
                return false;
            }
            finally
            {
                imageStream.Position = 0;
            }
        }

        public async Task<Stream> ResizeAsync(Stream imageStream, int maxWidth, int maxHeight)
        {
            imageStream.Position = 0;

            using var image = await Image.LoadAsync(imageStream);

            var ratio = Math.Min((decimal)maxWidth / image.Width, (decimal)maxHeight / image.Height);

            if (ratio < 1)
            {
                var newWidth = (int)(image.Width * ratio);
                var newHeight = (int)(image.Height * ratio);

                image.Mutate(x => x.Resize(newWidth, newHeight));
            }

            var outputStream = new MemoryStream();
            
            await image.SaveAsWebpAsync(outputStream);
            
            outputStream.Position = 0;
            
            return outputStream;
        }
    }
}