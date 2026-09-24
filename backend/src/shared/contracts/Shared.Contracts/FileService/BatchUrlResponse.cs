namespace Shared.Contracts.FileService
{
    public sealed record BatchUrlResponse(string Status, int ExpiresIn, List<FileUrl> Urls, List<FileError> Errors, string Reason);
    public sealed record FileError(string Key, string Reason);
    public sealed record FileUrl(string Key, string Url);


}