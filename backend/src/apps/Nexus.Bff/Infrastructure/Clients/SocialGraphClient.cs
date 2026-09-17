using System.Text.Json;
using Crossdyne.Toolkit.Primitives;
using Crossdyne.Toolkit.Results;
using Microsoft.Extensions.Options;
using Nexus.Bff.Features.Users.Models;
using Shared.Kernel.Errors;

namespace Nexus.Bff.Infrastructure.Clients
{
    public sealed class SocialGraphClient(HttpClient http, IOptions<JsonSerializerOptions> jsonOptions) : ISocialGraphClient
    {
        public async Task<Result<Unit>> SendFriendRequest(BffSendFriendRequest request)
        {
            try
            {            
                var response = await http.PostAsJsonAsync("api/v1/friends", request, jsonOptions.Value);

                response.EnsureSuccessStatusCode();
                    
                return Unit.Value;
            }
            catch (Exception ex)
            {
                return Result<Unit>.Failure(new Error(AppErrors.Api, $"Произошла ошибка при отправке запроса на дружбу: {ex}"));
            }
        }
    }
}