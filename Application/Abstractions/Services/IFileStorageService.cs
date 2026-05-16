namespace Application.Abstractions.Services
{
    public interface IFileStorageService
    {
        Task<string> UploadAsync(string folder, Stream fileStream, string fileName, CancellationToken ct = default);
        Task DeleteAsync(string fileUrl, CancellationToken ct = default);
        string GetFileUrl(string folder, string fileName);
    }
}