namespace Infrastructure.Storage
{
    public class StorageOptions
    {
        public string Provider { get; set; } = "Local";
        
        public LocalStorageOptions Local { get; set; } = new();
        
        public S3StorageOptions S3 { get; set; } = new();
    }

    public class LocalStorageOptions
    {
        public string BasePath { get; set; } = "uploads";
    }

    public class S3StorageOptions
    {
        public string BucketName { get; set; } = string.Empty;
        public string Region { get; set; } = "us-east-1";
        public string AccessKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string? Endpoint { get; set; }
        public string PublicUrl { get; set; } = string.Empty;
    }
}