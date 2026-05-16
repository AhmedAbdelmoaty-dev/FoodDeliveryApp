namespace Application.Abstractions.Services
{
    public interface IImageProcessor
    {
        Task<Stream> ResizeAsync(Stream imageStream, int maxWidth, int maxHeight);
        bool IsValidImage(Stream imageStream, out string? error);
        long MaxFileSizeBytes { get; }
        string[] AllowedContentTypes { get; }
        string[] AllowedExtensions { get; }
    }
}