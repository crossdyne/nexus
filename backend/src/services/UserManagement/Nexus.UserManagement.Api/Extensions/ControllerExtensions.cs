using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Crossdyne.Toolkit.Results;
using Microsoft.AspNetCore.Mvc;
using Nexus.UserManagement.Api.Models;

namespace Nexus.UserManagement.Api.Extensions
{
    public static class ControllerExtensions
    {
        public static Result<ExtractData> ExtractCredentials(this Controller controller, ClaimsPrincipal user, out IActionResult actionResult)
        {
            var extractData = new ExtractData();

            var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var login = user.FindFirstValue(JwtRegisteredClaimNames.Name);

            if (string.IsNullOrEmpty(userIdString))
            {
                actionResult = controller.Unauthorized("User ID не найден в токене.");
                return new Error(ErrorCode.Unauthorized, "");
            }
                
            if (!Guid.TryParse(userIdString, out var userIdGuid))
            {
                 actionResult = controller.BadRequest("Не верный User ID формат.");
                 return new Error(ErrorCode.BadRequest, "Формат идентификатора был не верный");
            }

            if (string.IsNullOrEmpty(login))
            {
                actionResult = controller.Unauthorized("Login не найден в токене.");
                return new Error(ErrorCode.Unauthorized, "Login отсутствует в claims");
            }

            actionResult = controller.Ok();
            extractData.UserId = userIdGuid;
            extractData.Login = login;

            return extractData;
        }
    }
}