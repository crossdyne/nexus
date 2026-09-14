using Crossdyne.Toolkit.Results;
using Shared.Contracts.FileService;

namespace Nexus.Bff.Infrastructure.Clients
{
    public interface IFileService
    {
        Task<Result<BatchUrlResponse>> GetUrls(BatchUrlRequest request, CancellationToken cancellationToken = default);
        Task<Result<string>> GetUrl(string bucket, string folder, string key);
    }
}