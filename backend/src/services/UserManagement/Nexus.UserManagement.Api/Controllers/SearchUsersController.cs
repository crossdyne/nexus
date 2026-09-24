using Crossdyne.Toolkit.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.UserManagement.Api.Extensions;
using Nexus.UserManagement.Api.Models;
using Nexus.UserManagement.Application.Features.Users.Queries.RequestsInfo;
using Nexus.UserManagement.Application.Features.Users.Queries.SearchUsers;
using Shared.Contracts.UserManagement.Requests;
using Shared.Contracts.UserManagement.Responses;
using Shared.Web.Extensions;

namespace Nexus.UserManagement.Api.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    [Authorize]
    public class SearchUsersController(IMediator mediator) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> SearchUsers([FromQuery] string? input, CancellationToken cancellationToken)
        {
            Result<ExtractData> extractResult = this.ExtractCredentials(User, out IActionResult actionResult);
            
            if (extractResult.IsFailure)
                return actionResult;
                
            var query = new SearchUsersQuery(input, extractResult.Value.Login);
            Result<List<SearchUserResponse>> result = await mediator.Send(query, cancellationToken);

            return result.Match(
                onSuccess: () => Ok(result.Value),
                onFailure: errors => this.MapActionResult(errors));
        }

        [HttpPost("requests/info")]
        public async Task<IActionResult> RequestsInfo([FromBody] RequestsInfoRequest request)
        {
            var query = new RequestsInfoQuery(request.UserIds);
            Result<List<RequestsInfoResponse>> result = await mediator.Send(query);

            return result.Match(
                onSuccess: () => Ok(result.Value),
                onFailure: errors => this.MapActionResult(errors));
        }
    }
}