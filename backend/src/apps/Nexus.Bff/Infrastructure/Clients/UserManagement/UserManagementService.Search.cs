using Crossdyne.Toolkit.Results;
using Shared.Contracts.UserManagement.Responses;

namespace Nexus.Bff.Infrastructure.Clients.UserManagement
{
    public partial class UserManagementService 
    {
        public async Task<Result<List<SearchUserResponse>>> SearchUsers(string? input)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/v1/users?input={input}");
                
                return await HandleResponse<List<SearchUserResponse>>(response);
            }
            catch (Exception ex)
            {
                return Result<List<SearchUserResponse>>.Failure(new Error(ErrorCode.Server, $"Ошибка в Api: {ex}"));
            }
        }
    }
}