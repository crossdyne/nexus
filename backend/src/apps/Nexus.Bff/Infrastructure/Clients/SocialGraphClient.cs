using System.Text.Json;
using Crossdyne.Toolkit.Primitives;
using Crossdyne.Toolkit.Results;
using Microsoft.Extensions.Options;
using Nexus.Bff.Features.Users.Models;
using Shared.Contracts.SocialGraph;
using Shared.Kernel.Errors;

namespace Nexus.Bff.Infrastructure.Clients
{
    public sealed class SocialGraphClient(HttpClient http, IOptions<JsonSerializerOptions> jsonOptions) : ISocialGraphClient
    {
        public async Task<Result<Unit>> SendFriendRequest(BffSendFriendRequest request)
        {
            try
            {            
                var response = await http.PostAsJsonAsync("api/v1/friends/request/send", request, jsonOptions.Value);

                response.EnsureSuccessStatusCode();
                    
                return Unit.Value;
            }
            catch (Exception ex)
            {
                return Result<Unit>.Failure(new Error(AppErrors.Api, $"Произошла ошибка при отправке запроса на дружбу: {ex}"));
            }
        }

        public async Task<Result<List<IncomingFriendResponse>>> IncomingFriendRequests()
        {
            try
            {            
                var response = await http.GetAsync("api/v1/friends/request/incoming");
                response.EnsureSuccessStatusCode();
                    
                return await response.Content.ReadFromJsonAsync<List<IncomingFriendResponse>>(jsonOptions.Value);
            }
            catch (Exception ex)
            {
                return new Error(AppErrors.Api, $"Произошла ошибка при отправке запроса на дружбу: {ex}");
            }
        }

        public async Task<Result<List<OutgoingFriendResponse>>> OutgoingFriendRequests()
        {
            try
            {            
                var response = await http.GetAsync("api/v1/friends/request/outgoing");
                response.EnsureSuccessStatusCode();
                    
                return await response.Content.ReadFromJsonAsync<List<OutgoingFriendResponse>>(jsonOptions.Value);
            }
            catch (Exception ex)
            {
                return new Error(AppErrors.Api, $"Произошла ошибка при отправке запроса на дружбу: {ex}");
            }
        }
    }
}