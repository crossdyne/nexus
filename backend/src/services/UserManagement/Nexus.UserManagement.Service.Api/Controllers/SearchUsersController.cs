using Crossdyne.Toolkit.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.UserManagement.Service.Application.Features.Users.Queries.SearchUsers;
using Shared.Contracts.UserManagement.Responses;
using Shared.Web.Extensions;

namespace Nexus.UserManagement.Service.Api.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    [Authorize]
    public class SearchUsersController(IMediator mediator) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> SearchUsers([FromQuery] string? input, CancellationToken cancellationToken)
        {
            var query = new SearchUsersQuery(input);
            Result<List<SearchUserResponse>> result = await mediator.Send(query, cancellationToken);

            return result.Match(
                onSuccess: () => Ok(result.Value),
                onFailure: errors => this.MapActionResult(errors));
        }
    }
}