using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.UserManagement.Application.Features.UserInternal.Queries.GetByLoginInternal;
using Nexus.UserManagement.Application.Features.Users.Queries.ByInviteCode;
using Nexus.UserManagement.Application.Features.Users.Queries.GetById;
using Shared.Contracts.UserManagement.Responses;
using Shared.Web.Extensions;

namespace Nexus.UserManagement.Api.Controllers
{
    [ApiController]
    [Route("internal/api/users")]
    public class InternalUserController(IMediator mediator) : Controller
    {
        [HttpGet("by-login/{login}")]
        [AllowAnonymous]
        public async Task<IActionResult> ByLogin([FromRoute] string login)
        {
            var query = new GetUserByLoginInternalQuery(login);

            var result = await mediator.Send(query);

            return result.Match(
                onSuccess: Ok,
                onFailure: errors => this.MapActionResult(errors));
        }

        [HttpGet("by-id/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginById([FromRoute] Guid id)
        {
            var query = new GetUserByIdQuery(id);

            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("by-invite-code/{inviteCode}")]
        [AllowAnonymous]
        public async Task<IActionResult> ByInviteCode([FromRoute] string inviteCode)
        {
            var query = new GetByInviteCodeQuery(inviteCode);
            ByInviteCodeResponse result = await mediator.Send(query);

            return Ok(result);
        }
    }
}
