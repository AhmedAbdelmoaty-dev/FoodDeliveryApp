using Application.Abstractions.Services;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Storage
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _basePath;
        private readonly IWebHostEnvironment _environment;

        public LocalFileStorageService(IWebHostEnvironment environment)
        {
            _environment = environment;
            _basePath = Path.Combine(_environment.WebRootPath ?? _environment.ContentRootPath, "uploads");
            
            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }
        }

        public async Task<string> UploadAsync(string folder, Stream fileStream, string fileName, CancellationToken ct = default)
        {
            var folderPath = Path.Combine(_basePath, folder);
            
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var uniqueFileName = $"{Guid.NewGuid()}-{fileName}";
            var filePath = Path.Combine(folderPath, uniqueFileName);

            await using var outputStream = new FileStream(filePath, FileMode.Create);
            await fileStream.CopyToAsync(outputStream, ct);

            return $"/uploads/{folder}/{uniqueFileName}";
        }

        public Task DeleteAsync(string fileUrl, CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(fileUrl))
                return Task.CompletedTask;

            var uri = new Uri(fileUrl);
            var relativePath = uri.AbsolutePath;
            
            var filePath = Path.Combine(_environment.WebRootPath ?? _environment.ContentRootPath, relativePath.TrimStart('/'));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }

        public string GetFileUrl(string folder, string fileName)
        {
            return $"/uploads/{folder}/{fileName}";
        }
    }
}