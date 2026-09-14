namespace Shared.Contracts.FileService
{
    public sealed record BatchUrlRequest(List<FileRequest> Files, int? Expires);
    public sealed record FileRequest(string Bucket, string Folder, string Key);
}